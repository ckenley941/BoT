using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;
using System.Linq.Expressions;

namespace BucketOfThoughts.FileService
{
    public abstract class IngestionFileProcessorBase
    {
        protected virtual string ConstructErrorMessageForInvalidField(string fieldName, string fieldValue, int? rowNumber = null)
        {
            return rowNumber.HasValue
                ? $"Invalid value '{fieldValue}' for field {fieldName} on row number {rowNumber}."
                : $"Invalid value '{fieldValue}' for field {fieldName}.";
        }

        //protected virtual ImportFile CreateImportFile(string createdByEmail, ImportFileType fileType)
        //{
        //    var createdDate = DateTime.UtcNow;
        //    return new ImportFile
        //    {
        //        CreatedBy = createdByEmail,
        //        CreatedDate = createdDate,
        //        FileType = fileType,
        //    };
        //}

        protected virtual async Task<IEnumerable<TData>> GetRecordsAndLogErrors<TData, TClassMap>(
            FileStream stream,
            ICsvProcessor csvProcessor,
            FileIngestionResult fileIngestionResult,
            FileIngestionOptions options = null,
            bool unsuccessfulOnError = false
        ) where TClassMap : ClassMap<TData>
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = args =>
                {
                    var currentFieldName = args.Context.Reader.HeaderRecord?[args.Context.Reader.CurrentIndex];
                    var message = ConstructErrorMessageForInvalidField(currentFieldName, args.Field, args.Context.Reader.Parser.Row);
                    if (unsuccessfulOnError)
                    {
                        fileIngestionResult.Errors.Add(message);
                    }
                    else
                    {
                        fileIngestionResult.Unprocessed.Add(message);
                    }
                },
                PrepareHeaderForMatch = args => args.Header.ToUpper(),
                ReadingExceptionOccurred = args =>
                {
                    switch (args.Exception)
                    {
                        case FieldValidationException:
                            {
                                var errorMessages = GetValidationErrorsForRow<TData>(args);
                                foreach (var errorMessage in errorMessages)
                                {
                                    var errorMessageWithRow = $"Error on row number {args.Exception.Context.Reader.Parser.Row}: {errorMessage}";
                                    if (unsuccessfulOnError)
                                    {
                                        fileIngestionResult.Errors.Add(errorMessageWithRow);
                                    }
                                    else
                                    {
                                        fileIngestionResult.Unprocessed.Add(errorMessageWithRow);
                                    }
                                }

                                return false;
                            }
                        case TypeConverterException typeConverterException:
                            {
                                var currentFieldName = typeConverterException.Context.Reader.HeaderRecord?[typeConverterException.Context.Reader.CurrentIndex];
                                var message = ConstructErrorMessageForInvalidField(currentFieldName, typeConverterException.Text, typeConverterException.Context.Reader.Parser.Row);

                                if (unsuccessfulOnError)
                                {
                                    fileIngestionResult.Errors.Add(message);
                                }
                                else
                                {
                                    fileIngestionResult.Unprocessed.Add(message);
                                }

                                return false;
                            }
                        case CsvHelperException generalException:
                            {
                                string message;
                                if (generalException.Context.Reader.CurrentIndex > -1)
                                {
                                    var currentFieldName = generalException.Context.Reader.HeaderRecord?[generalException.Context.Reader.CurrentIndex];
                                    message = ConstructErrorMessageForInvalidField(currentFieldName, generalException?.InnerException?.Message, generalException?.Context.Reader.Parser.Row);
                                }
                                else
                                {
                                    message = $"Error on row {generalException.Context.Reader.Parser.Row}: {generalException.Message}; {generalException.InnerException?.Message}";
                                }

                                if (unsuccessfulOnError)
                                {
                                    fileIngestionResult.Errors.Add(message);
                                }
                                else
                                {
                                    fileIngestionResult.Unprocessed.Add(message);
                                }

                                return false;
                            }
                        default:
                            return true;
                    }
                },
                TrimOptions = TrimOptions.Trim,
            };

            if (options == null)
            {
                return await csvProcessor.GetRecords<TData, TClassMap>(stream, configuration);
            }

            return await csvProcessor.GetRecords<TData, TClassMap>(stream, configuration, options);
        }

        //protected virtual async Task<string> UploadFileToStorage(IFormFile file, ImportFileType fileType, IAzureBlobStorageService azureBlobStorageService)
        //{
        //    if (file.Length <= 0)
        //    {
        //        return default;
        //    }

        //    var filename = ConstructFileName(fileType);
        //    using var memoryStream = new MemoryStream();
        //    await file.CopyToAsync(memoryStream);
        //    var fileInBytes = memoryStream.ToArray();

        //    return await azureBlobStorageService.SaveDocumentToAzureFileShare(
        //        fileInBytes,
        //        filename,
        //        new[] { azureBlobStorageService.CsrUploadsFolder, fileType.ToString() });
        //}

        //private static string ConstructFileName(ImportFileType fileType)
        //{
        //    var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss-m");
        //    return $"{fileType}_{timestamp}.csv";
        //}

        /// <summary>
        /// Gets a collection of validation error messages for a row that caused a <see cref="FieldValidationException"/>.
        /// </summary>
        /// <param name="args">The <see cref="ReadingExceptionOccurredArgs"/> associated with a validation exception.</param>
        /// <typeparam name="TData">The row type being read.</typeparam>
        /// <returns>A collection of validation error messages associated with the row.</returns>
        /// <remarks>
        /// CsvHelper throws a <see cref="FieldValidationException"/> on the first instance of a validation error for a row.
        /// There is currently no mechanism for getting all validation errors for a row, so this method presents a workaround to continue
        /// processing the row and return all validation errors. The feature request to add this functionality to CsvHelper can be found at
        /// <see href="https://github.com/JoshClose/CsvHelper/issues/1357"/>. This method was adapted from a workaround in this issue.
        /// </remarks>
        private static IEnumerable<string> GetValidationErrorsForRow<TData>(ReadingExceptionOccurredArgs args)
        {
            var classMap = args.Exception.Context.Maps.Find<TData>();
            if (classMap is null)
            {
                yield break;
            }

            for (var i = 0; i < args.Exception.Context.Reader.Parser.Count; i++)
            {
                // Get the header name for the current field and the field data
                var currentFieldHeader = args.Exception.Context.Reader.HeaderRecord?[i];
                var rawFieldData = args.Exception.Context.Reader[i];

                // Get the member map for the current field from your defined class map
                var currentFieldMemberMap = classMap.MemberMaps
                    .First(map => map.Data.Names.First() == currentFieldHeader);

                if (currentFieldMemberMap.Data.ValidateExpression is null)
                {
                    continue;
                }

                var validateArgs = new ValidateArgs(rawFieldData, args.Exception.Context.Reader);

                // Get validate expression and error message expression from the member map
                var validateExpression = Expression.Invoke(currentFieldMemberMap.Data.ValidateExpression, Expression.Constant(validateArgs));
                var messageExpression = Expression.Invoke(currentFieldMemberMap.Data.ValidateMessageExpression, Expression.Constant(validateArgs));

                var validateExpressionLambda = Expression.Lambda<Func<ValidateArgs, bool>>(validateExpression, Expression.Parameter(typeof(ValidateArgs), "args"));
                var messageExpressionLambda = Expression.Lambda<Func<ValidateArgs, string>>(messageExpression, Expression.Parameter(typeof(ValidateArgs), "args"));

                var validateResult = validateExpressionLambda.Compile()(validateArgs);
                if (validateResult)
                {
                    continue;
                }

                var errorMessageResult = messageExpressionLambda.Compile()(validateArgs);
                yield return errorMessageResult;
            }
        }
    }
}

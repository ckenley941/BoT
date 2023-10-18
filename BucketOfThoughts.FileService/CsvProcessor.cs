using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Http;
using System.Formats.Asn1;
using System.Globalization;

namespace BucketOfThoughts.FileService
{
    public interface ICsvProcessor
    {
        Task<byte[]> GenerateFile<TData, TMap>(IEnumerable<TData> records)
            where TMap : ClassMap<TData>;

        Task<int> GetLineCount(FileStream file);

        Task<IEnumerable<TData>> GetRecords<TData, TMap>(FileStream file, CsvConfiguration csvConfiguration)
          where TMap : ClassMap<TData>;

        Task<IEnumerable<TData>> GetRecords<TData, TMap>(
            FileStream file,
            CsvConfiguration csvConfiguration,
            FileIngestionOptions options)
            where TMap : ClassMap<TData>;

        Task<bool> ValidateHeader<TData, TMap>(FileStream file)
            where TMap : ClassMap<TData>;
        //Task<IEnumerable<TData>> GetRecords<TData, TMap>(IFormFile file, CsvConfiguration csvConfiguration)
        //    where TMap : ClassMap<TData>;

            //Task<IEnumerable<TData>> GetRecords<TData, TMap>(
            //    IFormFile file,
            //    CsvConfiguration csvConfiguration,
            //    FileIngestionOptions options)
            //    where TMap : ClassMap<TData>;

            //Task<bool> ValidateHeader<TData, TMap>(IFormFile file)
            //    where TMap : ClassMap<TData>;
    }

    public class CsvProcessor : ICsvProcessor
    {
        public async Task<byte[]> GenerateFile<TData, TMap>(IEnumerable<TData> records)
            where TMap : ClassMap<TData>
        {
            await using var memoryStream = new MemoryStream();
            await using var textWriter = new StreamWriter(memoryStream);
            await using var csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TMap>();
            await csv.WriteRecordsAsync(records);
            await textWriter.FlushAsync();
            return memoryStream.ToArray();
        }

        public async Task<int> GetLineCount(FileStream stream)
        {
            int lineCount = 0;
            //await using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            while (reader.ReadLine() != null)
                ++lineCount;
            return lineCount;
        }

        public async Task<IEnumerable<TData>> GetRecords<TData, TMap>(FileStream stream, CsvConfiguration csvConfiguration) where TMap : ClassMap<TData>
        {
            //await using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csvReader = new CsvReader(reader, csvConfiguration);
            csvReader.Context.RegisterClassMap<TMap>();
            await csvReader.ReadAsync();
            csvReader.ReadHeader();
            csvReader.ValidateHeader<TData>();

            return csvReader.GetRecords<TData>().ToList();
        }

        public async Task<IEnumerable<TData>> GetRecords<TData, TMap>(
            FileStream stream,
            CsvConfiguration csvConfiguration,
            FileIngestionOptions options)
            where TMap : ClassMap<TData>
        {
            //await using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csvReader = new CsvReader(reader, csvConfiguration);
            AddTypeConvertersToCache(csvReader.Context, options.TypeConverters);
            AddTypeConverterOptionsToCache(csvReader.Context, options.TypeConverterOptions);
            csvReader.Context.RegisterClassMap<TMap>();
            await csvReader.ReadAsync();
            csvReader.ReadHeader();

            return csvReader.GetRecords<TData>().ToList();
        }

        public async Task<bool> ValidateHeader<TData, TMap>(FileStream stream) where TMap : ClassMap<TData>
        {
            //await using var stream = file.OpenReadStream();
            var reader = new StreamReader(stream);
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<TMap>();
            await csvReader.ReadAsync();
            csvReader.ReadHeader();

            try
            {
                csvReader.ValidateHeader<TData>();
                return true;
            }
            catch (HeaderValidationException)
            {
                return false;
            }
        }

        private void AddTypeConvertersToCache(CsvContext context, IDictionary<Type, ITypeConverter> typeConverters)
        {
            foreach (var kvp in typeConverters)
            {
                context.TypeConverterCache.AddConverter(kvp.Key, kvp.Value);
            }
        }

        private void AddTypeConverterOptionsToCache(CsvContext context, IDictionary<Type, TypeConverterOptions> typeConverterOptions)
        {
            foreach (var kvp in typeConverterOptions)
            {
                context.TypeConverterOptionsCache.AddOptions(kvp.Key, kvp.Value);
            }
        }
    }
}
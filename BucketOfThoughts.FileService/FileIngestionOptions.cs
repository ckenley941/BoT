using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.FileService
{
    public class FileIngestionOptions
    {
        public IDictionary<Type, ITypeConverter> TypeConverters { get; set; } = new Dictionary<Type, ITypeConverter>();
        public IDictionary<Type, TypeConverterOptions> TypeConverterOptions { get; set; } = new Dictionary<Type, TypeConverterOptions>();
    }
}

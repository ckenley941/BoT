using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.FileService
{
    public class FileIngestionResult
    {
        public ICollection<string> Errors { get; } = new List<string>();

        public bool IsSuccessful => !Errors.Any();

        public ISet<string> Unprocessed { get; } = new HashSet<string>();

        public ISet<string> ImportedRecords { get; } = new HashSet<string>();

        public string StorageUrl { get; set; }
    }
}

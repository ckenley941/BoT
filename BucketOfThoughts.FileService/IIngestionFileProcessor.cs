using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketOfThoughts.FileService
{
    public interface IIngestionFileProcessor
    {
        Task<FileIngestionResult> Process(FileStream stream); //UserInfo
    }
}

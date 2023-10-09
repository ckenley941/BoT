using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Objects.Thoughts
{
    public class InsertThoughtDto
    {
        public string Description { get; set; }
        public int ThoughtCategoryId { get; set; }
        public string ThoughtSource { get; set; }
        public List<string> Details { get; set; }
    }
}

using BucketOfThoughts.Core.Infrastructure.Enums;

namespace BucketOfThoughts.Services.Thoughts.Objects
{
    public class InsertThoughtDto
    {
        public string Description { get; set; } = string.Empty;
        public int ThoughtCategoryId { get; set; } = 0;
        public string ThoughtSource { get; set; } = string.Empty;
        public string TextType { get; set; } = TextTypes.Text.ToString();
        public List<string> Details { get; set; } = new List<string>();
        public JsonDetail JsonDetails { get; set; } = new JsonDetail();
        public List<string> WebsiteLinks { get; set; } = new List<string>();
    }
    public class JsonDetail
    {
        public List<string> Keys { get; set; } = new List<string>();
        public string Json { get; set; } = string.Empty;
    }
}
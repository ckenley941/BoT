using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Cooking.Data
{
    public partial class Recipe : BaseModifiableDbTable
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Protein { get; set; }
        public string? Category { get; set; }
        public string? CuisineType { get; set; }
        public string? Serves { get; set; }
        public string? PrepTime { get; set; }
        public string? CookTime { get; set; }
        public string? TotalTime { get; set; }
        public string? ServeWith { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public virtual ICollection<RecipeInstruction> RecipeInstructons { get; set; } = new List<RecipeInstruction>();
        public virtual ICollection<RecipeWebsiteLink> RecipeWebsiteLinks { get; set; } = new List<RecipeWebsiteLink>();
        public virtual ICollection<RecipeNote> RecipeNotes { get; set; } = new List<RecipeNote>();
    }
}

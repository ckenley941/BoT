using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Cooking.Data
{
    public partial class RecipeIngredient : BaseModifiableDbTable
    {
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string Measurement { get; set; } = null!;
        public string? AdditionalNotes { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;
    }
}

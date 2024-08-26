using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Cooking.Data
{
    public partial class RecipeInstruction : BaseModifiableDbTable
    {
        public int RecipeId { get; set; }
        public int StepNo { get; set; }
        public string Description { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}

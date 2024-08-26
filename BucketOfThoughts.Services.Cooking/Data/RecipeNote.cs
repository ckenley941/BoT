using BucketOfThoughts.Core.Infrastructure.BaseClasses;

namespace BucketOfThoughts.Services.Cooking.Data
{
    public partial class RecipeNote : BaseModifiableDbTable
    {
        public int RecipeId { get; set; }

        public int NoteId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        //public virtual Note Note { get; set; } = null!;
    }
}

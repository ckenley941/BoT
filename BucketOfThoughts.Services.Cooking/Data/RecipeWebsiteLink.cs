namespace BucketOfThoughts.Services.Cooking.Data;
public partial class RecipeWebsiteLink 
{
    public int RecipeId { get; set; }

    public int WebsiteLinkId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    //public virtual WebsiteLink WebsiteLink { get; set; } = null!;
}

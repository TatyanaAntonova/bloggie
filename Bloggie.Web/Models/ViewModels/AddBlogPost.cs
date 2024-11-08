namespace Bloggie.Web.Models.ViewModels;

public class AddBlogPost
{
    public string Heading { get; set; }
    public string PageTitle { get; set; }
    public string Content { get; set; }
    public string ShortDescription { get; set; }
    public string FeatureImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
    public bool Visible { get; set; }
}
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories;

public class BlogPostRepository(BloggieDbContext dbContext) : IBlogPostRepository
{
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await dbContext.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost> GetByIdAsync(Guid id)
    {
        return await dbContext.BlogPosts.FindAsync(id);
    }

    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        await dbContext.BlogPosts.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        var existingBlogPost = await dbContext.BlogPosts.FindAsync(blogPost.Id);
        if (existingBlogPost != null)
        {
            existingBlogPost.Heading = blogPost.Heading;
            existingBlogPost.PageTitle = blogPost.PageTitle;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.ShortDescription = blogPost.ShortDescription;
            existingBlogPost.FeatureImageUrl = blogPost.FeatureImageUrl;
            existingBlogPost.UrlHandle = blogPost.UrlHandle;
            existingBlogPost.PublishDate = blogPost.PublishDate;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.Visible = blogPost.Visible;
        }

        await dbContext.SaveChangesAsync();
        return existingBlogPost;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingBlogPost = await dbContext.BlogPosts.FindAsync(id);
        if (existingBlogPost != null)
        {
            dbContext.BlogPosts.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
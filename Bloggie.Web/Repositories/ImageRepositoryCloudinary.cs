using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web.Repositories;

public class ImageRepositoryCloudinary : IImageRepository
{
    public readonly Account account;

    public ImageRepositoryCloudinary(IConfiguration config)
    {
        account = new Account(
            config.GetSection("Cloudinary")["CloudName"],
            config.GetSection("Cloudinary")["ApiKey"],
            config.GetSection("Cloudinary")["ApiSecret"]
        );
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var client = new Cloudinary(account);
        var uploadFileResult = await client.UploadAsync(
            new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            }
        );
        if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return uploadFileResult.SecureUrl.ToString();

        }
        
        return null;
    }
}
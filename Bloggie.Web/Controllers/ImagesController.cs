using System.Net;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController(IImageRepository imageRepository) : Controller
{
    [HttpPost]
    public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
    {
        var imageUrl = await imageRepository.UploadAsync(file);
        if (imageUrl == null)
        {
            return Problem("Image file could not be uploaded.", null, (int)HttpStatusCode.InternalServerError);
        }

        return Json(new { link = imageUrl });
    }
}
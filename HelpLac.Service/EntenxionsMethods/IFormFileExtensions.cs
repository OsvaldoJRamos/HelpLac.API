using HelpLac.Domain.Validation;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace HelpLac.Service.EntenxionsMethods
{
    public static class IFormFileExtensions
    {
        public async static Task<byte[]> GetBytesAsync(this IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }

            return null;
        }

        public static void ValidateImage(this IFormFile image)
        {
            if (image != null)
            {
                var extension = Path.GetExtension(image.FileName).ToUpperInvariant();

                if (extension != ".PNG" && extension != ".JPG" && extension != ".JPEG")
                    throw new ValidationEntityException("Image must be PNG, JPG or JPEG.", "Image");
            }
        }
    }
}

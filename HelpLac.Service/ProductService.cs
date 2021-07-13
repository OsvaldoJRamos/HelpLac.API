using HelpLac.Domain.Entities;
using HelpLac.Domain.Validation;
using HelpLac.Repository.Interfaces;
using HelpLac.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service
{
    public class ProductService : ServiceBase<Product, Guid, IProductRepository>, IProductService
    {
        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
        }

        public async Task<Product> CreateAsync(string name, string ingredients, bool containsLactose, IFormFile image, CancellationToken cancellationToken)
        {
            ValidateImage(image);

            var bytesImage = await GetBytes(image);

            var product = new Product(name, containsLactose, ingredients, bytesImage);
            product.Validate();

            await _repository.AddAsync(product, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return product;
        }

        private void ValidateImage(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToUpperInvariant();

            if(extension != ".PNG" && extension != ".JPG" && extension != ".JPEG")
                throw new ValidationEntityException("Image must be PNG, JPG or JPEG.", "Image");
        }

        private async Task<byte[]> GetBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
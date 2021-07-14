using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities;
using HelpLac.Domain.PaginatedEntities;
using HelpLac.Domain.Validation;
using HelpLac.Repository.Interfaces;
using HelpLac.Service.Interfaces;
using LinqKit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
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

            if (extension != ".PNG" && extension != ".JPG" && extension != ".JPEG")
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

        public async Task<PaginatedEntity<Product>> GetAsync(ProductDto request, PaginationRequestDto pagination, CancellationToken cancellationToken)
        {
            Expression<Func<Product, bool>> filterExpression = PredicateBuilder.New<Product>(true);

            if (request.ContainsLactose.HasValue) filterExpression = filterExpression.And(x => x.ContainsLactose == request.ContainsLactose.Value);
            if (!string.IsNullOrEmpty(request.Name)) filterExpression = filterExpression.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            if (!string.IsNullOrEmpty(request.Ingredients)) filterExpression = filterExpression.And(x => x.Ingredients.ToLowerInvariant().Contains(request.Ingredients.ToLowerInvariant()));

            var products = await _repository.SearchAsync(pagination, filterExpression, cancellationToken);
            return new PaginatedEntity<Product>(products);
        }
    }
}
using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities;
using HelpLac.Domain.PaginatedEntities;
using HelpLac.Domain.Validation;
using HelpLac.Repository.Interfaces;
using HelpLac.Service.EntenxionsMethods;
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
            image.ValidateImage();

            var bytesImage = await image.GetBytesAsync();

            var product = new Product(name, containsLactose, ingredients, bytesImage);
            product.Validate();

            await _repository.AddAsync(product, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return product;
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

        public async Task<Product> UpdateAsync(Guid id, string name, string ingredients, bool containsLactose, IFormFile image, CancellationToken cancellationToken)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null)
                throw new ValidationEntityException("Product not found");

            image.ValidateImage();
            var bytesImage = await image.GetBytesAsync();

            product.Update(name, containsLactose, ingredients, bytesImage);
            product.Validate();

            await base.UpdateAsync(product, cancellationToken);
            return product;
        }
    }
}
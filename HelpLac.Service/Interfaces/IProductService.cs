using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities;
using HelpLac.Domain.PaginatedEntities;
using HelpLac.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service.Interfaces
{
    public interface IProductService : IServiceBase<Product, Guid, IProductRepository>
    {
        Task<Product> CreateAsync(string name, string ingredients, bool containsLactose, IFormFile image, CancellationToken cancellationToken);
        Task<PaginatedEntity<Product>> GetAsync(ProductDto request, PaginationRequestDto pagination, CancellationToken cancellationToken);
    }
}

using HelpLac.Domain.Entities;
using HelpLac.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service.Interfaces
{
    public interface IProductService : IServiceBase<Product, Guid, IProductRepository>
    {
        Task<Product> CreateAsync(CancellationToken cancellationToken);
    }
}

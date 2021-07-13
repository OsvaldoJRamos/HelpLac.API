using HelpLac.Domain.Entities;
using HelpLac.Repository;
using HelpLac.Repository.Interfaces;
using HelpLac.Service.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service
{
    public class ProductService : ServiceBase<Product, Guid, IProductRepository>, IProductService
    {
        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
        }

        public async Task<Product> CreateAsync(CancellationToken cancellationToken)
        {
            var product = new Product("nome");
            await _repository.AddAsync(product, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return product;
        }
    }
}
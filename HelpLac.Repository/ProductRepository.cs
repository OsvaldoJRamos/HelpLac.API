using HelpLac.Domain.Entities;
using HelpLac.Repository.Interfaces;
using System;

namespace HelpLac.Repository
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}

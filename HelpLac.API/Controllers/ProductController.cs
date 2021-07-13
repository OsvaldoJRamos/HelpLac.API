using HelpLac.Domain.Validation;
using HelpLac.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.CreateAsync(cancellationToken);
                return new ObjectResult(product);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

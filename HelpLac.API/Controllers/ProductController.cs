using HelpLac.API.Models;
using HelpLac.Domain.Entities;
using HelpLac.Domain.Validation;
using HelpLac.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.API.Controllers
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(1000000)]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.CreateAsync(request.Name, request.Ingredients, request.ContainsLactose, request.Image, cancellationToken);
                return new ObjectResult(product);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id, cancellationToken);
                return new ObjectResult(product);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

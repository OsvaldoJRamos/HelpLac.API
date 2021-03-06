using AutoMapper;
using HelpLac.API.Models;
using HelpLac.API.Models.Request;
using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities;
using HelpLac.Domain.Validation;
using HelpLac.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(
            IMapper mapper,
            IProductService productService)
            : base(mapper)
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
                var product = await _productService.CreateAsync(request.Name, request.Ingredients, request.ContainsLactose, request.Image, request.ImageUrl, cancellationToken);
                return new ObjectResult(product);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id, cancellationToken);
                if (product == null)
                    throw new ValidationEntityException("Product not found");

                await _productService.DeleteAsync(product, cancellationToken);
                return new ObjectResult(product);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromForm] CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.UpdateAsync(id, request.Name, request.Ingredients, request.ContainsLactose, request.Image, request.ImageUrl, cancellationToken);
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] ProductRequest request, [FromQuery] PaginationRequest pagination, CancellationToken cancellationToken)
        {
            try
            {
                var requestDto = _mapper.Map<ProductDto>(request);
                var paginationDto = _mapper.Map<PaginationRequestDto>(pagination);

                var products = await _productService.GetAsync(requestDto, paginationDto, cancellationToken);
                return new ObjectResult(products);
            }
            catch (ValidationEntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

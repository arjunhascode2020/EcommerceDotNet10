using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CatalogsController> _logger;

        public CatalogsController(IMediator mediator, ILogger<CatalogsController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {

            var query = new GetAllProductQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("get-product-by-id/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get-product-by-name/{productName}")]
        public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetProductByName(string prodcutName)
        {
            var query = new GetProductByNameQuery(prodcutName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get-product-by-brand/{brandName}")]

        public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetProductsByBrandName(string brandName)
        {
            var query = new GetProductByBrandQuery(brandName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<ActionResult<Response>> DeleteProdcut(string id)
        {
            var query = new DeleteProductCommand(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("update-product/{id}")]
        public async Task<ActionResult<Response>> UpdateProduct(string id, UpdateProductDto updateProductDto)
        {
            var updateCommand = updateProductDto.ToUpdateCommand(id);
            var result = _mediator.Send(updateCommand);
            return Ok(result);
        }

        [HttpGet("get-all-brands")]
        public async Task<ActionResult<BrandResponse>> GetAllProductBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        //[HttpGet("get-brand-by-id/{id}")]
        //public async Task<ActionResult<BrandResponse>> GetBrandById(string id)
        //{

        //}
        [HttpGet("get-all-types")]
        public async Task<ActionResult<TypesResponse>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

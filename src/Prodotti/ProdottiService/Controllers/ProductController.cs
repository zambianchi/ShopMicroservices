using Microsoft.AspNetCore.Mvc;
using ProdottiService.Models.API;
using ProdottiService.Services.Int;

namespace ProdottiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            this._logger = logger;
            this._productService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            try
            {
                var getProductsApiResponse = await _productService.GetProducts(cancellationToken);
                return Ok(getProductsApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idProduct}", Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(long idProduct, CancellationToken cancellationToken)
        {
            try
            {
                var getProductApiResponse = await _productService.GetProduct(idProduct, cancellationToken);
                return Ok(getProductApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createProductApiResponse = await _productService.CreateProduct(request, cancellationToken);
                return Ok(createProductApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

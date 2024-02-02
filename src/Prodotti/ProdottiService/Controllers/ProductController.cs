using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ProdottiService.Models.API.Entity;
using ProdottiService.Models.API.Request;
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

        /// <summary>
        /// Richiede tutti i prodotti
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Lista di prodotti</returns>
        [SwaggerResponse(200, typeof(List<ProductDTO>))]
        [SwaggerResponse(400, typeof(string))]
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

        /// <summary>
        /// Richiede una specifica lista di prodotti
        /// </summary>
        /// <param name="productIds">Lista di id prodotti</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Lista di prodotti</returns>
        [SwaggerResponse(200, typeof(List<ProductDTO>))]
        [SwaggerResponse(400, typeof(string))]
        [HttpPost(Name = "GetSpecificProducts")]
        public async Task<IActionResult> GetSpecificProducts([FromBody] List<long> productIds, CancellationToken cancellationToken)
        {
            try
            {
                var getSpecificProductsApiResponse = await _productService.GetSpecificProducts(productIds, cancellationToken);
                return Ok(getSpecificProductsApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Richiede un prodotto specifico
        /// </summary>
        /// <param name="idProduct">ID prodotto</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Prodotto</returns>
        [SwaggerResponse(200, typeof(ProductDTO))]
        [SwaggerResponse(400, typeof(string))]
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

        /// <summary>
        /// Crea un nuovo prodotto
        /// </summary>
        /// <param name="request">Dettaglio prodotto da creare</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Prodotto creato</returns>
        [SwaggerResponse(200, typeof(ProductDTO))]
        [SwaggerResponse(400, typeof(string))]
        [HttpPut(Name = "CreateProduct")]
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

        /// <summary>
        /// Elimina un prodotto
        /// </summary>
        /// <param name="idProduct">ID prodotto</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        [SwaggerResponse(200, typeof(void))]
        [SwaggerResponse(400, typeof(string))]
        [HttpDelete("{idProduct}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(long idProduct, CancellationToken cancellationToken)
        {
            try
            {
                await _productService.DeleteProduct(idProduct, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Modifica un prodotto
        /// </summary>
        /// <param name="request">Prodotto da modificare con relativi campi</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Prodotto modificato</returns>
        [SwaggerResponse(200, typeof(ProductDTO))]
        [SwaggerResponse(400, typeof(string))]
        [HttpPatch(Name = "EditProduct")]
        public async Task<IActionResult> EditProduct([FromBody] EditProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var editProductApiResponse = await _productService.EditProduct(request, cancellationToken);
                return Ok(editProductApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

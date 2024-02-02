using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Services.Int;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayProductsController : ControllerBase
    {
        private readonly ILogger<ApiGatewayProductsController> _logger;
        private readonly IApiGatewayProductService _apiGatewayService;

        public ApiGatewayProductsController(ILogger<ApiGatewayProductsController> logger, IApiGatewayProductService apiGatewayService)
        {
            this._logger = logger;
            this._apiGatewayService = apiGatewayService;
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
                var getProductApiResponse = await _apiGatewayService.GetProduct(idProduct, cancellationToken);
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
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDTO request, CancellationToken cancellationToken)
        {
            try
            {
                var createProductApiResponse = await _apiGatewayService.CreateProduct(request, cancellationToken);
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
                await _apiGatewayService.DeleteProduct(idProduct, cancellationToken);
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
        public async Task<IActionResult> EditProduct([FromBody] EditProductRequestDTO request, CancellationToken cancellationToken)
        {
            try
            {
                var editProductApiResponse = await _apiGatewayService.EditProduct(request, cancellationToken);
                return Ok(editProductApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

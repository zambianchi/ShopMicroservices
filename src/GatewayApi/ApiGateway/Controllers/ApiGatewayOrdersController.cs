using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Services.Int;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayOrdersController : ControllerBase
    {
        private readonly ILogger<ApiGatewayOrdersController> _logger;
        private readonly IApiGatewayOrderService _apiGatewayService;

        public ApiGatewayOrdersController(ILogger<ApiGatewayOrdersController> logger, IApiGatewayOrderService apiGatewayService)
        {
            this._logger = logger;
            this._apiGatewayService = apiGatewayService;
        }

        /// <summary>
        /// Richiede un ordine specifico
        /// </summary>
        /// <param name="idOrder">ID ordine</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Ordine</returns>
        /// <response code="200">Ritorna l'ordine ricercato</response>
        /// <response code="400">Ritorna se l'ordine non è presente</response>
        [SwaggerResponse(200, typeof(OrderDTO))]
        [SwaggerResponse(400, typeof(string))]
        [HttpGet("{idOrder}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            try
            {
                var getOrderApiResponse = await _apiGatewayService.GetOrder(idOrder, cancellationToken);
                return Ok(getOrderApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Crea un ordine
        /// </summary>
        /// <param name="request">Dettagli ordine da creare</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Ordine creato</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT CreateOrder
        ///     {
        ///       "UserId": 1,
        ///       "DataOrdine": "2024-02-03T01:21:29.542Z",
        ///       "Products": [
        ///         {
        ///            "IdProduct": 1,
        ///            "Amount": 10
        ///         }
        ///       ]
        ///     }
        /// </remarks>
        /// <response code="200">Ritorna l'ordine creato</response>
        /// <response code="400">Ritorna in caso di errore della creazione ordine</response>
        [SwaggerResponse(200, typeof(OrderDTO))]
        [SwaggerResponse(400, typeof(string))]
        [HttpPut(Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDTO request, CancellationToken cancellationToken)
        {
            try
            {
                var createOrderApiResponse = await _apiGatewayService.CreateOrder(request, cancellationToken);
                return Ok(createOrderApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un ordine
        /// </summary>
        /// <param name="idOrder">ID ordine</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        /// <response code="200">Ritorna per eliminazione con successo dell'ordine</response>
        /// <response code="400">Ritorna in caso di errore durante l'eliminazione dell'ordine</response>
        [SwaggerResponse(200, typeof(void))]
        [SwaggerResponse(400, typeof(string))]
        [HttpDelete("{idOrder}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(long idOrder, CancellationToken cancellationToken)
        {
            try
            {
                await _apiGatewayService.DeleteOrder(idOrder, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

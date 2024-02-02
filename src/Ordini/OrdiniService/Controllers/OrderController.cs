using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using OrdiniService.Models.API.Entity;
using OrdiniService.Models.API.Request;
using OrdiniService.Services.Int;

namespace OrdiniService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            this._logger = logger;
            this._orderService = orderService;
        }

        /// <summary>
        /// Richiede tutti gli ordini
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Lista di ordini</returns>
        [HttpGet(Name = "GetOrders")]
        [SwaggerResponse(200, typeof(List<OrderDTO>))]
        [SwaggerResponse(400, typeof(string))]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            try
            {
                var getOrdersApiResponse = await _orderService.GetOrders(cancellationToken);
                return Ok(getOrdersApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Richiede un ordine specifico
        /// </summary>
        /// <param name="idOrder">ID ordine</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Ordine</returns>
        [HttpGet("{idOrder}", Name = "GetOrder")]
        [SwaggerResponse(200, typeof(OrderDTO))]
        [SwaggerResponse(400, typeof(string))]
        public async Task<IActionResult> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            try
            {
                var getOrderApiResponse = await _orderService.GetOrder(idOrder, cancellationToken);
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
        [SwaggerResponse(200, typeof(OrderDTO))]
        [SwaggerResponse(400, typeof(string))]
        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createOrderApiResponse = await _orderService.CreateOrder(request, cancellationToken);
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
        /// <param name="cancellationToken"CancellationToken</param>
        [SwaggerResponse(200, typeof(void))]
        [SwaggerResponse(400, typeof(string))]
        [HttpDelete("{idOrder}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(long idOrder, CancellationToken cancellationToken)
        {
            try
            {
                await _orderService.DeleteOrder(idOrder, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

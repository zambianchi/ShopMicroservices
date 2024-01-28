using Microsoft.AspNetCore.Mvc;
using OrdiniService.Services.Int;

namespace OrdiniService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _ordiniService;

        public OrderController(ILogger<OrderController> logger, IOrderService ordiniService)
        {
            this._logger = logger;
            this._ordiniService = ordiniService;
        }

        [HttpGet(Name = "GetOrders")]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            try
            {
                var getOrdersApiResponse = await _ordiniService.GetOrders(cancellationToken);
                return Ok(getOrdersApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idOrder}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            try
            {
                var getOrderApiResponse = await _ordiniService.GetOrder(idOrder, cancellationToken);
                return Ok(getOrderApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

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

        [HttpGet(Name = "GetOrdini")]
        public async Task<IActionResult> GetOrdini(CancellationToken cancellationToken)
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

        [HttpGet("{idOrdine}", Name = "GetOrdine")]
        public async Task<IActionResult> GetOrdine(long idOrdine, CancellationToken cancellationToken)
        {
            try
            {
                var getOrderApiResponse = await _ordiniService.GetOrder(idOrdine, cancellationToken);
                return Ok(getOrderApiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

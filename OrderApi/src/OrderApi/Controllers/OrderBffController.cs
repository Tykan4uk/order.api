using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderApi.Models.Requests;
using OrderApi.Services.Abstractions;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = "ApiScopeBff")]
    public class OrderBffController : ControllerBase
    {
        private readonly ILogger<OrderBffController> _logger;
        private readonly IOrderService _orderService;

        public OrderBffController(
            ILogger<OrderBffController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> GetByPage([FromBody] GetByPageRequest request)
        {
            var result = await _orderService.GetByPageAsync(request.UserId, request.Page, request.PageSize);

            if (result == null)
            {
                _logger.LogInformation("(OrderBffController/GetByPage)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
        {
            var result = await _orderService.GetByIdAsync(request.UserId, request.OrderId);

            if (result == null)
            {
                _logger.LogInformation("(OrderBffController/GetById)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRequest request)
        {
            var result = await _orderService.AddAsync(request.UserId, request.Products);

            if (result == null)
            {
                _logger.LogInformation("(OrderBffController/Add)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

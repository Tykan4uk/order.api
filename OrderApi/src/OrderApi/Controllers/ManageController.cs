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
    [Authorize(Policy = "ApiScope")]
    public class ManageController : ControllerBase
    {
        private readonly ILogger<ManageController> _logger;
        private readonly IOrderService _orderService;

        public ManageController(
            ILogger<ManageController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPage([FromQuery] GetByPageRequest request)
        {
            var result = await _orderService.GetByPageAsync(request.UserId, request.Page, request.PageSize);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/GetByPage)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdRequest request)
        {
            var result = await _orderService.GetByIdAsync(request.UserId, request.OrderId);

            if (result == null)
            {
                _logger.LogInformation("(ManageController/GetById)Null result. Bad request.");
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
                _logger.LogInformation("(ManageController/Add)Null result. Bad request.");
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

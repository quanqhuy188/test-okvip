using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/")]
    public class HomeController : ControllerBase
    {

        private readonly IDemoService _service;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDemoService service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<Model>>> Get() =>
            await _service.GetAllAsync();


        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] int num)
        {
            await _service.InsertRandomProductsAsync(num);
            return Ok(new { Data = "Ok" });
        }
    }
}

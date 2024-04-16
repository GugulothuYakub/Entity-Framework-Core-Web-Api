using entitycore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace entitycore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly SuperheroContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddressController> _logger;
        private readonly string _className;
        public AddressController(SuperheroContext context, IConfiguration configuration, ILogger<AddressController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _className = context.GetType().Name;
        }
        [HttpGet]
        public async Task<ActionResult<List<Address>>> Get()
        {
            try
            {
                _logger.LogInformation("Entered into Getmethod: {ClassName}", _className);
                int a = 0;
                int b = 10 / a;
                var _Addr = await _context.Address.ToListAsync();
                _logger.LogInformation("Exiting from Getmethod: {ClassName}", _className);

                return Ok(_Addr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured in Getmethod: {ClassName}", _className);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult<Address>> AddAddress(Address AddAdress)
        {
            if (AddAdress == null)
                return BadRequest();

            _context.Address.Add(AddAdress);
            await _context.SaveChangesAsync();
            var addr = _context.Address.FindAsync(AddAdress.Id);

            return Ok(addr);
        }
    }
}

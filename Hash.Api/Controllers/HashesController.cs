using Hash.Model.ViewModels;
using Hash.Service.Implementations;
using Hash.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hash.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HashesController : ControllerBase
    {

        private readonly ILogger<HashesController> _logger;
        private readonly IHashService _hashService;

        public HashesController(IHashService hashService , ILogger<HashesController> logger)
        {
            _hashService = hashService;
            _logger = logger;
        }

        [HttpGet(Name = "hashesget")]
        public async Task<IEnumerable<HashGroupedViewModel>> Get()
        {
            return await _hashService.GetAllGroupedAsync();
        }

        [HttpPost(Name = "hashespost")]
        public void Post()
        {
            Hash.Helper.RabbitMQ.GenerateHashes(40000);
        }
    }
}
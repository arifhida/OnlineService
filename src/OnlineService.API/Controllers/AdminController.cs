using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineService.Data.Abstracts;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineService.API.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        // GET: api/values
        [HttpGet("GetAllUser",Name ="GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await _userRepository.GetAll();
            var json = JsonConvert.SerializeObject(data, _serializerSettings);
            return new OkObjectResult(json);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquorShopService.Services;
using Microsoft.AspNetCore.Mvc;
using TescoWineShopSql;

namespace EvanApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        IUserService service;
        public UserController(IUserService _userservice)
        {
             service=_userservice;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
              return await service.GetAllUsersAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

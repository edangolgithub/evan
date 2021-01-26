using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvanApi.models;
using Microsoft.AspNetCore.Mvc;

namespace EvanApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IInterestsRepository _repository;

        public AccountController(IInterestsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Account>> Get()
        {
            return await _repository.AllAccounts();
        }

        [HttpPost]
        public async Task Post([FromBody]Account model)
        {
           if(model==null)
           {
               return;
           }
            await _repository.AddAccount(model);
        }
        [HttpPut]
        public async Task PutAsync([FromBody] Account entity)
        {
             await _repository.UpdateAccount(entity);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAccount(id);
        }


    }
}

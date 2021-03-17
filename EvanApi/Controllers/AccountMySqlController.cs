using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvanApi.models;
using Microsoft.AspNetCore.Mvc;

namespace EvanApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountMySqlController : ControllerBase
    {
        private IInterestMysqlRepository _repository;

        public AccountMySqlController(IInterestMysqlRepository repository)
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
        public async Task DeleteAsync([FromBody] Account entity)
        {
            await _repository.DeleteAccount(entity);
        }


    }
}

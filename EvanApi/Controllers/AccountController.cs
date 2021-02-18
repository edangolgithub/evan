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
        public async Task<IEnumerable<Transaction>> Get()
        {
            return await _repository.AllTransactions();
        }

        [HttpPost]
        public async Task Post([FromBody]Transaction model)
        {
           if(model==null)
           {
               return;
           }
            await _repository.AddTransaction(model);
        }
        [HttpPut]
        public async Task PutAsync([FromBody] Transaction entity)
        {
             await _repository.UpdateTransaction(entity);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteTransaction(id);
        }


    }
}

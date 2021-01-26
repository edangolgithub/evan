using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvanApi.models;
using Microsoft.AspNetCore.Mvc;

namespace EvanApi.Controllers
{
    [Route("api/[controller]")]
    public class AccounttypeController : ControllerBase
    {
        private IInterestsRepository _repository;

        public AccounttypeController(IInterestsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Accounttype>> Get()
        {
            return await _repository.Allaccounttype();
        }

        [HttpPost]
        public async Task Post([FromBody]Accounttype model)
        {
           if(model==null)
           {
               return;
           }
            await _repository.Addaccounttype(model);
        }
        [HttpPut]
        public async Task PutAsync([FromBody] Accounttype entity)
        {
             await _repository.Updateaccounttype(entity);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _repository.Deleteaccounttype(id);
        }


    }
}

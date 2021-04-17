using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvanApi1.models;
using Microsoft.AspNetCore.Mvc;

namespace EvanApi1.Controllers
{
    [Route("api/[controller]")]
    public class AccounttypeMySqlController : ControllerBase
    {
        private IInterestMysqlRepository _repository;

        public AccounttypeMySqlController(IInterestMysqlRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AccountType>> Get()
        {
            return await _repository.Allaccounttype();
        }

        [HttpPost]
        public async Task Post([FromBody]AccountType model)
        {
           if(model==null)
           {
               return;
           }
            await _repository.Addaccounttype(model);
        }
        [HttpPut]
        public async Task PutAsync([FromBody] AccountType entity)
        {
             await _repository.Updateaccounttype(entity);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromBody] AccountType entity)
        {
            await _repository.Deleteaccounttype(entity);
        }


    }
}
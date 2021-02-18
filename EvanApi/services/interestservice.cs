using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using EvanApi.models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EvanApi
{
    public interface IInterestsRepository
    {
        // Task<IEnumerable<AccountType>> Allaccounttype();
        // Task Addaccounttype(AccountType entity);
        // Task Deleteaccounttype(string id);
        // Task Updateaccounttype(AccountType entity);

        
        // Task<IEnumerable<Account>> AllAccounts();
        // Task AddAccount(Account entity);
        // Task DeleteAccount(string id);
        // Task UpdateAccount(Account entity);

        
        Task<IEnumerable<Transaction>> AllTransactions();
        Task AddTransaction(Transaction entity);
        Task DeleteTransaction(string id);
        Task UpdateTransaction(Transaction entity);


    }
    public class InterestsRepository : IInterestsRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public InterestsRepository()
        {
            _client = new AmazonDynamoDBClient();
            _context = new DynamoDBContext(_client);
        }




        public async Task<IEnumerable<Transaction>> AllTransactions()
        {

            var table = _context.GetTargetTable<Transaction>();
            var scanConditions = new List<ScanCondition>() { new ScanCondition("id", ScanOperator.IsNotNull) };
            var searchResults = _context.ScanAsync<Transaction>(scanConditions, null);
            return (IEnumerable<Transaction>)await searchResults.GetNextSetAsync();
        }

        public async Task DeleteTransaction(string id)
        {
            await _context.DeleteAsync<Transaction>(id);
        }

        public async Task UpdateTransaction(Transaction entity)
        {
            await _context.SaveAsync<Transaction>(entity);
        }


        public async Task AddTransaction(Transaction entity)
        {
            entity.id
             = Guid.NewGuid().ToString();
           
            await _context.SaveAsync<Transaction>(entity);
        }
    }
}
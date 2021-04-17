using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using EvanApi1.Data;
using EvanApi1.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EvanApi1
{
    public interface IInterestMysqlRepository
    {
        Task<IEnumerable<AccountType>> Allaccounttype();
        Task<int> Addaccounttype(AccountType entity);
        Task<int> Deleteaccounttype(AccountType id);
        Task<int> Updateaccounttype(AccountType entity);


        Task<IEnumerable<Account>> AllAccounts();
        Task<int> AddAccount(Account entity);
        Task<int> DeleteAccount(Account id);
        Task<int> UpdateAccount(Account entity);


        Task<IEnumerable<Transaction>> AllTransactions();
        Task<int> AddTransaction(Transaction entity);
        Task<int> DeleteTransaction(Transaction id);
        Task<int> UpdateTransaction(Transaction entity);


    }
    public class InterestMysqlRepository : IInterestMysqlRepository
    {
        CoopContext _context;

        public InterestMysqlRepository(CoopContext context)
        {
            _context = context;
        }

        public async Task<int> Addaccounttype(AccountType entity)
        {
            entity.id
             = Guid.NewGuid().ToString();
            _context.AccountTypes.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountType>> Allaccounttype()
        {
            return (IEnumerable<AccountType>)await _context.AccountTypes.ToListAsync();
        }

        public async Task<int> Deleteaccounttype(AccountType id)
        {
            _context.AccountTypes.Remove(id);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Updateaccounttype(AccountType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAccount(Account entity)
        {
            entity.id
             = Guid.NewGuid().ToString();
            _context.Accounts.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> AllAccounts()
        {
            return (IEnumerable<Account>)await _context.Accounts.ToListAsync();
        }

        public async Task<int> DeleteAccount(Account id)
        {
            _context.Accounts.Remove(id);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAccount(Account entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Transaction>> AllTransactions()
        {
            return (IEnumerable<Transaction>)await _context.Transactions.ToListAsync();
        }

        public async Task<int> DeleteTransaction(Transaction id)
        {
            _context.Transactions.Remove(id);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTransaction(Transaction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }


        public async Task<int> AddTransaction(Transaction entity)
        {
            entity.id
             = Guid.NewGuid().ToString();

            _context.Transactions.Add(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
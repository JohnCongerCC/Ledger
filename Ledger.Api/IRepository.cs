using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ledger.Models;

namespace Ledger
{
    public interface IRepository
    {
        //Async Gets
        Task<List<Models.AccountOwner>> GetOwnersAsync();
        Task<decimal> GetBalanceAsync(string ownerName, string accountName, string ticker);

        List<AccountOwner> Owners { get; set; }
        void AddOwner(string name);
        void AddAccount(string ownerName, string accountName);
        void AddEntry(string ownerName, string accountName, string ticker, decimal amount, DateTime date);
        
    }
}

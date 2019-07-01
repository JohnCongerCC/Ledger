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

        List<AccountOwner> Owners { get; set; }
        void AddOwner(string name);
    }
}

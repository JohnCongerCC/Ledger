﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ledger.Models;

namespace Ledger
{
    public class MemoryDB : IRepository
    {
        public static readonly Lazy<MemoryDB> Instance = new Lazy<MemoryDB>(() => new MemoryDB());
        public List<AccountOwner> Owners { get; set; } = new List<AccountOwner>();
        public async Task<List<AccountOwner>> GetOwnersAsync()
        {
            var t = new Task<List<AccountOwner>>(() => { return Owners; });
            t.Start();
            return await t;
        }

        public void AddOwner(string name)
        {
            Owners.Add(new AccountOwner { Name = name, Accounts = new List<Models.Account>() });
        }

        public void AddAccount(string ownerName, string accountName)
        {
            var owner = Owners.Where(w => w.Name == ownerName).FirstOrDefault();
            if (owner == null)
            {
                owner = new AccountOwner { Name = ownerName, Accounts = new List<Models.Account>() };
                Owners.Add(owner);
            }
            if (owner.Accounts == null)
                owner.Accounts = new List<Models.Account>();
            owner.Accounts.Add(new Models.Account { Name = accountName });
        }
    }
}

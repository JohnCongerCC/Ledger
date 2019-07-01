using System;
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
        public List<Asset> Assets { get; set; } = new List<Asset>() { new Asset { Ticker = "BTC" } };
        public List<AccountEntry> AccountEntries { get; set; } = new List<AccountEntry>();
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
                owner.Accounts = new List<Account>();

            owner.Accounts.Add(new Account { Name = accountName, OwnerName = ownerName,
                                             Entries = AccountEntries.Where(w => w.Account.Name == ownerName 
                                                                                            && w.Account.OwnerName == ownerName).OrderBy(o => o.Date).ToList()});
        }

        public void AddEntry(string ownerName, string accountName, string ticker, decimal amount, DateTime date)
        {
            var asset = Assets.Where(w => w.Ticker == ticker).FirstOrDefault();
            if (asset == null)
                throw new Exception("unable to find asset with ticker:" + ticker);

            var owner = Owners.Where(w => w.Name == ownerName).FirstOrDefault();
            if (owner == null)
                throw new Exception("unable to find account owner");

            var account = owner.Accounts.Where(w => w.Name == accountName).FirstOrDefault();
            if (account == null)
                throw new Exception("unable to find account owned by:" + owner.Name);

            AccountEntries.Add(new AccountEntry { Account = account, Asset = asset, Date = date, Amount = amount });
        }

        public async Task<decimal> GetBalanceAsync(string ownerName, string accountName, string ticker)
        {
            var t = new Task<decimal>(() =>
            {
                var Total = AccountEntries.Where(w => w.Account.OwnerName == ownerName && w.Account.Name == accountName && w.Asset.Ticker == ticker).Sum(s => s.Amount);
                return Total;
            });
            t.Start();
            return await t;
        }
    }
}

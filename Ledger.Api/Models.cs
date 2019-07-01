using System.Collections.Generic;
using System.Linq;

namespace Ledger.Models
{
    public class AccountOwner
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }

    public class Asset
    {
        public string Ticker { get; set; }
    }

    public class Account
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public List<AccountEntry> Entries { get; set; }
        public decimal GetBalance(string ticker)
         => Entries.Where(w => w.Asset.Ticker == ticker).Sum(s => s.Amount);
    }
    public class AccountEntry
    {
        public Asset Asset { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
    }
}

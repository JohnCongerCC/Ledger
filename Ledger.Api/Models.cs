﻿using System.Collections.Generic;

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
        
    }
    public class AccountEntry
    {
        public Account Account { get; set; }
        public Asset Asset { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
    }
}

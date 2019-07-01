using NUnit.Framework;
using System;
using System.Linq;


namespace Ledger
{
    public class Tests
    {
        public IRepository Repo { get; set; }

        [SetUp]
        public void Setup() => Repo = MemoryDB.Instance.Value;

        [Test]
        public void AddUserTest()
        {
            var OwnerName = RandomString(25);
            Repo.AddOwner(OwnerName);
            Assert.IsNotNull(Repo.Owners.Where(w => w.Name == OwnerName).FirstOrDefault());
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
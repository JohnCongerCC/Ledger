using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IRepository Repo { get; set; }
        public AccountController() => Repo = MemoryDB.Instance.Value;

        [HttpGet]
        public async Task<IActionResult> Get()
        => Ok(await Repo.GetOwnersAsync());

        [HttpPost]
        [Route("NewOwner")]
        public void Post(string OwnerName)
       => Repo.AddOwner(OwnerName);

        [HttpPost]
        [Route("NewAccount")]
        public void Post(string OwnerName, string AccountName)
       => Repo.AddAccount(OwnerName, AccountName);
    }
}

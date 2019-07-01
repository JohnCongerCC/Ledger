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
        public void Post(string OwnerName)
       => Repo.AddOwner(OwnerName);
    }
}

using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class ClientSourcesController : ControllerBase
    {
        private ConfigurationDbContext _db;

        // public ClientSourcesController(
        //     ConfigurationDbContext db
        // )
        // {
        //     _db = db;
        // }

        // public List<Client> GetList()
        // {
        //     var clients = _db.Clients
        //         .OrderByDescending(x => x.Id)
        //         .Take(10)
        //         .ToList()
        //         .Select(x => x.ToModel())
        //         .ToList();

        //     return clients;
        // }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Clients;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Is4Ef = IdentityServer4.EntityFramework.Entities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private readonly ConfigurationDbContext _db;

        public Clients(
            ConfigurationDbContext db
        )
        {
            _db = db;
        }

        [HttpGet]
        public async Task<LimitResponse<Client>> Get(DefaultRequest request)
        {
            var total = _db.Clients.CountAsync();

            var clients = _db.Clients
                .OrderByDescending(x => x.Id)
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToListAsync();

            return new LimitResponse<Client>
            {
                List = (await clients).Select(x => x.ToModel()).ToList(),
                Total = await total
            };
        }

        [HttpPut]
        public void Put([FromBody, Required] PutRequest client)
        {
            var en = new Client
            {
                ClientId = client.ClientId,
                ClientSecrets = client.ClientSecrets
                    .Select(x => new Secret(x.Value.Sha256(), x.ExpresIn))
                    .ToList(),
                AllowedScopes = client.Scopes
            }.ToEntity();
            _db.Clients.Add(en);
            _db.SaveChanges();
        }

        [HttpPost]
        public void Post([FromBody, Required] Client client)
        {
            var en = client.ToEntity();
            _db.Clients.Update(en);
            _db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string clientId)
        {
            _db.Clients.Remove(new Is4Ef.Client { ClientId = clientId });
            _db.SaveChanges();
        }
    }
}
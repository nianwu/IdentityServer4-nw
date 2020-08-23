using System.Linq;
using System.Threading.Tasks;
using Api.Models;
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
    public class IdentityResourcesController : ControllerBase
    {
        private readonly ConfigurationDbContext _db;

        public IdentityResourcesController(
            ConfigurationDbContext db
        )
        {
            _db = db;
        }

        [HttpPut]
        public void Put(IdentityResource request)
        {
            _db.IdentityResources.Add(request.ToEntity());
            _db.SaveChanges();
        }

        [HttpDelete]
        public void Delete(string name)
        {
            _db.IdentityResources.Remove(new Is4Ef.IdentityResource
            {
                Name = name
            });
            _db.SaveChanges();
        }

        [HttpGet]
        public async Task<LimitResponse<IdentityResource>> GetAsync([FromQuery] DefaultRequest request)
        {
            var total = _db.IdentityResources.CountAsync();

            var list = _db.IdentityResources
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToList()
                .Select(x => x.ToModel())
                .ToList();

            return new LimitResponse<IdentityResource>
            {
                List = list,
                Total = await total
            };
        }

        [HttpPost]
        public void Post(IdentityResource request)
        {
            _db.IdentityResources.Update(request.ToEntity());
            _db.SaveChanges();
        }
    }
}
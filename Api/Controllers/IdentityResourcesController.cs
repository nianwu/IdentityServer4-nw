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
            var total = await _db.IdentityResources.CountAsync();

            var list = await _db.IdentityResources
                .Skip(request.Skip)
                .Take(request.Limit)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ToListAsync();

            return new LimitResponse<IdentityResource>
            {
                List = list.Select(x => x.ToModel()).ToList(),
                Total = total
            };
        }

        [HttpGet("{name}")]
        public async Task<IdentityResource> GetAsync(string name)
        {
            var result = await _db.IdentityResources
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .SingleAsync(x => x.Name == name);

            return result.ToModel();
        }

        [HttpPost]
        public void Post(IdentityResource request)
        {
            _db.IdentityResources.Update(request.ToEntity());
            _db.SaveChanges();
        }
    }
}
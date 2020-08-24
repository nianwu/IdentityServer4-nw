using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/ApiResources")]
    [ApiController]
    public class ApiResourcesController : ControllerBase
    {
        private readonly ConfigurationDbContext _db;

        public ApiResourcesController(
            ConfigurationDbContext db
            , IMapper mapper
        )
        {
            _db = db;
        }

        [HttpPut]
        public async Task PutAsync(ApiResource request)
        {
            await _db.ApiResources.AddAsync(request.ToEntity());
            await _db.SaveChangesAsync();
        }

        [HttpDelete("{name}")]
        public async Task DeleteAsync(string name)
        {
            _db.Remove(new ApiResource(name).ToEntity());

            await _db.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<LimitResponse<ApiResource>> GetAsync([FromQuery] DefaultRequest request)
        {
            var list = await _db.ApiResources
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToListAsync();

            var total = await _db.ApiResources.CountAsync();

            return new LimitResponse<ApiResource>
            {
                List = list.Select(x => x.ToModel()).ToList(),
                Total = total
            };
        }

        [HttpGet("{name}")]
        public async Task<ApiResource> GetAsync(string name)
        {
            var result = await _db.ApiResources
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .SingleAsync(x => x.Name == name);

            return result.ToModel();
        }

        [HttpPost]
        public void PostAsync(ApiResource entity)
        {
            _db.Update(entity);
            _db.SaveChanges();
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/ApiResources")]
    [ApiController]
    public class ApiResourcesController : ControllerBase
    {
        private readonly ConfigurationDbContext _db;
        private readonly IMapper _mapper;

        public ApiResourcesController(
            ConfigurationDbContext db
            , IMapper mapper
        )
        {
            _db = db;
            _mapper = mapper;
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
        public LimitResponse<ApiResource> GetAsync([FromQuery] DefaultRequest request)
        {
            var list = _db.ApiResources
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToList()
                .Select(x => x.ToModel())
                .ToList();

            var total = _db.ApiResources.Count();

            return new LimitResponse<ApiResource>
            {
                List = list,
                Total = total
            };
        }

        [HttpPost]
        public void PostAsync(ApiResource entity)
        {
            _db.Update(entity);
            _db.SaveChanges();
        }
    }
}
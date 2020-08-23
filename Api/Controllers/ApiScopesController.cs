using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.ApiScopes;
using AutoMapper;
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
    public class ApiScopesController : ControllerBase
    {
        private readonly ConfigurationDbContext _db;

        public ApiScopesController(
            ConfigurationDbContext db
            , IMapper mapper
        )
        {
            _db = db;
        }

        [HttpPut]
        public void Put([FromBody, Required] ApiScope request)
        {
            var en = new ApiScope(request.Name, request.DisplayName).ToEntity();
            _db.ApiScopes.Add(en);
            _db.SaveChanges();
        }

        [HttpDelete("{name}")]
        public void Delete([FromRoute] string name)
        {
            _db.ApiScopes.Remove(new Is4Ef.ApiScope { Name = name });
            _db.SaveChanges();
        }

        [HttpGet]
        public async Task<LimitResponse<ApiScope>> Get([FromQuery] DefaultRequest request)
        {
            var total = _db.ApiScopes.CountAsync();

            var apiScopes = _db.ApiScopes
                .OrderByDescending(x => x.Name)
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToListAsync();

            return new LimitResponse<ApiScope>
            {
                List = (await apiScopes).Select(x => x.ToModel()).ToList(),
                Total = await total
            };
        }

        [HttpPost("[action]")]
        public ApiScope DefaultConstructor(ApiScopeDefaultConstructorRequest request)
        {
            return new ApiScope(request.Name, request.DisplayName);
        }

        [HttpPost]
        public void Post([FromBody, Required] ApiScope request)
        {
            var en = request.ToEntity();
            _db.ApiScopes.Update(en);
            _db.SaveChanges();
        }
    }
}
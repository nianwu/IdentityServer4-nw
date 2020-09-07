using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Security.Claims;
using Server.Entities;

namespace Server
{
    public static class Extend
    {
        public static string Md5(this string e)
        {
            var result = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(e))).Replace("-", "");
            return result;
        }

        public static string SaltMd5(this string e, object salt)
        {
            var source = JsonConvert.SerializeObject(new
            {
                e,
                salt
            });

            var result = source.Md5();

            return result;
        }

        public static IServiceCollection AddUserAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<UserClaim, Claim>()
                    .ConstructUsing(x => new Claim(x.Name, x.Value))
                    .ReverseMap()
                    .ForMember(x => x.Name, x => x.MapFrom(o => o.Type))
                    .ForMember(x => x.Value, x => x.MapFrom(o => o.Value));

                config.CreateMap<Entities.UserEntity, Models.User>()
                    .ReverseMap();

            }, typeof(Startup));

            return services;
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using utils;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigAuth(IServiceCollection services)
        {
            var config = new Options();
            Configuration.Bind(config);
            var migrationsAssembly = typeof(Server.Startup).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                // this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(options =>
                {
                    // options.ConfigureDbContext = builder => builder.UseSqlite(connectionString);
                    options.ConfigureDbContext = b => b.UseSqlServer(config.ConnectionStrings.Mssql,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                });
        }

        public void ConfigSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "oauth2 sources manager",
                    Description = "oauth2 资源管理api",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nianwu",
                        Email = "yuanzhiyuan_yzy@163.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void ConfigAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                // options.CreateMap<Models.Clients.ClientDefaultConstructorRequest, Client>();
                // options.CreateMap<Models.Clients.PutSecrets, Secret>()
                //     .ConstructUsing(request=> new Secret(request.Value.Sha256(), request.ExpresIn))
                //     .ForMember(x=>x.Value, x=>x.MapFrom(o=>o.Value.Sha256()));
            }, typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigAuth(services);
            ConfigSwagger(services);
            ConfigAutomapper(services);

            services.Configure<Options>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^/?$", "/swagger"));

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

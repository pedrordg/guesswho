using Autofac;
using AutoMapper;
using GuessWho.Execution.Automapper;
using GuessWho.Execution.Table;
using GuessWho.Infrastructure.SignalR;
using GuessWho.Models;
using GuesWho.ExecutionDependencyInjection;
using Matrix.PaymentGateway.Infra.Blob.Extensions;
using Matrix.PaymentGateway.Infra.TableStorage.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using System;

namespace GuessWho.App
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddMicrosoftIdentityWebApi(options =>
                   {
                       _configuration.Bind("AzureAdB2C", options);

                       options.TokenValidationParameters.NameClaimType = "name";
                   },
            options => { _configuration.Bind("AzureAdB2C", options); });
            services.AddAuthorization(options =>
            {
                //// Create policy to check for the scope 'read'
                //options.AddPolicy("ReadScope",
                //    policy => policy.Requirements.Add(new ScopesRequirement("read")));
            });

            services.AddControllers();

            services.AddResponseCaching();

            //services.AddMvc();
            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .WithOrigins("http://localhost:4200");
            }));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSignalR()
                    .AddAzureSignalR(options =>
                    {
                        options.ConnectionString = _configuration.GetConnectionString("SignalR");
                        options.ApplicationName = "guesswho";
                    });

            services.AddAutoMapper(typeof(GuessWhoProfile));

            services.ConfigureBlob();

            services.ConfigureStorageTable()
                    .AddTable<IdolEntity>("idol")
                    .AddTable<ThemeEntity>("theme")
                    .Seed();
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ExecutionModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseResponseCaching();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromMinutes(5)
                    };
                context.Response.Headers[HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OldChatHub>("/chat");
            });
        }
    }
}

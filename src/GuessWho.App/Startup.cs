using Autofac;
using AutoMapper;
using GuessWho.Execution.Automapper;
using GuessWho.Execution.Table;
using GuessWho.Infrastructure.SignalR;
using GuessWho.Models;
using GuesWho.ExecutionDependencyInjection;
using Matrix.PaymentGateway.Infra.Blob.Extensions;
using Matrix.PaymentGateway.Infra.TableStorage.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessWho.App
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();

            services.AddControllers();

            services.AddMvc();
            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
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

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OldChatHub>("/chat");
            });
        }
    }
}

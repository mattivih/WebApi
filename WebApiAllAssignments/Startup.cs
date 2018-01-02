using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiAllAssignments.Mongodb;
using WebApiAllAssignments.Processors;
using WebApiAllAssignments.Repositories;

namespace WebApiAllAssignments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<PlayerProcessor>();
            services.AddSingleton<ItemProcessor>();
            //services.AddSingleton<IPlayerRepository, PlayerInMemoryRepository>();
            services.AddSingleton<IPlayerRepository, MongoDbRepository>();
            services.AddSingleton<MongoDbClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
﻿using System.IO;
using AutoMapper;
using Masiv.OnlineGames.Business;
using Masiv.OnlineGames.Business.Implementation;
using Masiv.OnlineGames.Business.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Configuration;
using Masiv.OnlineGames.DataAccess.MongoDb.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;
using Masiv.OnlineGames.DataAccess.MongoDb.Repository;
using Masiv.OnlineGames.Models;
using Masiv.OnlineGames.Models.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Masiv.OnlineGames.RouletteApi
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
            services.AddControllers();
            services.AddSingleton<ServiceResponse>();
            services.AddTransient<Roulette>();
            services.AddTransient<RouletteModel>();
            services.AddSingleton<IRouletteRepository, RouletteRepository>();
            services.AddSingleton<IRouletteBll, RouletteBll>();
            //Configure MongoDb
            services.Configure<StoreDataBaseSettings>(
                Configuration.GetSection(nameof(StoreDataBaseSettings)));

            services.AddSingleton<IStoreDataBaseSettings>(sp =>
            sp.GetRequiredService<IOptions<StoreDataBaseSettings>>().Value);
            services.AddControllers().AddNewtonsoftJson();
            //Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RouletteApi", Version = "v1" });
            });
            //AutoMapper
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });
            IMapper iMapper = mapperConfig.CreateMapper();
            services.AddSingleton(iMapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Games");
            });
        }
    }
}

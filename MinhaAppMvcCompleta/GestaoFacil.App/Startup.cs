using GestaoFacil.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GestaoFacil.App.Configurations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace GestaoFacil.App
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        /*o trecho abaixo permiti acesso para cada tipo de ambiente (produção/teste/desenvolviemnto)*/
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(hostingEnvironment.ContentRootPath)
                 .AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                 .AddEnvironmentVariables();

            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();

            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

     
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddIdentityConfiguration(Configuration);

            //ESSE CONTEXTO É O MEU DA MINHA APLICAÇÃO NORMAL.
            services.AddDbContext<MeuDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));


            services.AddControllersWithViews();
            services.AddRazorPages();

            /*
             Na linha abaixo estou dizendo para procurar dentro do assembky "gestaofacil.app" procurar por "profile" de herança do automapper.
           */
            services.AddAutoMapper(typeof(Startup));


            services.AddMcvConfiguration();
            services.ResolveDepencencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalizationConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
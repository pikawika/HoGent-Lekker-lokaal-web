using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LekkerLokaal.Data;
using LekkerLokaal.Models;
using LekkerLokaal.Services;
using LekkerLokaal.Models.Domain;
using System.Security.Claims;
using LekkerLokaal.Data.Repositories;
using LekkerLokaal.Filters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace LekkerLokaal
{
    public class Startup
    {
        //Aangepast om met secrets om te kunnen gaan
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
            foreach (var item in configuration.AsEnumerable())
            {
                Configuration[item.Key] = item.Value;
            }
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                facebookOptions.Fields.Add("first_name");
                facebookOptions.Fields.Add("last_name");
                facebookOptions.Fields.Add("gender");
            });

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
                twitterOptions.RetrieveUserDetails = true;
            });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.AddScoped<CartSessionFilter>();
            services.AddScoped<LekkerLokaalDataInitializer>();
            services.AddSession();
            services.AddScoped<IBonRepository, BonRepository>();
            services.AddScoped<ICategorieRepository, CategorieRepository>();
            services.AddScoped<IHandelaarRepository, HandelaarRepository>();
            services.AddScoped<IGebruikerRepository, GebruikerRepository>();
            services.AddScoped<IBestellijnRepository, BestellijnRepository>();
            services.AddScoped<IBestellingRepository, BestellingRepository>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.Configure<AuthMessageSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LekkerLokaalDataInitializer datainit)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "zoeken",
                    template: "{controller=Home}/{action=Zoeken}/{ZoekSoort}/{ZoekKey?}/{Categorie?}/{MaxStartPrijs?}");

                routes.MapRoute(
                    name: "detail",
                    template: "{controller=Home}/{action=Detail}/{Id?}");

                routes.MapRoute(
                    name: "handelaarsVerzoekEvaluatie",
                    template: "{controller=Admin}/{action=HandelaarVerzoekEvaluatie}/{Id?}");

                routes.MapRoute(
                    name: "handelaarsBewerken",
                    template: "{controller=Admin}/{action=HandelaarBewerken}/{Id?}");

                routes.MapRoute(
                    name: "winkelwagen",
                    template: "{controller=Winkelwagen}/{action=Add}/{Id}/{Prijs}/{Aantal}");

                routes.MapRoute(
                    name: "geneerGekochteBon",
                    template: "{controller=Manage}/{action=BonAanmaken}/{Id}");

                routes.MapRoute(
                    name: "Account",
                    template: "{controller=Account}/{action=CheckoutMethode}/{checkoutId}/{returnUrl}");
            });
            //datainit.InitializeData().Wait();
        }
    }
}

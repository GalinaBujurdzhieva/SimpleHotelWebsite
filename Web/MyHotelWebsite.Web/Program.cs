namespace MyHotelWebsite.Web
{
    using System;
    using System.Reflection;

    using Hangfire;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MyHotelWebsite.Common.CustomModelBinders;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Data.Seeding;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Services.Messaging;
    using MyHotelWebsite.Web.ViewModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHangfire(hangfire =>
            {
                hangfire.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                hangfire.UseSimpleAssemblyNameTypeSerializer();
                hangfire.UseRecommendedSerializerSettings();
                hangfire.UseColouredConsoleLogProvider();
                hangfire.UseSqlServerStorage(
                             configuration.GetConnectionString("HangfireConnection"),
                             new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                    });

                var backgroundServer = new BackgroundJobServer(new BackgroundJobServerOptions
                {
                    ServerName = "hangfire-test",
                });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                });
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHangfireServer();
            services.AddSingleton(configuration);
            services.AddHttpContextAccessor();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IBlogsService, BlogsService>();
            services.AddTransient<IDishesService, DishesService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<IGuestsService, GuestsService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IReservationsService, ReservationsService>();
            services.AddTransient<IShoppingCartsService, ShoppingCartsService>();
        }

        private static void Configure(WebApplication app/*, IBackgroundJobClient backgroundJobs*/)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<IRoomsService>("is-occupied-turned-true", service => service.OccupyRoomsAsync(), Cron.Daily);
            RecurringJob.AddOrUpdate<IRoomsService>("is-occupied-turned-false", service => service.LeaveOccupiedRoomsAsync(), Cron.Daily);
            RecurringJob.AddOrUpdate<IRoomsService>("is-reserved-turned-false", service => service.RemoveIsReservedPropertyOfNotReservedRooms(), Cron.Daily);
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints => endpoints.MapHangfireDashboard());

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            // app.MapRazorPages();
        }
    }
}

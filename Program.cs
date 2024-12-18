using Fantastic4News.Data;
using Fantastic4News.Helper;
using Fantastic4News.Models.Db;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var connectionString1 = builder.Configuration.GetConnectionString("ServerConnection") ?? throw new InvalidOperationException("Connection string 'ServerConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddControllersWithViews();
			builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor
			builder.Services.AddDistributedMemoryCache(); // Required for session state


			builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

			builder.Services.AddTransient<IEmailSender, EmailSender>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); // Add this line to enable session middleware

            app.UseRouting();

            app.UseAuthorization();
            //app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Customer}/{action=Index}/{id?}");
            app.MapRazorPages();


            //creates a new scope using the application's service provider. Scopes, for managing the lifetime of services.
            using (var scope = app.Services.CreateScope())
            {
                //This retrieves the service provider for the current scope.
                var services = scope.ServiceProvider;

                //This gets an instance of ApplicationDbContext from the service provider. GetRequiredService<T>()
				//ensures that the service is available and throws an exception if it's not.
                var context = services.GetRequiredService<ApplicationDbContext>();

                //it will delete whole db and migrate every time while running
                //context.Database.EnsureDeleted();
                //context.Database.Migrate();


                if (!context.Articles.Any())
				{
                    try
                    {
                        SeedData.InitializeDataSeeding(context, services).Wait(); // Seed the database
                    }
                    catch (Exception ex)
                    {
                        // Log errors or handle exceptions
                        Console.WriteLine("An error occurred while seeding the database.", ex); throw;
                    }

                }

				
			}

            app.Run();
		}
	}

       
}

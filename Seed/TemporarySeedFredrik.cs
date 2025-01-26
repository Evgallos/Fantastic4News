using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Fantastic4News.Seed
{
    public static class TemporarySeedFredrik
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                await SeedUsers_FredrikAndersson(userManager);
            }

            static async Task SeedUsers_FredrikAndersson(UserManager<User> userManager)
            {
                User[] fredriksUsers =
                {
                    new User
                    {
                        FirstName = "Fredrik",
                        LastName = "Andersson",
                        Email = "fredrik_admin@new.se",
                        UserName = "Fredrik_Admin",
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Fredrik2",
                        LastName = "Andersson",
                        Email = "fredrik_editor@new.se",
                        UserName = "Fredrik_Editor",
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Fredrik3",
                        LastName = "Andersson",
                        Email = "fredrik_journalist@new.se",
                        UserName = "Fredrik_Journalist",
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Fredrik4",
                        LastName = "Andersson",
                        Email = "fredrik_customer@new.se",
                        UserName = "Fredrik_Customer",
                        EmailConfirmed = true
                    },
                };

                var password = "*Qwerty123";

                foreach (var usr in fredriksUsers)
                {
                    if (userManager.FindByEmailAsync(usr.Email).Result == null)
                    {
                        // Seed the user
                        IdentityResult userResult = await userManager.CreateAsync(usr, password);

                        if (userResult.Succeeded)
                        {
                            // Add role to this user
                            if (usr.UserName.Contains("Admin"))
                            {
                                userManager.AddToRoleAsync(usr, "Admin").Wait();
                            }
                            else if (usr.UserName.Contains("Customer"))
                            {
                                userManager.AddToRoleAsync(usr, "Customer").Wait();
                            }
                            else if (usr.UserName.Contains("Editor"))
                            {
                                userManager.AddToRoleAsync(usr, "Editor").Wait();
                            }
                            else if (usr.UserName.Contains("Journalist"))
                            {
                                userManager.AddToRoleAsync(usr, "Journalist").Wait();
                            }
                        }
                    }
                }
            }
        }
    }
}
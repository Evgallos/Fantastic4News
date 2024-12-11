using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;
using System.Linq;

namespace Fantastic4News.Helper
{
    public class SeedData
    {
        private static ApplicationDbContext _db;
        public static RoleManager<IdentityRole> roleManager;
        public static UserManager<User> userManager;

        public static async Task InitializeDataSeeding(ApplicationDbContext db, IServiceProvider services)
        {
            if (db is null) throw new NullReferenceException(nameof(ApplicationDbContext));

            _db = db;

            if (_db.Articles.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager is null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<User>>();
            if (userManager is null) throw new NullReferenceException(nameof(UserManager<User>));


            var roleNames = new[] { "Admin", "Customer", "Journalist","Editor" };
            await CreateRoles(roleNames);

            // User admin = await CreateAdmin();

            await CreateUserwithRole();
            

            var cateGories = new[] { "National", "International", "Politics","Local" };
            await CreateCategoies(cateGories);


            var subscritionsNames = new[] { "Free", "Standard", "Primium" };
            await CreateSubsciptionsType(subscritionsNames);


            await CreateArticles();

        }




        private static async Task CreateArticles()
        {
            List<Article> articles = new();

            Article art = new();
            art.HeadLine = "New rules for Syrians";
            art.Content = "In Sweden, many people have fled Syria because of the leader Bashar al-Assad. But now the leader al-Assad has lost power in the country.He has fled to Russia.\n\nGroups of men with guns have taken over Syria. They are Islamists.The United Nations says the largest group are terrorists.\n\nNo one knows what will happen in Syria now. Or who will lead the country.\n\nNow several countries have decided to pause the permits for people coming from Syria. It is not certain that they need permission to stay, asylum.\n\nSweden is one of the countries that has taken a break.\n\nBut Syrians who have received a decision that they are to be deported are currently allowed to stay in Sweden.\n\nThe rules shall apply until further notice.\n\n- We are following the situation in Syria closely, says Carl Bexelius who is head of the Swedish Migration Agency.The Sweden Democrats party wants to take back all Syrians' permission to stay in Sweden, even if groups of Islamists take over the country.";
            art.LinkText = art.HeadLine;
            art.DateStamp = DateTime.Now;
            art.ImageLink = "https://8sidor.se/wp-content/uploads/2024/12/Syrien.jpg";
            art.ContentSummary = art.Content.Substring(0, art.Content.IndexOf("."));
            art.CategoryId = _db.Categories.Where(c => c.Name == "International").Select(c=> c.Id).FirstOrDefault();
            art.UserId = _db.Users.Where(u => u.FirstName == "Fantastic").Select(u => u.Id).FirstOrDefault();
            
            articles.Add(art);

            Article art1 = new();

            art1.HeadLine = "New driver's license coming soon";
            art1.Content = "The Swedish driving license has looked the same for eight years. Now comes a new one. From January 2025, you can get the new driver's license.\n\nThe new driver's license is similar to the old one. But some things have changed.\n\nNow the part that switches between the image of the face and social security number is in the clear box.\n\nThe new driver's license mustalso be safer.\r\nIt should be more difficult to make fake driver's licenses.\n\nThe old driver's license is still valid. You only need to get a new one if the time on our old one runs out. When the Swedish Transport Agency alks about new driving licences, they show a picture of a driving licence.It's not a real driver's license. It is an example of what the driver's license can look like. But the photo on the driver's license has always been of a real person.\n\nThat is not the case this year.\r\nThis year, the person in the picture is nmade with the help of smart computers, AI. She doesn't really exist.";
            art1.LinkText = art1.HeadLine;
            art1.ImageLink = "https://8sidor.se/wp-content/uploads/2024/12/241210-korkort-684f6d05-a001nh.jpg";
            art1.ContentSummary = art1.Content.Substring(0, art1.Content.IndexOf("."));
            art1.DateStamp = DateTime.Now;
            art1.CategoryId = _db.Categories.Where(c => c.Name == "National").Select(c => c.Id).FirstOrDefault();
            art1.UserId = _db.Users.Where(u => u.FirstName == "John").Select(u => u.Id).FirstOrDefault();


            articles.Add(art1);

            Article art2 = new();
            art2.HeadLine = "The Nobel Prize is visible in Stockholm";
            art2.Content = "On December 10, the Nobel Prizes will be awarded. Then there is extra light in Stockholm.\r\nIn several different places in Stockholm, artists have made\r\nspecial works of art with light. They shine until December 15th.\r\nThe artworks should make people think about important ideas\r\nthat changed the world. The Nobel Prize is awarded to researchers who have discovered important things.\r\nOn Nobel Day, this year's winners receive their prizes. They receive the prizes from the king. The prizes are awarded\r\nin the Konserthuset in Stockholm. Then there will be a big party\r\nin the City Hall in Stockholm.\r\nOne of the prizes, the Peace Prize, is awarded in Oslo, Norway.\r\nYou can watch the awards ceremony and party on TV on December 10. It is shown on the channel SVT 1.\r\nAt 12.50 you can watch when the Nobel Peace Prize is awarded in Oslo. At 15.55 the award ceremony starts in the Konserthuset in Stockholm. At 20.00 is the party  in the City Hall in Stockholm.\r\n";
            art2.LinkText = art2.HeadLine;
            art2.ImageLink = "https://8sidor.se/wp-content/uploads/2024/12/nobel1.jpg";
            art2.ContentSummary = art2.Content.Substring(0, art2.Content.IndexOf("."));
            art2.DateStamp = DateTime.Now;
            art2.CategoryId = _db.Categories.Where(c => c.Name == "National").Select(c => c.Id).FirstOrDefault();
            art2.UserId = _db.Users.Where(u => u.FirstName == "John").Select(u => u.Id).FirstOrDefault();

            articles.Add(art2);

            Article art3 = new();
            art3.HeadLine = "Music help has started";
            art3.Content = "The Music Help program has started. Three people will lead the program around the clock for a week. The program can be heard and seen on television, radio and the internet.\r\nLinnéa Wikblad, Assia Dahir from Sveriges Radio will lead the program with Emil Hansius. He is known from the site Youtube. The program is broadcast every year at this time from different cities. This year it is broadcast from Stora torget in Sundsvall.\r\nThere, the presenters will take turns leading the program\r\nfrom a small house with large windows. Anyone who goes to the square can see them. They will be visited by famous people and by artists playing music.\r\nWhen Musikhjälpen started on Monday evening , among others, the artist Daniel Adams Ray played . The program is for raising money for people who need help. This year, the money will go\r\nto pregnant women in poor countries or in countries at war. Every two minutes, a woman in the world dies because she is pregnant or because she is about to give birth.\r\nIt shouldn't be like that.That's according to the organization Radiohjälpen, which collects the money. - Everyone has the right to survive their pregnancy, says the organization Radiohjälpen.\r\nThe music aid started on Monday evening, December 9\r\nat 8 p.m. You can watch Musikhjälpen on SVT Play, among others.\r\n";
            art3.LinkText = art3.HeadLine;
            art3.ImageLink = "https://8sidor.se/wp-content/uploads/2024/12/musikhjalpen1.jpg";
            art3.ContentSummary = art3.Content.Substring(0, art3.Content.IndexOf("."));
            art3.DateStamp = DateTime.Now;
            art3.CategoryId = _db.Categories.Where(c => c.Name == "National").Select(c => c.Id).FirstOrDefault();
            art3.UserId = _db.Users.Where(u => u.FirstName == "John").Select(u => u.Id).FirstOrDefault();

            articles.Add(art3);

            Article art4 = new();
            art4.HeadLine = "You can grow mushrooms at home";
            art4.Content = "Now the season for mushrooms in the forest is over. But that doesn't mean you have to wait until next year to have your own mushroom. You can grow mushrooms at home.\r\nGrowing mushrooms is not difficult. With the right things, it's easy. In a few weeks, you will have oyster shards ready. You can have it in your pasta or soup.\r\n- The oyster shell is most commonly cultivated. It grows quickly. That's what Elisabeth Bååth says. She is an expert on mushrooms. On the internet you can find companies\r\nthat sell what you need to start growing yourself.\r\nFor oyster shucking , a cardboard box and a few weeks of cultivation are enough. For other mushrooms, you need\r\nmore time and a log of wood to grow the mushroom on. \r\n- Interest in mushrooms and growing them yourself has increased\r\nrecently, says Elisabeth Bååth. - In the past, it was mostly\r\npensioners who did it. But now there are young people\r\nwho also want to grow mushrooms. - It's great fun,\r\nshe says\r\n";
            art4.LinkText = art4.HeadLine;
            art4.ImageLink = "https://8sidor.se/wp-content/uploads/2024/12/241125-ttvpsvampodling-95cdb50f-a001nh.jpg";
            art4.ContentSummary = art4.Content.Substring(0, art4.Content.IndexOf("."));
            art4.DateStamp = DateTime.Now;
            art4.CategoryId = _db.Categories.Where(c => c.Name == "Local").Select(c => c.Id).FirstOrDefault();
            art4.UserId = _db.Users.Where(u => u.FirstName == "John").Select(u => u.Id).FirstOrDefault();

            articles.Add(art4);


            await _db.AddRangeAsync(articles);
            await _db.SaveChangesAsync();
        }




        private static async Task CreateSubsciptionsType(string[] subscritionsNames)
        {
            double pris = 0;
            foreach (var subscription in subscritionsNames)
            {
                if (_db.SubscriptionTypes.Any(s => s.TypeName == subscription)) continue;
                SubscriptionType st = new();
                if (subscription == "Free")
                {
                    st.TypeName = subscription;
                    st.Description = $"This{subscription} pakage is free package. ";
                    st.Price = 0;
                }
                else
                {
                    st.TypeName = subscription;
                    st.Description = $"This is {subscription} package";
                    pris += 75;
                    st.Price = pris;

                }

                await _db.AddAsync(st);
                await _db.SaveChangesAsync();

            }
        }



        private static async Task CreateCategoies(string[] cateGories)
        {
            var categories = new List<Category>();
            foreach (var catego in cateGories)
            {
                Category c = new() { Name = catego };
                categories.Add(c);
            }

            await _db.AddRangeAsync(categories);
            await _db.SaveChangesAsync();


        }

        private static async Task CreateUserwithRole()
        {
            List<User> newUsersList = new();

            var user = new User
            {
                FirstName = "Fantastic",
                LastName = "Fira",
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now,
                UserName = "admin@new.se",
                Email = "admin@new.se"

            };
            newUsersList.Add(user);

            var user1 = new User
            {
                FirstName = "John",
                LastName = "Smith",
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now,
                UserName = "journalist@new.se",
                Email = "journalist@new.se"
            };
            newUsersList.Add(user1);

            var user2 = new User
            {
                FirstName = "Edith",
                LastName = "Smith",
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now,
                UserName = "editor@new.se",
                Email = "editor@new.se"
            };
            newUsersList.Add(user2);

            foreach(var usr in newUsersList)
            {
                var result=await userManager.CreateAsync(usr, "S3cr3t!");
                if (!result.Succeeded) throw new Exception("Cant create user");
            }

            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.AddToRoleAsync(user1, "Journalist");
            await userManager.AddToRoleAsync(user2, "Editor");
            
            await _db.SaveChangesAsync();


            //var result = await userManager.CreateAsync(user, "S3cr3t!");


           
        }



        private static async Task CreateRoles(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                await _db.SaveChangesAsync();

                if (!result.Succeeded) throw new Exception("Cant create roles");
            }
        }
    }
}

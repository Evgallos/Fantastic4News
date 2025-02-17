### 📜 **Fantastic4News - README (About Section)**  

#### 🌟 **Project Overview**  
**Fantastic4News** is a modern and dynamic article-based news website developed by a team of four students as a school project. The name **Fantastic4News** was inspired by our extraordinary team, which included a talented backend developer who is visually impaired but an exceptional programmer.  

This project was built with a strong emphasis on **accessibility**, ensuring a seamless experience for visually impaired users by implementing **keyboard navigation, tab-friendly UI, and screen reader support**.  

#### 🛠️ **Technologies Used**  
The project is built using **ASP.NET Core MVC** and integrates various technologies and features, including:  
- **Frontend Development**: HTML, CSS, Bootstrap  
- **Backend Development**: C#, .NET, Entity Framework  
- **Authentication & Security**: Identity Authentication System  
- **APIs Integration**:  
  - Weather API  
  - Name Day Calendar API  
  - Statistics API  
- **Azure Services**:  
  - Azure Functions  
  - Azure Time Queue for email confirmations  
- **Views & Components**:  
  - MVC Views & Partial Views  
  - Custom Components  

#### 🎨 **My Role in the Project**  
As a **Frontend Developer**, I was responsible for:  
✔️ Designing and developing the **user interface** (HTML, CSS, Bootstrap)  
✔️ Creating a **responsive and accessible** layout for all devices  
✔️ Assisting in backend development and API integrations  
✔️ Designing the **logo** and all visual elements of the website  
✔️ Implementing **keyboard-friendly navigation** for visually impaired users  

#### ♿ **Accessibility Features**  
To enhance usability for visually impaired users, **Fantastic4News** includes:  
- **Keyboard Navigation**: Users can navigate using `Tab` and `Enter` keys.  
- **ARIA Landmarks & Labels**: Ensuring proper screen reader support.  
- **High Contrast Mode**: Improved readability for low-vision users.  
- **Skip to Content Link**: Allows users to bypass navigation and go directly to the main content.  

##### ✨ **Accessibility Code Example**  
Below is a **snippet** demonstrating a **skip navigation link** for screen readers and keyboard users:  

```yaml
<a href="#main-content" class="skip-link">Skip to main content</a>

<style>
  .skip-link {
    position: absolute;
    top: -40px;
    left: 10px;
    background: #000;
    color: #fff;
    padding: 8px;
    z-index: 100;
  }
  .skip-link:focus {
    top: 10px;
  }
</style>

<main id="main-content" tabindex="-1">
  <h1>Welcome to Fantastic4News</h1>
  <p>Your source for the latest news with accessibility in mind.</p>
</main>
```

---

#### 🌱 **Database Seeding (User Roles & Accounts)**  
One of our team members, **Fredrik**, who is visually impaired, developed the **user seeding** functionality for creating initial user accounts. The seed script assigns roles to users based on their predefined accounts.  

##### 📌 **User Seeding Code by Fredrik**  
```yaml
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Identity;

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
        }

        static async Task SeedUsers_FredrikAndersson(UserManager<User> userManager)
        {
            User[] fredriksUsers =
            {
                new User { FirstName = "Fredrik", LastName = "Andersson", Email = "fredrik_admin@new.se", UserName = "Fredrik_Admin", EmailConfirmed = true },
                new User { FirstName = "Fredrik2", LastName = "Andersson", Email = "fredrik_editor@new.se", UserName = "Fredrik_Editor", EmailConfirmed = true },
                new User { FirstName = "Fredrik3", LastName = "Andersson", Email = "fredrik_journalist@new.se", UserName = "Fredrik_Journalist", EmailConfirmed = true },
                new User { FirstName = "Fredrik4", LastName = "Andersson", Email = "fredrik_customer@new.se", UserName = "Fredrik_Customer", EmailConfirmed = true },
            };

            var password = "*Qwerty123";

            foreach (var usr in fredriksUsers)
            {
                if (userManager.FindByEmailAsync(usr.Email).Result == null)
                {
                    IdentityResult userResult = await userManager.CreateAsync(usr, password);

                    if (userResult.Succeeded)
                    {
                        if (usr.UserName.Contains("Admin"))
                            userManager.AddToRoleAsync(usr, "Admin").Wait();
                        else if (usr.UserName.Contains("Customer"))
                            userManager.AddToRoleAsync(usr, "Customer").Wait();
                        else if (usr.UserName.Contains("Editor"))
                            userManager.AddToRoleAsync(usr, "Editor").Wait();
                        else if (usr.UserName.Contains("Journalist"))
                            userManager.AddToRoleAsync(usr, "Journalist").Wait();
                    }
                }
            }
        }
    }
}
```

📌 **Key Features of This Seeding Process**:  
- **Automated User Creation**: Ensures predefined users are added to the database.  
- **Role Assignment**: Automatically assigns **Admin, Editor, Journalist, and Customer** roles.  
- **Security Best Practices**: Uses **hashed passwords** and confirms user emails.  

---

#### ⏳ **Project Duration**  
The development of **Fantastic4News** took approximately **two months** from start to finish.  

#### 🔗 **Project Links**  
- **Portfolio - Live**: [www.evgallos.com](https://www.evgallos.com)  
- **GitHub Profile**: [github.com/Evgallos](https://www.github.com/Evgallos)  
- **GitHub Repository**: [github.com/Evgallos/Fantastic4News](https://www.github.com/Evgallos/Fantastic4News)  

---

### ⚖️ **Copyright & License**  
This project is **protected under copyright laws**, and the **source code** belongs **exclusively** to the **four team members** who developed it. No one outside our team has the right to **use, modify, or distribute** this code without **explicit permission** from all four members.  

🚀 **Fantastic4News** is a showcase of teamwork, accessibility, and modern web development principles, proving that **limitations are just a challenge to overcome!**

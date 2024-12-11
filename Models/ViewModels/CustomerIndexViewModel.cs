using Fantastic4News.Models.Db;

namespace Fantastic4News.Models.ViewModels
{
    public class CustomerIndexViewModel
    {
       public ICollection<Article> DailyNews { get; set; }
       public ICollection<Article> PopularNews { get; set; }
       public  ICollection<Article> EditorsChoice { get; set; }

    }
}

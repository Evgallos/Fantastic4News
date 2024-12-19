using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fantastic4News.ViewModels
{
    public class ArticleIndexVM
    {
        public IEnumerable<Article> Articles { get; set; }

        public Article Article { get; set; }

        public User User { get; set; }

        public SelectList CategoriesSelectList { get; set; }

    }
}

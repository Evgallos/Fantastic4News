using Fantastic4News.Data;
using Fantastic4News.Models;

namespace Fantastic4News.Services
{
    public class ArticleService : IArticleService
    {
        // Injections

        private readonly ApplicationDbContext _db;

        public ArticleService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Methods

public IEnumerable<Article> GetArticles()
        {
            return _db.Articles;
        }

        public Article GetArticleById(int id)
        {
            return _db.Articles.Find(id);
        }


    }
}

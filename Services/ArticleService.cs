using Fantastic4News.Data;
using Fantastic4News.Models.Db;

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

        public void UpdateArticle(Article obj)
        {
            _db.Articles.Update(obj);
            _db.SaveChanges();
        }


    }
}

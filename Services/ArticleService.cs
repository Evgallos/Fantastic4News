using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.EntityFrameworkCore;

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
            return _db.Articles.Include(c => c.Category);
        }

        public IEnumerable<Article> GetArticlesWithJournalist()
        {
            return _db.Articles.Include(a=>a.User).ToList();
        }
        public Article GetArticleById(int id)
        {
            return _db.Articles.Include(c => c.Category).Include(u => u.User).FirstOrDefault(a => a.Id == id);
        }

        public void CreateArticle(Article obj)
        {
            obj.DateStamp = DateTime.Now;
            _db.Articles.Add(obj);
            _db.SaveChanges();
        }

        public void UpdateArticle(Article obj)
        {
            _db.Articles.Update(obj);
            _db.SaveChanges();
        }


    }
}

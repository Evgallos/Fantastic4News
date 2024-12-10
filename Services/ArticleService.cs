using Fantastic4News.Data;

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


    }
}

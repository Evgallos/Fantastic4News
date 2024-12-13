using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles();
        IEnumerable<Article> GetArticlesWithJournalist();

        Article GetArticleById(int id);

        void UpdateArticle(Article obj);
    }
}

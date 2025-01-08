using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles();
        IEnumerable<Article> GetArticlesWithJournalist();

        Article GetArticleById(int id);

        void CreateArticle(Article obj);

        void UpdateArticle(Article obj);

        List<Article> GetAllArticles();

        void DeleteArticle(int id);
    }
}

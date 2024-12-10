using Fantastic4News.Models;

namespace Fantastic4News.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles();

        Article GetArticleById(int id);
    }
}

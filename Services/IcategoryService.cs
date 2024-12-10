using Fantastic4News.Models;

namespace Fantastic4News.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);
    }
}

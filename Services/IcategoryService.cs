using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategoriesWithAritcles();



		Category GetCategoryById(int id);

        public void CreateCategories(Category cate);

    }
}

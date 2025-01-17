using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Services
{
    public interface ICategoryService
    {

        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategoriesWithAritcles();



		Category GetCategoryById(int id);

        public void CreateCategories(Category cate);

        public void UpdateCategories(Category category);

        public void RemoveCategories(Category category);
    }
}

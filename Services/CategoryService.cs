using Fantastic4News.Data;
using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public class CategoryService : ICategoryService
    {
        // Injections

        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Methods

public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return _db.Categories.Find(id);
        }
    }
}

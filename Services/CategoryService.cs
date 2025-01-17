using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<Category> GetCategoriesWithAritcles()
        {
            var categories = _db.Categories.Include(c => c.Articles).ToList();

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            return _db.Categories.Find(id);
        }

        public void CreateCategories(Category cate)
        {
            _db.Categories.Add(cate);
            _db.SaveChangesAsync();
        }
        public void  UpdateCategories(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
        public void RemoveCategories(Category category)
        {
            _db.Remove(category);
            _db.SaveChanges();
        }

    }
}

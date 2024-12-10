using Fantastic4News.Data;

namespace Fantastic4News.Services
{
    public class CategoryService : IcategoryService
    {
        // Injections

        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Methods


    }
}

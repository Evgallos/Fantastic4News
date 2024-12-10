using Fantastic4News.Data;

namespace Fantastic4News.Services
{
    public class CustomerService : ICustomerService
    {
        // Injections

        private readonly ApplicationDbContext _db;

        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Methods


    }
}

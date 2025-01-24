using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fantastic4News.ViewModels
{
    public class CustomerDetailVM
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }

        public string CustomerUserName { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerEmail { get; set; } = string.Empty;



    }
}

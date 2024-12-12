using Fantastic4News.Controllers;
using Fantastic4News.Models.Db;

namespace Fantastic4News.Models.ViewModels
{
	public class LinkTextNewsVm
	{
        public string CategoryName { get; set; }
		public IEnumerable<Article> Articles { get; set; }
    }
}

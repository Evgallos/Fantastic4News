using System.ComponentModel.DataAnnotations;

namespace Fantastic4News.Models.Db
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }=string.Empty;

        //Nav
        public IEnumerable<Article> Articles { get; set; }
    }
}

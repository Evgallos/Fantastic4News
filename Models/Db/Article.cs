using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Fantastic4News.Models.Db
{
    public class Article
    {
        public int Id { get; set; }

        public DateTime? DateStamp { get; set; }

        public string LinkText { get; set; }=string.Empty;

        [StringLength(50)]
        public string HeadLine { get; set; }= string.Empty;

        public string ContentSummary { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public int Views { get; set; }

        public int Like { get; set; }

        public string ImageLink { get; set; } = string.Empty;
      

        public bool IsArhived { get; set; } = false;

        //Nav

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

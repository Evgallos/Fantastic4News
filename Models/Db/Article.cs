using Fantastic4News.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public bool IsArhived { get; set; } = false;

        public bool EditorsChoice { get; set; } = false;

        public bool IsPublished { get; set; } = false;

        public ArticlePriority Priority { get; set; } = ArticlePriority.Low;

		//Nav
		public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }

      

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}

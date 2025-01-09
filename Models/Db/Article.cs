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


       public bool IsComplete { get; set; } = false;//complete button and to send the article to editor for approving
      

        [Display(Name ="Editors Choice")]
        public bool EditorsChoice { get; set; } = false;
        public bool IsPublished { get; set; } = false;//when article is approved this is set as true 

        public string? editorsComment { get; set; }// when its not approved ispublished remain false and adds editorscomment why its not approved
        public ArticlePriority Priority { get; set; } = ArticlePriority.Low;

		//Nav
		public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }

      

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}

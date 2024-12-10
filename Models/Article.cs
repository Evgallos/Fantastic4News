using System.Security.Principal;

namespace Fantastic4News.Models
{
    public class Article
    {
        public int Id { get; set; }

        public DateTime DateStamp { get; set; }

        public string LinkText { get; set; }

        public string HeadLine { get; set; }

        public string ContentSummary { get; set; }

        public string Content { get; set; }
        public int Views { get; set; }

        public int Like { get; set; }
        public int DisLike { get; set; }

        public string ImageLink { get; set; }
        public string Category { get; set; }

        public bool IsArhived { get; set; }

    }
}

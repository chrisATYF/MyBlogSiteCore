namespace MyBlogSiteCore.Models
{
    public class Posting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime PostingDate { get; set; }
        public string Content { get; set; }
    }
}

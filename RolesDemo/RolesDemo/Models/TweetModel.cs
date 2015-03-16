namespace RolesDemo.Models
{
    public class TweetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AuthorUserName { get; set; }
        public bool AuthorIsPremium { get; set; }
    }
}
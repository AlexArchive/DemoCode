using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Question
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; } 
    }
}
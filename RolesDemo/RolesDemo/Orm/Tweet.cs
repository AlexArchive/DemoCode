using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RolesDemo.Orm
{
    public class Tweet  
    {
        public int Id { get; set; } 
        public IdentityUser Author { get; set; }
        public string Text { get; set; }    
        public DateTime PublishDateTime { get; set; }
    }
}
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RolesDemo.Orm
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Tweet> Tweets { get; set; }    
        public ApplicationContext() : base(nameOrConnectionString: "RolesDemo")
        {
            
        }
    }
}
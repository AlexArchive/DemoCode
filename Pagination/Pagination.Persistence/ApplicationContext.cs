using System.Data.Entity;

namespace Pagination.Persistence
{
    public class ApplicationContext : DbContext
    {
        public IDbSet<Post> Posts { get; set; }
    }
}
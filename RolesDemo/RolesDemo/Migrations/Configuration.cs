using System.Data.Entity.Migrations;
using RolesDemo.Orm;

namespace RolesDemo.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Experimento.Orm.ApplicationContext";
        }
    }
}

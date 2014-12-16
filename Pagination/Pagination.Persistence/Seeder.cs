using System.Data.Entity;
using FizzWare.NBuilder;

namespace Pagination.Persistence
{
    public class Seeder : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var phraseGenerator = new RandomGenerator();
            var posts = Builder<Post>
                .CreateListOfSize(100)
                .All()
                .With(x => x.Content = phraseGenerator.Phrase(450))
                .Build();

            foreach (var post in posts)
            {
                context.Posts.Add(post);
            }

            context.SaveChanges();
        }
    }
}
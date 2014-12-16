using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using WebApplication.Controllers;
using WebApplication.Models;
using Xunit;
using Xunit.Extensions;

namespace WebApplication.Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void SutIsController()
        {
            var repoDummy = new QuestionRepoDouble();
            var sut = new HomeController(repoDummy);
            Assert.IsAssignableFrom<IController>(sut);
        }

        [Fact]
        public void IndexRendersDefaultView()
        {
            var repoDummy = new QuestionRepoDouble();
            var sut = new HomeController(repoDummy);
            sut.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [Fact]
        public void IndexReturnsCorrectModelType()
        {
            var repoDummy = new QuestionRepoDouble();
            var sut = new HomeController(repoDummy);
            sut.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Question>>();
        }

        [Fact]
        public void IndexReturnsModelWithCorrectRecordCount()
        {
            var repoStub = new QuestionRepoDouble();
            var sut = new HomeController(repoStub);
            sut.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Question>>(actual => actual.Count() == 1);
        }

        [Fact]
        public void IndexReturnsModelWithLatestRecordFirst()
        {
            var repoStub = new QuestionRepoDouble();
            repoStub.Questions.Add(new Question { Title = "Foo" });
            repoStub.Questions.Add(new Question { Title = "Bar" });
            var sut = new HomeController(repoStub);
            sut.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Question>>(actual => actual.First().Title == "Bar");
        }

        [Fact]
        public void AddQuestionRendersDefaultView()
        {
            var repoDummy = new QuestionRepoDouble();
            var sut = new HomeController(repoDummy);
            sut.WithCallTo(c => c.AddQuestion())
                .ShouldRenderDefaultView();
        }

        [Fact]
        public void AddQuestionRedirectsToIndexIfSuccessful()
        {
            var repoSpy = new QuestionRepoDouble();
            var questionDummy = new QuestionInput();
            var sut = new HomeController(repoSpy);
            sut.WithCallTo(c => c.AddQuestion(questionDummy))
                .ShouldRedirectTo(c => c.Index());
        }

        [Fact]
        public void AddQuestionInteractsWithQuestionRepo()
        {
            var repoDummy = new QuestionRepoDouble();
            var questionDummy = new QuestionInput();
            var sut = new HomeController(repoDummy);
            sut.WithCallTo(c => c.AddQuestion(questionDummy))
                .ShouldRedirectTo(c => c.Index());
            Assert.Equal(1, repoDummy.AddCallCount);
        }

        [Fact]
        public void AddInvalidQuestionReturnsError()
        {
            var repoDummy = new QuestionRepoDouble();
            var questionDummy = new QuestionInput();
            var sut = new HomeController(repoDummy);
            sut.ViewData.ModelState.AddModelError("Title", "");
            sut.ViewData.ModelState.AddModelError("Body", "");
            sut.ViewData.ModelState.AddModelError("Tags", "");
            sut.WithCallTo(c => c.AddQuestion(questionDummy))
                .ShouldRenderDefaultView()
                .WithModel<QuestionInput>()
                .AndModelErrorFor(m => m.Title)
                .AndModelErrorFor(m => m.Body)
                .AndModelErrorFor(m => m.Tags);
        }

        [Fact]
        public void SearchTagsReturnsCorrectModelType()
        {
            var repoDummy = new QuestionRepoDouble();
            var sut = new HomeController(repoDummy);
            var actual = sut.SearchTags("").JsonRequestBehavior;
            Assert.Equal(JsonRequestBehavior.AllowGet, actual);
        }

        [Theory]
        [InlineData("abc", 0)]
        [InlineData("w", 2)]
        [InlineData("W", 2)]
        public void SearchTagsReturnsCorrectResult(
            string searchTerm,
            int expectedTagCount)
        {
            var repoStub = new QuestionRepoDouble();
            repoStub.Questions.Add(
                new Question { Tags = new[] { "wibble", "wobble" } });
            repoStub.Questions.Add(
                new Question { Tags = new[] { "wibble", "fred" } });
            var sut = new HomeController(repoStub);
            var actual = ((IEnumerable<string>)sut.SearchTags(searchTerm).Data).Count();
            Assert.Equal(expectedTagCount, actual);
        }
    }
}

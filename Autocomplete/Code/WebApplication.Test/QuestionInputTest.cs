using System.Collections.Generic;
using WebApplication.Models;
using Xunit;
using Xunit.Extensions;

namespace WebApplication.Test
{
    public class QuestionInputTest
    {
        [Theory]
        [InlineData(null, new string[0])]
        [InlineData("", new string[0])]
        [InlineData("foo, ", new[] { "foo" })]
        [InlineData("foo", new[] { "foo" })]
        [InlineData("foo, bar", new[] { "foo", "bar" })]
        public void ToQuestionReturnsCorrectResult(
            string tags,
            IEnumerable<string> expected)
        {
            var sut = new QuestionInput();
            sut.Tags = tags;
            sut.Title = "foo";
            sut.Body = "bar";

            var actual = sut.ToQuestion();

            Assert.Equal(expected, actual.Tags);
            Assert.Equal(sut.Title, actual.Title);
            Assert.Equal(sut.Body, actual.Body);
        }
    }
}
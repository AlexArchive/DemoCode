using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Persistence
{
    public class QuestionRepository : IQuestionRepository
    {
        private static readonly List<Question> questions = new List<Question>();
 
        public IEnumerable<Question> All()
        {
            yield return new Question
            {
                Title = "Overwriting Modern Chart Style in WPF",
                Body = @"I am writing a WPF application which utilises the excellent ModernUI and ModernUI
Charts projects. I'm in a situation where I need to resize the RadialGaugeChart, which means altering the
default style (the size of the chart is hard-coded in the default style).",
                Tags = new[] { "WPF", "XAML" }
            };

            yield return new Question
            {
                Title = "How do i convert dbSet to ObjectQuery?",
                Body = "I am getting an error on the following line of code.",
                Tags = new[] { "C#", "Entity Framework" }
            };

            yield return new Question
            {
                Title = "How do I format the output of a nested JSON object with JSON.net?",
                Body = @"First, this has been asked a number of times but having read all the posts I 
found none provided an answer that fixed my particular scenario.",
                Tags = new[] { "C#", "Json", "Json.net" }
            };

            foreach (var question in questions)
                yield return question;
        }

        public void Add(Question question)
        {
            questions.Add(question);
        }
    }
}
using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Persistence;

namespace WebApplication.Test
{
    public class QuestionRepoDouble : IQuestionRepository
    {
        public int AddCallCount { get; set; }
        public List<Question> Questions { get; set; }

        public QuestionRepoDouble()
        {
            Questions = new List<Question>();
        }

        public IEnumerable<Question> All()
        {
            return Questions;
        }

        public void Add(Question question)
        {
            AddCallCount += 1;
        }
    }
}
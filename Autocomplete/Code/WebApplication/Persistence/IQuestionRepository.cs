using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Persistence
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> All();
        void Add(Question question);
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApplication.Models
{
    public class QuestionInput
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Tags { get; set; }

        public Question ToQuestion()
        {
            var question = new Question();
            question.Title = Title;
            question.Body = Body;

            // This is probably a bad condition because in *this* system, it is never valid to have no tags.
            if (Tags == null)
            {
                question.Tags = new string[0];
            }
            else
            {
                question.Tags = Tags
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Where(tag => !string.IsNullOrWhiteSpace(tag))
                    .Select(tag => tag.Trim());
            }

            return question;
        }
    }
}
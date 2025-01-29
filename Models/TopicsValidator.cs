using FluentValidation;

namespace QuizAPI.Models
{
    public class TopicsValidator:AbstractValidator<TopicsModel>
    {
        public TopicsValidator()
        {
           
            RuleFor(u => u.TopicName)
              .NotEmpty()
              .WithMessage("TopicName Compulsory");
        }
    }
}

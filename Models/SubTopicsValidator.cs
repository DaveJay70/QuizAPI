using FluentValidation;

namespace QuizAPI.Models
{
    public class SubTopicsValidator:AbstractValidator<SubTopicsModel>
    {
        public SubTopicsValidator()
        {
         
            RuleFor(u => u.TopicID)
              .NotEmpty()
              .WithMessage("TopicID Compulsory");
            RuleFor(u => u.SubtopicName)
              .NotEmpty()
              .WithMessage("SubtopicName Compulsory");
            

        }
    }
}

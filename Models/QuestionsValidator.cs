using FluentValidation;

namespace QuizAPI.Models
{
    public class QuestionsValidator : AbstractValidator<QuestionsModel>
    {
        public QuestionsValidator()
        {
            
            RuleFor(u => u.SubtopicID)
                .NotEmpty()
                .WithMessage("SubtopicID Compulsory");
            RuleFor(u => u.LevelID)
                .NotEmpty()
                .WithMessage("LevelID Compulsory");
            RuleFor(u => u.QuestionText)
                .NotEmpty()
                .WithMessage("QuestionText Compulsory");
            RuleFor(u => u.QuestionType)
                .NotEmpty()
                .WithMessage("QuestionType Compulsory");
            RuleFor(u => u.Mark)
                .NotEmpty()
                .WithMessage("Mark Compulsory");
           
        }
    }
}

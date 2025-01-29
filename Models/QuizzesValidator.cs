using FluentValidation;

namespace QuizAPI.Models
{
    public class QuizzesValidator:AbstractValidator<QuizzesModel>
    {
        public QuizzesValidator()
        {
           
            RuleFor(u => u.UserID)
                .NotEmpty()
                .WithMessage("UserID Compulsory");
            RuleFor(u => u.QuizName)
                .NotEmpty()
                .WithMessage("QuizName Compulsory");
            RuleFor(u => u.LevelID)
                .NotEmpty()
                .WithMessage("LevelID Compulsory");
            RuleFor(u => u.SubtopicID)
                .NotEmpty()
                .WithMessage("SubtopicID Compulsory");
            RuleFor(u => u.Time)
                .NotEmpty()
                .WithMessage("Time Compulsory");
        }
    }
}

using FluentValidation;
namespace QuizAPI.Models
{
    public class LevelsValidator:AbstractValidator<LevelsModel>
    {
        public LevelsValidator()
        {
            
            RuleFor(u => u.LevelName)
                .NotEmpty()
                .WithMessage("LevelName Compulsory ");
        }
    }
}

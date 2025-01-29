using FluentValidation;

namespace QuizAPI.Models
{
    public class QuizQuestionsValidator:AbstractValidator<QuizQuestionsModel>
    {
        public QuizQuestionsValidator() 
        {
           
            RuleFor(u => u.QuizID)
               .NotEmpty()
               .WithMessage("QuizID Compulsory");
            RuleFor(u => u.QuestionID)
               .NotEmpty()
               .WithMessage("QuestionID Compulsory");
        }
    }
}

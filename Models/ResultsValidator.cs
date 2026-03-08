using FluentValidation;

namespace QuizAPI.Models
{
    public class ResultsValidator:AbstractValidator<ResultModel>
    {
        public ResultsValidator()
        {
            
            RuleFor(u => u.Score)
               .NotEmpty()
               .WithMessage("Score Compulsory");
         
           
        }
    }
}

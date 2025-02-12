using FluentValidation;

namespace QuizAPI.Models
{
    public class UsersValidator:AbstractValidator<UsersModel>
    {
        public UsersValidator()
        {
           
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username Compulsory");
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email Compulsory");
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password Compulsory");
            
            
        }
    }
}

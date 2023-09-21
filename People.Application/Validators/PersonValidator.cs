using FluentValidation;
using People.Application.Models;

namespace People.Application.Validators
{
    public class PersonValidator:AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("The firstname can not be empty");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage("The lastname can not be empty"); ;
        }
    }
}

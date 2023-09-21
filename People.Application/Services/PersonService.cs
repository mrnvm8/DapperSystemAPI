using FluentValidation;
using People.Application.Models;
using People.Application.Repositories.PeopleRepository;

namespace People.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IValidator<Person> _personValidator;

        public PersonService(IPeopleRepository peopleRepository, 
            IValidator<Person> personValidator)
        {
            _peopleRepository = peopleRepository;
            _personValidator = personValidator;
        }

        public async Task<bool> CreateAsync(Person person, CancellationToken token = default)
        {
            await _personValidator.ValidateAndThrowAsync(person, cancellationToken:token);
           return await _peopleRepository.CreateAsync(person, token);
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
           return _peopleRepository.DeleteByIdAsync(id, token);
        }

        public Task<IEnumerable<Person>> GetAllAsync(CancellationToken token = default)
        {
            return _peopleRepository.GetAllAsync(token);
        }

        public Task<Person?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _peopleRepository.GetByIdAsync(id, token);
        }

        public async Task<Person?> UpdateAsync(Person person, CancellationToken token = default)
        {
            //business logic here
            await _personValidator.ValidateAndThrowAsync(person, cancellationToken: token);
            var personExist = await _peopleRepository.ExistByIdAsync(person.Id, token);
            if (!personExist)
            {
                return null;
            }
             await _peopleRepository.UpdateAsync(person, token);

            return person;
        }
    }
}

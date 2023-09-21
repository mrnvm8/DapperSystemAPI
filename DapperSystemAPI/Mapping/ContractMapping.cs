using People.Application.Models;
using People.Contracts.Requests;
using People.Contracts.Responses;

namespace DapperSystemAPI.Mapping
{
    public static class ContractMapping
    {
        public static Person MapToPerson(this CreatePersonRequest request)
        {
            return new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = (Gender)request.Gender
            };
        }

        public static PersonResponse MapToResponse(this Person person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = (int)person.Gender
            };
        }

        public static PeopleResponse MapToResponse(this IEnumerable<Person> people)
        {
            return new PeopleResponse
            {
                _People = people.Select(MapToResponse)
            };
        }

        public static Person MapToPerson(this UpadatePersonRequest request, Guid id)
        {
            return new Person()
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = (Gender)request.Gender
            };
        }
    }
}

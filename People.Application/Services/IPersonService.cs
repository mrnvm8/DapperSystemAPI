using People.Application.Models;

namespace People.Application.Services
{
    public interface IPersonService
    {
        //This will be using DTO instead of domain person object
        //and will be doing mapping of the movies
        //default token it's not nice to pass null, unless you need it
        Task<bool> CreateAsync(Person person, CancellationToken token = default);
        Task<Person?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Person>> GetAllAsync(CancellationToken token = default);
        Task<Person?> UpdateAsync(Person person, CancellationToken token = default);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
       
    }
}

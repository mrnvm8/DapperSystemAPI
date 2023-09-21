using People.Application.Models;

namespace People.Application.Repositories.PeopleRepository
{
    public interface IPeopleRepository
    {
        Task<bool> CreateAsync(Person person, CancellationToken token = default);
        Task<Person?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Person>> GetAllAsync(CancellationToken token = default);
        Task<bool> UpdateAsync(Person person, CancellationToken token = default);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
        Task<bool> ExistByIdAsync(Guid id, CancellationToken token = default);
    }
}

using Dapper;
using People.Application.Database;
using People.Application.Models;

namespace People.Application.Repositories.PeopleRepository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PeopleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<bool> CreateAsync(Person person, CancellationToken token = default)
        {
            //get the connection first
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();

            var sql = "insert into person (id, firstname, lastname, gender) " +
                      "values(@Id, @FirstName, @LastName, @Gender)";

            var result = await connection.ExecuteAsync(new CommandDefinition(sql, person, cancellationToken:token));
            transaction.Commit();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            //get the connection first
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();
            var sql = "delete from person where id=@id";
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, new {id}, cancellationToken: token));
            transaction.Commit();
            return result > 0;
        }


        public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken token = default)
        {
            //get the connection first
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            var sql = "select * from person";

            var result = await connection.QueryAsync(new CommandDefinition(sql, cancellationToken: token));

            return result.Select(x => new Person
            {
                Id = x.id,
                FirstName = x.firstname,
                LastName = x.lastname,
                Gender = (Gender)x.gender,
            });
        }

        public async Task<Person?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            //get the connection first
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

            var sql = "select * from person where id = @id";

            var person = await connection.QuerySingleOrDefaultAsync<Person>(
                new CommandDefinition(sql, new {id}, cancellationToken: token));
            
            if (person is null)
            {
                return null;
            }
            return person;

        }

        public async Task<bool> UpdateAsync(Person person, CancellationToken token = default)
        {
            //get the connection first
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            var transaction = connection.BeginTransaction();
            var sql = "update person set firstname=@FirstName, lastname=@LastName, gender=@Gender " +
                      "where id=@Id";
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, person, cancellationToken: token));
            transaction.Commit();

            return result > 0;
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken token = default)
        {
            //get the connection first
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            var sql = "select count(1) from person where id = @id";
            return await connection.ExecuteScalarAsync<bool>(
                new CommandDefinition(sql, new {id}, cancellationToken: token));
        }
    }
}

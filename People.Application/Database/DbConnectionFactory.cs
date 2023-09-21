using Npgsql;
using System.Data;

namespace People.Application.Database
{
    public interface IDbConnectionFactory
    {
        //This method need only 1 method for creating connection to handle request
        Task<IDbConnection> CreateConnectionAsync (CancellationToken token = default);

    }

    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
       
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            //* will be creating the connection
            var connection = new NpgsqlConnection(_connectionString);
            //* after creation, will be opening that connection to be ready for usage
            await connection.OpenAsync(token);
            //* then return connection 
            return connection;
        }
    }
}

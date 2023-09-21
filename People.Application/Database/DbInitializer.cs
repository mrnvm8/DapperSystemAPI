using Dapper;

namespace People.Application.Database
{
    //The purpose of this class is for creating/initialize database,
    //since wil be no migrations
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            //create a connection
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            //will use Dapper to run/excute some sql
            //to generate some schema for the database 

            //* create the person Table
            await connection.ExecuteAsync("""
                create table if not exists person(
                id UUID primary key,
                firstname TEXT not null,
                lastname TEXT not null,
                gender integer not null);
            """);
        }
    }
}

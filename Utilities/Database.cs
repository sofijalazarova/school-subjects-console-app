using Npgsql;

public class Database
{
    
    static Database()
    {
        DotNetEnv.Env.Load();
    }
    public static NpgsqlConnection GetConnection()
    {
        string ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            string host = Environment.GetEnvironmentVariable("DB_HOST");
            string port = Environment.GetEnvironmentVariable("DB_PORT");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string dbName = Environment.GetEnvironmentVariable("DB_NAME");

            ConnectionString = $"Host={host};Port={port};Username={user};Password={password};Database={dbName}";
        }

        return new NpgsqlConnection(ConnectionString);
    }

}
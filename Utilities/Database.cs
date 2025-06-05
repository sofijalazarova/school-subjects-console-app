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
        return new NpgsqlConnection(ConnectionString);
    }

}
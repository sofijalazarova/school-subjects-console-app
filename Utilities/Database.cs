using Npgsql;

public class Database
{
    private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=sofija18;Database=schoolsystemDb";

    public static NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }

}
using Npgsql;

namespace Infrastructure.Data;

public class DataContext
{
    const string connectionString = "Service=localhost;Database=Exzam4;Username=postgres;Password=123456";
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}

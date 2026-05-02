using Dapper;
using Infrastructure.Data;
namespace Infrastructure.Services;

public class Customer
{
    private readonly DataContext context = new();
    public async Task AddCustomer(Customer customer)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "insert into customers(fullname) values(@FullName)";
        await connection.ExecuteAsync(sql, customer);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "update customers set fullname = @FullName where id = @Id";
        await connection.ExecuteAsync(sql, customer);
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "select id, fullname as FullName from customers order by id";
        return (await connection.QueryAsync<Customer>(sql)).ToList();
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "select id, fullname as FullName from customers where id=@id";
        return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { id });
    }

    public async Task DeleteCustomer(int id)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "delete from customers where id=@id";
        await connection.ExecuteAsync(sql, new { id });
    }
}

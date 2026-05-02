using Dapper;
using Infrastructure.Data;
namespace Infrastructure.Services;

public class Order
{
    private readonly DataContext context = new();

    public async Task AddOrder(Order order)
    {
        using var connection = context.GetConnection();
        connection.Open();

        var sql = "insert into orders(customer_id, amount) values(@CustomerId, @Amount)";
        connection.Execute(sql, order);
    }

    public async Task UpdateOrder(Order order)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "update orders set customer_id=@CustomerId, amount=@Amount where id=@Id";
        await connection.ExecuteAsync(sql, order);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = @"select id,
        customer_id as CustomerId,amount,
        created_at as CreatedAt
        from orders
        order by id";

        return (await connection.QueryAsync<Order>(sql)).ToList();
    }

    public async Task<Order?> GetOrderById(int id)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = @"select id,
        customer_id as CustomerId,amount,
        created_at as CreatedAt
        from orders
        where id=@id";
        return await connection.QuerySingleOrDefaultAsync<Order>(sql, new { id });
    }

    public async Task DeleteOrder(int id)
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "delete from orders where id=@id";
        await connection.ExecuteAsync(sql, new { id });
    }
}

using Dapper;
using Infrastructure.Data;
namespace Infrastructure.Services;
public class AnaliticServices
{
    private readonly DataContext context = new();
    public async Task<List<Join>> GetAll()
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = @"select o.Id,c.Fullname,o.amount,o.created_at
            from orders o
            join customers c on o.customer_id=c.id";
        var result = await connection.QueryAsync<Join>(sql);
        return result.ToList();
    }

    public async Task<decimal> SumDecimal()
    {
        using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = "select sum(amount) from orders";
        var f = (decimal)await connection.ExecuteScalarAsync(sql);

        return f;
    } 
    public async Task<List<Customer>> CountOrder()
    {
       using var connection = context.GetConnection();
        await connection.OpenAsync();

        var sql = @"select c.fullname, count(o.id) 
        from orders o
        join customers c on o.customer_id = c.id
        group by coun(o.id)";
        var result = await connection.QueryAsync<Customer>(sql);
        return result.ToList();
    }
}

internal class Join
{
}
namespace Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int Customer_Id { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Created_At { get; set; }
}

// See https://aka.ms/new-console-template for more information
public class Order
{
    public List<Payment>? Payments { get; set; }
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Double Amount { get; set; }
    public bool Status { set; get; }


    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int? EployeeId { get; set; }
    public Employee? Employee { get; set; }

    public List<Product> Products { get; set; }
}

// See https://aka.ms/new-console-template for more information
public class Product
{
    public int Id { set; get; }
    public string Description { get; set; }
    public double Prince { get; set; }

    public List<Order> Orders { get; set; }
}
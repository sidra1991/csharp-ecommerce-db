// See https://aka.ms/new-console-template for more information
public class Employee
{
    public int Id { set; get; }
    public string Name { get; set; }
    public int Level { set; get; }

    public List<Order> Orders { get; set; }
}

// See https://aka.ms/new-console-template for more information
public class Payment
{
    public int Id { set; get; }
    public DateTime? date { get; set; }
    public double Amount { set; get; }
    public bool Status { get; set; }



    public int OrderID { get; set; }
    public Order Orders { get; set; }
}

// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;

Console.WriteLine("Hello, World!");

Ecommerce Db = new Ecommerce();


//quando l’applicazione si avvia chiede se l’utente è un dipendete o un cliente
//se è un dipendente potrà eseguire CRUD sugli ordini
//se è un cliente potrà acquistare degli ordini
int Select()
{
    int select = 0;
    try
    {
        select = Convert.ToInt32(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.WriteLine("formato sbagliato inserire un numero");

    }
	return select;
}

void Menu()
{
	Console.WriteLine("login");
    Console.WriteLine("1. cliente");
    Console.WriteLine("2. dipendente ");



	switch (Select())
	{
		case 0:
			Menu();
			break;
		case 1:
			MenuClient();
			break;

	}
}




void MenuClient(){

	Console.WriteLine("menu cliente");
    Console.WriteLine("1 compra");

    switch (Select())
	{
		case 1:
			buy();
			break;
		default:
			MenuClient();
			break;
	}


}

void createProductsFirstLogin()
{
	for (int i = 0; i < 10; i++)
	{
		
		Product product = new Product();
		product.Description = "testo descrittio " + i;
		product.Prince = i;
		Db.Products.Add(product);

		Db.SaveChanges();
	}
}

if(Db.Products.Count() == 0)
{
	createProductsFirstLogin();
}

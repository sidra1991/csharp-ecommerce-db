// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
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
bool yesOrNot()
{
	bool yesOrNot = false;
	string select =Console.ReadLine();
	if (select == "yes" || select == "si" || select == "s" || select == "y" )
	{
		yesOrNot = true;
	}

	return yesOrNot;
}
Customer LoginCustomer(){

		Console.WriteLine("indica il tuo id oppure il tuo nome");

		string login = Console.ReadLine();
		Customer client = null;
		int? loginInt = null;
		try
		{
			loginInt = Convert.ToInt32(login);
		}
		catch (Exception)
		{
			loginInt = null;
		}

		foreach (Customer item in Db.Customers)
		{
			if(item.Name == login || item.Id == loginInt)
			{
				client = item;
			}
		}
		if( client != null)
		{
			return client;
		}
		else
		{
			Console.WriteLine("questo utente non esiste");
			return LoginCustomer();
		}
	}
Customer createCustomer()
{
	Customer customer = new Customer();
	Console.WriteLine("inserire parametri");
	Console.WriteLine("nome");
	customer.Name = Console.ReadLine();
    Console.WriteLine("cognome");
    customer.Surname = Console.ReadLine();
    Console.WriteLine("email");
    customer.Email = Console.ReadLine();

    Db.Customers.Add(customer);

    Db.SaveChanges();

    return customer;

}

void Menu()
{
	Console.WriteLine("login");
    Console.WriteLine("1. cliente");
    Console.WriteLine("2. nuovo cliente");
    Console.WriteLine("3. dipendente ");

	
	int select = Select();

	switch (select)
	{
		case 0:
			Menu();
			break;
		case 1:
			{
				Customer customer = LoginCustomer();
				MenuClient(customer);
			}
			break;
		case 2:
			{

				Customer customer =	createCustomer();
				MenuClient(customer);
			}
			break;

    }
}

void buyProduct()
{
	Console.WriteLine("vuoi vedere la lista dei prodotti disponibili?");

	if (yesOrNot())
	{
		foreach (Product item in Db.Products)
		{
			Console.WriteLine(item.Id + " " + item.Prince + " " + item.Description);
		}
	}


}


void MenuClient(Customer customer){

	Console.WriteLine("menu cliente");
    Console.WriteLine("1 compra");

    switch (Select())
	{
		case 1:
            buyProduct();
			break;
		default:
			MenuClient(customer);
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

Menu();

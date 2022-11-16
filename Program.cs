// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.Design;

Console.WriteLine("Hello, World!");

Ecommerce Db = new Ecommerce();

//permette l'inserimento di un numero
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

//serve a prendere il si del utente
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

//login cliente
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
				break;
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

//creazione nuovo cliente
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

//creazione nuovo dipendente
Employee createEmployee()
{
    Employee employee = new Employee();
    Console.WriteLine("inserire parametri");
    Console.WriteLine("nome");
    employee.Name = Console.ReadLine();
    Console.WriteLine("livello");
	employee.Level = Select();
    Console.WriteLine("email");


    Db.Employees.Add(employee);

    Db.SaveChanges();

    return employee;

}

//calcola randomicamente la riuscita di qualcosa
bool randPay()
{
	Random random = new Random();
	if(random.Next(1, 100) % 2 == 1)
	{
		return true;
	}
	else
	{
		return false;
	}

}

//crea un ordine
void buyProduct(Customer customer)
{
	Console.WriteLine("ciao " + customer.Name + " con id numero " + customer.Id);
	Order order = new Order();
	List<Product> products = new List<Product>();
    foreach (Product item in Db.Products)
	{
		products.Add(item);
	}
	Console.WriteLine();
    Console.WriteLine("quanti prodotti vuoi comprare?");
	int manyBuy = Select();
	for (int i = 0; i < manyBuy; i++)
	{
		Console.WriteLine("vuoi vedere la lista dei prodotti?");
        if (yesOrNot())
        {
            foreach (Product item in products)
            {
                Console.WriteLine(item.Id + " " + item.Prince + " " + item.Description);
            }
        }
		Console.WriteLine("inserire l'iD del prodotto");
		 int select = Select();
		order.Products =new List<Product>();
		order.Products.Add(products[select]);

    }
	double ammount = 0;

	for (int i = 0; i < order.Products.Count; i++)
	{
		ammount += order.Products[i].Prince;
	}
    order.Date = DateTime.Now;
	order.CustomerId = customer.Id;
	order.Amount = ammount;
    Db.Orders.Add(order);
	Payment paymnt = new Payment();
	Console.WriteLine("pagare subito?");
	if (yesOrNot())
	{
		Console.WriteLine("inserire dati carta di credito");
		Console.ReadLine();
		if (randPay())
		{
			Console.WriteLine("pagamento eseguito con successo");
			paymnt.Status = true;
		}
		else
		{
			Console.WriteLine("pagamento fallito");
			paymnt.Status = false;
		}
        
    }
	else
	{
        paymnt.Status = false;
    }
	paymnt.date=DateTime.Now;
	paymnt.Amount=ammount;
	paymnt.OrderID = order.Id;
	paymnt.Orders = order;
	order.Payments = new List<Payment>();
	order.Payments.Add(paymnt);
    Db.Payments.Add(paymnt);
	customer.Orders.Add(order);
	try
	{
		Db.SaveChanges();
	}
	catch (Exception ex)
	{

		Console.WriteLine(ex.Message);
	}

	MenuClient(customer);
}

//permette di pagare gli ordini non ancora pagati
void pay(Customer customer)
{
	List<Order> ordersForPay = new List<Order>();
	string stringList = "";
	foreach (Order item in customer.Orders)
	{
		foreach (Order item2 in customer.Orders)
		{
			if (!item.Payments[item.Payments.Count - 1].Status)
			{
				ordersForPay.Add(item);
				stringList += "l'ordine con id " + item.Id + " fatto in data " + item.Date + " è ancora da pagare il suo prezzo è " + item.Amount + "\r\n";
				break;
            }
		}

	}

	if (ordersForPay.Count > 0)
	{

		Console.WriteLine("ti serve la lista degli ordini da pagare?");
		if (yesOrNot())
		{
			Console.WriteLine(stringList);
		}

		Console.WriteLine(" indica l'id del pagamento che devi effettuare ");


		int id = Select();
		List<Order> order = Db.Orders.ToList<Order>();
		Order orderSelect = null;

		foreach (Order item in order) 
		{ 
			if (item.Id == id) { orderSelect = item; } 
		}

		bool verificy = false;
        foreach (var item in ordersForPay)
        {
            Payment pp = item.Payments[item.Payments.Count - 1];
            if (item.Id == id && pp.Status) { verificy = true; }
        }

        if (verificy)
		{
			Payment payment = new Payment();
			payment.date = DateTime.Now;
			payment.Amount = orderSelect.Amount;
			payment.Status = true;
			payment.OrderID = id;
			orderSelect.Payments.Add(payment);
			Db.Payments.Add(payment);
			Console.WriteLine("inserire dati carta di credito");
			Console.ReadLine();
			Db.SaveChanges();
		}
		else
		{
			Console.WriteLine("questo ordine è gia pagato o non esistente");
		}
	}
	else
	{
		Console.WriteLine("non hai nulla da pagare");
	}
}

//login del lavoratore
Employee LoginEmployee()
{

    Console.WriteLine("indica il tuo id oppure il tuo nome");

    string login = Console.ReadLine();
    Employee client = null;
    int? loginInt = null;
    try
    {
        loginInt = Convert.ToInt32(login);
    }
    catch (Exception)
    {
        loginInt = null;
    }

    foreach (Employee item in Db.Employees)
    {
        if (item.Name == login || item.Id == loginInt)
        {
            client = item;
            break;
        }
    }
    if (client != null)
    {
        return client;
    }
    else
    {
        Console.WriteLine("questo utente non esiste");
        return LoginEmployee();
    }
}

//riemepe le liste in order e in payment del cliente
void loadDB(Customer customer)
{
	customer.Orders = new List<Order>();

    foreach (Order item in Db.Orders)
	{
		if (item.CustomerId == customer.Id)
		{
			customer.Orders.Add(item);
		}
	}


	if (customer.Orders.Count > 0)
	{
		foreach (Order item in customer.Orders)
		{
			item.Payments = new List<Payment>();

			foreach (Payment item2 in Db.Payments)
			{
				if (item2.OrderID == item.Id)
				{
					item.Payments.Add(item2);
				}
			}
		}
	}
}

//permette di vedere la lista degli ordini
void listOrder(Customer customer )
{
    if (customer.Orders.Count > 0)
	{
        foreach (Order item in customer.Orders)
		{
            Payment payment = item.Payments[item.Payments.Count - 1];

			string text = " non pagato";
			if (payment.Status)
			{
				text = " pagato il " + payment.date;
			}

			Console.WriteLine("id del ordine " + item.Id + " ordine effettuato il " + item.Date + " " + item.Amount + " " + text);
		}
	}
	else
	{
		Console.WriteLine("non hai ornidi registrati");
	}
}

// menu del dipendente per cancellare gli ordini 
void MenuDeleteOrder(Employee employee)
{
	List<Order> orders = Db.Orders.ToList<Order>();
	Console.WriteLine("1 elimina tutti gli ordini");
    Console.WriteLine("2 elimina un ordine specifico");
    int select = Select();

	switch (select)
	{
		case 1:
			foreach(Order item in Db.Orders)
			{
				Db.Orders.Remove(item);
			}
		break;
			case 2:
			foreach(Order item in Db.Orders)
			{
				Console.WriteLine(" ordine del " + item.Date + " id numero " + item.Id);
			}
			Console.WriteLine();
            Console.WriteLine("quanti ne vuoi cancellare? ");
			int number = Select();
			for (int i = 0; i < number; i++)
			{
				Console.WriteLine("inserire id del ordine da cancellare");
				int id = Select();
				foreach (Order item in Db.Orders)
				{
					if(item.Id == id)
					{
						Db.Orders.Remove(item);
					}
				}
			}
			break;
		default:
            MenuEmployee(employee);
			break;


    }
}

//permette di modificare un ordine
void Modify(Employee employee)
{
	foreach (Order item in Db.Orders)
	{
		Console.WriteLine(" orndine id " + item.Id + " data del ordine " + item.Date);
	}
    Console.WriteLine("seleziona id ordine da modificare");
	int id = Select();
	Order order = null;
	foreach(Order item in Db.Orders)
	{
		if(id == item.Id)
		{
			order = item;
		}
	}

	if(order != null)
	{
        //Console.WriteLine("modificare " + order.Date + " ?");
        //if (yesOrNot())
        //{
        //	Console.WriteLine("inserire nuovo parametro");

        //	//Console.WriteLine("data hhhh/mm/dd");
        //	//string data = Console.ReadLine();

        //	//order.Date = new DateTime(Convert. data);
        //}
        Console.WriteLine("modificare " + order.Amount + " ?");
        if (yesOrNot())
        {
            Console.WriteLine("inserire nuovo parametro");
			order.Amount = Select();
        }
        Console.WriteLine("modificare " + order.Status + " ?");
        if (yesOrNot())
        {
            Console.WriteLine("parametro convertito");
			order.Status = !order.Status;
        }

		order.EployeeId = employee.Id;
        order.Employee = employee;

		Db.SaveChanges();
    }

}

//menu principale
void Menu()
{
	Console.WriteLine("login");
    Console.WriteLine("1. cliente");
    Console.WriteLine("2. nuovo cliente");
	Console.WriteLine("3. dipendente ");
    Console.WriteLine("4. nuovo dipendente ");


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
		case 3:
			{
				Employee employee = LoginEmployee();
				MenuEmployee(employee);
            }
			break;
        case 4:
            {
                Employee employee = createEmployee();
                MenuEmployee(employee);
            }
            break;
		default:
			Menu();
			break;
    }
}

//menu principale dipendente
void MenuEmployee(Employee employee)
{
	Console.WriteLine("menu staf");
    Console.WriteLine("1. elimina ordini dal DB");
	Console.WriteLine("2. modifica ordine");
	Console.WriteLine("3. prepara ordine");
    Console.WriteLine("4. spedisci ordine");

    int select = Select();

	switch (select)
	{
		case 1:
			MenuDeleteOrder(employee);
			MenuEmployee(employee);

            break;
		case 2:
			Modify(employee);
            MenuEmployee(employee);
            break;
		case 3:
			Console.WriteLine("ordini non ancora preparati");
			List<Order> orders = new List<Order>();
			foreach (Order item in Db.Orders)
			{
				if (!item.Status)
				{
					orders.Add(item);
				}
			}
			Console.WriteLine("inserire id del ordine preparato");
			int id = Select();
			foreach (Order item in orders)
			{
				if (item.Id == id) 
				{ 
					item.Status = !item.Status; 
					item.EployeeId = employee.Id;
					item.Employee = employee;
				}
            }

			Db.SaveChanges();
            MenuEmployee(employee);
            break;
        case 4:
            Console.WriteLine("ordini non ancora consegnati");
            List<Order> orders2 = new List<Order>();
            foreach (Order item in Db.Orders)
            {
                if (item.Status)
                {
                    orders2.Add(item);
                }
            }
            Console.WriteLine("inserire id del ordine consegnato");
            int id2 = Select();
				//non so fisicamente cosa deve fare ora
            MenuEmployee(employee);
            break;
        default:
			MenuEmployee(employee);
			break;

    }
}

//menu principale cliente
void MenuClient(Customer customer){
    if (customer.Orders == null)
    {
        loadDB(customer);
    }

    Console.WriteLine("menu cliente");
    Console.WriteLine("1 compra");
    Console.WriteLine("2 guarda i tuoi ordini");
	Console.WriteLine("3 paga conti non pagati");

    switch (Select())
	{
		case 1:
            buyProduct(customer);
			MenuClient(customer);
			break;
        case 2:
			listOrder(customer);
            MenuClient(customer);
            break;
        case 3:
            pay(customer);
            MenuClient(customer);
            break;
        default:
			MenuClient(customer);
			break;
	}


}

//crea 10 prodotti alla prima attivazione del'app console
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

//controlla se sia la prima attivazione
if(Db.Products.Count() == 0)
{
	createProductsFirstLogin();
}

Menu();

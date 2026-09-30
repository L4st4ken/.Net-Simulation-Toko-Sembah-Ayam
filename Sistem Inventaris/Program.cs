using Sistem_Inventaris.Models;
using System.ComponentModel.Design;

//Default Toko
List<Produk> products = new List<Produk>()
{
    new Produk{id = 1, name = "Ayam Goreng", price = 20000, stock = 25},
    new Produk{id = 2, name = "Ayam Bakar", price = 25000, stock = 30}
};

List<Transaksi> transactions = new List<Transaksi>()
{
    new Transaksi{id = 1, date = DateTime.Now, total = 1000000}
};
transactions[0].detailTransaction.Add(new DetailTransaksi { id = 1, transactionId = 1, product = products[0], qty = 20, subtotal = products[0].price*20 });
transactions[0].detailTransaction.Add(new DetailTransaksi { id = 2, transactionId = 1, product = products[1], qty = 24, subtotal = products[1].price*24 });

Console.WriteLine($"Welcome to Toko Sembah Ayam - {DateTime.Now}");

while (true)
{
    Console.WriteLine($"{Environment.NewLine}Who Are You?");
    Console.WriteLine($"1. I am an Admin");
    Console.WriteLine($"2. I am a Customer");
    Console.WriteLine($"3. Exit");
    Console.Write($"Choose option(1-3)> ");

    string input = Console.ReadLine();

    int.TryParse(input, out int option);

    switch (option)
    {
        case 1:
            Console.WriteLine("\n[Success] Welcome Admin! You're In!");
            bool isAdminLoggedIn = true;
            while (isAdminLoggedIn)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View Products");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. View Transactions");
                Console.Write("Choose option(1-4)> ");

                string inputAdmin = Console.ReadLine();
                int.TryParse(inputAdmin, out int adminOption);
                switch (adminOption)
                {
                    case 1:
                        Console.Write("Input Product Id: ");
                        int idInput = int.Parse(Console.ReadLine());
                        Console.Write("Input Product Name: ");
                        string nameInput = Console.ReadLine();
                        Console.Write("Input Product Price: ");
                        int priceInput = int.Parse(Console.ReadLine());
                        Console.Write("Input Product Stock: ");
                        int stockInput = int.Parse(Console.ReadLine());

                        products.Add(new Produk { id=idInput, name=nameInput, price=priceInput, stock=stockInput });
                        break;
                    case 2:
                        Console.WriteLine("\nList of Products:");
                        foreach(var product in products)
                        {
                            Console.WriteLine($"Id: {product.id}, Name: {product.name}, Price: {product.price}, Stock: {product.stock}");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[Success] Admin Logout!");
                        isAdminLoggedIn = false;
                        Console.ResetColor();
                        break;
                    case 4:
                        Console.WriteLine("\nList of Transactions:");
                        foreach(var transaction in transactions)
                        {
                            Console.WriteLine($"Transaction Id: {transaction.id}, Date: {transaction.date}, Total: {transaction.total}");
                            foreach (var detail in transaction.detailTransaction)
                            {
                                Console.WriteLine($"  Detail Id: {detail.id}, Product: {detail.product.name}, Qty: {detail.qty}, Subtotal: {detail.subtotal}");
                            }
                        }
                        Console.WriteLine("\n");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[Error] Invalid input! Please choose numbers 1, 2, or 3.");
                        Console.ResetColor();
                        break;
                }
            }
            break;
        case 2:
            Console.WriteLine("\n[Success] Welcome Customer! Happy Shopping!");
            bool isCustomerLoggedIn = true;
            while (isCustomerLoggedIn)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Give me the Menu");
                Console.WriteLine("2. I want to buy product");
                Console.WriteLine("3. Logout");
                Console.Write("Choose option(1-3)> ");

                string inputCustomer = Console.ReadLine();
                int.TryParse(inputCustomer, out int customerOption);
                switch (customerOption)
                {
                    case 1:
                        Console.WriteLine("\nMenu:");
                        foreach(var product in products)
                        {
                            Console.WriteLine($"{product.id}. {product.name} - Rp.{product.price}");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 2:
                        //Ask what product the customer wants to buy
                        Console.WriteLine("\nFrom the menu what do you want to buy?");
                        string inputProduct = Console.ReadLine();
                        //check if the product is available in the menu
                        var productToBuy = products.Find(p => p.name.ToLower() == inputProduct.ToLower());
                        if(productToBuy != null)
                        {
                            //Check Stock
                            Console.WriteLine("How many do you want to buy?");
                            string productQtyInput = Console.ReadLine();
                            int.TryParse(productQtyInput, out int productQty);
                            if(productQty > 0 && productQty <= productToBuy.stock)
                            {
                                Console.WriteLine("Yes, we have that!");
                                Console.WriteLine($"The Total Price is: Rp.{productToBuy.price*productQty}");
                                //Ask if the customer wants to buy it
                                Console.WriteLine("Do you want to buy it? (yes/no)");
                                string buyConfirmation = Console.ReadLine();
                                if(buyConfirmation.ToLower() == "yes")
                                {
                                    Console.WriteLine("Nice! We will process your order.\n");

                                    //Update Stock
                                    int newStock = productToBuy.stock - productQty;
                                    products[productToBuy.id - 1].stock = newStock;
                                    productToBuy.stock = newStock;

                                    //Check if today there is no transaction
                                    //yet, if yes create new transaction, if not add to existing transaction
                                    int newTransactionId = transactions.Count + 1;
                          
                                    if(transactions.Exists(t => t.date.Date == DateTime.Now.Date))
                                    {
                                        var existingTransactionId = transactions.Find(t => t.date.Date == DateTime.Now.Date);
                                        //Add Detail Transaction
                                        existingTransactionId.detailTransaction.Add(new DetailTransaksi
                                        {
                                            id = existingTransactionId.detailTransaction.Count + 1,
                                            transactionId = existingTransactionId.id,
                                            product = productToBuy,
                                            qty = productQty,
                                            subtotal = productToBuy.price * productQty
                                        });
                                        //Update Total Transaction
                                        int indexToAddTotalFromExistingTransaction = existingTransactionId.detailTransaction.Count -1;
                                        existingTransactionId.total += existingTransactionId.detailTransaction[indexToAddTotalFromExistingTransaction].subtotal;
                                    }
                                    else
                                    {
                                        //Create Transaction
                                        transactions.Add(new Transaksi
                                        {
                                            id = newTransactionId,
                                            date = DateTime.Now,
                                            total = 0
                                        });
                                        //Add Detail Transaction
                                        transactions[newTransactionId - 1].detailTransaction.Add(new DetailTransaksi
                                        {
                                            id = transactions[newTransactionId - 1].detailTransaction.Count + 1,
                                            transactionId = newTransactionId,
                                            product = productToBuy,
                                            qty = productQty,
                                            subtotal = productToBuy.price * productQty
                                        });
                                        foreach (var transaction in transactions[newTransactionId - 1].detailTransaction)
                                        {
                                            transactions[newTransactionId - 1].total += transaction.subtotal;
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Okay, maybe next time! Byee\n");
                                    break;
                                }
                            }
                            else if (productQty == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[Error] Sorry, we don't have that quantity available.");
                                Console.ResetColor();
                                break;
                            }
                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[Error] Product not found in the menu!");
                            Console.ResetColor();
                            break;
                        }

                        break;
                    case 3:
                        isCustomerLoggedIn = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[Success] Customer Logout! Thank You!");
                        Console.ResetColor();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[Error] Invalid input! Please choose numbers 1, 2, or 3.");
                        Console.ResetColor();
                        break;
                }
            }
            break;
        case 3:
            Console.WriteLine("\nThank You for Visiting Toko Sembah Ayam!");
            return;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[Error] Invalid input! Please choose numbers 1, 2, or 3.");
            Console.ResetColor();
            break;

    }

}
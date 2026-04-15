using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

class Product
{
    public int ID {get; set;}
    public string Name {get; set;}
    public double Price {get; set;}
    public int Stock {get; set;}

    public Product(int id, string name, double price, int stock)
    {
        ID = id;
        Name = name;
        Price = price;
        Stock = stock;
    }
}
class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>()
        {
            new Product(1, "Quokka", 350, 10),
            new Product(2, "Leebit", 350, 50),
            new Product(3, "Puppym", 350, 30)
        };
        Product[] cart = new Product[15];
        int cartCount = 0;

        while (true)
        {
            Console.WriteLine("\n==WELCOME TO SKZOO SHOP!==");
            Console.WriteLine("1 - View Products");
            Console.WriteLine("2 - View Cart");
            Console.WriteLine("3 - Checkout");
            Console.WriteLine("4 - Exit");
            Console.Write("\nSelect an option: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                while (true)
                {
                    Console.WriteLine("\n==PRODUCTS==");

                    for (int i = 0; i < products.Count; i++)
                    {
                        Console.WriteLine($"{products[i].ID}. {products[i].Name} - {products[i].Price} (Stock: {products[i].Stock})");
                    }

                    Console.WriteLine("\nEnter product ID to add to cart");
                    Console.WriteLine("Type D to finish adding");
                    Console.WriteLine("Type 0 to go back");

                    string? input = Console.ReadLine();

                    if (string.Equals(input, "D", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Finished adding items.");
                        break; // only exits product loop
                    }

                    int id;

                    if (!int.TryParse(input, out id))
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    if (id == 0)
                    {
                        break; // go back to main menu safely
                    }

                    Product? selected = null;

                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].ID == id)
                        {
                            selected = products[i];
                            break;
                        }
                    }

                    if (selected == null)
                    {
                        Console.WriteLine("Product not found.");
                        continue; // don't exit program
                    }

                    Console.Write("Enter quantity: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    if (quantity <= 0 || quantity > selected.Stock)
                    {
                        Console.WriteLine("Invalid or insufficient stock.");
                        continue;
                    }
                    if (cartCount + quantity > 15)
                    {
                        Console.WriteLine("Not enough space in cart!");
                    }
                    else
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            cart[cartCount] = selected;
                            cartCount++;
                     
                        }
                        selected.Stock -= quantity;
                        Console.WriteLine("Added to cart!");
                    }
                }
            }

            else if (choice == 2)
            {
                Console.WriteLine("\n==CART==");
                double total = 0;
                for (int i = 0; i < cartCount; i++)
                {
                    Console.WriteLine($"{cart[i].Name} - {cart[i].Price}");
                    total += cart[i].Price;
                }
                Console.WriteLine($"Total: {total}");
            }
           else if (choice == 3)
            {
                double total = 0;

                // compute total
                for (int i = 0; i < cartCount; i++)
                {
                    total += cart[i].Price;
                }

                // DISCOUNT LOGIC
                if (total >= 5000)
                {
                    double discount = total * 0.10;
                    total -= discount;

                    Console.WriteLine("\n10% Discount Applied!");
                    Console.WriteLine($"Discount: {discount}");
                }

                Console.WriteLine($"\nTotal amount: {total}");

                double payment = 0;

                while (true)
                {
                    Console.Write("Enter payment amount: ");
                    payment = Convert.ToDouble(Console.ReadLine());

                    if (payment >= total)
                    {
                        Console.WriteLine($"\nPayment successful! Change: {payment - total}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient payment, please try again.");
                    }
                }

                // RECEIPT (MOVED OUTSIDE LOOP)
                Console.WriteLine("\nRECEIPT:");
                for (int i = 0; i < cartCount; i++)
                {
                    Console.WriteLine($"{cart[i].Name} - {cart[i].Price}");
                }

                Console.WriteLine($"TOTAL: {total}");
                Console.WriteLine($"PAYMENT: {payment}");
                Console.WriteLine($"CHANGE: {payment - total}");

                cartCount = 0;
                Console.WriteLine("\nThank you for shopping with us! >w<");
            }
            else if (choice == 4)
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option, please try again.");
            }
        }
    }
    
}

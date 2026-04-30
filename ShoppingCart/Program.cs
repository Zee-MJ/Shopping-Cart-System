using System;


//PROGRAM CLASS
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- WELCOME TO SKZ MERCH SHOP --");

        //Array
        Product[] products = new Product[7];

        products[0] = new Product { ID = 1, Name = "Skz Light Stick (Nachimbong)", Price = 2500, RemainingStock = 80, Category = "Official" };
        products[1] = new Product { ID = 2, Name = "Skzoo Plush (Set)", Price = 500, RemainingStock = 250, Category = "Official" };
        products[2] = new Product { ID = 3, Name = "Varsity Jacket(STAY Edition)", Price = 650, RemainingStock = 500, Category = "Jacket" };
        products[3] = new Product { ID = 4, Name = "DOIT BluetoothSpeaker", Price = 1000, RemainingStock = 350, Category = "Electronics" };
        products[4] = new Product { ID = 5, Name = "Skzoo Bluetooth Headphones", Price = 1500, RemainingStock = 247, Category = "Electronics" };
        products[5] = new Product { ID = 6, Name = "SKZOO Acrylic Keychain(Random Blind Pack)", Price = 125, RemainingStock = 10000, Category = "FanMade" };
        products[6] = new Product { ID = 7, Name = "SKZ polaroid-style sticker pack(5 pcs)", Price = 50, RemainingStock = 15, Category = "FanMade" };

        bool running = true;
        int cartCount = 0;
        Product[] cart = new Product[5];
        int[] quantities = new int[5];

        //RECEIPT AND DATE
        string orderDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        string[] history = new string[10];
        int receiptNumber = 0001;

        //Start ng Loop
        while (running)
        {
            Console.WriteLine("\n===== MAIN MENU =====");

            Console.WriteLine("1. Buy Product");
            Console.WriteLine("2. Cart Management");
            Console.WriteLine("3. View History");
            Console.WriteLine("4. Exit");
            Console.Write("Select Option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n--- BUY PRODUCTS ---");
                    Console.WriteLine("1. View All Products");
                    Console.WriteLine("2. Search Product by Name");
                    Console.WriteLine("3. Filter by Category");
                    Console.WriteLine("4. Back to Menu");
                    Console.Write("Choice: ");
                    string browseChoice = Console.ReadLine();

                    switch (browseChoice)
                    {
                        //DISPLAY ALL PRODUCTS
                        case "1":
                            foreach (Product p in products)
                            {
                                p.DisplayProduct(); //calling the method
                                
                            }
                            AddToCart(products, cart, quantities, ref cartCount);
                            break;

                        //SEARCH PRODUCT BY NAME
                        case "2":

                            bool searching = true;
                            while (searching)
                            {
                                Console.Write("\nEnter product name to search (or type 'back' to exit): ");
                                string searchName = Console.ReadLine().ToLower();

                                if (searchName == "back")
                                {
                                    searching = false;
                                    break;
                                }

                                bool found = false;
                                foreach (Product p in products)
                                {
                                    if (p.Name.ToLower() == searchName)
                                    {
                                        p.DisplayProduct();
                                        found = true;
                                    }
                                }

                                if (!found)
                                {
                                    Console.WriteLine("Product not found!");
                                }
                            }
                            break;

                        case "3":
                            bool filtering = true;
                            while (filtering)
                            {
                                Console.WriteLine("\n== FILTER CATEGORY ==");
                                Console.WriteLine("1. Snacks\n2. Beverages\n3. Electronics\n4. Toys");
                                Console.Write("Enter category to filter (or type 'back' to exit): ");

                                string searchCategory = Console.ReadLine().ToLower();

                                if (searchCategory == "back")
                                {
                                    filtering = false;
                                    break;
                                }

                                string selectedCategory = "";

                                switch (searchCategory)
                                {
                                    case "1": selectedCategory = "Official"; break;
                                    case "2": selectedCategory = "Jacket"; break;
                                    case "3": selectedCategory = "Electronics"; break;
                                    case "4": selectedCategory = "FanMade"; break;
                                    default:
                                        Console.WriteLine("Invalid category! Try again.");
                                        continue; 
                                }

                                Console.WriteLine($"\n--- {selectedCategory} Items ---");
                                foreach (Product p in products)
                                {
                                    if (p.Category == selectedCategory)
                                    {
                                        p.DisplayProduct();
                                    }
                                }
                            }
                            break;

                        case "4":
                            Console.WriteLine("Returning to main menu...");
                            break;
                    }
                
                break;

                //CART MANAGEMENT
                case "2":
                    if (cartCount == 0)
                    {
                        Console.WriteLine("Cart is empty.");
                    }
                    else
                    {
                        bool inCartMenu = true;
                        while (inCartMenu)
                        {
                            Console.WriteLine("\n--- CART MANAGEMENT MENU ---");
                            Console.WriteLine("1. View Cart\n2. Remove Item\n3. Update Quantity\n4. Clear Cart\n5. Checkout\n6. Back to Main Menu");
                            Console.Write("Input your choice: ");
                            string cartChoice = Console.ReadLine();

                            switch (cartChoice)
                            {
                                case "1":
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        Console.WriteLine("--- YOUR CART ---");
                                        Console.WriteLine($"\n{i + 1}. {cart[i].Name} x{quantities[i]} - P{cart[i].Price * quantities[i]}");
                                    }
                                    break;

                                case "2":
                                    Console.Write("Enter item number to remove: ");
                                    if (int.TryParse(Console.ReadLine(), out int removeIndex))
                                    {
                                        int index = removeIndex - 1;
                                        if (index >= 0 && index < cartCount)
                                        {
                                            cart[index].RemainingStock += quantities[index];

                                            for (int i = index; i < cartCount - 1; i++)
                                            {
                                                cart[i] = cart[i + 1];
                                                quantities[i] = quantities[i + 1];
                                            }
                                            cartCount--;
                                            Console.WriteLine("Item removed.");
                                        }
                                        else { Console.WriteLine("Invalid item number!"); }
                                    }
                                    break;

                                case "3":
                                    Console.Write("Enter item number to update: ");
                                    if (int.TryParse(Console.ReadLine(), out int updateNum))
                                    {
                                        int index = updateNum - 1;
                                        if (index >= 0 && index < cartCount)
                                        {
                                            Console.Write($"Enter new quantity for {cart[index].Name}: ");
                                            if (int.TryParse(Console.ReadLine(), out int newQty) && newQty > 0)
                                            {
                                                cart[index].RemainingStock += quantities[index];
                                                if (cart[index].HasEnoughStock(newQty))
                                                {
                                                    quantities[index] = newQty;
                                                    cart[index].DeductStock(newQty);
                                                    Console.WriteLine("Quantity updated!");
                                                }
                                                else
                                                {
                                                    cart[index].DeductStock(quantities[index]);
                                                    Console.WriteLine("Insufficient stock!");
                                                }
                                            }
                                        }
                                    }
                                    break;


                                case "4":
                                    for (int i = 0; i < cartCount; i++) products[cart[i].ID - 1].RemainingStock += quantities[i];
                                    cartCount = 0;
                                    Console.WriteLine("Cart cleared.");
                                    inCartMenu = false;
                                    break;
                                case "5": // CHECKOUT
                                    double bill = 0;
                                    Console.WriteLine("\n--- OFFICIAL RECEIPT ---");
                                    Console.WriteLine($"\nReceipt No: {receiptNumber:D4}\nDate: {DateTime.Now}");
                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        double sub = cart[i].Price * quantities[i];
                                        Console.WriteLine($"\n{cart[i].Name} x{quantities[i]} = {sub}");
                                        bill += sub;
                                    }
                                    if (bill >= 5000) { double disc = bill * 0.1; bill -= disc; Console.WriteLine("Discount: " + disc); }
                                    double originalTotal = 0;

                                    for (int i = 0; i < cartCount; i++)
                                    {
                                        originalTotal += cart[i].Price * quantities[i];
                                    }

                                    double discount = 0;

                                    if (originalTotal >= 5000)
                                    {
                                        discount = originalTotal * 0.10;
                                    }

                                    double finalTotal = originalTotal - discount;

                                    Console.WriteLine($"\nORIGINAL TOTAL: PHP {originalTotal:N2}");
                                    Console.WriteLine($"DISCOUNT: PHP {discount:N2}");
                                    Console.WriteLine($"FINAL TOTAL: PHP {finalTotal:N2}");

                                    while (true)
                                    {
                                        Console.Write("\nPayment: ");
                                        if (double.TryParse(Console.ReadLine(), out double pay) && pay >= bill)
                                        {
                                            Console.WriteLine("\nChange: " + (pay - bill));
                                            break;
                                        }
                                        Console.WriteLine("Invalid payment!");
                                    }

                                    if (receiptNumber <= 10)
                                    {
                                        history[receiptNumber - 1] = $"Receipt #{receiptNumber:D4} - Total: P{bill:N2} - {DateTime.Now}";
                                        receiptNumber++;
                                    }

                                    Console.WriteLine("\n--- LOW STOCK ALERT ---");
                                    foreach (Product p in products) if (p.RemainingStock <= 5) Console.WriteLine($"ALERT: {p.Name} - {p.RemainingStock} left");

                                    cartCount = 0;
                                    inCartMenu = false;

                                    string rep = "";
                                    while (true)
                                    {
                                        Console.Write("\nAnother transaction? (Y/N): ");
                                        rep = Console.ReadLine().ToUpper();
                                        if (rep == "Y" || rep == "N") break;
                                    }
                                    if (rep == "N")
                                    {
                                        running = false;

                                        Console.WriteLine("Goodbye! :3");
                                        Console.WriteLine("\nPress any key to close...");
                                        Console.ReadLine();
                                    }
                                break;

                                case "6": inCartMenu = false; 
                                    break;

                                default:
                                    Console.WriteLine("Invalid Input");
                                    break;
                            }
                            if (cartCount == 0 && inCartMenu) inCartMenu = false;
                        }
                    }
                    break;

                //VIEW HISTORY
                case "3":
                Console.WriteLine("\n===== ORDER HISTORY =====");
                bool hasHistory = false;
                for (int i = 0; i < 10; i++)
                {
                    if (history[i] != null)
                    {
                        Console.WriteLine(history[i]);
                        hasHistory = true;
                    }
                }
                if (!hasHistory) Console.WriteLine("No records found.");
                break;

            //EXIT
            case "4":
                Console.WriteLine("Byeee!");
                running = false;
                return;

            default:
                Console.WriteLine("Invalid option!");
                break;
            }

        }

    }

    static void AddToCart(Product[] products, Product[] cart, int[] quantities, ref int cartCount)
    {
        Console.Write("\nEnter Product ID to add: ");
        int inputid;

        if (!int.TryParse(Console.ReadLine(), out inputid))
        {
            Console.WriteLine("Invalid input!");
            return;
        }

        Product selectedProduct = null;
        foreach (Product p in products)
        {
            if (p.ID == inputid)
            {
                selectedProduct = p;
                break;
            }
        }

        if (selectedProduct == null)
        {
            Console.WriteLine("Invalid product!");
            return;
        }

        if (!selectedProduct.HasEnoughStock(1))
        {
            Console.WriteLine("Out of stock!");
            return;
        }

        Console.Write("How many?: ");
        int inputqty;

        if (!int.TryParse(Console.ReadLine(), out inputqty) || inputqty <= 0)
        {
            Console.WriteLine("Invalid quantity!");
            return;
        }

        if (!selectedProduct.HasEnoughStock(inputqty))
        {
            Console.WriteLine("Not enough stock available.");
            return;
        }

        int existing = -1;
        for (int i = 0; i < cartCount; i++)
        {
            if (cart[i].ID == selectedProduct.ID)
            {
                existing = i;
                break;
            }
        }

        if (existing != -1)
        {
            quantities[existing] += inputqty;
            selectedProduct.DeductStock(inputqty);
            Console.WriteLine("Updated existing item in cart");
        }
        else
        {
            if (cartCount >= 5)
            {
                Console.WriteLine("Cart is full.");
            }
            else
            {
                cart[cartCount] = selectedProduct;
                quantities[cartCount] = inputqty;
                cartCount++;
                selectedProduct.DeductStock(inputqty);
                Console.WriteLine("Added to cart!");
            }
        }
    }

}


class Product
{

    public int ID;
    public string Name;
    public double Price;
    public int RemainingStock;
    public string Category;

    public void DisplayProduct()
    {
        Console.WriteLine($"{ID}. {Name} - ${Price} - (Stock: {RemainingStock}) - Category: {Category}");

    }
    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}

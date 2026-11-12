using InventoryManager.Services;

Inventory inventory = new Inventory();
inventory.LoadFromFile();
Boolean exit = false;

while (!exit)
{
    
    Console.WriteLine("\n========================================");
    Console.WriteLine("      MINIMARKET INVENTORY SYSTEM       ");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Add a product");
    Console.WriteLine("2. View all products");
    Console.WriteLine("3. Update a product");
    Console.WriteLine("4. Delete a product");
    Console.WriteLine("5. Generate report");
    Console.WriteLine("6. Save and Exit");
    Console.Write("Choose an option (1-6): ");
    string option = Console.ReadLine();

    switch (option)
    {
        case "1":

            inventory.AddProduct();
            break;
        case "2":
            inventory.ViewProducts();
            break;
        case "3":
            
            inventory.UpdateProduct();
            
            break;
        case "4":
            inventory.DeleteProduct();
            break;
        case "5":
            inventory.GenerateReport();
            break;
        case "6":
            inventory.SaveAndExit();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option. Please try again.");
            Console.ResetColor();
            break;
    }
    
}

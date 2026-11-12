using InventoryManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace InventoryManager.Services
{
    internal class Inventory
    {
        private List<Product> products = new List<Product>();
        private readonly string filePath = "inventory.json";

        public void AddProduct()
        {
            while (true)
            {
                string name;
                while (true)
                {
                    Console.Write("Enter Product Name or 'Exit' for return: ");
                    name = Console.ReadLine()?.Trim();
                    if (name?.ToLower() == "exit")
                    {
                        Console.WriteLine("Returning to main menu...");
                        return; // exit the method and return to main menu
                    }
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        break; // valid name entered, exit the loop
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name cannot be empty. Please try again.");
                    Console.ResetColor();
                }
            
                int quantity;
                while (true)
                {
                    Console.Write("Enter Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                    {
                        break; // valid quantity entered, exit the loop
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid quantity. Must be a valid number. Please try again.");
                    Console.ResetColor();
                }
                
                decimal price;
                while (true)
                {
                    Console.Write("Enter Price: ");
                    if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
                    {
                        break; // valid price entered, exit the loop
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Must be a valid number. Please try again.");
                    Console.ResetColor();
                }
               
                Random random = new Random();
                int id = random.Next(1000, 10000);
                do
                {
                    id = random.Next(1000, 10000); // 4-digit random ID
                } while (products.Any(p => p.Id == id));

                Product newProduct = new Product(name, quantity, price, id);
                products.Add(newProduct);
                Console.WriteLine("Product added successfully.");

            }
         }
        public void ViewProducts()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=========================================================");
            Console.WriteLine("                     PRODUCT INVENTORY                   ");
            Console.WriteLine("=========================================================");
            Console.ResetColor();
            if (!products.Any())
            {
                Console.WriteLine("The inventory is currently empty.");
                return;
            }

            
            Console.WriteLine($"| {"ID",-8} | {"Name",-20} | {"Stock",-8} | {"Price",-10} |");
            Console.WriteLine("---------------------------------------------------------");

            
            foreach (var product in products)
            {
                Console.WriteLine($"  {product.Id,-8}   {product.Name,-20}   {product.Quantity,-8}   ${product.Price,-9:F2}");
            }

            Console.WriteLine("---------------------------------------------------------");
        }
        public void UpdateProduct()
        {
            while (true)
            {
                Console.Write("\nEnter the Product ID to update or type 'Exit' to return to menu: ");
                string inputId = Console.ReadLine()?.Trim();

                // return to main menu if user types 'exit'
                if (inputId?.ToLower() == "exit")
                {
                    Console.WriteLine("Returning to main menu...");
                    break; 
                }

                if (!int.TryParse(inputId, out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format. Please try again.");
                    Console.ResetColor();
                    continue; //ask for the ID again if the input is not a valid integer
                }

                // look for the product with the given ID
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product not found. Please try again.");
                    Console.ResetColor();
                    continue; //ask for the ID again if the product is not found
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nCurrent product details:");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"| {"ID",-8} | {"Name",-20} | {"Stock",-8} | {"Price",-10} |");
                Console.WriteLine("---------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine($"  {product.Id,-8}   {product.Name,-20}   {product.Quantity,-8}   ${product.Price,-9:F2}");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Enter new details:");
               
                string newName;
                while (true)
                {
                    Console.Write("New Name: ");
                    newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name cannot be empty. Update for this product cancelled.");
                    Console.ResetColor();
                    
                 
                }

                int newQuantity;
                while (true)
                {
                    Console.Write("New Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out newQuantity) && newQuantity >= 0)
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid quantity. Must be a valid number. Update for this product cancelled.");
                    Console.ResetColor();

                }

                decimal newPrice;
                while (true)
                {
                    Console.Write("New Price: ");
                    if (decimal.TryParse(Console.ReadLine(), out newPrice) && newPrice >= 0)
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Must be a valid number. Update for this product cancelled.");
                    Console.ResetColor();
                }
              

                // if all inputs are valid, update the product
                product.Name = newName;
                product.Quantity = newQuantity;
                product.Price = newPrice;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product updated successfully!");
                Console.ResetColor();
            }
        }
        public void DeleteProduct()
        {
            while (true)
            {
                Console.Write("\nEnter the Product ID to delete or type 'Exit' to return to menu: ");
                string inputId = Console.ReadLine()?.Trim();

                if (inputId?.ToLower() == "exit")
                {
                    Console.WriteLine("Returning to main menu...");
                    break;
                }

                if (!int.TryParse(inputId, out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format. Please try again.");
                    Console.ResetColor();
                    continue;
                }
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product not found. Please try again.");
                    Console.ResetColor();
                    continue;
                }

                // Show product details before deletion for confirmation
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nProduct found:");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"| {"ID",-8} | {"Name",-20} | {"Stock",-8} | {"Price",-10} |");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"  {product.Id,-8}   {product.Name,-20}   {product.Quantity,-8}   ${product.Price,-9:F2}");
                Console.WriteLine("---------------------------------------------------------");
                Console.ResetColor();
                // confirmation prompt before deletion
                Console.Write("Are you sure you want to delete this product? (y/n): ");
                string confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation == "y" || confirmation == "s")
                {
                    products.Remove(product);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Product deleted successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Deletion cancelled. The product was not removed.");
                    Console.ResetColor();
                }

                // The 'while (true)' loop will continue so you can keep deleting more products if you wish
            }
        }
        public void GenerateReport()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=========================================================");
            Console.WriteLine("                     INVENTORY REPORT                     ");
            Console.WriteLine("=========================================================");
            Console.ResetColor();
            if (!products.Any())
            {
                Console.WriteLine("No data available to generate a report.");
                return;
            }

            int totalItems = products.Sum(p => p.Quantity);
            decimal totalValue = products.Sum(p => p.Quantity * p.Price);

            Console.WriteLine($"Total Unique Products: {products.Count}");
            Console.WriteLine($"Total Stock Items: {totalItems}");
            Console.WriteLine($"Total Inventory Value: ${totalValue:F2}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=========================================================");
            Console.ResetColor();
            
        }
        public void SaveAndExit()
        {
            saveToFile();
            Console.WriteLine("Exiting the application. Goodbye!");
            Environment.Exit(0);
        }

        public void saveToFile()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(products, options);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine("Inventory successfully saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        public void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    // Read the raw JSON text from a file
                    string jsonString = File.ReadAllText(filePath);
                    // Convert the text back into a C# List of Product objects
                    // products is guaranteed never to be null
                    products = JsonSerializer.Deserialize<List<Product>>(jsonString) ?? new List<Product>(); 
                    Console.WriteLine("Inventory successfully loaded from file.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file: {ex.Message}");
                    products = new List<Product>();
                }
            }
            else
            {
                Console.WriteLine("No previous inventory file found. Starting fresh.");
                products = new List<Product>();
            }

        }

    }
}

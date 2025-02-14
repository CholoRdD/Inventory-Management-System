using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

class Program
{
    static void Main()
    {
        //Calling the Inventory Management System
        IMS ims = new IMS();

        //Displaying Choices
        while (true)
        {
            Console.WriteLine("\n\n\n1. Add a new Product");
            Console.WriteLine("2. Remove a Product from Inventory");
            Console.WriteLine("3. Update Specific Product Quantity");
            Console.WriteLine("4. Display List of Products in Inventory");
            Console.WriteLine("5. Get the Total Inventory Value");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            int userChoice;

            if (!int.TryParse(Console.ReadLine(), out userChoice)) continue;

        //User inputs for specifying functions
            switch (userChoice)
            {
                case 1:
                    Console.Write("\nEnter Product ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Enter Price: ");
                    double price = double.Parse(Console.ReadLine());
                    ims.AddProduct(new Product(id, name, quantity, price));
                    break;
                case 2:
                    Console.Write("\nEnter Product ID to remove: ");
                    int removeId = int.Parse(Console.ReadLine());
                    ims.RemoveProduct(removeId);
                    break;
                case 3:
                    Console.Write("\nEnter Product ID to update: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Enter new quantity: ");
                    int newQuantity = int.Parse(Console.ReadLine());
                    ims.UpdateProduct(updateId, newQuantity);
                    break;
                case 4:
                    ims.ListProducts();
                    break;
                case 5:
                    ims.GetTotalValue();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }


    }
}

//Product Detailing
class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int QuantityInStock { get; set; }
    public double Price { get; set; }

    //Making Constraits
    public Product(int productid, string name, int quantityinstock, double price)
    {
        if (ProductId < 0) throw new ArgumentException("The Product Id needs to be a positive integer");
        if (Price < 0) throw new ArgumentException("Price cannot be a negative value");
        if (QuantityInStock < 0) throw new ArgumentException("Stocks should not be negative");

        ProductId = productid;
        Name = name;
        QuantityInStock = quantityinstock;
        Price = price;

    }
}

//Inventory Management System
class IMS 
{
    private List<Product> products = new List<Product>();
    
    //Adding a product to the list
    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"New product \"{product.Name}\" added in inventory list");
    }

    //Removing Product in list
    public void RemoveProduct(int productid) 
    {
        var product = products.FirstOrDefault(product => product.ProductId == productid);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine($"The product \"{product.Name}\" was removed from inventory list");
        }
        else
        {
            Console.WriteLine("That product ID is not in the list");
        }

    }

    //Updating Product Quantity
    public void UpdateProduct(int productid, int newQty)
    {
        if(newQty< 0)
        {
            Console.WriteLine("Quantity cannot be negative");
        }

        var product = products.FirstOrDefault(product => product.ProductId == productid);
        if (product != null)
        {
            product.QuantityInStock = newQty;
            Console.WriteLine($"The Product \"{product.Name}\" quantity has been updated");
        }
        else
        {
            Console.WriteLine("The product Id does not exist");
        }
    }

    //Displaying List breakdown of inventory
    public void ListProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("\n\n\nThe list is empty");
            return;
        }

        Console.WriteLine("\n\n\nInventory Breakdown");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.ProductId} | Name: {product.Name} | Quantity: {product.QuantityInStock} | Price: {product.Price:C}");
        }
    }

    //Getting total Value
    public double GetTotalValue()
    {
        double totalVal = products.Sum(product => product.QuantityInStock * product.Price);
        Console.WriteLine($"\n\n\nThe total inventory value is {totalVal:C}");
        return totalVal;
    }
}

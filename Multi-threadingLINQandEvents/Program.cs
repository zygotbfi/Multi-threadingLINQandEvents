using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// Multi-threading
class NumberProcessor
{
    public void ProcessNumber(int number)
    {
        // Simulate time-consuming operation
        Thread.Sleep(new Random().Next(1000, 5000));
        Console.WriteLine($"Processed number: {number}");
    }
}

// LINQ
class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Multi-threading
        NumberProcessor numberProcessor = new NumberProcessor();
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        foreach (var number in numbers)
        {
            Thread thread = new Thread(() => numberProcessor.ProcessNumber(number));
            thread.Start();
        }

        // LINQ
        List<Product> products = new List<Product>
        {
            new Product { Name = "Product1", Price = 10.99 },
            new Product { Name = "Product2", Price = 20.49 },
            new Product { Name = "Product3", Price = 5.99 },
            new Product { Name = "Product4", Price = 15.99 }
        };

        double specifiedValue = 10.0;
        var filteredProducts = products.Where(p => p.Price >= specifiedValue);
        var upperCaseProductNames = filteredProducts.Select(p => p.Name.ToUpper());

        Console.WriteLine("\nProducts with price greater than or equal to specified value:");
        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
        }

        Console.WriteLine("\nUpper-cased product names:");
        foreach (var productName in upperCaseProductNames)
        {
            Console.WriteLine(productName);
        }

        // Events
        Downloader downloader = new Downloader();
        downloader.ProgressChanged += ProgressHandler;
        downloader.DownloadFile();
    }

    // Event handler
    static void ProgressHandler(int progress)
    {
        Console.WriteLine($"Download Progress: {progress}%");
    }
}

class Downloader
{
    public int Progress { get; private set; }
    public event ProgressUpdate ProgressChanged;

    public void DownloadFile()
    {
        for (int i = 0; i <= 100; i += 10)
        {
            Progress = i;
            ProgressChanged?.Invoke(i);
            Thread.Sleep(1000); // Simulate download progress
        }
    }
}

// Delegate for event
delegate void ProgressUpdate(int progress);

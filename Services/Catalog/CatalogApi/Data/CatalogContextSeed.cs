using CatalogApi.Entities;
using MongoDB.Driver;

namespace CatalogApi.Data;

public class CatalogContextSeed
{

    public static void SeedData(IMongoCollection<Product> ProductCollection)
    {
        bool existproduct = ProductCollection.Find(p => true).Any();

        if (!existproduct)
        {
            ProductCollection.InsertManyAsync(GetSeedData());
        }
    }

    private static IEnumerable<Product> GetSeedData()
    {
        return new List<Product>()
    {
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a101",
            Name = "Wireless Mouse",
            Category = "Electronics",
            Summery = "Comfortable wireless mouse",
            Description = "A high-precision ergonomic wireless mouse with long battery life.",
            ImageFile = "mouse.jpg",
            Price = 29.99m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a102",
            Name = "Bluetooth Speaker",
            Category = "Electronics",
            Summery = "Portable Bluetooth speaker",
            Description = "Compact and powerful speaker with deep bass and 10-hour battery.",
            ImageFile = "speaker.jpg",
            Price = 59.99m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a103",
            Name = "Running Shoes",
            Category = "Fashion",
            Summery = "Lightweight running shoes",
            Description = "Breathable and durable shoes for everyday running.",
            ImageFile = "shoes.jpg",
            Price = 89.50m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a104",
            Name = "Laptop Stand",
            Category = "Accessories",
            Summery = "Adjustable aluminum laptop stand",
            Description = "Ergonomic stand for laptops up to 17 inches, helps with cooling.",
            ImageFile = "stand.jpg",
            Price = 42.00m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a105",
            Name = "Noise Cancelling Headphones",
            Category = "Electronics",
            Summery = "Over-ear noise cancelling headphones",
            Description = "Perfect for focus and immersive audio experience.",
            ImageFile = "headphones.jpg",
            Price = 119.99m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a106",
            Name = "Backpack",
            Category = "Fashion",
            Summery = "Waterproof travel backpack",
            Description = "Durable backpack with laptop compartment and USB port.",
            ImageFile = "backpack.jpg",
            Price = 39.99m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a107",
            Name = "Smart Watch",
            Category = "Electronics",
            Summery = "Smart watch with health tracking",
            Description = "Tracks heart rate, steps, sleep, and displays notifications.",
            ImageFile = "smartwatch.jpg",
            Price = 74.90m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a108",
            Name = "Coffee Mug",
            Category = "Kitchen",
            Summery = "Ceramic coffee mug",
            Description = "350ml mug with heat-resistant handle.",
            ImageFile = "mug.jpg",
            Price = 8.99m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a109",
            Name = "Desk Lamp",
            Category = "Home",
            Summery = "LED desk lamp with touch control",
            Description = "Adjustable brightness and color temperature.",
            ImageFile = "lamp.jpg",
            Price = 26.75m
        },
        new Product
        {
            Id = "64e4a1b7a2c39b5f00a1a10a",
            Name = "Fitness Band",
            Category = "Electronics",
            Summery = "Basic fitness tracking band",
            Description = "Waterproof band with step counter and notifications.",
            ImageFile = "band.jpg",
            Price = 19.99m
        }
    };
    }

}

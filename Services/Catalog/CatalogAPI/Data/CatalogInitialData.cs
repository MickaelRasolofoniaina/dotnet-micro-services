using Marten.Schema;

namespace CatalogAPI.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store(GetPreconfiguredProducts());

        await session.SaveChangesAsync(cancellation);
    }

    // Impelement this method to return a list of preconfigured products
    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return
        [
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple iPhone 14",
                Description = "Latest model of the Apple iPhone series with advanced features.",
                Price = 999.99m,
                ImageFile = "https://example.com/images/iphone14.jpg",
                Category = ["Smartphones"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S23",
                Description = "Flagship smartphone from Samsung with cutting-edge technology.",
                Price = 899.99m,
                ImageFile = "https://example.com/images/galaxy-s23.jpg",
                Category = ["Smartphones"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM5",
                Description = "Noise-canceling wireless headphones with superior sound quality.",
                Price = 399.99m,
                ImageFile = "https://example.com/images/sony-wh-1000xm5.jpg",
                Category = ["Headphones"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 13",
                Description = "High-performance ultrabook with a sleek design.",
                Price = 1299.99m,
                ImageFile = "https://example.com/images/dell-xps-13.jpg",
                Category = ["Laptops"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple MacBook Pro 16\"",
                Description = "Powerful laptop with M1 Pro chip for professionals.",
                Price = 2499.99m,
                ImageFile = "https://example.com/images/macbook-pro-16.jpg",
                Category = ["Laptops"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 7",
                Description = "Google's latest smartphone with AI-powered features.",
                Price = 599.99m,
                ImageFile = "https://example.com/images/pixel-7.jpg",
                Category = ["Smartphones"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Nintendo Switch OLED",
                Description = "Hybrid gaming console with an OLED display.",
                Price = 349.99m,
                ImageFile = "https://example.com/images/nintendo-switch-oled.jpg",
                Category = ["Gaming Consoles"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony PlayStation 5",
                Description = "Next-generation gaming console with immersive graphics.",
                Price = 499.99m,
                ImageFile = "https://example.com/images/playstation-5.jpg",
                Category = ["Gaming Consoles"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bose QuietComfort 45",
                Description = "Comfortable noise-canceling headphones with premium sound.",
                Price = 329.99m,
                ImageFile = "https://example.com/images/bose-qc45.jpg",
                Category = ["Headphones"]
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple iPad Pro 12.9\"",
                Description = "High-performance tablet with Liquid Retina XDR display.",
                Price = 1099.99m,
                ImageFile = "https://example.com/images/ipad-pro-12-9.jpg",
                Category = ["Tablets"]
            }
        ];
    }
}

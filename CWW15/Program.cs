
using CWW15.DataAccess;
using CWW15.Repositories;
using CWW15.Services;


var dbContext = new AppDbContext();
var productRepository = new ProductRepository(dbContext);
var productService = new ProductService(productRepository);


Console.WriteLine("--- Product Search ---");

Console.Write("Enter Product Name (or leave empty): ");
string? name = Console.ReadLine();

Console.Write("Enter Max Price (or leave empty): ");

decimal? maxPrice = null;
var maxPriceInput = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(maxPriceInput))
{
    
    if (decimal.TryParse(maxPriceInput, out decimal price))
    {
        maxPrice = price;
    }
}

Console.WriteLine("\nSearching for products...");

var results = productService.SearchProducts(
    name,
    minPrice: null, 
    maxPrice: maxPrice,
    categoryId: null,
    categoryName: null,
    color: null,
    brand: null,
    minStock: null
);

Console.WriteLine("\n--- Search Results ---");
 if (results.Any()) 
{
    foreach (var product in results)
    {
        
        Console.WriteLine(product);
    }
}
else
{
    Console.WriteLine("No products found matching your criteria.");
}
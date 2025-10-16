

using CWW15.DataAccess;
using CWW15.Dtos; 
using CWW15.Repositories;
using CWW15.Services;

var dbContext = new AppDbContext();
var productRepository = new ProductRepository(dbContext);
var productService = new ProductService(productRepository);

Console.WriteLine("--- Product Search ---");

var searchDto = new ProductSearchDto();

Console.Write("Enter Product Name (or leave empty): ");
searchDto.Name = Console.ReadLine();

Console.Write("Enter Max Price (or leave empty): ");
var maxPriceInput = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(maxPriceInput))
{
    if (decimal.TryParse(maxPriceInput, out decimal price))
    {
        searchDto.MaxPrice = price;
    }
}

Console.WriteLine("\nSearching for products...");


var results = productService.SearchProducts(searchDto);

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
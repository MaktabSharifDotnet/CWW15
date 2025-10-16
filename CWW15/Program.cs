
using CWW15.DataAccess;
using CWW15.Dtos;
using CWW15.Enums;
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
    try
    {
     
        searchDto.MaxPrice = decimal.Parse(maxPriceInput);
    }
    catch (FormatException)
    {
      
        Console.WriteLine("Invalid format for price. Price filter will be ignored.");
    }
}

Console.WriteLine("\n--- Sort Options ---");
Console.WriteLine("Sort by: 1. Name | 2. Price | 3. Stock | (Leave empty for no sort)");
Console.Write("Enter your choice: ");
var sortByInput = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(sortByInput))
{
    try
    {
        int sortByChoice = int.Parse(sortByInput);
        switch (sortByChoice)
        {
            case 1:
                searchDto.SortBy = SortByOptionEnum.Name;
                break;
            case 2:
                searchDto.SortBy = SortByOptionEnum.Price;
                break;
            case 3:
                searchDto.SortBy = SortByOptionEnum.Stock;
                break;
            default:
                Console.WriteLine("Invalid choice for sort by. Sort will be ignored.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid format for sort choice. Sort will be ignored.");
    }
}

if (searchDto.SortBy.HasValue)
{
    Console.WriteLine("Sort direction: 1. Ascending | 2. Descending");
    Console.Write("Enter your choice: ");
    var sortDirectionInput = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(sortDirectionInput))
    {
        try
        {
            int sortDirectionChoice = int.Parse(sortDirectionInput);
            switch (sortDirectionChoice)
            {
                case 2:
                    searchDto.SortDirection = SortDirectionOptionEnum.Descending;
                    break;
                case 1:
                    searchDto.SortDirection = SortDirectionOptionEnum.Ascending;
                    break;
                default:
                    Console.WriteLine("Invalid choice for sort direction. Defaulting to Ascending.");
                    searchDto.SortDirection = SortDirectionOptionEnum.Ascending;
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format for sort direction. Defaulting to Ascending.");
            searchDto.SortDirection = SortDirectionOptionEnum.Ascending;
        }
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
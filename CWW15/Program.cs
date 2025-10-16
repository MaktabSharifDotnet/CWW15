
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



Console.WriteLine("\n--- Sort Options ---");
Console.WriteLine("Sort by: 1. Name | 2. Price | 3. Stock | (Leave empty for no sort)");
Console.Write("Enter your choice: ");
var sortByInput = Console.ReadLine();
if (int.TryParse(sortByInput, out int sortByChoice))
{
   
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
      
    }

    if (searchDto.SortBy.HasValue)
    {
        Console.WriteLine("Sort direction: 1. Ascending | 2. Descending");
        Console.Write("Enter your choice: ");
        var sortDirectionInput = Console.ReadLine();
        if (int.TryParse(sortDirectionInput, out int sortDirectionChoice))
        {
            switch (sortDirectionChoice)
            {
                case 2:
                    searchDto.SortDirection = SortDirectionOptionEnum.Descending;
                    break;
                default: 
                    searchDto.SortDirection = SortDirectionOptionEnum.Ascending;
                    break;
            }
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
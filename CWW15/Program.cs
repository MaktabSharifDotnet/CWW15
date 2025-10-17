using CWW15.DataAccess;
using CWW15.Dtos;
using CWW15.Enums;
using CWW15.Repositories;
using CWW15.Services;


var dbContext = new AppDbContext();
var productRepository = new ProductRepository(dbContext);
var productService = new ProductService(productRepository);

while (true)
{
    Console.Clear();
    Console.WriteLine("--- Product Search Engine ---");
    Console.WriteLine("Type 'exit' at any time to quit the program.");

    var searchDto = new ProductSearchDto();

   
    Console.Write("\nEnter Product Name (or leave empty): ");
    var nameInput = Console.ReadLine();
    if (nameInput?.ToLower() == "exit") break;
    searchDto.Name = nameInput;

    Console.Write("Enter Max Price (or leave empty): ");
    var maxPriceInput = Console.ReadLine();
    if (maxPriceInput?.ToLower() == "exit") break;
    if (!string.IsNullOrWhiteSpace(maxPriceInput))
    {
        try { searchDto.MaxPrice = decimal.Parse(maxPriceInput); }
        catch (FormatException) { Console.WriteLine("Invalid format for price. Filter ignored."); }
    }

 
    Console.WriteLine("\n--- Sort Options ---");
    while (true)
    {
        Console.WriteLine("Add a sort criterion? (y/n)");
        var addSort = Console.ReadLine();
        if (addSort?.ToLower() != "y")
        {
            break;
        }

        var criterion = new SortCriterionDto();

        Console.WriteLine("Sort by: 1. Name | 2. Price | 3. Stock");
        Console.Write("Enter your choice: ");
        var sortByInput = Console.ReadLine();

        try
        {
            int sortByChoice = int.Parse(sortByInput);
            switch (sortByChoice)
            {
                case 1: criterion.SortBy = SortByOptionEnum.Name; break;
                case 2: criterion.SortBy = SortByOptionEnum.Price; break;
                case 3: criterion.SortBy = SortByOptionEnum.Stock; break;
                default: Console.WriteLine("Invalid choice. Ignoring this criterion."); continue;
            }

            Console.WriteLine("Sort direction: 1. Ascending | 2. Descending (default: Ascending)");
            Console.Write("Enter your choice: ");
            var sortDirectionInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(sortDirectionInput))
            {
                if (int.Parse(sortDirectionInput) == 2)
                {
                    criterion.SortDirection = SortDirectionOptionEnum.Descending;
                }
            }

            searchDto.SortCriteria.Add(criterion);
            Console.WriteLine("--> Sort criterion added!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format. Ignoring this criterion.");
        }
    }


    Console.WriteLine("\n--- Pagination Options ---");
    Console.Write("Enter Page Size (e.g., 5, or leave empty for all): ");
    var pageSizeInput = Console.ReadLine();
    if (pageSizeInput?.ToLower() == "exit") break;
    if (!string.IsNullOrWhiteSpace(pageSizeInput))
    {
        try
        {
            searchDto.PageSize = int.Parse(pageSizeInput);
            searchDto.PageNumber = 1;

            Console.Write($"Enter Page Number (default is 1): ");
            var pageNumberInput = Console.ReadLine();
            if (pageNumberInput?.ToLower() == "exit") break;
            if (!string.IsNullOrWhiteSpace(pageNumberInput))
            {
                searchDto.PageNumber = int.Parse(pageNumberInput);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format for pagination. Pagination will be ignored.");
            searchDto.PageSize = null;
            searchDto.PageNumber = null;
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

    Console.WriteLine("\nPress any key to start a new search...");
    Console.ReadKey();
}

Console.WriteLine("Exiting program. Goodbye!");

using CWW15.DataAccess;
using CWW15.Dtos; 
using CWW15.Entities;
using CWW15.Enums;
using Microsoft.EntityFrameworkCore;

namespace CWW15.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Product> SearchProducts(ProductSearchDto searchDto)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDto.Name))
            {
                query = query.Where(p => p.Name.Contains(searchDto.Name));
            }
            if (searchDto.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= searchDto.MinPrice.Value);
            }

            if (searchDto.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= searchDto.MaxPrice.Value);
            }

            if (searchDto.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == searchDto.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchDto.CategoryName))
            {
                query = query.Where(p => p.Category.Name.Contains(searchDto.CategoryName));
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Color))
            {
                query = query.Where(p => p.Color == searchDto.Color);
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Brand))
            {
                query = query.Where(p => p.Brand == searchDto.Brand);
            }

            if (searchDto.MinStock.HasValue)
            {
                query = query.Where(p => p.Stock >= searchDto.MinStock.Value);
            }

            if (searchDto.SortCriteria.Any())
            {
                
                var firstCriterion = searchDto.SortCriteria.First();
                IOrderedQueryable<Product> orderedQuery;

                if (firstCriterion.SortDirection == SortDirectionOptionEnum.Descending)
                {
                    
                    switch (firstCriterion.SortBy)
                    {
                        case SortByOptionEnum.Name:
                            orderedQuery = query.OrderByDescending(p => p.Name);
                            break;
                        case SortByOptionEnum.Price:
                            orderedQuery = query.OrderByDescending(p => p.Price);
                            break;
                        case SortByOptionEnum.Stock:
                            orderedQuery = query.OrderByDescending(p => p.Stock);
                            break;
                        default:
                            orderedQuery = query.OrderByDescending(p => p.Id); 
                            break;
                    }
                }
                else
                {
                 
                    switch (firstCriterion.SortBy)
                    {
                        case SortByOptionEnum.Name:
                            orderedQuery = query.OrderBy(p => p.Name);
                            break;
                        case SortByOptionEnum.Price:
                            orderedQuery = query.OrderBy(p => p.Price);
                            break;
                        case SortByOptionEnum.Stock:
                            orderedQuery = query.OrderBy(p => p.Stock);
                            break;
                        default:
                            orderedQuery = query.OrderBy(p => p.Id); 
                            break;
                    }
                }           
                foreach (var criterion in searchDto.SortCriteria.Skip(1))
                {
                    if (criterion.SortDirection == SortDirectionOptionEnum.Descending)
                    {
                        switch (criterion.SortBy)
                        {
                            case SortByOptionEnum.Name:
                                orderedQuery = orderedQuery.ThenByDescending(p => p.Name);
                                break;
                            case SortByOptionEnum.Price:
                                orderedQuery = orderedQuery.ThenByDescending(p => p.Price);
                                break;
                            case SortByOptionEnum.Stock:
                                orderedQuery = orderedQuery.ThenByDescending(p => p.Stock);
                                break;
                        }
                    }
                    else
                    {
                        switch (criterion.SortBy)
                        {
                            case SortByOptionEnum.Name:
                                orderedQuery = orderedQuery.ThenBy(p => p.Name);
                                break;
                            case SortByOptionEnum.Price:
                                orderedQuery = orderedQuery.ThenBy(p => p.Price);
                                break;
                            case SortByOptionEnum.Stock:
                                orderedQuery = orderedQuery.ThenBy(p => p.Stock);
                                break;
                        }
                    }
                }

                query = orderedQuery;
            }
            if (searchDto.PageNumber.HasValue && searchDto.PageSize.HasValue)
            {
              
                query = query.Skip((searchDto.PageNumber.Value - 1) * searchDto.PageSize.Value)
                           
                             .Take(searchDto.PageSize.Value);
            }
            return query.ToList();
        }
    }
}
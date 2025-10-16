

using CWW15.DataAccess;
using CWW15.Dtos; 
using CWW15.Entities;
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

            return query.ToList();
        }
    }
}
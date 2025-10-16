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

        public List<Product> SearchProducts(ProductSearchDto productSearchDto)
            
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(productSearchDto.Name))
            {
                query = query.Where(p => p.Name.Contains(productSearchDto.Name));
            }

            if (productSearchDto.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= productSearchDto.MinPrice.Value);
            }

            if (productSearchDto.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= productSearchDto.MaxPrice.Value);
            }

            if (productSearchDto.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == productSearchDto.CategoryId.Value);
            }

           
            if (!string.IsNullOrWhiteSpace(productSearchDto.CategoryName))
            {
                
                query = query.Where(p => p.Category.Name.Contains(productSearchDto.CategoryName));
            }

            if (!string.IsNullOrWhiteSpace(productSearchDto.Color))
            {
                query = query.Where(p => p.Color == productSearchDto.Color);
            }

            if (!string.IsNullOrWhiteSpace(productSearchDto.Brand))
            {
                query = query.Where(p => p.Brand == productSearchDto.Brand);
            }

            if (productSearchDto.MinStock.HasValue)
            {
                query = query.Where(p => p.Stock >= productSearchDto.MinStock.Value);
            }

            return query.ToList();
        }
    }
}
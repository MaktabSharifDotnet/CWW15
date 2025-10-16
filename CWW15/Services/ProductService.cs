
using CWW15.Dtos;
using CWW15.Entities;
using CWW15.Repositories;

namespace CWW15.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

      
        public List<Product> SearchProducts(ProductSearchDto searchDto)
        {
            
            var products = _productRepository.SearchProducts(searchDto);
            return products;
        }
    }
}
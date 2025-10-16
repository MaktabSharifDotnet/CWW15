using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWW15.Dtos
{
    public class ProductSearchDto
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public int? MinStock { get; set; }
    }
}

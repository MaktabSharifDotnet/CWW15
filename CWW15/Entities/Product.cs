using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWW15.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price:C}, Brand: {Brand}, Stock: {Stock}, Category: {Category.Name}";
        }
    }
}

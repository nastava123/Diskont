using DataLayer.Interface;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
           _repository = repository;
        }

        public bool Add(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentNullException("Ime proizvoda je obavezno");
            if (product.Price <= 0)
                throw new ArgumentException("Cena veca od nule");
            if (product.Count < 0)
                throw new ArgumentException("Broj proizvoda mora biti veci od nule");
            if (product.IdCategory == 0)
                throw new ArgumentException("Morate odabrati kategoriju proizvoda");


            return _repository.Add(product);
        }

        public Product? Get(int id)
        {
            return _repository.Get(id);
        }
        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }
            
    }
}

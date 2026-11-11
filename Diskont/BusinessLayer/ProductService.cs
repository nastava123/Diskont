using DataLayer.Interface;
using Entities;

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
            Validate(product);
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

        public List<Product> GetByCategory(int idCategory)
        {
            return _repository.GetByCategory(idCategory);
        }

        // kupcu se prikazuju samo proizvodi koji su na stanju
        public List<Product> GetAvailable()
        {
            return _repository.GetAll().Where(p => p.Count > 0).ToList();
        }

        public bool Update(Product product)
        {
            if (_repository.Get(product.IdProduct) == null)
                throw new InvalidOperationException("Proizvod ne postoji");

            Validate(product);
            return _repository.Update(product);
        }

        public bool Delete(Product product)
        {
            return _repository.Delete(product);
        }

        // ista pravila važe i za dodavanje i za izmenu
        private void Validate(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Ime proizvoda je obavezno");
            if (product.Price <= 0)
                throw new ArgumentException("Cena mora biti veća od nule");
            if (product.Count < 0)
                throw new ArgumentException("Broj proizvoda ne može biti negativan");
            if (product.IdCategory == 0)
                throw new ArgumentException("Morate odabrati kategoriju proizvoda");
        }
    }
}

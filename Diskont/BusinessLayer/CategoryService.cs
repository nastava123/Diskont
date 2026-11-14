using DataLayer.Interface;
using Entities;

namespace BusinessLayer
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Category category)
        {
            Validate(category);
            if (_repository.GetByName(category.Name!) != null)
                throw new InvalidOperationException("Kategorija sa tim nazivom već postoji");

            return _repository.Add(category);
        }

        public Category? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public bool Update(Category category)
        {
            if (_repository.Get(category.IdCategory) == null)
                throw new InvalidOperationException("Kategorija ne postoji");

            Validate(category);
            Category? sameName = _repository.GetByName(category.Name!);
            if (sameName != null && sameName.IdCategory != category.IdCategory)
                throw new InvalidOperationException("Kategorija sa tim nazivom već postoji");

            return _repository.Update(category);
        }

        public bool Delete(Category category)
        {
            return _repository.Delete(category);
        }

        private void Validate(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Naziv kategorije je obavezan");
            if (category.Name.Length > 50)
                throw new ArgumentException("Naziv kategorije može imati najviše 50 znakova");
        }
    }
}

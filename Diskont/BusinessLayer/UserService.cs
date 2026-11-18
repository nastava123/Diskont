using DataLayer.Interface;
using Entities;

namespace BusinessLayer
{
    public class UserService
    {
        private const int CustomerRole = 2;     // uloga „Kupac" iz 02_podaci.sql

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        // Registracija novog kupca.
        // Napomena: heširanje lozinke radi se u vežbama o autentifikaciji.
        public bool Add(User user)
        {
            Validate(user);
            user.Email = user.Email!.Trim().ToLower();
            if (_repository.GetByEmail(user.Email) != null)
                throw new InvalidOperationException("Korisnik sa tim e-mailom već postoji");

            user.Status = true;
            user.IdRole = CustomerRole;
            return _repository.Add(user);
        }

        public User? Get(int id)
        {
            return _repository.Get(id);
        }

        public User? GetByEmail(string email)
        {
            return _repository.GetByEmail(email.Trim().ToLower());
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }

        public bool Update(User user)
        {
            if (_repository.Get(user.IdUser) == null)
                throw new InvalidOperationException("Korisnik ne postoji");

            Validate(user);
            return _repository.Update(user);
        }

        // Blokiranje i odblokiranje naloga
        public bool SetStatus(int idUser, bool status)
        {
            User? user = _repository.Get(idUser);
            if (user == null)
                throw new InvalidOperationException("Korisnik ne postoji");

            user.Status = status;
            return _repository.Update(user);
        }

        public bool Delete(User user)
        {
            return _repository.Delete(user);
        }

        private void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new ArgumentException("Ime je obavezno");
            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new ArgumentException("Prezime je obavezno");
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains('@'))
                throw new ArgumentException("E-mail nije ispravan");
            if (string.IsNullOrWhiteSpace(user.PasswordHash) || user.PasswordHash.Length < 8)
                throw new ArgumentException("Lozinka mora imati najmanje 8 znakova");
        }
    }
}

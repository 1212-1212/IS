using EShop.Domain.Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Create(Client client)
        {
            _repository.Insert(client);
        }

        public void Delete(string id)
        {
            _repository.Delete(Get(id));
        }

        public Client Get(string? id)
        {
           return  _repository.Get(id);
        }

        public List<Client> GetAll()
        {
           return _repository.GetAll().ToList();
        }

        public Client GetByEmail(string email)
        {
           return _repository.GetByEmail(email);
        }

        public void Update(Client client)
        {
             _repository.Update(client);
        }
    }
}

using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Client> GetAll();

        Client Get(string id);

        void Insert(Client client);

        void Update(Client client);

        void Delete(Client client);

        Client GetByEmail(string email);
    }
}

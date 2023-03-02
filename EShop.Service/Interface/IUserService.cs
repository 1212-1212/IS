using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IUserService
    {
        List<Client> GetAll();
        Client Get(string? id);
        void Create(Client client);
        void Update(Client client);
        void Delete(string id);

        Client GetByEmail(string email);
    }
}

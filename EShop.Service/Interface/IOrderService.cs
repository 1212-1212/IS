using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order Get(Guid id);
        void Create(Order order);
        void Update(Order order);
        void Delete(Order order);

        List<Order> GetAllFromUser(Client client);

        IEnumerable<Order> GetAllBetweenDates(DateTime   from, DateTime to);

    }
}

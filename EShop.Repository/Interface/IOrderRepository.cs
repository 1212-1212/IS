using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IOrderRepository
    {

        IEnumerable<Order> GetAll();
        Order Get(Guid id);
        void Insert(Order entity);
        void Update(Order entity);
        void Delete(Order entity);
    }
}

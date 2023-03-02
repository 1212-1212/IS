using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IOrderContainsCarPartRepository
    {
        IEnumerable<OrderContainsCarPart> GetAll();
        OrderContainsCarPart Get(Guid? carPartId, Guid? orderId);
        void Insert(OrderContainsCarPart entity);
        void Update(OrderContainsCarPart entity);
        void Delete(OrderContainsCarPart entity);
        IEnumerable<OrderContainsCarPart> GetAllFromOrder(Order order);
    }
}

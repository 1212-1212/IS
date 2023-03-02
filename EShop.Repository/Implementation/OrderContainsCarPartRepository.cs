using Eshop.Repository.Data;
using EShop.Domain.Models;
using EShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementation
{
    public class OrderContainsCarPartRepository : IOrderContainsCarPartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<OrderContainsCarPart> _entities;

        public OrderContainsCarPartRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<OrderContainsCarPart>();
        }

        public void Delete(OrderContainsCarPart entity)
        {
            _entities.Remove(entity);
        }

        public OrderContainsCarPart Get(Guid? carPartId, Guid? orderId)
        {
            return _entities
                .Include(item => item.Order)
                .Include(item => item.CarPart)
                .Include(item => item.CarPart.Brand)
                .Include(item => item.CarPart.Type)
                .Include(item => item.CarPart.Stage)
                .FirstOrDefault(item => item.CarPartId == carPartId && item.OrderId == orderId);
        }

        public IEnumerable<OrderContainsCarPart> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public IEnumerable<OrderContainsCarPart> GetAllFromOrder(Order order)
        {
            return _entities
                 .Include(item => item.Order)
                 .Include(item => item.CarPart)
                 .Include(item => item.CarPart.Brand)
                 .Include(item => item.CarPart.Type)
                 .Include(item => item.CarPart.Stage)
                 .Where(item => item.OrderId == order.Id);
        }

        public void Insert(OrderContainsCarPart entity)
        {
           _entities.Add(entity);
        }

        public void Update(OrderContainsCarPart entity)
        {
            _entities.Update(entity);
        }
    }
}

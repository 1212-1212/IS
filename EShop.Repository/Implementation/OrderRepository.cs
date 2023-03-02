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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Order> _entities;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<Order>();
        }

        public void Delete(Order entity)
        {
            _entities.Remove(entity);
        }

        public Order Get(Guid id)
        {
            return _entities
                .Include("OrderContainsCarParts")
                .Include("Client")
                .Include("OrderContainsCarParts")
                 .Include("OrderContainsCarParts.CarPart.Brand")
                 .Include("OrderContainsCarParts.CarPart.Type")
                 .Include("OrderContainsCarParts.CarPart.Stage")
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _entities
          .Include("OrderContainsCarParts")
                .Include("Client")
               .Include("OrderContainsCarParts")
                 .Include("OrderContainsCarParts.CarPart.Brand")
                 .Include("OrderContainsCarParts.CarPart.Type")
                 .Include("OrderContainsCarParts.CarPart.Stage")
                .AsEnumerable();
        }

        public void Insert(Order entity)
        {
            _entities.Add(entity);
        }

        public void Update(Order entity)
        {
            _entities.Update(entity);

        }
    }
}

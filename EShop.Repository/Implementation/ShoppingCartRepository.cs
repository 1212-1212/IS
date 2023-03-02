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
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private readonly DbSet<ShoppingCart> _entities;
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<ShoppingCart>();
        }

        public void Delete(ShoppingCart entity)
        {
            if(entity == null)
            {
                { throw new ArgumentNullException("entity"); }
            }
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public ShoppingCart Get(Guid shoppingCartId)
        {
            return _entities.Include(sc => sc.ShoppingCartContainsCarParts)
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart")
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Brand")
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Type")
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Stage")
                .Include(sc => sc.Client)
                .FirstOrDefault(sc => sc.Id == shoppingCartId);
        }

        public void Insert(ShoppingCart entity)
        {
            if (entity == null)
            {
                { throw new ArgumentNullException("entity"); }
            }
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Order(Client client)
        {
            throw new NotImplementedException();
        }

        public void Update(ShoppingCart entity)
        {
            if (entity == null)
            {
                { throw new ArgumentNullException("entity"); }
            }
            _entities.Update(entity);
            _context.SaveChanges();
        }
    }
}

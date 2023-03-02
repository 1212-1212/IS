using Eshop.Repository.Data;
using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementation
{
    public class ShoppingCartContainsCarPartRepository : IShoppingCartContainsCarPartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ShoppingCartContainsCarPart> _entities;

        public ShoppingCartContainsCarPartRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<ShoppingCartContainsCarPart>();
        }

        public void Delete(ShoppingCartContainsCarPart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public ShoppingCartContainsCarPart Get(Guid? carPartId, Guid? shoppingCartId)
        {
            return _entities
                .Include(e => e.CarPart.Brand)
                .Include(e => e.CarPart.Type)
                .Include(e => e.CarPart.Stage)
                .Include(e => e.CarPart.OrderContainsCarParts)
                .Include(e => e.CarPart.ShoppingCartContainsCarParts)
                .Include(e => e.CarPart.CarPartInStockAtDealerships)
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Brand")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Type")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Stage")
                .Include(e => e.ShoppingCart.Client)
                .SingleOrDefault(e => e.CarPartId == carPartId && e.ShoppingCartId == shoppingCartId);

        }

        public IEnumerable<ShoppingCartContainsCarPart> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public IEnumerable<ShoppingCartContainsCarPart> GetAllFromShoppingCart(ShoppingCart shoppingCart)
        {
            return _entities
                .Include(e => e.CarPart.Brand)
                .Include(e => e.CarPart.Type)
                .Include(e => e.CarPart.Stage)
                .Include(e => e.CarPart.OrderContainsCarParts)
                .Include(e => e.CarPart.ShoppingCartContainsCarParts)
                .Include(e => e.CarPart.CarPartInStockAtDealerships)
                .Include(e => e.ShoppingCart.ShoppingCartContainsCarParts)
                .Include(e => e.ShoppingCart.Client)
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart")
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Brand")
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Type")
                 .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Stage")
                .Where(e => e.ShoppingCartId == shoppingCart.Id);
        }

        public void Insert(ShoppingCartContainsCarPart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            _context.SaveChanges();

        }

        public void Update(ShoppingCartContainsCarPart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Update(entity);
            _context.SaveChanges();
        }
    }
}

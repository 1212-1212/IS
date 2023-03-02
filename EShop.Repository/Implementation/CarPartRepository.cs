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
    public class CarPartRepository : ICarPartRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<CarPart> entities;

        public CarPartRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<CarPart>();
        }

        public ShoppingCart AddToShoppingCart(CarPart carPart, ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public void Create(CarPart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(CarPart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public CarPart Get(Guid? id)
        {
            return entities
                .Include(e => e.Brand)
                .Include(e => e.Type)
                .Include(e => e.Stage)
                .Include(e => e.ShoppingCartContainsCarParts)
                .Include(e => e.OrderContainsCarParts)
                .Include(e => e.CarPartInStockAtDealerships)
                .SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<CarPart> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Update(CarPart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _context.SaveChanges();
        }
    }
}

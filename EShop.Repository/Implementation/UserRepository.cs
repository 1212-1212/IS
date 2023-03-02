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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Client> _entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<Client>();
        }

        public void Delete(Client client)
        {
           if(client == null)
            { throw new ArgumentNullException("client"); }
           _entities.Remove(client);
            _context.SaveChanges();
        }

        public Client Get(string id)
        {
            Console.WriteLine("CLIENT ID " + id);
            return _entities.Include(c => c.ShoppingCart)
                .Include(c => c.ShoppingCart.ShoppingCartContainsCarParts)
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Brand")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Type")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Stage")
                .SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
           return _entities.ToList();
        }

        public Client GetByEmail(string email)
        {
            return _entities.Include(c => c.ShoppingCart)
                .Include(c => c.ShoppingCart.ShoppingCartContainsCarParts)
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Brand")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Type")
                .Include("ShoppingCart.ShoppingCartContainsCarParts.CarPart.Stage")
                .SingleOrDefault(c => c.Email == email);
        }

        public void Insert(Client client)
        {
            if(client == null)
            {
                throw new ArgumentNullException("client");
            }
            _entities.Add(client);
            _context.SaveChanges();
        }

        public void Update(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            _entities.Update(client);
            _context.SaveChanges();
        }
    }
}

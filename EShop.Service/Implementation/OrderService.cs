using Eshop.Repository.Data;
using EShop.Domain.Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;   

        public OrderService(IOrderRepository repository)
        {
            _orderRepository = repository;
        }
        public void Create(Order order)
        {
            _orderRepository.Insert(order);
        }

        public void Delete(Order order)
        {
          _orderRepository.Delete(order);
        }

        public Order Get(Guid id)
        {
           return _orderRepository.Get(id);
        }

        public List<Order> GetAll()
        {
           return _orderRepository.GetAll().ToList();
        }

        public IEnumerable<Order> GetAllBetweenDates(DateTime from, DateTime to)
        {
           return GetAll().Where(item => item.OrderDate.CompareTo(from) >= 0 && item.OrderDate.CompareTo(to) <= 0);
        }

        public List<Order> GetAllFromUser(Client client)
        {
            return GetAll().Where(item => item.Client.Email == client.Email).ToList();
        }

        public void Update(Order order)
        {
             _orderRepository.Update(order);
        }
    }
}

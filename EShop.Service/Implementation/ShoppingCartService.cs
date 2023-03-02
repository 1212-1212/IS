using EShop.Domain.Models;
using EShop.Repository.Implementation;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using MailKit.Search;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _repository;
        private readonly IShoppingCartContainsCarPartRepository _shoppingCartContainsCarPartRepository;
        private readonly IShoppingCartContainsCarPartService _shoppingCartContainsCarPartService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderContainsCarPartRepository _orderContainsCarPartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<EmailMessage> _emailMessageRepository;
        public ShoppingCartService(IShoppingCartRepository repository, IShoppingCartContainsCarPartRepository shopping, IRepository<Order> orderRepository, 
            IOrderContainsCarPartRepository orderContainsCarPartRepository, IUserRepository userRepository,
            IRepository<EmailMessage> emailMessageRepository, IShoppingCartContainsCarPartService shoppingCartContainsCarPartService)
        {
            _repository = repository;
            _shoppingCartContainsCarPartRepository = shopping;
            _orderRepository = orderRepository;
            _orderContainsCarPartRepository = orderContainsCarPartRepository;
            _userRepository = userRepository;
            _emailMessageRepository = emailMessageRepository;
            _shoppingCartContainsCarPartService = shoppingCartContainsCarPartService;
        }

        public void DeleteItem(ShoppingCart shoppingCart, CarPart carPart)
        {
            var itemInShoppingCart = _shoppingCartContainsCarPartRepository.Get(carPart.Id, shoppingCart.Id);
            _shoppingCartContainsCarPartRepository.Delete(itemInShoppingCart);
            _repository.Update(shoppingCart);

        }

        public ShoppingCart Get(Guid shoppingCartId)
        {
            return _repository.Get(shoppingCartId);
        }

        public Order Order(Client client)
        {
            var shoppingCart = client.ShoppingCart;


            EmailMessage mail = new EmailMessage();
            mail.MailTo = client.Email;
            mail.Subject = "Order sucessfully created!";
            mail.Status = false;


            Order order = new Order
            {
                Id = Guid.NewGuid(),
                Client = client,
                ClientId = client.Id,
                OrderStatus = Domain.Models.Order.ORDER_STATUS.CREATED,
                OrderDate = DateTime.Now,
            };
            order.OrderContainsCarParts = _shoppingCartContainsCarPartService.toOrderContainsCarPart(shoppingCart.ShoppingCartContainsCarParts.ToList(), order);
            _orderRepository.Insert(order);

        

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(order.ToString());

            foreach(var item in order.OrderContainsCarParts.ToHashSet())
            {
                if(_orderContainsCarPartRepository.Get(item.CarPartId, item.OrderId) == null )
                    _orderContainsCarPartRepository.Insert(item);
            }

            mail.Content = stringBuilder.ToString();

            client.ShoppingCart.ShoppingCartContainsCarParts.Clear();

            _userRepository.Update(client);

            _emailMessageRepository.Insert(mail);

            return order;

        }

        public void SetQuantity(ShoppingCart shoppingCart, CarPart carPart, int quantity)
        {
            var itemInShoppingCart = _shoppingCartContainsCarPartRepository.Get(carPart.Id, shoppingCart.Id);
            if(itemInShoppingCart != null)
            {
                itemInShoppingCart.Quantity = quantity;
                _shoppingCartContainsCarPartRepository.Update(itemInShoppingCart);
            }
            _repository.Update(shoppingCart);
        }
       


    }
}

using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels
{
    public  class OrderViewModel
    {
        public OrderViewModel(List<Order>? _orders, string? _username, DateTime? _from, DateTime? _to)
        {
            allOrders = _orders.OrderByDescending(item => item.OrderDate);
            usernameSearch = _username;
            fromDate = _from;
            toDate = _to;
      
        }
        public OrderViewModel() { }
        public IEnumerable<Order>? allOrders { get; set; }
        public string? usernameSearch { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }


    }
}

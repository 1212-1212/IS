using EShop.Domain.Models;
using EShop.Domain.ViewModels;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EShop.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IOrderContainsCarPartRepository _orderContainsCarPartRepository;
        public OrdersController(IOrderService orderService, IUserService userService, IOrderContainsCarPartRepository orderContainsCarPartRepository)
        {
            _orderService = orderService;
            _userService = userService;
            _orderContainsCarPartRepository = orderContainsCarPartRepository;

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index(string username, DateTime from, DateTime to)
        {
            ViewBag.Emails = _userService.GetAll();
           
            var orders = _orderService.GetAll();

            var user = _userService.GetByEmail(username);

            if (!string.IsNullOrEmpty(username))
            {
               orders = _orderService.GetAllFromUser(user);
             
            }

            
             if(to == DateTime.MinValue)
            {
                to = DateTime.MaxValue;
            }
            orders = orders.AsEnumerable().Intersect(_orderService.GetAllBetweenDates(from, to).ToList()).ToList();




            OrderViewModel viewModel = new OrderViewModel
            {
                allOrders = orders,
                usernameSearch = username,
                fromDate = from,
                toDate = to,
            };

        
            return View(viewModel);
        }

        public IActionResult CreateInvoice(Guid orderId)
        {
            var order = this._orderService.Get(orderId);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            order.Client = _userService.Get(order.ClientId);

            order.OrderContainsCarParts = _orderContainsCarPartRepository.GetAllFromOrder(order).ToList();

            document.Content.Replace("{{OrderNumber}}", order.Id.ToString());
            document.Content.Replace("{{Email}}", order.Client.Email);

            StringBuilder sb = new StringBuilder();


            sb.Append(order.ToString());
            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", order.totalPrice().ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "Invoice.pdf");
        }
    }
}

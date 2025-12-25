using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;

        public OrderController(
            IOrderService orderService,
            ICustomerService customerService,
            ICarService carService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _carService = carService;
        }

        // GET: /Order
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        // GET: /Order/Details/5
        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderWithDetails(id);
            if (order == null) return NotFound();

            return View(order);
        }

        // GET: /Order/Create
        public IActionResult Create()
        {
            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.Cars = _carService.GetAllCars();
            return View();
        }

        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order, int[] CarIds, int[] Quantities)
        {
            if (!ModelState.IsValid || CarIds == null || CarIds.Length == 0)
            {
                ViewBag.Customers = _customerService.GetAllCustomers();
                ViewBag.Cars = _carService.GetAllCars();
                return View(order);
            }

            // Create order details
            decimal total = 0;
            for (int i = 0; i < CarIds.Length; i++)
            {
                var car = _carService.GetCarById(CarIds[i]);
                if (car != null)
                {
                    int quantity = i < Quantities.Length ? Quantities[i] : 1;
                    var orderDetail = new OrderDetail
                    {
                        CarId = CarIds[i],
                        Quantity = quantity,
                        Price = car.Price
                    };
                    order.OrderDetails.Add(orderDetail);
                    total += car.Price * quantity;
                }
            }

            order.Total = total;
            order.OrderDate = DateTime.Now;

            _orderService.CreateOrder(order);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Order/Delete/5
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

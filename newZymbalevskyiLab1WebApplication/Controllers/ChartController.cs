using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using newZymbalevskyiLab1WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newZymbalevskyiLab1WebApplication;

namespace newZymbalevskyiLab1WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DblibraryContext _context;
        public ChartController(DblibraryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var types = _context.Categories.Include(a => a.Products).ToList();
            List<object> categ = new List<object>();
            categ.Add(new[] { "Type of a product", "Quantity of products" });
            foreach (var type in types)
            {
                categ.Add(new object[] { type.Name, type.Products.Count() });
            }
            return new JsonResult(categ);
        }

        [HttpGet("JsonDataOne")]
        public JsonResult JsonDataOne()
        {
            var customers = _context.Customers.Include(a => a.Orders).ToList();
            List<object> cust = new List<object>();
            cust.Add(new[] { "Customer", "Quantity of orders" });
            foreach (var customer in customers)
            {
                cust.Add(new object[] { $"{customer.FirstName} {customer.LastName}", customer.Orders.Count() });
            }
            return new JsonResult(cust);
        }
    }
}
using Baker_Back.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baker_Back.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _context { get;  }
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Where(x=>x.IsDeleted==false).Take(9).ToList());
        }
    }
}

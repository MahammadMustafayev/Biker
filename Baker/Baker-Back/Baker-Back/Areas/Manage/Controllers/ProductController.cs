using Baker_Back.DAL;
using Baker_Back.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Baker_Back.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get;  }
        private IWebHostEnvironment _env { get;  }
        public ProductController(AppDbContext context,IWebHostEnvironment env)
        {

            _env = env;
            _context = context;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                string fileName = product.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets", "image", "product", fileName);
                using(FileStream fs = new FileStream(path, FileMode.Create))
                {
                    product.Photo.CopyTo(fs);
                }
                product.Image = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index));
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            Product product=  _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            return View(nameof(Index));
        }

        // POST: ProductController/Delete/5
        
    }
}

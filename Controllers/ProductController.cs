using AppAspnetMvc.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AppAspnetMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ContextMongoDb _context;

        public ProductController(ContextMongoDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // retornando todos os produtos para a view index
            return View(await _context.Products.Find(p => true).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Find(p => p.id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                await _context.Products.InsertOneAsync(product);

                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Find(p => p.id == id).FirstOrDefaultAsync();
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Product product)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                // atualizando um documento no banco de dados
                await _context.Products.ReplaceOneAsync(p => p.id == id, product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            // encontrando o produto no banco
            var product = await _context.Products.Find(p => p.id == id).FirstOrDefaultAsync();

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // removendo um produto do banco
            await _context.Products.DeleteOneAsync(p => p.id == id);
            return RedirectToAction("Index");
        }

       
    }
}

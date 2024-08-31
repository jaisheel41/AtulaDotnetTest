using Microsoft.AspNetCore.Mvc;
using AtulaDotnetTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult GetProducts()
    {
        var products = _context.Products
            .Select(p => new
            {
                id = p.Id,
                name = p.Name
            })
            .ToList();

        return Json(new { data = products });
    }

    // Index method to display all products
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Categories).ToListAsync();
        return View(products);
    }

    // GET: Create Product
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return PartialView("_Create"); // Use a partial view for the modal
    }

    // POST: Create Product
    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Sku = productDto.Sku,
                Name = productDto.Name
            };

            foreach (var categoryId in productDto.CategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category != null)
                {
                    product.Categories.Add(category);
                }
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Product created successfully" });
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return Json(new { success = false, message = "Validation failed", errors });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var productDto = new ProductDTO
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            CategoryIds = product.Categories.Select(c => c.Id).ToList()
        };

        ViewBag.Categories = await _context.Categories.ToListAsync();

        return PartialView("_Edit", productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == productDto.Id);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            // Update basic product properties
            product.Sku = productDto.Sku;
            product.Name = productDto.Name;

            // Update categories
            product.Categories.Clear();
            foreach (var categoryId in productDto.CategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category != null)
                {
                    product.Categories.Add(category);
                }
            }

            _context.Update(product);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Product updated successfully" });
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return Json(new { success = false, message = "Validation failed", errors });
    }

    // GET: Load the delete confirmation modal
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return Json(new { success = false, message = "Product not found" });
        }

        var productDto = new ProductDTO
        {
            Id = product.Id,
            Name = product.Name
        };

        return PartialView("_Delete", productDto);
    }

    // POST: Handle the delete operation
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return Json(new { success = false, message = "Product not found" });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Product deleted successfully" });
    }
}

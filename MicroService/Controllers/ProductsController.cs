using MicroService.Data;
using MicroService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Route("GetProducts")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseModel>> GetProducts()
        {
            var products = _dbContext.Products.Include(p => p.Category).ToList();
            var productsWithCategoryNames = products.Select(p => new ProductResponseModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            }).ToList();
            return Ok(productsWithCategoryNames);
        }
        [Route("GetProductsByCategory")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseModel>> GetProductsByCategory(int categoryId)
        {
            var products = _dbContext.Products.Include(p => p.Category)
                                             .Where(p => p.CategoryId == categoryId)
                                             .ToList();

            var productsWithCategoryNames = products.Select(p => new ProductResponseModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            }).ToList();

            return Ok(productsWithCategoryNames);
        }
        [Route("GetProductsByCategoryId")]
        [HttpPost]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int? categoryId)
        {
            if (categoryId != null)
            {
                var products = _dbContext.Products
               .Where(p => p.CategoryId == categoryId)
               .ToList();

                return Ok(products);
            }
            else
            {
                var products = _dbContext.Products.ToList();
                return Ok(products);
            }
           
        }
    }
}

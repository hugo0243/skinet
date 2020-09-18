using System.Collections.Generic;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            List<Product> result = await _storeContext.Products.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _storeContext.Products.FindAsync(id);
        }
    }
}
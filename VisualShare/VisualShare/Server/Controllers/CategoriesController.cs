using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VisualShare.Shared;

namespace VisualShare.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public CategoriesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }
        
        [HttpPost]
        public async Task CreateCategory(string name)
        {
            _dbContext.Categories.Add(new Category(name));
            await _dbContext.SaveChangesAsync();
        }
    }
}
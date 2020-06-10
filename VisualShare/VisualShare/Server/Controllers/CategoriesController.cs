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
        public List<string> GetCategories()
        {
            return _dbContext.Categories.Select(category => category.Name).ToList();
        }
        
        [HttpPost]
        public async Task CreateCategory([FromBody] string name)
        {
            _dbContext.Categories.Add(new Category(name));
            await _dbContext.SaveChangesAsync();
        }
    }
}
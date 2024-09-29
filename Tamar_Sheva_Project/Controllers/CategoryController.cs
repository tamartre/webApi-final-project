
using Entities;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Paddings;
using Service;
using System.Linq.Expressions;
using Tamar_Sheva_Project;

namespace Repository.Controllers
{ 
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        
        private ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<Catgory>> Get()
        {

            List<Catgory> Resualt = await _categoryService.Get();
            if (Resualt != null)
            {
                return Ok(Resualt);
            }

            return BadRequest();
        }

    }

    }





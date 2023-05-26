using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;

namespace SrtnQuizApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Category.AddAsync(category);
             _unitOfWork.Save();

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || category.Id != id)
            {
                return BadRequest();
            }

            var existingCategory = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = category.Name;
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

             _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}

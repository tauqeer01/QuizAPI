using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;

namespace SrtnQuizApi.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _unitOfWork.Subjects.GetAllAsync();
            return Ok(subjects);
            
        }

        [HttpGet("id", Name = "GetSubjects")]
        public async Task<IActionResult> GetSubjects(int id)
        {
            var subject = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(x => x.Id == id);   
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubjects([FromBody] Subjects subjects)
        {
            if (subjects == null)
            {
                return BadRequest();
            }

            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == subjects.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError(nameof(Subjects.CategoryId), "Invalid category id");
                return BadRequest(ModelState);
            }
            await _unitOfWork.Subjects.AddAsync(subjects);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetSubjects), new { id = subjects.Id }, subjects);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateSubjects(int id , [FromBody] Subjects subjects)
        {
           if(id != subjects.Id)
            {
                return BadRequest();
            }
            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == subjects.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError(nameof(Subjects.CategoryId), "Invalid category id");
                return BadRequest(ModelState);
            }
             _unitOfWork.Subjects.Update(subjects);
            _unitOfWork.Save();
            return Ok();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSubjects(int id)
        {
            var subjects = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(s => s.Id == id);
            if(subjects== null)
            {
                return NotFound();
            }
             _unitOfWork.Subjects.Remove(subjects);
            _unitOfWork.Save();

            return Ok();
        }
    }
}

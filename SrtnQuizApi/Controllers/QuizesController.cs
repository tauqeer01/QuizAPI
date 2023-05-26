using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;

namespace SrtnQuizApi.Controllers
{
    [Route("api/quizes")]
    [ApiController]
    public class QuizesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizes()
        {
            var quizes = await _unitOfWork.Quizes.GetAllAsync();
            return Ok(quizes);
        }

        [HttpGet("{id}", Name = "GetQuiz")]
        public async Task<IActionResult> GetQuiz(int id)
        {
            var quiz = await _unitOfWork.Quizes.GetFirstOrDefaultAsync(x => x.Id == id);
            return Ok(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] Quiz quizes)
        {
            if(quizes == null)
            {
                return BadRequest();
            }

            var subject = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(s => s.Id == quizes.SubjectId);
            if(subject== null)
            {
                ModelState.AddModelError(nameof(Subjects.CategoryId), "Invalid category id");
                return BadRequest(ModelState);
            }

            await _unitOfWork.Quizes.AddAsync(quizes);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetQuiz), new { id = quizes.Id }, quizes);
        }

        [HttpPut("{id}", Name ="UpdateQuiz")]

        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] Quiz quizes)
        {
            if(id !=quizes.Id)
            {
                return BadRequest();
            }

            var subject= await _unitOfWork.Subjects.GetFirstOrDefaultAsync(s => s.Id == quizes.SubjectId);
            if(subject==null)
            {
                return BadRequest();
            }

            var existingQuizes = await _unitOfWork.Quizes.GetFirstOrDefaultAsync(q => q.Id == id);
            if(existingQuizes==null)
            {
                return NotFound();
            }

            existingQuizes.SubjectId = id;
            existingQuizes.Name = quizes.Name;
            existingQuizes.PassScore= quizes.PassScore;
             _unitOfWork.Quizes.Update(existingQuizes);
             _unitOfWork.Save();
            return CreatedAtRoute("GetQuiz", new { id = quizes.Id }, quizes);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteQuiz(int Id)
        {
            var quiz = await _unitOfWork.Quizes.GetFirstOrDefaultAsync(q =>q.Id == Id);
            if(quiz == null)
            {
                return BadRequest();
            }
            _unitOfWork.Quizes.Remove(quiz);
            _unitOfWork.Save();
             return Ok();
        }
    }
}

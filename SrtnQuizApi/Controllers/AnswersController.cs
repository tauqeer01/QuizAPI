using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;

namespace SrtnQuizApi.Controllers
{
    [Route("api/answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswers()
        {
            var answers = await _unitOfWork.Answer.GetAllAsync();
            return Ok(answers);
        }

        [HttpGet("{Id}" , Name ="GetAnswer")]

        public async Task<IActionResult> GetAnswer(int Id)
        {
            var answer = await _unitOfWork.Answer.GetFirstOrDefaultAsync(a => a.Id == Id);
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] Answer answer)
        {
            if(answer == null)
            {
                return BadRequest();
            }
            var  QuestionId = await _unitOfWork.Question.GetFirstOrDefaultAsync(q => q.Id == answer.QuestionId);
            if (QuestionId == null)
            {
                return BadRequest();
            }
            await _unitOfWork.Answer.AddAsync(answer);
            _unitOfWork.Save();

            return CreatedAtRoute("GetAnswer", new { id = answer.Id }, answer);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAnswer(int Id, [FromBody] Answer answer)
        {
            if (answer == null || answer.Id != Id)
            {
                return BadRequest();
            }

            var questionId = await _unitOfWork.Question.GetFirstOrDefaultAsync(q => q.Id == answer.QuestionId);
            if(questionId == null)
            {
                return BadRequest();
            }

            var ExistingAnswer = await _unitOfWork.Answer.GetFirstOrDefaultAsync(a => a.Id == Id);
            if(ExistingAnswer == null)
            {
                return BadRequest();
            }
            
            ExistingAnswer.Text = answer.Text;
            ExistingAnswer.QuestionId = answer.QuestionId;
            ExistingAnswer.IsCorrect = answer.IsCorrect;
            _unitOfWork.Answer.Update(ExistingAnswer);
            _unitOfWork.Save();

            return Ok("Updated successfully");
        }

        [HttpDelete("{Id}")]

        public async Task<IActionResult> DeleteAnswer(int Id)
        {
            var answerId = await _unitOfWork.Answer.GetFirstOrDefaultAsync(a =>a.Id == Id); 
            if (answerId == null)
            {
                return BadRequest();
            }

            _unitOfWork.Answer.Remove(answerId);
            _unitOfWork.Save();
            return Ok("Deleted successfully");
        }
    }
}

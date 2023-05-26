using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;
using System.Formats.Asn1;

namespace SrtnQuizApi.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _unitOfWork.Question.GetAllAsync();
            return Ok(questions);
        }

        [HttpGet("{Id}" , Name ="GetQuestion")]
        public async Task<IActionResult> GetQuestion(int Id)
        {
            var question = await _unitOfWork.Question.GetFirstOrDefaultAsync(x => x.Id == Id);
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] Question questions)
        {
            if (questions == null)
            {
                return BadRequest();
            }
            var quiz = await _unitOfWork.Quizes.GetFirstOrDefaultAsync(q => q.Id == questions.QuizId);
            if (quiz == null)
            {
                return BadRequest();
            }
            var subjects = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(s => s.Id == questions.SubjectId);
            if (subjects == null)
            {
                return BadRequest();
            }
            
            await _unitOfWork.Question.AddAsync(questions);
            _unitOfWork.Save();

            return CreatedAtRoute("GetQuestion", new { id = questions.Id }, questions);
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateQuestion(int Id, [FromBody] Question question)
        {
             if(question == null || question.Id != Id)
             {
                return BadRequest();
             }
            var subject = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(s => s.Id == question.SubjectId);
            if(subject == null)
            {
                return BadRequest();
            }

            var quiz = await _unitOfWork.Quizes.GetFirstOrDefaultAsync(q => q.Id == question.QuizId);
            if(quiz == null)
            {
                return BadRequest();
            }
            var existingQuestion = await _unitOfWork.Question.GetFirstOrDefaultAsync(q => q.Id == Id);
            if (existingQuestion == null)
            {
                return BadRequest();
            }

            existingQuestion.Text = question.Text;
            existingQuestion.DifficultyLevel = question.DifficultyLevel;
            existingQuestion.QuizId = question.QuizId;  
            existingQuestion.SubjectId = question.SubjectId;
            _unitOfWork.Question.Update(existingQuestion);
            _unitOfWork.Save();
            return CreatedAtRoute("GetQuestion", new { id = question.Id }, question);
        }


        [HttpDelete("{Id}")]

        public async Task<IActionResult> DeleteQuestion(int Id)
        {
           var question = await _unitOfWork.Question.GetFirstOrDefaultAsync(q => q.Id == Id);
            if(question == null)
            {
                return BadRequest();
            }

            _unitOfWork.Question.Remove(question);
            _unitOfWork.Save();
            return Ok("Deleted");
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrtnQuizApi.DataAccess.Repository.IRepository;

namespace SrtnQuizApi.Controllers
{
    [Route("api/quizAttempt")]
    [ApiController]
    public class QuizAttemptController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuizAttemptController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizAttempts()
        {
            var quizAttempted = await _unitOfWork.QuizAttempt.GetAllAsync();
            return Ok(quizAttempted);
        }
    }
}

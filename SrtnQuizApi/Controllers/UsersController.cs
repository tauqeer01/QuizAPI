using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SrtnQuizApi.DataAccess.Repository.IRepository;
using SrtnQuizApi.Models;

namespace SrtnQuizApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _unitOfWork.User.GetAllAsync();
            return Ok(user);
        }
        [HttpGet("{Id}", Name ="GetUser")]
        public async Task<IActionResult> GetUser(string Id)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser applicationUser)
        {
            if (applicationUser == null)
            {
                return BadRequest();
            }

            var appuser = new ApplicationUser 
            {   
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
            };

          
            var  result = await _userManager.CreateAsync(appuser, applicationUser.PasswordHash);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _signInManager.SignInAsync(appuser, false);

            return CreatedAtRoute("GetUser", new { id = appuser.Id }, appuser);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(string Id , [FromBody] ApplicationUser applicationUser)
        {
            if(applicationUser == null)
            {
                return BadRequest();
            }
            var existingUser = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Id == Id);
            if (existingUser == null)
            {
                return BadRequest();
            }
            existingUser.FirstName = applicationUser.FirstName;
            existingUser.LastName = applicationUser.LastName;
            existingUser.Email = applicationUser.Email;
            existingUser.PasswordHash = applicationUser.PasswordHash;
            existingUser.UserName = applicationUser.UserName;
             _unitOfWork.User.Update(existingUser);
             _unitOfWork.Save();
             return CreatedAtRoute("GetUser" , new {id = existingUser.Id}, existingUser);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            if(Id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                return BadRequest();
            }

            _unitOfWork.User.Remove(user);
            _unitOfWork.Save();

            return Ok("User Deleted Successfully");
        }
       

    }
}

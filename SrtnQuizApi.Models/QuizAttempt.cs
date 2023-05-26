using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.Models
{
    public class QuizAttempt
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int Score { get; set; }

        // Foreign keys
        public string UserId { get; set; }
        public int QuizId { get; set; }

        // Navigation properties
        //public ApplicationUser User { get; set; }
        //public Quiz Quiz { get; set; }

        // Status property
        //public string Status
        //{
        //    get { return Score >= Quiz.PassScore ? "Pass" : "Fail"; }
        //}
    }
}

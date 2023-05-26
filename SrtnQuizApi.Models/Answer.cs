using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        // Foreign key
        public int QuestionId { get; set; }

        // Navigation property
        //public Question Question { get; set; }
    }
}

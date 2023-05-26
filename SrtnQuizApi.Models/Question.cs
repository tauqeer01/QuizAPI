using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int DifficultyLevel { get; set; }
        public int SubjectId { get; set; }
        public int QuizId { get; set; }

        //public ICollection<Answer> Answers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public int PassScore { get; set; }

        //// Foreign keys
        //

        //// Navigation properties
        //public Subjects Subjects { get; set; }
        //public ICollection<Question> Questions { get; set; }
        //public ICollection<QuizAttempt> QuizAttempts { get; set; }

        //// PassScore property

    }
}

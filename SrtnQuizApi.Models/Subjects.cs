using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.Models
{
    public class Subjects
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign keys
        public int CategoryId { get; set; }

        // Navigation properties
        //public Category Category { get; set; }
       //public ICollection<Quizes> Quizzes { get; set; }
    }
}

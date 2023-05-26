
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SrtnQuizApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtnQuizApi.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Quiz>()
        //        .HasMany(q => q.Questions)
        //        .WithOne(q => q.Quizes)
        //        .HasForeignKey(q => q.QuizId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}

    }
}

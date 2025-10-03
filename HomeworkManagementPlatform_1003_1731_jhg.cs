// 代码生成时间: 2025-10-03 17:31:53
// This is a simple Homework Management Platform using C# and Entity Framework framework.

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeworkManagementPlatform
{
    // Define the Homework entity
    public class Homework
    {
        public int HomeworkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }

    // Define the Submission entity
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string StudentName { get; set; }
        public string Content { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int HomeworkId { get; set; }
        public virtual Homework Homework { get; set; }
    }

    // Define the context for Entity Framework
    public class HomeworkContext : DbContext
    {
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Your_Connection_String");
        }
    }

    // Homework Management Service
    public class HomeworkService
    {
        private readonly HomeworkContext _context;

        public HomeworkService(HomeworkContext context)
        {
            _context = context;
        }

        // Add a new homework assignment
        public async Task AddHomeworkAsync(Homework homework)
        {
            try
            {
                _context.Homeworks.Add(homework);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred adding the homework: {ex.Message}");
            }
        }

        // Get all homework assignments
        public async Task<List<Homework>> GetAllHomeworksAsync()
        {
            try
            {
                return await _context.Homeworks.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred retrieving homeworks: {ex.Message}");
            }
        }

        // Add a submission to a homework assignment
        public async Task AddSubmissionAsync(Submission submission)
        {
            try
            {
                _context.Submissions.Add(submission);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred adding the submission: {ex.Message}");
            }
        }
    }
}

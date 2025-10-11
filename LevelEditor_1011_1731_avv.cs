// 代码生成时间: 2025-10-11 17:31:06
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

// Define the namespace for the application.
namespace GameLevelEditor
{
    // Define the Level class to represent a game level.
    public class Level
    {
        public int LevelId { get; set; }
        public string Name { get; set; }
        public int? Difficulty { get; set; }
        public string Description { get; set; }
    }

    // Define the DbContext for database operations.
    public class GameContext : DbContext
    {
        public DbSet<Level> Levels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("your_connection_string_here");
        }
    }

    // Define the LevelEditor class to handle level editing operations.
    public class LevelEditor
    {
        private readonly GameContext _context;

        public LevelEditor(GameContext context)
        {
            _context = context;
        }

        // Method to add a new level to the database.
        public bool AddLevel(Level level)
        {
            try
            {
                if (level == null)
                {
                    throw new ArgumentNullException(nameof(level), "Level cannot be null.");
                }

                _context.Levels.Add(level);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log error message or handle the exception as needed.
                Console.WriteLine($"Error adding level: {ex.Message}");
                return false;
            }
        }

        // Method to update an existing level in the database.
        public bool UpdateLevel(Level level)
        {
            try
            {
                if (level == null || level.LevelId == 0)
                {
                    throw new ArgumentException("Level must have a valid ID.");
                }

                _context.Levels.Update(level);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log error message or handle the exception as needed.
                Console.WriteLine($"Error updating level: {ex.Message}");
                return false;
            }
        }

        // Method to delete a level from the database.
        public bool DeleteLevel(int levelId)
        {
            try
            {
                var level = _context.Levels.Find(levelId);
                if (level == null)
                {
                    throw new KeyNotFoundException("Level not found.");
                }

                _context.Levels.Remove(level);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log error message or handle the exception as needed.
                Console.WriteLine($"Error deleting level: {ex.Message}");
                return false;
            }
        }

        // Method to retrieve a level by its ID.
        public Level GetLevel(int levelId)
        {
            try
            {
                return _context.Levels.Find(levelId);
            }
            catch (Exception ex)
            {
                // Log error message or handle the exception as needed.
                Console.WriteLine($"Error retrieving level: {ex.Message}");
                return null;
            }
        }

        // Method to retrieve all levels from the database.
        public List<Level> GetAllLevels()
        {
            try
            {
                return _context.Levels.ToList();
            }
            catch (Exception ex)
            {
                // Log error message or handle the exception as needed.
                Console.WriteLine($"Error retrieving all levels: {ex.Message}");
                return null;
            }
        }
    }
}

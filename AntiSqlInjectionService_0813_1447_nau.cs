// 代码生成时间: 2025-08-13 14:47:01
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity.Validation;

/// <summary>
/// This class demonstrates how to prevent SQL injection using Entity Framework.
/// </summary>
public class AntiSqlInjectionService
{
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the AntiSqlInjectionService class.
    /// </summary>
    /// <param name="context">The Entity Framework context.</param>
    public AntiSqlInjectionService(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves data from the database using parameterized queries to prevent SQL injection.
    /// </summary>
    /// <typeparam name="T">The type of the entity to query.</typeparam>
    /// <param name="query">The query to execute.</param>
    /// <returns>The query result.</returns>
    public IQueryable<T> GetData<T>(System.Linq.Expressions.Expression<Func<T, bool>> query) where T : class
    {
        try
        {
            // This method uses parameterized queries provided by Entity Framework to prevent SQL injection.
            return _context.Set<T>().Where(query);
        }
        catch (DbEntityValidationException ex)
        {
            // Handle entity validation errors
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
            throw;
        }
        catch (Exception ex)
        {
            // Handle other potential exceptions
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    public void SaveChanges()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (DbEntityValidationException ex)
        {
            // Handle entity validation errors
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
            throw;
        }
        catch (Exception ex)
        {
            // Handle other potential exceptions
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
    }
}

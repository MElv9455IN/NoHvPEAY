// 代码生成时间: 2025-09-20 07:16:30
// <copyright file="DatabaseConnectionPoolManager.cs" company="YourCompany">
//     Copyright (c) YourCompany. All rights reserved.
// </copyright>

/* DatabaseConnectionPoolManager.cs
 * This class manages a pool of database connections using Entity Framework.
 * It ensures efficient and reliable database connection management.
 */

using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace YourNamespace
{
    /// <summary>
    /// A class to manage database connections in a pool.
    /// </summary>
    public class DatabaseConnectionPoolManager
    {
        private readonly ConcurrentBag<DbContext> _connectionPool;
        private readonly string _connectionString;
        private readonly string _dbContextType;

        /// <summary>
        /// Initializes a new instance of the DatabaseConnectionPoolManager class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="dbContextType">The type of the DbContext to use.</param>
        public DatabaseConnectionPoolManager(string connectionString, string dbContextType)
        {
            // Check for null arguments
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrEmpty(dbContextType))
                throw new ArgumentNullException(nameof(dbContextType));

            _connectionString = connectionString;
            _dbContextType = dbContextType;
            _connectionPool = new ConcurrentBag<DbContext>();
        }

        /// <summary>
        /// Checks out a database context from the pool or creates a new one if the pool is empty.
        /// </summary>
        /// <returns>A database context instance.</returns>
        public DbContext CheckoutContext()
        {
            try
            {
                // Try to get a context from the pool
                DbContext context;
                if (_connectionPool.TryTake(out context))
                {
                    return context;
                }
                else
                {
                    // If no context is available, create a new one
                    return CreateNewContext();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during context checkout
                throw new InvalidOperationException("Error checking out a database context.", ex);
            }
        }

        /// <summary>
        /// Returns a database context to the pool.
        /// </summary>
        /// <param name="context">The database context to return.</param>
        public void ReturnContext(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Dispose any open connections before returning the context to the pool
            context.Database.CloseConnection();

            _connectionPool.Add(context);
        }

        /// <summary>
        /// Creates a new database context instance.
        /// </summary>
        /// <returns>A new database context instance.</returns>
        private DbContext CreateNewContext()
        {
            // Use reflection to create the DbContext type dynamically
            var dbContextType = Type.GetType(_dbContextType);
            if (dbContextType == null)
                throw new InvalidOperationException($"DbContext type '{_dbContextType}' not found.");

            var context = (DbContext)Activator.CreateInstance(dbContextType, _connectionString);
            context.Database.Initialize(force: true); // Ensure the database is initialized
            return context;
        }

        /// <summary>
        /// Clears the connection pool and disposes all contexts.
        /// </summary>
        public void ClearPool()
        {
            foreach (var context in _connectionPool)
            {
                context.Dispose();
            }

            _connectionPool.Clear();
        }
    }
}
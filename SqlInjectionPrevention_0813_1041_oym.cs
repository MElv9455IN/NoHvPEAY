// 代码生成时间: 2025-08-13 10:41:26
using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;

// 数据库上下文
public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}

// 用户模型
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// 业务逻辑类
public class UserService
{
    private readonly MyDbContext _context;

    public UserService(MyDbContext context)
    {
        _context = context;
    }

    // 添加用户，防止SQL注入
    public void AddUser(string name, string email)
    {
        try
        {
            // 使用参数化查询防止SQL注入
            var user = new User { Name = name, Email = email };
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        catch (DbEntityValidationException ex)
        {
            // 处理验证异常
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
            // 处理其他异常
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
    }

    // 根据ID查询用户，防止SQL注入
    public User GetUserById(int id)
    {
        try
        {
            // 使用Entity Framework的强类型查询防止SQL注入
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
    }
}

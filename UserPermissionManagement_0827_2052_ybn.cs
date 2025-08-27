// 代码生成时间: 2025-08-27 20:52:18
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义用户类
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}

// 定义权限类
public class Permission
{
    public int PermissionId { get; set; }
    public string Name { get; set; }
    public List<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}

// 定义用户和权限的关联类
public class UserPermission
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}

// 用户权限管理数据库上下文
public class PermissionManagementDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public PermissionManagementDbContext(DbContextOptions<PermissionManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserPermission>().HasKey(up => new { up.UserId, up.PermissionId });
    }
}

// 用户权限管理系统
public class UserPermissionManagement
{
    private readonly PermissionManagementDbContext _context;

    public UserPermissionManagement(PermissionManagementDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 添加用户
    public async Task AddUserAsync(string username, string password)
    {
        var user = new User { Username = username, Password = password };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // 添加权限
    public async Task AddPermissionAsync(string name)
    {
        var permission = new Permission { Name = name };
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
    }

    // 分配权限给用户
    public async Task AssignPermissionToUserAsync(int userId, int permissionId)
    {
        var userPermission = new UserPermission { UserId = userId, PermissionId = permissionId };
        _context.UserPermissions.Add(userPermission);
        await _context.SaveChangesAsync();
    }

    // 移除用户的权限
    public async Task RemovePermissionFromUserAsync(int userId, int permissionId)
    {
        var userPermission = await _context.UserPermissions.FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);
        if (userPermission != null)
        {
            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Permission not found");
        }
    }

    // 获取用户的所有权限
    public async Task<List<Permission>> GetUserPermissionsAsync(int userId)
    {
        return await _context.UserPermissions
            .Where(up => up.UserId == userId)
            .Select(up => up.Permission)
            .ToListAsync();
    }
}

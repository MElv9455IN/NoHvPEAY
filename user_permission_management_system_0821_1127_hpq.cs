// 代码生成时间: 2025-08-21 11:27:27
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Define the User and Permission entities
public class UserPermission {
    public int UserId { get; set; }
    public User User { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}

public class User {
    public int Id { get; set; }
    public string Username { get; set; }
    public List<UserPermission> UserPermissions { get; set; }
}

public class Permission {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserPermission> UserPermissions { get; set; }
}

// Define the DbContext
public class UserPermissionDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public UserPermissionDbContext(DbContextOptions<UserPermissionDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<UserPermission>()
            .HasKey(up => new { up.UserId, up.PermissionId });

        modelBuilder.Entity<UserPermission>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(up => up.UserId);

        modelBuilder.Entity<UserPermission>()
            .HasOne(up => up.Permission)
            .WithMany(p => p.UserPermissions)
            .HasForeignKey(up => up.PermissionId);
    }
}

// Define the service for managing user permissions
public class UserPermissionService {
    private readonly UserPermissionDbContext _context;

    public UserPermissionService(UserPermissionDbContext context) {
        _context = context;
    }

    // Method to add a new user
    public async Task AddUserAsync(string username) {
        var user = new User { Username = username };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    // Method to add a new permission
    public async Task AddPermissionAsync(string permissionName) {
        var permission = new Permission { Name = permissionName };
        await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
    }

    // Method to assign a permission to a user
    public async Task AssignPermissionToUserAsync(int userId, int permissionId) {
        var userPermission = new UserPermission { UserId = userId, PermissionId = permissionId };
        await _context.UserPermissions.AddAsync(userPermission);
        await _context.SaveChangesAsync();
    }

    // Method to remove a permission from a user
    public async Task RemovePermissionFromUserAsync(int userId, int permissionId) {
        var userPermission = await _context.UserPermissions
            .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);
        if (userPermission != null) {
            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();
        } else {
            throw new InvalidOperationException("Permission not found for user.");
        }
    }

    // Method to check if a user has a specific permission
    public async Task<bool> HasPermissionAsync(int userId, int permissionId) {
        return await _context.UserPermissions
            .AnyAsync(up => up.UserId == userId && up.PermissionId == permissionId);
    }
}

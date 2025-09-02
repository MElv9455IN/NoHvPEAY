// 代码生成时间: 2025-09-03 01:29:17
using System;
using Microsoft.EntityFrameworkCore;

// 注释：定义数据上下文
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    // 注释：定义实体集合
    public DbSet<Person> People { get; set; }

    // 注释：重写OnModelCreating方法以配置模型
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 注释：配置实体属性和关系
        modelBuilder.Entity<Person>()
            .Property(p => p.Name).IsRequired()
            .HasColumnType("nvarchar(50)");
    }
}

// 注释：定义实体
public class Person
{
    // 注释：定义主键
    public int Id { get; set; }
    // 注释：定义实体属性
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}

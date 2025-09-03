// 代码生成时间: 2025-09-03 14:30:43
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义一个响应式布局设计的控制器
[ApiController]
[Route("api/[controller]"])
public class ResponsiveDesignController : ControllerBase
{
    private readonly DataContext _context;

    // 通过构造函数注入数据库上下文
    public ResponsiveDesignController(DataContext context)
    {
        _context = context;
    }

    // 获取所有响应式布局元素
    [HttpGet]
    public async Task<ActionResult<List<ResponsiveElement>>> GetAllResponsiveElements()
    {
        try
        {
            // 使用LINQ查询数据库
            var elements = await _context.ResponsiveElements.ToListAsync();
            return Ok(elements);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // 创建一个新的响应式布局元素
    [HttpPost]
    public async Task<ActionResult<ResponsiveElement>> CreateResponsiveElement(ResponsiveElement element)
    {
        try
        {
            // 添加新元素到数据库
            _context.ResponsiveElements.Add(element);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetResponsiveElement), new { id = element.Id }, element);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // 获取单个响应式布局元素
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponsiveElement>> GetResponsiveElement(int id)
    {
        try
        {
            // 根据ID查询元素
            var element = await _context.ResponsiveElements.FindAsync(id);
            if (element == null)
            {
                // 如果元素不存在，返回404错误
                return NotFound();
            }
            return Ok(element);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // 更新响应式布局元素
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponsiveElement>> UpdateResponsiveElement(int id, ResponsiveElement element)
    {
        try
        {
            // 检查元素是否存在
            var elementToUpdate = await _context.ResponsiveElements.FindAsync(id);
            if (elementToUpdate == null)
            {
                return NotFound();
            }

            // 更新元素属性
            elementToUpdate.Name = element.Name;
            elementToUpdate.Style = element.Style;
            elementToUpdate.IsActive = element.IsActive;

            // 保存更改
            await _context.SaveChangesAsync();
            return Ok(elementToUpdate);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    // 删除响应式布局元素
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponsiveElement>> DeleteResponsiveElement(int id)
    {
        try
        {
            // 根据ID查询元素
            var element = await _context.ResponsiveElements.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }

            // 从数据库中删除元素
            _context.ResponsiveElements.Remove(element);
            await _context.SaveChangesAsync();
            return Ok(element);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}

// 定义响应式布局元素的实体类
public class ResponsiveElement
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Style { get; set; }
    public bool IsActive { get; set; }
}

// 定义数据库上下文
public class DataContext : DbContext
{
    public DbSet<ResponsiveElement> ResponsiveElements { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}

// 代码生成时间: 2025-08-31 20:34:13
// ResponsiveDesignProgram.cs
// 此程序使用CSHARP和ENTITYFRAMEWORK框架实现响应式布局设计

using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResponsiveDesign
{
    // 定义数据库上下文
    public class ResponsiveDbContext : DbContext
    {
        public ResponsiveDbContext(DbContextOptions<ResponsiveDbContext> options) : base(options)
        {
        }

        public DbSet<Layout> Layouts { get; set; } // 定义响应式布局数据集
    }

    // 定义响应式布局实体
    public class Layout
    {
        public int Id { get; set; } // 主键
        public string Name { get; set; } // 布局名称
        public string Description { get; set; } // 布局描述
        public string CssStyles { get; set; } // CSS样式
    }

    // 定义布局服务
    public class LayoutService
    {
        private readonly ResponsiveDbContext _context;

        public LayoutService(ResponsiveDbContext context)
        {
            _context = context;
        }

        // 添加布局
        public async Task<Layout> AddLayoutAsync(Layout layout)
        {
            if (layout == null)
            {
                throw new ArgumentNullException(nameof(layout), "布局不能为空");
            }

            _context.Layouts.Add(layout);
            await _context.SaveChangesAsync();

            return layout;
        }

        // 获取所有布局
        public async Task<List<Layout>> GetAllLayoutsAsync()
        {
            return await _context.Layouts.ToListAsync();
        }

        // 更新布局
        public async Task<Layout> UpdateLayoutAsync(Layout layout)
        {
            if (layout == null)
            {
                throw new ArgumentNullException(nameof(layout), "布局不能为空");
            }

            _context.Layouts.Update(layout);
            await _context.SaveChangesAsync();

            return layout;
        }

        // 删除布局
        public async Task<int> DeleteLayoutAsync(int id)
        {
            var layout = await _context.Layouts.FindAsync(id);
            if (layout == null)
            {
                throw new KeyNotFoundException("布局不存在");
            }

            _context.Layouts.Remove(layout);
            return await _context.SaveChangesAsync();
        }

        // 查找布局
        public async Task<Layout> FindLayoutAsync(int id)
        {
            return await _context.Layouts.FindAsync(id);
        }
    }

    // 定义响应式布局控制器
    public class LayoutController : ControllerBase
    {
        private readonly LayoutService _layoutService;

        public LayoutController(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        // GET: /layout
        [HttpGet]
        public async Task<ActionResult<List<Layout>>> GetLayouts()
        {
            try
            {
                var layouts = await _layoutService.GetAllLayoutsAsync();
                return Ok(layouts);
            }
            catch (Exception ex)
            {
                // 处理异常
                return BadRequest(ex.Message);
            }
        }

        // POST: /layout
        [HttpPost]
        public async Task<ActionResult<Layout>> CreateLayout(Layout layout)
        {
            try
            {
                var createdLayout = await _layoutService.AddLayoutAsync(layout);
                return CreatedAtAction(nameof(GetLayouts), new { id = createdLayout.Id }, createdLayout);
            }
            catch (Exception ex)
            {
                // 处理异常
                return BadRequest(ex.Message);
            }
        }

        // PUT: /layout/{id}
        [HttpPut]" + "{" + "id" + ":" + "{id}" + "}
        public async Task<ActionResult<Layout>> UpdateLayout(int id, Layout layout)
        {
            try
            {
                var existingLayout = await _layoutService.FindLayoutAsync(id);
                if (existingLayout == null)
                {
                    return NotFound($"布局{id}不存在");
                }

                existingLayout.Name = layout.Name;
                existingLayout.Description = layout.Description;
                existingLayout.CssStyles = layout.CssStyles;

                var updatedLayout = await _layoutService.UpdateLayoutAsync(existingLayout);
                return Ok(updatedLayout);
            }
            catch (Exception ex)
            {
                // 处理异常
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /layout/{id}
        [HttpDelete]" + "{" + "id" + ":" + "{id}" + "}
        public async Task<ActionResult<int>> DeleteLayout(int id)
        {
            try
            {
                var deleted = await _layoutService.DeleteLayoutAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                // 处理异常
                return BadRequest(ex.Message);
            }
        }
    }
}

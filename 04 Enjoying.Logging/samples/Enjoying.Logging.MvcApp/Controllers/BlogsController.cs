using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Enjoying.Logging.MvcApp.Models;

namespace Enjoying.Logging.MvcApp.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return Ok(blogs);
        }

        public async Task<IActionResult> Post([FromBody] Blog blog)
        {
            if (blog == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return CreatedAtRoute("Get", new { id = blog.BlogId }, blog);
            }
        }

        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }
        
    }
}

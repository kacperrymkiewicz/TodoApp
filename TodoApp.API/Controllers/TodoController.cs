using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Models;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.OrderBy(t => t.Id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var newItem = new TodoItem
            {
                Title = todoItem.Title,
                Description = todoItem.Description,
                IsCompleted = false
            };

            _context.TodoItems.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItems), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ToggleTodoItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();
            item.IsCompleted = !item.IsCompleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

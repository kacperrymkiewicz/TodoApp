using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Dtos;
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
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _context.TodoItems
                .OrderByDescending(t => t.Id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(todoItems);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItemCreateDto newTodoItem)
        {
            var newItem = new TodoItem
            {
                Title = newTodoItem.Title,
                Description = newTodoItem.Description,
                IsCompleted = false
            };

            _context.TodoItems.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItems), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem([FromRoute] int id, [FromBody] TodoItemUpdateDto updatedTodoItem)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
                return NotFound();

            todoItem.Title = updatedTodoItem.Title;
            todoItem.Description = updatedTodoItem.Description;
            todoItem.IsCompleted = updatedTodoItem.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(todoItem);
        }
    }
}

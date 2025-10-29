using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Controllers;
using TodoApp.API.Data;
using TodoApp.API.Dtos;
using TodoApp.API.Models;

namespace TodoApp.API.Tests
{
    [TestFixture]
    public class TodoControllerTests
    {
        private DbContextOptions<TodoContext> _dbOptions;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Test]
        public async Task GetTodoItems_WhenItemsExist_ReturnsOkAndListOfItems()
        {
            // ARRANGE
            await using (var context = new TodoContext(_dbOptions))
            {
                context.TodoItems.Add(new TodoItem { Title = "Test 1", IsCompleted = false });
                context.TodoItems.Add(new TodoItem { Title = "Test 2", IsCompleted = true });
                await context.SaveChangesAsync();
            }

            await using (var context = new TodoContext(_dbOptions))
            {
                var controller = new TodoController(context);

                // ACT
                var actionResult = await controller.GetTodoItems();

                // ASSERT
                Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());
                var okResult = actionResult.Result as OkObjectResult;

                Assert.That(okResult.Value, Is.Not.Null);
                Assert.That(okResult.Value, Is.AssignableTo<IEnumerable<TodoItem>>());

                var items = (IEnumerable<TodoItem>)okResult.Value;
                Assert.That(items.Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public async Task PostTodoItem_WithValidDto_AddsItemToDatabase()
        {
            // ARRANGE
            var dto = new TodoItemCreateDto
            {
                Title = "New test item",
                Description = "Test description"
            };

            await using (var context = new TodoContext(_dbOptions))
            {
                var controller = new TodoController(context);

                // ACT
                var actionResult = await controller.PostTodoItem(dto);

                // ASSERT
                Assert.That(actionResult.Result, Is.TypeOf<CreatedAtActionResult>());
                var createdResult = actionResult.Result as CreatedAtActionResult;

                Assert.That(createdResult.Value, Is.TypeOf<TodoItem>());
                var returnedItem = createdResult.Value as TodoItem;

                Assert.That(returnedItem.Title, Is.EqualTo(dto.Title));
                Assert.That(returnedItem.IsCompleted, Is.False);

                await using (var verifyContext = new TodoContext(_dbOptions))
                {
                    var itemInDb = await verifyContext.TodoItems.FindAsync(returnedItem.Id);
                    Assert.That(itemInDb, Is.Not.Null);
                    Assert.That(itemInDb.Title, Is.EqualTo(dto.Title));
                }
            }
        }
    }
}
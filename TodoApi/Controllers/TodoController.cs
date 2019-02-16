using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            if (!_context.TodoItems.Any())
            {

                _context.TodoItems.Add(new TodoItem {Name = "Item1"});
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.SaveChanges();
            }
        }

        // GET : api/ Todo

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST : api/Todo
        /// <summary>
        /// this is a to delete a specific item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        //PUT : api/Todo 
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttodoItem(long id, TodoItem modifiedItem)
        {
            if (id != modifiedItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(modifiedItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // delete : api/Todo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item==null)
            {
                return BadRequest();
            }

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }






        //// GET: api/<controller>
        // [HttpGet]
        //    public IEnumerable<string> Get()
        //    {
        //        return new string[] { "value1", "value2" };  
        //    }

        //    // GET api/<controller>/5
        //    [HttpGet("{id}")]
        //    public string Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST api/<controller>
        //    [HttpPost]
        //    public void Post([FromBody]string value)
        //    {
        //    }

        //    // PUT api/<controller>/5
        //    [HttpPut("{id}")]
        //    public void Put(int id, [FromBody]string value)
        //    {
        //    }

        //    // DELETE api/<controller>/5
        //    [HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //    }
    }
}

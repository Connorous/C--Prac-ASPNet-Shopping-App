using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext databaseContex;

        public AdminController(ApplicationDbContext databaseContex)
        {
            this.databaseContex = databaseContex;
        }

        public void Index()
        {
            //return View();
        }

        //Items CRUD operations

        [HttpGet("Item/")]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            return await databaseContex.Items.ToListAsync();
        }


        [HttpGet("Item/{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await databaseContex.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }



        [HttpPut("Item/{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            databaseContex.Entry(item).State = EntityState.Modified;

            try
            {
                await databaseContex.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        [HttpPost("Item/")]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            databaseContex.Items.Add(item);
            await databaseContex.SaveChangesAsync();


            return CreatedAtAction(nameof(item), new { id = item.Id }, item);
        }


        [HttpDelete("Item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await databaseContex.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            databaseContex.Items.Remove(item);
            await databaseContex.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return databaseContex.Items.Any(e => e.Id == id);
        }
    }
}

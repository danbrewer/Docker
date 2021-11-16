using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Item>> Get() =>
            _dbContext.Items.ToList<Item>();

        [HttpGet("{id}", Name = "GetItem")]
        public ActionResult<Item> GetItem(string id)
        {
            var result = _dbContext.Items.Find(id); // Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public ActionResult<Item> AddItem(ItemDto itemDto)
        {
            var item = new Item
            {
                Description = itemDto.Description,
                Name = itemDto.Name
            };
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtRoute(
                routeName: "GetItem",
                routeValues: new { id = item.Id },
                value: item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(string id, ItemDto itemDto)
        {
            var item = _dbContext.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = itemDto.Name;
            item.Description = itemDto.Description;

            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var itemToFind = _dbContext.Items.Find(id);

            if (itemToFind == null)
            {
                return NotFound();
            }

            _dbContext.Remove(itemToFind);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}

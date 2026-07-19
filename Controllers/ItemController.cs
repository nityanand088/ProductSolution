using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSolution.BL;
using ProductSolution.DTO;

namespace ProductSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly ItemBL _itemBL;

        public ItemController(ItemBL itemBL)
        {
            _itemBL = itemBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var data = await _itemBL.GetAllItems();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _itemBL.GetItemById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(ItemRequest request)
        {
            var item = await _itemBL.AddItem(request);

            return Ok(item);
        }

        [HttpPut("UpdateItem/{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemRequest request)
        {
            var result = await _itemBL.UpdateItem(id, request);

            if (!result)
                return NotFound();

            return Ok("Item Updated Successfully");
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _itemBL.DeleteItem(id);

            if (!result)
                return NotFound();

            return Ok("Item Deleted Successfully");
        }
    }
}
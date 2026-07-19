using ProductSolution.DAL;
using ProductSolution.DTO;
using ProductSolution.Model;

namespace ProductSolution.BL
{
    public class ItemBL
    {
        private readonly ItemDAL _itemDAL;

        public ItemBL(ItemDAL itemDAL)
        {
            _itemDAL = itemDAL;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _itemDAL.GetAllItems();
        }

        public async Task<Item?> GetItemById(int id)
        {
            return await _itemDAL.GetItemById(id);
        }

        public async Task<Item> AddItem(ItemRequest request)
        {
            Item item = new Item
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            return await _itemDAL.AddItem(item);
        }

        public async Task<bool> UpdateItem(int id, ItemRequest request)
        {
            Item item = new Item
            {
                Id = id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            return await _itemDAL.UpdateItem(item);
        }

        public async Task<bool> DeleteItem(int id)
        {
            return await _itemDAL.DeleteItem(id);
        }
    }
}
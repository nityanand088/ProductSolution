using Microsoft.EntityFrameworkCore;
using ProductSolution.DAL.Data;
using ProductSolution.Model;

namespace ProductSolution.DAL
{
    public class ItemDAL
    {
        private readonly ApplicationDbContext _context;

        public ItemDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _context.Items.Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<Item?> GetItemById(int id)
        {
            return await _context.Items
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Item> AddItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateItem(Item item)
        {
            var existing = await _context.Items.FindAsync(item.Id);

            if (existing == null)
                return false;

            existing.ProductId = item.ProductId;
            existing.Quantity = item.Quantity;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return false;

            _context.Items.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
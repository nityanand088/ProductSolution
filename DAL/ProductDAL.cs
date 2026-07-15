using Microsoft.EntityFrameworkCore;
using ProductApi.DAL.Models;
using ProductSolution.DAL.Data;

namespace ProductSolution.DAL
{
    public class ProductDAL
    {
        private readonly ApplicationDbContext _context;

        public ProductDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ProductApi.DAL.Models;
using ProductSolution.DAL.Data;
//using ProductSolution.DAL.Models;

namespace ProductSolution.BL;

public class ProductBL
{
    private readonly ApplicationDbContext _context;

    public ProductBL(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get All Products
    public async Task<List<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // Get Product By Id
    public async Task<Product?> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    // Add Product
    public async Task AddProduct(Product product)
    {
        product.CreatedOn = DateTime.Now;

        _context.Products.Add(product);

        await _context.SaveChangesAsync();
    }

    // Update Product
    public async Task UpdateProduct(int id, Product product)
    {
        var data = await _context.Products.FindAsync(product.Id);

        if (data == null)
            return;

        data.ProductName = product.ProductName;
        data.CreatedBy = product.CreatedBy;
        data.CreatedOn = product.CreatedOn;
        data.ModifiedBy = product.ModifiedBy;
        data.ModifiedOn = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    // Delete Product
    public async Task DeleteProduct(int id)
    {
        var data = await _context.Products.FindAsync(id);

        if (data == null)
            return;

        _context.Products.Remove(data);

        await _context.SaveChangesAsync();
    }
}
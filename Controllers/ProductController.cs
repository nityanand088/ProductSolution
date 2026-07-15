using Microsoft.AspNetCore.Mvc;
using ProductSolution.BL;
namespace ProductSolution.Controllers;
using Microsoft.AspNetCore.Authorization;
using ProductApi.DAL.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductBL _productBL;
    private readonly ILogger<ProductController> _logger;

    //public ProductController(ProductBL productBL)
    //{
    //    _productBL = productBL;
    //}
    public ProductController(ProductBL productBL,
                         ILogger<ProductController> logger)
    {
        _productBL = productBL;
        _logger = logger;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all products");    

        var products = await _productBL.GetAllProducts();

        return Ok(products);
    }

    // GET: api/Product/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productBL.GetProductById(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // POST: api/Product
    [HttpPost("AddProduct")]
    public async Task<IActionResult> Add(Product product)
    {
        _logger.LogInformation("Adding new product.");

        await _productBL.AddProduct(product);

        return Ok("Product Added Successfully");
    }

    // PUT: api/Product
    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        _logger.LogInformation("Updating product.");

        await _productBL.UpdateProduct(id,product);

        return Ok("Product Updated Successfully");
    }

    // DELETE: api/Product/1
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting product.");

        await _productBL.DeleteProduct(id);

        return Ok("Product Deleted Successfully");
    }
}
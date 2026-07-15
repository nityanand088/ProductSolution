using Microsoft.EntityFrameworkCore;
using ProductApi.DAL.Models;
using ProductSolution.Model;

namespace ProductSolution.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    //public DbSet<RefreshToken> RefreshTokenRequest { get; set; }

}
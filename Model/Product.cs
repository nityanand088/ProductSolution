using ProductSolution.Model;
using System.Collections.Generic;

namespace ProductApi.DAL.Models;

public class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public ICollection<Item>? Items { get; set; }
}
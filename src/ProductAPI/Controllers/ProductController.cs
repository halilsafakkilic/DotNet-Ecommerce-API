using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IList<Product> _products = new List<Product>
    {
        new(Guid.NewGuid(), "Toy", "toy", 5),
        new(Guid.NewGuid(), "Smart Phone", "phone", 2),
        new(Guid.NewGuid(), "Super Computer", "computer", 4),
        new(Guid.NewGuid(), "Gold Bicycle", "bicycle", 3)
    };

    [HttpGet("find")]
    public IActionResult Get()
    {
        return Ok(_products);
    }

    [HttpGet("findBySKU/{sku}")]
    public IActionResult GetBySku(string sku)
    {
        var product = _products.FirstOrDefault(x => x.Sku == sku);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
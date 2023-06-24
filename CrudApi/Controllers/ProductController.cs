﻿using CrudApi.DAL;
using CrudApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public ProductController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();
                if (products == null)
                {
                    return NotFound("Product not available. Ürün yok ");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Product model)
        {
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();
                return Ok("Product created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(Product model) 
        {
            
            if (model==null || model.Id==0)
            {
                if (model==null)
                {
                    return BadRequest("Model data is invalid");
                }
                else if (model.Id==0)
                {
                    return BadRequest($"Product Id{model.Id} is invalid");
                }
            }
            try
            {
                var product = _context.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound($"Product not found with id{model.Id}");
                }

                
                product.ProductName = model.ProductName;
                product.Qty = model.Qty;
                product.Price = model.Price;
                _context.SaveChanges();
                return Ok("Product details updated.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product not found with id {id}");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("Product details deleted.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

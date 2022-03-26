#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Backend.DataContext;
using Northwind.Backend.Services;

namespace Northwind.Backend.Models
{
    public class OrdersController : ControllerBase
    {
        private NorthwindContext _context;
        private IOrderService _service;

        public OrdersController(IOrderService service, NorthwindContext context)
        {
            _context = context;
            _service = service;
        }


        /// <summary>
        /// Get Orders
        /// </summary>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("orders")]
        [ProducesResponseType(typeof(OrderListResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrders([FromQuery] int? top, [FromQuery] int? skip)
        {
            var lr = new OrderListRequest() { Top = top, Skip = skip };

            //var result = await _context.Orders.ToListAsync();
            var result = await _service.GetOrdersAsync(lr);

            return Ok(result);
            
        }

        /// <summary>
        /// Get Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orders/{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("orders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("orders/{id}")]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("orders/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Backend.DataContext;
using Northwind.Backend.Models;

namespace Northwind.Backend.Controllers
{
    public class OrderDetailsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrderDetailsController(NorthwindContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Order Details
        /// </summary>
        /// <returns></returns>
        [HttpGet("order-details")]
        [ProducesResponseType(typeof(OrderListResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderDetailsAsync()
        {
            var result = await _context.OrderDetails.ToListAsync();
            return Ok(result);
        }

        
        [HttpGet("order-details/{id}")]
        [ProducesResponseType(typeof(OrderDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderDetailAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }
        
        [HttpPut("order-details/{id}")]
        public async Task<IActionResult> PutOrderDetailAsync(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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
        
        [HttpPost("order-details")]
        public async Task<ActionResult<OrderDetail>> PostOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderId }, orderDetail);
        }
        
        [HttpDelete("order-details/{id}")]
        public async Task<IActionResult> DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}

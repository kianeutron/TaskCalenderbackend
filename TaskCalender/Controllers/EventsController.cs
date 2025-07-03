using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using TaskCalender.Data;
using TaskCalender.Models;

namespace TaskCalender.Controllers
{
    [Authorize]
    [Route("odata/[controller]")]
    public class EventsController : ODataController
    {
        private readonly TaskCalenderContext _context;
        public EventsController(TaskCalenderContext context)
        {
            _context = context;
        }

        // GET odata/Events/Assigned
        [HttpGet("Assigned")]
        public IActionResult GetAssignedEvents()
        {
            try
            {
                var events = GetEventsForCurrentUser();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET odata/Events/Details/{id}
        [HttpGet("Details/{id}")]
        public IActionResult GetEventDetails(int id)
        {
            try
            {
                var order = GetOrderById(id);
                if (order == null)
                    return NotFound();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private List<Order> GetEventsForCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return new List<Order>();
            var principal = _context.Principals.FirstOrDefault(p => p.pcl_Id.ToString() == userId);
            if (principal == null || principal.Relation_rel_Id == null)
                return new List<Order>();
            return _context.OrderInspectors
                .Include(oi => oi.Order)
                .Where(oi => oi.Inspector_rel_Id == principal.Relation_rel_Id)
                .Select(oi => oi.Order)
                .Distinct()
                .ToList();
        }

        private Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.ord_Id == id);
        }
    }
} 
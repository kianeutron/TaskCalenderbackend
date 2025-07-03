using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using TaskCalender.Data;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var principal = _context.Principals.FirstOrDefault(p => p.pcl_Id.ToString() == userId);
            if (principal == null || principal.Relation_rel_Id == null)
                return NotFound();

            var assignedEvents = _context.OrderInspectors
                .Include(oi => oi.Order)
                .Where(oi => oi.Inspector_rel_Id == principal.Relation_rel_Id)
                .Select(oi => oi.Order)
                .Distinct()
                .ToList();

            return Ok(assignedEvents);
        }

        // GET odata/Events/Details/{id}
        [HttpGet("Details/{id}")]
        public IActionResult GetEventDetails(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.ord_Id == id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }
    }
} 
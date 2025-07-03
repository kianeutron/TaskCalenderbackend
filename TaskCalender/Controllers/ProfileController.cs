using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Security.Claims;
using TaskCalender.Data;

namespace TaskCalender.Controllers
{
    [Authorize]
    [Route("odata/[controller]")]
    public class ProfileController : ODataController
    {
        private readonly TaskCalenderContext _context;
        public ProfileController(TaskCalenderContext context)
        {
            _context = context;
        }

        [HttpGet("Me")]
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var principal = _context.Principals.FirstOrDefault(p => p.pcl_Id.ToString() == userId);
            if (principal == null)
                return NotFound();

            var profile = _context.Relations.FirstOrDefault(r => r.rel_Id == principal.Relation_rel_Id);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }
    }
} 
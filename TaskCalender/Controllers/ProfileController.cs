using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Security.Claims;
using TaskCalender.Data;
using TaskCalender.Models;

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
            try
            {
                var profile = GetProfileForCurrentUser();
                if (profile == null)
                    return NotFound();
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private Relation GetProfileForCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return null;
            var principal = _context.Principals.FirstOrDefault(p => p.pcl_Id.ToString() == userId);
            if (principal == null)
                return null;
            return _context.Relations.FirstOrDefault(r => r.rel_Id == principal.Relation_rel_Id);
        }
    }
} 
using DemoProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Controllers
{
    [Route("api/schools")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
       private readonly DemoProjectContext _context;

        public SchoolController(DemoProjectContext context)
        {
            _context = context;
        }
        [HttpGet("codes")]
        public async Task<ActionResult<IEnumerable<string>>> GetSchoolCodes()
        {
            var schoolCodes = await _context.Schools.Select(s => s.SchoolCode).ToListAsync();
            return schoolCodes;
        }
    }
}

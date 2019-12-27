using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestJsonService2.Models;

namespace TestJsonService2.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase {

        private AppDbContext _context = null;

        public ConfigsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Config>>> GetAll() {
            return await _context.Configs.ToListAsync();
        }
    }
}
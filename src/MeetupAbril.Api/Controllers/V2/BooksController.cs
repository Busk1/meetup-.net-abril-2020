using MeetupAbril.Db.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAbril.Api.Controllers.V2
{
    [ApiController, Route("api/[controller]"), ApiVersion("2.0")]
    public class BooksController : ControllerBase
    {
        private readonly MeetupAbrilContext _context;

        public BooksController(MeetupAbrilContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Get() => Ok(await _context.Books.Include(i => i.AuthorIdNavigation).ToListAsync());

        [Route("{name}")]
        public async Task<IActionResult> Get(string name) => Ok(await _context.Books.Where(w => w.Name == name).FirstOrDefaultAsync());

    }
}

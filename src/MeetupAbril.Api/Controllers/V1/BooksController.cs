using MeetupAbril.Db.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAbril.Api.Controllers.V1
{
    [ApiController, Route("api/[controller]"), ApiVersion("1.0", Deprecated = true)]
    public class BooksController : ControllerBase
    {
        private readonly MeetupAbrilContext _context;

        public BooksController(MeetupAbrilContext context)
        {
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Get() => Ok(await _context.Books.ToListAsync());

        [Route("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _context.Books.Where(w => w.Id == id).FirstOrDefaultAsync());

        [Route("{id}/Author")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAuthor(int id) => Ok(await _context.Books.Where(w => w.Id == id).Include(i => i.AuthorIdNavigation).Select(s => s.AuthorIdNavigation).FirstOrDefaultAsync());
    }
}

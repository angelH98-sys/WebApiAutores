using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id) 
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(l => l.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro) 
        {
            if(!(await context.Autores.AnyAsync(a => a.Id.Equals(libro.AutorId))))
            {
                return BadRequest($"No existe el autor de Id: {libro.AutorId}");
            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

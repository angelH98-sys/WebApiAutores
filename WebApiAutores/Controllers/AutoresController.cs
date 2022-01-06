using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    //[Authorize]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTranscient servicioTranscient;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ServicioScoped servicioScoped;
        private readonly ILogger logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio,
            ServicioTranscient servicioTranscient, ServicioSingleton servicioSingleton,
            ServicioScoped servicioScoped, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.servicio = servicio;
            this.servicioTranscient = servicioTranscient;
            this.servicioSingleton = servicioSingleton;
            this.servicioScoped = servicioScoped;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Estamos obteniendo los autores");
            logger.LogWarning("Este es un mensaje de advertencia de prueba");
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("GUID")]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult ObtenerGuids() 
        {
            return Ok(new
            {
                AutoresController_Transcient = servicioTranscient.Guid,
                ServicioA_Transcient = servicio.ObtenerTranscient(),
                AutoresController_Scoped = servicioScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),
                AutoresController_Singleton = servicioSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerSingleton()
            });
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> Get([FromHeader] int id, [FromQuery] string nombre)
        {

            //var autor = await context.Autores.Include(a => a.Libros).FirstOrDefaultAsync(a => a.Id.Equals(id));

            //if (autor == null) 
            //    return NotFound();

            //return autor;

            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]//api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
                return BadRequest("El id del autor no coincide con el id de la URL");
            

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {

            if (!(await context.Autores.AnyAsync(a => a.Id.Equals(id))))
                return NotFound();

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}

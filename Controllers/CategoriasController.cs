using System.Numerics;
using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalago.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _context.Categorias.ToList();

            if(categorias is null)
            {
                return NotFound("Categorias não encontradas.");
            }
            
            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if(categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return categoria;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if(categoria is null)
            {
                return BadRequest();
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new {id = categoria.CategoriaId}, categoria);
        }

    }
}
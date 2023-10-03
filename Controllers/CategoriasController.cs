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
        public ActionResult<IEnumerable<Categoria>> Get(){
            var categorias = _context.Categorias.ToList();

            if(categorias is null){
                return NotFound("Categorias não encontradas.");
            }
            
            return categorias;
        }

    }
}
using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalago.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //IEnumerable<Produto> indica que se espera de retorno uma coleção de objetos do tipo Produto, pode-se também utilizar List<Produto> que significa a mesma coisa, porém é menos otimizado
        //O recurso ActionResult permite que o método retorne tanto a lista de produtos, quanto todos os métodos pertencentes ao tipo de retorno suportados por ActionResult, como, por exemplo o NotFound()
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();

            if(produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            
            return produtos;
        }

    }
}
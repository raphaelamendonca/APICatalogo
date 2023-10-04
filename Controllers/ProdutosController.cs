using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var produtos = _context.Produtos.AsNoTracking().ToList();

            if(produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            
            return produtos;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            return produto;
        }

        [HttpPost]
        //O método escrito desta forma (apenas com o ActionResult),indica que retorna apenas as mensagens de status HTTP, ou seja, ele não retorna um tipo
        public ActionResult Post(Produto produto)
        {
            if(produto is null)
            {
                return BadRequest();
            }

            //Recebe o produto e inclui no contexto do EF
            _context.Produtos.Add(produto);
            //Persiste os dados no banco
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.CategoriaId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);  
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
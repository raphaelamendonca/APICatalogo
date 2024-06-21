using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly APICatalogoContext _context;

    public ProdutosController(APICatalogoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        try
        {
            var produtos = _context.Produtos.AsNoTracking().ToList();

            if (!produtos.Any())
                return NotFound("Produtos não encontrados.");

            return produtos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
               "Ocorreu um erro ao tratar a sua solicitação. Tente novamente mais tarde.");
        }
    }

    [HttpGet("{id:int}", Name="ObterProduto")]
    public ActionResult<Produto> GetById(int id)
    {
        try
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(x => x.ProdutoId == id);

            if (produto == null)
                return NotFound($"Produto com id={id} não encontrado.");

            return produto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
               "Ocorreu um erro ao tratar a sua solicitação. Tente novamente mais tarde.");
        }
    }

    [HttpGet("categoria/{id:int}", Name = "ObterProdutosPorCategoria")]
    public ActionResult<IEnumerable<Produto>> GetByCategoriaId(int id)
    {
        try
        {
            var produtos = _context.Produtos.AsNoTracking().Include(x => x.Categoria).Where(x => x.CategoriaId == id).ToList();

            if (!produtos.Any())
                return NotFound("Não existem produtos cadastrados para esta categoria.");

            return produtos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um erro ao tratar a sua solicitação. Tente novamente mais tarde.");
        }
    }

    [HttpPost]
    public ActionResult<Produto> Post(Produto produto)
    {
        try
        {
            if (produto is null)
                return BadRequest("Dados inválidos.");

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao tratar a sua solicitação. Tente novamente mais tarde.");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(int id, Produto produto)
    {
        try
        {
            if (id != produto.ProdutoId)
                return BadRequest("Dados inválidos.");

            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
               "Ocorreu um erro ao tratar a sua solicitação. Tente novamente mais tarde.");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (produto is null)
                return NotFound($"Produto com id={id} não encontrado.");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao tratar a sua solicitação. Tente novamente mais tarde.");
        }
    }
}

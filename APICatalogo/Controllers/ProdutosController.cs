using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
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
        var produtos = _context.Produtos.ToList();

        if (!produtos.Any())
            return NotFound("Produtos não encontrados");

        return produtos;
    }

    [HttpGet("{id:int}", Name="Get")]
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
       
        if (produto == null)
            return NotFound("Produto não encontrado");

        return produto;
    }

    [HttpPost]
    public ActionResult<Produto> Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("Get", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(int id, Produto produto)
    {
        if(id != produto.ProdutoId)
            return BadRequest();
        
        _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

        if (produto is null)
            return NotFound("Produto não encontrado");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}

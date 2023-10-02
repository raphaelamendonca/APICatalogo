using APICatalago.Context;
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

        
    }
}
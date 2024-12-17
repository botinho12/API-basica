using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_basica.Data;
using API_basica.Models;

namespace API_basica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para registrar a entrada de um produto
        [HttpPost("entrada")]
        public async Task<IActionResult> RegistrarEntrada([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest();
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // Endpoint para registrar a saída de um produto
        [HttpPost("saida")]
        public async Task<IActionResult> RegistrarSaida([FromBody] HistoricoSaida historicoSaida)
        {
            if (historicoSaida == null)
            {
                return BadRequest();
            }

            var produto = await _context.Produtos.FindAsync(historicoSaida.ProdutoId);
            if (produto == null || produto.Quantidade < historicoSaida.Quantidade)
            {
                return NotFound("Produto não encontrado ou quantidade insuficiente.");
            }

            // Atualiza a quantidade do produto
            produto.Quantidade -= historicoSaida.Quantidade;

            // Adiciona o histórico de saída
            _context.HistoricoSaida.Add(historicoSaida);

            _context.SaveChanges();

            return Ok(historicoSaida);
        }

        // Endpoint para obter o histórico de saída de um produto
        [HttpGet("historico/{produtoId}")]
        public async Task<ActionResult<IEnumerable<HistoricoSaida>>> GetHistorico(int produtoId)
        {
            var historico = await _context.HistoricoSaida
                .Where(h => h.ProdutoId == produtoId)
                .ToListAsync();

            if (!historico.Any())
            {
                return NotFound("Nenhum histórico encontrado.");
            }

            return Ok(historico);
        }

        // Endpoint para obter os detalhes de um produto
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }
    }
}

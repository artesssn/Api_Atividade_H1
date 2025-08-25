using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        public class Produto
        {
            public string NomeProduto { get; set; }
            public float Peso { get; set; }
            public float Altura { get; set; }
            public float Largura { get; set; }
            public float Comprimento { get; set; }
            public string UF { get; set; }
        }

        [HttpPost("calcular-frete")]
        public IActionResult CalcularFrete([FromBody] Produto produto)
        {
            if (produto.Altura <= 0 || produto.Largura <= 0 || produto.Comprimento <= 0)
                return BadRequest("Dimensões inválidas.");

            float volume = produto.Altura * produto.Largura * produto.Comprimento;

            float taxaPorCm3 = 0.01f;

            float taxaEstado = produto.UF.ToUpper() switch
            {
                "SP" => 50f,
                "RJ" => 60f,
                "MG" => 55f,
                _ => 70f
            };

            float valorFrete = (volume * taxaPorCm3) + taxaEstado;

            return Ok(new
            {
                produto.NomeProduto,
                Volume = volume,
                TaxaEstado = taxaEstado,
                ValorFrete = valorFrete
            });
        }
    }
}

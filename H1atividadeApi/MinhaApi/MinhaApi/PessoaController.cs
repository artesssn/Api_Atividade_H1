using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        public class Pessoa
        {
            public string Nome { get; set; }
            public float Peso { get; set; }
            public float Altura { get; set; }
        }

        [HttpPost("calcular-imc")]
        public IActionResult CalcularIMC([FromBody] Pessoa pessoa)
        {
            if (pessoa.Altura <= 0 || pessoa.Peso <= 0)
                return BadRequest("Altura e peso devem ser maiores que zero.");

            float imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);
            return Ok(new { pessoa.Nome, IMC = imc });
        }

        [HttpGet("consulta-tabela-imc/{imc}")]
        public IActionResult ConsultarTabelaIMC(float imc)
        {
            string classificacao;

            if (imc < 18.5) classificacao = "Abaixo do peso";
            else if (imc < 25) classificacao = "Peso normal";
            else if (imc < 30) classificacao = "Sobrepeso";
            else if (imc < 35) classificacao = "Obesidade Grau I";
            else if (imc < 40) classificacao = "Obesidade Grau II";
            else classificacao = "Obesidade Grau III";

            return Ok(new { IMC = imc, Classificacao = classificacao });
        }
    }
}
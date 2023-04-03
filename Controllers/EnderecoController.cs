using System.Text;
using Aula02.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aula02.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private readonly IConfiguration _config;
    public EnderecoController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    [Route("cep")]
    public async Task<IActionResult> ObterEndereco(int cep)
    {
        var urlCep = _config.GetValue<string>("APIsExternas:CEP");
        StringBuilder sb = new StringBuilder(urlCep);
        sb.Replace("{0}", cep.ToString());

        using (var httpClient = new HttpClient())
        {

            using (var response = await httpClient.GetAsync(sb.ToString()))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var cepDto = JsonConvert.DeserializeObject<CepDto>(apiResponse);
                var frase = $"{cepDto.Logradouro}, bairro {cepDto.Bairro}. {cepDto.Localidade} - {cepDto.UF}";
                return Ok(frase);
            }
        }

    }

}

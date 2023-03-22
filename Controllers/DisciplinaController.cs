using Aula02.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aula02.Controllers;

[ApiController]
[Route("[controller]")]
public class DisciplinaController : ControllerBase
{

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(string? nome)
    {
        var repository = new DisciplinaRepository();
        var disciplinas = repository.ListarDisciplinas(nome);

        return Ok(disciplinas);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Obter(int id)
    {

        if (id <= 0)
            return BadRequest("Id deve ser maior que zero");

        var repository = new DisciplinaRepository();
        var disciplina = repository.ObterDisciplina(id);

        if (disciplina == null)
            return NotFound();

        return Ok(disciplina);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Atualizar(int id, [FromBody] DisciplinaDto dto)
    {
        var repository = new DisciplinaRepository();
        var disciplina = repository.AtualizarCargaHoraria(id, dto);
        return Ok(disciplina);
    }

    // Criar endpoint para criar disciplina

}

public class DisciplinaDto{
    public int CargaHoraria { get; set; }
    public string Nome { get; set; }
}
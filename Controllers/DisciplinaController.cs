using Aula02.Dtos;
using Aula02.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aula02.Controllers;

[ApiController]
[Route("[controller]")]
public class DisciplinaController : ControllerBase
{

    [HttpGet]
    public IActionResult Listar([FromQuery] string? nome)
    {
        var repository = new DisciplinaRepository();
        var disciplinas = repository.ListarDisciplinas(nome);
        
        return Ok(disciplinas);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Obter([FromRoute] int id)
    {

        if (id <= 0)
            return BadRequest("Id deve ser maior que zero");

        var repository = new DisciplinaRepository();
        var disciplina = repository.ObterDisciplina(id);

        if (disciplina == null)
            return NotFound();

        return Ok(disciplina);
    }

    [HttpPost]
    public IActionResult Criar([FromBody] DisciplinaDto dto)
    {

        var repository = new DisciplinaRepository();
        var disciplina = repository.CriarDisciplina(dto);
        return CreatedAtAction(nameof(DisciplinaController.Obter), new { id = disciplina.Id }, disciplina);
    }

    [HttpPatch]
    [Route("{id}")]
    public IActionResult Atualizar(int id, [FromForm] DisciplinaDto dto)
    {
        var repository = new DisciplinaRepository();
        var disciplina = repository.AtualizarDisciplina(id, dto);
        return Ok(disciplina);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Excluir(int id)
    {

        var repository = new DisciplinaRepository();
        repository.ExcluirDisciplina(id);

        return NoContent();
    }


}

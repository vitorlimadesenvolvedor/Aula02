using System.Globalization;
using System.Text;
using Aula02.Controllers;
using Aula02.Dtos;
using Aula02.Models;

namespace Aula02.Repositories;

public class DisciplinaRepository
{

    private static List<Disciplina> lista = new List<Disciplina>(){
       new Disciplina { Id = 1, CargaHoraria = 20, Nome = "História" },
       new Disciplina { Id = 2, CargaHoraria = 10, Nome = "História do Brasil" },
       new Disciplina { Id = 3, CargaHoraria = 25, Nome = "História Geral" },
       new Disciplina { Id = 4, CargaHoraria = 20, Nome = "Matemática" }
    };

    public List<Disciplina> ListarDisciplinas(string filtroNome)
    {

        if (string.IsNullOrEmpty(filtroNome))
            return lista;
        else
            return lista.Where(z => z.Nome.ToLower().RemoverAcentos().Contains(filtroNome.ToLower().RemoverAcentos()))
            .OrderBy(x => x.Id)
            .ToList();
    }

    public Disciplina? ObterDisciplina(int id)
    {
        return lista.FirstOrDefault(x => x.Id == id);
    }

    public Disciplina AtualizarDisciplina(int id, DisciplinaDto dto)
    {

        var disciplina = ObterDisciplina(id);
        lista.Remove(disciplina);

        disciplina.CargaHoraria = dto.CargaHoraria;
        disciplina.Nome = dto.Nome;
        disciplina.DataDaAlteracao = DateTime.Now;

        lista.Add(disciplina);

        return disciplina;
    }

    public Disciplina CriarDisciplina(DisciplinaDto dto)
    {

        var disciplina = new Disciplina();
        disciplina.Id = GerarId();
        disciplina.CargaHoraria = dto.CargaHoraria;
        disciplina.Nome = dto.Nome;
        disciplina.DataDaInclusao = DateTime.Now;

        lista.Add(disciplina);

        return disciplina;
    }

    public void ExcluirDisciplina(int id)
    {
        var disciplina = lista.FirstOrDefault(a => a.Id == id);

        if (disciplina != null)
            lista.Remove(disciplina);
    }

    private int GerarId()
    {
        return lista.Last().Id + 1;
    }

}

public static class StringExtension
{
    public static string RemoverAcentos(this string text)
    {
        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }
        return sbReturn.ToString();
    }
}
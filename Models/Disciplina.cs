namespace Aula02.Models;

public class Disciplina {

    public int Id { get; set; }
    public string Nome { get; set; }
    public int CargaHoraria { get; set; }
    public DateTime DataDaInclusao { get; set; }
    public DateTime? DataDaAlteracao { get; set; }
}
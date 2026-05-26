using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

public class Caixa : EntidadeBase<Caixa>
{
    public string Etiqueta { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public int DiasDeEmprestimo { get; set; } = 7;
    public List<Revista> Revistas { get; set; } = [];

    public Caixa() { }
    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasDeEmprestimo = diasDeEmprestimo;
    }
    public override void AtualizarDados(Caixa entidadeAtualizada)
    {
        Etiqueta = entidadeAtualizada.Etiqueta;
        Cor = entidadeAtualizada.Cor;
        DiasDeEmprestimo = entidadeAtualizada.DiasDeEmprestimo;
    }

    public void AddRevistaHaCaixa(Revista revista)
    {
        Revistas.Add(revista);
    }

    public void RetirarRevistaDaCaixa(Revista revista)
    {
        Revistas.Remove(revista);
    }
}

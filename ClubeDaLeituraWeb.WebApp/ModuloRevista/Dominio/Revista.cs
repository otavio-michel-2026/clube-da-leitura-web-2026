using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public class Revista : EntidadeBase<Revista>
{
    public string Titulo { get; set; } = string.Empty;
    public uint NumeroDeEdicao { get; set; }
    public int AnoDePublicacao { get; set; }
    public Caixa Caixa { get; set; } = null!;
    public StatusRevista StatusRevista { get; set; } = StatusRevista.Disponível;

    public Revista() { }

    public Revista
    (
        string titulo,
        uint numeroDeEdicao,
        int anoDePublicacao,
        Caixa caixa
    )
    {
        Titulo = titulo;
        NumeroDeEdicao = numeroDeEdicao;
        AnoDePublicacao = anoDePublicacao;
        Caixa = caixa;
        Caixa.AddRevistaHaCaixa(this);
    }

    public void EmprestarRevista()
    {
        StatusRevista = StatusRevista.Emprestada;
    }
    public void DevolverRevista()
    {
        StatusRevista = StatusRevista.Disponível;
    }
    public void ReservarRevista()
    {
        StatusRevista = StatusRevista.Reservada;
    }

    public override void AtualizarDados(Revista entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        NumeroDeEdicao = entidadeAtualizada.NumeroDeEdicao;
        AnoDePublicacao = entidadeAtualizada.AnoDePublicacao;
        Caixa = entidadeAtualizada.Caixa;
    }
}

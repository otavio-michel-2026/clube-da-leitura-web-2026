using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public class Revista : EntidadeBase<Revista>
{
    public string Titulo { get; set; } = string.Empty;
    public uint NumeroDeEdicao { get; set; }
    public int AnoDePublicacao { get; set; }
    public Caixa Caixa { get; set; } = null!;

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
    }

    public override void AtualizarDados(Revista entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        NumeroDeEdicao = entidadeAtualizada.NumeroDeEdicao;
        AnoDePublicacao = entidadeAtualizada.AnoDePublicacao;
        Caixa = entidadeAtualizada.Caixa;
    }
}

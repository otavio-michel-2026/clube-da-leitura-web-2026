using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Infra;

public class RepositrioCaixaEmArquivo : RepositorioBaseEmArquivo<Caixa>, IRepositorioCaixa
{
    public RepositrioCaixaEmArquivo(ContextoJson contexto) : base(contexto) { }
    protected override List<Caixa> CarregarRegistros()
    {
        return contexto.Caixas;
    }
}

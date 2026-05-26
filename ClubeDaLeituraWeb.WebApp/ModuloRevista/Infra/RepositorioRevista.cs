using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Infra;

public class RepositorioRevista : RepositorioBaseEmArquivo<Revista>, IRepositorioRevista
{
    public RepositorioRevista(ContextoJson contexto) : base(contexto) { }
    protected override List<Revista> CarregarRegistros()
    {
        return contexto.Revistas;
    }
}

using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Infra;

public class repositorioRevista : RepositorioBaseEmArquivo<Revista>, IRepositorioRevista
{
    public repositorioRevista(ContextoJson contexto) : base(contexto) { }
    protected override List<Revista> CarregarRegistros()
    {
        return contexto.Revistas;
    }
}

using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Infra
{
    public class RepositorioAmigo : RepositorioBaseEmArquivo<Amigo>, IRepositorioAmigo
    {
        public RepositorioAmigo(ContextoJson contexto) : base(contexto) { }
        protected override List<Amigo> CarregarRegistros()
        {
            return contexto.Amigos;
        }
    }
}

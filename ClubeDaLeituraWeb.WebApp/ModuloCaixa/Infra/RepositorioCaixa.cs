using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Infra
{
    public class RepositorioCaixa : RepositorioBaseEmArquivo<Caixa>, IRepositorioCaixa
    {
        public RepositorioCaixa(ContextoJson contexto) : base(contexto) { }
        protected override List<Caixa> CarregarRegistros()
        {
            return contexto.Caixas;
        }
    }
}

using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Infra;

public class RepositorioEmprestimo : RepositorioBaseEmArquivo<Emprestimo>, IRepositorioEmprestimo
{
    public RepositorioEmprestimo(ContextoJson contexto) : base(contexto) { }

    protected override List<Emprestimo> CarregarRegistros()
    {
        return contexto.Emprestimos;
    }

    public void Devolver(Emprestimo emprestimo)
    {
        emprestimo.Revista.DevolverRevista();
        emprestimo.ConcluirEmprestimo();
        contexto.Salvar();
    }
}

using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public interface IRepositorioEmprestimo : IRepositorio<Emprestimo>
{
    void Devolver(Emprestimo emprestimo);
}

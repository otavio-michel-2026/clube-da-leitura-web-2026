using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

public class Amigo : EntidadeBase<Amigo>
{
    public string Nome { get; set; } = string.Empty;
    public string NomeResponsavel { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public List<Emprestimo> Emprestimos { get; set; } = [];

    public Amigo() { }

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }

    public Amigo(Amigo a) : this(a.Nome, a.NomeResponsavel, a.Telefone) { }

    public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        Emprestimos.Add(emprestimo);
    }
    public void RemoverEmprestimo(Emprestimo emprestimo)
    {
        Emprestimos.Remove(emprestimo);
    }
    

    public override void AtualizarDados(Amigo entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        NomeResponsavel = entidadeAtualizada.NomeResponsavel;
        Telefone = entidadeAtualizada.Telefone;
    }
}

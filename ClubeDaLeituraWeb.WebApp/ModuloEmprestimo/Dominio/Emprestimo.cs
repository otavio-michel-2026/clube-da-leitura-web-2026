using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public class Emprestimo : EntidadeBase<Emprestimo>
{

    public Amigo Amigo { get; set; } = null!;
    public Revista Revista { get; set; } = null!;
    public DateTime DataEmprestimo { get; set; } = DateTime.Now;
    public DateTime DataDevolucao { get; set; }
    private StatusEmprestimo status;
    public StatusEmprestimo StatusEmprestimo
    {
        get
        {
            if (status == StatusEmprestimo.Aberto && DataEmprestimo > DataDevolucao)
                return StatusEmprestimo.Atrasado;
            else return status;
        }
        set
        {
            status = value;
        }
    }

    public Emprestimo() { }

    public Emprestimo(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        DataDevolucao = DataEmprestimo.AddDays(Revista.Caixa.DiasDeEmprestimo);
        StatusEmprestimo = StatusEmprestimo.Aberto;
        Amigo.AdicionarEmprestimo(this);
        Revista.EmprestarRevista();
    }

    public void ConcluirEmprestimo()
    {
        StatusEmprestimo = StatusEmprestimo.Concluído;
    }

    public override void AtualizarDados(Emprestimo entidadeAtualizada)
    {
        if (entidadeAtualizada.Amigo != Amigo)
        {
            Amigo.RemoverEmprestimo(this);
            Amigo = entidadeAtualizada.Amigo;
            Amigo.AdicionarEmprestimo(this);
        }
        if (entidadeAtualizada.Revista != Revista)
        {
            Revista.DevolverRevista();
            Revista = entidadeAtualizada.Revista;
            Revista.EmprestarRevista();
            DataDevolucao = DataEmprestimo.AddDays(Revista.Caixa.DiasDeEmprestimo);
        }
    }
}

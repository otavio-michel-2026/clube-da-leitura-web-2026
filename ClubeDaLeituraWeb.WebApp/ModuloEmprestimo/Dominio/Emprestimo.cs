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
    public DateTime? DataDevolvido { get; set; }
    private StatusEmprestimo statusEmprestimo;
    public StatusEmprestimo StatusEmprestimo
    {
        get
        {
            if (statusEmprestimo == StatusEmprestimo.Aberto && DateTime.Now > DataDevolucao)
                return StatusEmprestimo.Atrasado;
            else return statusEmprestimo;
        }
        set
        {
            statusEmprestimo = value;
        }
    }

    private StatusMulta statusMulta;
    public StatusMulta StatusMulta
    {
        get
        {
            if (statusMulta == StatusMulta.Quitada)
                return StatusMulta.Quitada;
            else if (StatusEmprestimo == StatusEmprestimo.Atrasado || StatusEmprestimo == StatusEmprestimo.ConcluidoAtrasado)
                return StatusMulta.Pendente;
            else
                return StatusMulta.SemMulta;
        }
        set
        {
            statusMulta = value;
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
        Revista.DevolverRevista();
        DataDevolvido = DateTime.Now;
        StatusEmprestimo = StatusEmprestimo == StatusEmprestimo.Atrasado ? StatusEmprestimo.ConcluidoAtrasado : StatusEmprestimo.Concluido;
    }

    public decimal CalcularMulta()
    {
        DateOnly data = DateOnly.FromDateTime(DataDevolvido ?? DateTime.Now);
        int dias = data.DayNumber - DateOnly.FromDateTime(DataDevolucao).DayNumber;
        return dias > 0 ? dias * 2.0m : 0;
    }

    public void QuitarMulta()
    {
        StatusMulta = StatusMulta.Quitada;
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

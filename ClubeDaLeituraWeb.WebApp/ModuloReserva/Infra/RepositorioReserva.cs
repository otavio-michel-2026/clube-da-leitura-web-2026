using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloReserva.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloReserva.Infra;

public class RepositorioReserva : RepositorioBaseEmArquivo<Reserva>, IRepositorioReserva

{
    public RepositorioReserva(ContextoJson contexto) : base(contexto) { }

    protected override List<Reserva> CarregarRegistros()
    {
        return contexto.Reservas;
    }
    public void Cancelar(Reserva reserva)
    {
        reserva.CancelarReserva();
        contexto.Salvar();
    }
    public void RealizarEmprestimo(Reserva reserva)
    {
        reserva.RealizarEmprestimo();
        contexto.Salvar();
    }
}

using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloReserva.Dominio;

public interface IRepositorioReserva : IRepositorio<Reserva>
{
    public void Cancelar(Reserva reserva);
    public void RealizarEmprestimo(Reserva reserva);
}

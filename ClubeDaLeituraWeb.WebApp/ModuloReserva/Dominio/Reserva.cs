using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloReserva.Dominio;

public class Reserva : EntidadeBase<Reserva>
{
    public Amigo Amigo { get; set; } = null!;
    public Revista Revista { get; set; } = null!;
    public StatusReserva StatusReserva { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;

    public Reserva() { }
    public Reserva(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        StatusReserva = StatusReserva.Aberta;
    }
    public override void AtualizarDados(Reserva entidadeAtualizada)
    {
        Amigo = entidadeAtualizada.Amigo;
        Revista = entidadeAtualizada.Revista;
    }
    public void ConverterParaEmprestimo()
    {
        StatusReserva = StatusReserva.Concluida;
    }
    public void CancelarReserva()
    {
        StatusReserva = StatusReserva.Cancelada;
    }
}

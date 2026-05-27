using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloReserva.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloReserva.Apresentacao;

public record ReservaViewModel
(
    [Required(ErrorMessage = "O campo \"Caixa\" deve ser preenchido.")]
    string AmigoId,

    [Required(ErrorMessage = "O campo \"Revista\" deve ser preenchido.")]
    string RevistaId,

    string Id = ""
);
public record ReservaMostrarViewModel
(
    string Amigo,

    string Revista,

    DateTime Data,

    StatusReserva Status,

    string Id
);



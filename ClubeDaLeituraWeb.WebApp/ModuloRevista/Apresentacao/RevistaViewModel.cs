using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public record RevistaViewModel
(
    [Required(ErrorMessage = "O campo \"Titulo\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Titulo\" deve conter no máximo 100 e no minimo 2 caracteres.")]
    string Titulo,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"NumeroDeEdicao\" deve ser positivo (maior que zero).")]
    uint NumeroDeEdicao,

    [Range(1600, 2026, ErrorMessage = "O campo \"Dias de Empréstimo\" deve ser um ano valido")]
    int AnoDePublicacao,

    [Required(ErrorMessage = "O campo \"Caixa\" deve ser preenchido.")]
    string CaixaId,

    string Id = ""
);

public record RevistaMostrarViewModel
(
    string Titulo,

    uint NumeroDeEdicao,

    int AnoDePublicacao,

    string Caixa,

    string Id
);

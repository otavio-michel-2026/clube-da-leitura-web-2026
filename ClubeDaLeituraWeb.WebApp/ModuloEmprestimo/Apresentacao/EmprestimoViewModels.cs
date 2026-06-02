using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao;

public record EmprestimoViewModel(
    [Required(ErrorMessage = "O campo \"Amigo\" deve ser preenchido.")]
    string AmigoId,

    [Required(ErrorMessage = "O campo \"Revista\" deve ser preenchido.")]
    string RevistaId,

    string Id = ""
);

public record EmprestimoMostrarViewModel(
    string Amigo,
    string Revista,
    DateTime DataEmprestimo,
    DateTime DataDevolucao,
    StatusEmprestimo StatusEmprestimo,
    StatusMulta StatusMulta,
    decimal Multa,
    string Id
);

public record EmprestimoDevolverViewModel(
    string Revista,
    string Id
);

public record EmprestimoQuitarMultaViewModel(
    string Amigo,
    string Id
);



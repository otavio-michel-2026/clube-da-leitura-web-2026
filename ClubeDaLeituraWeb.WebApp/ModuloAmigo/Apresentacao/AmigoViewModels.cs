using System.ComponentModel.DataAnnotations;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao;

public record AmigoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Nome do Responsável\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome do Responsável\" deve conter no máximo 100 caracteres.")]
    string NomeResponsavel,
    
    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\)\s(9?\d{4})-\d{4}$", ErrorMessage = "Telefone no formato inválido")]
    string Telefone,
    
    string Id = ""
);

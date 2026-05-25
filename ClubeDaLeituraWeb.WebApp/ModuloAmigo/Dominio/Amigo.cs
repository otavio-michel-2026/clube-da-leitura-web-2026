using System.Text.RegularExpressions;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

public class Amigo : EntidadeBase<Amigo>
{
    public string Nome { get; set; } = string.Empty;
    public string NomeResponsavel { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public Amigo() { }

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }

    public Amigo(Amigo a) : this(a.Nome, a.NomeResponsavel, a.Telefone) { }

    public override void AtualizarDados(Amigo entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        NomeResponsavel = entidadeAtualizada.NomeResponsavel;
        Telefone = entidadeAtualizada.Telefone;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        // Nome
        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        if (Nome.Length < 3)
            erros.Add("O campo \"Nome\" deve conter no mínimo 3 caracteres.");
        if (Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter no máximo 100 caracteres.");

        // Nome do Responsável
        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros.Add("O campo \"Nome do Responsável\" deve ser preenchido.");
        if (NomeResponsavel.Length < 3)
            erros.Add("O campo \"Nome do Responsável\" deve conter no mínimo 3 caracteres.");
        if (NomeResponsavel.Length > 100)
            erros.Add("O campo \"Nome do Responsável\" deve conter no máximo 100 caracteres.");

        // Telefone
        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");

        if (Regex.IsMatch(@"^\(\d{2}\)\s(9?\d{4})-\d{4}$", Telefone))
            erros.Add("O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) 9XXXX-XXXX");

        return erros;
    }
}

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public static class StatusExtensions
{
    public static string String(this StatusEmprestimo statusEmprestimo)
    {
        return statusEmprestimo switch
        {
            StatusEmprestimo.Aberto => "Aberto",
            StatusEmprestimo.Concluido => "Concluído",
            StatusEmprestimo.ConcluidoAtrasado => "Concluído Atrasado",
            StatusEmprestimo.Atrasado => "Atrasado",
            _ => string.Empty
        };
    }
    public static string String(this StatusMulta statusEmprestimo)
    {
        return statusEmprestimo switch
        {
            StatusMulta.Pendente => "Pendente",
            StatusMulta.Quitada => "Quitada",
            StatusMulta.SemMulta => "Sem Multa",
            _ => string.Empty
        };
    }
}

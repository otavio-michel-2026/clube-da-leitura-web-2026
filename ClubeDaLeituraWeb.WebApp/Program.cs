using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Infra;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Infra;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Infra;

namespace ClubeDaLeituraWeb.WebApp;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();
        
            contextoJson.Carregar();
        
            return contextoJson;
        });

        builder.Services.AddScoped<IRepositorioAmigo, RepositorioAmigo>();

        builder.Services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();
        
            contextoJson.Carregar();
        
            return contextoJson;
        });
        builder.Services.AddScoped<IRepositorioCaixa, RepositorioCaixa>();
        builder.Services.AddScoped<IRepositorioRevista, RepositorioRevista>();
        builder.Services.AddScoped<IRepositorioAmigo, RepositorioAmigo>();
        
        // Configuração de Serviços
        builder.Services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Resetar a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Views dos módulos: /ModuloCaixa/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        var app = builder.Build();

        // Configuração de Middlewares
        app.UseStaticFiles();

        app.UseRouting();
        app.MapDefaultControllerRoute();

        // Execução do App
        app.Run();
    }
}

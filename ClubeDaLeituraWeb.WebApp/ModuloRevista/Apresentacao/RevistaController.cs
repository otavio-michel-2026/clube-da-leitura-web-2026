using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.Modulorevista.Apresentacao;

public class RevistaController : Controller
{
    private readonly IRepositorioRevista repositorioRevista;

    public RevistaController(IRepositorioRevista repositorioRevista)
    {
        this.repositorioRevista = repositorioRevista;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var vms = repositorioRevista.SelecionarTodos()
            .Select(c => new RevistaViewModel(c.Titulo, c.NumeroDeEdicao, c.AnoDePublicacao, c.Id)).ToList();

        return View(vms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        RevistaViewModel revista = new RevistaViewModel(
            string.Empty,
            0,
            0
        );

        return View(revista);
    }

    [HttpPost]
    public ActionResult Cadastrar(RevistaViewModel revista)
    {
        if (!ModelState.IsValid)
            return View(revista);

        Revista novarevista = new(
        revista.Titulo,
        revista.NumeroDeEdicao,
        revista.AnoDePublicacao
    );

        repositorioRevista.Cadastrar(novarevista);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Revista? revista = repositorioRevista.SelecionarPorId(id);

        if (revista == null)
            return RedirectToAction(nameof(Listar));

        RevistaViewModel revistaView = new RevistaViewModel(
            revista.Titulo,
            revista.NumeroDeEdicao,
            revista.AnoDePublicacao,
            id
        );

        return View(revistaView);
    }

    [HttpPost]
    public ActionResult Editar(RevistaViewModel revista)
    {
        if (!ModelState.IsValid)
            return View(revista);

        Revista novarevista = new Revista(
        revista.Titulo,
        revista.NumeroDeEdicao,
        revista.AnoDePublicacao
    );

        repositorioRevista.Editar(revista.Id, novarevista);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Revista? revista = repositorioRevista.SelecionarPorId(id);

        if (revista == null)
            return RedirectToAction(nameof(Listar));

        RevistaViewModel excluirVm = new RevistaViewModel(
        revista.Titulo,
        revista.NumeroDeEdicao,
        revista.AnoDePublicacao,
        id
    );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(RevistaViewModel excluirVm)
    {
        Revista? revista = repositorioRevista.SelecionarPorId(excluirVm.Id);

        if (revista != null)
            repositorioRevista.Excluir(revista);

        return RedirectToAction(nameof(Listar));
    }
}

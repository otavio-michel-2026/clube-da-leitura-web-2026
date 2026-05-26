using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubeDaLeituraWeb.WebApp.Modulorevista.Apresentacao;

public class RevistaController : Controller
{
    private readonly IRepositorioRevista repositorioRevista;
    private readonly IRepositorioCaixa repositorioCaixa;
    public RevistaController(IRepositorioRevista repositorioRevista, IRepositorioCaixa repositorioCaixa)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var vms = repositorioRevista.SelecionarTodos()
            .Select(c => new RevistaMostrarViewModel(c.Titulo, c.NumeroDeEdicao, c.AnoDePublicacao, c.Caixa.Etiqueta, c.Id)).ToList();

        return View(vms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Caixas = CarregarCaixas();

        RevistaViewModel revista = new RevistaViewModel(
            string.Empty,
            0,
            1600,
            string.Empty
        );

        return View(revista);
    }

    [HttpPost]
    public ActionResult Cadastrar(RevistaViewModel vm)
    {
        if (repositorioRevista.SelecionarTodos().Any(r => r.Titulo == vm.Titulo && r.NumeroDeEdicao == vm.NumeroDeEdicao))
        {
            ModelState.AddModelError(nameof(vm.Titulo), "Ja existe uma Revista com esse titulo e n° edicao");
            ModelState.AddModelError(nameof(vm.NumeroDeEdicao), "Ja existe uma Revista com esse titulo e n° edicao");
        }
        Caixa? caixa = repositorioCaixa.SelecionarPorId(vm.CaixaId);

        if (caixa is null || vm.CaixaId == string.Empty)
            ModelState.AddModelError(nameof(vm.CaixaId), "Selecione uma caixa valida");

        if (!ModelState.IsValid)
        {
            ViewBag.Caixas = CarregarCaixas();
            return View(vm);
        }

        Revista novarevista = new(
        vm.Titulo,
        vm.NumeroDeEdicao,
        vm.AnoDePublicacao,
        caixa!
        );

        repositorioRevista.Cadastrar(novarevista);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        ViewBag.Caixas = CarregarCaixas();

        Revista? revista = repositorioRevista.SelecionarPorId(id);

        if (revista == null)
            return RedirectToAction(nameof(Listar));

        RevistaViewModel revistaView = new RevistaViewModel(
            revista.Titulo,
            revista.NumeroDeEdicao,
            revista.AnoDePublicacao,
            revista.Caixa.Id,
            id
        );

        return View(revistaView);
    }

    [HttpPost]
    public ActionResult Editar(RevistaViewModel vm)
    {
        if (repositorioRevista.SelecionarTodos().Any(r => r.Titulo == vm.Titulo && r.NumeroDeEdicao == vm.NumeroDeEdicao))
        {
            ModelState.AddModelError(nameof(vm.Titulo), "Ja existe uma Revista com esse titulo e n° edicao");
            ModelState.AddModelError(nameof(vm.NumeroDeEdicao), "Ja existe uma Revista com esse titulo e n° edicao");
        }
        Caixa? caixa = repositorioCaixa.SelecionarPorId(vm.CaixaId);

        if (caixa is null || vm.CaixaId == string.Empty)
            ModelState.AddModelError(nameof(vm.CaixaId), "Selecione uma caixa valida");

        if (!ModelState.IsValid)
        {
            ViewBag.Caixas = CarregarCaixas();
            return View(vm);
        }

        Revista novarevista = new Revista(
        vm.Titulo,
        vm.NumeroDeEdicao,
        vm.AnoDePublicacao,
        caixa!
        );

        repositorioRevista.Editar(vm.Id, novarevista);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Revista? revista = repositorioRevista.SelecionarPorId(id);

        if (revista == null)
            return RedirectToAction(nameof(Listar));

        RevistaMostrarViewModel excluirVm = new RevistaMostrarViewModel(
        revista.Titulo,
        revista.NumeroDeEdicao,
        revista.AnoDePublicacao,
        revista.Caixa.Etiqueta,
        id
    );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(RevistaMostrarViewModel excluirVm)
    {
        Revista? revista = repositorioRevista.SelecionarPorId(excluirVm.Id);

        if (revista != null)
        {
            revista.Caixa.RetirarRevistaDaCaixa(revista);
            repositorioRevista.Excluir(revista);
        }
        return RedirectToAction(nameof(Listar));
    }

    private List<SelectListItem> CarregarCaixas()
    {
        return repositorioCaixa.SelecionarTodos().Select(c => new SelectListItem(c.Etiqueta, c.Id)).ToList();
    }
}

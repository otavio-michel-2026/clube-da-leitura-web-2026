using System.Collections.Immutable;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Apresentacao;

public class CaixaController : Controller
{
    private readonly IRepositorioCaixa repositorioCaixa;

    public CaixaController(IRepositorioCaixa repositorioCaixa)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var vms = repositorioCaixa.SelecionarTodos()
            .Select(c => new CaixaViewModel(c.Etiqueta, c.Cor, c.DiasDeEmprestimo, c.Revistas.Count, c.Id)).ToList();

        return View(vms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CaixaViewModel caixa = new CaixaViewModel(
            string.Empty,
            string.Empty,
            0,
            7
        );

        return View(caixa);
    }

    [HttpPost]
    public ActionResult Cadastrar(CaixaViewModel vm)
    {
        if (repositorioCaixa.SelecionarTodos().Any(c => c.Etiqueta == vm.Etiqueta))
            ModelState.AddModelError(nameof(vm.Etiqueta), "Ja existe uma Caixa com essa Etiqueta");

        if (!ModelState.IsValid)
            return View(vm);

        Caixa novaCaixa = new(
            vm.Etiqueta,
            vm.Cor,
            vm.DiasDeEmprestimo
        );

        repositorioCaixa.Cadastrar(novaCaixa);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Caixa? caixa = repositorioCaixa.SelecionarPorId(id);

        if (caixa == null)
            return RedirectToAction(nameof(Listar));

        CaixaViewModel caixaView = new CaixaViewModel(
            caixa.Etiqueta,
            caixa.Cor,
            caixa.DiasDeEmprestimo,
            caixa.Revistas.Count,
            id
        );

        return View(caixaView);
    }

    [HttpPost]
    public ActionResult Editar(CaixaViewModel vm)
    {
        if (repositorioCaixa.SelecionarTodos().Any(c => c.Etiqueta == vm.Etiqueta))
            ModelState.AddModelError(nameof(vm.Etiqueta), "Ja existe uma Caixa com essa Etiqueta");

        if (!ModelState.IsValid)
            return View(vm);

        Caixa novaCaixa = new Caixa(
            vm.Etiqueta,
            vm.Cor,
            vm.DiasDeEmprestimo
        );

        repositorioCaixa.Editar(vm.Id, novaCaixa);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Caixa? caixa = repositorioCaixa.SelecionarPorId(id);

        if (caixa == null)
            return RedirectToAction(nameof(Listar));

        CaixaViewModel excluirVm = new CaixaViewModel(
        caixa.Etiqueta,
        caixa.Cor,
        caixa.DiasDeEmprestimo,
        caixa.Revistas.Count,
        id
    );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(CaixaViewModel excluirVm)
    {
        Caixa? caixa = repositorioCaixa.SelecionarPorId(excluirVm.Id);

        if (caixa == null)
            return RedirectToAction(nameof(Listar));

        if (caixa.Revistas.Count != 0)
            ModelState.AddModelError(nameof(excluirVm.Id), "Essa Caixa contem revistas");

        if (!ModelState.IsValid)
            return View(excluirVm);

        repositorioCaixa.Excluir(caixa);

        return RedirectToAction(nameof(Listar));
    }
}

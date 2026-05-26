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
            .Select(c => new CaixaViewModel(c.Etiqueta, c.Cor, c.DiasDeEmprestimo, c.Id)).ToList();

        return View(vms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CaixaViewModel caixa = new CaixaViewModel(
            string.Empty,
            string.Empty,
            7
        );

        return View(caixa);
    }

    [HttpPost]
    public ActionResult Cadastrar(CaixaViewModel caixa)
    {
        if (!ModelState.IsValid)
            return View(caixa);

        Caixa novaCaixa = new(
            caixa.Etiqueta,
            caixa.Cor,
            caixa.DiasDeEmprestimo
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
            id
        );

        return View(caixaView);
    }

    [HttpPost]
    public ActionResult Editar(CaixaViewModel caixa)
    {
        if (!ModelState.IsValid)
            return View(caixa);

        Caixa novaCaixa = new Caixa(
            caixa.Etiqueta,
            caixa.Cor,
            caixa.DiasDeEmprestimo
        );

        repositorioCaixa.Editar(caixa.Id, novaCaixa);

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
        id
    );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(CaixaViewModel excluirVm)
    {
        Caixa? caixa = repositorioCaixa.SelecionarPorId(excluirVm.Id);

        if (caixa != null)
            repositorioCaixa.Excluir(caixa);

        return RedirectToAction(nameof(Listar));
    }
}

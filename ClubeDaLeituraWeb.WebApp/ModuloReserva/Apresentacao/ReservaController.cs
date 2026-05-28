using System.Data.Common;
using System.Text.RegularExpressions;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloReserva.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloReserva.Infra;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubeDaLeituraWeb.WebApp.ModuloReserva.Apresentacao;

public class ReservaController : Controller
{
    private readonly IRepositorioReserva repositorioReserva;
    private readonly IRepositorioAmigo repositorioAmigo;
    private readonly IRepositorioRevista repositorioRevista;

    public ReservaController
    (
    IRepositorioReserva repositorioReserva,
    IRepositorioAmigo repositorioAmigo,
    IRepositorioRevista repositorioRevista
    )
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var vms = repositorioReserva.SelecionarTodos()
            .Select(c => new ReservaMostrarViewModel(c.Amigo.Nome, c.Revista.Titulo, c.Data.ToString(format: "dd/MM/yyyy"), c.StatusReserva, c.Id)).ToList();

        return View(vms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Amigos = CarregarAmigos();
        ViewBag.Revistas = CarregarRevistas();

        ReservaViewModel vm = new(
            string.Empty,
            string.Empty
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(ReservaViewModel vm)
    {
        Amigo? amigo = repositorioAmigo.SelecionarPorId(vm.AmigoId);
        Revista? revista = repositorioRevista.SelecionarPorId(vm.RevistaId);

        if (amigo is null || vm.AmigoId == string.Empty)
            ModelState.AddModelError(nameof(vm.AmigoId), "Selecione um amigo válido");
        if (revista is null || vm.RevistaId == string.Empty)
            ModelState.AddModelError(nameof(vm.RevistaId), "Selecione uma revista válida");
        if (!ModelState.IsValid)
            return View(vm);

        if (!ModelState.IsValid)
        {
            ViewBag.Amigos = CarregarAmigos();
            ViewBag.Revistas = CarregarRevistas();
            return View(vm);
        }

        Reserva reserva = new(amigo!, revista!);

        repositorioReserva.Cadastrar(reserva);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Cancelar(string id)
    {
        Reserva? reserva = repositorioReserva.SelecionarPorId(id);

        if (reserva is null)
            return RedirectToAction(nameof(Listar));

        ReservaMostrarViewModel vm = new(
            reserva.Amigo.Nome,
            reserva.Revista.Titulo,
            reserva.Data.ToString(format: "dd/MM/yyyy"),
            reserva.StatusReserva,
            reserva.Id
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cancelar(ReservaViewModel vm)
    {
        Reserva? reserva = repositorioReserva.SelecionarPorId(vm.Id);

        if (reserva is not null)
            repositorioReserva.Cancelar(reserva);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult RealizarEmprestimo(string id)
    {
        Reserva? reserva = repositorioReserva.SelecionarPorId(id);

        if (reserva is null)
            return RedirectToAction(nameof(Listar));

        ReservaDevolverViewModel vm = new(
            reserva.Revista.Titulo,
            reserva.Id
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult RealizarEmprestimo(ReservaDevolverViewModel vm)
    {
        Reserva? reserva = repositorioReserva.SelecionarPorId(vm.Id);

        if (reserva is not null)
            repositorioReserva.RealizarEmprestimo(reserva);


        return RedirectToAction(nameof(Listar));
    }

    private List<SelectListItem> CarregarAmigos()
    {
        return repositorioAmigo.SelecionarTodos()
            .Select(a => new SelectListItem(a.Nome, a.Id))
            .ToList();
    }

    private List<SelectListItem> CarregarRevistas()
    {
        return repositorioRevista.SelecionarTodos()
            .Where(r => r.StatusRevista == StatusRevista.Disponível)
            .Select(r => new SelectListItem(r.Titulo, r.Id))
            .ToList();
    }
}

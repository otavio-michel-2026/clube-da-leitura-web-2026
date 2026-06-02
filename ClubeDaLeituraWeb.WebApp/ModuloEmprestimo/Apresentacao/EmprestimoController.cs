using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao
{
    public class EmprestimoController : Controller
    {
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IRepositorioAmigo repositorioAmigo;
        private readonly IRepositorioRevista repositorioRevista;

        public EmprestimoController(IRepositorioEmprestimo repositorioEmprestimo,
                                    IRepositorioAmigo repositorioAmigo,
                                    IRepositorioRevista repositorioRevista)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var vms = repositorioEmprestimo.SelecionarTodos()
                .Select(e => new EmprestimoMostrarViewModel(e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo, e.DataDevolucao, e.StatusEmprestimo, e.StatusMulta, e.CalcularMulta(), e.Id))
                .ToList();

            return View(vms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.Amigos = CarregarAmigos();
            ViewBag.Revistas = CarregarRevistas();

            EmprestimoViewModel vm = new(string.Empty, string.Empty);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Cadastrar(EmprestimoViewModel vm)
        {
            Amigo? amigo = repositorioAmigo.SelecionarPorId(vm.AmigoId);
            Revista? revista = repositorioRevista.SelecionarPorId(vm.RevistaId);

            if (amigo is null || vm.AmigoId == string.Empty)
                ModelState.AddModelError(nameof(vm.AmigoId), "Selecione um amigo válido");
            if (revista is null || vm.RevistaId == string.Empty)
                ModelState.AddModelError(nameof(vm.RevistaId), "Selecione uma revista válida");

            if (!ModelState.IsValid)
            {
                ViewBag.Amigos = CarregarAmigos();
                ViewBag.Revistas = CarregarRevistas();
                return View(vm);
            }

            Emprestimo emprestimo = new(amigo!, revista!);

            repositorioEmprestimo.Cadastrar(emprestimo);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Devolver(string id)
        {
            Emprestimo? emprestimo = repositorioEmprestimo.SelecionarPorId(id);

            if (emprestimo is null)
                return RedirectToAction(nameof(Listar));

            EmprestimoDevolverViewModel vm = new(emprestimo.Revista.Titulo, emprestimo.Id);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Devolver(EmprestimoDevolverViewModel vm)
        {
            Emprestimo? emprestimo = repositorioEmprestimo.SelecionarPorId(vm.Id);

            if (emprestimo is not null)
                repositorioEmprestimo.Devolver(emprestimo);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult QuitarMulta(string id)
        {
            Emprestimo? emprestimo = repositorioEmprestimo.SelecionarPorId(id);

            if (emprestimo is null)
                return RedirectToAction(nameof(Listar));

            EmprestimoQuitarMultaViewModel vm = new(emprestimo.Amigo.Nome, emprestimo.Id);

            return View(vm);
        }

        [HttpPost]
        public ActionResult QuitarMulta(EmprestimoQuitarMultaViewModel vm)
        {
            Emprestimo? emprestimo = repositorioEmprestimo.SelecionarPorId(vm.Id);

            if (emprestimo is not null)
                repositorioEmprestimo.QuitarMulta(emprestimo);

            return RedirectToAction(nameof(Listar));
        }

        private List<SelectListItem> CarregarAmigos()
        {
            return repositorioAmigo.SelecionarTodos()
                .Where(a => a.Emprestimos.Any(e => (e.StatusEmprestimo == StatusEmprestimo.Concluido || e.StatusEmprestimo == StatusEmprestimo.ConcluidoAtrasado) && e.StatusMulta != StatusMulta.Pendente) || a.Emprestimos.Count == 0)
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
}

using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao
{
    public class AmigoController : Controller
    {
        private readonly IRepositorioAmigo repositorioAmigo;

        public AmigoController(IRepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var vms = repositorioAmigo.SelecionarTodos()
                .Select(a => new AmigoMostrarViewModel(a.Nome, a.NomeResponsavel, a.Telefone, a.Emprestimos.Any(e => e.StatusMulta != StatusMulta.SemMulta),a.Id)).ToList();

            return View(vms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            AmigoViewModel vm = new(string.Empty, string.Empty, string.Empty);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Cadastrar(AmigoViewModel vm)
        {
            if (repositorioAmigo.SelecionarTodos().Any(a => a.Nome == vm.Nome && a.Telefone == vm.Telefone))
            {
                ModelState.AddModelError(nameof(vm.Nome), "Ja existe um Amigo com esse nome e telefone");
                ModelState.AddModelError(nameof(vm.Telefone), "Ja existe um Amigo com essa nome e telefone");
            }

            if (!ModelState.IsValid)
                return View(vm);

            Amigo amigo = new(
                vm.Nome,
                vm.NomeResponsavel,
                vm.Telefone
            );

            repositorioAmigo.Cadastrar(amigo);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Editar(string id)
        {
            Amigo? amigo = repositorioAmigo.SelecionarPorId(id);

            if (amigo is null)
                return RedirectToAction(nameof(Listar));

            AmigoViewModel vm = new(
                amigo.Nome,
                amigo.NomeResponsavel,
                amigo.Telefone,
                amigo.Id
            );

            return View(vm);
        }

        [HttpPost]
        public ActionResult Editar(AmigoViewModel vm)
        {
            if (repositorioAmigo.SelecionarTodos().Where(r => r.Id != vm.Id).Any(a => a.Nome == vm.Nome && a.Telefone == vm.Telefone))
            {
                ModelState.AddModelError(nameof(vm.Nome), "Ja existe um Amigo com esse nome e telefone");
                ModelState.AddModelError(nameof(vm.Telefone), "Ja existe um Amigo com essa nome e telefone");
            }

            if (!ModelState.IsValid)
                return View(vm);

            Amigo amigoEditado = new(
                vm.Nome,
                vm.NomeResponsavel,
                vm.Telefone
            );

            repositorioAmigo.Editar(vm.Id, amigoEditado);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Excluir(string id)
        {
            Amigo? amigo = repositorioAmigo.SelecionarPorId(id);

            if (amigo is null)
                return RedirectToAction(nameof(Listar));

            AmigoViewModel vm = new(
                amigo.Nome,
                amigo.NomeResponsavel,
                amigo.Telefone,
                amigo.Id
            );

            return View(vm);
        }

        [HttpPost]
        public ActionResult Excluir(AmigoViewModel vm)
        {
            Amigo? amigo = repositorioAmigo.SelecionarPorId(vm.Id);

            if (amigo is null)
                return RedirectToAction(nameof(Listar));

            if (amigo.Emprestimos.Count != 0)
            {
                ViewBag.Erro = "Esse amigo tem empréstimo aberto";
                return View(vm);
            }

            repositorioAmigo.Excluir(amigo);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult ListarMultas(string id)
        {
            Amigo? amigo = repositorioAmigo.SelecionarPorId(id);

            if (amigo is null)
                return RedirectToAction(nameof(Listar));

            ViewBag.Nome = amigo.Nome;

            var vms = amigo.Emprestimos.Where(e => e.StatusMulta != StatusMulta.SemMulta).Select(e => new AmigoMultasViewModel(e.Revista.Titulo, e.CalcularMulta(), e.StatusMulta)).ToList();

            return View(vms);
        }
    }
}

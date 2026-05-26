using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
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
                .Select(a => new AmigoViewModel(a.Nome, a.NomeResponsavel, a.Telefone, a.Id)).ToList();

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

            if (amigo == null)
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
            if (repositorioAmigo.SelecionarTodos().Any(a => a.Nome == vm.Nome && a.Telefone == vm.Telefone))
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

            if (amigo == null)
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

            if (amigo != null)
                repositorioAmigo.Excluir(amigo);

            return RedirectToAction(nameof(Listar));
        }
    }
}

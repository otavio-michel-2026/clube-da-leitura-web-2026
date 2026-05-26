using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao
{
    [Route("Amigos")]
    public class AmigoController : Controller
    {
        private readonly IRepositorioAmigo repositorioAmigo;

        public AmigoController(IRepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
        }

        [HttpGet("Listar")]
        public ActionResult Listar()
        {
            var vms = repositorioAmigo.SelecionarTodos()
                .Select(a => new AmigoViewModel(a.Nome, a.NomeResponsavel, a.Telefone, a.Id)).ToList();

            return View(vms);
        }

        [HttpGet("Cadastrar")]
        public ActionResult Cadastrar()
        {
            AmigoViewModel vm = new(string.Empty, string.Empty, string.Empty);

            return View(vm);
        }

        [HttpPost("Cadastrar")]
        public ActionResult Cadastrar(AmigoViewModel vm)
        {
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

        [HttpGet("Editar")]
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

        [HttpPost("Editar")]
        public ActionResult Editar(AmigoViewModel vm)
        {
            Amigo amigoEditado = new(
                vm.Nome,
                vm.NomeResponsavel,
                vm.Telefone
            );

            repositorioAmigo.Editar(vm.Id, amigoEditado);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet("Excluir")]
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

        [HttpPost("Excluir")]
        public ActionResult Excluir(AmigoViewModel vm)
        {
            Amigo? amigo = repositorioAmigo.SelecionarPorId(vm.Id);

            if (amigo != null)
                repositorioAmigo.Excluir(amigo);

            return RedirectToAction(nameof(Listar));
        }
    }
}

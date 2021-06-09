using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReservaUnica.Models;
using System.IO;
using System.Text.Json;

namespace ReservaUnica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _filePath;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _filePath = _hostingEnvironment.WebRootPath;
        }

        public IActionResult Index()
        {
            CadastroPontosReservaViewModel cadastroPontosReservaViewModel = new CadastroPontosReservaViewModel();
            if (System.IO.File.Exists(Path.Combine(_filePath, "ReservaUnicaDB.json")))
            {
                //se existir um registro eu habilito o botão de atualização
                cadastroPontosReservaViewModel.Id = 1;
            }
            return View(cadastroPontosReservaViewModel);
        }

        public IActionResult Cadastrar()
        {
            CadastroPontosReservaViewModel cadastroPontosReservaViewModel = new CadastroPontosReservaViewModel();            
            return View("~/Views/Home/Cadastrar.cshtml", cadastroPontosReservaViewModel);
        }

        [HttpPost]
        public IActionResult SalvarCadastroPontos(CadastroPontosReservaViewModel cadastroPontosReservaViewModel)
        {
            //aqui iria no serviço e salvaria os dados no banco
            //para esse exemplo farei gravar num arquivo json
            cadastroPontosReservaViewModel.Id = 1;

            CadastroPontosReservaViewModel pontos = cadastroPontosReservaViewModel;

            foreach (var node in cadastroPontosReservaViewModel.LstNodeDataArray)
            {
                if (!string.IsNullOrEmpty(node.X_Y))
                {
                    var arrayX_Y = new string[2];
                    arrayX_Y = node.X_Y.Split(" ");
                    node.X = arrayX_Y[0];
                    node.Y = arrayX_Y[1];
                }
            }

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(pontos, options);
            System.IO.File.WriteAllText(Path.Combine(_filePath, "ReservaUnicaDB.json"), json);

            return Json(pontos);
            //return RedirectToRoute(new { Controller = "Home", Action = "Editar", cadastroPontosReservaViewModel.Id });
        }

        public IActionResult Editar(int id)
        {
            CadastroPontosReservaViewModel cadastroPontosReservaViewModel = new CadastroPontosReservaViewModel();
            //aqui deveria buscar no banco o id para edição e retornar, porém gravei em arquivo json para facilitar
            if (id > 0 && System.IO.File.Exists(Path.Combine(_filePath, "ReservaUnicaDB.json")))
            {
                var json = System.IO.File.ReadAllText(Path.Combine(_filePath, "ReservaUnicaDB.json"));

                var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                    WriteIndented = true
                };
                cadastroPontosReservaViewModel = JsonSerializer.Deserialize<CadastroPontosReservaViewModel>(json, options);
                //como os dados do json são substituídos toda vez que salva, não precisei pesquisar pelo id
            }
            return View(cadastroPontosReservaViewModel);
        }
    }
}

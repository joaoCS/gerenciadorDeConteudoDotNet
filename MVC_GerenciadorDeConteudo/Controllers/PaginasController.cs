using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NVelocity;
using NVelocity.App;
using System.Text;
using System.IO;

namespace MVC_GerenciadorDeConteudo.Controllers
{
    public class PaginasController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Paginas = new Pagina().Lista();
            return View();
        }

        public ActionResult Novo()
        {
           
            return View();
        }
    
        [HttpPost]      
        public void Criar()
        {
            DateTime data;
            DateTime.TryParse(Request["data"], out data);

            var pagina = new Pagina();
            pagina.Nome = Request["nome"];
            pagina.Conteudo = Request["conteudo"];
            pagina.Data = data;

            pagina.Save();

            Response.Redirect("/paginas");
        }

        public void Excluir(int id)
        {
            Pagina.Excluir(id);
            Response.Redirect("/paginas");
        }

        public ActionResult Editar(int id)
        {
            var pagina = Pagina.BuscaPorId(id);
            ViewBag.Pagina = pagina;

            return View();
        }

        public ActionResult Preview(int id)
        {
            var pagina = Pagina.BuscaPorId(id);
            ViewBag.Pagina = pagina;

            return View();
        }
        
        public ActionResult PreviewDinamico(int id)
        {
            var pagina = Pagina.BuscaPorId(id);
            Velocity.Init();

            var modelo = new
            {
                Header = "Lista de dados dinamicos",
                Itens = new[]
                {
                    new { ID = 1, Nome = "Texto 001", Negrito = false },
                    new { ID = 2, Nome = "Texto 002", Negrito = true },
                    new { ID = 3, Nome = "Texto 003", Negrito = false }
                }
            };

            var velocityContext = new VelocityContext();
            velocityContext.Put("model", modelo);
            velocityContext.Put("paginas", new Pagina().Lista());

            var html = new StringBuilder();
            bool result = Velocity.Evaluate(velocityContext, new StringWriter(html), "NomeParaCapturarLogError", new StringReader(pagina.Conteudo));

            ViewBag.Html = html.ToString();

            return View();
        }

        public string PreviewDinamicoNoTema(int id)
        {
            var pagina = Pagina.BuscaPorId(id);
            Velocity.Init();

            var modelo = new
            {
                Header = "Lista de dados dinamicos",
                Itens = new[]
                {
                    new { ID = 1, Nome = "Texto 001", Negrito = false },
                    new { ID = 2, Nome = "Texto 002", Negrito = true },
                    new { ID = 3, Nome = "Texto 003", Negrito = false }
                }
            };
            
            var velocityContext = new VelocityContext();
            velocityContext.Put("model", modelo);
            velocityContext.Put("paginas", new Pagina().Lista());

            var html = new StringBuilder();
            bool result = Velocity.Evaluate(velocityContext, new StringWriter(html), "NomeParaCapturarLogError", new StringReader(pagina.Conteudo));
            
            return html.ToString();
        }

        [HttpPost]
        [ValidateInput(false)]
        public void Alterar(int id)
        {
            try
            {
                Pagina pagina = Pagina.BuscaPorId(id);

                DateTime data;
                DateTime.TryParse(Request["data"], out data);

                pagina.Nome = Request["nome"];
                pagina.Conteudo = Request["conteudo"];
                pagina.Data = data;

                pagina.Save();

                TempData["sucesso"] = "Página alterada com sucesso!";
            }
            catch(Exception e)
            {
                TempData["erro"] = "Página não pôde ser alterada! (" + e.Message + ")";
            }

            Response.Redirect("/paginas");
        }
    }
}
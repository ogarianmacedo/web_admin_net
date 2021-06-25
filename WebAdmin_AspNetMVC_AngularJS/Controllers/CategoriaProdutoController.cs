using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.DAO;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    [Authorize]
    public class CategoriaProdutoController : Controller
    {
        private CategoriaProdutoDAO categoriaDAO;

        /**
         * Construtor 
         */
        public CategoriaProdutoController(CategoriaProdutoDAO categoriaDAO)
        {
            this.categoriaDAO = categoriaDAO;
        }

        /**
         * View de listagem de categorias
         */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Retorna lista de categorias
         */
        public JsonResult GetCategoriasProduto()
        {
            IList<CategoriaProduto> categorias = categoriaDAO.Listar();
            return Json(categorias, JsonRequestBehavior.AllowGet);
        }

        /**
         * Adiciona nova categoria
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Adiciona(CategoriaProduto categoria)
        {
            try
            {
                categoriaDAO.Adiciona(categoria);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        /**
         * Excluir
         */
        [HttpPost]
        public JsonResult Excluir(int Id)
        {
            var categoria = categoriaDAO.BuscaPorId(Id);
            if (categoria == null)
            {
                return Json(new { success = false });
            }
            else
            {
                categoriaDAO.Excluir(categoria);
                return Json(new { success = true });
            }
        }
    }
}
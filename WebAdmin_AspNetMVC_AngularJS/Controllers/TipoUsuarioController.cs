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
    public class TipoUsuarioController : Controller
    {
        private TipoUsuarioDAO tipoUsuarioDAO;

        /**
         * Construtor 
         */
        public TipoUsuarioController(TipoUsuarioDAO tipoUsuarioDAO)
        {
            this.tipoUsuarioDAO = tipoUsuarioDAO;
        }

        /**
         * View de listagem de tipos usuário
         */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Retorna lista de tipos usuário
         */
        public JsonResult GetTiposUsuario()
        {
            IList<TipoUsuario> tipos = tipoUsuarioDAO.Listar();
            return Json(tipos, JsonRequestBehavior.AllowGet);
        }

        /**
         * Adicona novo tipo usuário
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Adiciona(TipoUsuario tipoUsuario)
        {
            try
            {
                tipoUsuarioDAO.Adiciona(tipoUsuario);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        /**
         * Excluir tipo usuário
         */
        [HttpPost]
        public JsonResult Excluir(int Id)
        {
            var tipo = tipoUsuarioDAO.BuscaPorId(Id);
            if (tipo == null)
            {
                return Json(new { success = false });
            }
            else
            {
                tipoUsuarioDAO.Excluir(tipo);
                return Json(new { success = true });
            }
        }   
    }
}
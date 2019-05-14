using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAdmin.DAO;
using WebAdmin.Models;
using WebMatrix.WebData;

namespace WebAdmin.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private UsuarioDAO usuarioDAO;
        private TipoUsuarioDAO tipoUsuarioDAO;

        /**
         * Construtor
         */
        public UsuarioController(UsuarioDAO usuarioDAO, TipoUsuarioDAO tipoUsuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;
            this.tipoUsuarioDAO = tipoUsuarioDAO;
        }

        /**
         * View de listagem de usuarios
         */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Retorna lista de usuarios
         */
        public JsonResult GetUsuarios()
        {
            IList<Usuario> usuarios = usuarioDAO.Listar();
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        /**
         * Retorna lista de tipos de usuario
         */
        public JsonResult GetTipoUsuario()
        {
            IList<TipoUsuario> tipoUsuarios = tipoUsuarioDAO.Listar();
            return Json(tipoUsuarios, JsonRequestBehavior.AllowGet);
        }

        /**
         * Adiciona novo usuario
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Adiciona(UsuarioAcesso usuarioAcesso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(usuarioAcesso.Email, usuarioAcesso.Senha, new { Nome = usuarioAcesso.Nome, Email = usuarioAcesso.Email, TipoUsuarioId = usuarioAcesso.TipoUsuarioId, Imagem = usuarioAcesso.Imagem });
                    return Json(new { success = true });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("usuario.Invalido", e.Message);
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }
        }

        /**
         * Excluir usuário
         */
        [HttpPost]
        public JsonResult Excluir(int Id)
        {
            var usuario = usuarioDAO.BuscaPorId(Id);
            if(usuario == null)
            {
                return Json(new { success = false });
            }
            else
            {
                usuarioDAO.Excluir(usuario);
                return Json(new { success = true });
            }
        }

        /**
         * Editar usuário
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Editar(Usuario usuario)
        {
            var dados = usuarioDAO.BuscaPorId(usuario.Id);
            if (dados == null)
            {
                return Json(new { success = false });
            }
            else
            {
                usuarioDAO.Editar(usuario);
                return Json(new { success = true });
            }
        }

        /**
         * Busca usuario por id
         */
        public JsonResult GetUsuario(int Id)
        {
            var usuario = usuarioDAO.BuscaPorId(Id);
            return Json(usuario, JsonRequestBehavior.AllowGet);
        }


        /**
         * Armazena imagem no diretorio /Upload/Usuario
         */
        [HttpPost]
        public async Task<JsonResult> UploadFile(string nameFile)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        var path = Path.Combine(Server.MapPath("~/Upload/Usuario"), nameFile);
                        fileContent.SaveAs(path);
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

    }
}
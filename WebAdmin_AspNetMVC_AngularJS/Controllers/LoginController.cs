using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.DAO;
using WebAdmin.Models;
using WebMatrix.WebData;

namespace WebAdmin.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioDAO usuarioDAO;

        /**
         * Construtor
         */
        public LoginController(UsuarioDAO usuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;
        }

        /**
         * View de login
         */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Autenticar usuario/ fazer login na aplicação
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(string email, string senha)
        {
            if (WebSecurity.Login(email, senha))
            {
                Usuario usuario = usuarioDAO.Busca(email);
                Session["nomeUsuario"] = usuario.Nome;
                Session["tipoUsuario"] = usuario.TipoUsuario.Nome;
                Session["imagemUsuario"] = usuario.Imagem;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("login.Invalido", "E-mail ou senha incorretos");
                return View("Index");
            }
        }

        /**
         * Fazer logout na aplicação
         */
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index");
        }
    }
}
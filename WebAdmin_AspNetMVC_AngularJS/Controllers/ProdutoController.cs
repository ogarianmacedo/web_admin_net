using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAdmin.DAO;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private ProdutoDAO produtoDAO;

        /**
         * Construtor 
         */
        public ProdutoController(ProdutoDAO produtoDAO)
        {
            this.produtoDAO = produtoDAO;
        }

        /**
         * Retorna listagem de produtos
         */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Listar Produtos
         */
        public JsonResult GetProdutos()
        {
            IList<Produto> produtos = produtoDAO.Listar();
            return Json(produtos, JsonRequestBehavior.AllowGet);
        }

        /**
         * Adiciona novo produto
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Adiciona(Produto produto)
        {
            try
            {
                produto.DataEntrada = DateTime.Now;
                produtoDAO.Adiciona(produto);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        /**
        * Editar produto
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Editar(Produto produto)
        {
            var dados = produtoDAO.BuscaPorId(produto.Id);
            if(dados == null)
            {
                return Json(new { success = false });
            }
            else
            {
                produtoDAO.Editar(produto);
                return Json(new { success = true });
            }
        }

        /**
         * Excluir produto
         */
        public JsonResult Excluir(int Id)
        {
            var produto = produtoDAO.BuscaPorId(Id);
            if (produto == null)
            {
                return Json(new { success = false });
            }
            else
            {
                produtoDAO.Excluir(produto);
                return Json(new { success = true });
            }
        }

        /**
         * Buscar produto por id
         */
        public JsonResult GetProduto(int Id)
        {
            var produto = produtoDAO.BuscaPorId(Id);
            return Json(produto, JsonRequestBehavior.AllowGet);
        }

        /**
         * Retorna view de adicionar imagens
         */
        public ActionResult AdicionarImagens(string Nome)
        {
            return View();
        }

        /**
        * Retorna produto por nome
        */
        public JsonResult GetProdutoPorNome(Produto produtoModel)
        {
            var produto = produtoDAO.BuscaPorNome(produtoModel.Nome);
            return Json(produto, JsonRequestBehavior.AllowGet);
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
                        var path = Path.Combine(Server.MapPath("~/Upload/Produto"), nameFile);
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

        /**
         * Adiciona dados da imagem
         */
        [HttpPost]
        public JsonResult AdicionaDadosImagem(string Nome, int Id)
        {
            try
            {
                produtoDAO.AdicionaDadosImagem(Nome, Id);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }

        /**
         * Busca imagens do produto por produtoId
         */
        [HttpPost]
        public JsonResult BuscaImagensProduto(int Id)
        {
            try
            {
                var imagens = produtoDAO.BuscaImagensProduto(Id);
                return Json(imagens, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        /**
         * Exclui imagens 
         */
        public JsonResult ExcluirImagem(int Id)
        {
            var imagem = produtoDAO.BuscaImagemPorId(Id);
            if (imagem == null)
            {
                return Json(new { success = false });
            }
            else
            {
                produtoDAO.ExcluirImagem(imagem);
                return Json(new { success = true });
            }

        }
    }
}
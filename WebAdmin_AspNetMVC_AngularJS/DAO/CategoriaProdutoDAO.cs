using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdmin.Models;

namespace WebAdmin.DAO
{
    public class CategoriaProdutoDAO
    {
        private WebAdminContext context;

        /**
         * Construtor
         */
        public CategoriaProdutoDAO(WebAdminContext context)
        {
            this.context = context;
        }

        /**
         * Retorna lista de categorias
         */
        public IList<CategoriaProduto> Listar()
        {
            return context.CategoriaProdutos.ToList();
        }

        /**
         * Adiciona nova categoria
         */
        public void Adiciona(CategoriaProduto categoria)
        {
            context.CategoriaProdutos.Add(categoria);
            context.SaveChanges();
        }

        /**
         * Busca categoria por id
         */
        public CategoriaProduto BuscaPorId(int Id)
        {
            return context.CategoriaProdutos.Where(c => c.Id == Id).FirstOrDefault();
        }

        /**
         * Excluir
         */
        public void Excluir(CategoriaProduto categoria)
        {
            context.CategoriaProdutos.Remove(categoria);
            context.SaveChanges();
        }
    }
}
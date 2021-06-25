using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdmin.Models;

namespace WebAdmin.DAO
{
    public class ProdutoDAO
    {
        private WebAdminContext context;

        /**
         * Construtor
         */
        public ProdutoDAO(WebAdminContext context)
        {
            this.context = context;
        } 

        /**
         * Listar Produtos
         */
        public IList<Produto> Listar()
        {
            return context.Produtos.ToList();
        }

        /**
         * Adiciona novo produto
         */
        public void Adiciona(Produto produto)
        {
            context.Produtos.Add(produto);
            context.SaveChanges();
        }

        /**
         * Editar produto
         */
        public void Editar(Produto produto)
        {
            var produtoModel = context.Produtos.Where(p => p.Id == produto.Id).FirstOrDefault();

            produtoModel.Nome = produto.Nome;
            produtoModel.Descricao = produto.Descricao;
            produtoModel.ValorCusto = produto.ValorCusto;
            produtoModel.ValorPromocao = produto.ValorPromocao;
            produtoModel.ValorVenda = produto.ValorVenda;
            produtoModel.Quantidade = produto.Quantidade;
            produtoModel.StPromocao = produto.StPromocao;
            produtoModel.CategoriaProdutoId = produto.CategoriaProdutoId;

            context.SaveChanges();
        }

        /**
         * Busca produto por id
         */
        public Produto BuscaPorId(int Id)
        {
            return context.Produtos.Include("CategoriaProduto").Where(p => p.Id == Id).FirstOrDefault();
        }

        /**
         * Excluir produto
         */
        public void Excluir(Produto produto)
        {
            context.Produtos.Remove(produto);
            context.SaveChanges();
        }

        /**
         * Retorna produto por nome
         */
        public Produto BuscaPorNome(string Nome)
        {
            return context.Produtos.Where(p => p.Nome == Nome).FirstOrDefault();
        }

        /**
         * Adiciona dados da imagem
         */
        public void AdicionaDadosImagem(string Nome, int Id)
        {
            ImagemProduto imagem = new ImagemProduto();
            imagem.Imagem = Nome;
            imagem.ProdutoId = Id;
            imagem.StPrincipal = false;

            context.ImagemProdutos.Add(imagem);
            context.SaveChanges();
        }

        /**
         * Busca imagens por produtoId
         */
        public IList<ImagemProduto> BuscaImagensProduto(int Id)
        {
            return context.ImagemProdutos.Where(img => img.ProdutoId == Id).ToList();
        }

        /**
         * Busca imagem por Id
         */
        public ImagemProduto BuscaImagemPorId(int Id)
        {
            return context.ImagemProdutos.Where(img => img.Id == Id).FirstOrDefault();
        }

        /**
         * Exclui imagem
         */
        public void ExcluirImagem(ImagemProduto imagem)
        {
            context.ImagemProdutos.Remove(imagem);
            context.SaveChanges();
        }
    }
}
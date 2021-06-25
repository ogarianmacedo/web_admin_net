using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    [Table("ImagemProdutos")]
    public class ImagemProduto
    {
        public int Id { get; set; }

        public string Imagem { get; set; }

        public bool StPrincipal { get; set; }

        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
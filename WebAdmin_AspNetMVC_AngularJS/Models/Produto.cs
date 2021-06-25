using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal ValorCusto { get; set; }

        public decimal ValorVenda { get; set; }

        public decimal ValorPromocao { get; set; }

        public DateTime DataEntrada { get; set; }

        public int Quantidade { get; set; }

        public bool StPromocao { get; set; }

        public int CategoriaProdutoId { get; set; }

        public virtual CategoriaProduto CategoriaProduto { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; } 

        public int TipoUsuarioId { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        public string Imagem { get; set; }
    }
}
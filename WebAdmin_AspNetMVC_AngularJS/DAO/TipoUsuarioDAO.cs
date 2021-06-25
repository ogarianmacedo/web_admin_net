using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdmin.Models;

namespace WebAdmin.DAO
{
    public class TipoUsuarioDAO
    {
        private WebAdminContext context;

        /**
         * Construtor
         */
        public TipoUsuarioDAO(WebAdminContext context)
        {
            this.context = context;
        }

        /**
         * Retorna lista de tipos de usuario
         */
        public IList<TipoUsuario> Listar()
        {
            return context.TipoUsuarios.ToList();
        }

        /**
         * Adiciona novo tipo usuario
         */
        public void Adiciona(TipoUsuario tipoUsuario)
        {
            context.TipoUsuarios.Add(tipoUsuario);
            context.SaveChanges();
        }

        /**
         *  Busca tipo usuário por id
         */
        public TipoUsuario BuscaPorId(int Id)
        {
            return context.TipoUsuarios.Where(t => t.Id == Id).FirstOrDefault();
        }

        /**
         * Excluir tipo usuário
         */
        public void Excluir(TipoUsuario tipo)
        {
            context.TipoUsuarios.Remove(tipo);
            context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdmin.Models;
using WebMatrix.WebData;

namespace WebAdmin.DAO
{
    public class UsuarioDAO
    {
        private WebAdminContext context;
        private UsuarioAcesso usuarioAcesso;

        /**
         * Construtor
         */
        public UsuarioDAO(WebAdminContext context, UsuarioAcesso usuarioAcesso)
        {
            this.context = context;
            this.usuarioAcesso = usuarioAcesso;
        }

        /**
         * Adiciona novo usuário
         */
        public void Adicionar(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        /**
         * Lista usuários
         */
        public IList<Usuario> Listar()
        {
            return context.Usuarios.ToList();
        }

        /**
         * Busca usuário logado
         */
        public Usuario Busca(string email)
        {
            return context.Usuarios.First(u => u.Email == email);
        }

        /**
         *  Busca usuário por id
         */
        public Usuario BuscaPorId(int Id)
        {
            return context.Usuarios.Include("TipoUsuario").Where(u => u.Id == Id).FirstOrDefault();
        }

        /**
         * Excluir usuário
         */
        public void Excluir(Usuario usuario)
        {
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
        }

        /**
         * Editar usuário
         */
        public void Editar(Usuario usuario)
        {
            var usuarioModel = context.Usuarios.Where(u => u.Id == usuario.Id).FirstOrDefault();

            usuarioModel.Nome = usuario.Nome;
            usuarioModel.Email = usuario.Email;
            usuarioModel.TipoUsuarioId = usuario.TipoUsuarioId;
            usuarioModel.Imagem = usuario.Imagem;

            context.SaveChanges();   
        }
    }
}
using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Helpers;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories
{
    public class UsuarioRepositorio : Repository<Usuario>, IUsuarioRepositorio
    {
        private readonly AppDbContext _db;
        private readonly TokenJwtHelper _tokenJwtHelper;
        public UsuarioRepositorio(AppDbContext db, IConfiguration configuration) :base(db)
        {
            _db = db;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }
        public bool IsUsuarioUnico (string userName)
        {
            var usuario= _db.Usuarios.FirstOrDefault(u => u.NombreUsuario.ToLower() == userName.ToLower());
            if(usuario == null)
            {
                return true;
            }
            return false;
        }

        public async Task<Usuario> Registrar(UsuarioCreateDTO usuarioCreateDTO)
        {
            Usuario usuario = new()
            {
                Nombre = usuarioCreateDTO.Nombre,
                Apellido = usuarioCreateDTO.Apellido,
                NombreUsuario = usuarioCreateDTO.NombreUsuario,
                Contraseña = usuarioCreateDTO.Contraseña,
                Mail = usuarioCreateDTO.Mail,
            };
            await _db.AddAsync(usuario);
            await _db.SaveChangesAsync();
            usuario.Contraseña = "";
            return usuario;
        } 
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == loginRequestDTO.UserName.ToLower() &&
                                                                    u.Contraseña == loginRequestDTO.Password);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }
            //Si el usuario existe generamos JW Token
            var token = _tokenJwtHelper.GenerateToken(user);
            LoginResponseDTO loginResponseDTO = new()
            
            {
                Token = token,
                Usuario = user,
            };
            return loginResponseDTO;
        }

        public async Task<Usuario> Update(Usuario entity)
        {
           
            _db.Usuarios.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        
    }
}

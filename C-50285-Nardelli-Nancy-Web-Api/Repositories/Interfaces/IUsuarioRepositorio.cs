using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces
{
    public interface IUsuarioRepositorio
    {
        public bool IsUsuarioUnico(string UserName);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<Usuario> Registrar(UsuarioCreateDTO usuarioCreateDTO);
        Task<Usuario> Update(Usuario entity);
    }
}

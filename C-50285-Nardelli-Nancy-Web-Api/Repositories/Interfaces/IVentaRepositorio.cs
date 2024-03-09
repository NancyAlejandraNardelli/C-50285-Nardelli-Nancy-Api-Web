using C_50285_Nardelli_Nancy_Web_Api.Models;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces
{
    public interface IVentaRepositorio
    {
        Task<List<Ventum>> GetVentasByUserId(int idUsuario);

    }
}

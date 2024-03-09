using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces
{
    public interface IProductoVendidoRepositorio
    {
        Task<List<ProductoVendido>> GetProductosVendidosPorUsuario(int idUsuario);
    }
}

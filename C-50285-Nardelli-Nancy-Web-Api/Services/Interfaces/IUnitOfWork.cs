using C_50285_Nardelli_Nancy_Web_Api.Repositories;

namespace C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public UsuarioRepositorio UsuarioRepositorio { get; }
        public ProductoRepositorio ProductoRepositorio { get; }
        public ProductoVendidoRepositorio ProductoVendidoRepositorio { get; }
        public VentaRepositorio VentaRepositorio { get; }

        Task<int> Complete();
    }
}

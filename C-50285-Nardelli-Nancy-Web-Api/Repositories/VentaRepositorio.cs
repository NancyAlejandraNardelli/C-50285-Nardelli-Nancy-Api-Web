using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories
{
    public class VentaRepositorio: Repository<Ventum>, IVentaRepositorio
    {
        private readonly AppDbContext _db;
        public VentaRepositorio(AppDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
        }

        //public async Task<List<Ventum>> GetVentasByUserId(int idUsuario)
        //{
        //    return await _dbContext.Venta
        //        .Include(v => v.ProductoVendidos)
        //        .ThenInclude(pv => pv.IdProductoNavigation)
        //        .Where(v => v.IdUsuario == idUsuario)
        //        .ToListAsync();
        //}

        public async Task<List<Ventum>> GetVentasByUserId(int idUsuario)
        {
            return await GetAll(v => v.IdUsuario == idUsuario, includeProperties: "ProductoVendidos.IdProductoNavigation");
        }
    }
}

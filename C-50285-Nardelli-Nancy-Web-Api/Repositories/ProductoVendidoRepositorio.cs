using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories
{
    public class ProductoVendidoRepositorio: Repository<ProductoVendido>, IProductoVendidoRepositorio
    {
        private readonly AppDbContext _db;
        public ProductoVendidoRepositorio(AppDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
        }

        public async Task<List<ProductoVendido>> GetProductosVendidosPorUsuario(int idUsuario)
        {
            var productosVendidos = await (
            from pv in _db.ProductoVendidos
            join v in _db.Venta on pv.IdVenta equals v.Id into ventaJoin
            from venta in ventaJoin.DefaultIfEmpty()
            join u in _db.Usuarios on venta.IdUsuario equals u.Id into usuarioJoin
            from usuario in usuarioJoin.DefaultIfEmpty()
            join p in _db.Productos on pv.IdProducto equals p.Id into productoJoin
            from producto in productoJoin.DefaultIfEmpty()
            where usuario.Id == idUsuario
            select pv
             )
             .ToListAsync();

            return productosVendidos;
        }   
    }
}

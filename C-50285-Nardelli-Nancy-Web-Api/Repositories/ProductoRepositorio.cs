using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories
{
    public class ProductoRepositorio:Repository<Producto>, IProductoRepositorio
    {
        private readonly AppDbContext _db;
        public ProductoRepositorio(AppDbContext db, IConfiguration configuration) :base(db) 
        {
            _db = db;
        }

        public async Task<Producto> Update(Producto entity)
        {

            _db.Productos.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }


    }
}

﻿using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.Repositories;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;

namespace C_50285_Nardelli_Nancy_Web_Api.Services
{
    public class UnitOfWorkService: IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UsuarioRepositorio UsuarioRepositorio { get; private set; }
        public ProductoRepositorio ProductoRepositorio { get; private set; }
        public ProductoVendidoRepositorio ProductoVendidoRepositorio { get; private set; }

        public VentaRepositorio VentaRepositorio { get; private set; }

        public UnitOfWorkService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            UsuarioRepositorio = new UsuarioRepositorio(_context, _configuration);
            ProductoRepositorio = new ProductoRepositorio(_context, _configuration);
            ProductoVendidoRepositorio = new ProductoVendidoRepositorio(_context, _configuration);
            VentaRepositorio = new VentaRepositorio(_context, _configuration);
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

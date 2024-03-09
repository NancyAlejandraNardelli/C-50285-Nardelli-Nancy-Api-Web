using AutoMapper;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace C_50285_Nardelli_Nancy_Web_Api.Controllers
{
    public class ProductoVendidoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly ILogger<ProductoVendidoController> _logger;
        public ProductoVendidoController(ILogger<ProductoVendidoController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
            _logger = logger;
        }

        [HttpGet("api/productovendido/{idUsuario:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductoVendidoDTO>>> GetProductosVendidosPorUsuario(int idUsuario)
        {
            var productosVendidos = await _unitOfWork.ProductoVendidoRepositorio.GetProductosVendidosPorUsuario(idUsuario);

            if (productosVendidos == null || !productosVendidos.Any())
            {
                return NotFound();
            }

            // Mapear los productos vendidos y sus propiedades relacionadas al DTO
            var productosVendidosDTO = productosVendidos.Select(pv => new ProductoVendidoDTO
            {
                Id = pv.Id,
                Stock = pv.Stock,
                IdProducto = pv.IdProducto,
                IdVenta = pv.IdVenta,
                NombreProducto = pv.IdProductoNavigation?.Descripciones, // Nombre del producto
                PrecioProducto = pv.IdProductoNavigation?.PrecioVenta ?? 0, // Precio del producto
                IdUsuario = pv.IdVentaNavigation?.IdUsuario ?? 0, // ID del usuario de la venta
                NombreUsuario = pv.IdVentaNavigation?.IdUsuarioNavigation?.Nombre, // Nombre del usuario de la venta
                
                ComentariosVenta = pv.IdVentaNavigation?.Comentarios // Comentarios de la venta
            }).ToList();

            return Ok(productosVendidosDTO);
        }


    }
}

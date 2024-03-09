namespace C_50285_Nardelli_Nancy_Web_Api.DTOs
{
    public class ProductoVendidoDTO
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        // Propiedades adicionales del Producto
        public string NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }

        // Propiedades adicionales de la Venta
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ComentariosVenta { get; set; }

    }
}

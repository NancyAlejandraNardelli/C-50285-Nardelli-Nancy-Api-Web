namespace C_50285_Nardelli_Nancy_Web_Api.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal? Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }
}

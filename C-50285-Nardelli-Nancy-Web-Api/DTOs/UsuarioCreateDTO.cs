namespace C_50285_Nardelli_Nancy_Web_Api.DTOs
{
    public class UsuarioCreateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }
    }
}

using C_50285_Nardelli_Nancy_Web_Api.Models;

namespace C_50285_Nardelli_Nancy_Web_Api.DTOs
{
    public class LoginResponseDTO
    {
        public Usuario  Usuario { get; set; }
        public string Token { get; set; }
       
    }
}

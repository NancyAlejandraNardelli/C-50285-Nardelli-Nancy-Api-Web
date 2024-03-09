using AutoMapper;
using C_50285_Nardelli_Nancy_Web_Api.DTOs;
using C_50285_Nardelli_Nancy_Web_Api.Models;

namespace C_50285_Nardelli_Nancy_Web_Api
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Usuario, UsuarioCreateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<ProductoVendido, ProductoVendidoDTO>().ReverseMap();
            CreateMap<Ventum, VentaDTO>().ReverseMap();

        }
    }
}

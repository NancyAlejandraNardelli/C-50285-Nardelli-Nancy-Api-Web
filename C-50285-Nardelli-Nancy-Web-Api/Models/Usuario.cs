﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C_50285_Nardelli_Nancy_Web_Api.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Productos = new HashSet<Producto>();
            Venta = new HashSet<Ventum>();
        }
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}

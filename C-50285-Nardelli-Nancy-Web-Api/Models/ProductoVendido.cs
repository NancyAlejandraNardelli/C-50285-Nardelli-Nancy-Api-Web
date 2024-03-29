﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C_50285_Nardelli_Nancy_Web_Api.Models
{
    public partial class ProductoVendido
    {
        [Key]
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}

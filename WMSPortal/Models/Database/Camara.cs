using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Camara
    {
        public Camara()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCamara { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Ip { get; set; }
        public int? IdUbicacion { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}

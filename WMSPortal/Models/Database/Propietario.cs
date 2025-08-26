using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Propietario
    {
        public Propietario()
        {
            ClienteTipos = new HashSet<ClienteTipo>();
            Clientes = new HashSet<Cliente>();
            INavConfigEncs = new HashSet<INavConfigEnc>();
            MotivoDevolucions = new HashSet<MotivoDevolucion>();
            ProductoClasificacions = new HashSet<ProductoClasificacion>();
            ProductoEstados = new HashSet<ProductoEstado>();
            ProductoFamilia = new HashSet<ProductoFamilium>();
            ProductoMarcas = new HashSet<ProductoMarca>();
            ProductoTipos = new HashSet<ProductoTipo>();
            Productos = new HashSet<Producto>();
            PropietarioBodegas = new HashSet<PropietarioBodega>();
            PropietarioReglasEncs = new HashSet<PropietarioReglasEnc>();
            Proveedors = new HashSet<Proveedor>();
            TareaHhs = new HashSet<TareaHh>();
            TmsTickets = new HashSet<TmsTicket>();
            TransServicioDets = new HashSet<TransServicioDet>();
            TransServicioEncs = new HashSet<TransServicioEnc>();
            UnidadMedida = new HashSet<UnidadMedidum>();
        }

        public int IdPropietario { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoActualizacionCosto { get; set; }
        public string Contacto { get; set; }
        public string NombreComercial { get; set; }
        public byte[] Imagen { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public string Email { get; set; }
        public bool? ActualizaCostoOc { get; set; }
        public int? Color { get; set; }
        public string Codigo { get; set; }
        public bool? Sistema { get; set; }
        public string Nit { get; set; }
        public string CodigoAcceso { get; set; }
        public string ClaveAcceso { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual TipoActualizacionCosto IdTipoActualizacionCostoNavigation { get; set; }
        public virtual ICollection<ClienteTipo> ClienteTipos { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<INavConfigEnc> INavConfigEncs { get; set; }
        public virtual ICollection<MotivoDevolucion> MotivoDevolucions { get; set; }
        public virtual ICollection<ProductoClasificacion> ProductoClasificacions { get; set; }
        public virtual ICollection<ProductoEstado> ProductoEstados { get; set; }
        public virtual ICollection<ProductoFamilium> ProductoFamilia { get; set; }
        public virtual ICollection<ProductoMarca> ProductoMarcas { get; set; }
        public virtual ICollection<ProductoTipo> ProductoTipos { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<PropietarioBodega> PropietarioBodegas { get; set; }
        public virtual ICollection<PropietarioReglasEnc> PropietarioReglasEncs { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
        public virtual ICollection<TareaHh> TareaHhs { get; set; }
        public virtual ICollection<TmsTicket> TmsTickets { get; set; }
        public virtual ICollection<TransServicioDet> TransServicioDets { get; set; }
        public virtual ICollection<TransServicioEnc> TransServicioEncs { get; set; }
        public virtual ICollection<UnidadMedidum> UnidadMedida { get; set; }
    }
}

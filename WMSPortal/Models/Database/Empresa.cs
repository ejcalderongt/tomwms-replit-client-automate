using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class Empresa
    {
        public Empresa()
        {
            Bodegas = new HashSet<Bodega>();
            Clientes = new HashSet<Cliente>();
            EmpresaTransportes = new HashSet<EmpresaTransporte>();
            INavConfigEncs = new HashSet<INavConfigEnc>();
            Impresoras = new HashSet<Impresora>();
            MotivoAnulacions = new HashSet<MotivoAnulacion>();
            MotivoDevolucions = new HashSet<MotivoDevolucion>();
            MotivoUbicacions = new HashSet<MotivoUbicacion>();
            Operadors = new HashSet<Operador>();
            Propietarios = new HashSet<Propietario>();
            Proveedors = new HashSet<Proveedor>();
            Rols = new HashSet<Rol>();
            Tarimas = new HashSet<Tarima>();
            TransServicioEncs = new HashSet<TransServicioEnc>();
            TransaccionesLogs = new HashSet<TransaccionesLog>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string RazonSocial { get; set; }
        public string Representante { get; set; }
        public long? CorrCodBarra { get; set; }
        public string PathPrinter { get; set; }
        public bool? Activo { get; set; }
        public string UserAgr { get; set; }
        public DateTime? FecAgr { get; set; }
        public string UserMod { get; set; }
        public DateTime? FecMod { get; set; }
        public bool? ClienteRapido { get; set; }
        public byte[] Imagen { get; set; }
        public bool? OperadorLogistico { get; set; }
        public int? PuertoEscaner { get; set; }
        public bool? ControlPresentaciones { get; set; }
        public bool? AnulacionesPorSupervisor { get; set; }
        public string Codigo { get; set; }
        public string Clave { get; set; }
        public int? Intento { get; set; }
        public int? Duracionclave { get; set; }
        public int? Duracionclavetemporal { get; set; }
        public bool? CodigoAutomatico { get; set; }
        public bool? PoliticaContraseñas { get; set; }
        public int? IdMotivoAjusteInventario { get; set; }
        public int? CantidadDecimalesDespliegue { get; set; }
        public int? CantidadDecimalesCalculo { get; set; }
        public double? MinutosTimerJornadaSistema { get; set; }
        public DateTime? HoraCorteJornadaSistema { get; set; }
        public bool? GenerarStockJornada { get; set; }

        public virtual ICollection<Bodega> Bodegas { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<EmpresaTransporte> EmpresaTransportes { get; set; }
        public virtual ICollection<INavConfigEnc> INavConfigEncs { get; set; }
        public virtual ICollection<Impresora> Impresoras { get; set; }
        public virtual ICollection<MotivoAnulacion> MotivoAnulacions { get; set; }
        public virtual ICollection<MotivoDevolucion> MotivoDevolucions { get; set; }
        public virtual ICollection<MotivoUbicacion> MotivoUbicacions { get; set; }
        public virtual ICollection<Operador> Operadors { get; set; }
        public virtual ICollection<Propietario> Propietarios { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
        public virtual ICollection<Rol> Rols { get; set; }
        public virtual ICollection<Tarima> Tarimas { get; set; }
        public virtual ICollection<TransServicioEnc> TransServicioEncs { get; set; }
        public virtual ICollection<TransaccionesLog> TransaccionesLogs { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}

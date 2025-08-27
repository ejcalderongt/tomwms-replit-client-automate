using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwFiscalMercaVencidum
    {
        public int IdOrdenCompraEnc { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public int IdStock { get; set; }
        public int? IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Material { get; set; }
        public int IdPropietario { get; set; }
        public string NombreCliente { get; set; }
        public int? IdProductoBodega { get; set; }
        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; }
        public string NumeroOrden { get; set; }
        public int Regimen { get; set; }
        public string CodigoRegimen { get; set; }
        public int DiasRegimen { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Poliza { get; set; }
        public int IdPresentacion { get; set; }
        public string Presentacion { get; set; }
        public string CodigoBarra { get; set; }
        public double CantidadPresentacion { get; set; }
        public double CantidadUmbas { get; set; }
        public double CantidadReservada { get; set; }
        public double Disponible { get; set; }
        public double? Peso { get; set; }
        public string UnidadPeso { get; set; }
        public int? DiasVida { get; set; }
    }
}

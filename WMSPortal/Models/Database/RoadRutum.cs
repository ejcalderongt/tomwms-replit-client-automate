using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class RoadRutum
    {
        public RoadRutum()
        {
            TransDespachoEncs = new HashSet<TransDespachoEnc>();
            TransTrasEncs = new HashSet<TransTrasEnc>();
        }

        public int IdRuta { get; set; }
        public int? IdPropietarioBodega { get; set; }
        public int? IdUbicacionTransito { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Activo { get; set; }
        public string Vendedor { get; set; }
        public string Venta { get; set; }
        public string Forania { get; set; }
        public string Sucursal { get; set; }
        public string Tipo { get; set; }
        public string Subtipo { get; set; }
        public string Bodega { get; set; }
        public string Subbodega { get; set; }
        public string Descuento { get; set; }
        public string Bonif { get; set; }
        public string Kilometraje { get; set; }
        public string Impresion { get; set; }
        public string Recibopropio { get; set; }
        public string Celular { get; set; }
        public string Rentabil { get; set; }
        public string Oferta { get; set; }
        public double Percrent { get; set; }
        public string Pasarcredito { get; set; }
        public string Teclado { get; set; }
        public string Editdevprec { get; set; }
        public string Editdesc { get; set; }
        public string Params { get; set; }
        public int Semana { get; set; }
        public int Objano { get; set; }
        public int Objmes { get; set; }
        public string Syncfold { get; set; }
        public string Wlfold { get; set; }
        public string Ftpfold { get; set; }
        public string Email { get; set; }
        public int Lastimp { get; set; }
        public int Lastcom { get; set; }
        public int Lastexp { get; set; }
        public string Impstat { get; set; }
        public string Expstat { get; set; }
        public string Comstat { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public double Pesolim { get; set; }
        public int IntervaloMax { get; set; }
        public int LecturasValid { get; set; }
        public int IntentosLect { get; set; }
        public int HoraIni { get; set; }
        public int HoraFin { get; set; }
        public int AplicacionUsa { get; set; }
        public int PuertoGps { get; set; }
        public bool EsRutaOficina { get; set; }
        public bool DiluirBon { get; set; }
        public bool PreimpresionFactura { get; set; }

        public virtual ICollection<TransDespachoEnc> TransDespachoEncs { get; set; }
        public virtual ICollection<TransTrasEnc> TransTrasEncs { get; set; }
    }
}

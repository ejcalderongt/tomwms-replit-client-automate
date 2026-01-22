using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Trans_oc
{
    public class clsBeTrans_oc_pol : ICloneable
    {
        [Column("IdOrdenCompraPol")]
        [DisplayName("IdOrdenCompraPol")]
        public int IdOrdenCompraPol { get; set; } = 0;

        [Column("IdOrdenCompraEnc")]
        [DisplayName("IdOrdenCompraEnc")]
        public int IdOrdenCompraEnc { get; set; } = 0;

        [Column("bl_no")]
        [DisplayName("bl_no")]
        public string Bl_no { get; set; } = "";

        [Column("NoPoliza")]
        [DisplayName("NoPoliza")]
        public string NoPoliza { get; set; } = "";

        [Column("pto_descarga")]
        [DisplayName("pto_descarga")]
        public string Pto_descarga { get; set; } = "";

        [Column("viaje_no")]
        [DisplayName("viaje_no")]
        public string Viaje_no { get; set; } = "";

        [Column("buque_no")]
        [DisplayName("buque_no")]
        public string Buque_no { get; set; } = "";

        [Column("remitente")]
        [DisplayName("remitente")]
        public string Remitente { get; set; } = "";

        [Column("fecha_abordaje")]
        [DisplayName("fecha_abordaje")]
        public DateTime Fecha_abordaje { get; set; } = DateTime.Now;

        [Column("destino")]
        [DisplayName("destino")]
        public string Destino { get; set; } = "";

        [Column("dir_destino")]
        [DisplayName("dir_destino")]
        public string Dir_destino { get; set; } = "";

        [Column("descripcion")]
        [DisplayName("descripcion")]
        public string Descripcion { get; set; } = "";

        [Column("po_number")]
        [DisplayName("po_number")]
        public string Po_number { get; set; } = "";

        [Column("cantidad")]
        [DisplayName("cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("piezas")]
        [DisplayName("piezas")]
        public int Piezas { get; set; } = 0;

        [Column("total_kgs")]
        [DisplayName("total_kgs")]
        public double Total_kgs { get; set; } = 0;

        [Column("cbm")]
        [DisplayName("cbm")]
        public double Cbm { get; set; } = 0;

        [Column("dua")]
        [DisplayName("dua")]
        public string Dua { get; set; } = "";

        [Column("fecha_poliza")]
        [DisplayName("fecha_poliza")]
        public DateTime Fecha_poliza { get; set; } = DateTime.Now;

        [Column("pais_procede")]
        [DisplayName("pais_procede")]
        public string Pais_procede { get; set; } = "";

        [Column("tipo_cambio")]
        [DisplayName("tipo_cambio")]
        public double Tipo_cambio { get; set; } = 0;

        [Column("total_valoraduana")]
        [DisplayName("total_valoraduana")]
        public double Total_valoraduana { get; set; } = 0;

        [Column("total_lineas")]
        [DisplayName("total_lineas")]
        public int Total_lineas { get; set; } = 0;

        [Column("total_bultos")]
        [DisplayName("total_bultos")]
        public int Total_bultos { get; set; } = 0;

        [Column("total_bultos_peso")]
        [DisplayName("total_bultos_peso")]
        public double Total_bultos_peso { get; set; } = 0;

        [Column("total_usd")]
        [DisplayName("total_usd")]
        public double Total_usd { get; set; } = 0;

        [Column("total_flete")]
        [DisplayName("total_flete")]
        public double Total_flete { get; set; } = 0;

        [Column("total_seguro")]
        [DisplayName("total_seguro")]
        public double Total_seguro { get; set; } = 0;

        [Column("user_agr")]
        [DisplayName("user_agr")]
        public string User_agr { get; set; } = "";

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.Now;

        [Column("user_mod")]
        [DisplayName("user_mod")]
        public string User_mod { get; set; } = "";

        [Column("fec_mod")]
        [DisplayName("fec_mod")]
        public DateTime Fec_mod { get; set; } = DateTime.Now;

        [Column("codigo_poliza")]
        [DisplayName("codigo_poliza")]
        public string Codigo_poliza { get; set; } = "";

        [Column("ticket")]
        [DisplayName("ticket")]
        public string Ticket { get; set; } = "";

        [Column("numero_orden")]
        [DisplayName("numero_orden")]
        public string Numero_orden { get; set; } = "";

        [Column("fecha_aceptacion")]
        [DisplayName("fecha_aceptacion")]
        public DateTime Fecha_aceptacion { get; set; } = DateTime.Now;

        [Column("fecha_llegada")]
        [DisplayName("fecha_llegada")]
        public DateTime Fecha_llegada { get; set; } = DateTime.Now;

        [Column("total_otros")]
        [DisplayName("total_otros")]
        public double Total_otros { get; set; } = 0;

        [Column("IdRegimen")]
        [DisplayName("IdRegimen")]
        public int IdRegimen { get; set; } = 0;

        [Column("total_bultos_peso_neto")]
        [DisplayName("total_bultos_peso_neto")]
        public double Total_bultos_peso_neto { get; set; } = 0;

        [Column("clave_aduana")]
        [DisplayName("clave_aduana")]
        public string Clave_aduana { get; set; } = "";

        [Column("nit_imp_exp")]
        [DisplayName("nit_imp_exp")]
        public string Nit_imp_exp { get; set; } = "";

        [Column("clase")]
        [DisplayName("clase")]
        public string Clase { get; set; } = "";

        [Column("mod_transporte")]
        [DisplayName("mod_transporte")]
        public string Mod_transporte { get; set; } = "";

        [Column("total_liquidar")]
        [DisplayName("total_liquidar")]
        public double Total_liquidar { get; set; } = 0;

        [Column("total_general")]
        [DisplayName("total_general")]
        public double Total_general { get; set; } = 0;

        [Column("Codigo_Barra")]
        [DisplayName("Codigo_Barra")]
        public string Codigo_Barra { get; set; } = "";

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;
        public bool IsNew { get; set; } = false;
        public clsBeTrans_oc_pol() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMSWebAPI.Be
{
    public class clsBeLog_error_wms : ICloneable
    {
        [Column("IdError")]
        [DisplayName("IdError")]
        public int IdError { get; set; } = 0;

        [Column("IdEmpresa")]
        [DisplayName("IdEmpresa")]
        public int IdEmpresa { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("Fecha")]
        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column("MensajeError")]
        [DisplayName("MensajeError")]
        public string MensajeError { get; set; } = "";

        [Column("IdPedidoEnc")]
        [DisplayName("IdPedidoEnc")]
        public int IdPedidoEnc { get; set; } = 0;

        [Column("IdPickingEnc")]
        [DisplayName("IdPickingEnc")]
        public int IdPickingEnc { get; set; } = 0;

        [Column("IdRecepcionEnc")]
        [DisplayName("IdRecepcionEnc")]
        public int IdRecepcionEnc { get; set; } = 0;

        [Column("IdUsuarioAgr")]
        [DisplayName("IdUsuarioAgr")]
        public int IdUsuarioAgr { get; set; } = 0;

        [Column("Line_No")]
        [DisplayName("Line_No")]
        public int Line_No { get; set; } = 0;

        [Column("Item_No")]
        [DisplayName("Item_No")]
        public string Item_No { get; set; } = "";

        [Column("UmBas")]
        [DisplayName("UmBas")]
        public string UmBas { get; set; } = "";

        [Column("Variant_Code")]
        [DisplayName("Variant_Code")]
        public string Variant_Code { get; set; } = "";

        [Column("Cantidad")]
        [DisplayName("Cantidad")]
        public double Cantidad { get; set; } = 0;

        [Column("Referencia_Documento")]
        [DisplayName("Referencia_Documento")]
        public string Referencia_Documento { get; set; } = "";

        public clsBeLog_error_wms() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

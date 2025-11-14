using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Proveedor
{
    public class clsBeProveedor_bodega : ICloneable
    {
        [Column("IdAsignacion")]
        [DisplayName("IdAsignacion")]
        public int IdAsignacion { get; set; } = 0;

        [Column("IdProveedor")]
        [DisplayName("IdProveedor")]
        public int IdProveedor { get; set; } = 0;

        [Column("IdBodega")]
        [DisplayName("IdBodega")]
        public int IdBodega { get; set; } = 0;

        [Column("activo")]
        [DisplayName("activo")]
        public bool Activo { get; set; } = false;

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

        [Column("IdAreaOrigen")]
        [DisplayName("IdAreaOrigen")]
        public int IdAreaOrigen { get; set; } = 0;
        public clsBeProveedor Proveedor { get; set; } = new clsBeProveedor();

        public clsBeProveedor_bodega() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

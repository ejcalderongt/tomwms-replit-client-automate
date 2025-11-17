using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Stock
{
    using System;

    [Serializable]
    public partial class clsBeStock : ICloneable, IDisposable
    {
        public int IdBodega { get; set; } = 0;
        public int IdStock { get; set; } = 0;
        public int IdPropietarioBodega { get; set; } = 0;
        public int IdProductoBodega { get; set; } = 0;
        public int IdProductoEstado { get; set; } = 0;
        public int IdPresentacion { get; set; } = 0;
        public int IdUnidadMedida { get; set; } = 0;
        public int IdUbicacion { get; set; } = 0;
        public int IdUbicacion_anterior { get; set; } = 0;
        public int IdRecepcionEnc { get; set; } = 0;
        public int IdRecepcionDet { get; set; } = 0;
        public int IdPedidoEnc { get; set; } = 0;
        public int IdPickingEnc { get; set; } = 0;
        public int IdDespachoEnc { get; set; } = 0;
        public string Lote { get; set; } = "";
        public string Lic_plate { get; set; } = "";
        public string Serial { get; set; } = "";
        public double Cantidad { get; set; } = 0.0;
        public DateTime Fecha_Ingreso { get; set; } = new DateTime(1900, 1, 1);
        public DateTime Fecha_vence { get; set; } = new DateTime(1900, 1, 1);
        public double Uds_lic_plate { get; set; } = 0;
        public int No_bulto { get; set; } = 0;
        public DateTime Fecha_Manufactura { get; set; } = new DateTime(1900, 1, 1);
        public int Añada { get; set; } = 0;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public double Peso { get; set; } = 0.0;
        public double Temperatura { get; set; } = 0.0;
        public string Atributo_Variante_1 { get; set; } = "";
        public bool Pallet_No_Estandar { get; set; } = false;
        public double Cantidad_Reservada { get; set; } = 0;
        public int IdProductoTallaColor { get; set; } = 0;

        public bool IsNew { get; set; } = true;
        public bool ProductoValidado { get; set; }
        public string UbicacionAnterior { get; set; } = "";
        public clsBeProducto_presentacion Presentacion { get; set; } = new clsBeProducto_presentacion();
        public clsBeProducto_estado ProductoEstado { get; set; } = new clsBeProducto_estado();
        public System.Collections.Generic.List<clsBeStock_parametro> Parametros { get; set; } = new System.Collections.Generic.List<clsBeStock_parametro>();
        public clsBeProducto Producto { get; set; } = new clsBeProducto();
        public int IdStockOrigen { get; set; } = 0;
        public bool IsReportStockEnFecha { get; set; } = false;
        public bool UbicacionPicking { get; set; } = false;
        public int UbicacionNivel { get; set; } = 0;
        public bool Pallet_Completo { get; set; } = false;
        public string Talla { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int IdPickingUbicStock { get; set; } = 0;
        public int IdPickingUbic { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);         
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public clsBeStock() { }
    }
}
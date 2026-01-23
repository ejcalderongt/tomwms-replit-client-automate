using WMS.EntityCore.Producto;

namespace WMS.EntityCore.Stock
{
    using System;
    using WMS.EntityCore.Datos_Maestros;
    using WMSWebAPI.Be;

    [Serializable]

    public partial class clsBeStock_3pl : ICloneable, IDisposable
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
        public DateTime Fecha_ingreso { get; set; } = new DateTime(1900, 1, 1);
        public DateTime Fecha_vence { get; set; } = new DateTime(1900, 1, 1);
        public double Uds_lic_plate { get; set; } = 0;
        public int No_bulto { get; set; } = 0;
        public DateTime Fecha_manufactura { get; set; } = new DateTime(1900, 1, 1);
        public int Añada { get; set; } = 0;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = false;
        public double Peso { get; set; } = 0.0;
        public double Temperatura { get; set; } = 0.0;
        public string Atributo_variante_1 { get; set; } = "";
        public bool Pallet_no_estandar { get; set; } = false;
        public int IdPickingUbicStock { get; set; } = 0;
        public int IdPickingUbic { get; set; } = 0;
        public int IdPedidoDet { get; set; } = 0;
        public List<clsBeBodega_area> Bodega_Areas { get; set; } = new List<clsBeBodega_area>();
        public List<clsBeBodega_sector> Bodega_Sectores { get; set; } = new List<clsBeBodega_sector>();
        public List<clsBeBodega_ubicacion> Bodega_Ubicaciones { get; set; } = new List<clsBeBodega_ubicacion>();
        public List<clsBeBodega_tramo> Bodega_Tramos { get; set; } = new List<clsBeBodega_tramo>();
      
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

        public clsBeStock_3pl() { }
    }

}
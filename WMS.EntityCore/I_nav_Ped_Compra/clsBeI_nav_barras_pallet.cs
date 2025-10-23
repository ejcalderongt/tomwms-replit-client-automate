namespace WMS.EntityCore.I_nav_Ped_Compra
{
    using System;

    public class clsBeI_nav_barras_pallet : ICloneable
    {
        public int IdPallet { get; set; } = 0;
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Camas_Por_Tarima { get; set; } = 0;
        public int Cajas_Por_Cama { get; set; } = 0;
        public double Cantidad_Presentacion { get; set; } = 0;        
        public double Cantidad_UMP { get; set; } = 0;
        public string UM_Producto { get; set; } = string.Empty;
        public string Lote { get; set; } = string.Empty;
        public long Lote_Numerico { get; set; } = 0;
        public DateTime Fecha_Agregado { get; set; } = DateTime.Now;
        public DateTime Fecha_Ingreso { get; set; } = new DateTime(1990, 1, 1);
        public DateTime Fecha_Vence { get; set; } = new DateTime(1990, 1, 1);
        public DateTime Fecha_Produccion { get; set; } = new DateTime(1990, 1, 1);
        public bool Activo { get; set; } = true;
        public bool Recibido { get; set; } = false;
        public int IdRecepcion { get; set; } = 0;
        public string Bodega_Origen { get; set; } = string.Empty;
        public string Bodega_Destino { get; set; } = string.Empty;
        public string Codigo_barra { get; set; } = string.Empty;

        public clsBeI_nav_barras_pallet() { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

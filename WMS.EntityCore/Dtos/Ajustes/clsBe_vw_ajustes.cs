using System;

namespace WMS.EntityCore.Dtos.Ajustes
{   
    public class clsBe_vw_ajustes : ICloneable
    {
        public int IdAjusteEnc { get; set; } = 0;
        public int IdAjusteDet { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Codigo_Producto { get; set; } = string.Empty;
        public string Nombre_Producto { get; set; } = string.Empty;
        public int IdPresentacion { get; set; } = 0;
        public string UMBas { get; set; } = string.Empty;
        public int IdBodegaERP { get; set; } = 0;
        public string Codigo_Bodega { get; set; } = string.Empty;
        public string Nombre_Bodega { get; set; } = string.Empty;
        public double Cantidad_original { get; set; } = 0.0;
        public double Cantidad_nueva { get; set; } = 0.0;
        public double Peso_nuevo { get; set; } = 0.0;
        public double Peso_original { get; set; } = 0.0;
        public DateTime Fecha_vence_nueva { get; set; } = DateTime.Now;
        public DateTime Fecha_vence_original { get; set; } = DateTime.Now;
        public string Lote_Original { get; set; } = string.Empty;
        public string Lote_Nuevo { get; set; } = string.Empty;
        public string Tipo_Ajuste { get; set; } = string.Empty;
        public bool Modifica_Cantidad { get; set; } = false;
        public bool Enviado { get; set; } = false;
        public string Motivo_Ajuste { get; set; } = string.Empty;
        public string Observacion { get; set; } = string.Empty;
        public string Seccion { get; set; } = string.Empty;
        public int IdProductoFamilia { get; set; } = 0;
        public string Nombre_Presentacion { get; set; } = string.Empty;
        public double Factor { get; set; } = 0;
        public string Codigo_Centro_Costo { get; set; } = string.Empty;
        public string Nombre_Centro_Costo { get; set; } = string.Empty;
        public string Talla { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string User_Agr { get; set; } = string.Empty;

        /// <summary>
        /// #CKFK20251030 Agregamos estos tres campos para la integración con ERP 
        /// ya que centro de costo maneja centro de costo dirección y departamento
        /// </summary>
        public string Centro_Costo_Erp { get; set; } = string.Empty;
        public string Centro_Costo_Dir_Erp { get; set; } = string.Empty;
        public string Centro_Costo_Dep_Erp { get; set; } = string.Empty;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public clsBe_vw_ajustes()
        {
        }

        /// <summary>
        /// Constructor con parámetros principales
        /// </summary>
        public clsBe_vw_ajustes(
            int IdAjusteEnc,
            string Fecha,
            string Referencia,
            string Codigo_Producto,
            string Nombre_Producto,
            int IdPresentacion,
            string UMBas,
            int IdBodegaERP,
            string Codigo_Bodega,
            string Nombre_Bodega,
            double cantidad_original,
            double cantidad_nueva,
            double peso_nuevo,
            double peso_original,
            DateTime fecha_vence_nueva,
            DateTime fecha_vence_original,
            string Lote_Original,
            string Lote_Nuevo,
            string Tipo_Ajuste,
            bool Modifica_Cantidad,
            bool Enviado,
            string Motivo_Ajuste)
        {
            this.IdAjusteEnc = IdAjusteEnc;
            this.IdAjusteDet = IdAjusteDet;
            this.Fecha = Fecha ?? string.Empty;
            this.Referencia = Referencia ?? string.Empty;
            this.Codigo_Producto = Codigo_Producto ?? string.Empty;
            this.Nombre_Producto = Nombre_Producto ?? string.Empty;
            this.IdPresentacion = IdPresentacion;
            this.UMBas = UMBas ?? string.Empty;
            this.IdBodegaERP = IdBodegaERP;
            this.Codigo_Bodega = Codigo_Bodega ?? string.Empty;
            this.Nombre_Bodega = Nombre_Bodega ?? string.Empty;
            this.Cantidad_original = cantidad_original;
            this.Cantidad_nueva = cantidad_nueva;
            this.Peso_nuevo = peso_nuevo;
            this.Peso_original = peso_original;
            this.Fecha_vence_nueva = fecha_vence_nueva;
            this.Fecha_vence_original = fecha_vence_original;
            this.Lote_Original = Lote_Original ?? string.Empty;
            this.Lote_Nuevo = Lote_Nuevo ?? string.Empty;
            this.Tipo_Ajuste = Tipo_Ajuste ?? string.Empty;
            this.Modifica_Cantidad = Modifica_Cantidad;
            this.Enviado = Enviado;
            this.Motivo_Ajuste = Motivo_Ajuste ?? string.Empty;
        }

        /// <summary>
        /// Crea una copia superficial del objeto
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Representación en string del objeto
        /// </summary>
        public override string ToString()
        {
            return $"Ajuste: {IdAjusteEnc}-{IdAjusteDet} | {Codigo_Producto} | {Cantidad_nueva} {UMBas}";
        }
    }
}
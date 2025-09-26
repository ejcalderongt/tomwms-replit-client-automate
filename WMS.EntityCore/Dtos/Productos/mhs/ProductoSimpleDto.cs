using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSWebAPI.Dtos.Catalogos
{
    public class ProductoSimpleDto
    {

        public int IdProducto { get; set; }
        public int IdPropietario { get; set; }
        public string? CodigoClasificacion { get; set; }
        public string? CodigoFamilia { get; set; }
        public string? CodigoMarca { get; set; }
        public int? IdTipoProducto { get; set; }
        public int IdUnidadMedidaBasica { get; set; }
        public int? IdCamara { get; set; }
        public int? IdTipoRotacion { get; set; }
        public int? IdPerfilSerializado { get; set; }
        public int? IdIndiceRotacion { get; set; }
        public int? IdSimbologia { get; set; }
        public int? IdArancel { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo_Barra { get; set; }
        public float? Precio { get; set; }
        public float? Existencia_Min { get; set; }
        public float? Existencia_Max { get; set; }
        public float? Costo { get; set; }
        public float? Peso_Referencia { get; set; }
        public float? Peso_Tolerancia { get; set; }
        public float? Temperatura_Referencia { get; set; }
        public float? Temperatura_Tolerancia { get; set; }
        
        public bool? Serializado { get; set; }
        public bool? Genera_Lote { get; set; }
        public bool? Genera_Lp_Old { get; set; }
        public bool? Control_Vencimiento { get; set; }
        public bool? Control_Lote { get; set; }
        public bool? Peso_Recepcion { get; set; }
        public bool? Peso_Despacho { get; set; }
        public bool? Temperatura_Recepcion { get; set; }
        public bool? Temperatura_Despacho { get; set; }
        public bool? Materia_Prima { get; set; }
        public bool? Kit { get; set; }
        public int? Tolerancia { get; set; }
        public int? Ciclo_Vida { get; set; }
        //public string? User_Agr { get; set; }
        //public DateTime? Fec_Agr { get; set; }
        //public string? User_Mod { get; set; }
        //public DateTime? Fec_Mod { get; set; }
        public string? NoSerie { get; set; }
        public string? NoParte { get; set; }
        public bool? FechaManufactura { get; set; }
        public bool? Capturar_Aniada { get; set; }
        public bool? Control_Peso { get; set; }
        public bool? Captura_Arancel { get; set; }
        public bool? Es_Hardware { get; set; }
        public float? Largo { get; set; }
        public float? Alto { get; set; }
        public float? Ancho { get; set; }
        public int? IdUnidadMedidaCobro { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public int? Dias_Inventario_Promedio { get; set; }
        public int? IDPRODUCTOPARAMETROA { get; set; }
        public int? IDPRODUCTOPARAMETROB { get; set; }
        public int? IdTipoManufactura { get; set; }
        public byte[]? imagen { get; set; } = Array.Empty<byte>();

    }
}

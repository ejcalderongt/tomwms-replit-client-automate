using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.EntityCore.Producto
{
    public class clsBeProducto_imagen : ICloneable
    {
        [Key]
        [Column("IdProductoImagen")]
        [DisplayName("IdProductoImagen")]
        public int IdProductoImagen { get; set; }

        [Column("IdProducto")]
        [DisplayName("IdProducto")]
        [Required]
        public int IdProducto { get; set; }

        [Column("Etiqueta")]
        [DisplayName("Etiqueta")]
        [Required]
        public string Etiqueta { get; set; } = string.Empty;

        [Column("Imagen")]
        [DisplayName("Imagen")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        [Column("user_agr")]
        [DisplayName("user_agr")]
        [Required]
        public string User_agr { get; set; } = string.Empty;

        [Column("fec_agr")]
        [DisplayName("fec_agr")]
        public DateTime Fec_agr { get; set; } = DateTime.UtcNow;

        public clsBeProducto_imagen() { }

        public object Clone()
        {
            return new clsBeProducto_imagen
            {
                IdProductoImagen = IdProductoImagen,
                IdProducto = IdProducto,
                Etiqueta = Etiqueta,
                Imagen = (byte[])(Imagen?.Clone() ?? Array.Empty<byte>()),
                User_agr = User_agr,
                Fec_agr = Fec_agr
            };
        }
    }
}
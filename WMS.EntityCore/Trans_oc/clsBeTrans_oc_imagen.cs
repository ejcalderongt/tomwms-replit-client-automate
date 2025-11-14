namespace WMS.EntityCore.Trans_oc
{
    public class clsBeTrans_oc_imagen : ICloneable, IDisposable
    {
        public int IdOrdenCompraImg { get; set; } = 0;
        public int IdOrdenCompraEnc { get; set; } = 0;
        public int Orden { get; set; } = 0;
        public byte[] Imagen { get; set; } = Array.Empty<byte>();
        public string Descripcion { get; set; } = string.Empty;
        public bool IsNew { get; set; } = false;

        public clsBeTrans_oc_imagen() { }

        public clsBeTrans_oc_imagen(int idOrdenCompraImg, int idOrdenCompraEnc, int orden, byte[] imagen, string descripcion)
        {
            IdOrdenCompraImg = idOrdenCompraImg;
            IdOrdenCompraEnc = idOrdenCompraEnc;
            Orden = orden;
            Imagen = imagen ?? Array.Empty<byte>(); // Ensure no null assignment  
            Descripcion = descripcion;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Dispose()
        {
            // Liberación manual de recursos si fuese necesario  
            Imagen = Array.Empty<byte>(); // Replace null assignment with an empty array  
            GC.SuppressFinalize(this);
        }
    }
}

namespace WMS.EntityCore.Dtos.Prefactura
{
    /// <summary>
    /// Request para marcar una prefactura como procesada
    /// </summary>
    public class MarcarProcesadoRequestDto
    {
        /// <summary>
        /// ID de la prefactura
        /// </summary>
        public int IdPrefacturaEnc { get; set; }
    }
}
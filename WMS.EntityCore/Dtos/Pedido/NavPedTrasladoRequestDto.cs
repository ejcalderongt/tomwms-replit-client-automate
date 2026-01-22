namespace WMS.EntityCore.Dtos.Pedido
{
    using WMS.EntityCore.Pedido;

    public class NavPedTrasladoRequestDto
    {
        /// <summary>
        /// Documento de traslado/pedido completo con encabezado y líneas de detalle.
        /// Este campo coincide con el wrapper "beINavPedCompraEnc" del JSON.
        /// </summary>
        public clsBeI_nav_ped_traslado_enc beINavPedCompraEnc { get; set; } = new clsBeI_nav_ped_traslado_enc();
    }

}

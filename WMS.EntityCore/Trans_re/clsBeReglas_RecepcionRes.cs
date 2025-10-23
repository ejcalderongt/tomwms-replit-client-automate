namespace WMS.EntityCore.Trans_re
{
    public class clsBeReglas_RecepcionRes
    {
        public bool PermitirProductoFaltantes { get; set; } = true;
        public bool PermitirProductosAdicionales { get; set; } = true;
        public bool PermitirCantidadFaltantePorProducto { get; set; } = true;
        public bool PermitirCantidadSobrantePorProducto { get; set; } = true;
        public bool PermitirCostoDiferentePorProducto { get; set; } = true;
        public bool PermitirPesoMayor { get; set; } = true;
        public bool PermitirPesoMenor { get; set; } = true;
        public bool Rechazar { get; set; } = false;
    }
}

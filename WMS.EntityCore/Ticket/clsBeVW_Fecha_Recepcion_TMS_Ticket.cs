namespace WMS.EntityCore.Ticket
{
    public class clsBeVW_Fecha_Recepcion_TMS_Ticket : ICloneable
    {
        public int IdTicket { get; set; } = 0;
        public int IdOrdenCompraEnc { get; set; } = 0;
        public int IdRecepcionEnc { get; set; } = 0;
        public DateTime Fecha_Ingreso { get; set; } = DateTime.Now;
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
        public DateTime Fecha_Recepcion { get; set; } = DateTime.Now;

        public clsBeVW_Fecha_Recepcion_TMS_Ticket()
        {
            // EJC: Add your constructor here...
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

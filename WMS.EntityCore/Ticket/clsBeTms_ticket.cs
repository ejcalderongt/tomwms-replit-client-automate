namespace WMS.EntityCore.Ticket
{
    public class clsBeTms_ticket : ICloneable
    {
        public int IdTicket { get; set; } = 0;
        public int IdEmpresa { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public int IdUbicacionDestino { get; set; } = 0;
        public int IdPiloto { get; set; } = 0;
        public int IdVehiculo { get; set; } = 0;
        public int IdEmpresaTransporte { get; set; } = 0;
        public string Tipo_Operacion { get; set; } = "";
        public DateTime Fecha_Ingreso { get; set; } = new DateTime(1900, 1, 1);
        public DateTime Fecha_Salida { get; set; } = new DateTime(1900, 1, 1);
        public DateTime Fecha_Finalizado { get; set; } = new DateTime(1900, 1, 1);
        public string Estado { get; set; } = "";
        public string No_Poliza { get; set; } = "";
        public string No_Placa { get; set; } = "";
        public string No_Documento_Piloto { get; set; } = "";
        public string Tipo_Documento_Piloto { get; set; } = "";
        public string Nombres_Piloto { get; set; } = "";
        public string Apellidos_Piloto { get; set; } = "";
        public string No_TC { get; set; } = "";
        public clsBeTms_ticket_pol? ObjPoliza { get; set; } = new clsBeTms_ticket_pol();
        public bool Procesado_Stock_Jornada { get; set; } = false;
        public DateTime Fecha_Procesado_Stock_Jornada { get; set; } = new DateTime(1900, 1, 1);

        public clsBeTms_ticket()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
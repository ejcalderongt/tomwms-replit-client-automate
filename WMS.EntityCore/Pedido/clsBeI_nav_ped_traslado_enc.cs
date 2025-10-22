using static WMS.EntityCore.clsDataContractDI;

namespace WMS.EntityCore.Pedido
{    
    public class clsBeI_nav_ped_traslado_enc : ICloneable
    {
        public string No { get; set; } = string.Empty;
        public DateTime? Posting_Date { get; set; } = DateTime.Now;
        public DateTime? Receipt_Date { get; set; } = DateTime.Now;
        public DateTime? Shipment_Date { get; set; } = DateTime.Now;
        public int Status { get; set; }
        public string Transfer_from_Code { get; set; } = string.Empty;
        public string Transfer_from_Contact { get; set; } = string.Empty;
        public string Transfer_from_Name { get; set; } = string.Empty;
        public string Transfer_to_Code { get; set; } = string.Empty;
        public string Transfer_to_Contact { get; set; } = string.Empty;
        public string Transfer_to_Name { get; set; } = string.Empty;
        public string Transfer_to_CodeField { get; set; } = string.Empty;
        public string Product_Owner_Code { get; set; } = string.Empty;
        public bool Is_Internal_Transfer { get; set; } = false;
        public string Receipt_Document_Reference { get; set; } = string.Empty;
        public tTipoDocumentoSalida Document_Type { get; set; } = 0;
        public string External_Document_No { get; set; } = string.Empty;
        public string RoadCodigoRuta { get; set; } = string.Empty;
        public string RoadCodigoVendedor { get; set; } = string.Empty;
        public clsDataContractDI.Manufacturing_Process Manufacturing_Process { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string Company_Code { get; set; } = string.Empty;
        public bool IsExport { get; set; } = false;

        // Nueva propiedad agregada
        public List<clsBeI_nav_ped_traslado_det> Lineas_Detalle { get; set; } = new List<clsBeI_nav_ped_traslado_det>();

        public clsBeI_nav_ped_traslado_enc() { }

        public clsBeI_nav_ped_traslado_enc(
            ref string No,
            DateTime Posting_Date,
            DateTime Receipt_Date,
            DateTime Shipment_Date,
            bool Status,
            string Transfer_from_Code,
            string Transfer_from_Contact,
            string Transfer_from_Name,
            string Transfer_to_Code,
            string Transfer_to_Contact,
            string Transfer_to_Name,
            string transfer_to_CodeField)
        {
            this.No = No;
            this.Posting_Date = Posting_Date;
            this.Receipt_Date = Receipt_Date;
            this.Shipment_Date = Shipment_Date;
            this.Status = Status ? 1 : 0;
            this.Transfer_from_Code = Transfer_from_Code;
            this.Transfer_from_Contact = Transfer_from_Contact;
            this.Transfer_from_Name = Transfer_from_Name;
            this.Transfer_to_Code = Transfer_to_Code;
            this.Transfer_to_Contact = Transfer_to_Contact;
            this.Transfer_to_Name = Transfer_to_Name;
            this.Transfer_to_CodeField = transfer_to_CodeField;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

}

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavCliente
    {
        public int IdCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public bool ProcesadoWms { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNo { get; set; }
        public string ContactName { get; set; }
        public string SearchName { get; set; }
        public string VatRegistratrionNo { get; set; }
        public string LocationCode { get; set; }
    }
}

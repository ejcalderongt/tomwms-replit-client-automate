using WMS.EntityCore.Dtos.Ajustes.WMS.Core.Entities;

namespace WMS.EntityCore.Dtos.Ajustes
{
    public class AjustesPendientesEnvioResponse
    {
        public string Resultado { get; set; } = "";
        public List<clsBeAjustesMI3> Ajustes { get; set; } = new();
    }  
    
}
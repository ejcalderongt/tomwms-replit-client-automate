
namespace WMS.EntityCore.Trans_oc
{
    public class clsBeTrans_oc_embarcador : ICloneable
    {
        public int IdEmbarcador { get; set; } = 0;
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";

        public clsBeTrans_oc_embarcador()
        {
            // EJC: Add your constructor here...
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

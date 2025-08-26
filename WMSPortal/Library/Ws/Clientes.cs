using Microsoft.Extensions.Configuration;
using ServiceReference1;
using ServiceReference2;
using ServiceReference3;
using System;
using System.ServiceModel;

namespace WMSPortal.Library.Ws
{
    public class Clientes
    {
        private string IpServer;
        private Uri MyUri;
        private EndpointAddress Address;
        private readonly BasicHttpBinding Binding = new();
        private readonly IConfiguration _Config;

        public Clientes(IConfiguration Config)
        {
            _Config = Config;
            SetIpServer();
            Binding.MaxBufferPoolSize = int.MaxValue;
            Binding.MaxReceivedMessageSize = int.MaxValue;
        }

        public void SetIpServer()
        {
            IpServer = _Config["IpWebServices"];
        }

        public string GetIpServer()
        {
            return IpServer;
        }

        public void SetServicio(string Servicio)
        {
            MyUri = new Uri(IpServer + Servicio);
            Address = new EndpointAddress(MyUri);
        }

        public ServiceBodegaClient GetClienteBodega()
        {
            SetServicio("/Bodega/ServiceBodega.svc");
            return new ServiceBodegaClient(Binding, Address);
        }

        public IsrvReportesClient GetClienteReportes()
        {
            SetServicio("/Reportes/srvReportes.svc");
            return new IsrvReportesClient(Binding, Address);
        }

        public IsrvUsuarioClient GetClienteUsuario()
        {
            SetServicio("/Usuario/srvUsuario.svc");
            return new IsrvUsuarioClient(Binding, Address);
        }
    }
}

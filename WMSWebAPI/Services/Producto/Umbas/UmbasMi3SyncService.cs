using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Umbas
{
    public class UmbasMi3SyncService : IUmbasMi3SyncService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public UmbasMi3SyncService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarUmbasMi3Dto(UnidadMedidaMi3Dto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Codigo))
                    throw new ArgumentNullException(nameof(dto), "La UmbasMi3 no puede estar vacio.");

                var UmbasMi3 = _mapper.Map<clsBeUnidad_medidaMi3>(dto);
                clsLnUnidad_medida.Valida_Atributos(UmbasMi3, conn, tx);
                

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar UmbasMi3 " + ex.Message);
            }
        }
        public List<clsBeUnidad_medida> Get_All()
        {
            try
            {
                return clsLnUnidad_medida.GetAll(_configuration);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar UmbasMi3 " + ex.Message);
            }
        }
    }
}

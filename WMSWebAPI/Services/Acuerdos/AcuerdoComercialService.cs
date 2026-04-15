using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.AcuerdosComerciales;
using WMS.EntityCore.Dtos.Acuerdos;
using WMS.DALCore.AcuerdosComerciales;

namespace WMSWebAPI.Services.AcuerdosComerciales
{
    public class AcuerdoComercialService : IAcuerdoComercialService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AcuerdoComercialService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarAcuerdoComercialDesdeDto(AcuerdoComercialEncDto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "El DTO del acuerdo comercial no puede ser nulo");

                if (dto.IdAcuerdoEnc > 0 || (dto.codigo_acuerdo.HasValue && dto.codigo_acuerdo.Value > 0))
                {
                    Valida_Atributos(dto, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar Acuerdo Comercial (IdAcuerdoEnc: {dto?.IdAcuerdoEnc}) → {ex.Message}", ex);
            }
        }

        public void ProcesarListaAcuerdosDesdeDto(List<AcuerdoComercialEncDto> dtos, SqlConnection conn, SqlTransaction tx)
        {
            if (dtos == null || !dtos.Any())
                return;

            try
            {
                foreach (var dto in dtos)
                {
                    ProcesarAcuerdoComercialDesdeDto(dto, conn, tx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar lista de acuerdos comerciales → {ex.Message}", ex);
            }
        }

        public void Valida_Atributos(AcuerdoComercialEncDto entity, SqlConnection conn, SqlTransaction tx)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (conn == null)
                throw new ArgumentNullException(nameof(conn));

            if (tx == null)
                throw new ArgumentNullException(nameof(tx));

            try
            {
                var acuerdoEnc = _mapper.Map<clsBeTrans_acuerdoscomerciales_enc>(entity);
                var detalles = _mapper.Map<List<clsBeTrans_acuerdoscomerciales_det>>(entity.Detalles);

                bool existe = ExisteAcuerdo(acuerdoEnc.IdAcuerdoEnc, conn, tx);

                if (!existe)
                {
                    InsertarAcuerdoCompleto(acuerdoEnc, detalles, conn, tx);
                }
                else
                {
                    ActualizarAcuerdoCompleto(acuerdoEnc, detalles, conn, tx);
                }
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
            }
        }

        public void InsertarAcuerdoCompleto(
            clsBeTrans_acuerdoscomerciales_enc acuerdoEnc,
            List<clsBeTrans_acuerdoscomerciales_det> detalles,
            SqlConnection conn,
            SqlTransaction tx)
        {
            try
            {
                if (acuerdoEnc.IdAcuerdoEnc == 0)
                {
                    acuerdoEnc.IdAcuerdoEnc = Get_MaxId(conn, tx) + 1;
                }

                int rowsAffectedEnc = clsLnTrans_acuerdoscomerciales_enc.Insertar(acuerdoEnc, conn, tx);

                if (rowsAffectedEnc <= 0)
                    throw new Exception("No se pudo insertar el encabezado del acuerdo comercial");

                if (detalles != null && detalles.Any())
                {
                    foreach (var detalle in detalles)
                    {
                        detalle.IdAcuerdoEnc = acuerdoEnc.IdAcuerdoEnc;

                        if (detalle.IdAcuerdoDet == 0)
                        {
                            detalle.IdAcuerdoDet = clsLnTrans_acuerdoscomerciales_det.MaxID(conn, tx) + 1;
                        }

                        int rowsAffectedDet = clsLnTrans_acuerdoscomerciales_det.Insertar(detalle, conn, tx);

                        if (rowsAffectedDet <= 0)
                            throw new Exception($"No se pudo insertar el detalle del acuerdo comercial para el producto: {detalle.Codigo_producto}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar acuerdo comercial completo: {ex.Message}", ex);
            }
        }

        public void ActualizarAcuerdoCompleto(
            clsBeTrans_acuerdoscomerciales_enc acuerdoEnc,
            List<clsBeTrans_acuerdoscomerciales_det> detalles,
            SqlConnection conn,
            SqlTransaction tx)
        {
            try
            {
                int rowsAffectedEnc = clsLnTrans_acuerdoscomerciales_enc.Actualizar(acuerdoEnc, conn, tx);

                if (rowsAffectedEnc <= 0)
                    throw new Exception("No se pudo actualizar el encabezado del acuerdo comercial");

                // Primero, eliminar los detalles existentes
                int rowsDeleted = clsLnTrans_acuerdoscomerciales_det.EliminarPorIdAcuerdoEnc(_configuration, acuerdoEnc.IdAcuerdoEnc, conn, tx);

                // Luego, insertar los nuevos detalles
                if (detalles != null && detalles.Any())
                {
                    foreach (var detalle in detalles)
                    {
                        detalle.IdAcuerdoEnc = acuerdoEnc.IdAcuerdoEnc;

                        if (detalle.IdAcuerdoDet == 0)
                        {
                            detalle.IdAcuerdoDet = clsLnTrans_acuerdoscomerciales_det.MaxID(conn, tx) + 1;
                        }

                        int rowsAffectedDet = clsLnTrans_acuerdoscomerciales_det.Insertar(detalle, conn, tx);

                        if (rowsAffectedDet <= 0)
                            throw new Exception($"No se pudo insertar el detalle del acuerdo comercial para el producto: {detalle.Codigo_producto}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar acuerdo comercial completo: {ex.Message}", ex);
            }
        }

        public List<clsBeTrans_acuerdoscomerciales_enc> Get_All()
        {
            try
            {
                return clsLnTrans_acuerdoscomerciales_enc.GetAll(_configuration);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los acuerdos comerciales → {ex.Message}", ex);
            }
        }

        public List<clsBeTrans_acuerdoscomerciales_enc> Get_All_By_IdCliente(int IdCliente)
        {
            try
            {
                return clsLnTrans_acuerdoscomerciales_enc.GetAllByIdCliente(_configuration, IdCliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener acuerdos comerciales por cliente (IdCliente: {IdCliente}) → {ex.Message}", ex);
            }
        }

        public clsBeTrans_acuerdoscomerciales_enc? Get_By_Id(int IdAcuerdoEnc)
        {
            try
            {
                var acuerdo = new clsBeTrans_acuerdoscomerciales_enc { IdAcuerdoEnc = IdAcuerdoEnc };
                bool existe = clsLnTrans_acuerdoscomerciales_enc.GetSingle(_configuration, ref acuerdo);

                if (existe)
                {
                    return acuerdo;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener acuerdo comercial (IdAcuerdoEnc: {IdAcuerdoEnc}) → {ex.Message}", ex);
            }
        }

        public List<clsBeTrans_acuerdoscomerciales_det> Get_Detalles_By_IdAcuerdoEnc(int IdAcuerdoEnc)
        {
            try
            {
                return clsLnTrans_acuerdoscomerciales_det.GetAllByIdAcuerdoEnc(_configuration, IdAcuerdoEnc);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener detalles del acuerdo comercial (IdAcuerdoEnc: {IdAcuerdoEnc}) → {ex.Message}", ex);
            }
        }

        public void EliminarAcuerdoComercial(int IdAcuerdoEnc, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                // Primero eliminar los detalles
                int detallesEliminados = clsLnTrans_acuerdoscomerciales_det.EliminarPorIdAcuerdoEnc(_configuration, IdAcuerdoEnc, conn, tx);

                // Luego eliminar el encabezado
                var acuerdo = new clsBeTrans_acuerdoscomerciales_enc { IdAcuerdoEnc = IdAcuerdoEnc };
                int encabezadoEliminado = clsLnTrans_acuerdoscomerciales_enc.Eliminar(_configuration, acuerdo, conn, tx);

                if (encabezadoEliminado <= 0)
                    throw new Exception($"No se pudo eliminar el encabezado del acuerdo comercial (IdAcuerdoEnc: {IdAcuerdoEnc})");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar acuerdo comercial (IdAcuerdoEnc: {IdAcuerdoEnc}) → {ex.Message}", ex);
            }
        }

        public int Get_MaxId(SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                return clsLnTrans_acuerdoscomerciales_enc.MaxID(conn, tx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener máximo ID: {ex.Message}", ex);
            }
        }

        public bool ExisteAcuerdo(int IdAcuerdoEnc, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                return clsLnTrans_acuerdoscomerciales_enc.Existe(IdAcuerdoEnc, conn, tx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia del acuerdo: {ex.Message}", ex);
            }
        }
    }
}
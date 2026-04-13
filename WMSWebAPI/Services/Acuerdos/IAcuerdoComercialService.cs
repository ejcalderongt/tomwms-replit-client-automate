using Microsoft.Data.SqlClient;
using WMS.EntityCore.AcuerdosComerciales;
using WMS.EntityCore.Dtos.Acuerdos;

namespace WMSWebAPI.Services.AcuerdosComerciales
{
    public interface IAcuerdoComercialService
    {
        /// <summary>
        /// Procesa un acuerdo comercial completo (encabezado + detalles) desde un DTO
        /// </summary>
        /// <param name="dto">DTO del acuerdo comercial con encabezado y detalles</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        void ProcesarAcuerdoComercialDesdeDto(AcuerdoComercialEncDto dto, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Procesa una lista de acuerdos comerciales desde DTOs
        /// </summary>
        /// <param name="dtos">Lista de DTOs de acuerdos comerciales</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        void ProcesarListaAcuerdosDesdeDto(List<AcuerdoComercialEncDto> dtos, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Obtiene todos los acuerdos comerciales
        /// </summary>
        /// <returns>Lista de acuerdos comerciales</returns>
        List<clsBeTrans_acuerdoscomerciales_enc> Get_All();

        /// <summary>
        /// Obtiene todos los acuerdos comerciales por ID de cliente
        /// </summary>
        /// <param name="IdCliente">ID del cliente</param>
        /// <returns>Lista de acuerdos comerciales del cliente</returns>
        List<clsBeTrans_acuerdoscomerciales_enc> Get_All_By_IdCliente(int IdCliente);

        /// <summary>
        /// Obtiene un acuerdo comercial por su ID
        /// </summary>
        /// <param name="IdAcuerdoEnc">ID del acuerdo comercial</param>
        /// <returns>Acuerdo comercial encontrado o null</returns>
        clsBeTrans_acuerdoscomerciales_enc? Get_By_Id(int IdAcuerdoEnc);

        /// <summary>
        /// Obtiene los detalles de un acuerdo comercial por ID del encabezado
        /// </summary>
        /// <param name="IdAcuerdoEnc">ID del encabezado del acuerdo</param>
        /// <returns>Lista de detalles del acuerdo comercial</returns>
        List<clsBeTrans_acuerdoscomerciales_det> Get_Detalles_By_IdAcuerdoEnc(int IdAcuerdoEnc);

        /// <summary>
        /// Elimina un acuerdo comercial completo (encabezado + detalles)
        /// </summary>
        /// <param name="IdAcuerdoEnc">ID del acuerdo comercial a eliminar</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        void EliminarAcuerdoComercial(int IdAcuerdoEnc, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Valida y procesa los atributos de un acuerdo comercial
        /// </summary>
        /// <param name="entity">DTO del acuerdo comercial</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        void Valida_Atributos(AcuerdoComercialEncDto entity, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Inserta un acuerdo comercial completo (encabezado + detalles)
        /// </summary>
        /// <param name="acuerdoEnc">Entidad del encabezado</param>
        /// <param name="detalles">Lista de entidades de detalles</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        void InsertarAcuerdoCompleto(clsBeTrans_acuerdoscomerciales_enc acuerdoEnc, List<clsBeTrans_acuerdoscomerciales_det> detalles, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Actualiza un acuerdo comercial completo (encabezado + detalles)
        /// </summary>
        /// <param name="acuerdoEnc">Entidad del encabezado</param>
        /// <param name="detalles">Lista de entidades de detalles</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        void ActualizarAcuerdoCompleto(clsBeTrans_acuerdoscomerciales_enc acuerdoEnc, List<clsBeTrans_acuerdoscomerciales_det> detalles, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Obtiene el máximo ID de acuerdo comercial
        /// </summary>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        /// <returns>Máximo ID</returns>
        int Get_MaxId(SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Verifica si existe un acuerdo comercial por ID
        /// </summary>
        /// <param name="IdAcuerdoEnc">ID del acuerdo comercial</param>
        /// <param name="conn">Conexión SQL</param>
        /// <param name="tx">Transacción SQL</param>
        /// <returns>True si existe, False si no</returns>
        bool ExisteAcuerdo(int IdAcuerdoEnc, SqlConnection conn, SqlTransaction tx);
    }
}
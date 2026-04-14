using Microsoft.Data.SqlClient;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Propietario;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services.Propietario
{
    public interface IPropietarioService
    {
        /// <summary>
        /// Procesa un propietario desde DTO (inserta o actualiza)
        /// </summary>
        void ProcesarPropietarioDesdeDto(PropietarioDto dto, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Procesa una lista de propietarios desde DTOs
        /// </summary>
        void ProcesarListaPropietariosDesdeDto(List<PropietarioDto> dtos, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Obtiene todos los propietarios
        /// </summary>
        List<clsBePropietarios> GetAll();

        /// <summary>
        /// Obtiene propietario por ID
        /// </summary>
        clsBePropietarios? GetById(int idPropietario);

        /// <summary>
        /// Obtiene propietario por código
        /// </summary>
        clsBePropietarios? GetByCodigo(string codigo);

        /// <summary>
        /// Obtiene propietarios activos
        /// </summary>
        List<clsBePropietarios> GetActivos();

        /// <summary>
        /// Obtiene propietarios con filtros
        /// </summary>
        PropietarioListResponseDto GetByFilter(PropietarioFilterDto filter);

        /// <summary>
        /// Elimina un propietario
        /// </summary>
        void EliminarPropietario(int idPropietario, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Valida y procesa atributos del propietario
        /// </summary>
        void ValidaAtributos(PropietarioDto entity, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Verifica si existe un propietario por ID
        /// </summary>
        bool ExistePropietario(int idPropietario, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Verifica si existe un propietario por código
        /// </summary>
        bool ExistePropietarioByCodigo(string codigo, SqlConnection conn, SqlTransaction tx);

        /// <summary>
        /// Obtiene el máximo ID
        /// </summary>
        int GetMaxId(SqlConnection conn, SqlTransaction tx);
        List<int> GetBodegasByPropietarioId(int idPropietario);
        void SincronizarBodegasPropietario(int idPropietario, SqlConnection conn, SqlTransaction tx);
        List<clsBeBodega> GetBodegasCompletasByPropietarioId(int idPropietario);        
        List<object> GetAllWithBodegas();
    }
}
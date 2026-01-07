using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto.ProductoSimple;
using WMS.EntityCore.Proveedor;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services.Proveedor
{
    public class SyncProveedorService : ISyncProveedorService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SyncProveedorService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarProveedorDto(ProveedorDto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.Codigo != null)
                    throw new ArgumentNullException(nameof(dto), "El proveedor no puede estar vacio.");

                var Proveedor = _mapper.Map<clsBeProveedor>(dto);
                clsLnProveedor.Valida_Atributos(Proveedor, conn, tx);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Proveedor → " + ex.Message, ex);
            }
        }

        public void ProcesarProveedorListDto(List<ProveedorDto> listaDto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (listaDto == null || listaDto.Count == 0)
                    throw new ArgumentNullException(nameof(listaDto), "La lista de proveedores no puede ser nula o vacía.");

                var proveedorList = _mapper.Map<List<clsBeProveedor>>(listaDto);
                if (proveedorList != null)
                    clsLnProveedor.InsertarOActualizar(proveedorList, conn, tx);
                    
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Proveedores → " + ex.Message, ex);
            }
        }
        // Método en la clase de servicio (SyncProveedorService)
        public List<clsBeProveedor> Get_All()
        {
            try
            {
                return clsLnProveedor.GetAll(_configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener proveedores → " + ex.Message, ex);
            }
        }     
        private static string ValidateAndTrim(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"El {fieldName} del proveedor es obligatorio.", fieldName);

            return value.Trim();
        }

        private static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            var trimmedEmail = email.Trim();

            // Validación básica de formato email
            if (!trimmedEmail.Contains("@") && !string.IsNullOrEmpty(trimmedEmail))
                throw new ArgumentException("El formato del email no es válido.", nameof(email));

            return trimmedEmail;
        }

        public void Procesarmi3ProveedorDto(mi3ProveedorDto dto, SqlConnection conn, SqlTransaction tx)
        {
            // Validaciones iniciales con expresión throw
            _ = dto ?? throw new ArgumentNullException(nameof(dto));
            _ = conn ?? throw new ArgumentNullException(nameof(conn));
            _ = tx ?? throw new ArgumentNullException(nameof(tx));

            // Validar código (parece ser el campo clave)
            if (string.IsNullOrWhiteSpace(dto.Codigo))
                throw new ArgumentException("El código del proveedor es obligatorio.", nameof(dto.Codigo));

            string safeNombre = "";
            string safeemail = "";

            if (dto.Nombre != null)
                safeNombre = dto.Nombre;
            if (dto.Email != null)
                safeemail = dto.Email;

            try
            {
                var proveedor = new clsBeProveedor
                {
                    Codigo = dto.Codigo.Trim(),
                    Nombre = ValidateAndTrim(safeNombre, "nombre"),
                    Telefono = dto.Telefono?.Trim() ?? string.Empty,
                    Nit = dto.Nit?.Trim() ?? string.Empty,
                    Direccion = dto.Direccion?.Trim() ?? string.Empty,
                    Email = ValidateEmail(safeemail),
                    Contacto = dto.Contacto?.Trim() ?? string.Empty,
                    Activo = dto.Activo.GetValueOrDefault(),
                    Es_bodega_recepcion = dto.Es_Bodega_Traslado.GetValueOrDefault(),
                    Es_bodega_traslado = dto.Es_Bodega_Recepcion.GetValueOrDefault(),
                    Referencia = string.Empty,
                    Codigo_Empresa_ERP = string.Empty
                };

                clsLnProveedor.Valida_Atributos(proveedor, conn, tx);
            }
            catch (Exception ex) when (ex is ArgumentNullException or ArgumentException)
            {
                throw; // Relanzar excepciones de validación
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar Proveedor: {ex.Message}", ex);
            }
        }
    }
}

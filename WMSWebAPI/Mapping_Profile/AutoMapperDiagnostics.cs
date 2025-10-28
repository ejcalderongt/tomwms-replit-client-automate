using AutoMapper;
using System.Reflection;

namespace WMSWebAPI.Mapping_Profile
{
    public static class AutoMapperDiagnostics
    {
        public static void ValidateAndReport(IServiceProvider sp, ILogger logger)
        {
            var mapper = sp.GetRequiredService<IMapper>();
            var cfg = mapper.ConfigurationProvider;

            try
            {
                cfg.AssertConfigurationIsValid();
                logger.LogInformation("AutoMapper configuration is valid.");
            }
            catch (AutoMapperConfigurationException ex)
            {
                // 1) Mensaje completo (incluye detalle por mapa en la mayoría de versiones)
                logger.LogError(ex, "AutoMapper configuration INVALID:\n{Dump}", ex.ToString());


                // 2) Detalle por cada error SIN depender de APIs eliminadas
                foreach (var err in ex.Errors)
                {
                    // Source/Destination por reflexión (algunas versiones exponen .Types, otras no)
                    string src = TryGetTypeName(err, "SourceType");
                    string dst = TryGetTypeName(err, "DestinationType");

                    if (!string.IsNullOrEmpty(src) || !string.IsNullOrEmpty(dst))
                        logger.LogError("Map: {Src} -> {Dst}", src, dst);

                    // UnmappedPropertyNames si existe
                    var unmapped = TryGetStringArray(err, "UnmappedPropertyNames");
                    if (unmapped.Length > 0)
                        logger.LogError("  Unmapped destination members: {Props}", string.Join(", ", unmapped));

                    // PropertyMapErrors si existe
                    var pmErrors = TryGetEnumerable(err, "PropertyMapErrors");
                    foreach (var pm in pmErrors)
                        logger.LogError("  PropertyMap error: {Msg}", pm?.ToString());

                    // Path si existe
                    var path = TryGetString(err, "Path");
                    if (!string.IsNullOrWhiteSpace(path))
                        logger.LogError("  Path: {Path}", path);

                    // Inner exception por error
                    var inner = TryGetException(err, "Exception");
                    if (inner != null)
                        logger.LogError(inner, "  Inner: {Msg}", inner.Message);
                }

                // Re-lanzar para fallar el arranque (comportamiento recomendado)
                throw;
            }
        }

        // ==== Helpers de reflexión tolerantes a versión ====
        private static string TryGetTypeName(object err, string typeProp)
        {
            // err.Types?.SourceType?.FullName
            var typesObj = err.GetType().GetProperty("Types", BindingFlags.Public | BindingFlags.Instance)
                               ?.GetValue(err);
            if (typesObj == null) return string.Empty;

            var tProp = typesObj.GetType().GetProperty(typeProp, BindingFlags.Public | BindingFlags.Instance);
            var tVal = tProp?.GetValue(typesObj) as Type;
            return tVal?.FullName ?? string.Empty;
        }

        private static string[] TryGetStringArray(object obj, string propName)
        {
            var val = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance)
                         ?.GetValue(obj);
            if (val is string[] arr) return arr;
            if (val is System.Collections.IEnumerable en)
                return en.Cast<object>().Select(o => o?.ToString() ?? "").Where(s => !string.IsNullOrEmpty(s)).ToArray();
            return Array.Empty<string>();
        }

        private static IEnumerable<object?> TryGetEnumerable(object obj, string propName)
        {
            var val = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance)
                         ?.GetValue(obj) as System.Collections.IEnumerable;
            return val?.Cast<object?>() ?? Array.Empty<object?>();
        }

        private static string TryGetString(object obj, string propName)
            => obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance)
                  ?.GetValue(obj)?.ToString() ?? string.Empty;

        private static Exception? TryGetException(object obj, string propName)
            => obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance)
                  ?.GetValue(obj) as Exception;
    }
}
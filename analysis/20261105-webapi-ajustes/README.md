# Ajustes WebAPI — #EJC20261105_AJUSTES_API

Cambio local en TOMWMS_BOF, rama dev_2028_merge. Sin commit, push ni despliegue.

Endpoint: GET /api/sync/ajustes/mi3/pendientes-envio.
Respuestas full y minimal: FechaTransaccion desde encabezado.Fecha, Usr_Agr desde encabezado.User_agr, TipoUnidadAjuste PRESENTACION cuando detalle.IdPresentacion != 0 y UNIDAD en otro caso; Presentacion contiene Nombre_Presentacion o cadena vacia para UNIDAD.
DAL ahora carga IdPresentacion y Nombre_Presentacion de VW_Ajustes. Cantidad y UMBas se conservan.

Validacion: compilacion WebAPI --no-restore exitosa, sin errores; advertencia CS8602 preexistente en WMS.DALCore/Stock/clsLnStock.cs:1587.
Verify.csproj verifica el mapper real por reflexion y las respuestas full/minimal del controlador con servicio simulado, en PRESENTACION y UNIDAD; verifica fecha, usuario, nombre y cantidad. Resultado PASS.
No ejecuta el servicio real ni el endpoint contra SQL: el GET existente puede actualizar estado de detalles que no modifican cantidad. No se ha validado un despliegue ni un ajuste operativo en unidades minimas.
El codigo BOF consultado usa IdPresentacion != 0 para distinguir presentaciones. El factor no se usa como discriminante.
Las consultas previas del ajuste 2 fueron exclusivamente SELECT. No se guardan credenciales en este informe.
claims.json registra hashes de los cuatro archivos modificados, commit base del repo de codigo y estado draft; provenance.py --root TOMWMS_BOF: 0 invalidos.
Catalogo y router Brain no pudieron regenerarse por ausencia de PyYAML en el Python disponible; analisis apoyado en codigo local y consultas acotadas, sin consolidar fuentes historicas discrepantes.

## Replica en tom_2028_prd — #EJC20261105_AJUSTES_API_PRD

Por instruccion del usuario se replica exclusivamente el cambio de ajustes en la rama ya seleccionada tom_2028_prd. Inicio sin cambios locales; commit base cc41eebf107017e97e226ba2ad327793d6ff175f. Cuatro archivos de producto, 24 inserciones; sin cambios de HH, SQL o configuraciones.
Compilacion WebAPI --no-restore exitosa: 0 errores y advertencia CS8602 en Stock/clsLnStock.cs:1536, fuera del cambio.
Verificacion Verify.csproj repetida contra la rama destino: PASS para mapper y respuestas full/minimal con PRESENTACION y UNIDAD.
claims-prd.json validado con provenance.py y raiz del repositorio de codigo: 0 invalidos. Estado draft: codigo local sin commit, no evidencia de despliegue.
Sin commit, push, despliegue ni acceso a base de datos durante esta replica.

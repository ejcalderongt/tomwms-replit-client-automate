# LC0001 - cierre de tarea HH al finalizar picking desde BOF

Estado: fix implementado y validado en `dev_2028_merge`.

Tag de trazabilidad: `#EJC20260716_LC0001_PICKING_BOF_CLOSE`.

## Síntoma

Un picking procesado al 100 % desde BOF quedaba activo en el monitor porque el
flujo BOF terminaba las líneas de `trans_picking_ubic`, pero no llevaba la tarea
PIK de `tarea_hh` al estado finalizado como sí ocurre en el flujo HH.

## Path reutilizable

1. BOF procesa una o varias líneas mediante `Procesar_Picking_Desde_BOF`.
2. Las líneas quedan persistidas en `trans_picking_ubic`.
3. Se hace una lectura fresca con
   `Get_All_PickingUbic_Pendientes_HH_By_IdPickingEnc`.
4. Solo cuando la lista pendiente existe y su conteo es cero se localiza la
   tarea PIK con `clsLnTarea_hh.GetIdTarea`.
5. La tarea se actualiza a `IdEstado = 4` y `FechaFin = Now` dentro de la misma
   conexión/transacción del procesamiento.

Archivo:

`TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb`

## Regla reusable

No cerrar `tarea_hh` por el solo hecho de ejecutar el método BOF: ese método
también procesa líneas individuales. La condición canónica de cierre es una
consulta posterior a la persistencia que confirme cero líneas activas
pendientes para el encabezado.

La actualización debe reutilizar la conexión y transacción remota cuando
existan; así no puede confirmarse la tarea separadamente de las líneas del
picking.

## Validación

- `dotnet build TOMIMSV4/DAL/DAL.vbproj --no-restore`
- Resultado: 0 errores y 0 advertencias.
- El archivo VB conserva UTF-8 con BOM.

## Checklist de regresión

- Picking parcial BOF: la tarea permanece activa.
- Última línea BOF: la tarea pasa a estado 4 y registra `FechaFin`.
- Picking terminado por HH: conserva su comportamiento existente.
- Reproceso sin tarea PIK: no intenta actualizar una identidad cero.


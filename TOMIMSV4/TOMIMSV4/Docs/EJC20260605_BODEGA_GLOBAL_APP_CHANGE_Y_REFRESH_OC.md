# EJC20260605 - Cambio global de bodega + refresh de lotes OC

## Objetivo
1) Al cambiar bodega desde `frmMenu`, evitar formularios abiertos con contexto viejo.  
2) Al imprimir desde `frmImpresionRecepcion_OC`, refrescar lotes de `frmOrdenCompra` sin cerrar/reabrir manual.

## Implementación aplicada

## A) Cambio de bodega global
- Archivo: `Clases_AP/IMS.vb`
  - Nuevo campo: `IdBodegaGlobalAppChange`.

- Archivo: `Mantenimientos/Principales/frmMenu.vb`
  - En `frmMenu_Load`: inicializa `AP.IdBodegaGlobalAppChange = AP.IdBodega`.
  - En `BarManager1_ItemClick` (al cambiar bodega):
    - actualiza `AP.IdBodega`, `AP.NomBodega`, `AP.IdConfiguracionInterface`,
    - actualiza `AP.IdBodegaGlobalAppChange`,
    - ejecuta `AplicarCambioBodegaEnFormasAbiertas(Me)`.

- Nuevo método:
  - `AplicarCambioBodegaEnFormasAbiertas(...)`:
    - detecta qué listas estaban abiertas (`Pedido`, `OC`, `Recepción`, `Picking`),
    - detecta si `frmPrincipal02` estaba abierto,
    - cierra formularios dependientes de bodega,
    - reabre esas listas si estaban visibles,
    - reabre `frmPrincipal02` para recargar monitor con la bodega nueva.
  - `EsFormularioDependienteDeBodega(...)`:
    - catálogo inicial de formularios a cerrar por seguridad de contexto.

## B) Refresh lotes OC desde impresión
- Archivo: `Mantenimientos/Impresion_OC/frmImpresion_OC.vb`
  - Nuevo evento: `NotificarActualizacionLotesOC`.
  - `ListarBarrasPallet(Optional pNotificar As Boolean = False)`:
    - cuando `pNotificar=True`, dispara evento.
  - Se cambió a `ListarBarrasPallet(True)` en puntos de impresión/cierre donde hay persistencia real.

- Archivo: `Transacciones/Orden_Compra/frmOrdenCompra.vb`
  - En `cmdPreImpresionOC_ItemClick`:
    - suscribe evento `NotificarActualizacionLotesOC`.
  - Nuevo método:
    - `RecargarLotesOCDesdeImpresion()` -> llama `Cargar_Detalle_Lotes_OC()`.

## Validación funcional esperada
1) Cambiar bodega en menú:
   - formularios transaccionales abiertos se cierran,
   - listas principales que estaban abiertas se vuelven a abrir con nueva bodega.
2) En OC, abrir impresión y generar licencia/fardo:
   - al volver, pestaña Lotes queda refrescada (sin actualizar manual).

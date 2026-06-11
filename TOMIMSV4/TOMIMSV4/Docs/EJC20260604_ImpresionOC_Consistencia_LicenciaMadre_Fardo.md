# EJC20260604 - Impresión OC: Consistencia licencia madre al imprimir fardos

## Contexto
En `frmImpresionRecepcion_OC`, hoy la persistencia en `i_nav_barras_pallet` ocurre cuando se imprime/cierra licencia (`cmdImpresionLicencia`), no cuando se imprime fardo (`cmdImpresionBarra`).

Esto abre una ventana de inconsistencia:
- Se imprimen fardos con una licencia madre visible.
- Pero la licencia madre puede no existir aún en `i_nav_barras_pallet` (porque no se cerró/imprimió licencia).

## Trazabilidad de código actual
- Formulario:
  - `TOMIMSV4/TOMIMSV4/Mantenimientos/Impresion_OC/frmImpresion_OC.vb`
- Inserción en `i_nav_barras_pallet`:
  - `ImprimirLicencias_SoloLicencia(...)` -> `clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(...)`
  - `CerrarEImprimirLicenciaConBultos(...)` -> `clsLnI_nav_barras_pallet.Guardar_Pallet_PreImpresion(...)`
- Impresión de fardo (sin insert actual):
  - `cmdImpresionBarra_Click(...)` -> `Imprimir_Producto(...)`
- DAL:
  - `TOMIMSV4/DAL/Interface/Barras_Pallet/clsLnI_nav_barras_pallet.vb`
  - `Get_Single_By_Licencia(...)` valida existencia por `Codigo_Barra`
  - `Guardar_Pallet_PreImpresion(...)` inserta registro

## Riesgo funcional identificado
Si se ejecuta impresión de fardo antes de cerrar/imprimir licencia:
- puede existir etiqueta física de fardo asociada a licencia madre,
- sin existir registro de la licencia madre en `i_nav_barras_pallet`.

Impacto:
- brecha de trazabilidad entre impresión física y persistencia.
- riesgo de validaciones posteriores que asumen licencia existente en tabla.

## Requisito propuesto
Al imprimir fardo en modo `LicenciaBulto`, asegurar persistencia de licencia madre:
- Si licencia madre no existe en `i_nav_barras_pallet`, crearla en ese momento.
- Crearla con `Impreso = 0` (registro técnico de consistencia, no cierre formal de licencia).

## Diseño del fix (fino, mínimo)
Punto de aplicación: `cmdImpresionBarra_Click(...)` antes de llamar `Imprimir_Producto(...)`.

### Paso A: Guard de consistencia (nueva rutina)
Crear helper transaccional local, por ejemplo:
- `AsegurarLicenciaMadrePersistidaParaFardo(...)`

Lógica:
1. Iniciar `clsTransaccion`.
2. Evaluar `Get_Single_By_Licencia(licenciaMadre, cn, tx)`.
3. Si no existe:
   - construir `clsBeI_nav_barras_pallet` con los datos de la línea/lote/licencia madre.
   - setear `Impreso = False`.
   - insertar con `Guardar_Pallet_PreImpresion(...)`.
4. Commit/Rollback.

### Paso B: Integración en el flujo de fardo
En `cmdImpresionBarra_Click(...)`, en `LicenciaBulto`:
1. Resolver `licenciaMadre` (ya existe lógica de anclaje).
2. Ejecutar `AsegurarLicenciaMadrePersistidaParaFardo(licenciaMadre)`.
3. Si falla persistencia, no imprimir fardo (fail-fast).
4. Si ok, imprimir fardo.

## Campos mínimos para inserción autoconsistente
Tomar base de `CrearPalletPreImpresion(...)`:
- `Codigo`, `Nombre`
- `Camas_Por_Tarima`, `Cajas_Por_Cama`
- `Cantidad_Presentacion` (usar capacidad actual o cantidad derivada de tarima)
- `UM_Producto`
- `Lote`, `Fecha_Vence`
- `Codigo_barra = licenciaMadre`
- `IdOrdenCompraEnc`, `IdOrdenCompraDet`
- `Activo = 1`, `Recibido = 0`
- `Impreso = 0` (clave del requerimiento)

Nota:
- Mantener `Bodega_Origen/Bodega_Destino` con misma regla actual de preimpresión.

## Concurrencia
`Get_Single_By_Licencia` + `Insert` dentro de la misma transacción reduce carrera en un mismo proceso.
Recomendado adicional (si DB lo permite): índice único en `Codigo_barra` para blindar duplicados por concurrencia cruzada.

## Criterios de aceptación
1. Caso normal:
   - imprimir fardo sin haber cerrado/imprimido licencia.
   - resultado: existe licencia madre en `i_nav_barras_pallet` con `Impreso=0`.
2. Caso existente:
   - si licencia ya existe, no duplica.
3. Caso error DB:
   - no imprime fardo y muestra error de persistencia.
4. Cierre posterior de licencia:
   - el flujo de cierre/licencia no rompe por existencia previa.
   - debe actualizar/reusar registro, no duplicar.

## Decisión de capa
Este ajuste es de **BOF (UI + lógica de proceso)**, no de WS/HH.
Razón: el problema nace en la secuencia de impresión local de OC y su persistencia previa al cierre.

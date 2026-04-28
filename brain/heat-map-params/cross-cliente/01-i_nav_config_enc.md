# Cross-cliente: `i_nav_config_enc`

Tabla nominalmente del módulo NAV pero que en la práctica rige TODA la
configuración de empresa/bodega (incluso clientes SAP_BO y PREFACTURA la
usan). Es 1 fila por bodega lógica, no por empresa.

## Conteos de columnas

| Cliente | Cols | Filas (config sets) |
|---|---:|---:|
| BECOFARMA | 71 | 1 |
| K7 | 69 | 6 |
| MAMPA | 72 | 33 |
| BYB | 63 | 2 |
| CEALSA | 60 | 2 |
| **Union** | **78** | — |

## Schema drift detectado (vs BYB como referencia)

### BECOFARMA tiene 8 cols extra que BYB no
```
bodega_faltante, centro_costo_dep_erp, centro_costo_dir_erp, centro_costo_erp,
codigo_cliente_virtual, fecha_vence_defecto, lote_defecto_entrada_mercancia_sap,
requerir_centro_costo_obligatorio
```

### K7 tiene 10 cols extra Y faltan 4 (case-mismatch + duplicados)
```
NO TIENE (vs BYB - vienen en lower_case):
  codigo_bodega_erp_nc, lote_defecto_entrada_nc, rango_dias_importacion,
  recepcion_genera_historico

EXTRA (vienen en CamelCase + variantes):
  Codigo_Bodega_ERP_NC, Lote_Defecto_Entrada_NC, Rango_Dias_Importacion,
  bodega_faltante, centro_costo_dep_erp, centro_costo_dir_erp, centro_costo_erp,
  codigo_bodega_nc_erp, explosio_automatica_nivel_max, lote_defecto_nc
```

**HALLAZGOS BRUTALES en K7**:
- `codigo_bodega_erp_nc` (BYB) vs `Codigo_Bodega_ERP_NC` (K7) — case-mismatch.
- `codigo_bodega_nc_erp` adicional (mismo concepto, palabras reordenadas) —
  duplicado por mala migración.
- `lote_defecto_nc` adicional (otra variante del mismo campo).
- `explosio_automatica_nivel_max` — **typo** (le falta la "n", debería ser
  `explosion_*`). Si el WebAPI usa el nombre canónico, K7 fallará.

### MAMPA tiene 9 cols extra
Mismo set de BECOFARMA + 1 propio: `cantidad_en_presentacion_transacciones_out`.

### CEALSA tiene 5 cols MENOS y 2 extra
```
NO TIENE: bodega_facturacion, bodega_prorrateo, bodega_prorrateo1,
          equiparar_productos, valida_solo_codigo
EXTRA: fecha_vence_defecto, lote_defecto_entrada_mercancia_sap
```

## Matriz de valores reales (1ra fila por cliente)

> Solo cols con valor relevante o que muestran diferencia interesante.

| Columna | BECO | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| **nombre_ejecutable** | SAPBOSync.exe | SAPBOSyncKillios.exe | SAPBOSyncMampa.exe | NavSync.exe | CEALSASync.exe |
| **interface_sap** | True | True | True | False | False |
| **IdAcuerdoEnc** | 0 | 0 | 0 | NULL | **1** |
| **IdProductoEstado** (default ingreso) | 1 | 1 | **3** | 1 | 0 |
| **IdProductoEstado_NC** (no comercial) | 3 | **14** | 7 | NULL | NULL |
| **IdTipoEtiqueta** | 8 | 10 | 12 | 2 | 2 |
| **IdTipoRotacion** | 3 | 3 | 3 | 3 | NULL |
| **IdIndiceRotacion** | NULL | 0 | **4** | NULL | NULL |
| **IdTipoDocumentoTransferenciasIngreso** | 3 | 3 | 3 | **8** | 3 |
| **Ejecutar_En_Despacho_Automaticamente** | True | True | True | False | False |
| **explosion_automatica** | False | True | False | True | False |
| **implosion_automatica** | False | True | False | True | False |
| **inferir_bonificacion_pedido_sap** | False | False | False | False | False |
| **push_ingreso_nav_desde_hh** | False | False | False | False | False |
| **rechazar_bonificacion_incompleta** | False | False | False | False | False |
| **rechazar_pedido_incompleto** | 0 | 1 | 0 | 0 | 0 |
| **reservar_umbas_primero** | False | False | False | True | False |
| **sap_control_draft_ajustes** | False | False | False | False | False |
| **sap_control_draft_traslados** | False | False | False | False | False |
| **bodega_facturacion** | NULL | BOD7 | NULL | NULL | (no col) |
| **bodega_faltante** | NULL | NULL | 23 | (no col) | (no col) |
| **valida_solo_codigo** | False | False | True | False | (no col) |
| **requerir_centro_costo_obligatorio** | False | (no col) | True | (no col) | (no col) |
| **rango_dias_importacion** (lower) | NULL | (no col) | 365 | NULL | NULL |
| **Rango_Dias_Importacion** (Camel) | (no col) | 0 | (no col) | (no col) | (no col) |
| **vence_defecto_nc** | 1900-01-01 | 1900-01-01 | 1900-01-01 | NULL | NULL |
| **nombre** | B01 GENERAL BECOFARM | BOD1 | CEDIS | Mi Configuracion | Mi Configuracion |

## Lecturas operativas

### Interface ERP — taxonomía corregida
- **SAP B1**: BECOFARMA, K7, MAMPA (3 de 5).
- **NAV (Microsoft Dynamics)**: BYB.
- **PREFACTURA dedicada**: CEALSA (no envía stock al ERP, envía rubros de
  cobro contra acuerdo comercial).

Esto invalida la suposición previa de que NAV era mayoritario. La WebAPI .NET 10
debe asumir SAP_BO como caso default y NAV/PREFACTURA como variantes.

### `IdAcuerdoEnc=1` solo en CEALSA
Confirma que CEALSA usa acuerdos comerciales activamente (cabecera del modelo
de prefactura). Ningún otro los toca: BECO/K7/MAMPA tienen `0` y BYB `NULL`.

### `IdProductoEstado` de default al recibir
- BECOFARMA, K7, BYB: estado **1** (probable "Buen Estado").
- MAMPA: estado **3** (mapeo distinto — habría que inspeccionar el catálogo
  `producto_estado` de MAMPA para confirmar).
- CEALSA: **0** (sin estado real — no usa el control de calidad).

### `Ejecutar_En_Despacho_Automaticamente` divide la operación
- **True**: BECOFARMA, K7, MAMPA (los 3 SAP B1) — el despacho dispara
  automáticamente las transacciones al ERP al cerrar.
- **False**: BYB, CEALSA — el despacho queda en cola, otro proceso lo empuja.

### `bodega_facturacion=BOD7` solo K7
K7 (Killios) tiene una bodega lógica `BOD7` para cargar la facturación. No hay
una bodega física BOD7 en la lista de 6 activas — debe ser virtual / lógica
que existe solo para que SAP B1 vea movimientos.

### `bodega_faltante=23` solo MAMPA
MAMPA tiene una bodega 23 dedicada para registrar faltantes. El operador no
los ajusta, los mueve. Esto es trazabilidad operativa fuerte.

### `valida_solo_codigo=True` solo MAMPA
En MAMPA cuando se escanea un producto se valida solo por código (no por
licencia / lote / fecha). Lógico para tienda con miles de SKUs.

### `requerir_centro_costo_obligatorio=True` solo MAMPA
MAMPA pide centro de costo obligatorio en transacciones — coherente con
control financiero por punto de servicio (33 puntos).

### Casos no comerciales (NC)
- Solo BECOFARMA, K7, MAMPA tienen `IdProductoEstado_NC` valuado:
  - BECOFARMA → estado **3** para NC.
  - K7 → estado **14** para NC.
  - MAMPA → estado **7** para NC.
- BYB y CEALSA tienen NULL → no manejan no comerciales como concepto.

### Banderas que NADIE tiene activadas
- `inferir_bonificacion_pedido_sap`, `push_ingreso_nav_desde_hh`,
  `rechazar_bonificacion_incompleta`, `sap_control_draft_ajustes`,
  `sap_control_draft_traslados`: todas False en los 5 clientes.

Esto significa que la WebAPI puede asumir el camino "no draft" en SAP por
defecto y exponer estas banderas pero no esperar que se usen pronto.

### Banderas activas SOLO en uno
- `reservar_umbas_primero=True` solo BYB → política de stock de BYB consume
  primero las unidades base (UMBAS) y deja el packing para el final.
- `explosion_automatica=True` y `implosion_automatica=True` solo K7 y BYB.
- `rechazar_pedido_incompleto=1` solo K7.

## Pendientes Q-* derivadas
- **Q-K7-DUPLICADOS-CONFIG**: confirmar si las 4 variantes del mismo campo
  (`codigo_bodega_erp_nc` lower / `Codigo_Bodega_ERP_NC` Camel /
  `codigo_bodega_nc_erp` reordered / `lote_defecto_nc`) las lee todas el
  código o son zombies de migración.
- **Q-K7-TYPO-EXPLOSION**: `explosio_automatica_nivel_max` (typo K7) vs
  `explosion_automatica_nivel_max` (canónico). Confirmar si el código compila
  contra el typo en K7.
- **Q-MAMPA-IDPRODESTADO-3**: por qué default es 3 y no 1 como los demás.
- **Q-MAMPA-IDINDICE-4**: qué significa `IdIndiceRotacion=4` en MAMPA (los
  demás NULL o 0).
- **Q-CEALSA-IDACUERDO-1**: qué acuerdo es el `IdAcuerdoEnc=1` (default? único?
  marcador?). Cruzar con `cealsa_vwacuerdocomercialenc`.
- **Q-K7-BOD7**: existe la bodega física BOD7 o es solo lógica para
  facturación SAP.
- **Q-MAMPA-BOD23-FALTANTES**: confirmar que la bodega 23 sirve para
  registrar faltantes y cómo entra el stock ahí.

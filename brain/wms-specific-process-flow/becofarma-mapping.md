---
output_type: mapping-profundo-cliente
client: BECOFARMA
db_name: IMS4MB_BECOFARMA_PRD
authored_by: agente-brain (sesion replit)
authored_at: 2026-04-28T23:15:00-03:00
ciclo: 8c (sesion fuera-de-ciclo, BD nueva)
fuente: SQL live READ-ONLY 28-abr-2026
status: ratificado-tecnico-pendiente-validacion-erik
relacionado_con: clients/becofarma.md
---

## ⚠ Reinterpretacion post-Erik (29-abr-2026): BD diagnostica, no productiva

Tras la respuesta de Erik el 28-abr-2026 (ver L-014 y L-015 en `brain/learnings/`):

- Esta BD es un **snapshot diagnostico restaurado para entrenamiento del agente**, NO la productiva.
- Los hallazgos de **schema, modulos, naming, catalogos, configuracion** (H-025 hasta H-027, H-030, mas H15/16/18/19/20/23/24 documentados sin evento) **siguen siendo validos** — afinidad-procesos confirmable.
- Los hallazgos de **salud operativa** que dependian de medir flujo vivo — especialmente **H-028 (i_nav_transacciones_out (outbox) 85% pendiente)** y **H-029 (44% Pickeado terminal)** — **NO se pueden interpretar como sintomas operativos**. Son la foto congelada del momento del backup.
- En la productiva real `SAPBOSync.exe` **si corre** (Erik confirmado) y procesa el i_nav_transacciones_out con normalidad. El %  pendiente real puede ser muy distinto al de esta copia.

Los datos numericos del documento se conservan tal cual estan medidos en esta copia. Lo que se ajusta son las **inferencias operativas** que se intentaron derivar de ellos.

---

# Mapeo profundo de IMS4MB_BECOFARMA_PRD

> Sesion fuera-de-ciclo: la BD aparecio el 28-abr-2026 a las 08:32 (creada en SQL hoy). Erik la asignio para enriquecer el brain. Este documento es el INVENTARIO EXHAUSTIVO; los hallazgos accionables se elevan via `_inbox/` para ratificacion.

## TL;DR

| Aspecto | Valor |
|---|---|
| Cliente | BECOFARMA (`IdEmpresa=1`), distribuidora farmaceutica |
| ERP | SAP B1 via DI-API (binario `SAPBOSync.exe`) |
| Tablas | 354 (mas que K7=345, BB=348, CEALSA=351) |
| Vistas | 210 |
| SPs | 40 (mucho menos que BB=76, casi igual a CEALSA=39 y K7=39) |
| FKs | 378 |
| Triggers | 0 |
| Total filas | ~1,037,089 |
| Bodegas | 1 (GENERAL) |
| Empresa | 1 |
| Patron operativo | 98% PIK (extremo outbound) |
| Estados pedido dominantes | Despachado 51% + Pickeado 44% |
| Outbox SAP | 36,576 filas, **85% pendientes** |
| Naming | PascalCase + snake_case (inconsistencia INTRA-BD en `i_nav_transacciones_out`) |

## 1. Inventario comparativo entre las 4 BDs

| BD | Tablas | Vistas | SPs | FKs | Indices | Filas totales |
|---|---:|---:|---:|---:|---:|---:|
| **IMS4MB_BECOFARMA_PRD** | **354** | 210 | 40 | 378 | 603 | ~1.0M |
| IMS4MB_BYB_PRD | 348 | 228 | 76 | 392 | 625 | ~5M+ |
| IMS4MB_CEALSA_QAS | 351 | 210 | 39 | 374 | 584 | (no medido) |
| TOMWMS_KILLIOS_PRD | 345 | 220 | 39 | 389 | 608 | (no medido) |

## 2. Tablas exclusivas de BECOFARMA (13)

| Tabla | Filas | Modulo | Comentario |
|---|---:|---|---|
| `ComparacionInventario` | (no medido) | inventario | Modulo de comparacion entre WMS y SAP |
| `Licencias` | 5 | farmacia | Licencias sanitarias por producto-lote |
| `LIcencias2` | 2 | farmacia | **Typo doble I que persistio**, sucesor de Licencias |
| `Presentacion_Factor` | (no medido) | productos | Factor de conversion entre presentaciones |
| `cliente_config` | 0 | auto-routing | Auto-routing por cliente |
| `proveedor_config` | 0 | auto-routing | Auto-routing por proveedor |
| `log_error_wms_pe` | 7,098 | logging segmentado | Errores en flujo de pedidos |
| `log_error_wms_reab` | 6,462 | logging segmentado | Errores en reabastecimiento |
| `log_verificacion_bof` | (no medido) | verificacion | Bitacora de verificaciones del BOF |
| `pedidos_no_despachados` | (no medido) | reporting | Probable vista materializada |
| `producto_presentacion1` | (no medido) | productos | Sufijo "1" sugiere migracion paralela |
| `trans_verificacion_etiqueta` | (no medido) | verificacion | Transacciones de verificacion etiqueta |
| `verificacion_estado` | 2 | verificacion | Catalogo de estados |

(Las otras tablas de logging segmentado — `log_error_wms_rec`, `log_error_wms_ubic`, `log_error_wms_pick` — tambien son exclusivas pero pueden existir como nombre alternativo en otras BDs; verificar caso-sensitive).

## 3. Tablas faltantes en BECOFARMA vs union (BYB ∪ CEALSA ∪ KILLIOS)

73 tablas. Categorias:

### 3.1 ASP.NET membership (legacy, BB-only)

`aspnet_Applications`, `aspnet_Membership`, `aspnet_Roles`, `aspnet_SchemaVersions`, `aspnet_Users`, etc. Estas son legacy de un modulo web ASP.NET viejo que solo BB conserva. **OK que no esten en BECOFARMA**.

### 3.2 Especificas de CEALSA

`Polizas_CEALSA`, `Polizas_Ilegibles`, `Inv_SAP`, `Inv_WMS`, `ClientesNAV`, `Auditoria`, `Contacto`, `Infraestructura`, `Organizacion`, `Propuesta`, `Prospecto`, `SiteContent`, `Usuario_Bitacora`. Estructura de CEALSA como 3PL. **OK que no esten en BECOFARMA**.

### 3.3 Otras

`TMP_UBICACIONES_FALTANES` (typo en nombre, "FALTANES" debe ser "FALTANTES"), tablas de catalogo de proveedores/clientes alternativas que BECOFARMA no usa.

## 4. Catalogo de tareas (sis_tipo_tarea)

BECOFARMA: 30 tipos.
BB: 35 tipos.
CEALSA-QAS: 33 tipos.
K7: 35 tipos.

### 4.1 Faltantes en BECOFARMA (vs BB):

- `ANUL_PICK` — anulacion de picking
- `AJLOTENI` — ajuste lote negativo inicial
- `AJLOTEPI` — ajuste lote positivo inicial
- `AJVENCENI` — ajuste vencimiento negativo inicial
- `AJVENCEPI` — ajuste vencimiento positivo inicial

Probable razon: catalogo congelado en epoca anterior. Coherente con la `fec_agr=2017` de `i_nav_config_enc`.

### 4.2 Catalogo completo BECOFARMA

```
1=RECE  2=UBIC  3=CEST  4=TRAS  5=DESP  6=INVE  7=AJUS
8=PIK   9=DEVP 10=PICK 11=VERI 12=PACK
13=AJCANTP 14=AJPESO 15=AJVENC 16=AJLOTE 17=AJCANTN 18=AJCANTNI 19=AJCANTPI
20=EXPLOSION
21=UBIC PICKING  (con espacio, en K7 es UBIC_PICK con underscore — H18b inconsistencia naming)
23=REABMAN
24=REUBICSTOCKRES
25=REEMP_BE_PICK 26=REEMP_ME_PICK 27=REEMP_NE_PICK
28=REEMP_BE_VERI 29=REEMP_ME_VERI
34=CESTI 35=CUBII
```

(Nota: faltan los IDs 22, 30-33; gaps probablemente de tipos eliminados o reservados).

### 4.3 Hallazgo H19: PIK vs PICK

`8=PIK` y `10=PICK` co-existen. Toda la operacion usa `8=PIK` (7081 filas). `10=PICK` parece sin uso en `tarea_hh`. Sugiere migracion / co-existencia de codigos legacy y nuevos sin consolidar.

## 5. Naming en BECOFARMA: inconsistencia intra-BD (H15)

Verificacion empirica de las primeras cols de tablas representativas:

| Tabla | Cols 1-3 | Convencion |
|---|---|---|
| `trans_pe_enc` | `IdPedidoEnc, IdBodega, IdCliente` | PascalCase |
| `trans_despacho_enc` | `IdDespachoEnc, IdBodega, IdPropietarioBodega` | PascalCase |
| `trans_oc_enc` | `IdOrdenCompraEnc, IdPropietarioBodega, IdProveedorBodega` | PascalCase |
| `trans_re_enc` | (similar) | PascalCase |
| `i_nav_transacciones_out` | `idtransaccion, idempresa, idbodega` | **snake_case** |
| `i_nav_ped_traslado_enc` | `No, Posting_Date, Receipt_Date` | **Naming nativo SAP B1** |

**Conclusion**:
- Las tablas trans_* son del esquema WMS estandar (PascalCase).
- `i_nav_transacciones_out` es la unica trans-like en snake_case.
- `i_nav_ped_traslado_*` son **replicas directas** de tablas SAP B1 (`OWTR`, `WTR1`), preservando el naming nativo SAP.

**Implicancia para EF Core en la WebAPI .NET 10**: para BECOFARMA hay que mapear con `[Column(...)]` en al menos `i_nav_transacciones_out` (snake_case) y todas las `i_nav_ped_*` (naming SAP).

## 6. Top 30 tablas por volumen

| # | Tabla | Filas | MB |
|---:|---|---:|---:|
| 1 | trans_movimientos | 149,577 | 46 |
| 2 | cliente_tiempos | 139,498 | 11 |
| 3 | trans_pe_det_log_reserva | 59,517 | 36 |
| 4 | trans_picking_ubic | 57,325 | 18 |
| 5 | trans_picking_ubic_stock | 57,305 | 17 |
| 6 | i_nav_ped_traslado_det | 56,224 | 23 |
| 7 | trans_pe_det | 53,505 | 21 |
| 8 | trans_picking_det | 53,070 | 9 |
| 9 | log_error_wms | 52,418 | 12 |
| 10 | i_nav_transacciones_out | 36,576 | 16 |
| 11 | stock_hist | 33,591 | 6 |
| 12 | trans_despacho_det | 31,486 | 6 |
| 13 | stock | 30,195 | 12 |
| 14 | stock_res | 26,114 | 9 |
| 15 | trans_picking_op | 16,766 | 1 |
| 16 | i_nav_ejecucion_enc | 10,120 | 0 |
| 17 | i_nav_ejecucion_res | 10,120 | 0 |
| 18 | trans_pe_enc | 8,539 | 6 |
| 19 | i_nav_ped_traslado_enc | 8,441 | 6 |
| 20 | i_nav_ejecucion_det_error | 7,588 | 3 |
| 21 | trans_inv_stock_prod | 7,235 | 0 |
| 22 | tarea_hh | 7,227 | 2 |
| 23 | log_error_wms_pe | 7,098 | 2 |
| 24 | trans_picking_enc | 7,081 | 2 |
| 25 | log_error_wms_rec | 6,941 | 1 |
| 26 | log_error_wms_reab | 6,462 | 1 |
| 27 | log_error_wms_ubic | 6,426 | 1 |
| 28 | log_error_wms_pick | 5,762 | 1 |
| 29 | dh_ocupacion_bodega | 5,474 | 0 |
| 30 | trans_oc_det | 5,249 | 3 |

## 7. Q-009 / Q-011 / Q-013 / Q-014 replicadas en BECOFARMA (afinidad-procesos)

### Q-009 (i_nav_transacciones_out shape) — CRITICO

```
total           = 36,576
con_pedido      = 36,576 (100%)
con_despacho    = 36,576 (100%)
con_recepcion   = 36,576 (100%)
con_oc          = 36,576 (100%)
con_tipo        = 36,576 (100%)
enviados (=1)   =  5,313 (14.5%)
pendientes (=0) = 31,263 (85.5%)

tipo_transaccion:
  SALIDA        = 31,486 (86%)
  INGRESO       =  5,090 (14%)
```

**H22 (NUEVO)**: en BECOFARMA TODAS las filas tienen TODAS las FKs pobladas. **Invalida parcialmente H08** (que decia que el i_nav_transacciones_out solo se simplifica a 2 tipos efectivos). Para BECOFARMA hay que respetar la lectura nativa: cada fila trae el contexto completo.

**Comparativa cross-cliente Q-009**:

| BD | total | con_pedido | con_despacho | con_recepcion | con_oc | enviados |
|---|---:|---:|---:|---:|---:|---:|
| K7 | 24,193 | 19,799 | 19,799 | (parcial) | (parcial) | ~24K (saludable) |
| BB | 533,329 | 422,427 | 422,427 | (parcial) | (parcial) | (saludable mixto) |
| BECOFARMA | 36,576 | 36,576 | 36,576 | **36,576** | **36,576** | **5,313** |

### Q-011 (bypass Despachado sin trans_despacho_det)

BECOFARMA: 4,411 Despachados, **bypass=0** (consulta exitosa, todos tienen detalle).

Lectura: BECOFARMA no exhibe el patron de bypass que H06 detecto en K7. Sin embargo, este no es un contrarrebatimiento de H06 — solo confirma que **el problema es heterogeneo entre clientes** y refuerza la doctrina "afinidad-procesos confirmable, afinidad-datos diferida".

### Q-014 (distribucion tareas HH)

| Codigo | Tarea | Cantidad | % |
|---|---|---:|---:|
| 8 | PIK | 7,081 | 98.0% |
| 1 | RECE | 144 | 2.0% |
| 6 | INVE | 2 | 0.0% |

**Solo 3 tipos de tareas registradas**. Esto extiende H07 con un cuarto perfil ("extremo outbound").

### Q-013-like (pedidos fiscales)

No replicada por completo (BECOFARMA no parece tener `control_poliza` poblado en `trans_pe_tipo`; el modelo de polizas fiscales puede no aplicar o usarse distinto). Pendiente para Erik.

## 8. Modulos exclusivos: estructura completa

### 8.1 Modulo verificacion etiquetas (H14)

```
tipo_etiqueta(IdTipoEtiqueta, Nombre, Alto, Ancho, MargenIzq, MagenDer, MargenSup, MargenInf,
              user_agr, fec_agr, user_mod, fec_mod, activo, dpi, codigo_zpl,
              Idclasificacion_etiqueta, es_inkjet)
              -- 3 filas

tipo_etiqueta_detalle(IdTipoEtiquetaDetalle, IdTipoEtiqueta, orden, nombre, campo,
                      coor_x, coor_y, width, height)
                      -- 3 filas

producto_clasificacion_etiqueta(Idclasificacion_etiqueta, Descripcion)
                                -- 3 filas

verificacion_estado(IdEstado, descripcion, user_agr, fec_agr, activo)
                    -- 2 filas

trans_verificacion_etiqueta -- transacciones (sin medir)
log_verificacion_bof        -- bitacora desde el front office (sin medir)
```

**Implicancia**: el modulo permite definir layouts de etiqueta (campos, posiciones, margenes) por clasificacion de producto, soportando ZPL (Zebra) e impresoras inkjet. Cada impresion genera registro de verificacion.

### 8.2 Modulo regla de vencimiento (H24, vacio)

```
regla_vencimiento(IdReglaVencimiento, NombreRegla, IdBodega, IdProductoFamilia,
                  IdProductoClasificacion, TiempoVencimientoDias, TipoNotificacion,
                  IdPropietarioMercancia, IdProveedor, IdCliente, Activa,
                  FechaCreacion, ...)
                  -- 0 filas

reglas_vencimiento_contacto(IdContacto, NombreContacto, CorreoElectronico,
                            TelefonoFijo, TelefonoMovil, IdReglaVencimiento, ...)
                            -- 0 filas

cliente_lotes(IdClienteLote, IdCliente, IdProducto, Lote, IdProductoEstado,
              user_agr, fec_agr, user_mod, fec_mod, activo, bloquear)
              -- 0 filas
```

**Implicancia**: subsistema completo construido para notificacion proactiva de vencimientos a contactos (correo, telefono fijo, telefono movil) y bloqueo de lotes especificos por cliente. **Sin uso productivo todavia**.

### 8.3 Modulo licencias (H27b)

```
-- Producto-licencia (sanitaria, cols con ESPACIOS en nombre, indica origen Excel)
Licencias(LICENCIA, [CODIGO PRODUCTO], LOTE, [FECHA VENCIMIENTO], CANTIDAD, BODEGA)  -- 5 filas
LIcencias2 (typo II) -- 2 filas, FECHA VENCIMIENTO ahora es datetime (en Licencias era float!)

-- Dispositivo-licencia (HH/PC)
licencia_item(idDisp, identificacion, tipo, bandera, estado, fecha_sistema)  -- 18 filas
licencia_llave(idLlave, Llave)  -- 1 fila (la llave maestra)
licencia_login(idDisp, valor)  -- 37 dispositivos
licencia_pendientes(licencia, Fecha_Llegada, Fecha_Inicial, Fecha_ticket)  -- 45 pendientes
licencia_solic(idDisp, identificacion, tipo, estado, fecha_solicitud)  -- 1 solicitud
licencias_pendientes_retroactivo  -- regularizacion historica
temp_licencia_llave  -- temporal
```

**Implicancia**:
- `Licencias` (sanitaria) tiene cols con espacios — origen probable: import Excel de un sistema regulatorio externo.
- Cambio de tipo `FECHA VENCIMIENTO` de `float` (Excel-style serial date) a `datetime` entre `Licencias` y `LIcencias2` confirma migracion en curso.
- Sistema de licencias por dispositivo HH (37 dispositivos registrados) tiene workflow de aprobacion/pendientes/retroactivo.

## 9. Hallazgos generables (candidatos a inbox/proposals)

| ID | Titulo | Tipo | Accion sugerida |
|---|---|---|---|
| H25 | BECOFARMA es BD restaurada/migrada el 28-abr-2026 (config 2017) | Hecho | Confirmar con Erik el motivo |
| H26 | Logs segmentados por proceso son patron exclusivo de BECOFARMA | Patron | Considerar adoptar en otras BDs / WebAPI |
| H27 | Modulo de verificacion de etiquetas exclusivo (ZPL+inkjet) | Modulo | Documentar como capability flag |
| H15 | Naming inconsistente intra-BD (snake_case en i_nav_transacciones_out, PascalCase en trans_*, SAP-naming en i_nav_ped_*) | Tech debt | Mapear con [Column] en EF Core |
| H28 | Backlog del i_nav_transacciones_out: 85% pendiente | Operativo critico | Validar SAPBOSync.exe esta corriendo |
| H18 | Catalogo sis_tipo_tarea congelado: faltan AJ*NI/PI y ANUL_PICK | Tech debt | Sincronizar catalogo con K7/BB |
| H19 | PIK (id=8) y PICK (id=10) co-existen, solo PIK se usa | Tech debt | Decidir codigo canonico, deprecar el otro |
| H20 | SP MantenimientoBaseDeDatos usa DBCC SHRINKDATABASE (controvertido) | Operativo | Revisar y posiblemente eliminar SHRINK |
| H29 | 44% de pedidos en estado "Pickeado" (no transitan a Despachado) | Operativo | Hipotesis: workflow verificacion bloquea |
| H30 | Outbox BECOFARMA tiene 100% FKs en cada fila (invalida parcialmente H08) | Patron | Dif documentado por cliente en bridge |
| H23 | Modulo cliente_lotes (bloqueo de lotes por cliente) construido sin uso | Capacidad | Activar para cumplimiento sanitario |
| H24 | Modulo regla_vencimiento + notificacion a contactos construido sin uso | Capacidad | Activar como piloto en BECOFARMA |

## 10. Implicancias para la WebAPI nueva (.NET 10)

### Diseño multi-tenant

1. **Capability flags por cliente** (extender `i_nav_config_enc`):
   - `usa_logs_segmentados` (BECOFARMA si, otros no)
   - `usa_verificacion_etiquetas` (BECOFARMA si, otros no)
   - `usa_reglas_vencimiento` (BECOFARMA si una vez activado)
   - `outbox_pattern` (`selective_fks` para K7/BB, `universal_fks` para BECOFARMA)

2. **EF Core mapeo de columnas por tabla**:
   - `[Column("idpedidoenc")]` para `i_nav_transacciones_out` en BECOFARMA
   - Naming nativo SAP en `i_nav_ped_traslado_*` (mantener como esta)
   - PascalCase para `trans_*`

3. **KPIs por perfil operativo**:
   - Picking productivity (BECOFARMA pesa 98%, K7 71%, BB 19%)
   - Putaway productivity (BB 50%, otros bajo)
   - Outbox health (% pendiente, antigüedad maxima)

4. **Endpoint de regla_vencimiento**:
   - Si el modulo se activa, BECOFARMA es el cliente piloto natural.
   - Endpoint `/api/clients/{id}/expiration-rules` CRUD.
   - Job de notificacion (correo/SMS) lee `reglas_vencimiento_contacto`.

5. **Documentar SAPBOSync.exe**:
   - Corregir `interfaces-erp-por-cliente.md`: BECOFARMA usa `SAPBOSync.exe` (no `SAPSYNC.vbproj`).
   - Aclarar si SAPBOSync.exe es el binario compilado de SAPSYNC.vbproj o un componente distinto.

## 11. Pendientes para Erik

1. **Confirmar motivo de la BD nueva**: ¿produccion migrada? ¿copia diagnostica? ¿sandbox?
2. **Confirmar estado de SAPBOSync.exe**: ¿esta corriendo? ¿hay backlog porque migrámos hoy y no se conecto aun?
3. **Aclarar `Pickeado` como estado terminal**: ¿es bug o es el flujo normal por el modulo de verificacion?
4. **Validar capability flags**: ¿queres que la WebAPI tenga los `usa_*` flags por cliente, o se infiere de otra cosa?
5. **Decidir destino de `Licencias`/`LIcencias2`**: ¿una de ellas se deprecia? ¿migrar definitivamente?
6. **Activar regla_vencimiento**: ¿es prioridad de negocio para BECOFARMA?

## 12. Cross-references

- `clients/becofarma.md` — perfil corto del cliente (este documento es la version exhaustiva).
- `wms-specific-process-flow/interfaces-erp-por-cliente.md` — corregir SAPSYNC.vbproj → SAPBOSync.exe.
- `outputs/ratificaciones/H06-H11-ratificacion.md` — H22 invalida parcialmente H08, considerar al ratificar.
- `_inbox/20260428-23**-H{25-30}-becofarma-*.json (H15/16/18/19/20/23/24 quedan documentados aqui sin evento)` — eventos para promover hallazgos a hipotesis ratificables (proximo commit).

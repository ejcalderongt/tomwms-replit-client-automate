# Capa 3 — Parametros de INTERFACE con ERP

> Define que ERP (o sistema externo) se conecta con el WMS y como.
> Los flags relevantes estan distribuidos en `i_nav_config_enc`,
> tablas de outbox/inbox, y tablas dedicadas por cliente.

## Taxonomia de interfaces (5 categorias)

| Categoria | Definicion | Cliente representativo |
|---|---|---|
| **NAV** | Microsoft Dynamics NAV (clasico) | BYB |
| **SAP** | SAP B1 / Hana | a confirmar |
| **SAP_BO** | SAP Business One | a confirmar |
| **PREFACTURA** | Solo rubros de cobro, NO transacciones de stock | CEALSA |
| **SIN_INTERFACE** | Sistema standalone, sin integracion ERP | a confirmar (¿INELAC?) |

## Flags discriminantes

### En `i_nav_config_enc` (a nivel empresa/bodega)

| Flag | Activa interface |
|---|---|
| `interface_sap` | SAP |
| `crear_recepcion_de_compra_nav` | NAV |
| `crear_recepcion_de_transferencia_nav` | NAV |
| `push_ingreso_nav_desde_hh` | NAV |
| `sap_control_draft_ajustes` | SAP |
| `sap_control_draft_traslados` | SAP |
| `inferir_bonificacion_pedido_sap` | SAP |
| `nombre_ejecutable` (string) | identifica el .exe del sync (NavSync.exe, CEALSASync.exe, SAPBOSync.exe, etc.) |

### En `bodega`
| Flag | Activa interface |
|---|---|
| `interface_SAP` (mayuscula!) | SAP a nivel bodega |
| `restringir_areas_sap` | restriccion areas SAP |
| `marcar_registros_enviados_mi3` | Marca registros enviados a MI3 |
| `codigo_bodega_erp` | Codigo de la bodega en el ERP externo |

## Tablas de transmision (outbox/inbox)

### Outbox WMS → ERP
| Tabla | Funcion | Existe en |
|---|---|---|
| `i_nav_transacciones_out` | Cola de transacciones a enviar al ERP | TODOS |
| `i_nav_transacciones_out_error` | Errores de transmision | TODOS (probable) |
| `tmp_i_nav_transacciones_out` | Staging temporal | CEALSA (a verificar otros) |

### Inbox ERP → WMS
| Tabla | Funcion | Existe en |
|---|---|---|
| `i_nav_ped_compra_enc/det` | Pedidos de compra desde ERP | TODOS (probable) |
| `i_nav_ped_compra_det_lote` | Lotes pre-asignados | BYB |
| `i_nav_barras_pallet` | Tarimas pre-recibidas | a verificar |
| `i_nav_acuerdo_*` | Acuerdos comerciales | a verificar |

### Vistas especificas por cliente
| Vista | Cliente | Funcion |
|---|---|---|
| `cealsa_vwacuerdocomercialenc/det` | CEALSA | Vista para acuerdos comerciales |
| `cealsa_vwclientes` | CEALSA | Vista de clientes |
| `Polizas_CEALSA` | CEALSA | Polizas especificas |

### Tablas exclusivas PREFACTURA
| Tabla | Cliente | Funcion |
|---|---|---|
| `trans_prefactura_enc` | CEALSA | Encabezado prefactura |
| `trans_prefactura_det` | CEALSA | Detalle (rubros) |
| `trans_prefactura_mov` | CEALSA | Movimientos prefactura |

## Mapeo cliente → interface (preliminar)

| Cliente | Interface primaria | Interface secundaria | Notas |
|---|---|---|---|
| BECOFARMA | a confirmar (SAP B1?) | — | Pendiente: ver `nombre_ejecutable` y flags |
| K7 | a confirmar (NAV o SAP?) | — | Pendiente: tiene `TRAS_WMS` y `PDV_NAV` (Pedido de Venta NAV) → NAV |
| MAMPA | a confirmar (SAP?) | — | Tiene `TRAS_SAP` (Traslado_Por_Estados_SAP) → SAP |
| BYB | NAV | — | `nombre_ejecutable=NavSync.exe`, `interface_sap=False` confirmado |
| CEALSA | PREFACTURA | — | No envia stock, envia rubros |

## Implicaciones para WebAPI .NET 10

La WebAPI debe definir un patron `IErpAdapter` con implementaciones:
- `NavErpAdapter` → habilita `crear_recepcion_de_compra_nav`, etc.
- `SapErpAdapter` → habilita `interface_sap`, drafts, bonificaciones.
- `PrefacturaAdapter` → distinto: no envia transacciones, gestiona rubros.
- `NoErpAdapter` → standalone (si aplica).

El routing al adapter se decide al iniciar la sesion segun
`i_nav_config_enc.nombre_ejecutable` o `interface_sap`.

## Cross-cliente (PENDIENTE)

PROXIMA ITERACION: capturar valor real de `nombre_ejecutable`,
`interface_sap`, `marcar_registros_enviados_mi3` en los 5 clientes.

## Pendientes

- Q-INTERFACE-MI3: que es MI3? (aparece en `marcar_registros_enviados_mi3`).
- Confirmar que cliente usa SAP B1 vs SAP Hana.
- Confirmar si INELAC tiene interface o es SIN_INTERFACE (recibe produccion).
- Documentar el flujo bidireccional CEALSA via prefactura (esquema completo).

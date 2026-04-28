# Cross-cliente: matriz exhaustiva de flags por capa

Este folder contiene la matriz `flag x cliente` con datos REALES extraídos
del SQL Server EC2 (52.41.114.122,1437) — copia parcial de las cinco
implementaciones productivas. Para cada capa se incluye:

1. **Schema drift**: qué columnas tiene cada cliente vs el resto.
2. **Valores reales**: lo que está activado en cada cliente HOY.
3. **Lecturas operativas**: qué significa que esa combinación esté ahí.

El método es: capturar la verdad declarada por la base, no inferirla del código.
Esto sirve de baseline para el code-deep-flow posterior (cómo viaja un parámetro
por backend / BOF / HH y afecta tablas).

## Conteos macro por cliente

| Cliente | Bodegas activas | Productos activos | producto_bodega | Estados producto | Propietarios reales | ERP |
|---|---:|---:|---:|---:|---:|---|
| BECOFARMA | 1 | 1.835 | 1.835 | 10 | 1 | SAP B1 (SAPBOSync.exe) |
| K7 (Killios) | 6 | 319 | 1.914 | 18 | 1 | SAP B1 (SAPBOSyncKillios.exe) |
| MAMPA | 33 | **31.397** | **1.036.101** | 19 | 1 | SAP B1 (SAPBOSyncMampa.exe) |
| BYB | 2 | 623 | 1.109 | 24 | 1 | NAV (NavSync.exe) |
| CEALSA | 2 (de 3) | 1.635 | 3.428 | **3.200** (3.197 ruido) | 3 | Prefactura (CEALSASync.exe) |

### Hallazgos brutales del macro

- **MAMPA tiene 31.397 productos activos**: explosión combinatoria por
  talla x color x SKU. La operación de tienda multitalla genera el catálogo
  más grande de los cinco.
- **CEALSA 3.200 estados RESUELTO en wave 5**: 3.197 son ruido de
  scaffolding automatico que crea "Buen Estado" por cada IdPropietario
  (insertados entre 2022-01-27 y 2025-04-15 por user_agr=6). Los unicos
  estados utiles son 4: Buen Estado, Mal Estado, RH, HR (significado de
  RH/HR pendiente). Filtrar siempre por IdPropietario presente en
  producto. Detalle en [`04-producto.md`](04-producto.md).
- **NINGÚN cliente tiene propietarios activos**: TOM no opera como 3PL en
  ninguno de los cinco. La columna `IdPropietario` aparece en muchas tablas
  pero está vacía o defaulteada en producción.
- **Tres de cinco clientes usan SAP Business One** (BECOFARMA, K7, MAMPA).
  Solo BYB es Microsoft Dynamics NAV. CEALSA es prefactura dedicada (no envía
  stock al ERP, envía rubros de cobro). La taxonomía de interface se
  simplifica: SAP_BO mayoritaria / NAV (BYB) / PREFACTURA (CEALSA).

## Archivos

- [`01-i_nav_config_enc.md`](01-i_nav_config_enc.md) — Configuración por
  empresa/bodega del módulo NAV (heredado, en realidad rige toda la app).
  78 cols únicas en union. Schema drift gigante. ERPs reales por cliente.
- [`02-bodega.md`](02-bodega.md) — 123 cols únicas. Flags por bodega y
  matriz de capabilities operativas (voz, ML, talla/color, fiscal, etc).
- [`03-tipos-documento.md`](03-tipos-documento.md) — `trans_oc_ti` (tipos
  de ingreso) y `trans_pe_tipo` (tipos de pedido) con datos cruzados.
- [`04-producto.md`](04-producto.md) — `producto` (60 cols), `producto_bodega`,
  `producto_estado`, `producto_estado_ubic` con distribuciones de flags
  reales por cliente. Resuelve el caso CEALSA 3.200 estados. Identifica
  capabilities NUNCA implementadas en TOM (serializado, kit, materia_prima,
  temperatura, arancel, hardware) y capabilities con drift severo
  (control_lote, control_vencimiento, IdTipoRotacion, IdSimbologia).

## Inventario cerrado

Las cuatro capas del nucleo de configuracion estan cubiertas. Drift
medido. Hipotesis Q-* abiertas registradas en cada archivo y en el
INDEX general. Esto cierra **Wave 5 (capa 4)** y deja el inventario
listo para arrancar el **code-deep-flow**: dado un parametro X, mapear
como viaja por el backend / BOF VB.NET / HH Android y que tablas
afecta.

## Pendientes residuales (no bloquean code-deep-flow)

- Q-* abiertas en cada archivo (Q-RESERVAR-EN-UMBAS, Q-CEALSA-ORIGEN-PROP-3197,
  Q-MAMPA-IDINDICE-4, Q-CEALSA-RH-HR, Q-CEALSA-IDTIPO-NULL,
  Q-BYB-NO-DISPONIBLE-NAV-BD, Q-MAMPA-MERMA-CARNE-FLUJO,
  Q-GENERA-LP-OLD-LEGADO, mas las 16 abiertas en wave 4).
- Investigar tablas inbox (`i_nav_*`, `cealsa_vw*`) — esto ya forma
  parte del code-deep-flow.
- Confirmar caso CLAVAUD (cliente futuro o legacy).
- Confirmar uso de `marcar_registros_enviados_mi3` (qué es MI3).

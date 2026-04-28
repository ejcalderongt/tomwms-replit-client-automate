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

| Cliente | Bodegas activas | Productos activos | Estados producto | Propietarios | ERP |
|---|---:|---:|---:|---:|---|
| BECOFARMA | 1 | 1.835 | 10 | 0 | SAP B1 (SAPBOSync.exe) |
| K7 (Killios) | 6 | 319 | 18 | 0 | SAP B1 (SAPBOSyncKillios.exe) |
| MAMPA | 33 | **31.397** | 19 | 0 | SAP B1 (SAPBOSyncMampa.exe) |
| BYB | 2 | 623 | 24 | 0 | NAV (NavSync.exe) |
| CEALSA | 2 (de 3) | 1.635 | **3.200** | 0 | Prefactura (CEALSASync.exe) |

### Hallazgos brutales del macro

- **MAMPA tiene 31.397 productos activos**: explosión combinatoria por
  talla x color x SKU. La operación de tienda multitalla genera el catálogo
  más grande de los cinco.
- **CEALSA tiene 3.200 estados de producto** (vs 10-24 en el resto): no es
  un catálogo de "Buen Estado / Cuarentena", debe haber algo por póliza /
  cobro / lote fiscal. Pendiente Q-CEALSA-3200-ESTADOS investigar.
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

## Pendientes (próxima vuelta)

- Capa producto (60 cols) cross-cliente con sample 10 productos y conteos
  por flag.
- Tabla `producto_estado` (¿por qué CEALSA tiene 3.200?).
- Investigar tablas inbox (`i_nav_*`, `cealsa_vw*`) para el code-deep-flow.
- Confirmar caso CLAVAUD (cliente futuro o legacy).
- Confirmar uso de `marcar_registros_enviados_mi3` (qué es MI3).

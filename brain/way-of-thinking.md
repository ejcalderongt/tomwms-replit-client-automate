# Way of thinking — TomWMS Brain

> Notas del autor (Erik) y principios operativos. Vivientes, se actualizan en cada ciclo.

## Principios operativos

### 1. No hardcoded, todo parametrizable

Todo flujo debe partir de datos concretos en su ambiente real y abstraerse al flujo. Hardcodear códigos (productos, bodegas, lotes, tipos) introduce sesgo de un ambiente sobre otros y rompe la portabilidad cliente-a-cliente.

**Caso real**: el legacy hardcodeaba el producto `47022` en escenarios de reserva. Ese SKU **no existe en TOMWMS_KILLIOS_PRD**. El bridge debe parametrizar el producto por cliente y resolverlo dinámicamente (ej: SKU del producto más movido del último mes en `i_nav_movimiento`).

### 2. Datos productivos por encima de la documentación legada

Cuando el código VB documenta un flag o comportamiento, y los datos productivos muestran otra cosa, **los datos ganan**. La documentación previa puede haber sido aspiracional, retrocompatible, o haber quedado obsoleta tras refactorizaciones.

**Caso real**: el enum VB `tRechazarPedidoIncompleto` sugería `.No=0`/`.Si=1` con interpretación ambigua. La interpretación funcional confirmada por Erik es: **`1 = ESTRICTO`** (WMS reserva el total o avisa al ERP que no puede), `0 = permisivo`.

### 3. Read-only contra producción siempre

Las BDs Killios y BYB son **productivas** (CEALSA es QAS pero con datos reales). Toda la operación del agente es `SELECT`/`EXEC` de SPs de lectura. Cero modificaciones. Whitelist de prefijos en `brain/wms-agent/wmsa/killios.py`.

### 4. Aprender desde la fuente de verdad correcta

`VW_Configuracioninv` solo expone metadatos (8 columnas). La fuente de verdad de los flags es la tabla base `i_nav_config_enc` (69 columnas). Cualquier herramienta de aprendizaje debe apuntar a la tabla, no a la vista.

Análogamente: la fuente de verdad del **comportamiento real del motor** es `trans_pe_det_log_reserva` (auditoría de cada reserva con `Caso_Reserva` y `MensajeLog`), no los enums del código.

## Terminología — reserva-webapi vs reserva-WMS

Hay **dos implementaciones del motor de reserva** convivientes:

| Nombre | Stack | Estado | Donde corre |
|---|---|---|---|
| **reserva-WMS** | VB.NET (legacy) | En producción HOY | TOMWMS_KILLIOS_PRD, IMS4MB_BYB_PRD |
| **reserva-webapi** | .NET Core, DalCore + EntityCore | Rama `dev_2028` sin publicar | (futuro) |

Erik reescribió casi toda la lógica de negocio en .NET Core para usar webapis más veloces. Quería unificar las dos pero el reto era titánico por la complejidad acumulada del VB. Decisión: dejar reserva-WMS estable en prod hasta que reserva-webapi esté lista.

**Cuando este brain dice "el comportamiento esperado"** se refiere a la **reserva-webapi (dev_2028)**, no a la reserva-WMS legacy. El bridge existe para **estabilizar reserva-webapi** antes de su pase a producción usando los expected del legacy como referencia, pero corrigiendo donde el legacy está mal.

**Cuando este brain dice "el comportamiento observado"** se refiere a lo que está pasando en `trans_pe_det_log_reserva` HOY — eso es reserva-WMS legacy.

> Nota del autor (Erik): "quise unificarlas pero parecía un reto titánico debido a su complejidad — entonces para integrarme a sistemas más modernos, reescribí casi toda la lógica de negocio en .NET Core para poder utilizar webapis más veloces ;)"

## Marca de sesión en logs

Los `Caso_Reserva` en `trans_pe_det_log_reserva` llevan sufijo `_EJC202310090957` que es la marca de Erik J Calderón en sesión 2023-10-09 09:57 (versión del motor del legacy compilado en esa fecha). Si aparece otro sufijo, indica un build distinto.

## Convenciones de comunicación

- Español rioplatense, sin emojis (salvo este `;)` del autor que va literal).
- Sin "esta listo para publicar" / "listo para deploy" — el brain es documentación viva, no se "publica".
- Sin "drizzle" / "db:push" — esto NO es un proyecto Drizzle, son 3 BDs SQL Server productivas en EC2.
- Los workflows del template Replit no se reinician — irrelevantes para la tarea.

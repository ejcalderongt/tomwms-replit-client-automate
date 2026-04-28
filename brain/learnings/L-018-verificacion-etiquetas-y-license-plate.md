---
protocolVersion: 1
id: L-018
title: Verificacion de etiquetas es CORE-WMS parametrizable por cliente; license plate (lic_plate) es identificador universal de stock fisico
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-29T01:00:00Z
target:
  codename: tomwms-arquitectura
  environment: cross-cliente
relatedQuestions: [H-027]
relatedDocs:
  - brain/wms-specific-process-flow/becofarma-mapping.md
  - brain/learnings/L-017-i-nav-transacciones-out-fks-sentinela-cero.md
  - brain/outputs/explicaciones/H29-becofarma-pickeado-corte-marzo-2026.md
status: closed
priority: high
tags: [verificacion-etiqueta, license-plate, lic_plate, HH, capability-flag, core-wms, parametrizable]
---

## Que aprendimos (de Erik, 29-abr-2026)

> "las funcionalidad de etiquetas de verificacion es un proceso ligado
> a la HH durante el proceso de verificacion se puede emitir una especie
> de licencia que hace unico lo que se esta verificando, esta funcionalidad
> es parametrizable por cliente y es parte del core de wms"

### Hechos confirmados:

1. **Es CORE de WMS, no plugin/extension**. Vive en el codebase principal
   de TOMWMS (no es un modulo aparte que se instale). Que en BECOFARMA
   exista `trans_verificacion_etiqueta` + `verificacion_estado` +
   `log_verificacion_bof` y NO en K7/BB significa que esos clientes
   **tienen el modulo apagado por configuracion**, no que les falte
   codigo.

2. **Parametrizable por cliente**. Hay un capability flag (probable:
   en `i_nav_config_enc` o tabla equivalente de configuracion por
   cliente) que activa/desactiva el flujo de verificacion. BECOFARMA
   ON, K7/BB OFF.

3. **Ligado al modulo HH (Handheld Android)**, no al BOF (Backoffice
   VB.NET). El operador en el HH dispara la verificacion fisica del
   producto/lote/etiqueta, y como parte del proceso **emite una licencia**
   (license plate) que sella la unicidad de lo verificado.

4. **Estado en la state machine de pedido**: en BECOFARMA el flujo es
   `NUEVO -> Pendiente -> Pickeado -> Verificado -> Despachado`. El
   estado `Verificado` es el output del modulo de verificacion. En K7/BB
   el flujo salta `Verificado` y va directo `Pickeado -> Despachado`.

## Hallazgo adicional sobre license plate (lic_plate)

Al investigar las tablas relacionadas, descubri que **la columna
`lic_plate` en `i_nav_transacciones_out` no es exclusiva del modulo
de verificacion** — es el identificador fisico del stock que se mueve
en CADA transaccion, en TODOS los clientes:

```
Cobertura de lic_plate en i_nav_transacciones_out (28-abr-2026):

K7:
  INGRESO    total=  4,394   con lic_plate=  4,394 (100.0%)   3,347 lics distintas
  SALIDA     total= 19,799   con lic_plate= 19,784 (99.9%)    2,745 lics distintas

BB:
  INGRESO    total=110,902   con lic_plate=110,671 (99.8%)  108,974 lics distintas
  SALIDA     total=422,427   con lic_plate=418,631 (99.1%)  104,274 lics distintas

BECOFARMA:
  INGRESO    total=  5,090   con lic_plate=  5,090 (100.0%)   4,880 lics distintas
  SALIDA     total= 31,486   con lic_plate= 31,486 (100.0%)   3,826 lics distintas
```

### Interpretacion del patron de cardinalidad:

- **INGRESO** -> lics distintas ~ filas (~1:1). Cada item recibido se
  etiqueta como una licencia nueva. **La etiqueta nace en la recepcion**.
- **SALIDA** -> lics distintas << filas (1:N). Una misma licencia origen
  alimenta multiples picks. La licencia identifica el origen fisico
  (pallet/caja) de donde se saca el producto.

### Tablas de licencia comunes a TODOS los clientes (core):

```
licencia_item                    -- relacion licencia <-> producto/lote
licencia_llave                   -- identificador unico/llave de licencia
licencia_login                   -- asignacion a operador
licencia_solic                   -- solicitudes de emision
licencias_pendientes_retroactivo -- cola de retroactivas
producto_clasificacion_etiqueta  -- reglas de etiquetado por producto
tipo_etiqueta                    -- catalogo de tipos
tipo_etiqueta_detalle            -- atributos por tipo
temp_licencia_llave              -- staging
```

### Tablas exclusivas BECOFARMA (modulo verificacion activado):

```
Licencias              -- PascalCase, paralelo al resto
LIcencias2             -- segundo modelo (legacy?)
licencia_pendientes    -- cola
trans_verificacion_etiqueta  -- detalle de cada verificacion
verificacion_estado    -- catalogo de estados
log_verificacion_bof   -- log del backoffice (BOF VB.NET)
```

## Implicancias para la WebAPI .NET 10

### IMP-1: contrato de transaccion debe incluir lic_plate como first-class

El contrato `OutboundTransactionExit` y `OutboundTransactionEntry` (ver
L-017 §IMP-2) debe incluir:

```csharp
public string LicPlate { get; set; }      // NOT NULL en practica
public decimal UdsLicPlate { get; set; }  // unidades en la licencia
```

### IMP-2: capability flag de verificacion etiquetas

La WebAPI debe consultar el flag de cliente al exponer endpoints de
estado de pedido. Si el cliente tiene verificacion activada, el flujo
expone `Verificado` como estado intermedio; si no, lo omite. Probable
ubicacion del flag: `i_nav_config_enc` (a confirmar con Erik).

### IMP-3: WebAPI puede emitir licencias

El proceso de emision de licencia es invocado desde HH. Si la WebAPI
nueva intermedia las llamadas del HH (ver L-019 sobre rol arquitectonico),
el endpoint `POST /api/v1/licencias` debe encapsular la logica de
generacion + insercion en `licencia_llave/licencia_item/Licencias`,
respetando el set de tablas activas segun cliente.

### IMP-4: queries cross-cliente para reporting

Toda query de "trazabilidad de stock por licencia" debe usar la columna
`lic_plate` de `i_nav_transacciones_out` como pivote, NO unirse contra
una tabla de licencias del cliente (porque el set de tablas varia).
La WebAPI debe ofrecer un endpoint `GET /api/v1/transacciones?licPlate=XXX`
que sirva uniformemente para los 3 clientes.

## Evidencia

- Q&A directa con Erik el 29-abr-2026.
- Query `lic_plate` cobertura: ver tabla arriba.
- Query `sys.tables LIKE %verificacion% OR %etiqueta% OR %licencia%`
  en las 3 BDs: ver listado arriba.

## Accion en el brain

1. Mover H-027 a `_processed/` con `decision=applied`.
2. Documentar la state machine de pedido con la **variante con
   verificacion** (BECOFARMA) y la **variante sin verificacion**
   (K7/BB) en `state-machine-pedido.md`. Tarea pendiente.
3. Investigar (Bloque B) ubicacion del capability flag: probable
   `i_nav_config_enc` con campo tipo bit. Pregunta para Erik.
4. Cuando arranquemos el ADR de la WebAPI, este L-018 alimenta el
   contrato de transaccion (incluir lic_plate) y los endpoints de
   licencias y verificacion.

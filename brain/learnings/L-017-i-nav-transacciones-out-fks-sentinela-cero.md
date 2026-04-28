---
protocolVersion: 1
id: L-017
title: i_nav_transacciones_out usa 0 (cero) como sentinela en FKs no aplicables, NO NULL — patron universal K7/BB/BECOFARMA
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-29T00:35:00Z
target:
  codename: tomwms-arquitectura
  environment: cross-cliente
relatedQuestions: [H-008, H-030]
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-specific-process-flow/becofarma-mapping.md
  - brain/outputs/ratificaciones/H06-H11-ratificacion.md
status: closed
priority: high
tags: [outbox, i_nav_transacciones_out, sentinela, FK, multi-tenant, H08-validacion]
---

## Que aprendimos

La tabla `i_nav_transacciones_out` (lo que en lenguaje arquitectonico
llamabamos "outbox") usa **0 (cero) como valor centinela** en las FKs
que **no aplican semanticamente al tipo de transaccion**, en lugar de
NULL. Este patron es **universal** en K7, BB y BECOFARMA (las 3 BDs
miradas).

### Schema relevante (todas las cols son `int NULL`):

```
idtransaccion   PK
idordencompra   FK -> orden de compra (aplica solo a INGRESO)
idrecepcionenc  FK -> recepcion       (aplica a INGRESO siempre, a SALIDA mixto)
idpedidoenc     FK -> pedido          (aplica solo a SALIDA)
iddespachoenc   FK -> despacho        (aplica solo a SALIDA)
tipo_transaccion ('SALIDA' | 'INGRESO')
```

### Patrones reales medidos (28-abr-2026):

**Tipo `SALIDA`**:

| Cliente | filas | idpedidoenc | iddespachoenc | idrecepcionenc | idordencompra |
|---|---|---|---|---|---|
| K7 | 19,799 | 100% real | 100% real | 61% real, 39% =0 | **100% =0** |
| BB | 422,427 | 100% real | 100% real | 96% real, 4% =0 | **100% =0** |
| BECOFARMA | 31,486 | 100% real | 100% real | 50% real, 50% =0 | **100% =0** |

**Tipo `INGRESO`**:

| Cliente | filas | idpedidoenc | iddespachoenc | idrecepcionenc | idordencompra |
|---|---|---|---|---|---|
| K7 | 4,394 | **100% =0** | **100% =0** | 100% real | 100% real |
| BB | 110,902 | **100% =0** | **100% =0** | 100% real | 100% real |
| BECOFARMA | 5,090 | **100% =0** | **100% =0** | 100% real | 100% real |

### Interpretacion correcta del patron:

- Cada fila lleva las **2 FKs que aplican al tipo de transaccion**, las
  demas se rellenan con `0` como sentinela.
- `0` NO es una FK valida (no apunta a una fila real). Tratar `=0` como
  "no aplica" / "NULL semantico".
- En SALIDA, `idrecepcionenc` puede ser `>0` cuando el sistema rastrea
  cual recepcion/lote alimento el picking. Cuando no se rastrea, queda
  `0`. Es un campo opcional **dentro del tipo SALIDA**.

### Consecuencias para H-008 y H-030:

**H-008** (outbox simplifica a 2 patrones efectivos: SALIDA y INGRESO):
**VALIDADO Y REFORZADO** con datos de 3 BDs. La afirmacion era correcta
incluso cuando la lectura ingenua decia "todas las FKs estan al 100%"
(porque ese 100% incluia los ceros sentinela).

**H-030** (BECOFARMA usa "FKs universales" distinto de K7/BB):
**INVALIDADO**. La observacion inicial confundio "FK no nula" con "FK
real". Una vez filtrando por `>0`, los 3 clientes muestran exactamente
el mismo patron H-008. **No hay diferencia entre BECOFARMA y K7/BB en
este aspecto**.

## Implicancias para la WebAPI .NET 10

### IMP-1: lectura del outbox debe filtrar por `>0`, no `IS NOT NULL`

Cualquier query que use el outbox para reconstruir el contexto de una
transaccion debe leer las FKs como:

```sql
CASE WHEN idpedidoenc > 0 THEN idpedidoenc END AS pedido_real
```

Y NO:

```sql
CASE WHEN idpedidoenc IS NOT NULL THEN idpedidoenc END AS pedido_real
-- INCORRECTO: incluye los 0 sentinela como si fueran FKs
```

### IMP-2: el modelo de la WebAPI puede simplificarse a 2 contratos

- `OutboundTransactionExit` (SALIDA): `pedidoEncId`, `despachoEncId`,
  `recepcionEncId?` (opcional para tracking de origen del stock).
- `OutboundTransactionEntry` (INGRESO): `recepcionEncId`,
  `ordenCompraId`.

Sin necesidad de discriminar por cliente — el patron es uniforme.

### IMP-3: si la WebAPI nueva escribe en `i_nav_transacciones_out`

Para mantener compatibilidad con SAPBOSync.exe / NavSync.exe / SAPSYNC*
existentes, **debe rellenar las FKs no aplicables con 0, no NULL**.
Si los consumidores legacy no toleran NULL en esas columnas (probable),
NULL romperia el procesamiento.

### IMP-4: tarea futura de saneamiento (OPCIONAL, no urgente)

Migrar el patron a NULLs reales tendria valor a futuro (mejor signal
del schema), pero implica:
- Cambiar todos los SP que escriben al outbox.
- Cambiar todas las interfaces consumidoras (.exe).
- Migrar datos historicos (`UPDATE i_nav_transacciones_out SET
  idordencompra = NULL WHERE idordencompra = 0` × cada FK × cada cliente).

No vale la pena hasta que la WebAPI nueva este corriendo y se migren
los clientes uno por uno.

## Evidencia

- Query ejecutada el 28-abr-2026 en las 3 BDs (K7, BB, BECOFARMA):
  ver `brain/agent-context/sql-evidencia/2026-04-28-outbox-fks-sentinela.md`
  (snapshot completo).
- Datos exactos: tabla arriba.
- 4 muestras de filas reales (2 SALIDA + 2 INGRESO) por cliente
  confirman el patron en datos individuales.

## Accion en el brain

1. Mover H-030 a `_processed/` con `decision=invalidated` apuntando a este L-017.
2. Actualizar la ratificacion de H-008 en `outputs/ratificaciones/H06-H11-ratificacion.md`
   con referencia a esta evidencia ampliada (3 BDs, no solo 1).
3. Renombrar referencias a "outbox" -> "i_nav_transacciones_out" en
   docs principales del brain (manteniendo "outbox" entre parentesis
   como alias arquitectonico).
4. Documentar en el ADR de la WebAPI .NET 10 las IMP-1, IMP-2 e IMP-3
   cuando arranquemos.

---
tipo: other
clientes: [killios]
---
# Indice: SPs de regularizacion VERI (BD)

> Origen: handoff `2026-05-19-codex-learning-bof-veri-movimientos-duplicados`
> commits `9073061` (scripts) + `a256cb7` (refinamiento UMBAS).
> Tres SPs creados y probados en BD local Codex contra picking 1465.
> **NO desplegados en KILLIOS_PRD** todavia.

## Mapa

| SP | Bug que ataca | Operacion |
|---|---|---|
| `dbo.usp_WMS_VERI_RegularizarDuplicadosExactos` | Patron 1: VERI repetidos con misma llave logica | DELETE de `rn > 1` |
| `dbo.usp_WMS_VERI_RegularizarCantidadUmbas` | Patron 2: VERI grabada en presentacion en vez de UMBAS | UPDATE `cantidad = cantidad * Factor` |
| `dbo.usp_WMS_VERI_PostCheck` | Validacion | SELECT de pendientes residuales |

Prefijo `usp_` deliberado: SQL Server da tratamiento especial a procedimientos
`sp_` (busqueda en master primero), `usp_` evita ese costo y posible
ambiguedad.

## Parametros comunes

| Parametro | Default | Rol |
|---|---|---|
| `@IdPickingEnc` | `NULL` | Picking objetivo. Si `NULL`, requiere `@ProcesarTodos = 1`. |
| `@ProcesarTodos` | `0` | Bandera de seguridad. Debe ser `1` explicito para alcance global. |
| `@EjecutarDelete` | `0` | Solo en `usp_WMS_VERI_RegularizarDuplicadosExactos`. `0` = solo audita. |
| `@EjecutarUpdate` | `0` | Solo en `usp_WMS_VERI_RegularizarCantidadUmbas`. `0` = solo audita. |
| `@ConfirmarCommit` | `0` | `0` = ejecuta dentro de TRAN y hace `ROLLBACK` (simulacion). `1` = `COMMIT`. |

## Tres modos de ejecucion

| Modo | Bandera write | Bandera commit | Efecto |
|---|---|---|---|
| Auditoria | `0` | (irrelevante) | No abre transaccion. Devuelve candidatos. |
| Simulacion | `1` | `0` | Abre TRAN, ejecuta DML, hace `ROLLBACK`. Devuelve bitacora propuesta. |
| Aplicacion | `1` | `1` | Abre TRAN, ejecuta DML, hace `COMMIT`. Devuelve bitacora aplicada. |

Bloqueo: si `@IdPickingEnc IS NULL AND @ProcesarTodos = 0` → `THROW 53001`
("Configuracion invalida: para procesar todos los pickings debe usar
@ProcesarTodos = 1.").

## Criterio conservador (Patron 2)

`usp_WMS_VERI_RegularizarCantidadUmbas` solo propone UPDATE cuando TODO se
cumple:

1. Es VERI **unico** para la llave logica (no es duplicado).
2. `IdPresentacion > 0` con `Factor > 1`.
3. `trans_picking_ubic` matchea **exactamente 1 linea** con
   `Cantidad_Verificada > 0`.
4. `trans_movimientos.cantidad == SUM(trans_picking_ubic.Cantidad_Verificada)`.
5. El `PIK` del mismo grupo confirma la cantidad esperada en UMBAS
   (`cantidad_PIK = X * Factor`).

Si cualquiera falla → no se propone correccion para ese movimiento. Pasa a
analisis funcional.

## Orden recomendado

```sql
-- 1. Auditar y depurar duplicados primero (Patron 1).
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465;                              -- auditoria

EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465,
    @EjecutarDelete = 1,
    @ConfirmarCommit = 0;                              -- simulacion

EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465,
    @EjecutarDelete = 1,
    @ConfirmarCommit = 1;                              -- aplicacion

-- 2. Validar post-delete.
EXEC dbo.usp_WMS_VERI_PostCheck @IdPickingEnc = 1465;

-- 3. Corregir cantidades UMBAS (Patron 2).
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465;                              -- auditoria

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465,
    @EjecutarUpdate = 1,
    @ConfirmarCommit = 0;                              -- simulacion

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465,
    @EjecutarUpdate = 1,
    @ConfirmarCommit = 1;                              -- aplicacion

-- 4. Validar final.
EXEC dbo.usp_WMS_VERI_PostCheck @IdPickingEnc = 1465;
-- Esperado: DUPLICADOS_EXACTOS_RESTANTES = 0, MISMATCH_PRESENTACION_RESTANTES = 0
```

## Resultados validados (BD local Codex, picking 1465)

| Etapa | DUPLICADOS_EXACTOS_RESTANTES | MISMATCH_PRESENTACION_RESTANTES |
|---|---:|---:|
| Antes | >0 (pares 288682/288706 y 288683/288707) | 5 |
| Post-Patron 1 | 0 | 5 |
| Post-Patron 2 | 0 | 0 |

Movimientos corregidos en Patron 2 (cantidad pasa de presentacion 72 a UMBAS 1728):

| IdMovimiento | Antes | Despues |
|---:|---:|---:|
| 288676 | 72 | 1728 |
| 288678 | 72 | 1728 |
| 288679 | 72 | 1728 |
| 288680 | 72 | 1728 |
| 288681 | 72 | 1728 |

## Bitacora

Cada SP devuelve resultsets con: `RunId` (uniqueidentifier), `FechaEjecucion`,
modo, movimientos propuestos/aplicados, movimiento canonico (caso Patron 1),
cantidad antes/despues (caso Patron 2).

Hoy la bitacora es resultset volatil. Codex sugiere crear tabla persistente
`dbo.audit_regularizacion_veri` si se aplica masivamente. **No implementado.**

## Criterios de no-intervencion (SPs los respetan)

- Movimientos `VERI` huerfanos sin relacion clara con `trans_picking_ubic`.
- Grupos con mas de un `VERI` cuando NO son duplicados exactos.
- Diferencias donde el `PIK` NO confirma cantidad UMBAS esperada.
- Lineas con `Cantidad_Verificada = 0`.
- Casos donde `Factor <= 1`.

Estos quedan para analisis funcional separado.

## Estado de despliegue

| Ambiente | Instalado | Validado | Aplicado |
|---|:-:|:-:|:-:|
| BD local Codex | si | si (picking 1465) | si |
| KILLIOS_PRD | **no** | no | no |
| Otros clientes | no | no | no |

Aplicacion productiva requiere:
1. Autorizacion explicita Erik.
2. Backup BD o tablas afectadas (`trans_movimientos` minimo).
3. Ejecucion en ventana controlada (idealmente fuera de horario operativo).
4. Aplicar primero a un picking validado.
5. Conservar resultsets de bitacora exportados a archivo.
6. `PostCheck` antes y despues.

## Archivos crudos

Los tres scripts T-SQL estan en:

- `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/SCRIPT_AUDITORIA_REGULARIZACION_VERI_DUPLICADOS.sql` (standalone, 548 lineas)
- `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/SCRIPT_AUDITORIA_REGULARIZACION_VERI_CANTIDAD_UMBAS.sql` (standalone, 451 lineas)
- `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/SP_REGULARIZACION_VERI.sql` (3 SPs instalables, 613 lineas)

## Referencias

- Patron BOF completo: `wms-brain/brain/code-changes/BOF/PATTERNS-PICKING-VERI.md`
- Guia operativa Codex: `wms-brain/brain/handoffs/.../GUIA_OPERATIVA_REGULARIZACION_VERI.md`
- Regla UMBAS: `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md`
- Patron hermano BYB: `wms-brain/brain/code-changes/BOF/PATTERNS-RESERVA-MI3-UMBAS.md`

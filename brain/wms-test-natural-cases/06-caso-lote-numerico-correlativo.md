# CASO 06 — Lote numérico correlativo (`control_ultimo_lote`)

## Trigger

Cliente y proveedor (el que tiene el WMS) **acordaron contractualmente** que el cliente recibirá lotes en orden correlativo. Si ya se le despachó el lote N, no se le puede despachar N-1 (regresivo prohibido). Solo N o N+1.

## Parámetros activos

| Parámetro | Tabla | Tipo | Rol |
|-----------|-------|------|-----|
| `control_ultimo_lote` | `cliente` | bit | gate ON/OFF |
| `Lote_Numerico` | `i_nav_barras_pallet` | float | correlativo asignado al pallet en recepción |
| `Lote_Numerico` | `trans_re_det_lote_num` | int | log de recepción con correlativo |
| `Lote_Numerico` | `trans_despacho_det_lote_num` | int | log de despacho — la regla se valida aquí |
| `Lote` | `trans_re_det_lote_num` | nvarchar | lote alfanumérico original (NAV/SAP) |

## Tablas involucradas

```
cliente                          ← gate: control_ultimo_lote
i_nav_barras_pallet              ← Lote_Numerico asignado en barras de pallet
trans_re_det_lote_num            ← log recepción (8 cols, FK a IdRecepcionEnc)
trans_despacho_det_lote_num      ← log despacho — validación
trans_oc_det_lote                ← orden compra con lote
i_nav_ped_compra_det_lote        ← interface NAV pedido compra
i_nav_ped_traslado_det_lote      ← interface NAV pedido traslado
```

## Pseudo-código del flujo

### Asignación al recibir
```python
def asignar_lote_numerico_recepcion(recepcion_det, producto_bodega):
    if cliente_destino.control_ultimo_lote:
        # Buscar último Lote_Numerico recibido para ese producto
        ultimo = max(
            select Lote_Numerico from trans_re_det_lote_num
            where IdProductoBodega = producto_bodega.IdProductoBodega
        )
        nuevo_correlativo = (ultimo or 0) + 1

        insert into trans_re_det_lote_num (
            IdRecepcionEnc=recepcion_det.IdRecepcionEnc,
            IdProductoBodega=producto_bodega.IdProductoBodega,
            Codigo=producto.codigo,
            Lote=recepcion_det.Lote,           # alfanumérico ERP
            Lote_Numerico=nuevo_correlativo,    # ← correlativo WMS
            FechaIngreso=now(),
            Cantidad=recepcion_det.cantidad
        )

        # También registrar en barras de pallet si el LP se imprime
        i_nav_barras_pallet.Lote_Numerico = nuevo_correlativo
```

### Validación al despachar (la regla N+1)
```python
def reservar_con_control_ultimo_lote(pedido_det, cliente):
    if not cliente.control_ultimo_lote:
        return reserva_normal(pedido_det)

    # 1. ¿Cuál es el último Lote_Numerico despachado a este cliente para este producto?
    ultimo_despachado = max(
        select td.Lote_Numerico
        from trans_despacho_det_lote_num td
        join trans_pe_enc pe on pe.IdPedidoEnc = td.IdPedidoEnc
        where pe.IdCliente = cliente.IdCliente
          and td.IdProductoBodega = pedido_det.IdProductoBodega
    ) or 0

    # 2. Filtrar candidatos: solo lotes con Lote_Numerico >= ultimo_despachado
    candidatos = select s.*
        from stock s
        join trans_re_det_lote_num rln on rln.Lote = s.Lote
                                       and rln.IdProductoBodega = s.IdProductoBodega
        where rln.Lote_Numerico >= ultimo_despachado

    if not candidatos:
        raise Exception(
            f"Cliente {cliente.codigo} tiene control_ultimo_lote. "
            f"Último despachado: {ultimo_despachado}. "
            f"No hay stock disponible con Lote_Numerico >= {ultimo_despachado}."
        )

    # 3. Aplicar rotación dentro del set válido
    return reservar(candidatos)

    # NOTA: la regla "N o N+1" exacta puede tener tolerancia de saltos
    # (ej: si N+2 está disponible y N+1 no, ¿se permite saltar?)
    # Q-LOTE-NUM-SALTO abierta
```

## Caso operativo real

**Anécdota Erik (2026-04-29)**:
> "Para algunas operaciones nos encontramos gestiones de lotes numéricos donde el proveedor (el que tenía el WMS) y sus clientes acordaban que manejarían un correlativo de lotes. Si al cliente se le despachaba el lote N, no podía despachar N-1, podía ser N o N+1. Este desarrollo está ligado, recuerdo, a un campo `mtipo lote_numerico` y su flujo. La funcionalidad quedó entretejida pero sigue viva."

### Por qué existe esta regla
- **Trazabilidad regulatoria**: industrias farmacéuticas, alimenticias, químicas. El cliente exige no recibir lotes "viejos" después de haber recibido los nuevos (compliance auditoría).
- **Acuerdo comercial**: el cliente paga premium por garantía de frescura monotónica.
- **FEFO no alcanza**: FEFO es por fecha de vencimiento. El lote correlativo es por fecha de **producción** (nuevos números = más nuevos físicamente).

### Diferencia con FEFO
| FEFO | Lote_Numerico correlativo |
|------|---------------------------|
| Despacha lo que vence primero | Despacha lo que llegó después |
| No requiere acuerdo cliente | Requiere acuerdo y `control_ultimo_lote=True` |
| Universal | Por cliente |
| Decide WMS unilateralmente | Validado contra histórico de despacho a ESE cliente |

## Variantes y combinaciones

### Combina con `IdUbicacionAbastecerCon` (Caso 05)
Sí. Filtra primero por ubicación restringida, después por correlativo.

### Combina con `despachar_lotes_completos` (caso especial)
Si el cliente exige lotes completos Y correlativo, el WMS solo despacha si encuentra un lote N o N+1 que cubra el pedido entero (sin parciales).

### Sin stock con correlativo válido
La función falla. **Q-LOTE-NUM-FALLBACK**: ¿se puede saltar a N+2 si N+1 no existe? ¿Política configurable?

## Q-* asociadas

- ✅ **Q-LOTE-NUM-EXISTE** — confirmado: `cliente.control_ultimo_lote` + tabla `trans_re_det_lote_num` + `trans_despacho_det_lote_num`
- 🟡 **Q-LOTE-NUM-SALTO** — ¿permite N+2 si N+1 no está disponible?
- 🟡 **Q-LOTE-NUM-CLIENTES-ACTIVOS** — qué clientes hoy lo usan
- 🟡 **Q-LOTE-NUM-MTIPO** — Erik mencionó "campo `mtipo lote_numerico` y su flujo". ¿Existe este campo o se refiere a `Lote_Numerico` directo?

## Validación SQL

```sql
-- Clientes con control_ultimo_lote activo
SELECT c.IdCliente, c.codigo, c.nombre_comercial, c.control_ultimo_lote
FROM IMS4MB_MERCOPAN_PRD.dbo.cliente c
WHERE c.control_ultimo_lote = 1;

-- Lotes numéricos asignados (recepción)
SELECT TOP 50 IdProductoBodega, Codigo, Lote, Lote_Numerico, FechaIngreso, Cantidad
FROM IMS4MB_MERCOPAN_PRD.dbo.trans_re_det_lote_num
ORDER BY FechaIngreso DESC;

-- Lotes numéricos despachados (validación regla N+1)
SELECT TOP 50 IdPedidoEnc, IdProductoBodega, Lote_Numerico
FROM IMS4MB_MERCOPAN_PRD.dbo.trans_despacho_det_lote_num
ORDER BY IdPedidoEnc DESC;

-- Detectar regresiones (despachos donde se mandó N-1 después de N) — auditoría
WITH despachos_orden AS (
    SELECT pe.IdCliente, td.IdProductoBodega, td.Lote_Numerico,
           pe.fec_agr,
           LAG(td.Lote_Numerico) OVER (
               PARTITION BY pe.IdCliente, td.IdProductoBodega
               ORDER BY pe.fec_agr
           ) as lote_anterior
    FROM IMS4MB_MERCOPAN_PRD.dbo.trans_despacho_det_lote_num td
    JOIN IMS4MB_MERCOPAN_PRD.dbo.trans_pe_enc pe ON pe.IdPedidoEnc = td.IdPedidoEnc
)
SELECT * FROM despachos_orden
WHERE Lote_Numerico < lote_anterior;
```

## Tablas relacionadas (interface NAV/SAP)

```
i_nav_ped_compra_det_lote      ← NAV manda lote en orden compra
i_nav_ped_traslado_det_lote    ← NAV manda lote en traslado
trans_oc_det_lote              ← orden compra interna con lote
```

→ El WMS importa el `Lote` alfanumérico del ERP y le asigna su propio `Lote_Numerico` correlativo. Doble identificación: ERP usa lote textual, WMS usa correlativo numérico para la regla N+1.

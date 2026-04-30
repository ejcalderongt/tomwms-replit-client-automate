# 06 — Glosario de tipos de tarea y motivos

## Tipos de tarea (`IdTipoTarea` en `trans_movimientos`)

Verificar siempre por cliente con:
```sql
SELECT IdTipoTarea, COUNT(*) n FROM trans_movimientos GROUP BY IdTipoTarea ORDER BY 1;
```
Y mapear contra catálogo `tipo_tarea` si existe (varía por versión).

| Id  | Sigla         | Significado                             | Notas                                    |
|----:|:--------------|:----------------------------------------|:-----------------------------------------|
| 1   | RECE          | Recepción                               | Entrada de mercadería                    |
| 2   | UBIC          | Ubicación post-recepción                | Movimiento del staging a almacenaje      |
| 3   | CEST          | Cambio de estado (BUEN ↔ MAL)           | No mueve cantidad, cambia atributo       |
| 4   | (varía)       |                                         | Verificar por cliente                    |
| 5   | DESP          | Despacho / salida                       |                                          |
| 6   | INVE          | Inventario / conteo formal              | Genera ajustes con `ajuste_por_inventario=1` |
| 7   | (varía)       |                                         | Verificar por cliente                    |
| 8   | PIK           | Picking                                 | Movimiento de almacenaje a staging       |
| 11  | VERI          | Verificación pre-despacho               |                                          |
| 17  | AJCANTN       | Ajuste de cantidad                      | Manual desde BOF, NO se genera automático para dañados |
| 20  | EXPLOSION     | Explosión de bulto                      | Convierte un LP en múltiples UM unitarias |
| 25  | REEMP_BE_PICK | Reemplazo en picking                    |                                          |

**Tipos no listados:** verificar con catálogo del cliente. Algunos clientes definen IDs custom.

---

## Tipos de ajuste (`idtipoajuste` en `trans_ajuste_det`)

| Id | Nombre              | Comportamiento                            |
|---:|:--------------------|:------------------------------------------|
| 3  | Ajuste Positivo     | `cantidad_nueva > cantidad_original`      |
| 5  | Ajuste Negativo     | `cantidad_nueva < cantidad_original`      |
| 6  | Ajuste de Estado    | No cambia cantidad, cambia atributo (introducido dic-2024) |

Verificar nombres exactos en `tipo_ajuste` si existe la tabla, o inferir por signo del delta.

---

## Motivos de ajuste (`idmotivoajuste` en `trans_ajuste_det`)

Mapeados desde `ajuste_motivo`. Los más comunes vistos en Killios:

| Id | Nombre               | Lectura habitual                                           |
|---:|:---------------------|:-----------------------------------------------------------|
| 1  | Error en digitación  | Recepción/despacho mal cargado, se corrige a posteriori    |
| 2  | Falla de sistema     | El WMS perdió/duplicó stock, el equipo lo arregla manual   |
| 7  | Ajuste contra físico | Inventario físico encontró diferencia                      |
| 8  | Error recepción      | Llegó distinto a lo declarado                              |

**Otros vistos en Killios:** "Despacho Licencia" (105 ajustes negativos en CP-013, indica "se despachó pero el sistema no descontó" — síntoma del bug crónico).

Cada cliente puede tener motivos custom. Cargar el listado completo en su yml.

---

## Estados de reserva (`stock_res.estado`)

| Valor       | Significado                                |
|:------------|:-------------------------------------------|
| `UNCOMMITED`| Reservada pero no asignada a operador      |
| `PICKEADO`  | Operador la pickeó (sacó de la ubicación)  |
| `VERIFICADO`| Verificada pre-despacho                    |
| `''` (vacío)| Generalmente reservas tipo ubicación (UBI) |

---

## Indicadores de reserva (`stock_res.Indicador`)

| Valor | Significado                                        |
|:------|:---------------------------------------------------|
| `PED` | Reserva contra un pedido (despacho)                |
| `UBI` | Reserva por movimiento de ubicación interno        |

---

## Tipos de ubicación (`ubicacion.IdTipoUbicacion`)

Varía por cliente. Comunes:
| Id | Nombre típico    |
|---:|:-----------------|
| 1  | Picking          |
| 2  | Almacenaje       |
| 3  | Staging          |
| 4  | Merma / daños    |
| 5  | Recepción        |

Verificar por cliente con:
```sql
SELECT IdTipoUbicacion, COUNT(*) FROM ubicacion GROUP BY IdTipoUbicacion;
```

---

## Roles de usuario (`usuario.IdRol`)

Varía por cliente. Cargar mapeo en yml. Killios visto:
- 18 = Auditoría (genera escrituras automáticas en stock_hist)

---

## Convención

Cuando un cliente tiene un mapeo distinto, **NO sobrescribir este doc**: documentar la diferencia en `client-index/<cliente>.yml` sección `mapeos_custom`.

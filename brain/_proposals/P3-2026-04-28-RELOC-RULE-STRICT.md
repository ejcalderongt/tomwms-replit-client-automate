---
id: P3-2026-04-28-RELOC-RULE-STRICT
tipo: proposal
estado: vigente
titulo: "P3-2026-04-28-RELOC-RULE-STRICT - Cambio de ubicacion 100% restrictivo en BOF y HH"
tags: [proposal]
---

# P3-2026-04-28-RELOC-RULE-STRICT - Cambio de ubicacion 100% restrictivo en BOF y HH

> Generado por agente brain (sesion replit) el 28 abril 2026 tras consulta de Carol (Q-015).

## Origen

Consulta de Carol (CKFK/KKKL) reenviada por Erik el 28-abr-2026:

> "Al realizar el cambio de ubicacion de un producto en BOF y en HH que no se pueda mover un producto a una posicion donde no le corresponde, siempre se tiene que cumplir la regla de ubicacion, el mensaje debe ser 100% restrictivo."

## Hallazgo de proceso (validado en wms-db-brain)

El modelo de TOMWMS ya soporta esta regla en su totalidad. No hace falta inventar nada nuevo - hay que **enforce** lo que ya existe.

### Modelo de datos disponible

| Componente | Tabla/Vista | Rol |
|------------|-------------|-----|
| Encabezado de regla | `regla_ubic_enc` | Define una regla por bodega/empresa |
| Asignacion ubicacion-regla | `regla_ubicacion` | PK compuesta `IdUbicacion + IdReglaUbicacionEnc` |
| Detalle por indice rotacion | `regla_ubic_det_ir` | FK a `indice_rotacion` |
| Detalle por tipo rotacion | `regla_ubic_det_tr` | FK a `tipo_rotacion` |
| Detalle por estado producto | `regla_ubic_det_pe` | FK a `producto_estado` |
| Detalle por presentacion | `regla_ubic_det_pp` | FK a `producto_presentacion` |
| Detalle por propietario | `regla_ubic_det_prop` | FK a `propietario` |
| Detalle por tipo producto | `regla_ubic_det_tp` | FK a `producto_tipo` |
| Priorizacion | `regla_ubic_prio_*` (4 tablas) | Define prioridad cuando varias reglas aplican |
| Seleccion | `regla_ubic_sel_*` (4 tablas) | Filtros adicionales |
| Mensajes | `mensaje_regla` | Textos al usuario |
| Lookup | `ubicaciones_por_regla` | Vista materializada |
| Vista del flujo | `VW_Stock_CambioUbic` (73 cols, modify 2024-02-01) | Ya proyecta `IdUbicacion_anterior + IdUbicacion + IdIndiceRotacion` |

### Datos comparables (no afinidad-de-datos, son metadatos)

- `dbo.indice_rotacion`: 5 filas. Columna `IndicePrioridad` (int) - numericamente comparable.
- `dbo.producto`: tiene `IdIndiceRotacion` (col 11) y `IdTipoRotacion` (col 9).
- `dbo.bodega_ubicacion` y `dbo.estructura_ubicacion`: tienen `IdIndiceRotacion` y `IdTipoRotacion`.

## Regla formalizada

### R0 - regla principal (bloqueante)

```
PERMITIR cambio_ubicacion(producto P, ubicacion_origen Uo, ubicacion_destino Ud)
SI Y SOLO SI:
  EXISTE r EN regla_ubicacion DONDE r.IdUbicacion = Ud.IdUbicacion
  Y P CUMPLE TODOS los detalles de r.IdReglaUbicacionEnc
    (regla_ubic_det_ir, _tr, _pe, _pp, _prop, _tp activos)
SINO:
  RECHAZAR con mensaje de mensaje_regla.
  Mensaje 100% restrictivo - sin override en BOF ni en HH.
```

### E1 - excepcion de downgrade de rotacion (PENDIENTE de aclaracion)

```
PERMITIR adicionalmente cambio_ubicacion SI:
  P.IdIndiceRotacion -> indice_rotacion[].IndicePrioridad = pP
  Ud.IdIndiceRotacion -> indice_rotacion[].IndicePrioridad = pU
  Y pP <relacion> pU  // <- Carol debe confirmar la direccion del operador
```

**Ambiguedad a resolver con Carol**: ¿`IndicePrioridad` numericamente mayor significa rotacion mas alta (mas rapida) o mas baja (mas lenta)? La regla "puedo colocar producto de MAYOR indice en ubicacion de MENOR indice" se traduce a:
- Si "mayor indice" = mas rapida: `pP < pU` (poner producto rapido en ubicacion lenta).
- Si "mayor indice" = mas lenta: `pP > pU` (poner producto lento en ubicacion rapida).

### E2 - excepcion mismo producto (PENDIENTE de aclaracion)

```
PERMITIR adicionalmente cambio_ubicacion SI:
  Stock(Uo).IdProducto = Stock(Ud).IdProducto
  Y <atributos A,B,C,D coinciden segun lectura de Carol>
```

**Ambiguedad a resolver con Carol**: ¿"A y B iguales C y D iguales" se refiere a coordenadas de ubicacion, atributos del lote, presentaciones del producto, o algo mas? Tres lecturas en `wms-brain-client/questions/Q-015/question.md`.

## Pregunta abierta para investigar (otro ciclo)

¿El SP/codigo de cambio de ubicacion ACTUAL (BOF.NET y HH Android) realmente CONSULTA `regla_ubic_*` para enforcement, o el bloqueo es solo en UI? Si es solo UI, hay un gap server-side que la nueva WebAPI .NET 8 debe cerrar de origen.

## Decision provisional

`accepted_modelo_existe_falta_confirmar_enforcement_y_ambiguedades_e1_e2`

Acciones inmediatas:
1. Esperar aclaracion de Carol sobre E1 (sentido del operador) y E2 (significado de "A y B iguales C y D iguales").
2. Verificar (proximo ciclo via SQL live o code review): si los SPs/codigo actual hacen JOIN contra `regla_ubic_*` antes de aceptar el cambio.
3. Si Erik aprueba: emitir ADR-013-RELOC-RULE-STRICT con la regla formal en la nueva WebAPI - **sin override, sin permiso especial, mensaje siempre del catalogo `mensaje_regla`**.

## Ratificacion pendiente de

Erik Calderon (PrograX24) + aclaracion de Carol.

## Cross-references

- Inbox event: `brain/_inbox/20260428-2000-H12-regla-cambio-ubicacion-modelo-existe-en-tomwms.json`
- Card cliente: `wms-brain-client/questions/Q-015/question.md`
- Documento explicativo: `brain/wms-specific-process-flow/consulta-carol-reubicacion.md`
- Tags: `validated-via-wms-db-brain`, `afinidad-procesos`, `regla-ubicacion`, `cambio-ubicacion`, `carol-test`

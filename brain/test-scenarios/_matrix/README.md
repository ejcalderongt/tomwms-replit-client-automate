---
id: README
tipo: test-scenario
estado: vigente
titulo: Matriz cliente × configuración × escenario
tags: [test-scenario]
---

# Matriz cliente × configuración × escenario

Este directorio describe **el modelo formal** que usamos para responder dos preguntas:

1. _"El escenario `RES-NNN`, ¿aplica para el cliente X?"_
2. _"El cliente X y el cliente Y, ¿comparten la misma forma de reservar stock?"_

## El problema

El motor TOMWMS tiene ~80 banderas de configuración por bodega (`clsLnConfiguracion_qa`). Cada cliente tiene un perfil distinto:

| Cliente | `Rechazar_Pedido_Si_Esta_Incompleto` | `Permite_Explosion_En_Picking` | `Reservar_Desde_Picking_Antes_Que_Almacenamiento` | ... |
|---|---|---|---|---|
| IDEALSA | `true` | `true` | `false` | ... |
| KILLIOS | `true` | `true` | `false` | ... |
| LA CUMBRE | `false` | `true` | `false` | ... |
| BYB | `true` | `true` | `true` | ... |

Esto significa que un escenario como **CASO 8 / `RES-012`** (que valida el rechazo por pedido incompleto) **no tiene sentido ejecutarlo en un cliente que no tiene esa bandera activa** — daría falso negativo.

## La solución: declarar requisitos en cada escenario

Cada YAML de escenario tiene una sección `requires_config:` con la forma:

```yaml
id: RES-012
requires_config:
  - flag: Rechazar_Pedido_Si_Esta_Incompleto
    value: true
  - flag: Permite_Explosion_En_Picking
    value: false   # caso 8 NO usa explosión, debe estar desactivada o N/A
```

Y cada cliente conocido tiene una ficha en `_matrix/clients/<slug>.yaml`:

```yaml
slug: idealsa
display_name: IDEALSA
flags:
  Rechazar_Pedido_Si_Esta_Incompleto: true
  Permite_Explosion_En_Picking: true
  Reservar_Desde_Picking_Antes_Que_Almacenamiento: false
```

El brain entonces calcula una **matriz de aplicabilidad**:

```
                IDEALSA  KILLIOS  LA CUMBRE  BYB
RES-001 (FEFO)    OK       OK        OK       OK
RES-012 (rechazo) OK       OK        N/A      OK
RES-022 (BYB)     N/A      N/A       N/A      OK
```

## Cómo se llena `clients/<slug>.yaml`

Hay 3 caminos, en orden de preferencia:

1. **Automático**: ejecutar `wmsa learn-config <slug>` apuntando a una BD del cliente. El comando lee `clsLnConfiguracion_qa.Obtiene_Configuracion(IdBodega)` y dump al YAML.
2. **Semi-automático**: copiar la salida de `SELECT * FROM Configuracion_QA WHERE IdBodega = ?` directamente en la sección `flags:`.
3. **Manual**: capturar lo que el referente del cliente confirme.

**Mientras no hayamos aprendido un cliente**, su YAML va con `learned: false` y la matriz lo marca como `unknown` en vez de `OK` / `N/A`.

## Cómo se actualiza la matriz

`compute-matrix.cjs` (en este directorio) lee:

- todos los `RES-*.yaml` bajo `brain/test-scenarios/**/`
- todos los `clients/*.yaml` aquí
- y emite `compatibility.md` actualizado.

No hay nada que persistir en BD: la matriz es derivada y se vuelve a calcular en cada commit que toque escenarios o clientes.

## Reglas de coherencia

1. **Cliente sin flag declarado** ≠ flag en `false`. Si un escenario requiere `flag X = true` y el cliente nunca declaró el flag X, la celda va como `unknown`, no como `N/A`. Esto fuerza a aprender la config del cliente antes de afirmar que el escenario no aplica.
2. **Cliente con `learned: false`** todas sus celdas son `unknown`.
3. **Escenario sin `requires_config:`** se interpreta como _"aplica para todos"_ (caso de escenarios universales tipo FEFO básico).
4. **Conflicto en requisitos** (un mismo flag pedido en dos valores distintos en el mismo escenario): el escenario es inválido y la matriz lo destaca con `INVALID`.

## Archivos en este directorio

- `README.md` (este archivo) — el contrato
- `clients/idealsa.yaml`, `clients/killios.yaml`, `clients/byb.yaml`, `clients/la-cumbre.yaml` — placeholders, todos `learned: false` hasta correr `wmsa learn-config`
- `compatibility.md` — matriz pre-calculada con lo poco que sabemos hoy
- `compute-matrix.cjs` — script Node que regenera `compatibility.md`

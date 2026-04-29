# L-029 — FIX: bug historico de implosion sin validar estado/ubicacion → fix en `dev_2028_Cumbre`

> Etiqueta: `L-029_FIX_IMPLOSION-CUMBRE-VALIDACION_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-CUMBRE-RAMA-DEDICADA

## Hallazgo

El proceso de implosion legacy del WMS (presente en `dev_2023_estable`
y heredado a `dev_2028_merge`) tiene un bug de validacion: **NO valida
ni el estado del producto ni la ubicacion al implosionar**, lo que
permite que la misma licencia (LP) quede registrada en distintas
ubicaciones y/o con distintos estados simultaneamente.

Cita literal Carolina:

> "En esa rama [dev_2028_Cumbre] se esta trabajando una funcionalidad
> de La Cumbre para el cambio de ubicacion, ya que en el proceso de
> implosion nos pasa que no se validaba el estado del producto, ni su
> ubicacion y aun asi permitia implosionar, dejando la misma licencia
> en diferentes ubicaciones y con diferentes estados."

## Diagnostico tecnico

### Patron del bug

```
Estado pre-implosion:
  LP "ABC123" + ubicacion U1 + estado "Buen Estado" + cantidad 100
  LP "XYZ789" + ubicacion U2 + estado "Daniado"     + cantidad 50

Operador implosiona "XYZ789" sobre "ABC123":
  → WMS NO valida que U1 == U2
  → WMS NO valida que ambos LPs tengan el mismo IdProductoEstado

Estado post-implosion (con bug):
  LP "ABC123" + ubicacion U1 + estado "Buen Estado" + cantidad 150
  LP "ABC123" + ubicacion U2 + estado "Daniado"     + cantidad 0  (residual)
  o peor:
  LP "ABC123" + ubicacion U2 + estado "Buen Estado" + cantidad 50  (cambio silencioso)
```

### Consecuencias

1. **Inventario fisico vs logico desfasado**. Lo que el sistema dice no
   coincide con donde esta fisicamente la mercaderia.
2. **Estado del producto se pierde silenciosamente**. Producto
   "Daniado" termina contandose como "Buen Estado" y se vende.
3. **Picking / reservas inconsistentes**. Si la regla de negocio
   reserva por estado, hay inconsistencias con el motor de reserva
   (ver Wave 8 + Wave 9).
4. **stock_jornada cierra con desfase no detectable**. Las
   reconciliaciones diarias en MAMPA + CEALSA pueden haber estado
   absorbiendo este bug como ruido.

## Por que se descubrio en CUMBRE

CUMBRE es el cliente que pidio la funcionalidad de **cambio de
ubicacion mejorado**. Al revisar el flujo de cambio de ubicacion +
implosion como caso conjunto, salio el bug de validacion. Por eso el
fix vive en `dev_2028_Cumbre`.

Hipotesis: el bug existia desde 2020+ pero la mayoria de los clientes
no lo gatillaba porque:
- BECO / K7 / BYB no usan implosion automatica (heat-map Wave 4 lo
  confirma).
- MAMPA usa cambio de ubicacion pero con LPs muy disciplinados (poca
  ocasion de duplicar entre ubicaciones).
- CEALSA tiene LPs por propietario, lo que reduce probabilidad de
  colision.
- MERHONSA tiene `implosion_automatica=True` (Wave 7) → es candidato a
  reproducir el bug. Q-* relevante.

## El fix

Hipotesis del fix (a confirmar con Erik o leyendo
`dev_2028_Cumbre/TOMIMSV4/Transacciones/Implosion/frmImplosion.vb`):

```
Pre-implosion validation (nuevo en dev_2028_Cumbre):
  IF lp_origen.IdUbicacion != lp_destino.IdUbicacion THEN
    RAISE error "no se puede implosionar entre ubicaciones distintas"
    o disparar primero un cambio_ubicacion(lp_origen, lp_destino.IdUbicacion)

  IF lp_origen.IdProductoEstado != lp_destino.IdProductoEstado THEN
    RAISE error "estados de producto distintos no se pueden mezclar"
    o pedir confirmacion explicita del operador

Post-implosion:
  Asegurar atomicidad transaccional (Wave 10 L-026 IDENTITY context)
```

## Pregunta critica abierta: ¿este fix se propaga a todos los clientes?

`dev_2028_Cumbre` es una rama dedicada. **Si el fix se queda solo en
Cumbre**, los demas clientes 2028 (MAMPA en prod, BYB+IDEALSA+INELAC en
QAS) siguen con el bug.

Hipotesis razonable: cuando Cumbre se cierre y mergee a
`dev_2028_merge`, el fix llega a todos los clientes 2028 nuevos. Pero
**MAMPA YA esta en prod con dev_2028_merge sin el fix** → tiene que
recibirlo en algun update post-Cumbre.

→ Q-IMPLOSION-CUMBRE-FIX-PROPAGAR (cuestionario bloque 14).

## Implicaciones

### 1. Auditoria post-fix recomendada
Cuando el fix llegue a un cliente, ejecutar SQL diagnostico:
```sql
-- LPs con duplicado por ubicacion/estado
SELECT lic_plate, COUNT(DISTINCT IdUbicacion), COUNT(DISTINCT IdProductoEstado)
FROM stock
WHERE Cantidad > 0
GROUP BY lic_plate
HAVING COUNT(DISTINCT IdUbicacion) > 1 OR COUNT(DISTINCT IdProductoEstado) > 1;
```
El conteo > 0 indica casos historicos heredados que el fix NO va a
limpiar retroactivamente. Hay que decidir politica: limpiar
manualmente, dejar como es, mover a tabla `lp_corruptos_pre_fix` para
auditoria.

### 2. Tests de no-regresion
Cuando el merge de `dev_2028_Cumbre` a `dev_2028_merge` se haga, los
tests de implosion deben validar las dos invariantes (ubicacion y
estado). Sugerencia: crear `wms-test-natural-cases/08-caso-implosion-validacion.md`.

### 3. Conexion con Wave 7
El L-029 explica por que MERHONSA tiene la paradoja LP (Wave 7):
`implosion_automatica=True` + `genera_lp=False` puede haber
generado un escenario donde implosiona LPs heredados del ERP sin
validar estado/ubicacion → MERHONSA es probable candidato a tener
LPs corruptos del bug.

## Cierra Q-*

- `Q-CUMBRE-RAMA-DEDICADA` (Bloque 1, media) — RESUELTA. La rama es por
  fix de implosion + cambio de ubicacion.

## Q-* nuevas derivadas

- Q-IMPLOSION-CUMBRE-FIX-PROPAGAR (Q86 cuestionario bloque 14)
- Q-IMPLOSION-AUDITORIA-MAMPA: ¿hay LPs corruptos historicos en MAMPA
  por este bug?
- Q-IMPLOSION-AUDITORIA-MERHONSA: idem para MERHONSA (mas critica
  porque tiene implosion_automatica activo).
- Q-CUMBRE-CHANGEUBIC-FUNCIONALIDAD: ¿que mejora exacta de cambio de
  ubicacion pidio Cumbre, ademas del fix de validacion?

## Naked-erik prevista

Este hallazgo amerita entry en `naked-erik-anatomy/` posterior al fix
(el "moment cero" donde un fix de Cumbre mejora la integridad del
inventario para todos).

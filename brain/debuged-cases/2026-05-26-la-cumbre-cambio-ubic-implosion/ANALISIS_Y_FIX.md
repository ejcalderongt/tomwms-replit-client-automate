# La Cumbre QA - Cambio de Ubicacion + Implosion automatica

Fecha: 2026-05-26  
Ambiente: `TOMWMS_LA_CUMBRE_QA` (`26.2.183.105\SQLEXPRESS`)  
Caso: producto aparece en destino con licencia origen y no con licencia destino esperada.

## Hallazgos de datos (QA)

- En `bodega` (IdBodega=89):  
  - `ubic_implosion_auto = 1`  
  - `cambio_ubicacion_auto = 1`
- Caso observado en stock:
  - `IdStock=747070`, `lic_plate=QW000005636`, `IdUbicacion=113`, `IdUbicacion_anterior=112`.
- En `trans_movimientos` se observan movimientos de ubicacion (112->113 / 113->112) sin consolidacion de licencia hacia la licencia destino esperada.

## Causa tecnica

El flujo dirigido de HH (`Actualizar_Trans_Ubic_HH_Det -> Guardar_Detalle`) aplicaba movimiento directo con:

- `clsLnTrans_movimientos.Aplicar(...)`
- `clsLnTrans_movimientos.Insertar(...)`

Ese camino no forzaba el pipeline unificado que ya contiene:

1. cambio de estado (si aplica),  
2. implosion por licencia destino (si aplica),  
3. cambio de ubicacion.

Por eso, en escenarios dirigidos, se podia mover ubicacion sin implosionar licencia aunque `ubic_implosion_auto=1`.

## Fix aplicado (tag)

Archivo:

- `TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb`

Cambio:

- En `Guardar_Detalle`, cuando:
  - `IdTipoTarea = 2` (cambio de ubicacion),
  - `ubic_implosion_auto = 1`,
  - la ubicacion destino trae `LicenciaDestino`,
  - y `LicenciaDestino <> licencia origen`,
  se enruta por `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack_Interno(...)` dentro de la misma transaccion.

Tag en codigo:

- `#EJC20260526`

Adicional:

- Se agrego helper privado:
  - `Get_Parametro_Ubic_Implosion_Auto(...)`

## Efecto esperado post-fix

En flujo dirigido de cambio de ubicacion, si destino define licencia distinta y aplica parametro:

- no se debe quedar licencia origen en el stock final movido;
- debe ejecutarse implosion y luego movimiento de ubicacion;
- se mantiene la transaccion atomica para evitar estados intermedios inconsistentes.

## Validacion sugerida

1. Repetir escenario de cambio dirigido con destino que define licencia fija.  
2. Verificar en `stock` que la licencia final corresponda a destino.  
3. Verificar `trans_movimientos` para ver secuencia consistente (PACK/implosion + UBIC).  
4. Confirmar en HH consulta de existencias por ubicacion destino.


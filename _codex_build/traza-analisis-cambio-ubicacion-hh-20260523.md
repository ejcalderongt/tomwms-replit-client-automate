# Traza de analisis: Cambio de ubicacion HH (2026-05-23)

## Objetivo
Levantar traza fina HH <-> WSHHRN para optimizar roundtrips, fluidez de flujo y cobertura de casos de negocio por parametrizacion de bodega, sin romper contratos existentes.

## Fuentes analizadas
- HH:
  - `frm_tareas_cambio_ubicacion.java`
  - `frm_detalle_cambio_ubicacion.java`
  - `frm_cambio_ubicacion_dirigida.java`
  - `frm_cambio_ubicacion_ciega.java`
- WS:
  - `WSHHRN/TOMHHWS.asmx.vb`

## Flujo funcional actual (resumen)
1. **Tareas dirigidas**
   - HH consulta tareas con `Get_All_Cambio_Ubic_By_IdBodega_And_IdOperador`.
2. **Detalle de tarea dirigida**
   - HH consulta detalle con `Get_All_By_IdTransUbicEnc_And_IdOperador`.
3. **Cambio dirigido**
   - Validacion origen/destino (2 llamadas de ubicacion).
   - Validaciones de pallet/posiciones.
   - Persistencia con `Actualizar_Trans_Ubic_HH_Det`.
4. **Cambio no dirigido (ciega)**
   - Ubicacion default recepcion.
   - Producto por codigo.
   - Stock por ubicacion y/o licencia.
   - Validaciones de destino (regla, rack, mismo producto en posicion, etc. segun flags).
   - Aplicacion de cambio (simple o licencia completa).
5. **Variantes de negocio**
   - Inferir origen por licencia.
   - LP mixto/licencia completa.
   - Caja master/no estandar.
   - Destino sugerido ML / sugerido por licencia.

## Casos de negocio observados por parametro
- `inferir_origen_en_cambio_ubic=true`:
  - Exige correlacion LP + producto para resolver origen.
- `Control_Pallet_Mixto=true`:
  - Puede activar ruta de licencia completa con multiples productos hijos.
- `cambio_ubic_restrictivo=true`:
  - Valida regla de ubicacion antes de permitir aplicar.
- `permitir_cambio_ubic_indice_menor=true`:
  - Agrega control de indice/rotacion.
- `requerir_mismo_producto_posiciones=true`:
  - Agrega validacion de homogeneidad en posicion destino.
- `Restringir_Areas_SAP=true`:
  - Bloquea cambios entre areas distintas.

## Trazado de roundtrips (estimado)
- **Dirigida basica**: ~5 llamadas WS por movimiento.
- **No dirigida simple**: ~6-10 llamadas WS por movimiento.
- **No dirigida LP mixto/licencia completa**: ~8-14 llamadas WS por movimiento.

## Hallazgos principales
1. **Secuencialidad alta**
   - Las validaciones de destino y negocio se encadenan en multiples llamadas, acumulando latencia por cada Enter.
2. **Superficie WS redundante**
   - Existen dos rutas de sugerencia de ubicacion (`ml_get_ubicacion_sugerida_JSON` y `Get_Ubicacion_Sugerida_JSON`) con comportamiento cercano.
3. **Mezcla de parseo**
   - Convive parseo XML heredado (`xobj`) y parseo JSON (`Gson/JSONObject`) dentro del mismo flujo.
4. **Reconsultas evitables**
   - Faltan caches de sesion por tarea/LP para datos repetidos (ubicacion validada, stock LP, estados por propietario).
5. **Costo en licencia completa**
   - En LP mixto el ciclo de validaciones + aplicacion + ajuste de reservadas multiplica roundtrips.

## Riesgos de regresion si se optimiza sin control
- Romper reglas por bodega (restrictivo, indice, mismo producto, SAP areas).
- Alterar semantica de cambio de estado vs cambio de ubicacion (`modo_cambio`).
- Cambiar el comportamiento de licencia completa y mover hijos incorrectos.

## Propuesta de optimizacion por fases
### Fase 1 (segura, HH)
- Cache de sesion (TTL corto) para:
  - ubicaciones validadas por codigo,
  - estados por propietario,
  - resultados de stock LP en la misma pantalla.
- Guardias de reentrada y bloqueo UI unificado por paso.
- Telemetria de tiempos por callback para baseline real por cliente.

### Fase 2 (WS, alto impacto)
- Endpoint consolidado de **prevalidacion + aplicacion**:
  - recibe contexto completo (modo, producto, origen, destino, flags),
  - evalua regla/rack/posicion,
  - retorna accion final lista para aplicar o mensaje de bloqueo.
- Endpoint unico de sugerencia destino.

### Fase 3 (deuda tecnica)
- Unificacion de contratos JSON en todo cambio ubicacion.
- Reduccion gradual de rutas XML legacy en este modulo.

## Artefactos generados
- Contexto operativo YAML:
  - `C:/Users/yejc2/source/repos/TOMWMS/codex-context-cambio-ubicacion-hh.yml`
- Mapa auxiliar de metodos:
  - `C:/Users/yejc2/source/repos/TOMWMS/_codex_build/cambio_ubic_ws_methods.txt`
  - `C:/Users/yejc2/source/repos/TOMWMS/_codex_build/cambio_ubic_ws_decl_lines.txt`
  - `C:/Users/yejc2/source/repos/TOMWMS/_codex_build/cambio_ubic_ws_trace_map.csv`

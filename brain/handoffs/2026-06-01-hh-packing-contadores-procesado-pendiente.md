# Handoff: HH packing contadores PENDIENTE/PROCESADO
Fecha: 2026-06-01
Cliente: K7 / La Cumbre escenario de referencia
Repo: TOMHH2025 (`dev_2028_merge`)
Archivo tocado: `app/src/main/java/com/dts/tom/Transacciones/Packing/frm_preparacion_packing.java`

## Síntoma observado
- Caso parcial: `PENDIENTE` ya salía correcto.
- `PROCESADO` quedaba en `0` aunque ya existía registro guardado en `packing_enc`.
- En iteraciones previas aparecían invertidos según el criterio usado.

## Evidencia de logcat
- `Inserta_Packing` OK (`regs=1`)
- `Get_All_PickingUbic_By_PickingEnc` podía devolver 0 filas (`PICK_NULL_GUARD`)
- `Get_All_Packing_By_IdPicking` devolvía `saved_sz=1`
- Resultado final: `PENDIENTE=1 pick_sz=0 items_sz=1`

Conclusión: la UI estaba mezclando dos semánticas distintas para `PROCESADO`.

## Causa raíz
El contador `PROCESADO` se calculaba por `neto == cant` (completo) en lugar de reflejar
registros efectivamente guardados en packing.

## Fix aplicado
Tag inline:
`#EJC20260601 fix(packing-hh-contadores): PROCESADO debe reflejar registros guardados en packing_enc (items), no solo los "completos" por neto==cant.`

Cambio funcional:
- `procesados` pasa a inicializarse como `items.size()`.
- El loop de `counterSource` queda enfocado en calcular `pendientes`.
- Se elimina la suma incremental por `else { procesados++; }` para evitar mezclar criterios.
- Se agrega log de trazabilidad:
  - `COUNTER_PROCESADOS saved_items=... procesados_ui=...`

## Regla resultante
- `PENDIENTE`: unidades aún no cubiertas contra lo verificado.
- `PROCESADO`: registros guardados en `packing_enc`.

Esto permite que en parcial aparezca: `PENDIENTE=1` y `PROCESADO=1` sin inconsistencia.

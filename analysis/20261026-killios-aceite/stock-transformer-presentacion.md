# Stocks menores al factor conservando presentación — CKFK20261027

## Conclusión

No fue únicamente un ejemplo hipotético: existen registros reales y se reprodujo un generador del estado anómalo partiendo de stock válido. El ejemplo WebAPI de18 UN con Caja12 era sintético; no se debe presentar como un IdStock productivo identificado. El problema del metadato y la eventual creación de cantidad son dos defectos distintos.

## Consulta productiva actual, solo lectura

56-stock-presentacion-remanente-prd.json,27/10/2026:77 stocks positivos en unidad base UN con cantidad menor al factor y presentación vigente, en2 productos/licencias:

| Producto | Licencia | Stocks |
|---|---|---:|
|WMS215|JM001698|1|
|WMS272|FU07927|76|

Ejemplos actuales:

| Producto | IdStock | Cantidad UN | IdPresentacion | Factor |
|---|---:|---:|---:|---:|
|WMS215|210899|6|148 Caja12|12|
|WMS272|200601|11.04|43 Caja12|12|
|WMS272|172113|0.2|43 Caja12|12|

No se suman esos stocks como inventario inventado. Representan inconsistencia cantidad/presentación según la regla operativa indicada por Carolina.

Los dos stocks de aceite WMS282 consultados ahora,208668 y209033, tienen9 UN cada uno y **IdPresentacionNULL**. Actualmente están expresados en unidades. No se los debe citar como9 UN conservando Caja12. La reproducción9->12 del motor de reserva empleó stocks sintéticos con la condición anómala para demostrar el riesgo del consumidor.

## Generador reproducido desde stock válido

Se ejecutó el cambio de estado DAL transaccional completo, clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic, con la DAL real compilada de la rama. Stocks de ensayo de WMS272 creados como múltiplos exactos de Caja12; solicitudes en UN, con IdPresentacion capturada0 en los dos casos fraccionados.

| Origen inicial | Mover UN | Origen final | Destino final | Total |
|---|---:|---|---|---:|
|24 UN, Caja12|3|21 UN, Caja12|3 UN, Caja12|24|
|12 UN, Caja12|9|3 UN, Caja12|9 UN, Caja12|12|
|24 UN, Caja12|12|12 UN, Caja12|12 UN, Caja12|24|

Los tres devolvieron aplicado=true. El control de caja completa conserva presentación correctamente. Los casos de3/9 UN generan fragmentos menores al factor y conservan presentación indebidamente, aun cuando la solicitud especificó captura en UN. No fue necesario sembrar inicialmente un stock menor al factor.

Evidencia58-generacion-remanente-presentacion-qa.json. Cada caso terminó en rollback; stocks originales sin cambios. No es ejecución de pantalla ni llamada HTTP; es la rutina de negocio completa con transacción externa. Las ramas de cambio individual de HH invocan la misma operación Aplicar usada por este overload.

## Punto de código

Movimiento/clsLnTrans_movimientos_Partial.vb, función Aplicar:

- Clona el stock origen en BeStockNuevo.
- Calcula origen=cantidadOriginal-cantidadMovida y actualiza el origen.
- Asigna la cantidad movida al clon y conserva la presentación del origen.
- La conversión explícita a presentación0 está condicionada a IdTipoTarea20 (explosión manual). Un cambio de estado tipo3 no entra en ella.
- No se verifica automáticamente que un fragmento menor al factor quede en UN antes de persistir origen/destino.

Por tanto, la entrada correctamente normalizada en UN no basta: el backend puede generar la inconsistencia de presentación al dividir. La corrección Java de factor único y el tope de conversión en reserva no corrigen por sí solos este punto.

## Relación histórica, sin sobreafirmar

WMS272 tiene movimientos CEST del03/10 y las cantidades actuales de esa licencia mantienen el patrón de fragmentación documentado. Eso es compatible con este generador, pero no demuestra la versión/pantalla exacta ni vincula por clave cada movimiento a cada IdStock actual: trans_movimientos no incluye esa relación completa.

57-traza-registros-reales-prd.json también encuentra movimientos CEST/UBIC de WMS215/JM001698 del27/10 con cantidades parciales y presentación148, entre ellos606736 por6 UN. No se lo asigna concluyentemente al IdStock210899 porque no hay enlace directo por IdStock en ese movimiento y los horarios de cliente/creación difieren. La consulta stock_hist por los IdStock/IdNuevoStock seleccionados no devolvió un vínculo adicional.

El stock172113 de WMS272 era264 UN en el corte QAS; hoy es0.2 UN conservando presentación43. Esa comparación documenta degradación del metadato durante el período, pero no implica aumento del total de la licencia. El origen histórico de las3 UN adicionales del aceite sigue sin demostrar.

## Qué cambia en la evaluación previa

La prueba anterior41-cest-corregido-wms272.json ya contenía el caso264->261+3 con presentación43 en ambos. Se verificó conservación de cantidad, pero faltó exigir el invariante de presentación de los fragmentos. No debe interpretarse aquel resultado como certificación completa del cambio de estado.

La corrección debe cubrir al productor: normalizar/split de fragmentos de origen y destino según unidad real y factor del origen; mantener presentaciones de cajas completas y las reservas que las comprometen. No basta borrar presentación indiscriminadamente en un origen con reservas en cajas. En paralelo debe conservarse el guard del consumidor de reserva para que datos históricos inconsistentes nunca puedan fabricar unidades al convertir.

En esta revisión no se modificaron fuentes WMS ni producción. El nuevo hallazgo queda pendiente de corrección de backend y regresión con reservas previas, cambio dirigido/no dirigido y escritores relacionados.

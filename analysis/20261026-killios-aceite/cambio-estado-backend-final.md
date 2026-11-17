# Cambio de estado sin stock transformer — CKFK20261027

## Estado de entrega

Cambios locales aplicados en BOF, rama codex/killios-decimales-estados. Servicio WSHHRN y DAL compilados. Sin commit, push ni despliegue. No se ajustó inventario productivo.

Carolina confirmó la recuperación del acceso y se validó nuevamente QAS. La ronda posterior aprobó44 escenarios:29 de regresión,4 de concurrencia,8 de factores/tarimas y3 de reserva MI3. Ver pruebas-restablecidas-20261027.md y evidencias65–71. Queda pendiente la prueba desde pantalla/HTTP del servicio que Carolina está publicando. No se publicaron binarios automáticamente.

## Cambios de este ajuste

- Movimiento/clsLnTrans_movimientos_Partial.vb: validación de cantidad y disponibilidad, bloqueo transaccional del stock/reservas, protección de reserva propia en tarea dirigida, normalización de destino y separación del residuo libre en origen. Aplicado a Aplicar y a la partición física del cambio automático por picking.
- Stock/clsLnStock_Partial.vb: incorpora esas protecciones a la actualización de stock utilizada por BOF cuando cambian los estados.
- Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb: ambos overloads de Aplica_Cambio_Estado_Ubic rechazan solicitudes inválidas antes de registrarlas en SQL y solicitudes superiores al total libre antes de aplicar parcialmente.

La corrección anterior de Stock_Reservado/clsLnStock_res_Partial.vb permanece intacta (SHA256 BAB91387FE37D83FCA80FCA0F580C8C2C5C96C2909FB5D4B87B6612D93126246).

## Reglas implementadas

1. Cantidad positiva, finita y representable a6 decimales; en unidad base UN, cantidad entera.
2. En la captura de CEST que declara presentación, el equivalente en unidades debe representar cajas completas. Las solicitudes de pedidos y la reserva mixta1.5 CA no reciben esa restricción.
3. La solicitud no puede superar la existencia libre ni consumir reservas ajenas. Una tarea dirigida puede consumir su reserva propia, sin excederla.
4. Si el estado o ubicación de origen ya cambió frente al movimiento recibido, rechazar y solicitar una nueva consulta del stock.
5. Un destino que no equivale a presentaciones completas queda sin presentación. Los múltiplos exactos conservan su presentación física; se utiliza el factor del stock, incluyendo composición cuando la presentación es pallet.
6. En origen, conservar las cajas completas y sus reservas sobre el IdStock original; separar el residuo libre en otro stock en UN, con misma licencia/lote/ubicación/estado y copia de parámetros.
7. Si normalizar requiere cambiar la presentación de un saldo que sigue comprometido por reservas en presentación y no puede dejarse respaldado en las cajas originales, rechazar. No se migran silenciosamente reservas ni referencias de picking entre IdStock.
8. Usar UPDLOCK/HOLDLOCK para la lectura de stock y reservas dentro de la transacción de aplicación. No equivale a certificación de concurrencia de todos los escritores del WMS.

Ejemplo comprobado:24 UN en Caja12,12 UN ya reservadas, mover3 UN -> origen12 UN con Caja12 y la misma reserva; origen9 UN sin presentación; destino3 UN sin presentación. Total24. El IdStock reservado y su presentación permanecen intactos.

También se verificó el dato histórico de18 UN con Caja12 y12 reservadas: mover3 deja12 en caja respaldando la reserva,3 UN sueltas en origen y3 UN en destino. No se abre la caja ya comprometida.

## Validación anterior al corte de conexión

-29 casos de QAS:61-regresion-cambio-estado-qa.json; salida PASS en61-regresion-cest-run3.log. Incluye cambios individuales, enteros/parciales, reservas previas, datos históricos no múltiplos, tareas dirigidas, actualización de stock BOF, cambios automáticos por picking y rechazos por exceso, cantidades inválidas, fracciones de UN/cajas, factor cero y estado/ubicación obsoletos.
-En cada éxito se comprobó conservación de cantidad, estado/cantidad de destino, presentación únicamente para múltiplos completos y reservas por IdStock no superiores a su saldo. Se verificó que reservas previas mantuvieran su IdStock/cantidad/presentación. En rechazos se revirtió al punto anterior a la operación y se verificó ausencia de cambios parciales.
-Todos los ensayos finalizaron en rollback. Las fotografías de los stocks originales coinciden. Los contadores identity pueden avanzar por las inserciones revertidas.
-3 casos adicionales del motor MI3 completo con reservas previas: cest-44-explosion-parcial-reservada.json. Solicitudes3/15 UN, cantidades originales y reservas conservadas, resultadoTrue. Resultado revisado localmente tras la pausa, sin nuevas conexiones SQL.
-62-build-cest.log: compilación exitosa de WSHHRN y dependencias; git diff --check correcto; UTF8 con BOM y CRLF preservados en los tres fuentes.
-DLL probada: SHA256 6D318A8CD3001A8FA927E164777A9010F3DCF26F15E448DE9053DFB15F877FCD. La DLL local coincide con el hash de la matriz de29 casos.
-Auditoría automática local WARN por consulta de correlación de BD omitida. La revisión manual se apoya en las consultas y pruebas anteriores; el escáner no constituye PASS integral. Archivos de auditoría, alcance y búsqueda de rutas hermanas en cest-backend/.

Durante la preparación de la matriz se corrigieron dos errores del ejecutor de pruebas (construcción de nombres de columnas y evaluación de DBNull). Una primera ejecución también detectó que NaN llegaba a la bitácora antes de validar; se corrigió el backend, se recompiló y la matriz final pasó. Los intentos anteriores no cuentan como PASS.

## Preparación para prueba funcional con Carolina

Cuando se restablezca acceso, confirmar QAS y usar una versión del servicio que incluya esta DAL. No se ha publicado el binario automáticamente. Repetir desde pantalla HH/BOF los siguientes controles con licencias de ensayo:

Preflight de HH al preparar esta entrega: rama dev_2028_merge, sin las llamadas a CantidadCambioEstado de la mejora anterior en frm_cambio_ubicacion_ciega.java; CantidadCambioEstado.java aparece sin seguimiento. No se cambió esa rama ni se aplicó allí la mejora. Antes de probar pantalla, verificar/seleccionar la versión de HH del trabajo de Killios y su conexión al servicio QAS corregido. La mejora de backend no sustituye la corrección de captura del cliente.

| Preparación | Acción | Resultado esperado |
|---|---|---|
|Una caja12 sin reservas|Cambiar9 UN|3 UN en origen y9 UN en destino, ambas sin presentación.|
|Dos cajas12 sin reservas|Cambiar3 UN|12 UN en caja y9 UN sueltas en origen;3 UN sueltas en destino.|
|Dos cajas12, una ya reservada|Cambiar3 UN libres|Reserva de12 intacta sobre el IdStock original; mismo reparto12+9+3.|
|Dos cajas12|Cambiar una caja|12 UN con presentación en origen y12 UN con presentación en destino.|
|Una caja completamente reservada para otro documento|Cambiar3 UN|Rechazo; stock y reserva sin cambios.|
|Captura en Caja12|Ingresar0.5 caja en CEST|Rechazo o selección explícita de UN; esto no restringe pedidos0.5 CA.|
|Consulta de stock seguida de cambio de ubicación/estado por otro operador|Aplicar consulta anterior|Rechazo y nueva consulta; sin movimientos parciales.|

La automatización probó las clases de negocio, no pantallas ni SOAP/HTTP completos. Falta ese recorrido funcional con el servicio/HH apuntando a QAS. Se añadieron4 controles de bloqueo y revalidación entre dos sesiones y3 controles de tarima72; ver el informe de la ronda restablecida para sus límites. No se certificaron conservación integral de peso, parámetros específicos de otros clientes ni todos los escritores concurrentes del WMS.

## Alcance pendiente del caso general

Este ajuste corrige los productores de fragmentos en los cambios de estado VB cubiertos. No corrige los registros históricos automáticamente ni demuestra el origen histórico del +3 de aceite. Los defectos de reserva del motor C# WebAPI documentados en analisis-webapi.md permanecen separados y no fueron modificados aquí.

Parche de este ajuste: cambio-estado-backend-CKFK20261027.patch, limitado a los tres fuentes del cambio de estado. El archivo de reserva previo queda separado para revisión.

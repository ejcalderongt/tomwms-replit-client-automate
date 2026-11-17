# Corrección de conservación de stock — Carolina, #CKFK20261026_RESERVA_EXPLOSION

## Cambio aplicado

Rama local BOF: dev_2026_estable. Archivo único modificado: TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb.

La conversión queda limitada al mínimo entre cantidad a convertir, cantidad física del origen y cantidad disponible descontando reservas. El origen descuenta exactamente la cantidad convertida, redondeada a seis decimales. Las cantidades inválidas/no positivas se rechazan. La reserva creada no supera la cantidad convertida.

Se protegieron los 15 bloques activos que compartían el patrón en Reserva_Stock_From_MI3, Reserva_Stock_From_SAP y Reserva_Stock_From_Reabasto mediante un helper privado común. No se modificaron firmas públicas, contratos HH, configuración ni esquema SQL. Se preservaron UTF-8 con BOM y CRLF. Los tags históricos se conservaron; el nuevo tag de autoría es CKFK, confirmado por Carolina y brain/agent-context/EQUIPO_PROGRAX24.md.

## Validación

| Prueba del motor MI3 completo | Código original | Corregido |
|---|---|---|
| Stock 9 UN, Caja12, solicitar 3 UN | Total 12; reserva 3; aumento +3 | Total 9; reserva 3; aumento 0 |
| Stock 9.25 UN, solicitar 3 UN | Total 12; aumento +2.75 | Rechazo por política de unidades sin decimales; total 9.25 sin conversión |
| Stock 12 UN, solicitar 3 UN | Total 12; reserva 3 | Total 12; reserva 3 |
| Stock 24 UN, solicitar 3 UN | Total 24; reserva 3 | Total 24; reserva 3 |
| Stock 24 UN, con 18 reservadas para otro pedido, solicitar 3 | No ejecutado como referencia | Total 24; reserva anterior 18 intacta; nueva reserva 3; solo 6 se convierten |

El ensayo completo usa Reserva_Stock_From_MI3 real, configuración QAS 1 (explosión automática habilitada), estado NUEVA ERA, ubicación 756, producto WMS282 y una línea QAS ajustada temporalmente dentro de la transacción. El ensamblado de referencia se compiló sustituyendo únicamente el archivo por su copia original de la misma rama mediante un target de compilación temporal; el checkout corregido no se revirtió.

La firma de autoría se corrigió después de los ensayos completos; no hubo cambios de lógica posteriores. Se recompiló la versión final con CKFK y se ejecutaron 14 comprobaciones sobre el helper final, incluyendo disponibles reservados, límites, cero, negativos, NaN e infinito. Las 14 pasaron.

Compilación WSHHRN con DAL y dependencias: 0 errores, 0 advertencias. La prueba completa automatizada cubrió MI3; SAP y reabasto compilaron y se inspeccionaron sus 9 bloques homólogos, pero no se ejercitaron de extremo a extremo ni se probó concurrencia entre operadores. No se desplegó a producción.

## QAS y seguridad de datos

Cada caso ejecutó rollback sobre su transacción de datos. Verificación posterior: 11 stocks originales de WMS282, 484 UN, cero stocks y cero reservas con licencia de prueba. Los registros originales coinciden con la fotografía anterior en IdStock, cantidad, estado, presentación y licencia. Las inserciones revertidas consumen identidades; no se reiniciaron contadores. Las bitácoras internas del motor pueden usar conexiones independientes de la transacción de stock; no se afirma que sus asientos de prueba se hayan revertido.

La conexión productiva se utilizó únicamente para SELECT de alcance. No se ajustó inventario ni se modificaron conexiones/configuración del repositorio. Los archivos .config copiados automáticamente a las carpetas de compilación de análisis se retiraron; no forman parte del entregable.

## Alcance potencial investigado, sin asumir errores históricos

Desde el 19/09 hasta la consulta del 26/10, CASO_#21 registra 1,324 entradas de log, 543 pedidos y 155 códigos de producto. Son usos de la subrama, no un conteo de inventario aumentado; el registro no contiene el saldo anterior suficiente para calcular la afectación.

El stock actual contiene 77 candidatos con cantidad positiva menor al factor y presentación conservada:

| Producto | Licencia | Registros candidatos |
|---|---|---:|
| WMS215, vinagre balsámico | JM001698 | 1 |
| WMS272, elotitos | FU07927 | 76 |

Estos registros no se declaran erróneos por ese criterio; una fracción puede requerir interpretación adicional. Tampoco se suman las diferencias contra una caja como si fueran unidades ya generadas. WMS282 mantiene su diferencia histórica de 3 UN previamente documentada; reproducir el defecto completo aumenta la evidencia del mecanismo, pero no recupera el saldo del origen ausente en el incidente.

## Revisión y pendientes

- Se verificó que los 15 bloques activos usan el helper y descuentan la cantidad real; las variantes comentadas permanecieron intactas.
- diff --check correcto; solo un archivo fuente BOF modificado. Sin cambios HH, commits ni pushes.
- La auditoría automática produjo WARN por omitir su conexión de consulta genérica de esquema; las tablas/columnas se contrastaron mediante las consultas QAS y el catálogo del caso. No se habilitaron cambios SQL. El resultado WARN del escáner se conserva, no se presenta como un PASS automatizado.
- El índice remoto Brain no se actualizó porque BRAIN_IMPORT_TOKEN no está disponible; la traza local queda en este informe y en el handoff.
- La rama local estaba tres commits detrás de origin/dev_2026_estable; no se hizo pull ni merge automático.
- Antes de publicar: revisión humana de la corrección, prueba funcional en la versión de servicio que se desplegará y validación de rutas SAP/reabasto aplicables. El inventario histórico debe conciliarse y contarse antes de cualquier ajuste.

## Archivos de evidencia

23-fix-provenance.json; build-stable-final.log; baseline-25-prueba-motor-completo-qa.json; 25-prueba-motor-completo-qa.json; reservas-25-prueba-motor-completo-qa.json; 26-helper-regresion.json; 27-alcance-candidatos.json; 28-candidatos-agrupados.json; 29-control-qa-postfix.json; 30-validacion-final.json. Parche: correccion-dev_2026_estable.patch.

# Cruce contra BD del archivo RevisionVerificacionDoble.xlsx

## 1. Objetivo

Validar el archivo enviado por el cliente `RevisionVerificacionDoble.xlsx` contra la base de datos local de análisis y determinar si la evidencia coincide con el patrón de duplicación de movimientos `VERI` identificado previamente.

## 2. Resultado ejecutivo

El archivo del cliente coincide con la información consultada en base de datos para los movimientos listados.

El caso confirma el patrón de duplicación exacta de movimientos de verificación:

- picking / transacción: `1465`;
- barra pallet: `TEA4406620021237`;
- producto: `440662`;
- lote: `L0114:1625312`;
- cantidad operativa esperada: `72` cajas, equivalentes a `1,728` unidades;
- cantidad acumulada en movimientos `VERI`: `3,456` unidades;
- factor observado: `2x`;
- exceso por duplicación: `1,728` unidades.

## 3. Archivo recibido

El workbook contiene una hoja:

| Hoja | Filas útiles | Columnas |
| --- | ---: | ---: |
| `Hoja1` | 12 | 34 |

Las filas corresponden a movimientos de inventario para el mismo producto, lote y barra/pallet.

## 4. Movimientos del archivo

| IdMovimiento | Tipo tarea | IdTransaccion | Cantidad | Cantidad presentación | Fecha |
| ---: | --- | ---: | ---: | ---: | --- |
| 104387 | RECE | 426 | 1728 | 72 | 2025-11-08 13:31:24 |
| 104474 | UBIC | 1 | 1728 | 72 | 2025-11-08 16:21:54 |
| 147608 | AJCANTPI | 68 | 1728 | 72 | 2025-11-30 22:43:21 |
| 174131 | AJCANTN | 1821 | 1728 | 72 | 2025-12-13 17:37:50 |
| 284895 | PIK | 1465 | 1704 | 71 | 2026-02-17 18:42:54 |
| 284896 | PIK | 1465 | 24 | 1 | 2026-02-17 18:43:00 |
| 288682 | VERI | 1465 | 1704 | 71 | 2026-02-19 11:18:49 |
| 288683 | VERI | 1465 | 24 | 1 | 2026-02-19 11:18:49 |
| 288706 | VERI | 1465 | 1704 | 71 | 2026-02-19 11:19:00 |
| 288707 | VERI | 1465 | 24 | 1 | 2026-02-19 11:19:00 |
| 288730 | DESP | 1265 | 1704 | 71 | 2026-02-19 11:19:09 |
| 288731 | DESP | 1265 | 24 | 1 | 2026-02-19 11:19:09 |

## 5. Validación contra base de datos

Se consultó `vw_movimientos` por los 12 `IdMovimiento` recibidos en el archivo.

Resultado:

- los 12 movimientos existen en la base consultada;
- los campos principales coinciden: tipo de tarea, transacción, documento de ingreso, producto, barra/pallet, lote, fecha de vencimiento, cantidad, cantidad en presentación, origen, destino, estado y fecha;
- no se observó discrepancia entre el extracto del cliente y la base para esos registros.

## 6. Evidencia de duplicación VERI

Para `IdTransaccion = 1465` y `barra_pallet = TEA4406620021237`, los movimientos `VERI` son:

| IdMovimiento | Cantidad | Cantidad presentación | Repetición |
| ---: | ---: | ---: | ---: |
| 288682 | 1704 | 71 | 1 |
| 288706 | 1704 | 71 | 2 |
| 288683 | 24 | 1 | 1 |
| 288707 | 24 | 1 | 2 |

Esto muestra dos pares repetidos:

- `1704` unidades aparece dos veces;
- `24` unidades aparece dos veces.

Total esperado por una sola verificación:

```text
1704 + 24 = 1728 unidades
71 + 1 = 72 cajas
```

Total registrado en movimientos `VERI`:

```text
1704 + 24 + 1704 + 24 = 3456 unidades
71 + 1 + 71 + 1 = 144 cajas
```

La diferencia es:

```text
3456 - 1728 = 1728 unidades de exceso
144 - 72 = 72 cajas de exceso
```

## 7. Cruce contra trans_picking_ubic

Para el picking `1465`, el mismo producto, lote y barra/pallet tiene dos líneas operativas:

| IdPickingUbic | Cantidad solicitada | Cantidad recibida | Cantidad verificada |
| ---: | ---: | ---: | ---: |
| 42798 | 71 | 71 | 71 |
| 42849 | 1 | 1 | 1 |
| **Total** | **72** | **72** | **72** |

El factor del producto observado en el movimiento es `24` unidades por caja:

```text
72 cajas * 24 = 1728 unidades
```

Comparación:

| Fuente | Cantidad presentación | Cantidad unidades |
| --- | ---: | ---: |
| `trans_picking_ubic` | 72 | 1728 |
| `trans_movimientos` VERI | 144 | 3456 |
| Exceso | 72 | 1728 |
| Factor de duplicación | 2x | 2x |

## 8. Diagnóstico

El archivo enviado por el cliente confirma el patrón de duplicación exacta de movimientos `VERI`.

La verificación esperada para el picking `1465` era un set de dos movimientos:

- `1704` unidades;
- `24` unidades.

Sin embargo, el sistema registró ese mismo set dos veces, con la misma transacción, producto, recepción, ubicación, barra/pallet, lote, vencimiento y cantidad.

El comportamiento es consistente con una reejecución del proceso de verificación sobre una línea o picking que ya contaba con movimientos `VERI` generados.

## 9. Conclusión para comunicación al cliente

El archivo proporcionado por el cliente es consistente con la base de datos y permite confirmar la duplicación.

La diferencia no proviene de una captura distinta de cantidades, sino de la existencia de movimientos `VERI` repetidos para la misma operación lógica.

Para este caso, la cantidad verificada operativamente es `72` cajas (`1,728` unidades), mientras que la tabla de movimientos refleja `144` cajas (`3,456` unidades) por duplicación del set de movimientos de verificación.

## 10. Próximo paso recomendado

Preparar un script de auditoría y regularización para el picking `1465` que:

1. identifique duplicados exactos con `ROW_NUMBER()`;
2. conserve un único movimiento por llave lógica;
3. marque como candidatos a depuración los registros repetidos;
4. ejecute inicialmente en modo simulación con `ROLLBACK`;
5. muestre el antes y después contra `trans_picking_ubic`.

Para este archivo, los candidatos de depuración serían:

| IdMovimiento | Motivo |
| ---: | --- |
| 288706 | Duplicado exacto de `288682` |
| 288707 | Duplicado exacto de `288683` |

La ejecución de cualquier depuración debe realizarse únicamente con autorización explícita y respaldo previo.

## 11. Scripts y procedimientos preparados

Para facilitar la ejecución controlada del análisis y la regularización, se prepararon scripts T-SQL separados por tipo de hallazgo.

| Archivo | Propósito |
| --- | --- |
| `SCRIPT_AUDITORIA_REGULARIZACION_VERI_DUPLICADOS.sql` | Detecta y, si se autoriza, depura duplicados exactos `VERI` conservando un movimiento canónico. |
| `SCRIPT_AUDITORIA_REGULARIZACION_VERI_CANTIDAD_UMBAS.sql` | Detecta y, si se autoriza, corrige movimientos `VERI` grabados en cantidad de presentación cuando deberían estar en UMBAS. |
| `SP_REGULARIZACION_VERI.sql` | Crea procedimientos almacenados reutilizables para auditoría, regularización y verificación posterior. |

Los procedimientos almacenados creados por `SP_REGULARIZACION_VERI.sql` son:

| Procedimiento | Función |
| --- | --- |
| `dbo.usp_WMS_VERI_RegularizarDuplicadosExactos` | Audita o elimina duplicados exactos de movimientos `VERI`. |
| `dbo.usp_WMS_VERI_RegularizarCantidadUmbas` | Audita o actualiza movimientos `VERI` que fueron grabados en presentación en vez de UMBAS. |
| `dbo.usp_WMS_VERI_PostCheck` | Verifica después de la regularización si quedan duplicados o diferencias de presentación. |

## 12. Mecanismo de ejecución segura

Los procedimientos fueron diseñados con tres modos:

| Modo | Parámetros | Resultado |
| --- | --- | --- |
| Auditoría | `@EjecutarDelete = 0` o `@EjecutarUpdate = 0` | No modifica datos. Devuelve candidatos y bitácora propuesta. |
| Simulación | `@EjecutarDelete/@EjecutarUpdate = 1`, `@ConfirmarCommit = 0` | Ejecuta dentro de transacción y luego hace `ROLLBACK`. |
| Aplicación | `@EjecutarDelete/@EjecutarUpdate = 1`, `@ConfirmarCommit = 1` | Ejecuta y confirma los cambios con `COMMIT`. |

Para procesar un picking específico:

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465;

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465;
```

Para procesar todos los pickings, debe indicarse explícitamente:

```sql
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = NULL,
    @ProcesarTodos = 1;
```

Este bloqueo evita ejecutar regularizaciones globales por accidente.

## 13. Orden recomendado de regularización

El orden recomendado es:

1. Auditar duplicados exactos `VERI`.
2. Regularizar duplicados exactos, si aplica.
3. Ejecutar verificación posterior.
4. Auditar diferencias por cantidad en presentación vs UMBAS.
5. Regularizar cantidades UMBAS, si aplica.
6. Ejecutar verificación posterior final.

Para el picking `1465`, después de depurar duplicados exactos se observó:

| Control | Resultado |
| --- | ---: |
| Duplicados exactos restantes | 0 |
| Diferencias presentación restantes | 5 |
| Movimientos candidatos a corregir UMBAS | 5 |

Los cinco movimientos pendientes corresponden a `VERI` registradas con `72` unidades cuando deberían ser `1,728` UMBAS según presentación `CAJA24`.

## 14. Comandos de ejemplo

### 14.1 Auditoría de duplicados exactos

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465;
```

### 14.2 Aplicar depuración de duplicados exactos

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465,
    @EjecutarDelete = 1,
    @ConfirmarCommit = 1;
```

### 14.3 Auditoría de cantidades UMBAS

```sql
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465;
```

### 14.4 Aplicar corrección de cantidades UMBAS

```sql
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465,
    @EjecutarUpdate = 1,
    @ConfirmarCommit = 1;
```

### 14.5 Validación posterior

```sql
EXEC dbo.usp_WMS_VERI_PostCheck
    @IdPickingEnc = 1465;
```

## 15. Validaciones esperadas

Después de aplicar duplicados exactos:

```text
DUPLICADOS_EXACTOS_RESTANTES = 0
```

Después de aplicar corrección de cantidades UMBAS:

```text
MISMATCH_PRESENTACION_RESTANTES = 0
```

Adicionalmente, para el caso del archivo del cliente, los movimientos:

| IdMovimiento | Antes | Después esperado |
| ---: | ---: | ---: |
| 288676 | 72 | 1728 |
| 288681 | 72 | 1728 |
| 288678 | 72 | 1728 |
| 288679 | 72 | 1728 |
| 288680 | 72 | 1728 |

La vista `vw_movimientos` debería reflejar `Cantidad_Presentacion = 72` para esos movimientos, una vez que `trans_movimientos.cantidad` quede en `1728` y se aplique el factor `24`.

## 16. Consideraciones para aplicación global

Los procedimientos permiten auditoría y regularización global con:

```sql
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = NULL,
    @ProcesarTodos = 1;
```

Sin embargo, antes de una aplicación global se recomienda:

- ejecutar solo auditoría;
- exportar la bitácora propuesta;
- revisar muestra por producto, presentación y picking;
- respaldar la base o las tablas afectadas;
- aplicar primero un picking validado;
- ejecutar `usp_WMS_VERI_PostCheck`;
- aplicar global únicamente con autorización explícita.

El análisis preliminar detectó candidatos globales, por lo que la aplicación masiva debe tratarse como actividad controlada de mantenimiento de datos.

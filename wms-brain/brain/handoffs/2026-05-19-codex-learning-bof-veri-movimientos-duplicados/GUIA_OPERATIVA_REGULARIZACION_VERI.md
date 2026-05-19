# Guía operativa: regularización de movimientos VERI

## Objetivo

Establecer un procedimiento seguro para auditar, simular, aplicar y validar la regularización de movimientos `VERI` asociados al proceso de verificación de picking.

Esta guía cubre dos hallazgos distintos:

1. Duplicados exactos de movimientos `VERI`.
2. Movimientos `VERI` grabados en cantidad de presentación en vez de UMBAS.

## Archivos incluidos

| Archivo | Descripción |
| --- | --- |
| `SCRIPT_AUDITORIA_REGULARIZACION_VERI_DUPLICADOS.sql` | Script standalone para duplicados exactos. |
| `SCRIPT_AUDITORIA_REGULARIZACION_VERI_CANTIDAD_UMBAS.sql` | Script standalone para cantidades VERI en presentación vs UMBAS. |
| `SP_REGULARIZACION_VERI.sql` | Script instalable con procedimientos almacenados. |

## Procedimientos almacenados

El archivo `SP_REGULARIZACION_VERI.sql` crea:

```sql
dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
dbo.usp_WMS_VERI_RegularizarCantidadUmbas
dbo.usp_WMS_VERI_PostCheck
```

Se usa prefijo `usp_` para evitar el comportamiento especial de SQL Server con procedimientos `sp_`.

## Parámetros de seguridad

Los procedimientos usan banderas explícitas:

| Parámetro | Uso |
| --- | --- |
| `@IdPickingEnc` | Picking específico a auditar o regularizar. |
| `@ProcesarTodos` | Debe ser `1` si `@IdPickingEnc = NULL`. Evita ejecuciones globales accidentales. |
| `@EjecutarDelete` | Ejecuta borrado de duplicados exactos. Solo aplica en `usp_WMS_VERI_RegularizarDuplicadosExactos`. |
| `@EjecutarUpdate` | Ejecuta actualización de cantidad VERI a UMBAS. Solo aplica en `usp_WMS_VERI_RegularizarCantidadUmbas`. |
| `@ConfirmarCommit` | Si es `1`, confirma cambios. Si es `0`, ejecuta `ROLLBACK`. |

## Modos de ejecución

### Solo auditoría

No modifica datos.

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465;

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465;
```

### Simulación con ROLLBACK

Ejecuta la operación dentro de una transacción y revierte al final.

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465,
    @EjecutarDelete = 1,
    @ConfirmarCommit = 0;

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465,
    @EjecutarUpdate = 1,
    @ConfirmarCommit = 0;
```

### Aplicación real con COMMIT

Debe usarse solo con autorización explícita.

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = 1465,
    @EjecutarDelete = 1,
    @ConfirmarCommit = 1;

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465,
    @EjecutarUpdate = 1,
    @ConfirmarCommit = 1;
```

## Orden recomendado

1. Instalar procedimientos.
2. Ejecutar auditoría de duplicados exactos.
3. Simular depuración de duplicados exactos.
4. Aplicar depuración de duplicados exactos.
5. Ejecutar `usp_WMS_VERI_PostCheck`.
6. Ejecutar auditoría de cantidades UMBAS.
7. Simular actualización de cantidades UMBAS.
8. Aplicar actualización de cantidades UMBAS.
9. Ejecutar `usp_WMS_VERI_PostCheck` final.

## Caso base: picking 1465

### Auditoría inicial

```sql
EXEC dbo.usp_WMS_VERI_PostCheck
    @IdPickingEnc = 1465;
```

Resultado esperado después de depurar duplicados exactos:

| Sección | Conteo esperado |
| --- | ---: |
| `DUPLICADOS_EXACTOS_RESTANTES` | 0 |
| `MISMATCH_PRESENTACION_RESTANTES` | 5 |

### Regularización de cantidades UMBAS

```sql
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = 1465;
```

Movimientos esperados como candidatos:

| IdMovimiento | Cantidad anterior | Cantidad nueva | Presentación | Factor |
| ---: | ---: | ---: | --- | ---: |
| 288676 | 72 | 1728 | CAJA24 | 24 |
| 288681 | 72 | 1728 | CAJA24 | 24 |
| 288678 | 72 | 1728 | CAJA24 | 24 |
| 288679 | 72 | 1728 | CAJA24 | 24 |
| 288680 | 72 | 1728 | CAJA24 | 24 |

### Validación final

```sql
EXEC dbo.usp_WMS_VERI_PostCheck
    @IdPickingEnc = 1465;
```

Resultado esperado después de aplicar la corrección UMBAS:

| Sección | Conteo esperado |
| --- | ---: |
| `DUPLICADOS_EXACTOS_RESTANTES` | 0 |
| `MISMATCH_PRESENTACION_RESTANTES` | 0 |

## Aplicación global

Los procedimientos soportan procesamiento global:

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = NULL,
    @ProcesarTodos = 1;

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = NULL,
    @ProcesarTodos = 1;
```

Para aplicar global:

```sql
EXEC dbo.usp_WMS_VERI_RegularizarDuplicadosExactos
    @IdPickingEnc = NULL,
    @ProcesarTodos = 1,
    @EjecutarDelete = 1,
    @ConfirmarCommit = 1;

EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas
    @IdPickingEnc = NULL,
    @ProcesarTodos = 1,
    @EjecutarUpdate = 1,
    @ConfirmarCommit = 1;
```

Antes de aplicar globalmente:

- ejecutar auditoría y exportar resultados;
- revisar una muestra funcional;
- respaldar base o tablas involucradas;
- validar primero un picking;
- aplicar en ventana controlada;
- conservar resultsets de bitácora.

## Bitácora

Cada procedimiento devuelve resultsets con:

- `RunId`;
- fecha de ejecución;
- modo de ejecución;
- movimientos propuestos o aplicados;
- movimiento conservado en caso de duplicados;
- cantidad anterior y nueva en caso de corrección UMBAS.

Si se requiere bitácora persistente, el siguiente paso recomendado es crear una tabla física de auditoría, por ejemplo:

```sql
dbo.audit_regularizacion_veri
```

La versión actual devuelve la bitácora como resultset para exportarla desde SSMS antes o después de la aplicación.

## Criterios de no intervención

No se corrigen automáticamente:

- movimientos `VERI` huérfanos sin relación clara con `trans_picking_ubic`;
- grupos con más de una `VERI` cuando no son duplicados exactos;
- diferencias donde el `PIK` no confirma la cantidad esperada en UMBAS;
- líneas con `Cantidad_Verificada = 0`;
- casos donde `factor <= 1`.

Estos casos quedan para análisis funcional separado.

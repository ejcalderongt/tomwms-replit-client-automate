# Log diario

## Propósito

Este archivo funciona como bitácora interna diaria del trabajo realizado entre Erik, Codex local y Brain Keeper.

Debe usarse para conservar un historial claro de:

- cambios aplicados;
- análisis realizados;
- decisiones técnicas;
- validaciones ejecutadas;
- riesgos detectados;
- pendientes para el siguiente ciclo.

La intención es que cada sesión deje una huella ordenada y útil, sin depender únicamente del historial del chat.

## Convención de uso

Cada entrada diaria debe mantener esta estructura:

```md
## YYYY-MM-DD - <tema corto>

### Resumen

### Cambios / análisis por área

### Validaciones

### Riesgos / what-if pendientes

### Próximos pasos

### Links / artefactos
```

Reglas:

- Mantener entradas breves pero accionables.
- Separar BOF, BD, API, HH y Brain cuando aplique.
- Registrar comandos importantes de validación.
- Registrar si hubo commit/push y a qué branch.
- No incluir credenciales ni datos sensibles.
- No usar este archivo para reemplazar `RESULT.md`; es una bitácora complementaria.

## 2026-05-19 - Regularización VERI y prevención BOF

### Resumen

Se analizó el caso de duplicación y diferencias de movimientos `VERI` en picking/verificación BOF.

El caso base fue el picking `1465`, validado contra el archivo del cliente `RevisionVerificacionDoble.xlsx`. Se confirmaron dos patrones:

1. Duplicados exactos `VERI`.
2. Movimientos `VERI` grabados en cantidad de presentación en vez de UMBAS.

También se preparó un parche preventivo en BOF para evitar recurrencia.

### Cambios / análisis por área

#### BOF

- Se analizó `frmPicking`:
  - `mnuProcesarLinea_ItemClick`;
  - `Process_Linea_Picking`;
  - `cmdNoVerificado_ItemClick`;
  - `Linea_No_Verificada`;
  - `mnuVerificarPickeados`.
- Se analizó `clsLnTrans_picking_ubic`:
  - `Actualizar_Picking`;
  - `Procesar_Picking_Desde_BOF`;
  - `Procesar_Verificacion_Desde_BOF`;
  - `Marcar_Linea_No_Verificada`;
  - `Marcar_Linea_No_Pickeada`.
- Se analizó `clsLnTrans_movimientos`:
  - `Insertar_Movimiento_Verificacion`;
  - `Eliminar_Movimiento_Verificacion_By_PickingUbic`.
- Se identificó que `Procesar_Verificacion_Desde_BOF` enviaba `Cantidad_Recibida` directa a `trans_movimientos.cantidad`.
- Se confirmó que, cuando existe presentación, `Cantidad_Recibida` puede estar en presentación, mientras que `trans_movimientos.cantidad` debe quedar en UMBAS.
- Se aplicó parche local preventivo:
  - conversión de cantidad `VERI` a UMBAS antes del insert;
  - validación idempotente antes de insertar `VERI`;
  - limpieza de `VERI` al marcar línea como no verificada, también en flujo BOF/manual.

Archivos BOF modificados localmente:

- `TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb`

#### BD

- Se cruzó el archivo del cliente contra `vw_movimientos`.
- Se confirmó que los movimientos del archivo existen y coinciden con BD local.
- Se identificaron candidatos de duplicado exacto.
- Se identificaron movimientos `VERI` con cantidad en presentación en vez de UMBAS.
- Se prepararon scripts:
  - `SCRIPT_AUDITORIA_REGULARIZACION_VERI_DUPLICADOS.sql`;
  - `SCRIPT_AUDITORIA_REGULARIZACION_VERI_CANTIDAD_UMBAS.sql`;
  - `SP_REGULARIZACION_VERI.sql`.
- Se crearon/probaron SPs en BD local:
  - `dbo.usp_WMS_VERI_RegularizarDuplicadosExactos`;
  - `dbo.usp_WMS_VERI_RegularizarCantidadUmbas`;
  - `dbo.usp_WMS_VERI_PostCheck`.

#### API

- No hubo cambios nuevos de API en esta etapa.
- Quedan cambios previos no relacionados con este caso en endpoint MI3 `mi3/di-estatus`.

#### HH

- No hubo cambios nuevos de HH en esta etapa.
- Queda como antecedente el ajuste previo de cantidad por presentación en recepción HH.

#### Brain / documentación

- Se documentó el análisis técnico.
- Se generó guía operativa de regularización.
- Se generaron scripts SQL y SPs.
- Se subió a `wms-brain` el commit:
  - `9073061 [learning 2026-05-19-codex-learning-bof-veri-movimientos-duplicados] docs: regularizacion VERI y scripts SQL`

### Validaciones

- `git -C C:\Users\yejc2\source\repos\wms-brain pull --ff-only`
- Compilación BOF DAL:

```powershell
MSBuild TOMIMSV4\DAL\DAL.vbproj /t:Build /p:Configuration=Debug /p:Platform=AnyCPU
```

Resultado:

```text
DAL.dll compilado correctamente.
```

- SPs probados en BD local para picking `1465`:

```sql
EXEC dbo.usp_WMS_VERI_RegularizarCantidadUmbas @IdPickingEnc = 1465;
EXEC dbo.usp_WMS_VERI_PostCheck @IdPickingEnc = 1465;
```

Resultado observado antes de aplicar corrección UMBAS:

```text
DUPLICADOS_EXACTOS_RESTANTES = 0
MISMATCH_PRESENTACION_RESTANTES = 5
```

### Riesgos / what-if pendientes

- El parche BOF previene nuevos casos, pero no corrige históricos.
- Los históricos deben regularizarse con los SPs preparados.
- `Eliminar_Movimiento_Verificacion_By_PickingUbic` usa la cantidad del movimiento `PIK`; si una `VERI` histórica quedó con cantidad incorrecta, puede no eliminarla. Esto queda cubierto por regularización histórica, pero debe considerarse en validaciones.
- Aplicación global de SPs requiere auditoría previa, respaldo y autorización explícita.
- Hay cambios locales no relacionados detectados en:
  - `TOMIMSV4/TOMIMSV4/Conn_Prograx.ini`;
  - `WSHHRN/TOMHHWS.asmx.vb`.

### Próximos pasos

- Revisar el parche BOF con Erik antes de commit.
- Decidir si se aplica primero en ambiente controlado solo con picking `1465`.
- Ejecutar post-check después de aplicar regularización histórica.
- Si se valida, preparar commit BOF separado del paquete BD/documentación.
- Documentar en Brain Keeper el patrón preventivo final una vez aprobado.

### Links / artefactos

- Análisis cliente:
  - `ANALISIS_CLIENTE_REVISION_VERIFICACION_DOBLE.md`
- Guía operativa:
  - `GUIA_OPERATIVA_REGULARIZACION_VERI.md`
- SPs:
  - `SP_REGULARIZACION_VERI.sql`
- Script duplicados:
  - `SCRIPT_AUDITORIA_REGULARIZACION_VERI_DUPLICADOS.sql`
- Script UMBAS:
  - `SCRIPT_AUDITORIA_REGULARIZACION_VERI_CANTIDAD_UMBAS.sql`

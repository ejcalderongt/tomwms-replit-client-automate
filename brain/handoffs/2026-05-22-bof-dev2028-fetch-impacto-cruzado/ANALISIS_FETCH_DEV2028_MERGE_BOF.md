# Analisis fetch dev_2028_merge BOF e impacto cruzado

Fecha: 2026-05-22  
Repo: TOMWMS_BOF / TOMWMS local  
Rama local: dev_2028_merge  
Remote revisado: origin/dev_2028_merge

## Resultado del fetch

Se ejecuto `git fetch origin dev_2028_merge --prune`.

Resultado de divergencia:

```text
git rev-list --left-right --count HEAD...origin/dev_2028_merge
0 0
```

Conclusion: no hay commits nuevos remotos pendientes de integrar en BOF. La rama local y `origin/dev_2028_merge` estan alineadas. No se hizo pull, merge ni commit.

## Cambios locales pendientes detectados

El worktree local contiene cambios no committeados en:

- `TOMIMSV4/DAL/Transacciones/Movimiento/clsLnTrans_movimientos_Partial.vb`
- `TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb`
- `brain/agents/domain-bof.yml`

Estos cambios corresponden al blindaje del caso VERI:

- Centralizar la normalizacion de cantidad VERI a UMBAS en `Insertar_Movimiento_Verificacion`.
- Permitir que los callers indiquen explicitamente cuando ya envian cantidad en UMBAS.
- Evitar que flujos de verificacion distribuida usen la cantidad original completa cuando la cantidad realmente aplicada a la linea era menor.
- Documentar la regla reusable en `brain/agents/domain-bof.yml`.

## Mejora funcional documentada

Antes, algunos flujos podian dejar `trans_movimientos.cantidad` VERI en presentacion o duplicar el valor logico cuando una cantidad se repartia entre varias filas `trans_picking_ubic`.

Ahora:

- `Insertar_Movimiento_Verificacion` recibe opcionalmente `pCantidadEnUmbas`.
- Si `pCantidadEnUmbas=False`, convierte usando factor de `producto_presentacion`.
- Si `pCantidadEnUmbas=True`, respeta la cantidad recibida porque el caller ya hizo la conversion.
- En los flujos distribuidos se pasa `CantPendiente` / `PesoPendiente`, no `pCantidad` / `pPeso` original.

## Impacto cruzado

### BOF / DAL

Impacto directo:

- `clsLnTrans_movimientos.Insertar_Movimiento_Verificacion`
- `clsLnTrans_picking_ubic.Actualizar_Picking`
- `clsLnTrans_picking_ubic.Actualizar_Picking_Desde_Consolidado`
- `clsLnTrans_picking_ubic.Actualizar_PickingUbic_Por_Verificacion`
- Flujos auxiliares de actualizacion de cantidad/peso de verificacion.

No cambia contrato publico porque el parametro nuevo es opcional y se agrego en DAL interno.

### WebMethods / WSHHRN

No hay cambio de firma en WebMethods. Brain muestra impacto en rutas expuestas que terminan usando estos metodos, especialmente:

- `Actualizar_Picking`
- `Actualizar_PickingUbic_Por_Verificacion`
- `Marcar_Danado`
- `Reemplazar_ListPickingUbic_Verificacion`

Como no hay cambio de contrato SOAP, no se debe tocar `Reference.vb`.

### HH Android

Impacto funcional indirecto:

- `frm_picking_datos.java` llama `Actualizar_Picking`.
- `frm_list_prod_reemplazo_verif.java` llama `Reemplazar_ListPickingUbic_Verificacion` y `Marcar_Danado`.

No se requiere ajuste en HH por este cambio BOF; el comportamiento corregido ocurre del lado DAL/BOF.

### Base de datos

Tablas funcionalmente impactadas por resultado:

- `trans_movimientos`: cantidad VERI debe quedar en UMBAS.
- `trans_picking_ubic`: la cantidad/peso aplicada por verificacion debe corresponder al saldo real de la linea.

No hay cambio de schema ni SP.

## Riesgos y controles

- Si un caller nuevo pasa cantidad en UMBAS pero no marca `pCantidadEnUmbas=True`, se podria multiplicar por factor dos veces. Regla: solo pasar `True` cuando el valor ya viene en UMBAS.
- Si un factor de presentacion es 0 o no existe, el helper conserva la cantidad original redondeada. Ese caso debe investigarse como dato maestro.
- Brain no encontro `Insertar_Movimiento_Verificacion` por nombre, asi que el analisis de callers para ese metodo se hizo con `rg` local. Brain si confirmo rutas cross-layer para WebMethods relacionados.

## Validacion local

Comando ejecutado:

```powershell
dotnet build TOMIMSV4\DAL\DAL.vbproj --no-restore
```

Resultado: compilacion correcta, 0 warnings, 0 errores.

## Estado recomendado

No hay cambios remotos nuevos que integrar. El patch local de VERI queda listo para revision de Erik. Antes de commit/publicacion conviene correr el post-check del caso VERI:

```sql
EXEC dbo.usp_WMS_VERI_PostCheck @IdPickingEnc = 1465;
```

Resultado esperado:

```text
DUPLICADOS_EXACTOS_RESTANTES = 0
MISMATCH_PRESENTACION_RESTANTES = 0
```

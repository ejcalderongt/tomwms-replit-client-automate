# Picking BOF: habilitación de etiquetas de empaque

Tag: #EJC20261026_PICKING_ETIQUETAS_EMPAQUE
Fecha: 2026-10-26 (America/Guatemala)
Estado: implementado y revisado estáticamente; build bloqueado por error previo ajeno al cambio.
Alcance: TOMWMS_BOF, formulario frmPicking; sin cambios de contrato, SQL, HH ni proxies.

## Regla operativa

La opción Imprimir > Etiquetas de empaque solo se habilita con un picking guardado
(IdPickingEnc > 0) y al menos una configuración marcada en pantalla:
Empaque a granel o Empaque por tarima. Sin ambas configuraciones, queda deshabilitada.
La misma condición se valida antes de abrir frmLicenciaEmpaque.
Se usan las casillas actuales del formulario: marcar/desmarcar actualiza el menú
inmediatamente; este cambio no agrega un guardado automático.

## Causa y traza fina

Archivo compilado: TOMIMSV4/TOMIMSV4/Transacciones/Picking/frmPicking.vb,
incluido por TOMIMSV4/TOMIMSV4/WMS.vbproj.

1. frmPicking_Shown, modo Editar, llama Set_Menu_Edicion antes de Cargar_Datos.
2. Antes, Set_Menu_Edicion habilitaba mnuLicenciasEmpaque solo con IdPickingEnc > 0.
3. Mostrar_Datos_Encabezado carga Tipo_Preparacion en las casillas Granel/Tarima;
   ahora recalcula explícitamente el menú al terminar ambas asignaciones.
4. Los eventos CheckedChanged mantienen exclusión mutua y recalculan el menú.
   La exclusión se ejecuta solo al marcar; antes, el evento anidado de desmarcado
   podía borrar la nueva selección al cambiar Granel <-> Tarima.
5. Al guardar, la lógica existente persiste Requiere_Preparacion como OR de ambas
   casillas y Tipo_Preparacion como Granel/Tarima, o vacío y Estado_Preparacion=N/A
   cuando no se configura empaque. No se modifica esta persistencia.
6. Process_Guardar_Picking vuelve a llamar Set_Menu_Edicion tras guardado exitoso.
7. Puede_Imprimir_Etiquetas_Empaque concentra la regla con cortocircuito para Nothing.
   Actualizar_Menu_Etiquetas_Empaque la aplica a Enabled.
8. mnuLicenciasEmpaque_ItemClick conserva el aviso de picking sin guardar y agrega
   el guard de configuración antes de ShowDialog de frmLicenciaEmpaque.

## Matriz de regresión

| Picking | Granel | Tarima | Opción |
|---|---|---|---|
| Nothing / ID <= 0 | cualquiera | cualquiera | Deshabilitada |
| Guardado | No | No | Deshabilitada; apertura directa bloqueada |
| Guardado | Sí | No | Habilitada |
| Guardado | No | Sí | Habilitada |
| Guardado | Sí -> No | No | Se deshabilita inmediatamente |
| Guardado | Sí -> No | No -> Sí | Tarima queda marcada; habilitada |
| Guardado | No -> Sí | Sí -> No | Granel queda marcada; habilitada |

Revisión estática: carga, guardado, click, cortocircuito y eventos anidados revisados.
Prueba interactiva en BOF: pendiente; no se afirma ejecución de esta matriz en UI.
No se añaden restricciones nuevas por estado del picking ni se alteran las demás
opciones del menú Imprimir.

## Evidencia y límites

- Diff acotado de frmPicking.vb: git diff --check sin errores.
- Archivo VB conservado en UTF-8 con BOM.
- Brain local: brain/tools/lint-brain.ps1 válido (8 rutas requeridas).
- Compilación WMS.vbproj Debug/AnyCPU: exit 1; ver bof-build.log. Error previo en
  frmLicenciaEmpaque.vb:385, carácter | antes de For (BC30037); produce también
  BC30092 en Next, línea 401. El diff de ese archivo ya existía en el preflight.
  Se conserva sin modificar. No se declara compilación exitosa ni binario validado.
- Brain remoto: brainctl doctor devuelve HTTP 404; token de importación ausente.
  Reindexación remota no realizada; documentación y referencias locales disponibles.
- Cambios previos del usuario en otros archivos preservados. Sin commits automáticos.

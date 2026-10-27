# #EJC20261026_PICKING_ACTUALIZAR_FLAGS_AUDIT

Alcance: validación, sin modificación de código funcional.

Ruta: frmPicking.mnuActualizar_ItemClick:2702 -> Guardar_PickingAsync:701 ->
clsLnTrans_picking_enc.Guardar (6 argumentos) -> Guarda_Trans_picking_enc ->
Actualizar:164. UPDATE incluye requiere_preparacion, tipo_preparacion,
estado_preparacion, fechas de preparación, confirmar_packing_entrega y
fotografia_verificacion. GetSingle vuelve a mapear esos campos al objeto.

Prueba con DAL compilado sobre MHS-DEV, picking88/bodega7:
validar-flags-picking.ps1 en DBA/migrations/20261026_mhs_tarimas_completas/.
UPDATE+GetSingle: sin empaque, Granel, Tarima y combinaciones de confirmar entrega/
fotografía: PASS. Cada UPDATE afectó una fila; rollback final verificó flags originales.
Estado inicial observado: requiere=True, tipo=Tarima, estado=Nuevo, confirmar=True.
Evidencia: validacion-flags-picking.log. No se pulsó la interfaz en esta prueba.

Hallazgos por revisión de código:
1. frmPicking:762-772 reinicia fechas a 1900-01-01 y Estado_Preparacion a Nuevo
   en toda actualización con empaque, incluso si ya había avance de packing.
2. mnuActualizar_ItemClick ignora Boolean de Guardar_PickingAsync y no usa
   Try/Finally: una salida False no se distingue en ese handler y una excepción
   deja el botón deshabilitado.
3. mnuPendientePacking.Enabled depende del objeto en Set_Menu_Edicion:383, pero
   ese menú no se recalcula después del handler Actualizar; puede quedar obsoleto.
   Etiquetas de empaque sí se recalcula en carga/cambio de casillas.
4. Actualizar lista de picking (mnuActualizarPicking) solo llama Cargar_Datos;
   recarga, no guarda cambios de casillas.

Conclusión: persistencia DAL correcta para los flags; flujo de edición no es
completamente correcto porque puede reiniciar el estado y las fechas de preparación.

## Corrección aplicada #EJC20261026_PICKING_ACTUALIZAR_EMPAQUE

Los hallazgos anteriores describen la versión previa al fix.

- Guardar_PickingAsync captura los flags y el tipo solicitado sin reiniciar
  estado ni fechas de preparación.
- Guarda_Trans_picking_enc llama Preparar_Configuracion_Empaque dentro de la
  transacción existente, leyendo el encabezado con UPDLOCK/HOLDLOCK.
- Si no cambia Requiere_Preparacion/Tipo_Preparacion, copia estado y fechas
  vigentes desde BD, conservando incluso avances HH posteriores a la carga.
- Picking nuevo o cambio sin avance: inicializa Nuevo/N/A y fechas sentinel.
- Cambio de configuración con fechas, estado avanzado o filas trans_packing_enc:
  rechaza, no borra ni reabre la preparación.
- Actualizar ejecuta Valida_Datos, comprueba Boolean, presenta error, usa Finally
  para recuperar disponibilidad y recalcula Set_Menu_Edicion al guardar.
- Actualizar lista de picking conserva su función de recarga.

Pruebas con DLL real y rollback en DEV, picking88: conserva Finalizado/fechas
ante snapshot obsoleto; guarda otros flags; bloquea Tarima->Granel con avance;
permite desactivar, Granel y Tarima sin avance; rollback conserva valores originales.
Script: DBA/migrations/20261026_mhs_tarimas_completas/validar-fix-actualizar-picking.ps1.
Resultado: fix-actualizar-tests.log (PASS).

WSHHRN compilación PASS: fix-actualizar-ws-build.log.
BOF en carpeta habitual: bloqueo de DAL.dll por Visual Studio y TOMWMS abiertos;
no se cerraron procesos del usuario. Compilación alternativa con OutDir separado:
fix-actualizar-build-aislado.log. Los binarios cargados en la sesión abierta no se
actualizan hasta reiniciar/recompilar fuera de depuración.
No migración adicional, no cambio de firmas públicas, no proxies/HH ni commits.
No prueba interactiva. Brain remoto sigue no disponible (404, sin token).

# NavSync: todos los formularios fallan en diseñador

#EJC20261029_NAVSYNC_DESIGNER_CACHE

Rama `codex/byb-unidad-medida`, checkout principal TOMWMS_BOF.

Reporte: diseñador rechaza frmEjecucion con base System.Void; Carolina confirma que afecta a todas las pantallas del proyecto.

## Evidencia

- NavSync.vbproj, frmEjecucion.vb y frmEjecucion.Designer.vb no difieren respecto a dev_2026_estable_prd. No hay evidencia de que el traslado de worktree haya cambiado la herencia.
- Proyecto referencia DevExpress 26.1.4.0.
- `DynamicsNavInterface/obj/Debug/DesignTimeResolveAssemblyReferences.cache` tiene fecha 24-agosto-2026 y referencias DevExpress.Data/Data.Desktop/Drawing 25.2.7.0.
- La caché de entrada del diseñador ya tiene referencias 26.1. Inconsistencia entre caché de salida antigua y entradas/proyecto actuales.

## Acción

Compilación normal de NavSync y dependencias exit 0; binarios locales actualizados en sus salidas habituales. Sin ejecutar NavSync.

La única caché de diseño obsoleta se trasladó, con comprobación de ruta y sin sobrescribir respaldo, a `analysis/navsync-designer-cache-backup-20261029/Debug-DesignTimeResolveAssemblyReferences.cache`.

No se cambió código de formularios, herencia, proyecto, configuración de conexión ni propiedades del diseñador. No se activó el diseñador fuera de proceso. No se eliminó .vs global ni cachés de otros proyectos. Después de la instrucción de no controlar pantalla, no se utilizaron herramientas de control de aplicaciones.

## Verificación y límite

`check-navsync-form-types-20261029.ps1` ejecutado en Windows PowerShell .NET Framework: ocho clases de formulario cargan con base `DevExpress.XtraBars.Ribbon.RibbonForm`; ninguna base real System.Void. Solo reflexión, sin instanciar formularios ni mostrar ventanas ni ejecutar consultas.

Registro compilación: `analysis/navsync-designer-build.log`.

El diseñador visual aún necesita validación por Carolina: cerrar/reabrir la solución TOMWMS y abrir un formulario. Visual Studio puede conservar su estado en memoria hasta recargar; no se cerró ni reinició su sesión. La caché incompatible es un hallazgo concreto, pero no se confirmó visualmente que sea la única causa.

## Persistencia y segundo ajuste

Carolina confirmó que retirar caché no resolvió el fallo. No considerar esa limpieza una solución confirmada. Su traza muestra `CodeDomDesignerLoader.EnsureDocument` / `VSCodeDomDesignerLoader.PerformLoad`: falla en carga/interpretación del documento. La traza no demuestra fallo de constructor ni de base de datos.

Evaluación `Compile` con DesignTimeBuild=true, BuildingProject=false, SkipCompilerExecution=true y ProvideCommandLineArgs=true: exit 0. Diagnóstico en `navsync-designtime-diagnostic.log`. Los tipos de diseñador FormDocumentDesigner y ScrollableControlDesigner resuelven para las ocho clases.

Ajuste de compatibilidad pendiente de validación visual: mover la misma declaración Inherits RibbonForm de los ocho archivos Designer.vb a sus ocho archivos principales, como `Public Partial Class ... / Inherits Global.DevExpress.XtraBars.Ribbon.RibbonForm`. No cambiar el contenido de InitializeComponent ni los controles/eventos/recursos/constructores. Script de aplicación: `fix-navsync-codedom-20261029.ps1`.

Compilación normal tras el ajuste: exit 0 (`navsync-codedom-build.log`). Ocho bases compiladas siguen siendo DevExpress.XtraBars.Ribbon.RibbonForm y diff --check correcto. No hay acceso de control de pantalla. Carolina debe cerrar la pestaña del diseñador con error y volver a abrirla para confirmar; compilación y reflexión no confirman por sí solas el comportamiento de CodeDom dentro de Visual Studio.

## Ajuste de herencia descartado; reinicialización completa

Carolina confirmó que el ajuste tampoco resolvió el problema y que ya había reiniciado Visual Studio. Se revirtió únicamente el movimiento de herencia, con comprobación de patrones; los 16 archivos de formularios vuelven a coincidir con HEAD. Las correcciones funcionales de NavSync/Pedido/Picking/eliminación permanecen intactas.

Carolina cerró Visual Studio de nuevo y autorizó continuar mediante el aviso de cierre. Se comprobó ausencia de procesos devenv antes de operar. Se trasladaron, sin borrar y con comprobación de rutas, `.vs` de TOMWMS_BOF y `DynamicsNavInterface/obj` a `analysis/navsync-solution-cache-backup-20261029-163941/`. Se verificó que obj no contiene archivos versionados. Es un respaldo recuperable del estado local de la solución; puede recuperar su configuración de sesión si hace falta, sin restaurar indiscriminadamente cachés obsoletas.

Rebuild normal de NavSync y dependencias tras retirar obj: exit 0, registro `navsync-fresh-cache-build.log`. No se alteró instalación/configuración global de Visual Studio ni se cambió de diseñador clásico a fuera de proceso. No hubo control de aplicaciones. Pendiente confirmar apertura visual del formulario tras abrir la solución principal con caché nueva; no afirmar solución definitiva todavía.

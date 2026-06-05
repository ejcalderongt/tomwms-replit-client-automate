# Fine Trace `frmDiseñoB` (Graficador de Bodega)
Fecha: 2026-06-03
Scope: `TOMIMSV4/TOMIMSV4/Mantenimientos/Bodega_Graficador/frmDiseñoB.vb`

## 1) Objetivo funcional actual
- Renderizar en tiempo de ejecucion el layout fisico de bodega (sector -> tramo -> ubicaciones).
- Permitir inspeccion visual por click de ubicacion y apertura de detalle (`frmColumViewervb`).
- Superponer ocupacion/stock para resaltar ubicaciones con existencias.

## 2) Modelo de datos canonico (jerarquia)
- `bodega` -> entidad raiz (`clsBeBodega`), incluye medidas (`Largo`, `Ancho`, `Zoom`) y colecciones hijas.
- `bodega_area` -> agrupador logico superior (`clsBeBodega_area`).
- `bodega_sector` -> bloque visual con coordenadas absolutas (`Pos_x`, `Pos_y`, `Horizontal`, `Largo`, `Ancho`).
- `bodega_tramo` -> rack/pasillo dentro de sector (`Margen_*`, `Orientacion`, `IdTipoRack`, `Es_Rack`, `Horizontal`, `Orden_Descendente`).
- `bodega_ubicacion` -> celda final (`Indice_x`, `Nivel`, `Orientacion_pos`, `Largo`, `Ancho`, `Posicion_X`, `Posicion_Y`).

## 3) Carga de estructura (DAL fine path)
`frmDiseñoB.Get_Parametros_Bodega()` llama:
- `clsLnBodega.Get_Estructura_By_IdBodega(BeBodega)`
  - `Cargar_Estructura(...)` llena:
    - `Areas = clsLnBodega_area.Get_All_By_IdBodega(...)`
    - `Sectores = GetSectores(Areas, ...)`
    - `Tramos = GetTramos(Sectores, ...)`
    - `Ubicaciones = GetUbicaciones(Tramos, ...)`

En runtime, el render trabaja sobre `BeBodega` ya materializada en memoria.

## 4) Pipeline de dibujo runtime (UI)
Entrada:
- `Dibujar_Bodega(Get_Data:=True/False)`

Etapas:
1. `Get_Parametros_Bodega()` (si `Get_Data=True`)
2. `Ajustar_Zoom()`:
   - escala `PanBodega`/`PanBorde` con `txtLargo`, `txtAncho`, `Zoom`.
3. `Dibujar_Sectores()`:
   - limpia panel (`Clear_Panel`),
   - crea `Panel` por sector (`Left=Pos_x*Zoom`, `Top=Pos_y*Zoom`),
   - filtra tramos por sector y llama `Dibujar_Tramos(...)`.
4. `Dibujar_Tramos(...)`:
   - crea `Panel` por tramo en coordenadas relativas de sector (`Margen_* * Zoom`),
   - calcula nivel grafico base (`clsLnBodega_tramo.NivelGrafico`),
   - filtra ubicaciones visibles por `IdTramo+Nivel+Area+Sector+Bodega`,
   - enruta por tipo:
     - rack horizontal: `Dibujar_GeoEspacialmente(...)`
     - rack no horizontal: `Dibujar_Ubicacion(...)`
     - piso: `Dibujar_Ubicacion_Piso_Horizontal/Vertical(...)`
5. Pintado de celdas (`LabelControl`):
   - set de bounds, texto (`Indice_x + letra`), color y tooltip.
   - binding ubicacion por `Tag = IdUbicacion`.
   - click -> `lblUbicacion_Click`.

## 5) Reglas de dibujo (core geometrico)
- `IdTipoRack` determina cuantas caras se pintan:
  - 1: una cara
  - 2: dos caras (horizontal)
  - 3: dos caras (vertical)
  - 4: cuatro caras
- `Orientacion` del tramo + `Orientacion_pos` de ubicacion hacen el match final de cara (`A/B/C/D`, `FL/FR/BL/BR`, `1/2/3/4`).
- `chkInvertir` o `Orden_Descendente` invierte secuencia de columnas (`Indice_x`) en render.
- Si una ubicacion no encuentra match para la cara, la etiqueta se oculta (`Visible=False`).

## 6) Stock overlay (superposicion operativa)
- `cmdGetStock`:
  - `Cargar_Stock()` -> `clsLnStock.Get_Inventario_Stock(IdBodega)`
  - `Dibujar_Stock()`:
    - busca ubicaciones ya visibles y las pinta (`CheckLabels`),
    - resuelve ubicaciones no visibles via `clsLnBodega_ubicacion.GetSingleWithTramoAndSector`,
    - fallback para nivel 1 en misma columna/tramo.

## 7) Interaccion actual
- Click ubicacion:
  - toggle de color local,
  - carga `clsBeBodega_ubicacion` completa,
  - abre `frmColumViewervb` para detalle/stock puntual.
- Click sector:
  - muestra info tecnica del panel.
- Export:
  - `PanBodega.DrawToBitmap` -> PNG.

## 8) Hallazgos tecnicos (estado actual)
- Renderizado altamente manual por control WinForms (`Panel` + `LabelControl`).
- Muchas reglas de posicion acopladas a condicionales por `IdTipoRack/Orientacion`.
- El algoritmo mezcla:
  - geometria base,
  - resolucion de entidad,
  - estilo visual,
  - interaccion UI.
- Esto complica mantenibilidad y evolucion hacia UX avanzada.

## 9) Blueprint "UI visual elite WMS" (meta/sueno)
Objetivo: mantener semantica actual pero evolucionar a motor declarativo, rapido y auditable.

### Fase 1: Geometry Engine (sin cambiar UX)
- Crear DTO plano de render:
  - `SectorRect`, `TramoRect`, `SlotRect` con coordenadas finales y metadata (`IdUbicacion`, `Estado`, `Capacidad`).
- Separar calculo (engine puro) de pintado (WinForms).
- Resultado: test unitario de geometria sin UI.

### Fase 2: Scene Graph + capas
- Capas:
  - `Layout` (estructura fija),
  - `Capacity` (ocupacion),
  - `Task` (tareas HH),
  - `Alerts` (bloqueos, merma, dano).
- Colores por paleta semantica centralizada, no hardcode disperso.

### Fase 3: Interaccion operativa pro
- Zoom/pan fluido y mini-map.
- Tooltip rico: ubicacion, LP, estado producto, capacidad, ult mov.
- Click contextual: abrir stock, historial, tareas, auditoria.
- Busqueda unificada por `Ubicacion`, `LP`, `SKU`, `Lote`.

### Fase 4: Telemetria y performance
- Trazas por etapa (`load_model`, `compute_geometry`, `paint_scene`, `overlay_stock`).
- Cache invalidador por version de estructura de bodega.
- Render incremental por sector/tramo (virtualizacion).

## 10) Recomendacion de implementacion en este repo
1. No reemplazar `frmDiseñoB` de una vez; envolver logica actual con servicio `LayoutRenderService`.
2. Extraer primero el mapeo `bodega_ubicacion -> SlotRect` (fase de menor riesgo).
3. Añadir pruebas de paridad visual por snapshots (muestra de 2-3 bodegas reales).
4. Cuando haya paridad, activar nuevo renderer por feature flag (`TOMWMS_BODEGA_RENDER_V2=1`).

## 11) Referencias de codigo clave
- `frmDiseñoB.Dibujar_Bodega` / `Dibujar_Sectores` / `Dibujar_Tramos`
- `frmDiseñoB.Dibujar_GeoEspacialmente`
- `frmDiseñoB.Dibujar_Ubicacion`
- `frmDiseñoB.Dibujar_Ubicacion_Piso_Horizontal/Vertical`
- `clsLnBodega.Get_Estructura_By_IdBodega`
- `clsLnBodega.Cargar_Estructura`

## 12) Nuevo baseline V2 (forma separada)
Se agrego una forma nueva y separada para evolucionar UX sin riesgo sobre `frmDiseñoB`:
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Bodega_Graficador/frmDisenoB_V2.vb`
- incluida en `WMS.vbproj` como `SubType=Form`.

Alcance inicial V2:
- Carga de empresa/bodega con los mismos servicios (`IMS.Listar_Empresas`, `AP.Listar_Bodegas_Login`).
- Carga de estructura real por `clsLnBodega.Get_Estructura_By_IdBodega`.
- Render base reutilizando `clsGraficadorBodega`.
- Trazas `Debug.WriteLine` por etapa:
  - inicio de contexto,
  - validacion de seleccion,
  - tiempo de carga de estructura (`DataMs`),
  - conteos (`Sectores/Tramos/Ubicaciones`),
  - tiempo total de redibujo.

Objetivo de esta base:
- permitir prototipar UI elite (minimap/capas/tooltips avanzados/virtualizacion)
  sin comprometer la operacion actual del graficador legacy.

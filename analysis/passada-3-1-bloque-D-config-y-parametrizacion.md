# Ciclo 3.1 — Bloque D: Configuración y Parametrización (BOF + HH)

## Estado
- **Generado**: 2026-04-27T04:32:46.710Z
- **Fuente datos**: TOMWMS_KILLIOS_PRD (Killios productivo SQL Server 2022, read-only) + TOMWMS_BOF/_git/TOMWMS_BOF (Azure DevOps, branch `dev_2028_merge`)
- **Cobertura**: 38 archivos clsLn de configuración → 24 clases lógicas (cada clase = main + _Partial), 275 métodos públicos, 28 tablas tocadas

## Objetivo del bloque
Identificar dónde vive la **configuración** y **parametrización** del WMS (BOF y HH), qué tablas la respaldan, qué clases la consultan y qué WMs públicos del WSHHRN la usan para alterar comportamiento del HH.

---

## D.1 Tablas y vistas de configuración (catálogo)

Filtré sobre el catálogo Killios (621 objetos, 345 tablas + 220 vistas) los nombres con patrones `*config*`, `i_nav_config*`, `tipo_*`, `rol*`, `menu*`, `valores_fijos*`. Resultado: **31 objetos** (29 tablas + 2 vistas + 0 SPs/funciones).

### Tablas con datos productivos (rows > 0)

| Tabla | Filas | Rol funcional |
|---|---:|---|
| `menu_rol` | 863 | Asigna menús a cada rol del sistema (qué ve cada perfil) |
| `menu_sistema` | 289 | Catálogo maestro de opciones de menú |
| `menu_rol_op` | 187 | Asigna menús del HH (operador) por rol |
| `configuracion_usuario_det` | 141 | **Detalle de configuración por usuario+host** (campos del template) |
| `configuracion_usuario_enc` | 19 | **Encabezado: usuario + host + template XML** (clave para HH) |
| `menu_sistema_op` | 19 | Catálogo de opciones de menú del HH |
| `rol_operador` | 14 | Roles operativos (HH) |
| `rol_usuario_estado` | 14 | Estados permitidos por rol+usuario |
| `tipo_tarima` | 10 | Tipos de tarima (paletas) |
| `rol` | 8 | 8 roles definidos: Admin, Supervisor, Interfaces, CoordinadoresDespacho, Recepcion, etc. |
| `i_nav_config_enc` | 6 | **Config global de interface por empresa+bodega** (parámetros de negocio) |
| `tipo_etiqueta` | 6 | Tipos de etiqueta para impresión |
| `tipo_contenedor` | 5 | Tipos de contenedor |
| `tipo_rack` | 4 | Tipos de rack |
| `tipo_rotacion` | 4 | Tipos de rotación (FIFO/LIFO/FEFO/etc) |
| `tipo_actualizacion_costo` | 3 | Tipos de actualización de costo |
| `valores_fijos_reporte_mercancias` | 3 | Valores fijos para reportes |
| `tipo_tarea_tiempos` | 1 | Tiempos por tipo de tarea |
| `configuracion_barra_pallet` | 1 | Config visual de barras de pallet |

### Tablas vacías (esquema sin datos en Killios — posibles para otros clientes)

`configuracion_alias_campos`, `configuracion_qa`, `i_nav_config_area_bodega`, `i_nav_config_det`, `i_nav_config_ent`, `i_nav_config_producto_estado`, `rol_bodega`, `rol_menu`, `tipo_etiqueta_detalle`.

> Estas tablas existen en el schema pero el cliente Killios no las usa. Pueden estar pobladas en BYB/Cealsa.

### Vistas relevantes

- `VW_Configuracion_Usuario_Template` — agrega encabezado + detalle de la config por usuario, expone el template a aplicar.
- `VW_Configuracioninv` — vista de configuración de inventario.
- `VW_navdetalleconfiguracion` — detalle navigable de la configuración de interfaces.

---

## D.2 Mapa de campos clave (valores reales, sanitizados)

### `configuracion_usuario_enc` (19 filas, núcleo del HH-por-host)

Columnas: `IdConfiguracionUsuarioEnc`, `IdEmpresa`, `IdUsuario`.

Sample (3 filas, hosts y templates reales):

| IdConfiguracion | IdUsuario | Maquina_Host | Nombre_Template | activo |
|---:|---:|---|---|---|
| - | 12 | `-` | `-` | - |
| - | 7 | `-` | `-` | - |
| - | 1 | `-` | `-` | - |
| - | 37 | `-` | `-` | - |
| - | 34 | `-` | `-` | - |
| - | 39 | `-` | `-` | - |

> Confirma la regla operativa: **la configuración del HH se identifica por `Usuario + Maquina_Host`** (no por sesión web). Cada PC física donde corre el HH tiene su propia config y un template XML asociado (ej. `frmIngreso_List.xml`, `frmDespacho_List.xml`).

### `rol` (8 filas, los perfiles del WMS)

| IdRol | Nombre | Activo | Requiere clave autorización |
|---:|---|---|---|
| 1 | Administrador | true | false |
| 2 | Jefe de Bodega | true | false |
| 3 | Asistente de Bodega | true | true |
| 4 | Verificador | true | false |
| 5 | Supervisor | true | false |
| 6 | Interfaces | true | false |
| 7 | CoordinadoresDespacho | true | false |
| 8 | Recepcion | true | false |

### `i_nav_config_enc` (6 filas, parámetros globales de interface)

Columnas: `idnavconfigenc`, `idempresa`, `idbodega`, `idPropietario`, `idUsuario`, `nombre`, `fec_agr`, `user_agr`, `fec_mod`, `user_mod`, `IdProductoEstado`, `rechazar_pedido_incompleto`, `despachar_existencia_parcial`, `convertir_decimales_a_umbas`, `generar_pedido_ingreso_bodega_destino`, `generar_recepcion_auto_bodega_destino`, `codigo_proveedor_produccion`, `idFamilia`, `idclasificacion`, `idMarca`, `idTipoProducto`, `control_lote`, `control_vencimiento`, `genera_lp`, `nombre_ejecutable`, `IdTipoDocumentoTransferenciasIngreso`, `crear_recepcion_de_transferencia_nav`, `IdTipoEtiqueta`, `equiparar_cliente_con_propietario_en_doc_salida`, `control_peso`, `crear_recepcion_de_compra_nav`, `IdAcuerdoEnc`, `push_ingreso_nav_desde_hh`, `reservar_umbas_primero`, `implosion_automatica`, `explosion_automatica`, `Ejecutar_En_Despacho_Automaticamente`, `IdTipoRotacion`, `explosio_automatica_nivel_max`, `explosion_automatica_desde_ubicacion_picking`, `explosion_automatica_nivel_max`, `conservar_zona_picking_clavaud`, `excluir_ubicaciones_reabasto`, `considerar_paletizado_en_reabasto`, `considerar_disponibilidad_ubicacion_reabasto`, `dias_vida_defecto_perecederos`, `codigo_bodega_nc_erp`, `lote_defecto_nc`, `vence_defecto_nc`, `Codigo_Bodega_ERP_NC`, `Lote_Defecto_Entrada_NC`, `IdProductoEstado_NC`, `interface_sap`, `sap_control_draft_ajustes`, `sap_control_draft_traslados`, `IdIndiceRotacion`, `Rango_Dias_Importacion`, `inferir_bonificacion_pedido_sap`, `rechazar_bonificacion_incompleta`, `equiparar_productos`, `bodega_facturacion`, `valida_solo_codigo`, `excluir_recepcion_picking`, `bodega_prorrateo`, `bodega_prorrateo1`, `centro_costo_erp`, `centro_costo_dir_erp`, `centro_costo_dep_erp`, `bodega_faltante`.

Total 6 configuraciones, una por combinación empresa+bodega+interface. Estos son los **flags de comportamiento** que el HH consulta para alterar su flujo (ej. ¿genera histórico al recibir?, ¿qué producto es 'buen estado' por defecto?).

---

## D.3 Clases DAL de configuración (`/TOMIMSV4/DAL/`, 38 archivos → 24 clases)

### Top 12 por superficie (métodos + tablas tocadas)

| Clase | Métodos | Tablas | Vistas | Tablas tocadas |
|---|---:|---:|---:|---|
| `clsLnI_nav_config_enc` | 24 | 1 | 1 | `i_nav_config_enc` |
| `clsLnRol` | 18 | 2 | 0 | `rol`, `menu_sistema` |
| `clsLnRol_operador` | 15 | 3 | 0 | `rol_operador`, `menu_sistema_op`, `menu_rol_op` |
| `clsLnTipo_tarea_tiempos` | 12 | 4 | 0 | `tipo_tarea_tiempos`, `empresa`, `bodega`, `sis_tipo_tarea` |
| `clsLnI_nav_config_det` | 16 | 2 | 1 | `i_nav_config_det`, `i_nav_ent` |
| `clsLnI_nav_config_ent` | 13 | 2 | 0 | `i_nav_config_ent`, `i_nav_ent` |
| `clsLnMenu_sistema` | 13 | 2 | 0 | `menu_sistema`, `menu_rol` |
| `clsLnTipo_etiqueta` | 13 | 2 | 0 | `tipo_etiqueta`, `producto_clasificacion_etiqueta` |
| `clsLnMenu_rol_op` | 11 | 2 | 0 | `menu_rol_op`, `menu_sistema_op` |
| `clsLnTipo_contenedor` | 13 | 1 | 0 | `tipo_contenedor` |
| `clsLnTipo_tarima` | 13 | 1 | 0 | `tipo_tarima` |
| `clsLnConfiguracion_usuario_enc` | 12 | 1 | 0 | `configuracion_usuario_enc` |

### Convención observada

- Cada tabla de config tiene par `clsLn<X>.vb` (auto-generado por DevExpress XPO/CRUD) + `clsLn<X>_Partial.vb` (extensiones a mano).
- Los `_partial` aportan típicamente 2-5 métodos custom (lookups por host, validaciones, helpers).
- 24 clases lógicas, 275 métodos públicos, 28 tablas distintas en queries SQL inline (consistente con hallazgo de ciclo 3.1: WMS NO usa SPs).

---

## D.4 Cruce con WSHHRN — qué WMs leen config

Sobre los **344 WMs** del WSHHRN parseados en bloque E, **solo 10 (2.9%) consultan ≥1 clsLn de configuración directamente**:

| Veces invocada | Clase config | Tabla principal |
|---:|---|---|
| 3 | `clsLnI_nav_config_enc` | `i_nav_config_enc` |
| 2 | `clsLnMenu_rol_op` | `menu_rol_op` |
| 2 | `clsLnRol_operador` | `rol_operador` |
| 2 | `clsLnTipo_etiqueta` | `tipo_etiqueta` |
| 1 | `clsLnConfiguracion_barra_pallet` | `configuracion_barra_pallet` |

### Los 10 WMs config-related

| Línea | WebMethod | Clase config invocada |
|---:|---|---|
| 764 | `Get_Menu_Rol_Op_For_HH` | `clsLnMenu_rol_op` |
| 1030 | `Get_Nombre_Rol_Operador_For_HH_By_IdRolOperador` | `clsLnRol_operador` |
| 1077 | `Get_Menu_By_IdRolOperador_For_HH` | `clsLnMenu_rol_op` |
| 1492 | `Get_BeRolOPerador_By_IdRolOperador` | `clsLnRol_operador` |
| 8949 | `Get_Tipo_Etiqueta_By_IdTipoEtiqueta` | `clsLnTipo_etiqueta` |
| 10350 | `Get_Configuracion_Barra_Pallet_By_IdConfiguracion` | `clsLnConfiguracion_barra_pallet` |
| 11207 | `Get_IdProductoBuenEstado_Por_Defecto_By_IdBodega_And_IdEmpresa` | `clsLnI_nav_config_enc` |
| 11257 | `Get_Recepcion_Genera_Historico` | `clsLnI_nav_config_enc` |
| 16289 | `Finalizar_Recepcion_S` | `clsLnI_nav_config_enc` |
| 18628 | `Get_TipoEtiqueta_By_Id` | `clsLnTipo_etiqueta` |

### Hallazgos del cruce

**1. `configuracion_usuario_*` NO se consulta vía WSHHRN.**

Aunque la tabla tiene 19 filas con `Maquina_Host`/`Nombre_Template`, ningún WM del WSHHRN lee `clsLnConfiguracion_usuario_enc` o `_det`. Implicación: el flujo template-por-host es **del lado BOF**, no del HH. El HH recibe del servicio web sólo lo que el BOF ya resolvió, o lo aplica al imprimir/exportar (templates XML cargados localmente en la PC del operador).

**2. `i_nav_config_enc` es la verdadera fuente de comportamiento del HH.**

3 de los 10 WMs config-related leen `i_nav_config_enc`. Ejemplos concretos:

- `Get_IdProductoBuenEstado_Por_Defecto_By_IdBodega_And_IdEmpresa` (línea 11207) — qué producto se considera "Buen Estado" en cada bodega.
- `Get_Recepcion_Genera_Historico` (línea 11257) — flag booleano que decide si la recepción genera histórico.
- `Finalizar_Recepcion_S` (línea 16289) — orquestación crítica: durante el finalizado de recepción consulta i_nav_config_enc para decidir flujo.

→ **El Brain debe modelar i_nav_config_enc como tabla de feature flags / parámetros de negocio por bodega+empresa.** No como un blob opaco — cada columna de esa tabla representa una regla de negocio activable.

**3. Permisos (`menu_rol_op`, `rol_operador`) se consultan al login.**

- `Get_Menu_Rol_Op_For_HH` (línea 764) — devuelve el menú permitido al operador después de login.
- `Get_Menu_By_IdRolOperador_For_HH` (línea 1077) — variante por id rol.
- `Get_Nombre_Rol_Operador_By_IdRolOperador` (línea 1030) — nombre amigable del rol.

→ El HH **NO** carga catálogos de tipos (`tipo_etiqueta`, `tipo_contenedor`, etc) al inicio salvo cuando un flujo lo requiere puntualmente.

---

## D.5 Implicaciones para el Brain

1. **Dos cabezas, dos fuentes de config:**
   - **BOF** consulta `configuracion_usuario_enc/det` para resolver template XML por usuario+host (impresión, layouts, exportaciones).
   - **HH** consulta `i_nav_config_enc` para feature flags de negocio (qué hacer, no cómo mostrarlo).

2. **Brain debe exponer `i_nav_config_enc` como entidad de primera clase** — cada flag (ej. `Recepcion_Genera_Historico`, `IdProductoBuenEstadoDefault`) es una regla parametrizable.

3. **Permisos = `rol_operador` × `menu_rol_op`.** Brain puede modelar control de acceso del HH listando los menús permitidos por rol.

4. **Catálogos de tipos** (`tipo_*`, 7 tablas con 33 filas totales) son enums pequeños que el HH consulta on-demand. Pueden cachearse.

5. **Tablas vacías en Killios pueden estar pobladas en BYB/Cealsa.** Si el Brain debe ser multi-cliente, modelarlas igual aunque acá no se usen.

## Anexo: archivos generados por este bloque

| Archivo | Contenido |
|---|---|
| `data/passada-3-1-bloque-D-config-objects.json` | 31 objetos config agrupados |
| `data/passada-3-1-bloque-D-values.json` | Columnas + sample de filas reales (sanitizado) de 19 tablas |
| `data/passada-3-1-bloque-D-parsed.json` | Métodos+tablas de cada clsLn config + cruce con WMs |
| `analysis/passada-3-1-bloque-D-config-y-parametrizacion.md` | Este documento |
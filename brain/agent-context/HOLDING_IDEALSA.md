---
id: HOLDING_IDEALSA
tipo: agent-context
estado: vigente
holding: idealsa
clientes: [mercopan, merhonsa, idealsa]
filiales: [mercopan, merhonsa]
implementaciones_directas: [idealsa-escuintla-gt]
country_holding: GT
country_filiales: [PA, HN]
relacionado_con: [RAMAS_Y_CLIENTES, mercopan, merhonsa, idealsa]
tags: [holding/idealsa, cliente/mercopan, cliente/merhonsa, pais/GT, pais/PA, pais/HN]
---

# Holding IDEALSA — MERHONSA + MERCOPAN + CD Escuintla GT

> **Wave 7 — análisis paralelo de las dos BDs nuevas**
> Fuente: descubrimiento EC2 2026-04-29 + revelación verbal Erik sobre estructura corporativa.
> Estado: confirmado holding por similitud de schema (98%+); pendientes datos productivos en MERHONSA.
> **Actualizado 2026-04-30**: Erik confirmó que IDEALSA opera además un
> CD propio en Escuintla (Guatemala), sin BD específica todavía en EC2.

---

## Resumen ejecutivo

| Aspecto | MERHONSA | MERCOPAN |
|---------|----------|----------|
| País | Honduras | Panamá |
| Rol corporativo | Sucursal IDEALSA | Filial IDEALSA |
| BD | `IMS4MB_MERHONSA_PRD` | `IMS4MB_MERCOPAN_PRD` |
| Creada en EC2 | 2026-04-28 20:14 | 2026-04-28 20:15 |
| Tablas | 319 | 322 |
| Vistas | 194 | 204 |
| SPs | 37 | 38 |
| Tablas comunes | **315 (98%)** | mismo |
| Branch BOF asociado | (pendiente confirmar — probable `dev_2028_merge` por timing) | mismo |
| Productos esperados | aceite (perecedero) + detergentes | aceite + detergentes |
| Datos en stock (al momento del análisis) | 3 filas | 2724 filas |
| Movimientos transaccionales | 0 | **323.374** |
| Tareas HH cambio ubicación | 331 enc / 16429 det | 737 enc / 3840 det |

**Conclusiones inmediatas**:
1. MERCOPAN está claramente en producción (323K movimientos, 2.7K stock).
2. MERHONSA está pre-producción o recién migrada (0 movimientos, 3 stock, pero ya con tareas HH = arranque operativo en curso).
3. Ambas BDs comparten 98% del schema → confirmado **patrón holding**: ambas usan el mismo flavor `IMS4MB_*` de TOMWMS multi-tenant.
4. Las diferencias de schema son MENORES y tienen patrones identificables (regulatorio, módulos opcionales).

---

## Por qué se llaman así

| Sigla | Significado probable | Evidencia |
|-------|---------------------|-----------|
| **MERHONSA** | **Mer**cantil **Hon**dureña **S.A.** | Nomenclatura típica centroamericana, alineada con nombre país |
| **MERCOPAN** | **Mer**cantil **Co**mercial **Pan**amá | Misma raíz "Mer-", indica empresa hermana |
| **IDEALSA** | **Idea**l **S.A.** | Casa matriz que agrupa ambas (mencionada por Erik) |

→ Confirmar con Erik el significado oficial.

---

## Diferencias de schema (las 11 tablas únicas)

### Solo en MERHONSA (4 tablas)
| Tabla | Hipótesis |
|-------|-----------|
| `RecuperacionINV` | Recuperación de inventario (¿después de incidentes/pérdidas?) — sin equivalente en MERCOPAN |
| `producto_presentacion_bk` | Backup tabla `producto_presentacion` (sufijo `_bk` = backup manual) |
| `stock_DESPACHADO` | Histórico de stock que ya salió por despacho (¿partición manual?) |
| `tmp_licencia_item` | Tabla temporal de items por LP (probable workspace de un proceso batch) |

→ Patrón: MERHONSA tiene **artefactos de recuperación post-error o de migración** (los `_bk`, `tmp_`, `RecuperacionINV`). Indica que tuvo problemas de data y se hicieron backups manuales.

### Solo en MERCOPAN (7 tablas)
| Tabla | Hipótesis |
|-------|-----------|
| `StockCocinero` | **Rol "cocinero"** específico de Panamá. ¿Producción de comidas en almacén? Sugiere preparación de mezclas o pre-armados. |
| `stock_BK_Cocinero` | Backup del anterior |
| `reglas_vencimiento_contacto` | Reglas de notificación al contacto comercial cuando un lote vence |
| `rol_usuario_estado` | Estado por rol de usuario (multi-rol con habilitación granular) |
| `stock_jornada_consecutivo` | **Regulatorio** Panamá — control jornada consecutiva (similar a MAMPA) |
| `stock_jornada_desfase` | Desfase de jornada (delta esperado vs real) |
| `stock_jornada_fecha_consecutiva` | Reconciliación fechas consecutivas |

→ Patrón: MERCOPAN tiene **control regulatorio panameño (`stock_jornada_*`)** + **rol cocinero** + **reglas de notificación a contactos**. Más maduro funcionalmente que MERHONSA.

### Tablas en ambos pero ausentes en CEALSA + MAMPA (universo cross-cliente)
- `diferencias_movimientos` — bitácora de diferencias entre lo planeado y lo ejecutado
- `operador_montacarga` — control de operadores con vehículos (montacargas)
- `stock_res_ped_164` — reservas de pedidos con sufijo `_164` (¿branch específico? ¿ticket número 164?)
- `us_solic_det` / `us_solic_enc` — solicitudes de usuario (módulo workflow no estándar)

→ Patrón: el holding IDEALSA **agregó funcionalidades propias** que no están en CEALSA ni MAMPA. Probable que sean features pedidas por la corporación específicamente.

---

## Comparativa de capabilities (`i_nav_config_enc`)

Solo se reporta el primer registro (existe un registro por bodega/propietario, las BDs nuevas tienen pocas filas):

| Flag | MERHONSA | MERCOPAN | CEALSA | MAMPA |
|------|----------|----------|--------|-------|
| `genera_lp` | **False** | True | True | True |
| `implosion_automatica` | **True** | False | False | False |
| `explosion_automatica` | **True** | False | False | False |
| `Ejecutar_En_Despacho_Automaticamente` | False | False | True | True |
| `generar_recepcion_auto_bodega_destino` | True | False | True | True |
| `valida_solo_codigo` | (n/a) | (n/a) | (n/a) | True |
| `codigo_bodega_erp_nc` | NULL | NULL | NULL | '0' |

**Interpretación**:
- **MERHONSA es la única con implosión Y explosión automáticas** entre los 4 → asume el patrón Cumbre legacy. Pero **NO genera LPs** (`genera_lp=False`), lo que es contradictorio: si no genera LPs, ¿qué implosiona? **Probable explicación**: hereda LPs del ERP (NAV/SAP) y solo los manipula, sin crearlos.
- **MERCOPAN no tiene implosión ni explosión auto**, sí genera LPs propios → patrón "moderno" como CEALSA.
- **MAMPA es el único con `valida_solo_codigo=True`** → control simplificado.

→ **Q-MERHONSA-PARADOJA-LP**: si MERHONSA no genera LPs propios pero sí implosiona, ¿de dónde vienen los LPs originales? ¿Del ERP NAV/SAP?

---

## Productos perecederos (aceite + detergentes)

### Esperado por contexto
Erik mencionó que ambas manejan **productos perecederos como aceite** (vencimiento corto, ~6-12 meses) y **detergentes** (sin vencimiento o vencimiento muy largo). Esto requiere control diferenciado:
- Para aceite: `control_vencimiento=True` + `dias_vida_defecto_perecederos` corto
- Para detergente: probablemente `control_vencimiento=False` o vencimiento ignorado

### Lo que se puede confirmar hoy
**No se pudo verificar productos reales** porque las tablas `productos`, `bodegas`, `empresas` no aparecen en el schema `dbo` de MERHONSA/MERCOPAN. Posibilidades:
- (a) Están en otro schema (¿`tomwms.`, `nav.`, `app.`?). **Pendiente** descubrir el schema real.
- (b) Aún no fueron creadas (BDs recién instaladas).
- (c) Las views `vw_productos` u otras hacen el wrapping.

→ **Q-SCHEMA-PRODUCTOS-MERHONSA**: en qué schema viven `productos`, `bodegas`, `empresas` en MERHONSA y MERCOPAN.

---

## Validación holding (¿realmente comparten infraestructura?)

### Indicadores POSITIVOS de holding
1. **98% schema común** (315 tablas iguales de ~320)
2. **Mismo prefijo `IMS4MB_*`** que CEALSA/BECO (familia multi-tenant)
3. **Creación contigua** (mismo día, mismo minuto casi)
4. **Tablas exclusivas comunes** (`diferencias_movimientos`, `operador_montacarga`, `stock_res_ped_164`, `us_solic_det/enc`) → features holding-specific
5. **Catálogo `propietarios` con 1 registro en cada una** → probablemente ambas tienen "IDEALSA" o el nombre de la empresa local como propietario único

### Indicadores que diferencian
- MERCOPAN tiene módulos panameños (`stock_jornada_*`) y rol cocinero
- MERHONSA tiene artefactos de recuperación (`_bk`, `tmp_`)
- Configuración `i_nav_config_enc` muy distinta (implosión auto en MERHONSA pero no en MERCOPAN)

### Hipótesis de gobernanza
La casa matriz IDEALSA probablemente:
- Mantiene un **template de schema unificado** que se replica en cada filial
- Permite **personalización por país** (regulatorio, productos)
- **Sincroniza datos maestros** (productos, propietarios) desde un master ERP
- Cada filial opera **su propio stock** independiente

→ **Q-IDEALSA-MASTER-DATA**: ¿hay un sync de productos/propietarios entre MERHONSA y MERCOPAN? ¿O se cargan independientes?
→ **Q-IDEALSA-OTROS-PAISES**: ¿hay más filiales (Guatemala, El Salvador, Costa Rica)? Si sí, deberían tener BDs similares.

---

## Próximos pasos recomendados

1. **Confirmar branch BOF asociado** a MERHONSA y MERCOPAN (probable `dev_2028_merge` por la fecha)
2. **Descubrir schema real** de `productos`, `bodegas`, `empresas` (probable que estén en schema no-`dbo`)
3. **Mapear `propietarios` y `bodegas`** una vez encontrado el schema
4. **Análisis profundo de `trans_movimientos` MERCOPAN** (323K filas, masa para detectar patrón merge LP — ver `03-implosion-y-merge-lp.md`)
5. **Comparar `i_nav_config_enc` entre TODAS las bodegas** (no solo el TOP 1) para entender heterogeneidad por bodega dentro de la misma empresa
6. **Preguntar a Erik** sobre las paradojas detectadas: MERHONSA implosión sin generar LP, qué es Clavaud, qué es UMB
7. **Localizar BD productiva del CD IDEALSA Escuintla GT** (Q-IDEALSA-DB-ESCUINTLA-GT). Si está en otro EC2, agregar credenciales al brain. Si está embebida en otra BD del grupo, identificar el `propietario_id` o tenant correspondiente.

---

## CD Escuintla (Guatemala) — operación directa del holding

> Confirmado por Erik 2026-04-30.

IDEALSA, además de poseer las filiales MERCOPAN (PA) y MERHONSA (HN),
**opera un CD propio en Escuintla, Guatemala**. Esto convierte a IDEALSA
en cliente *directo* del WMS (no solo holding pasivo).

### Lo que se sabe
- Implementación confirmada en GT.
- Ubicación: Escuintla.
- Sede del holding: Guatemala (consistente con la operación directa).

### Lo que NO se sabe (pendiente Erik)
- **BD productiva**: NO está creada en `52.41.114.122,1437`. Hipótesis:
  - (a) Corre en otro EC2 que el agente no tiene mapeado todavía.
  - (b) Está embebida en otra BD del grupo IDEALSA con un `propietario_id`
    distinto (multi-tenancy a nivel de fila — el WMS soporta `propietarios`
    y `empresas` que pueden particionar dentro de una BD).
  - (c) Aún no migró del sistema previo.
- **Rama BOF**: pendiente confirmar.
- **ERP integrado**: pendiente confirmar.
- **Comparación de schema** con MERCOPAN/MERHONSA: imposible mientras no
  haya BD accesible.

### Implicación para el brain
- Cuando aparezcan transacciones de IDEALSA Escuintla en el contexto
  (logs, tickets, menciones de Erik), buscar primero **a qué BD del EC2
  apuntan** (puede ser una de las dos del holding usando un `propietario_id`
  distinto). Si no aparece en ninguna, escalar como
  `Q-IDEALSA-DB-ESCUINTLA-GT`.
- El nodo `idealsa` en el grafo del brain debe representarse como
  **cliente directo** además de holding (doble rol).

---

## BDs en EC2 fuera del contexto WMS (no usar para análisis WMS)

El mismo server EC2 `52.41.114.122,1437` hospeda BDs **ajenas al WMS**
de la otra línea de productos POS/Road. Erik confirmó 2026-04-30:

| BD | Sistema | Comentario |
|---|---|---|
| `LIVE` | (otro sistema) | Fuera del scope WMS |
| `mpos_pollo_express_qa` | mPos (POS/restaurantes) | Línea POS, no WMS |
| `POD_BETA` | Proof-of-Delivery beta | Probable línea Road/POD |

→ Para queries READ-ONLY del agente: **whitelist de BDs WMS** =
`TOMWMS_*`, `IMS4MB_*` (con la salvedad de excluir copias diagnósticas
no productivas como BECOFARMA — ver `clients/README.md`).

---

## Q-* nuevas generadas en este análisis

- `Q-IDEALSA-MASTER-DATA` — ¿sync de master entre filiales?
- `Q-IDEALSA-OTROS-PAISES` — ¿hay más filiales? **PARCIALMENTE RESPONDIDA 2026-04-30**: Erik confirmó que IDEALSA tiene operación directa en Guatemala (CD Escuintla), pero esa instalación NO tiene BD propia en el EC2 actual.
- `Q-IDEALSA-DB-ESCUINTLA-GT` — **NUEVA 2026-04-30**: ¿dónde corre la BD productiva del CD GT? Posibilidades: (a) EC2 distinto, (b) embebida en otra BD del grupo con `propietario_id` distinto (multi-tenancy a nivel de fila), (c) aún no migró del sistema previo.
- `Q-MERHONSA-PARADOJA-LP` — implosión sin generar LP propio
- `Q-SCHEMA-PRODUCTOS-MERHONSA` — schema real de productos/bodegas
- `Q-COCINERO-ROLE-PANAMA` — qué hace exactamente el rol "cocinero" en MERCOPAN
- `Q-STOCK-RES-PED-164-SUFIJO` — qué significa el `_164`
- `Q-DIFERENCIAS-MOVIMIENTOS-USO` — cómo se popula esta tabla y para qué
- `Q-OPERADOR-MONTACARGA` — control de operadores con vehículos, ¿por turno/jornada?

(Todas se agregan a `CUESTIONARIO_CAROLINA.md` bloque 11)

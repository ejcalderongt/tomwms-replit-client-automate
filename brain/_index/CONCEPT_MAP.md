# CONCEPT MAP — vista temática del brain (Wave 6.1)

**Fecha**: 2026-04-28  
**Propósito**: agrupar las ~90 Q-* abiertas y los conceptos del brain por dominio, para navegar por tema en vez de cronología. Complementa al INDEX (que es cronológico por wave).

**Cómo leerlo**:
- Cada dominio tiene: descripción · conceptos clave (con xref) · Q-* abiertas con metadata
- Cada Q-* tiene: **prioridad** · **método de resolución** · **donde se documenta** · **resumen 1-línea**
- Al final hay: dependencias entre dominios · candidatos a próxima traza · quick-wins

**Notación de método de resolución**:
- `SQL` — query READ-ONLY a EC2
- `GREP` — búsqueda en repos `dev_2028_merge` o `dev_2023_estable`
- `DIFF` — diff entre ramas
- `ERIK` — necesita respuesta del humano
- `MIXTO` — combinación

---

## Dominio A — WMS Core: invariantes de stock (LP, lote, vencimiento, peso, serializado)

**Descripción**: las cuatro propiedades binarias que definen cómo se trackea cada producto a nivel unidad: License Plate (`genera_lp` + `genera_lp_old`), `control_lote`, `control_vencimiento`, `control_peso`, y `serializado`. Más `IdTipoRotacion` (FIFO/LIFO/FEFO).

**Conceptos clave**:
- **License Plate (LP)**: identificador único de unidad de carga — ver `traza-001-license-plate.md` (794 líneas)
- **Patrón consecutivo en clsLnProducto.cs (DALCore 2028)**: `serializado → genera_lote → genera_lp_old → control_vencimiento` aparece como bloque atómico. Solo en `dev_2028_merge`.
- **Anomalía `Producto.genera_lp` vs `Bodega.genera_lp_old`**: traza-001 §6 y §11.2

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-LP-CICLO-VIDA | alta | GREP+SQL | Recorrido completo del LP desde generación hasta despacho |
| Q-LP-EXPLOSION-COMO-OPERA | alta | GREP | Mecánica de explosión de un LP en N sub-LPs (caso K7) |
| Q-LP-LONG-DEFAULT | media | SQL | Longitud por defecto del LP en cada cliente |
| Q-LP-LONG-VS-DATOS-REALES | media | SQL | LPs reales vs longitud declarada — desfase histórico |
| Q-LP-OPERADOR-VS-USUARIO | media | GREP | ¿quién genera el LP, operador HH o usuario BOF? |
| Q-LP-S-VARIANTE | baja | GREP | El sufijo "S" en algunos LPs — qué significa |
| Q-LP-CORRELATIVO-NAV | media | SQL+GREP | LPs con prefijo NAV en BYB — origen y semántica |
| Q-LP-FALTANTES-PARA-QUE | media | ERIK | Función `Get_LpFaltantes` — caso de uso real |
| Q-LP-917-DRIFT-RESIDUAL | baja | SQL | El LP "917" aparece como hueco — explicar |
| Q-LP-EN-K7-DRIFT-25PCT | media | SQL | 25% de stock K7 sin LP — clasificar el origen |
| Q-LP-BYB-PRODS-SIN-LP | baja | SQL | Productos BYB con `genera_lp=0` — listar y validar política |
| Q-LP-NAMING-DB | baja | GREP | Inconsistencia `lic_plate` vs `LicensePlate` en código |
| Q-PRODUCTO-GENERA-LP-NUEVO | media | DIFF | El campo nuevo en `producto.genera_lp` — semántica vs `genera_lp_old` |
| Q-GENERA-LP-OLD-LEGADO | media | DIFF | ¿Por qué quedó `genera_lp_old` en `bodega` y no migró a `producto`? |
| Q-PRESENTACION1-MUERTA | baja | SQL | Tabla `presentacion1` pre-existente sin uso — abandonar o limpiar |

---

## Dominio B — Ramas, clientes y migración 2023→2028

**Descripción**: las 5 ramas BOF + 25 ramas HH, qué cliente corre qué hoy, y la transición major en curso (MAMPA primero, BECO/K7/BYB/CEALSA después).

**Conceptos clave**:
- **Mapa de ramas**: `agent-context/RAMAS_Y_CLIENTES.md`
- **Disclaimer global**: `dev_2028_merge` para scan, `dev_2023_estable` en producción 4/5 clientes
- **Diferencias 2023 vs 2028**: ver `RAMAS_Y_CLIENTES.md §3` y INDEX Wave 6.1

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-MIGRACION-2023-A-2028 | **alta** | ERIK | Cronograma + orden + scripts DB schema + breaking changes |
| Q-DEV2025-PROPOSITO | media | DIFF+ERIK | ¿Qué tiene `dev_2025`? ¿Es paso intermedio? |
| Q-MASTER-PROPOSITO | media | DIFF+ERIK | ¿`master` = release oficial vs `dev_2023_estable` activa? |
| Q-HH-RAMAS-25 | media | ERIK | Política de ramas HH — consolidación, qué clientes activos siguen forkeados |
| Q-MERCOPAN-MERCOSAL | baja | ERIK | Ramas con clientes fuera de la lista activa — históricos o prospectos |
| Q-CUMBRE-RAMA-DEDICADA | media | ERIK | ¿Por qué Cumbre tiene rama HH propia y no comparte 2028_merge? |
| Q-SOAP-A-JSON-2028 | media | GREP+ERIK | ¿Política consciente convertir SOAP → JSON? Hoy solo 1 caso (LP) |
| Q-CASE-CONTROL-CLIENTE | baja | GREP | Capability flags activadas por cliente — mapeo |

---

## Dominio C — DALCore / EntityCore / AppGlobalCore (.NET Core)

**Descripción**: la trinidad de capas duplicadas en .NET Core, paralela al WMS.DAL/Entity legacy en VB.NET Framework. **Solo existen en `dev_2028_merge`**. Documentado completo en `02-portal-y-dms.md §6`.

**Conceptos clave**:
- **Duplicación obligada**: incompatibilidad VB.NET Framework ↔ .NET Core
- **Oportunidad de cleanup**: cada port permite mejorar el legacy
- **Consumidores principales**: WMSWebAPI (B2B), DMS (probable), futuro Web BOF

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-DALCORE-PROPOSITO | ✅ | RESUELTA Wave 6.1 | Ver `02-portal-y-dms.md` |
| Q-DALCORE-PARIDAD | media | GREP | % del DAL legacy portado a DALCore |
| Q-DALCORE-CONSUMERS | alta | GREP | ¿Quién invoca DALCore? Solo WMSWebAPI o también DMS, etc. |
| Q-DALCORE-COMPORTAMIENTO | media | DIFF | Diferencias semánticas DAL legacy vs DALCore (excepciones, logging, validación) |
| Q-DALCORE-VS-DAL-LEGACY | media | DIFF | Tabla por tabla: cuáles tienen ambas, cuáles solo legacy |

---

## Dominio D — Portal CEALSA, DMS y replicación cloud

**Descripción**: la idea estratégica del portal multi-propietario, con el DMS de Efren como puente on-prem → cloud. Documentado en `02-portal-y-dms.md §1-§4`.

**Conceptos clave**:
- **Origen regulatorio**: SAT/SIB/aseguradora (Guatemala) → `stock_jornada`
- **Necesidad comercial**: visibilidad inter-cortes para propietarios pagantes
- **Decisión "como arriba es abajo"**: replicación transaccional, no raw
- **Principio Erik**: control total > performance (fila por fila, no bulk)
- **DMS ubicación**: `TOMWMS_BOF/DMS/` (Api + Clases)

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-DMS-USA-DALCORE | media | GREP | ¿DMS usa AppGlobalCore/DALCore o sus propias clases? |
| Q-DMS-DESTINO-CLOUD | media | ERIK | Tipo de target: SQL Server VPS / Azure SQL / containers |
| Q-DMS-PROPIETARIO-FILTER | media | GREP+ERIK | Dónde se configura qué propietarios replican |
| Q-PORTAL-STACK | alta | ERIK | Tecnología: ¿está construido? ¿qué repo? |
| Q-PORTAL-AUTH | media | ERIK | Autenticación: usuarios CEALSA-side o propietario-side |
| Q-PORTAL-MULTITENANCY-DECISION | media | ERIK | 1-BD-por-cliente vs 1-BD-compartida — decisión actual |
| Q-CEALSA-REPO | media | ERIK | Repo `CEALSA` en Azure DevOps vacío — ¿iba a ser el portal? |
| Q-TOMWEB-PROPOSITO | media | ERIK | TOMWeb = ASP.NET clásico+PHP — ¿qué es? portal corporativo, tienda de licencias |
| Q-WMS5-VACIO | baja | ERIK | Repo `TOMWMS5` vacío — ¿proyecto cancelado? placeholder? |
| Q-TOMWMSUX-VS-WMSPORTAL | media | ERIK | Si hay otros portales — distinguir |

---

## Dominio E — Multi-tenancy 3PL: propietarios, estados y tropicalización

**Descripción**: el modelo donde un cliente WMS puede tener N propietarios de mercancía, cada uno con su set de estados aceptables. Aplica fuerte en CEALSA (3PL puro) y MAMPA. Marginal en BECO/BYB/K7. Documentado en `02-portal-y-dms.md §7`.

**Conceptos clave**:
- **Tropicalización por propietario**: cada propietario define su criterio de exclusión sobre estados
- **Tablas validadas (CEALSA QAS)**: `propietarios`, `propietario_bodega`, `propietario_destinatario`, `propietario_reglas_enc/det`, `producto_estado`, `producto_estado_ubic`

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-PROPIETARIO-ESTADO-MODELO | alta | SQL | Tabla puente exacta entre propietario y estados aceptables |
| Q-PROPIETARIO-AGNOSTICO | media | SQL | Cómo manejan propietarios los clientes no-3PL (BECO/BYB/K7) |
| Q-CEALSA-3200-ESTADOS | media | SQL | 3200 filas en producto_estado — proliferación, ¿hay duplicados? |
| Q-CEALSA-AUSENTES-7 | baja | SQL | 7 propietarios sin asociaciones — limpiar o entender |
| Q-CEALSA-ORIGEN-PROP-3197 | baja | SQL | Origen del propietario 3197 — tracking |
| Q-CEALSA-IDACUERDO-1 | baja | SQL | IdAcuerdo=1 fallback genérico — política |
| Q-CEALSA-IDTIPO-NULL | media | SQL | Filas con IdTipo NULL — orphan data |
| Q-CEALSA-LICENCIAS-LOWERCASE | baja | SQL | Inconsistencia case en `licencias` |
| Q-CEALSA-PREFACTURA-MODELO | media | SQL+GREP | Modelo de prefactura del 3PL |
| Q-CEALSA-RH-HR | baja | SQL | Tablas RH/HR aparentes — ¿módulo HR del WMS? |
| Q-CEALSA-TYPO-DESPACHOS | baja | SQL | Typo histórico en columna despachos |
| Q-CEALSA-OUTBOX-VACIO | media | SQL | Outbox CEALSA con 0 filas — nunca usado o purgado |

---

## Dominio F — stock_jornada (corte regulatorio)

**Descripción**: snapshot diario de TODO el stock con su contexto transaccional completo. Materializa "como arriba es abajo". Activo en CEALSA y MAMPA. Documentado en `02-portal-y-dms.md §8`.

**Conceptos clave**:
- **Estructura 25+ columnas**: replica IdRecepcionEnc/Det, IdPedidoEnc, IdPickingEnc, IdDespachoEnc, lote, lic_plate, serial
- **Tablas auxiliares**: `jornada_sistema`, `stock_jornada_consecutivo`, `stock_jornada_desfase`, `stock_jornada_temporal`, `tmp_stock_jornada`, `pendiente_jornada`
- **Validado**: solo MAMPA y CEALSA tienen filas; BECO/K7/BYB tabla vacía

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-STOCK-JORNADA-PROCESO | alta | GREP+SQL | SP / componente que cierra la jornada (batch nocturno?) |
| Q-STOCK-JORNADA-CONSUMER | alta | ERIK | ¿Quién consume el resultado? SAT/SIB/portal/auditoría |
| Q-STOCK-JORNADA-DESFASE | media | SQL | Cómo se calcula `stock_jornada_desfase` y qué pasa cuando hay |
| Q-STOCK-JORNADA-MAMPA | alta | ERIK | ¿Por qué MAMPA tiene 21.883 filas? ¿es 3PL también o viene por defecto en 2028? |

---

## Dominio G — Integraciones (MI3, NAV, MercaERP, WSHHRN, WMSWebAPI)

**Descripción**: los 4 canales de comunicación con el mundo: HH Android (WSHHRN), ERPs cliente (MI3 + cealsasync + NAV adapters), B2B externo (WMSWebAPI), y el módulo controvertido ChatGPT.

**Conceptos clave**:
- **Mapa de cajas**: `code-deep-flow/00-mapa-de-cajas.md`
- **WMSWebAPI no greenfield**: existe desde 2023, B2B-only (MHS = primer caso real)
- **Anomalía NAV en CEALSAMI3**: clases con prefijo `Nav` que contradicen "NAV es solo BYB"

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-NAV-PREFIJO-CEALSA | **alta** | ERIK | Clases `clsSyncNav*` en CEALSAMI3 — copy-paste residual o NAV real |
| Q-MI3 / Q-MI3-QUE-ES / Q-INTERFACE-MI3 | media | ERIK | Definición precisa de MI3 — ¿bridge a MercaERP/IMS4? |
| Q-MHS-COMO-CLIENTE | alta | ERIK | Scope WebAPI con MHS, fecha go-live, qué maestros escribe |
| Q-WSHHRN-AS-PROXY-BYB | media | GREP | ¿WSHHRN actúa de proxy SOAP a NAV en BYB? |
| Q-WMSWEBAPI-MIGRACION-MAPA | media | GREP | Endpoints WebAPI — qué cubren vs qué cubre WSHHRN |
| Q-WMS-EXE-CONFIG-EN-WSHHRN | media | GREP | Config del WMS embebida en WSHHRN — desacoplar |
| Q-CHATGPT-SERVICE | alta (sec) | GREP | `WSHHRN/ChatGPTService.vb` — caso de uso real, ¿está en producción? |
| Q-BYB-NO-DISPONIBLE-NAV-BD | media | SQL+ERIK | ¿BD NAV de BYB no es accesible? Validar canal |
| Q-CEALSA-CEALSASYNC-ERP | media | GREP | Qué ERP exacto consume CEALSASYNC (IMS4? MercaERP?) |
| Q-K7-OC-TIPOS | baja | SQL | Tipos de OC en K7 |
| Q-K7-ML-MODELO | baja | SQL+ERIK | Modelo de Machine Learning en K7 (¿perceptron?) — ver Q-PERCEPTRON-USO-REAL |

---

## Dominio H — Generador de código (BE / LN-base / LN-partial)

**Descripción**: la primera automatización del WMS — generador que se pega a la BD y produce las 3 clases por tabla. Documentado en `02-portal-y-dms.md §5`.

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-GENERADOR-UBICACION | alta | ERIK | Dónde vive el código del generador (¿proyecto en TOMWMS_BOF? ¿app standalone?) |
| Q-GENERADOR-INPUTS | media | ERIK | Cómo se especifica el contrato de generación — plantillas T4 / templates manuales |
| Q-LN-DRIFT-AUDIT | media | GREP | % de clases LN con código manual en archivo "autogenerado" |
| Q-GENERADOR-WEB-VARIANTE | baja | ERIK | ¿Tercera variante para UI Web? (futuro Web BOF) |

---

## Dominio I — Verificación / capabilities (modelo BYB-style)

**Descripción**: el flag `verif*` en operaciones HH (verificación de picking, despacho, recepción) y cómo varía por cliente.

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-VERIF-BB | media | SQL+GREP | Verificación en BECO — modo y políticas |
| Q-VERIF-C9 | media | SQL+GREP | Verificación en cliente C9 (¿cuál?) |
| Q-VERIF-K7 | media | SQL+GREP | Verificación en K7 |
| Q-VERIF-K7-PERIODOS | baja | SQL | Períodos de verificación K7 |
| Q-VERIFICACION-CONSOL | media | GREP | Vista consolidada del modelo de verificación |
| Q-CAPABILITY-FLAG | alta | GREP | Patrón general de capability flags por cliente — listar todos |
| Q-CAPABILITY-FLAG-VERIF | media | GREP | Subconjunto de capability flags relacionados a verificación |
| Q-LICENCIAS-MODELO-NUEVO | media | DIFF | Modelo nuevo de licencias (¿2028?) — schema y consumidores |
| Q-BYB-VERIF-INCOMPLETA | media | SQL | Casos BYB con verificación parcial — política |

---

## Dominio J — Reservas de stock (Reservation 2 vs 3)

**Descripción**: en `dev_2028_merge` aparecen `WMS.StockReservation2` y `WMS.StockReservation3` — dos modelos paralelos.

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-STOCK-RESERVATION-2-VS-3 | alta | GREP | ¿Por qué hay 2 implementaciones? ¿Cuál se usa? ¿Cuál es legacy? |
| Q-RESERVAR-EN-UMBAS | media | GREP | Función "reservar en umbas" — semántica |

---

## Dominio K — Recepción / pedido / despacho / ciclo

**Descripción**: el flujo transaccional completo del WMS — desde `recepcion_enc/det` hasta `despacho_enc/det`. La traza-001 ya cubre la parte de LP. Quedan los eslabones intermedios.

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-RECEPCION-BOF-FLUJO | alta | GREP+SQL | Flujo BOF de recepción (no la HH) |
| Q-CICLO-8A-01..06 | media | GREP | Sub-pasos del ciclo "8A" — falta documentar cada uno |
| Q-HH-RECEPCION-DOS-VERSIONES | media | GREP | `frm_recepcion_datos.java` tiene 2 modos (HH) — diferenciar |
| Q-MAMPA-MERMA-CARNE-FLUJO | baja | SQL+ERIK | Caso de merma de carne en MAMPA — flujo específico |

---

## Dominio L — Cliente-específicas (anomalías y casos)

**Descripción**: hallazgos puntuales SQL que aplican a un cliente específico. Útiles para una "auditoría por cliente" más que para arquitectura.

**Q-* abiertas (resumen agrupado)**:

| Cliente | Q-* | Total | Prio agregada |
|---|---|---:|---|
| BECO | AJUSTE-BYB, DEVVENTA-IDPRODESTADO-NEG1, PRODUCCION | 3 | media |
| BYB | CORTE-2024, OUTBOX-BACKLOG, VERIF-INCOMPLETA, NO-DISPONIBLE-NAV-BD | 4 | media |
| K7 | BOD5-AMATITLAN-NOSAP, BOD7, BOD7-FACTURACION, DUPLICADOS-CONFIG, ML-MODELO, OC-TIPOS, TYPO-EXPLOSION | 7 | media |
| MAMPA | BOD23-FALTANTES, CEDIS-PANTALLA-LEGACY, ERP, IDINDICE-4, IDPRODESTADO-3, MERMA-CARNE-FLUJO, PUNTOS-SIMILARES, VOLUMEN-OUTBOX | 8 | media |

→ Idealmente cada cliente tendría un `agent-context/CLIENTE_<X>.md` con su panel de hallazgos. Pendiente Wave futura.

---

## Dominio M — Web BOF (futuro)

**Descripción**: la migración prevista BOF WinForms → Web. DALCore es preparación.

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-WEB-BOF-STACK | baja | ERIK | Tecnología (Blazor / Razor / SPA) |
| Q-WEB-BOF-TIMELINE | baja | ERIK | ¿Después de cerrar 2023→2028? ¿en paralelo? |

---

## Dominio N — Seguridad (urgencia alta)

**Descripción**: leaks de credenciales en repos.

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-SEC-OPENAI-KEY-LEAK | **CRÍTICA** | ACCIÓN | OpenAI API key hardcoded en `WSHHRN/ChatGPTService.vb`. Existe en 2023 también — leak pre-existente, mucho tiempo expuesto. Rotar + scrub git history |
| Q-SEC-CONNINI-CREDS | **alta** | GREP | Credenciales DB en archivos `.ini` / `connstring` — auditar y mover a vault/env |
| Q-CONNINI-SELECCION | media | GREP | Estrategia de selección de connstring por cliente |

---

## Dominio O — Otros / técnicos puntuales

**Q-* abiertas**:

| Q-* | Prio | Método | Resumen |
|---|---|---|---|
| Q-EC2-DESFASE | baja | SQL | Desfase de tiempo entre nodos EC2 — diagnóstico |
| Q-PERCEPTRON-USO-REAL | media | GREP+ERIK | "Perceptron" mencionado en código — modelo ML real o leftover |
| Q-CASE-NAME / Q-CASE-NAME-K7 | baja | SQL | Inconsistencias case-sensitive en nombres |

---

## Mapa de dependencias entre dominios

```
   ┌──────────────────────────────────────────┐
   │ B - Ramas/Migración                      │ ← TODOS dependen del disclaimer
   └──┬───────────────────────────────────────┘
      │
      ├──► A - WMS Core (LP, lote, etc.)
      │        └── traza-001 (LP) ✓
      │        └── traza-002 (lote+vencimiento) ← candidata
      │
      ├──► C - DALCore                              ┐
      │       └── solo aplica a 2028 + MAMPA       │
      │                                            ├─► M - Web BOF (futuro)
      ├──► D - Portal CEALSA / DMS                  │
      │       └── usa C (probable)                  │
      │       └── usa F (stock_jornada)             │
      │                                            
      ├──► E - Multi-tenancy 3PL                    
      │       └── habilita D                        
      │       └── habilita F                        
      │                                            
      ├──► F - stock_jornada (regulatorio)          
      │       └── alimenta D                        
      │                                            
      ├──► G - Integraciones (MI3/NAV/WSHHRN/WebAPI)
      │       └── crítica para B (qué se mantiene en migración)
      │       └── Q-NAV-PREFIJO-CEALSA bloqueante para mapa cliente
      │                                            
      ├──► H - Generador de código
      │       └── alimenta C (qué se duplica)
      │                                            
      ├──► I - Verificación / capabilities
      │       └── modelo nuevo licencias (2028)
      │                                            
      ├──► J - Reservas (2 vs 3)
      │       └── solo 2028
      │                                            
      ├──► K - Recepción/pedido/despacho/ciclo     
      │       └── usa A (LP, lote, etc.)
      │                                            
      ├──► L - Cliente-específicas (BECO/BYB/K7/MAMPA)
      │       └── auditoría por cliente — Wave futura
      │                                            
      └──► N - Seguridad (TRANSVERSAL, urgente)
              └── Q-SEC-OPENAI-KEY-LEAK = ACCIÓN ya
```

---

## Candidatos a próxima traza

Evaluación bajo el lente "B" (continuar con traza temática):

### Opción 1 — `traza-002 control_lote + control_vencimiento`
- **Dominio**: A (WMS Core)
- **Cubre**: 8-12 Q-* del dominio A no-LP
- **Validable**: 100% SQL + GREP en `dev_2023_estable` (aplica a 4/5 clientes prod)
- **Riesgo**: el patrón consecutivo `serializado+genera_lote+genera_lp_old+control_vencimiento` está en DALCore 2028, no en 2023. Habría que documentar **2 versiones** del flujo (legacy 2023 + nuevo 2028).
- **Tamaño estimado**: 600-800 líneas
- **Bloqueantes**: ninguno

### Opción 2 — `traza-002 stock_jornada` (cierre regulatorio)
- **Dominio**: F + D (porque alimenta el portal)
- **Cubre**: 4 Q-* del dominio F + parcial de D + parcial de E
- **Validable**: SQL CEALSA QAS + MAMPA QA + GREP componente cierre
- **Riesgo**: necesita Q-STOCK-JORNADA-CONSUMER respondida por Erik para cerrar el "para qué"
- **Tamaño estimado**: 400-600 líneas
- **Bloqueantes**: 1 (preguntar consumer)

### Opción 3 — `traza-002 cealsasync + NAV-prefijo-mystery`
- **Dominio**: G + B (la contradicción NAV)
- **Cubre**: Q-NAV-PREFIJO-CEALSA (alta prioridad), Q-CEALSA-CEALSASYNC-ERP, Q-MI3, Q-INTERFACE-MI3
- **Validable**: GREP sobre las 4 clases `clsSyncNav*` + comparación con un NAV real (BYB) + DIFF entre ramas
- **Riesgo**: si la respuesta es "copy-paste residual" la traza queda corta. Si es "CEALSA tiene NAV real" se vuelve grande.
- **Tamaño estimado**: 300-500 líneas (si copy-paste) o 700-900 (si NAV real)
- **Bloqueantes**: 1 pregunta corta a Erik para confirmar hipótesis antes de empezar

### Opción 4 — `traza-002 capability-flags por cliente`
- **Dominio**: I + L
- **Cubre**: Q-CAPABILITY-FLAG, Q-VERIF-*, todas las cliente-específicas
- **Validable**: SQL todos los EC2 + GREP `if Cliente.IsBYB Then` patterns
- **Riesgo**: ramificación enorme — cada flag genera un sub-dominio
- **Tamaño estimado**: 800-1200 líneas
- **Bloqueantes**: ninguno técnico, pero alcance puede explotar

### Opción 5 — Wave de cierre de seguridad (no-traza, ACCIÓN)
- **Dominio**: N
- **Cubre**: Q-SEC-OPENAI-KEY-LEAK (CRÍTICA), Q-SEC-CONNINI-CREDS
- **Validable**: GREP exhaustivo de credenciales + auditoría git history
- **Riesgo**: implica decisión operativa de Erik (rotar key, scrubbing, etc.)
- **Tamaño estimado**: doc de plan + script de remediación
- **Bloqueantes**: requiere acción Erik — pero el plan se puede escribir ya

---

## Quick wins (Q-* baratas, < 30 min cada una)

Resolubles solo con SQL/GREP ya disponibles:

1. **Q-LP-LONG-DEFAULT** — SQL en `producto_parametros` por cliente
2. **Q-LP-LONG-VS-DATOS-REALES** — `SELECT MAX(LEN(lic_plate))` por cliente
3. **Q-CEALSA-AUSENTES-7** — SQL ya escrita, falta consolidar
4. **Q-DALCORE-PARIDAD** — `git ls-tree | wc -l` + comparación nombres
5. **Q-DEV2025-PROPOSITO** — DIFF rápido entre `dev_2025` y `dev_2023_estable` + `dev_2028_merge`
6. **Q-MASTER-PROPOSITO** — DIFF rápido entre `master` y `dev_2023_estable`
7. **Q-LN-DRIFT-AUDIT** — GREP de métodos manuales fuera de `.partial.vb`
8. **Q-PROPIETARIO-AGNOSTICO** — `SELECT COUNT(*) FROM propietarios` en BECO/BYB/K7

→ Una "Wave 6.2 quick-wins" podría cerrar 8-12 Q-* en 1-2 turnos.

---

## Recomendación para evaluar Opción B

Dado el concept-map, mi sugerencia ranqueada:

1. **Antes de cualquier traza nueva**: hacer la **Wave 6.2 quick-wins** (~1-2 turnos) para cerrar 8 Q-* baratas que reducen el ruido del concept-map y dan datos para mejor decidir trazas futuras.

2. **Luego**, entre las opciones de traza, recomiendo **Opción 1 (control_lote + control_vencimiento)** porque:
   - Continúa el dominio A donde dejamos la traza-001 (cohesión narrativa)
   - 100% validable sin esperar respuestas Erik
   - Cubre la realidad de los 4 clientes prod (rama 2023)
   - Documenta 2 versiones (legacy + nuevo) — útil para Q-MIGRACION-2023-A-2028
   - Tamaño manejable (600-800 líneas)

3. **Paralelo / no bloqueante**: arrancar el **Plan de remediación seguridad (Opción 5)** porque Q-SEC-OPENAI-KEY-LEAK no debería esperar.

4. Las opciones 2, 3, 4 quedan para waves siguientes una vez resuelta Q-NAV-PREFIJO-CEALSA y Q-STOCK-JORNADA-CONSUMER (bloqueos cortos a despejar con Erik).

---

## Métricas del brain (Wave 6.1 cierre)

- **Q-* totales (sin placeholders)**: ~85
- **Q-* resueltas hasta hoy**: 1 (Q-DALCORE-PROPOSITO en Wave 6.1)
- **Q-* alta prioridad abiertas**: 11
- **Q-* críticas abiertas**: 1 (Q-SEC-OPENAI-KEY-LEAK)
- **Líneas brain total**: 2.624 (incluyendo este concept-map)
- **Archivos brain**: 5 principales

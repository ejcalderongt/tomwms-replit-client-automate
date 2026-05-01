---
id: CUESTIONARIO_CAROLINA
tipo: agent-context
estado: vigente
titulo: Cuestionario para Carolina — onboarding al brain WMS
ramas: [240Cealsa, 240byb, dev_2023_estable, dev_2025, dev_2028_merge]
tags: [agent-context]
---

# Cuestionario para Carolina — onboarding al brain WMS

**Fecha**: 2026-04-28  
**Contexto**: Carolina entra al proyecto. Erik dice que ella conoce bien el sistema y que cuando no sepa, consulta con él para homologar.  
**Cómo usar**: contestá en bloques. Si no sabés, dejá `(consultar Erik)` o `(no sé)` y seguís. Las respuestas que des acá las consolidamos en el brain. Pri: **alta** = bloquea trazas o decisiones; **media** = limpia ambigüedad; **baja** = trivia útil.

**Para situarte rápido**: leé `brain/_index/CONCEPT_MAP.md` y `brain/agent-context/RAMAS_Y_CLIENTES.md`. Esos dos archivos son el resumen del estado del brain Wave 6.1.

Total de preguntas: **38** agrupadas en **9 bloques**.

---

## Bloque 1 — Ramas y migración 2023→2028 (pri alta–media)

1. **Q-MIGRACION-2023-A-2028** (alta) — ¿Cuál es el orden previsto de migración de los 4 clientes restantes (BECO/K7/BYB/CEALSA) de `dev_2023_estable` a `dev_2028_merge`? ¿Hay cronograma? ¿Quién va después de MAMPA?

2. **Q-MIGRACION-2023-A-2028 (parte 2)** (alta) — ¿Existen scripts de migración de schema DB entre 2023 y 2028? Si sí, ¿dónde están? Si no, ¿se asume que el schema ya cambió en producción y solo falta el código?

3. **Q-MIGRACION-2023-A-2028 (parte 3)** (alta) — ¿Cuáles son los breaking changes funcionales del WMS 2028 que justifican que sea "major"?

4. **Q-DEV2025-PROPOSITO** (media) — La rama `dev_2025` existe en BOF y HH. ¿Qué contiene? ¿Es paso intermedio entre 2023 y 2028 o quedó muerta?

5. **Q-MASTER-PROPOSITO** (media) — `master` es la default en ambos repos. ¿Es la "release oficial congelada" vs `dev_2023_estable` que es trabajo activo, o son lo mismo?

6. **Q-HH-RAMAS-25** (media) — Hay 25+ ramas en TOMHH2025. ¿Se planea consolidar? Las ramas con nombres de cliente (`byb`, `cealsa`, `mercopan`, `mercosal`, `240byb`, `240Cealsa`) ¿están en uso o son históricas?

7. **Q-MERCOPAN-MERCOSAL** (media) — Existen ramas `mercopan` y `mercosal` en HH, pero esos nombres no aparecen en la lista activa de clientes (BECO/K7/BYB/CEALSA/MAMPA/Cumbre/MHS). ¿Son clientes históricos? ¿Prospectos? ¿Otros?

8. **Q-CUMBRE-RAMA-DEDICADA** (media) — Cumbre tiene rama propia `dev_2028_Cumbre`. ¿Por qué no comparte con `dev_2028_merge` como MAMPA? ¿Qué hace distinto Cumbre?

---

## Bloque 2 — Integraciones (pri alta — bloqueantes)

9. **Q-NAV-PREFIJO-CEALSA** (alta — bloqueante) — En `TOMWMS_BOF/CEALSAMI3/Clases Interface Sync/` hay clases con prefijo `Nav`: `clsSyncNavProducto`, `clsSyncNavCategoriasProducto`, `clsSyncNavGruposProducto`, `clsSyncNavTablaConversion`. Erik dijo "NAV es solo BYB". ¿Estas clases son copy-paste residual de un BYBSync original sin renombrar, o CEALSA usa NAV para algún módulo, o "Nav" significa otra cosa (no Microsoft Dynamics NAV)?

10. **Q-CEALSA-CEALSASYNC-ERP** (alta) — ¿Qué ERP exacto consume CEALSASYNC del lado del cliente? ¿IMS4? ¿MercaERP? ¿NAV? ¿Combinación?

11. **Q-MI3 / Q-MI3-QUE-ES / Q-INTERFACE-MI3** (alta) — Definición precisa: ¿qué es MI3? ¿Es bridge a MercaERP/IMS4? ¿Es un servicio específico? ¿Quiénes lo consumen?

12. **Q-MHS-COMO-CLIENTE** (alta) — MHS = primer cliente B2B vía WMSWebAPI. ¿Fecha go-live prevista? ¿Qué datos maestros van a escribir (productos, bodegas, propietarios)? ¿Qué transacciones van a leer (entradas, salidas, ajustes, todo)? ¿En qué fase está la integración hoy?

13. **Q-WSHHRN-AS-PROXY-BYB** (media) — En BYB, ¿WSHHRN actúa de proxy/adapter SOAP hacia NAV? ¿O la HH habla directo con NAV? ¿Cómo es el flujo HH → WSHHRN → NAV?

14. **Q-BYB-NO-DISPONIBLE-NAV-BD** (media) — La BD NAV de BYB ¿está accesible para el WMS de algún modo? ¿Por web service NAV? ¿Por replicación? ¿Solo escritura, solo lectura, ambas?

15. **Q-CHATGPT-SERVICE** (alta — sec colateral) — Existe `WSHHRN/ChatGPTService.vb` con la API key OpenAI hardcoded. ¿Está en producción este servicio? ¿Qué hace? ¿Quién lo usa? ¿Se puede desactivar/borrar mientras se rota la key?

---

## Bloque 3 — Portal CEALSA / DMS (pri alta–media)

16. **Q-PORTAL-STACK** (alta) — El portal para propietarios CEALSA — ¿está construido? ¿En qué tecnología (Angular, React, Razor, Blazor, otro)? ¿En qué repo de Azure DevOps vive el código?

17. **Q-PORTAL-AUTH** (media) — ¿Cómo autentica al propietario en el portal? ¿Usuarios CEALSA-side (admin del 3PL crea cuentas)? ¿Usuarios del propietario directamente (self-service)? ¿Federación con su correo corporativo?

18. **Q-PORTAL-MULTITENANCY-DECISION** (media) — Erik mencionó dos opciones: 1-BD-por-cliente vs 1-BD-compartida-multi-tenant. ¿Cuál se eligió? ¿O sigue abierta?

19. **Q-CEALSA-REPO** (media) — En Azure DevOps existe el repo `CEALSA` pero está vacío (0 MB, sin ramas). ¿Iba a contener el portal? ¿El DMS específico? ¿Se canceló y quedó?

20. **Q-DMS-DESTINO-CLOUD** (media) — El DMS replica on-prem → cloud. ¿Qué tipo de target en cloud usan? SQL Server en VPS, Azure SQL, contenedor en Docker, otro?

21. **Q-DMS-PROPIETARIO-FILTER** (media) — ¿Dónde se configura qué propietarios se replican? ¿Hay flag tipo `propietarios.replicar_a_cloud`? ¿Está en `App.config` del DMS? ¿Tabla de configuración?

22. **Q-TOMWEB-PROPOSITO** (media) — El repo `TOMWeb` es ASP.NET clásico + PHP (`.htaccess`, `index.php`, carpeta `xampp`, `login`, `en/`, `es/`). ¿Qué es? ¿Portal corporativo de PrograX24? ¿Tienda de licencias? ¿Algo legacy independiente del WMS?

23. **Q-WMS5-VACIO** (baja) — El repo `TOMWMS5` existe en Azure DevOps pero está vacío (0 MB, sin ramas). ¿Iba a ser una versión 5? ¿Migración fallida? ¿Placeholder reservado?

24. **Q-TOMWMSUX-VS-WMSPORTAL** (baja) — Si hay otros portales/UIs además del CEALSA-portal y TOMWeb, ¿cuáles son? ¿Para qué?

---

## Bloque 4 — Stock_jornada (pri alta — regulatorio)

25. **Q-STOCK-JORNADA-CONSUMER** (alta) — La tabla `stock_jornada` se cierra diariamente. ¿Quién consume el resultado? ¿Reporte para SAT? ¿Para SIB? ¿Aseguradora? ¿El portal CEALSA? ¿Solo auditoría interna? ¿Combinación?

26. **Q-STOCK-JORNADA-MAMPA** (alta) — `stock_jornada` tiene 21.883 filas en MAMPA. En CEALSA está activa también. En BECO/K7/BYB la tabla existe pero vacía. ¿Por qué MAMPA la usa? ¿Es 3PL regulado también, o se activó por defecto en `dev_2028_merge`?

27. **Q-STOCK-JORNADA-PROCESO** (media) — ¿Qué componente cierra la jornada? ¿Stored procedure batch nocturno? ¿Servicio Windows? ¿Trigger? ¿Manual desde algún form del BOF?

28. **Q-STOCK-JORNADA-DESFASE** (baja) — Existe tabla `stock_jornada_desfase`. ¿Cómo se calculan los desfases? ¿Qué se hace cuando hay desfase (alerta, ajuste automático, intervención manual)?

---

## Bloque 5 — DALCore / Generador de código (pri media)

29. **Q-DALCORE-CONSUMERS** (alta) — DALCore en .NET Core ¿quién lo invoca hoy en código corriendo? ¿Solo WMSWebAPI? ¿También DMS? ¿Algún componente más? ¿Hay app stand-alone que use DALCore directamente?

30. **Q-GENERADOR-UBICACION** (alta) — El generador de código BE/LN-base/LN-partial — ¿dónde vive el código? ¿Es proyecto en TOMWMS_BOF? ¿App standalone separada? ¿Quién lo mantiene (Efren, Erik, ambos)?

31. **Q-GENERADOR-INPUTS** (media) — ¿Cómo se especifica el contrato de generación? ¿Plantillas T4? ¿Templates manuales en código? ¿Configuración por archivo? ¿Hace introspección directa al schema SQL Server?

32. **Q-GENERADOR-WEB-VARIANTE** (baja) — Para el futuro web BOF, ¿se planea extender el generador a una tercera variante que genere UI Web (Blazor/Razor/SPA components)? ¿O esa parte se hace manual?

---

## Bloque 6 — Multi-tenancy 3PL (pri media)

33. **Q-PROPIETARIO-AGNOSTICO** (media) — En clientes no-3PL (BECO/BYB/K7), ¿usan la tabla `propietarios` con un único registro "default"? ¿Ignoran la tabla? ¿Tienen un patrón distinto?

34. **Q-CEALSA-PREFACTURA-MODELO** (media) — En el modelo CEALSA aparecen tablas/módulos de prefactura. ¿Cómo se calcula la prefactura del 3PL al propietario? ¿Por servicios (almacenaje, picking, despacho)? ¿Tarifa por unidad/peso/volumen? ¿Manual o automática?

35. **Q-CEALSA-RH-HR** (baja) — Aparecen tablas RH/HR en CEALSA. ¿El WMS tiene un módulo de recursos humanos integrado? ¿O son tablas legacy de otro sistema que comparten DB?

---

## Bloque 7 — Recepción / verificación / capabilities (pri media)

36. **Q-CAPABILITY-FLAG** (alta) — ¿Existe un patrón centralizado de "capability flags por cliente" (algo tipo `Cliente.IsBYB`, `Cliente.UsaVerificacion`, `Cliente.GeneraLP`)? ¿O las diferencias por cliente están dispersas en `if Cliente.Codigo = "BYB" Then` por todo el código? Si hay tabla central de flags, ¿dónde?

37. **Q-LICENCIAS-MODELO-NUEVO** (media) — Aparece referencia a un "modelo nuevo de licencias" en 2028. ¿Qué cambia respecto al modelo legacy? ¿Schema nuevo? ¿Lógica nueva? ¿Quiénes lo consumen?

---

## Bloque 8 — Web BOF (pri baja — futuro)

38. **Q-WEB-BOF-STACK + Q-WEB-BOF-TIMELINE** (baja) — Para la migración futura BOF WinForms → Web: ¿qué tecnología tiene Erik en mente (Blazor Server, Blazor WASM, React + .NET API, Razor Pages, otro)? ¿Cuándo se prevé arrancar (después de cerrar 2023→2028 o en paralelo)?

---

## Bloque 9 — Si tenés tiempo extra (pri baja, agradable saberlo)

A. **Q-K7-ML-MODELO / Q-PERCEPTRON-USO-REAL** — En K7 aparece referencia a "perceptron" en código. ¿Hay un modelo ML real en producción? ¿Para qué (predicción de demanda, optimización de picking, otro)?

B. **Q-MAMPA-MERMA-CARNE-FLUJO** — En MAMPA hay caso de "merma de carne" con flujo específico. ¿Cómo es? ¿Tabla / SP / form dedicado?

C. **Q-MAMPA-ERP** — ¿Cuál es el ERP de MAMPA?

D. **Q-MAMPA-CEDIS-PANTALLA-LEGACY** — ¿Hay pantalla CEDIS legacy específica de MAMPA? ¿Qué hace?

E. **Q-K7-OC-TIPOS** — ¿Cuántos tipos de OC (orden de compra) maneja K7? ¿Cuáles son?

F. **Q-WMS-EXE-CONFIG-EN-WSHHRN** — ¿Por qué la config del WMS está embebida en WSHHRN? ¿Es histórico? ¿Se planea desacoplar?

---

## Cómo me devolvés esto

Cuando respondas, podés:
- (a) Editar este archivo directamente y agregar tu respuesta debajo de cada pregunta con `**R:**`
- (b) Pegar las respuestas en el chat con el asistente, indicando el número de pregunta. Yo (asistente) las consolido y actualizo el brain
- (c) Hacer un audio/transcripción y pegarme el texto crudo, yo lo proceso

**Si una respuesta requiere consultar a Erik para homologar**: marcala como `**R: (consultar Erik) — mi opinión preliminar es ...`. Así sabemos qué queda pendiente.

**Si no sabés**: `**R: no sé`. Eso ya es información — significa que hay que ir al código o preguntar a Erik.

---

## Notas para Carolina sobre el estado del brain

- El brain está hoy en `/tmp/wms-brain/` (efímero del contenedor) + remoto en GitHub `ejcalderongt/tomwms-replit-client-automate` rama orphan `wms-brain`.
- 5 archivos principales, ~2.700 líneas:
  - `_index/INDEX.md` — historia cronológica por waves
  - `_index/CONCEPT_MAP.md` — vista temática por dominio (lectura recomendada para arrancar)
  - `agent-context/RAMAS_Y_CLIENTES.md` — disclaimer global de ramas
  - `agent-context/AZURE_ACCESS.md` — cómo accedo a Azure DevOps
  - `code-deep-flow/00-mapa-de-cajas.md` — diagrama de componentes
  - `code-deep-flow/02-portal-y-dms.md` — historia DALCore + portal + DMS (Wave 6.1)
  - `code-deep-flow/traza-001-license-plate.md` — primera traza de invariante (LP)
- Próxima traza prevista: `traza-002-control_lote_y_vencimiento.md` (después de Wave 6.2 quick-wins).
- Hay 1 issue de seguridad CRÍTICA: `Q-SEC-OPENAI-KEY-LEAK` — la API key de OpenAI está hardcoded en `WSHHRN/ChatGPTService.vb` y existe en `dev_2023_estable` y `dev_2028_merge`. Tu respuesta a la pregunta 15 ayuda a definir el plan de remediación.

---

## Bloque 10 — NUEVO (post Wave 6.2 quick wins, 2026-04-28)

Estas 5 preguntas surgieron al cerrar la Wave 6.2 con SQL/GREP. Agregadas después de tu primera vuelta — si ya respondiste el resto, contestá estas como bonus.

39. **Q-PORTAL-AUTH-CREDENCIALES-EN-PROPIETARIOS** (alta) — La tabla `propietarios` de CEALSA tiene dos columnas: `codigo_acceso` (nvarchar 100) y `clave_acceso` (nvarchar 100). ¿Son las credenciales del portal del propietario? Si sí: ¿`clave_acceso` está hasheada o en claro? ¿Quién las administra (CEALSA-side admin, self-service del propietario, ambos)? ¿Hay tabla de tokens/sesiones aparte o el portal hace login básico contra estas dos cols?

40. **Q-GENERADOR-ABANDONO** (alta) — Conté 626 archivos `clsLn*.vb` en el repo BOF, pero solo **57 tienen `clsLn*_partial.vb`** asociado. Es decir: el 90% de las clases base están sin partial y se editan in-place (con código de negocio adentro: `Imports DevExpress`, lógica async, etc). ¿El generador se usa hoy o quedó abandonado? Si se usa, ¿por qué no se respeta la convención partial? ¿Se planea un refactor masivo para mover el custom code de las bases a `_partial`, o ya el generador sirve solo para casos nuevos?

41. **Q-LP-CORRELATIVO-NAV-FORMATO** (media) — En BYB los LPs largos tienen formato tipo `BA00020002040400072334`. Parece: `BA` (prefijo) + `0002` (¿bodega?) + `0002` (¿propietario?) + `040400072334` (correlativo NAV). ¿Cuál es la descomposición exacta de la estructura? ¿Quién genera el LP — la HH, el WMS o NAV?

42. **Q-RAMA-MASTER-DEV2025-DUPLICADAS** (baja) — En el repo BOF, las ramas `master`, `dev_2023_estable` y `dev_2025` apuntan al **mismo commit** (`1f5cc2c4`). Son alias. ¿Por qué se mantienen las 3 vs borrar `master` y `dev_2025`? ¿`dev_2025` tuvo vida propia en algún momento del pasado y luego se sincronizó? ¿Hay convención de nombrado interna (master = release oficial, dev_2023_estable = trabajo activo, dev_2025 = checkpoint anual)?

43. **Q-LP-DATA-DIRTY-MIN** (baja) — En BECO hay 8 filas de stock con `lic_plate = "0"`. ¿Sabes si es data sucia de una migración antigua? ¿Se puede limpiar (UPDATE a NULL o borrar)? ¿O hay algún caso de negocio donde un LP "0" tenga sentido?

---

## Resumen actualizado

- Total preguntas: **43** (38 originales + 5 nuevas Wave 6.2)
- 9 bloques temáticos + 1 bonus
- Q-* alta prioridad bloqueantes: 13 (de 43)
- Q-* críticas: 1 (Q15 ChatGPT-Service / OpenAI key leak)

---

## Bloque 11 — NUEVO (post Wave 7, 2026-04-29)

Estas 11 preguntas surgieron al mapear las dos BDs nuevas del holding IDEALSA (MERHONSA Honduras + MERCOPAN Panamá) y al destrabar la lógica de implosión + merge LP de la migración 2028.

### Holding IDEALSA

44. **Q-IDEALSA-OTROS-PAISES** (alta) — Hoy mapeamos MERHONSA y MERCOPAN. ¿Hay más filiales del holding (Guatemala, El Salvador, Costa Rica, Nicaragua) que también deberían tener BD propia con schema espejo? Si sí: ¿están en planning, en migración, o ya viven en otro server distinto al EC2?

45. **Q-IDEALSA-MASTER-DATA** (alta) — Las dos filiales comparten 98% de schema (315 de ~320 tablas). ¿Cómo se sincroniza el master de productos / propietarios entre ellas? Opciones: (a) cada filial es independiente y carga su master; (b) hay un job de sync programado desde una BD master de IDEALSA; (c) el ERP corporativo (NAV/SAP) hace de fuente única y empuja a cada WMS local.

46. **Q-MERHONSA-PARADOJA-LP** (media) — En `i_nav_config_enc` MERHONSA tiene `genera_lp=False` pero `implosion_automatica=True` y `explosion_automatica=True`. ¿Cómo se implosiona si no se generan LPs propios? Hipótesis: hereda los LPs del ERP (NAV/SAP) y solo los manipula. ¿Es correcto?

47. **Q-COCINERO-ROLE-PANAMA** (media) — MERCOPAN tiene 2 tablas únicas: `StockCocinero` y `stock_BK_Cocinero`. ¿Qué hace exactamente el rol "cocinero" en MERCOPAN? Hipótesis: preparación de mezclas o pre-armado de SKUs antes de salida (kitting, repacking). ¿Es propio de Panamá o se replicará al resto?

48. **Q-SCHEMA-PRODUCTOS-MERHONSA** (media, técnica) — En MERHONSA y MERCOPAN, las tablas `propietarios` y `stock` están en schema `dbo.` pero `empresas`, `bodegas`, `productos` no aparecen ahí. ¿En qué schema viven? ¿Hay un schema custom (`tomwms.`, `nav.`, `app.`, `mer.`)? ¿O todavía no fueron creadas?

### Implosión y merge LP (Wave 7)

49. **Q-MERGE-LP-LOG-PATRON** (alta) — En el cambio de ubicación 2028, cuando un LP `f(y)` mergea sobre LP destino `f(z)`, dijiste que el log queda en `trans_movimientos`. La tabla solo tiene UN campo `lic_plate` y UN campo `barra_pallet`. ¿Cómo se registra el par (origen, destino)? Opciones: (a) 2 filas con mismo `IdTransaccion`; (b) 1 fila con `lic_plate=f(z)` + `barra_pallet=f(y)`; (c) hay tabla auxiliar (¿`bitacora_lp`, `trans_lp_merge`?).

50. **Q-IMPLOSION-BOF-VISIBILIDAD** (media) — El form `TOMIMSV4/Transacciones/Implosion/frmImplosion.vb` (1332 líneas) existe en BOF y NO cambió entre 2023 y 2028. Vos comentaste que "implosión no existe en BOF". Hipótesis: el form está oculto via permisos de rol (`OpcionesMenu`) en clientes nuevos y solo Cumbre lo tiene visible. ¿Confirmás que el patrón es "existe en código pero deshabilitado por permiso"? ¿O hay otra explicación?

51. **Q-CLAVAUD-MEANING** (baja) — En `i_nav_config_enc` hay un flag `conservar_zona_picking_clavaud` (bit). ¿Qué es "Clavaud"? Persona, método, proveedor, tipo de zona, sigla? Importante para documentar correctamente la parametrización.

### Capabilities e integraciones (Wave 7)

52. **Q-SAP-CLIENTES** (alta) — Descubrí que `i_nav_config_enc` tiene flag `interface_sap=bit` además de los flags `*_nav`. ¿Qué clientes hoy tienen `interface_sap=True`? ¿Coexiste NAV + SAP en la misma bodega o es uno u otro? Si SAP es real, ¿qué módulos están integrados (recepción, ajustes, traslados)?

53. **Q-UMB-CONCEPT** (media) — En `i_nav_config_enc` hay flags `reservar_umbas_primero` y `convertir_decimales_a_umbas`. ¿UMB = "Unidad Mínima Básica"? ¿O es otra cosa? Quiero entender bien antes de documentar la lógica de conversión.

54. **Q-LP-ZOMBIE** (baja) — Si un despacho completo deja un LP con cantidad=0 (porque sale toda su mercadería), el LP queda histórico en `trans_movimientos` pero también en `stock` con cantidad=0. ¿Hay un job de purga periódica que elimine LPs zombies del `stock` o se mantienen forever? ¿Qué pasa con los reportes operativos cuando hay miles de LPs zombies?

---

## Resumen actualizado Wave 7

- Total preguntas: **54** (43 antes + 11 nuevas Wave 7)
- 11 bloques temáticos
- Q-* alta prioridad bloqueantes: ~16 (de 54)
- Q-* críticas: 1 (Q15 ChatGPT-Service / OpenAI key leak)
- **Q-* RESUELTAS en Wave 7** (sin tu input): 4 (Q-LP-WHEN-DESTROYED, Q-LP-MERGE-EN-DESTINO, Q-CAPABILITY-FLAG, Q-CONTROL-LOTE-TABLA)

---

## Bloque 12 — NUEVO (post Wave 8, 2026-04-29)

Estas 7 preguntas surgieron al destrabar la **estrategia Clavaud** y el **proyecto MI3** (gracias a la anécdota Marcelo Clavaud + revelación MI3 de Erik). Cubren los huecos del algoritmo de reserva 2028.

### Algoritmo de rotación

55. **Q-UPSR-MEANING** (media) — La tabla `tipo_rotacion` tiene 4 valores: 1=FIFO, 2=LIFO, 3=FEFO, **4=UPSR**. Los primeros 3 son convenciones logísticas estándar. ¿Qué significa UPSR? ¿Es un acrónimo (Ubicación Por Sistema de Rotación, Unidad Por Selección Random, Último Por Stock Recibido)? ¿En qué casos se usa y cómo ordena?

56. **Q-ROTACION-PRECEDENCIA** (media) — `IdTipoRotacion` vive en 3 tablas: `producto`, `bodega_ubicacion` e `i_nav_config_enc`. ¿Cuál tiene precedencia cuando los 3 tienen valor? Hipótesis: producto > ubicación > config bodega. ¿Confirmás? ¿O es al revés?

### Estrategia Clavaud — operativa

57. **Q-CLAVAUD-THRESHOLD** (alta) — El flag `conservar_zona_picking_clavaud` activa la estrategia de excluir picking cuando el pedido equivale a 1+ pallet completo. ¿El umbral es exacto (>= 1 pallet) o tiene margen (ej: >= 80% del pallet, o un valor configurable)? ¿Hay un parámetro `pallet_threshold_clavaud` por bodega o es código duro?

### MI3 (Módulo de Integración con Terceros)

58. **Q-MI3-VS-CEALSAMI3** (media) — Detecté DOS proyectos en el repo BOF: `MI3/` (WCF/SOAP services genéricos) y `CEALSAMI3/` (app de sync standalone específica para CEALSA). ¿CEALSA usa ambos o solo CEALSAMI3 reemplazó MI3? ¿Otros clientes tienen un `<CLIENTE>MI3/` propio (BECOMI3, BYBMI3, MAMPAMI3) o todos usan el MI3 genérico?

59. **Q-DSUBICSUG-ALGORITMO** (alta) — En `CEALSAMI3/DAL/Transacciones/Movimiento/dsUbicSug.xsd` hay un dataset llamado **"Ubicación Sugerida"**. Esto huele a motor de decisión "dónde colocar X producto al recepcionar". ¿Cuál es el algoritmo? ¿Considera proximidad, rotación, espacio disponible, perfil de movimiento? ¿Es exclusivo de CEALSA o se generalizó al resto?

### Variantes del algoritmo de reserva

60. **Q-RESERVA-MULTIPLE-VARIANTES** (media) — `clsLnStock_res_Partial.vb` tiene 13+ funciones de reserva con nombres muy específicos: `Reserva_Stock`, `Reserva_Stock_NAV_BYB`, `Reserva_Stock_Lista_Result`, `Reserva_Stock_Especifico` (×2 overloads), `Reservar_Stock_By_IdStock` (×3 overloads), `Reservar_Stock_By_Stock_Reem`, `Reemplazo_Automatico`, `Reemplazo_Automatico_Conso`, `ReemplazarAuto_IdStock_By_Stock`, `Reservar_Y_Reemplazar_Stock_By_IdStock`. ¿Cuándo se usa cada una? ¿Hay una matriz mental de "para tal cliente / tal caso de uso, llamar tal función"?

61. **Q-REEMPLAZO-AUTO** (alta) — Las funciones `Reemplazo_Automatico*` sugieren que el WMS puede REEMPLAZAR un stock ya reservado por otro distinto. ¿Cuándo dispara este flujo? Hipótesis: cuando llega stock más fresco después de la reserva, o cuando el stock reservado quedó dañado/bloqueado, el sistema busca alternativa automáticamente. ¿Es así? ¿Es transparente al operador o requiere confirmación?

---

## Resumen actualizado Wave 8

- Total preguntas: **61** (54 antes + 7 nuevas Wave 8)
- 12 bloques temáticos
- Q-* alta prioridad bloqueantes: ~16 (de 61)
- Q-* críticas: 1 (Q15 ChatGPT-Service / OpenAI key leak)
- **Q-* RESUELTAS en Wave 8** (con tu anécdota Clavaud + MI3): 4 (Q-CLAVAUD-MEANING, Q-MI3-IDENTIDAD, Q-UMB-CONCEPT, Q-PROPIETARIO-AGNOSTICO)

---

## Bloque 13 — NUEVO (post Wave 9, 2026-04-29)

Estas 13 preguntas surgieron al construir la **matriz reservas × cliente × canal** (`brain/wms-test-natural-cases/`). Erik destrabó cinco mecanismos nuevos en una conversación: política de ubicación específica por cliente, control de lote permitido, tolerancia de vida útil multi-nivel, lotes numéricos correlativos N+1, y explosión bajo demanda con tope de niveles. Al escribir cada caso natural aparecieron huecos finos que solo vos podés cerrar.

### Adapters `_From_<CANAL>` (Wave 9 reveló que existen 3 dedicados)

62. **Q-FROM-MI3-DIFF** (alta) — `Reserva_Stock_From_MI3` está en línea 18.192 de `clsLnStock_res_Partial.vb`. ¿Qué hace distinto del core `Reserva_Stock`? Hipótesis: convierte códigos de bodega/producto del ERP NAV a IDs WMS, normaliza unidades, y deja una bandera "origen=MI3" en algún campo de auditoría. ¿Cuál es la diferencia real?

63. **Q-FROM-SAP-DIFF** (alta) — `Reserva_Stock_From_SAP` está en línea 26.680 (la última función del archivo). ¿Maneja `sap_control_draft_*` para emparejar la reserva contra un draft SAP que todavía no se confirmó? ¿Hay clientes en producción con `interface_sap=True` o todavía es código en preparación?

64. **Q-FROM-REABASTO-DIFF** (alta) — `Reserva_Stock_From_Reabasto` (línea 9.856) se llama desde `clsLnTrans_reabastecimiento_log_partial.vb:729`. ¿Re-reserva el mismo stock que acaba de moverse a la zona de picking, marcando origen reabasto? ¿O dispara una nueva reserva contra el stock recién depositado en picking?

65. **Q-FROM-NAV-FALTA** (media) — Hay `_From_MI3`, `_From_SAP`, `_From_Reabasto` pero **no existe** `Reserva_Stock_From_NAV`. ¿Por qué? Hipótesis: NAV BYB usa `Reserva_Stock_NAV_BYB` que no es un adapter genérico sino una variante con lógica de LP largo correlativo NAV. ¿Otros clientes con NAV (no-BYB) caen al core directamente sin adapter?

### Restricción de ubicación por cliente (caso 05)

66. **Q-UBICACION-RESTRINGIDA-FALLBACK** (alta) — Si `cliente.IdUbicacionAbastecerCon=42` y la ubicación 42 se queda sin stock del producto pedido, ¿el WMS falla con error duro (lo que pseudocodeé) o degrada graciosamente a "buscar en otras ubicaciones con flag de excepción"? Si falla duro: ¿quién resuelve operativamente — supervisor con override manual, o se rebota el pedido al cliente?

67. **Q-UBICACION-RESTRINGIDA-REABASTO** (media) — El reabasto automático que mueve stock de granel a picking, ¿respeta `IdUbicacionAbastecerCon` cuando decide a qué ubicación de picking llevar el stock? ¿O el reabasto opera sin conocer al cliente final y la restricción se aplica solo al reservar?

### Lote numérico correlativo (caso 06)

68. **Q-LOTE-NUM-GAP** (alta) — Si por algún motivo el correlativo se rompe (ej: se recibió N=5, después N=7 sin pasar por N=6), ¿cómo reacciona la regla N+1 al despachar? ¿Bloquea hasta que aparezca el N=6, o el "último despachado" se mueve al máximo recibido y sigue desde ahí?

69. **Q-LOTE-NUM-RESET** (media) — El correlativo `Lote_Numerico` ¿es global por producto, o se segmenta por (producto × bodega), (producto × cliente), o (producto × propietario)? Importa para clientes 3PL donde un mismo producto puede tener correlativos paralelos por dueño.

### Tolerancia de vencimiento (caso 07)

70. **Q-TOLERANCIA-PRECEDENCIA** (alta) — Hay 4 niveles de configuración de tolerancia: `cliente_tiempos` (cliente × familia × clasificacion), `producto_estado.tolerancia_dias_vencimiento`, `producto.tolerancia`, `i_nav_config_enc.dias_vida_defecto_perecederos`. ¿Cuál es el orden real de precedencia? Hipótesis: cliente_tiempos > producto_estado > producto > config bodega (más específico gana). ¿Confirmás? ¿O hay alguna combinación (suma, max, min) en vez de override?

71. **Q-CLIENTE-TIEMPOS-NXNXN** (media) — La tabla `cliente_tiempos` parece ser un cubo N×N×N (cliente × familia × clasificacion) con dos columnas `Dias_Local` y `Dias_Exterior`. ¿La distinción local/exterior se refiere al origen del producto (importado vs nacional) o al destino del despacho (mercado interno vs exportación)?

### Explosión bajo demanda (caso 04)

72. **Q-EXPLOSION-NIVEL-MAX-COMPORTAMIENTO** (media) — `nivel_max_explosion` (en `producto` o `i_nav_config_enc`) limita cuántas veces se puede explotar una caja → unidades → sub-unidades. Cuando se llega al tope: ¿el WMS rechaza la siguiente explosión, sugiere otra ubicación, o pide confirmación al operador HH?

73. **Q-MERHONSA-TYPO-COLUMNA** (baja) — En MERHONSA detecté **dos columnas separadas** en la misma tabla con casi el mismo nombre: `explosion_automatica_nivel_max` y `explosio_automatica_nivel_max` (la segunda **sin la 'n'**). ¿Es bug histórico (typo en una migración + columna nueva agregada con nombre correcto y la vieja quedó), o las dos columnas se usan con propósitos distintos? Si es typo, ¿cuál es la "buena"?

### Reemplazo (caso transversal)

74. **Q-REEMPLAZO-PATH-BOF** (media) — Detecté que TODAS las funciones `Reemplazo_*` se invocan desde `WSHHRN/TOMHHWS.asmx.vb` (handheld) o desde `frmCantidadreemplazo.vb` (form BOF que abre el operador, no automático). ¿Existe algún flujo donde el WMS dispare un reemplazo SIN intervención humana — por ejemplo, batch nocturno que reasigna reservas vencidas o stock bloqueado? ¿O el reemplazo es 100% operador-driven (HH en piso o supervisor en BOF)?

### Estados del producto

75. **Q-PRODUCTO-ESTADO-RESERVABLE** (alta) — La tabla `producto_estado` tiene 14 estados (Conforme, Cuarentena, Avería, Vencido, Bloqueado, etc — la lista completa ya está en Wave 8). De esos 14: ¿cuáles habilitan reserva (entran al universo de candidatos), cuáles la bloquean explícitamente, y cuáles requieren autorización supervisor para reservar (ej: Cuarentena con liberación parcial)?

### Mantenibilidad del código

76. **Q-CLSLNSTOCK-RES-DESCOMPOSICION** (baja) — El archivo `clsLnStock_res_Partial.vb` tiene **26.680 líneas** y **18+ funciones**. Las funciones `_From_<CANAL>` están físicamente dispersas (línea 9.856, 18.192, 26.680). ¿Hay plan a futuro de descomponer este archivo en partials por canal (`clsLnStock_res_From_MI3.vb`, `clsLnStock_res_From_SAP.vb`, etc) o queda monolítico para no romper el generador de código?

---

## Resumen actualizado Wave 9

- Total preguntas: **76** (61 antes + 15 nuevas Wave 9 — se renumeró 75 a 76 al separar producto_estado y mantenibilidad)
- 13 bloques temáticos
- Q-* alta prioridad bloqueantes: ~24 (de 76)
- Q-* críticas: 1 (Q15 ChatGPT-Service / OpenAI key leak)
- **Q-* RESUELTAS en Wave 9** (con la conversación de casos naturales con Erik): **5**
  - `Q-EXPLOSION-EXISTE` → sí, vía flag `Explosion_Automatica` + `nivel_max_explosion`, disparada desde `clsSyncNavEnvioAlm.vb:1194,2750`
  - `Q-RESTRICCION-UBICACION-CLIENTE` → sí, vía `cliente.IdUbicacionAbastecerCon` (filtro forzado al reservar)
  - `Q-LOTE-NUM-EXISTE` → sí, vía `cliente.control_ultimo_lote` + tabla `trans_re_det_lote_num.Lote_Numerico` correlativo
  - `Q-TOLERANCIA-MULTI-NIVEL` → sí, cascada de 4 niveles (cliente_tiempos × producto_estado × producto × config)
  - `Q-FROM-CHANNEL-FUNCTIONS` → sí, existen `_From_MI3`, `_From_SAP`, `_From_Reabasto` como adapters formales en el mismo archivo monolítico

### Documentos nuevos del brain en Wave 9

- `brain/wms-test-natural-cases/00-INDEX.md` — índice de casos
- `brain/wms-test-natural-cases/01-matriz-funcion-cliente-canal.md` — matriz maestra
- `brain/wms-test-natural-cases/03-caso-clavaud-conservar-picking.md`
- `brain/wms-test-natural-cases/04-caso-explosion-cajas-a-unidades.md`
- `brain/wms-test-natural-cases/05-caso-restriccion-ubicacion-por-cliente.md`
- `brain/wms-test-natural-cases/06-caso-lote-numerico-correlativo.md`
- `brain/wms-test-natural-cases/07-caso-tolerancia-vencimiento.md`
- `brain/naked-erik-anatomy/000-prologo.md` — diario técnico-poético-sarcástico (rationale)
- `brain/naked-erik-anatomy/001-2026-04-29-clavaud-mi3-y-el-rio-desviado.md` — primera entrada

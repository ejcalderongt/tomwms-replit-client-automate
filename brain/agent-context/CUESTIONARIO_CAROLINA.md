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

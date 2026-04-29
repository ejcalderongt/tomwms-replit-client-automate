# Wave 10 — Carolina responde Bloques 1 y 2 del cuestionario

**Fecha**: 2026-04-30
**Trigger**: Carolina envio respuestas escritas a las 11 preguntas de
los Bloques 1 (Ramas y migracion 2023→2028) y 2 (Integraciones) del
cuestionario.

**Resultado**: 10 nuevos `learnings/L-*`, 1 entry en `naked-erik-anatomy/`,
11 Q-* RESUELTAS, ~14 Q-* nuevas derivadas (bloque 14 cuestionario).

---

## Resumen ejecutivo

Carolina respondio 11 preguntas. **Una de ellas (Q-NAV-PREFIJO) reescribe
la interpretacion de TODO el codigo del WMS con prefijo Nav**. Otras
revelan: orden definido (sin fechas) de migracion 2028, root cause #1
del salto 2028 (IDENTITY autoincremental), descubrimiento de 2 clientes
nuevos (CLARISPHARMA + MERCOSAL), bug historico de implosion siendo
fixeado en rama Cumbre dedicada.

---

## Q-* RESUELTAS (11)

### Bloque 1 — Ramas y migracion

| ID | Resolucion | Learning derivado |
|---|---|---|
| Q-MIGRACION-2023-A-2028 (parte 1) | Orden: BYB+IDEALSA+INELAC paralelo (QAS) → Cumbre → BECO+CLARISPHARMA → MHS. Sin cronograma. Despues de MAMPA (PROD): MHS al final. | L-027 |
| Q-MIGRACION-2023-A-2028 (parte 2) | Carolina tiene script de migracion en su PC, **NO esta versionado**. Se guia por la "ultima vista actualizada" de SQL Server. Deuda tecnica detectada. | (no L-* directo, derivar Q-83) |
| Q-MIGRACION-2023-A-2028 (parte 3) | 4 breaking changes: (1) IDENTITY autoincremental (root cause concurrencia), (2) talla x color MAMPA, (3) migracion Interface → WebAPI, (4) inventario RFID. | L-026 |
| Q-DEV2025-PROPOSITO | Rama intermedia para feature talla x color MAMPA. Mergeada a `dev_2028_merge`, ahora historica. | L-030 |
| Q-MASTER-PROPOSITO | Release oficial congelada. NO se trabaja ahi. Trabajo activo en `dev_2023_estable` o `dev_2028_merge`. | L-031 |
| Q-HH-RAMAS-25 | Ramas por cliente HH son historicas, modelo no fue sostenible. Recomendacion: eliminar con cleanup. | L-032 |
| Q-MERCOPAN-MERCOSAL | MERCOPAN ya mapeada en Wave 7. **MERCOSAL es nuevo**, BD recien pasada a Erik. Probable El Salvador, probable holding IDEALSA. | L-034 |
| Q-CUMBRE-RAMA-DEDICADA | Rama dedicada para fix de **bug historico de implosion**: no validaba estado/ubicacion, permitia mismo LP en distintas ubicaciones/estados. | L-029 |

### Bloque 2 — Integraciones

| ID | Resolucion | Learning derivado |
|---|---|---|
| Q-NAV-PREFIJO-CEALSA | ⭐ **EL MAS IMPORTANTE**: el prefijo `Nav` en codigo NO es Microsoft Dynamics NAV. Es sinonimo historico de "ERP / Interface" reusado desde el primer cliente (BYB). Cualquier ERP usa la misma infraestructura `Nav*`. | L-025 |
| Q-CEALSA-CEALSASYNC-ERP | ERP de CEALSA es custom desarrollado por **ARITEC** (consultora externa). En proceso de migracion a **Odoo**. | L-028 |
| Q-MI3-QUE-ES | MI3 = "Modulo de Integracion con Terceros" — proyecto WCF/SOAP en `TOMWMS_BOF/MI3/`. **Consumers actuales: IDEALSA + INELAC + MERCOPAN**. CEALSA tiene `CEALSAMI3/` standalone propio (diferente). | L-028 |

---

## Hallazgos brutales (mas alla de las preguntas directas)

1. **CLARISPHARMA es cliente nuevo descubierto** — probable familia
   farmacia (Becofarma + Clarispharma juntas en migracion). No estaba
   en el mapa.
2. **MERCOSAL es probable tercera filial holding IDEALSA** (Honduras +
   Panama + El Salvador). El holding tiene mas alcance del que se
   veia en Wave 7.
3. **K7 (KILLIOS) NO aparece en el orden de migracion** — sigue en
   `dev_2023_estable` indefinidamente o se mergea con BECO. Hueco que
   no se cubrio.
4. **CEALSA NO aparece en el orden de migracion** — probable que se
   posterga hasta que su lado del ERP estabilice (migracion ARITEC →
   Odoo).
5. **WMS tiene 3 modelos distintos de integracion ERP**:
   - Sync EXE ClickOnce (BYB, BECO, K7, MAMPA)
   - MI3 SOAP services (IDEALSA, INELAC, MERCOPAN)
   - App de sync custom standalone (CEALSA via CEALSAMI3)
6. **Bug de implosion existe en MAMPA prod** — `dev_2028_merge` lo
   tiene. El fix esta en `dev_2028_Cumbre`. Cuando mergee, MAMPA debe
   recibir update + posible auditoria de LPs corruptos historicos.
7. **Scripts de migracion DB no estan versionados** — Carolina los
   tiene en su PC. Riesgo de bus factor.
8. **RFID inventario es feature 2028 nueva** — sin documentar todavia
   en el brain. Q-* nueva.
9. **Migracion Interface → WebAPI es uno de los 4 breaking changes 2028**
   — confirma direccion estrategica WMSWebAPI como reemplazo gradual
   de WSHHRN.
10. **Ramas HH historicas por cliente deben borrarse** — antes hacer
    backup + scan-comments-tree-map para no perder comentarios firmados
    valiosos.

---

## Estado actual de migracion 2028 (al 2026-04-30)

```
PROD 2028:        MAMPA ✅
QAS 2028:         BYB 🔄  IDEALSA 🔄  INELAC 🔄
Pendientes:       Cumbre ⏳ (con fix implosion)
                  BECO ⏳ + CLARISPHARMA ⏳
                  MHS ⏳ (ultimo)

No mapeados:      KILLIOS (K7) ❓
                  CEALSA ❓ (posterga por ARITEC→Odoo)
                  MERCOPAN, MERHONSA ❓ (probable bajo IDEALSA)
                  MERCOSAL ❓ (probable bajo IDEALSA)
```

---

## Q-* nuevas (cuestionario bloque 14, 14 preguntas)

Ver `agent-context/CUESTIONARIO_CAROLINA.md` bloque 14 para detalles.
Sintesis:

- Q77 Q-CLARISPHARMA-FINGERPRINT (alta)
- Q78 Q-MERCOSAL-FINGERPRINT + holding confirmacion (alta)
- Q79 Q-CEALSA-ARITEC-ERP-NOMBRE (media)
- Q80 Q-CEALSA-MIGRACION-ODOO-TIMELINE (media)
- Q81 Q-DEV2025-COMMIT-DRIFT (baja, tecnica)
- Q82 Q-INELAC-EN-HOLDING-IDEALSA (media)
- Q83 Q-MIGRATION-SCRIPTS-VERSIONADO (alta — bus factor)
- Q84 Q-RFID-INVENTARIO-2028 (alta — feature 2028 sin documentar)
- Q85 Q-WEBAPI-INTERFACE-MIGRACION-2028 (alta — alcance del migracion)
- Q86 Q-IMPLOSION-CUMBRE-FIX-PROPAGAR (alta — riesgo MAMPA prod)
- Q87 Q-RAMAS-HH-CLEANUP (media — accion concreta)
- Q88 Q-KILLIOS-MIGRACION-PLAN (alta — hueco)
- Q89 Q-CEALSA-MIGRACION-2028 (media — depende de ARITEC→Odoo)
- Q90 Q-IDEALSA-FAMILIA-MIGRACION (media — MERCOPAN/MERHONSA/MERCOSAL juntos?)

---

## Artefactos generados

### Learnings nuevos (10)
- `learnings/L-025-meta-nav-no-es-dynamics.md` ⭐
- `learnings/L-026-arch-identities-2028-concurrencia.md`
- `learnings/L-027-proc-orden-migracion-2028.md`
- `learnings/L-028-integ-mi3-consumers-holding.md`
- `learnings/L-029-fix-implosion-cumbre-validacion.md`
- `learnings/L-030-arch-dev2025-intermedia-talla-color.md`
- `learnings/L-031-arch-master-congelada.md`
- `learnings/L-032-proc-ramas-cliente-hh-obsoletas.md`
- `learnings/L-033-client-clarispharma-existe.md`
- `learnings/L-034-client-mercosal-existe.md`

### Diario subjetivo
- `naked-erik-anatomy/002-2026-04-30-nav-ya-no-es-nav.md`

### Wave doc
- `_index/WAVE-10-CAROLINA-RESPONDE.md` (este archivo)

### Updates a archivos existentes
- `agent-context/CUESTIONARIO_CAROLINA.md` — marcar 11 RESUELTAS,
  agregar bloque 14 con 14 Q-* nuevas
- `_index/INDEX.md` — agregar Wave 10
- `_index/DISCOVERY_TREE.md` — agregar Wave 10 + actualizar snapshot
  + agregar L-025..L-034 a apendice A
- `agent-context/HOLDING_IDEALSA.md` — agregar MI3 confirmation +
  MERCOSAL como tercera filial probable

---

## Conexion con waves anteriores

| Wave | Conexion |
|---|---|
| Wave 1 | Fingerprints originales: agregar CLARISPHARMA + MERCOSAL al inventario clientes |
| Wave 4 | Heat-map params: `i_nav_config_enc` siendo "tabla NAV" se reinterpreta como "tabla generica de interface ERP" (L-025) |
| Wave 6 | Traza-001 LP: el Get_Nuevo_Correlativo_LicensePlate de NAV en BYB SIGUE siendo NAV literal — caso unico |
| Wave 6.1 | DALCore + DMS + Portal: matiza Q-NAV-PREFIJO ya no abierta |
| Wave 6.2 | Quick wins: el hallazgo "master + dev_2023 + dev_2025 mismo SHA" se matiza con L-030 (dev_2025 SI tuvo vida propia) |
| Wave 7 | Holding IDEALSA: + MERCOSAL como tercera filial probable + INELAC como consumer MI3 |
| Wave 8 | MI3: confirma que IDEALSA/INELAC/MERCOPAN son consumers, CEALSA NO. CEALSAMI3 es standalone aparte |
| Wave 9 | Casos naturales reserva: el bug de implosion (L-029) puede afectar la integridad asumida en los casos 03 (Clavaud) y 05 (restriccion ubicacion) |

---

## Pendientes

- Pedir a Erik / Carolina:
  - Acceso BD CLARISPHARMA + MERCOSAL para fingerprints
  - Versionar el script de migracion DB (resolver Q83)
  - Confirmar si Killios entra en orden de migracion (Q88)
  - Confirmar si MERCOSAL es El Salvador y holding IDEALSA (Q78)
- Para mi (agente):
  - Actualizar `config/boost-keywords.yml` del scanner con CLARISPHARMA y
    MERCOSAL en lista de clients
  - Crear stub `fingerprint/CLARISPHARMA.md` y `fingerprint/MERCOSAL.md`
    con los campos pendientes
  - Considerar si L-025 amerita revisar TODOS los archivos del brain que
    mencionen `Nav`, `nav`, `_nc`, `_NC` para refinar lenguaje
- Bloques 3-9 + 10-13 del cuestionario quedan pendientes de respuesta
  de Carolina (52 preguntas restantes aprox).

---

## Estado del brain post-Wave 10

| Metrica | Valor |
|---|---|
| Total Q-* formales | **90** (76 antes + 14 nuevas bloque 14) |
| Q-* resueltas acumuladas | **~32** (35%) |
| L-* aprendizajes | **24** (L-009..L-034 con saltos) |
| Hipotesis H-* | 30+ (sin cambios) |
| Fingerprints clientes | 5 APPLIED + 5 DEF_PEND (ahora incluye CLARISPHARMA + MERCOSAL) |
| Trazas formales | 1 (LP) — traza-002 control_lote pendiente |
| Casos naturales | 7 docs |
| Naked-erik entries | 2 (000 prologo + 001 clavaud + 002 nav-ya-no-es-nav) |
| Bloques cuestionario | 14 |
| Total .md brain | ~107 |

---
output_type: convencion
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-replit
authored_at: 2026-05-29T00:00:00-06:00
ratificado_por: Erik Calderon (PrograX24)
ratificado_at: 2026-05-29T00:00:00-06:00
---

# Convención de Versionado — TOM WMS

## Mapa de versiones por capa

| Capa | Archivo | Variable / Constante | Versión actual |
|---|---|---|---|
| **HH (Android)** | `app/build.gradle` | `versionName` | `8.9.0` |
| **BOF (TOMIMSV4)** | `/TOMIMSV4/TOMIMSV4/Clases_AP/m_Global.vb` | `gVersionApp` | `8.6.6` |
| **WS (WSHHRN)** | `/WSHHRN/TOMHHWS.asmx.vb` | `WS_VERSION` (Public Const) | `8.9.0` |
| **WS ref en BOF** | `/TOMIMSV4/TOMIMSV4/Clases_AP/m_Global.vb` | `gVersionWS` | `8.9.0` |
| **BD** | `/TOMIMSV4/TOMIMSV4/Clases_AP/m_Global.vb` | `gVersionBD` | `1` |
| **Fecha versión** | `/TOMIMSV4/TOMIMSV4/Clases_AP/m_Global.vb` | `gFechaVersion` | `2026-05-29` |

---

## Reglas de bump

### HH (Android)
- **Cada fix o mejora pushed → bump obligatorio de `versionName`.**
- Formato: `MAYOR.MENOR.PATCH` (ej. `8.9.0` → `8.9.1` → `8.10.0`).
- También bump de `versionCode` (entero, incrementar en +1).
- El `[VERSION]` log en `frm_preparacion_packing.java` (`onCreate`) confirma en logcat qué versión corre el dispositivo.

### WS (WSHHRN / TOMHHWS.asmx.vb)
- `WS_VERSION` (Public Const en clase `TOMHHWS`) se actualiza con cada deploy del WS.
- Por convención: **alinear `WS_VERSION` con el `versionName` HH del mismo sprint/release**.
- `gVersionWS` en BOF `m_Global.vb` debe coincidir con `WS_VERSION`.

### BOF (TOMIMSV4)
- `gVersionApp` bump cuando hay fixes/mejoras en la capa BOF/DAL/Entity.
- `gFechaVersion` se actualiza a la fecha del commit.

---

## Inline tag obligatorio — formato

Al final de **cada fix o mejora** (en código y en mensaje de commit), incluir el tag inline:

### En código (VB.NET / Java):
```
' #EJC20260529 fix|mejora(area): descripción — HH v8.9.0 | WS v8.9.0 | BOF v8.6.6 [CLIENTE]
```

```java
// #EJC20260529 fix|mejora(area): descripción — HH v8.9.0 | WS v8.9.0 | BOF v8.6.6 [CLIENTE]
```

### En mensaje de commit:
```
#EJCDDMMAAAA fix|mejora(area): resumen corto
fix: detalle del fix
mejora: detalle de la mejora
Cliente: KILLIOS|LA_CUMBRE|BECOFARMA|MAMPA|CEALSA|TODOS | HH vX.Y.Z | WS vX.Y.Z | BOF vX.Y.Z
```

### Campos del tag:
| Campo | Descripción | Ejemplos |
|---|---|---|
| `fix\|mejora` | Tipo de cambio | `fix`, `mejora`, `chore`, `refactor` |
| `area` | Módulo o flujo | `packing-HH`, `recepcion-BOF`, `picking-WS` |
| `descripción` | Resumen legible | `NPE processListUbic cuando pick=null` |
| `HH vX.Y.Z` | Versión HH en que se pushó | `HH v8.9.0` |
| `WS vX.Y.Z` | Versión WS en que se pushó | `WS v8.9.0` |
| `BOF vX.Y.Z` | Versión BOF en que se pushó | `BOF v8.6.6` |
| `[CLIENTE]` | Si aplica solo a un cliente | `[LA_CUMBRE]`, `[KILLIOS]`, omitir si es general |

---

## Historial de versiones (releases significativos)

| Fecha | HH | WS | BOF | Hito |
|---|---|---|---|---|
| 2026-05-29 | 8.9.0 | 8.9.0 | 8.6.6 | Trazas VERSION+FLOW packing; fix NPE pick null; refactor mensajes |
| 2026-05-28 | 8.6.7 | — | 8.6.5 | Fix packing La Cumbre: fecha_packing, LP override, idle stuck |

---

## Logs de diagnóstico en logcat (HH)

| Tag | Momento | Info |
|---|---|---|
| `[VERSION]` | `onCreate` de `frm_preparacion_packing` | `v=8.9.0 picking=N pedido=M` |
| `[FLOW]` | cada `wsCallBack` | `case=N v=8.9.0` |

Filtrar con: `adb logcat -s PKG_T`

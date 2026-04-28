---
protocolVersion: 1
id: L-015
title: WMS se distribuye via ClickOnce con TODAS las interfaces empaquetadas; dispatch runtime via i_nav_config_enc.nombre_ejecutable
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-28T23:58:00Z
target:
  codename: tomwms-arquitectura
  environment: cross-cliente
relatedQuestions: [H-028]
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/architecture/adr/ADR-006-multi-environment-config.md
  - brain/clients/becofarma.md
status: closed
priority: high
tags: [arquitectura, deployment, clickonce, interfaces, dispatch-dinamico, multi-cliente]
---

## Que aprendimos

El **modelo de despliegue del WMS** es:

1. **Distribucion**: ClickOnce. Cada cliente instala el WMS en sus
   maquinas (BOF y dispositivos HH) via ClickOnce, con auto-update
   contra el publisher de PrograX24.

2. **Empaquetado**: en cada instalacion ClickOnce **viajan TODAS las
   interfaces** del catalogo:
   - `MI3.exe` (WCF para grupo Aurora: Idealsa/Merhonsa/Mercosal/Mercopan/Inelac)
   - `NavSync.exe` (push NAV: BYB)
   - `SAPBOSync.exe` (push SAP B1 generico: Becofarma)
   - `SAPSYNCKILLIOS.exe` (push SAP B1 customizado: Killios)
   - `SAPSYNCMAMPA.exe` (Mampa)
   - `SAPSYNCCUMBRE.exe` (La Cumbre)
   - (y la WebAPI nueva cuando se sume)

3. **Dispatch runtime**: el WMS lee `i_nav_config_enc.nombre_ejecutable`
   en el arranque/contexto operativo y **selecciona cual ejecutable
   abrir/invocar** segun el cliente. Ejemplos confirmados:

   | Cliente | i_nav_config_enc.nombre_ejecutable |
   |---|---|
   | BECOFARMA | `SAPBOSync.exe` |
   | (otros, por confirmar) | (a relevar) |

4. **Convivencia .vbproj ↔ .exe**:
   - `SAPSYNC.vbproj` (y los demas `.vbproj`) son los **proyectos
     fuente VB.NET** en el repo del WMS.
   - `SAPBOSync.exe` (y los demas `.exe`) son los **binarios
     compilados** que viajan en el ClickOnce.
   - El nombre del binario puede diferir del nombre del proyecto
     (`SAPSYNC.vbproj` -> `SAPBOSync.exe`). La fuente de verdad
     **runtime** es la columna `nombre_ejecutable`. La fuente de verdad
     **del codigo** es el `.vbproj`.

## Implicancias

### IMP-1: la WebAPI nueva (.NET 10) puede sumarse sin romper el modelo

Para un cliente que migre a la WebAPI, basta con que su
`i_nav_config_enc.nombre_ejecutable` apunte al binario nuevo (o quede
nullo si el dispatch lo interpreta como "usar WebAPI"). El resto del
WMS no cambia. Es una **estrategia de migracion incremental por
cliente**.

### IMP-2: no hay riesgo de "interface no instalada"

Como TODAS las interfaces viajan en cada ClickOnce, no existe el caso
"el cliente no tiene la interface instalada". Lo unico que cambia entre
clientes es **cual se invoca** (config-driven), no **cual esta presente
en disco** (todas estan).

### IMP-3: actualizar interfaces-erp-por-cliente.md

El doc actual presenta `MI3 / NavSync / SAPSYNC*` como si fueran
modulos desplegados por separado por cliente. La realidad: **se
despliegan juntos**, el cliente solo decide cual usar via config.
Corregir.

### IMP-4: para diagnostico, leer `nombre_ejecutable` confirma cual interface aplica

Comando util: `SELECT cliente, nombre_ejecutable FROM i_nav_config_enc`
en cada BD para mapear la matriz cliente↔binario sin depender del
nombre del proyecto fuente.

### IMP-5: cierre de H-028 (outbox 85% pendiente en BECOFARMA)

Como SAPBOSync.exe SI corre y SI esta en el server productivo del
cliente (Erik confirmado), el 85% pendiente que veo en la copia
diagnostica del 28-abr-2026 **NO indica caida de la interface**. Es el
snapshot congelado del outbox al momento del backup. Ver L-014 para
detalle.

## Evidencia

- Q&A directa con Erik el 28-abr-2026:
  > Pregunta: "¿SAPBOSync.exe esta corriendo donde quedo la BD?"
  > Erik: "el ejecutable funciona en cada cliente de wms, wms se
  > instala via clickonce y en esa instalacion viajan todas las
  > interfaces, sin embargo en la i_nav_config_enc, establecemos como
  > se llama el ejecutable que debe abrir wms en dependencia del
  > cliente, entonces en el caso de becofarma si, sabosync.exe
  > funciona y esta en el servidor de ellos"

- BECOFARMA `i_nav_config_enc.nombre_ejecutable = SAPBOSync.exe`
  (snapshot 28-abr-2026).

## Accion en el brain

1. Crear seccion "Modelo de despliegue ClickOnce + dispatch dinamico"
   en `interfaces-erp-por-cliente.md`.
2. Aclarar la convivencia `.vbproj` (fuente) vs `.exe` (binario) en la
   tabla de clientes.
3. Cerrar H-028 con esta interpretacion en `_processed/`.

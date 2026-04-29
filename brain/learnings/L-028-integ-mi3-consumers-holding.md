# L-028 — INTEG: MI3 (Modulo Integracion Terceros) — consumers son IDEALSA + INELAC + MERCOPAN

> Etiqueta: `L-028_INTEG_MI3-CONSUMERS-HOLDING_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuestas Carolina a Q-MI3-QUE-ES + Q-CEALSA-CEALSASYNC-ERP

## Hallazgo

MI3 (Modulo de Integracion con Terceros) es un proyecto especifico en
el repo `TOMWMS_BOF/MI3/` que contiene **WebServices WCF/SOAP** para
intercambiar datos con TOMWMS desde ERPs externos.

**Consumers actuales de MI3** (al 2026-04-30):

| Cliente | Usa MI3 | Notas |
|---|---|---|
| **IDEALSA** | si | Confirmado por Carolina, Wave 10 |
| **INELAC** | si | Confirmado por Carolina, Wave 10 |
| **MERCOPAN** | si | Confirmado por Carolina, Wave 10 |
| MERHONSA | (probable) | Filial holding IDEALSA, comparte infraestructura |
| MERCOSAL | (probable) | Filial holding IDEALSA recien revelada (Wave 10) |
| **CEALSA** | NO | Tiene `CEALSAMI3/` propio, app standalone diferente |
| BYB | NO | Usa NavSync.exe + WSHHRN proxy a NAV literal |
| BECOFARMA | NO | Usa SAPBOSync.exe |
| KILLIOS | NO | Usa SAPBOSyncKillios.exe |
| MAMPA | NO | Usa SAPBOSyncMampa.exe |
| CUMBRE | NO (probable) | Q-CUMBRE-INTEGRACION abierta |
| MHS | NO | Es B2B-only via WMSWebAPI directo |

## Diferencias MI3 vs CEALSAMI3

| Atributo | `MI3/` | `CEALSAMI3/` |
|---|---|---|
| Tipo | Proyecto WCF/SOAP services genericos | App standalone de sync (EXE) |
| Consumers | IDEALSA, INELAC, MERCOPAN | Solo CEALSA |
| Endpoints | Bodega, Cliente, Direcciones, Documentos, Barras_Pallet | Producto, Categorias, Grupos, TablaConversion (todos con prefijo `clsSyncNav` — ver L-025) |
| Direccion | Bidireccional (ERP llama, WMS llama) | Mayormente WMS lee del ERP custom de ARITEC |
| Estado actual | Productivo, parametrizable por cliente | Productivo, pendiente reescribir cuando CEALSA migre a Odoo |

## ERP de CEALSA

Cita literal Carolina:

> "CEALSA utilizaba un ERP que no recuerdo su nombre, pero lo desarrollo
> ARITEC y en este momento tienen pensado migrarse para Odoo."

Datos confirmados:
- ERP custom desarrollado por **ARITEC** (proveedor / consultora externa).
- Nombre exacto del producto: **pendiente** (Q-CEALSA-ARITEC-ERP-NOMBRE).
- Migracion futura a **Odoo** (open source, modular, ampliamente usado en
  LATAM).
- Timing de migracion: **pendiente** (Q-CEALSA-MIGRACION-ODOO-TIMELINE).

## Implicaciones

### 1. Patron de integracion por cliente
Hoy el WMS soporta **3 modelos distintos de integracion ERP**:

| Modelo | Mecanismo | Clientes | Pros | Contras |
|---|---|---|---|---|
| **Sync EXE ClickOnce** | App periodica que polea el ERP y empuja al WMS | BYB (NavSync), BECO/KILLIOS/MAMPA (SAPBOSync*) | Simple, no requiere infra del cliente | Latencia de polling, no real-time |
| **MI3 SOAP services** | ERP llama webservices del WMS via WCF | IDEALSA, INELAC, MERCOPAN | Real-time, bidireccional | Requiere que el ERP tenga capacidad de llamar SOAP |
| **App de sync custom** | Standalone con logica adhoc | CEALSA (CEALSAMI3) | Maxima flexibilidad para casos raros | Mas codigo a mantener |

### 2. Migracion de CEALSA a Odoo
Cuando CEALSA migre su ERP a Odoo:
- **CEALSAMI3 va a quedar obsoleta** salvo que se reuse el codigo
  pero apuntando a Odoo (refactor pesado).
- **Opcion estrategica A**: reescribir CEALSA encima de MI3 SOAP
  generico (alinear con IDEALSA/INELAC). Reduce mantenimiento.
- **Opcion estrategica B**: usar el conector estandar Odoo<->WMS
  (REST/XMLRPC). Mas moderno, pero requiere build adapter custom en
  WMSWebAPI.
- Decision pendiente (ADR-* a abrir cuando CEALSA confirme timing).

### 3. Holding IDEALSA y MI3
**Confirmacion fuerte**: IDEALSA + INELAC + MERCOPAN comparten MI3.
Si MERHONSA y MERCOSAL son del mismo holding (probable), tambien
deberian estar en MI3. Esto **refuerza la hipotesis de Wave 7** de que
el holding tiene infraestructura tecnica compartida.

Pregunta abierta: ¿INELAC es realmente del holding IDEALSA, o es
cliente independiente que solo casualmente usa MI3? La taxonomia
holding es pertinente para entender governance.

## Cierra Q-*

- `Q-MI3 / Q-MI3-QUE-ES / Q-INTERFACE-MI3` (Bloque 2, alta) — DEFINICION
  COMPLETA: proyecto WCF/SOAP en `TOMWMS_BOF/MI3/`, consumers IDEALSA +
  INELAC + MERCOPAN.
- `Q-CEALSA-CEALSASYNC-ERP` (Bloque 2, alta) — ERP custom de ARITEC, en
  proceso de migracion a Odoo.

## Conexion con L-* anteriores

- **L-025** (Nav no es Dynamics): explica por que las clases dentro de
  `CEALSAMI3/` tienen prefijo `clsSyncNav*` aunque CEALSA no usa NAV.
  Es solo el naming reusado.
- **L-022** (patron naming sincronizador): los consumers MI3 NO tienen
  binario tipo `*Sync.exe` propio; usan los WebServices SOAP via su
  ERP. La columna `nombre_ejecutable` en `i_nav_config_enc` para esos
  clientes podria estar NULL o tener un valor placeholder. **A
  confirmar**.

## Q-* nuevas derivadas

- Q-CEALSA-ARITEC-ERP-NOMBRE (Q79 cuestionario nuevo)
- Q-CEALSA-MIGRACION-ODOO-TIMELINE (Q80)
- Q-INELAC-EN-HOLDING-IDEALSA (Q82)
- Q-MERCOSAL-EN-HOLDING-IDEALSA (Q78)
- Q-MI3-NOMBRE-EJECUTABLE-CLIENTES (¿que valor tiene
  `i_nav_config_enc.nombre_ejecutable` para IDEALSA/INELAC/MERCOPAN?)
- Q-CEALSAMI3-FUTURO (¿se reescribe sobre MI3 generico cuando CEALSA
  migre a Odoo?)

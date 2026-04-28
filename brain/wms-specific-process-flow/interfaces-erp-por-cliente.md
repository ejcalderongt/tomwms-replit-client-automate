# Interfaces de integracion ERP por cliente — patron polimorfico del outbox

> Respondio: Erik (ciclo 9b).
> Doc generado a partir de P-21b explicada por Erik.
> Critico para entender el contrato del bridge nuevo (reserva-webapi).

## TL;DR

El outbox `i_nav_transacciones_out` es **polimorfico**: la misma tabla
sirve a cualquier patron de integracion. Por cada cliente nuevo el equipo
PrograX24 disena (historicamente) una interface dedicada. La estrategia
moderna es **estandarizar via WebAPI** (modelo MHS) para que clientes con
equipo de desarrollo no necesiten codigo nuevo del lado WMS.

## Modalidades del patron

### Modalidad 1 — WCF expuesto por WMS (cliente hace pull)

WMS expone servicios WCF en el proyecto **`MI3.vbproj`** (Módulo de
Integración con Terceros). El ERP del cliente tiene una interface
fuertemente tipada que **consume** esos metodos cuando lo necesita.

- WMS es **pasivo** (servidor de datos).
- El ERP polea o dispara consultas al WMS segun su propia logica.
- Cubre todo: ingresos, salidas, ajustes (los ajustes se obtienen siempre
  por WCF, NO van a `i_nav_transacciones_out`).

### Modalidad 2 — Push automatico programado (WMS dispara `.exe`)

WMS ejecuta otro `.exe` propio de interface (ej. **`NavSync.vbproj`**) y le
pasa via parametros/args lo que tiene que sincronizar. La interface:
1. Lee las tablas WMS (incluyendo el outbox).
2. Consume los webservices del ERP del cliente (NAV en este caso).
3. Envia los registros (ingresos o salidas) que WMS interpreta para el outbox.

- Los **ajustes** NO siguen este flujo en este modelo: son **en demanda**
  (cuando se hace el despacho se dispara la interface indicando documento,
  transaccion, usuario, bodega).

### Modalidad 3 — Push dedicado por cliente SAP (WMS dispara `.exe` especifico)

Para cada cliente SAP B1 hay un **`.vbproj` dedicado**. El esqueleto es
similar al modelo NAV pero adaptado al cliente:

- `SAPSYNC.vbproj` / **`SAPBOSync.exe`** — Becofarma (el primero / generico). El binario corriente segun `i_nav_config_enc.nombre_ejecutable` es **`SAPBOSync.exe`**; aclarar si es el `.exe` compilado del `.vbproj` o un componente distinto
- `SAPSYNCKILLIOS.vbproj` — Killios
- `SAPSYNCMAMPA.vbproj` — Mampa
- `SAPSYNCCUMBRE.vbproj` — La Cumbre

### Modalidad 4 — WebAPI moderno (cliente consume API REST WMS)

Modelo nuevo: WMS expone una **WebAPI REST** que el ERP del cliente
consume tanto para **leer como para escribir**. No requiere codigo
nuevo por cliente del lado WMS — el cliente (con su equipo de desarrollo)
implementa el consumidor.

- Primer caso productivo: MHS (Molinos Harineros Sula).
- Tendencia evolutiva: las empresas con equipo de desarrollo migran a este
  modelo porque simplifica y optimiza el proceso.

## Mapeo cliente → interface (lo que tengo de Erik)

| Cliente | ERP | Modalidad | Componente WMS |
|---|---|---|---|
| **Idealsa** | Aurora (homemade SQL Server) | WCF (pull desde ERP) | `MI3.vbproj` |
| **Merhonsa** | (mismo grupo, posiblemente Aurora) | WCF (pull desde ERP) | `MI3.vbproj` |
| **Mercosal** | (mismo grupo, posiblemente Aurora) | WCF (pull desde ERP) | `MI3.vbproj` |
| **Mercopan** | (mismo grupo, posiblemente Aurora) | WCF (pull desde ERP) | `MI3.vbproj` |
| **Inelac** | (mismo grupo, posiblemente Aurora) | WCF (pull desde ERP) | `MI3.vbproj` |
| **BYB** | NAV (Microsoft Dynamics) | Push programado + ajustes en demanda | `NavSync.vbproj` |
| **MHS** (Molinos Harineros Sula) | (no especificado) | WebAPI moderno (lee y escribe) | nueva API REST |
| **Becofarma** | SAP B1 | Push dedicado por cliente | `SAPBOSync.exe` (corregido 28-abr-2026 desde `nombre_ejecutable`; ver `clients/becofarma.md`) |
| **Killios** | SAP B1 | Push dedicado por cliente | `SAPSYNCKILLIOS.vbproj` |
| **Mampa** | SAP B1 | Push dedicado por cliente | `SAPSYNCMAMPA.vbproj` |
| **La Cumbre** | SAP B1 | Push dedicado por cliente | `SAPSYNCCUMBRE.vbproj` |

## Implicaciones para reserva-webapi (bridge)

### IMP-01: el outbox debe seguir siendo polimorfico

Cuando reserva-webapi escriba en `i_nav_transacciones_out` desde el motor
nuevo, las transacciones deben respetar el formato actual sin asumir un
modelo de transporte particular. Cada interface del lado salida (MI3 / NavSync /
SAPSYNC*) seguira leyendo igual.

### IMP-02: el bridge en si mismo es de modalidad 4 (WebAPI)

reserva-webapi es la siguiente generacion del modelo MHS. Es coherente con
la direccion estrategica del producto. Nuevos clientes con equipo de
desarrollo apuntaran a este modelo en vez de heredar un .vbproj.

### IMP-03: ajustes de inventario NO pasan por el outbox (modalidades 1 y 2)

Para Idealsa (modalidad 1): los ajustes se obtienen via WCF, no se
escriben en `i_nav_transacciones_out`.

Para BYB (modalidad 2): los ajustes son en demanda (al despachar), no
pasan por el outbox como parte del flujo programado.

→ Esto explica por que `i_nav_transacciones_out` no tiene una columna
`tipo_ajuste` o algo similar — los ajustes no son ciudadanos de primera
clase del outbox.

### IMP-04: cadencia y reintentos NO son responsabilidad del WMS sino de cada interface

- En modalidad 1 (WCF): la cadencia la decide el ERP cliente (cuando polea).
- En modalidad 2 (NavSync): la cadencia la decide el scheduler que invoca el .exe (no es WMS quien lo programa).
- En modalidad 3 (SAPSYNC*): igual, cada SAPSYNC* lo agenda quien corresponda.
- En modalidad 4 (WebAPI): el cliente decide cuando consultar/escribir.

Por eso los 24,193 registros del outbox no tienen un patron uniforme de
"procesado/error" — depende de la modalidad. **El bridge debe escribir
sabiendo que el cleanup NO es su responsabilidad**.

## Preguntas que me quedaron (PEND nuevas)

### PEND-06 — Modalidad 1 (WCF MI3): polling o push reactivo?

¿El ERP cliente (Aurora) hace **polling** periódico al WCF (cada N
minutos), o reacciona a eventos puntuales (ej. cuando recibe un input
manual el operador del ERP, dispara la consulta a WMS)?

### PEND-07 — Modalidad 2 (NavSync): quien lo dispara?

¿Quien invoca a `NavSync.vbproj`?
- (a) Un job de Windows Scheduler corriendo en el server de WMS cada N
  minutos.
- (b) El BackOffice (BO.NET) cuando ocurre un evento (ej. al cerrar un
  despacho).
- (c) Un trigger de SQL Server.
- (d) Otro mecanismo.

¿Cual es la cadencia tipica?

### PEND-08 — Reintentos: por interface o por outbox?

Si NavSync envia una transaccion y NAV devuelve error, ¿NavSync
**reintenta automaticamente** (con backoff)? ¿O marca como fallida en
algun campo del outbox y deja que el siguiente run la levante?

¿Hay un campo en `i_nav_transacciones_out` que marque "ya procesada con
exito" vs "pendiente" vs "error N veces"?

### PEND-09 — Limpieza/purga del outbox

¿Las 24,193 filas del outbox son **acumulado historico** (incluso las
procesadas) o solo las pendientes? ¿Hay job de limpieza que purga las
procesadas?

### PEND-10 — Marca de exito en cada modalidad

¿Como sabe cada interface (MI3/NavSync/SAPSYNC*) que una fila ya fue
procesada exitosamente? ¿Por una columna especifica del outbox, por una
tabla paralela, o por una respuesta del ERP que se loggea aparte?

### PEND-11 — Ajustes "en demanda" (BYB)

Cuando se hace el despacho en BYB y se dispara la interface en demanda,
¿el orden es:
- (a) WMS escribe en outbox → dispara NavSync con args → NavSync lee la
  fila recien insertada y la procesa, o
- (b) WMS dispara directamente NavSync con los datos del despacho como
  args (sin pasar por el outbox)?

## Para el bridge nuevo: contrato minimo

Asumiendo MHS como modelo objetivo:

1. WMS escribe en `i_nav_transacciones_out` con el evento (ya identificado:
   `idordencompra`, `idrecepcionenc`, `idpedidoenc`, `iddespachoenc`).
2. El cliente (con equipo de desarrollo) consume la WebAPI WMS para:
   - Leer eventos pendientes (paginado, filtrable por tipo).
   - Marcar como procesados (con respuesta del ERP: documento generado, error, etc).
3. Manejo de errores y reintentos: del lado del cliente.

Para clientes que NO tienen equipo de desarrollo, la interface dedicada
(`MI3`, `NavSync`, `SAPSYNC*`) sigue siendo la opcion. El bridge no
necesita reemplazarlas — coexiste.

---

## Apendice — Marca de envio y patrones reales en datos (Erik tarea 4)

### Mecanismo de marca

El outbox `i_nav_transacciones_out` usa **dos columnas clave** para que cada
interface (MI3 / NavSync / SAPSYNC* / WebAPI) sepa que enviar:

```sql
SELECT * FROM i_nav_transacciones_out
WHERE enviado = 0
  AND tipo_transaccion = 'INGRESO'   -- o 'SALIDA'
```

- **`enviado`** (`int`): `0` = pendiente de envio, `1` = ya enviado al ERP.
- **`tipo_transaccion`** (`nvarchar`): `'INGRESO'` o `'SALIDA'` — permite que
  cada interface filtre solo las transacciones que le competen (por ejemplo
  un job de ingresos no procesa salidas, o cada modulo del ERP cliente
  consume su propio tipo).

Esto cierra **PEND-10** (marca de exito).

### Lo que NO esta en el outbox

Verifique todas las cols. **NO existen** las siguientes columnas que serian
naturales para un patron outbox completo:

- `fecha_envio` / `fec_envio` — no se sabe **cuando** se envio (solo `fec_mod`).
- `intentos` / `reintentos` — no hay contador de reintentos.
- `mensaje_error` — no se guarda el error del ERP en el outbox.
- `docnum_respuesta` — el numero de documento generado por el ERP no vuelve
  al outbox (vive en `log_error_wms` segun lo visto en P-16b).

→ Implicacion: **la logica de error/reintento la maneja cada interface por
fuera del outbox**, probablemente loggeando en `log_error_wms` o tablas
propias de cada interface (`SAPSYNC*` etc).

### Cols nuevas detectadas (no documentadas en pases previos)

| Columna | Tipo | Observacion |
|---|---|---|
| `cantidad_enviada` | `float` | sugerie envio **parcial** posible (split de un registro en varios envios) |
| `cantidad_pendiente` | `float` | complementaria de la anterior |
| `auditar` | `bit` | flag de auditoria (sin investigar uso) |
| `IdRecepcionDet` | `int` | ya identificada en P-09 — granularidad linea |
| `IdDespachoDet` | `int` | ya identificada — granularidad linea |
| `IdPedidoEncDevol` | `int` | manejo de **devoluciones** (PEND nuevo) |
| `no_documento_salida_ref_devol` | `nvarchar` | doc de salida original (devoluciones) |

### Estado real del outbox por BD (snapshot 27/abr/2026)

#### Killios (TOMWMS_KILLIOS_PRD) — saludable

| tipo_transaccion | enviado | filas | % |
|---|---:|---:|---:|
| INGRESO | 0 (pendiente) | 3 | 0.07% |
| INGRESO | 1 (enviado) | 4,391 | 99.93% |
| SALIDA | 1 (enviado) | 19,799 | 100% |
| **TOTAL** | | **24,193** | |

> Los 3 pendientes son del **19/ago/2025** (>90 dias). **Posible fallo no
> atendido**: o nadie resolvio esos 3 INGRESOS, o el snapshot de la BD
> esta congelado en agosto-2025 y la BD viva los proceso despues.
> A confirmar con Erik.

#### BYB (IMS4MB_BYB_PRD) — bandera roja

| tipo_transaccion | enviado | filas | % |
|---|---:|---:|---:|
| INGRESO | 0 (pendiente) | 110,795 | **99.90%** |
| INGRESO | 1 (enviado) | 107 | 0.10% |
| SALIDA | 0 (pendiente) | 145,117 | 34.4% |
| SALIDA | 1 (enviado) | 277,310 | 65.6% |
| **TOTAL** | | **533,329** | |

> **Hallazgo critico**: los INGRESOS de BYB **practicamente no se procesan
> via outbox** (110k pendientes vs 107 enviados). Las SALIDAS si pero con
> backlog importante. Hipotesis posibles:
> 1. NavSync solo se ocupa de SALIDAS y los INGRESOS se manejan por otro
>    canal (WCF directo, manual, batch nightly).
> 2. NavSync esta caido para INGRESOS desde hace tiempo y nadie noto.
> 3. El criterio para que NavSync procese un INGRESO es mas estricto
>    (algun campo adicional ademas de `enviado=0`).
>
> **Pregunta nueva PEND-12**: ¿que pasa con esos 110k INGRESOS pendientes
> en BYB?

#### CEALSA (IMS4MB_CEALSA_QAS) — outbox vacio

| tipo_transaccion | enviado | filas |
|---|---:|---:|
| (sin filas) | | 0 |

> CEALSA es 3PL con esquema diferente. Probablemente usa otro mecanismo
> de integracion (no `i_nav_transacciones_out`).
#### Becofarma (IMS4MB_BECOFARMA_PRD) — backlog masivo, BD migrada el 28-abr-2026

| tipo_transaccion | enviado | filas | % |
|---|---:|---:|---:|
| SALIDA | 0 (pendiente) | (medir) | mayoritario |
| SALIDA | 1 (enviado) | (medir) | minoritario |
| INGRESO | 0 (pendiente) | (medir) | |
| INGRESO | 1 (enviado) | (medir) | |
| **TOTAL** | | **36,576** | |

Agregados (sin partir por estado):
- SALIDA total: 31,486 (86%)
- INGRESO total: 5,090 (14%)
- enviados (`enviado=1`): 5,313 (**14.5%**)
- pendientes (`enviado=0`): 31,263 (**85.5%**)

> **Hallazgo H28/H30**: BECOFARMA exhibe dos patrones distintos a K7/BB:
> 1. **85% del outbox esta pendiente** — coherente con BD migrada/restaurada hoy 28-abr-2026 (probable `SAPBOSync.exe` no arrancado tras restore).
> 2. **El 100% de las filas tienen TODAS las FKs pobladas** (`idpedidoenc`, `iddespachoenc`, `idrecepcionenc`, `idordencompra`). Esto **invalida parcialmente H08** (que decia que el outbox solo se simplifica a 2 tipos efectivos por cliente). En BECOFARMA el outbox usa **patron de copia universal** — cada fila trae el contexto completo. La WebAPI debe diferenciar el patron por cliente al consumir el outbox.
>
> Detalles en `clients/becofarma.md` y `wms-specific-process-flow/becofarma-mapping.md`.


# Interfaces de integracion ERP por cliente — patron polimorfico del outbox

> Respondio: Erik (pasada 9b).
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

- `SAPSYNC.vbproj` — Becofarma (el primero / generico)
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
| **Becofarma** | SAP B1 | Push dedicado por cliente | `SAPSYNC.vbproj` |
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

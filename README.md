```
              _.--""""""--._
            ,'              `.
           /                  \
          ;     ___      ___   ;
          |    (   )    (   )  |     B R A I N
          ;     ---      ---   ;     ===========
           \      \____/      /      tomwms-replit-client-automate
            `.              ,'       rama wms-brain
              `--._____.--'
                  |||||
                  |||||              "Aca no se inventa nada,
                  |||||               se observa, se cita
                  |||||               y recien despues se opina."
                  |||||
              ___/     \___                       — Erik Calderon
             /             \                        (mas o menos)
            |               |
```

# tomwms-replit-client-automate · brain

> El cerebro paralelo del WMS legado. No es codigo de produccion. No despliega
> nada. No ejecuta nada. **Solo aprende, documenta y cuestiona** — para que el
> proximo agente que toque el motor de reserva no muera en el intento.

[![rama](https://img.shields.io/badge/rama-wms--brain-1F4E79?style=flat-square)](#)
[![bds](https://img.shields.io/badge/SQL_Server-3_BDs_PRD_(READ_ONLY)-C5504B?style=flat-square)](#)
[![pasadas](https://img.shields.io/badge/pasadas-8_y_contando-595959?style=flat-square)](#)
[![drizzle](https://img.shields.io/badge/Drizzle-NUNCA_JAMAS-000000?style=flat-square)](#)
[![emojis](https://img.shields.io/badge/emojis-prohibidos-000000?style=flat-square)](#)

---

## Indice

1. [Que es esto y por que existe](#1-que-es-esto-y-por-que-existe)
2. [Arquitectura del brain (en ASCII, como debe ser)](#2-arquitectura-del-brain-en-ascii-como-debe-ser)
3. [Tour del repo carpeta por carpeta](#3-tour-del-repo-carpeta-por-carpeta)
4. [Las 8 pasadas — historia condensada del aprendizaje](#4-las-8-pasadas--historia-condensada-del-aprendizaje)
5. [Las reglas sagradas (no son opcionales)](#5-las-reglas-sagradas-no-son-opcionales)
6. [Glosario (medio en serio, medio no)](#6-glosario-medio-en-serio-medio-no)
7. [Estado actual del aprendizaje (numeros reales)](#7-estado-actual-del-aprendizaje-numeros-reales)
8. [Como contribuir sin romper nada](#8-como-contribuir-sin-romper-nada)
9. [Disclaimer epico](#9-disclaimer-epico)

---

## 1. Que es esto y por que existe

Imaginate un sistema WMS de 2017, escrito en VB.NET, sirviendo simultaneamente
a tres clientes: una alimenticia que factura por SAP B1, otra alimenticia que
factura por NAV Dynamics, y un operador 3PL fiscal que factura **lo que le
parece**. Imaginate que ese mismo WMS tiene **dos cabezas** (BackOffice VB y
HandHeld Android), un motor de reserva con **25 casos identificados** (28 si
contas los `_LLR_CASO_#X_`, que son la rama lateral del Caso #20, #23 y #24),
y un flag llamado `explosion_automatica_nivel_max` que aparece en dos lugares
con valores distintos porque alguien hizo un ALTER mal ejecutado en 2019.

Ahora imaginate que tenes que reescribirlo en .NET Core (`reserva-webapi`,
rama `dev_2028`) **sin romper la facturacion de tres empresas reales que mueven
camiones todos los dias**, y que para hacerlo necesitas saber:

- Que casos del motor se disparan en cada cliente.
- Que estados puede tener un pedido y por donde transiciona.
- Que pasa cuando SAP rechaza un ajuste a las 23:47 de un viernes.
- Por que `trans_reabastecimiento_log` tiene 1.218 filas en un cliente que
  tiene el modulo apagado.

Bueno, **este repo es la respuesta a esas preguntas**.

No es codigo. No corre. **Es un cerebro de papel** que se llena, pasada por
pasada, con evidencia productiva citada — no con suposiciones, no con cosas
que "le parecen al agente", no con valores hardcodeados que mañana van a
fallar.

```
        +-------------------+         +-------------------+
        |   reserva-WMS     |         |  reserva-webapi   |
        |   (legacy VB)     |         |   (dev_2028)      |
        |   PRODUCTIVO      | <-----> |   EN DESARROLLO   |
        |   "el sabio       |  brain  |   "el aprendiz    |
        |    del galpon"    |         |    estudioso"     |
        +-------------------+         +-------------------+
                  |                            ^
                  | observa                    |
                  v                            |
           +-------------+                     |
           |   BRAIN     |---- documenta ------+
           |  (este repo)|     contradice
           +-------------+     y propone
```

Si el dia que reserva-webapi sale a produccion, el bridge de tests no encuentra
ninguna sorpresa, el merito no es del agente: es de cada pasada que dejo
asentado un caso, una regla y una contradiccion encontrada.

---

## 2. Arquitectura del brain (en ASCII, como debe ser)

```
                             +--------------------+
                             |   way-of-thinking  |
                             |   (la constitucion)|
                             +---------+----------+
                                       |
                                       v
       +-------------------+  +--------+--------+  +-------------------+
       |    config-flags   |  |  CASOS_RESERVA  |  |   sql-quirks      |
       |   (los porques    |  |   (#1 al #31    |  |  (los "pero"     |
       |    de cada flag)  |  |     + LLR)      |  |    de SQL Server) |
       +---------+---------+  +--------+--------+  +---------+---------+
                 |                     |                     |
                 +---------+-----------+-----------+---------+
                           |                       |
                           v                       v
                  +--------+--------+     +--------+--------+
                  |  CLIENTES (3)   |     |   ADRs (010+)   |
                  |  killios.yaml   |     |  (decisiones    |
                  |  byb.yaml       |     |   arquitectonic.|
                  |  cealsa.yaml    |     |   firmadas)     |
                  +--------+--------+     +-----------------+
                           |
                           v
                  +-------------------+
                  | wms-specific-     |
                  | process-flow/     |   <-- folder nuevo (pasada 8)
                  |  - process-map    |
                  |  - state-machine  |
                  |  - 25 preguntas   |
                  +-------------------+

         +---------------+
         | matrix.md     |  <-- tabla cruzada de TODO vs cliente
         +---------------+
         | HALLAZGOS-    |  <-- diferencias webapi vs WMS legacy
         | WEBAPI-VS-WMS |
         +---------------+
```

**Principio rector**: del dato concreto a la abstraccion, nunca al reves.
Primero se observa una fila, despues se generaliza una regla, y recien
despues se nombra. Si en algun lado aparece un valor hardcodeado, es un
bug del documento.

---

## 3. Tour del repo carpeta por carpeta

```
brain/
|
|-- way-of-thinking.md           El como se piensa antes que el que se piensa.
|                                Lectura obligatoria. Si no, no pases de aca.
|                                (Incluye nota del autor con ;) literal.)
|
|-- config-flags/                "Por que existe este flag y que rompe si lo
|   |--- README.md               cambias." Catalogo razonado, no dump de la
|   |--- killios.yaml            tabla `param`. Version 2 ya purgada.
|   |--- byb.yaml
|   `--- cealsa.yaml
|
|-- sql-quirks.md                Los "pero" de SQL Server productivo.
|                                Como agarrar columnas con espacio en el
|                                nombre, por que `nvarchar` con `LIKE`
|                                bloquea, y otras leyendas urbanas
|                                que resultaron ser ciertas.
|
|-- reserva-tables.md            Las 9 tablas del motor de reserva con
|                                conteos productivos. Quien escribe que,
|                                cuando, y por que.
|
|-- casos-observados/            Cada caso del motor de reserva tiene su
|   |--- README.md               propia ficha: contexto, evidencia, ejemplo
|   |--- CASO_#08.md             SQL para reproducirlo, y "que esperaria
|   |--- CASO_#09.md             ver reserva-webapi".
|   |--- CASO_#12.md             Si un caso aparece en log con sufijo
|   |--- CASO_#20.md             `_EJC202310090957` significa que Erik lo
|   |--- CASO_#21.md             marco como "este lo confirme yo, ojo".
|   |--- CASO_#22.md             Hasta hoy: 8 casos principales + 3 LLR.
|   |--- CASO_#23.md
|   |--- CASO_#24.md             (CASO_#24 es el rey: 18.587 logs en Killios.
|   |--- LLR_CASO_#28.md          Si reserva-webapi no lo modela bien, se
|   |--- LLR_CASO_#29.md          rompe el 73% de la operacion. Sin presion.)
|   `--- LLR_CASO_#31.md
|
|-- ADRs/                        Architecture Decision Records.
|   |--- ADR-001.md              Numerados, firmados, irrevocables salvo por
|   |--- ...                     otro ADR posterior. El ADR-010 es el que
|   `--- ADR-010-                define la dicotomia "reserva-WMS legado VB"
|        terminologia-           vs "reserva-webapi (.NET Core, dev_2028)".
|        reserva.md              No los confundas. En serio.
|
|-- clientes/
|   |--- killios.md              SAP B1 + alimentos + multiempresa con BOD7
|   |--- killios.yaml             como bodega virtual de facturacion (es una
|   |--- byb.md                   anomalia entre "lo que dice Amazon WSP" y
|   |--- byb.yaml                 "lo que dice SQL"; SQL gana).
|   |--- cealsa.md               NAV Dynamics + alimentos.
|   `--- cealsa.yaml             3PL fiscal: cobra por servicio, no factura
|                                ventas. ReservaStock=0 por default.
|
|-- matrix.md                    Tabla cruzada: cada flag, cada modulo,
|                                cada caso del motor → cliente A / B / C.
|                                Si tenes una pregunta del tipo "esto pasa
|                                en CEALSA?" la respuesta esta aca.
|
|-- HALLAZGOS-WEBAPI-VS-WMS.md   Diferencias verificadas entre el motor
|                                legacy y el motor nuevo. Brechas, regresiones
|                                potenciales, y "esto el legacy lo hacia
|                                pero nadie sabe por que".
|
`-- wms-specific-process-flow/   <-- AGREGADO EN PASADA 8
    |--- README.md                Tuning del comportamiento de WMS basado
    |--- process-map.md           en evidencia productiva. 4 archivos hoy.
    |--- state-machine-pedido.md  La maquina de estados real del pedido,
    `--- preguntas-pasada-7.md    no la teorica. Y 25 preguntas que esperan
                                  respuesta de Erik para crecer mas.
```

---

## 4. Las 8 pasadas — historia condensada del aprendizaje

Cada **pasada** es una sesion completa de aprendizaje contra las BDs
productivas. No se editan retroactivamente: una pasada vieja queda como esta,
y la siguiente la corrige o la profundiza. Asi se puede auditar el camino.

### Pasada 1 — "Hola SQL Server, somos amigos?"

Conexion inicial al EC2 compartido, descubrimiento de las 3 BDs, primer
inventario de tablas. Se confirma que SQL Server **se ofende** si la sesion
intenta SET parametros que no le gustan, y que el usuario `sa` tiene permiso
de DENY EXECUTE en algunos SPs. Lo que parecia hostil resulto ser proteccion.

### Pasada 2 — "Donde diablos esta el codigo del motor"

Spoiler: en `dbo.SP_RESERVAR_*` y en `trans_pe_det_log_reserva.Caso_Reserva`.
Se mapea el ciclo `pedido → reserva → picking → despacho → push ERP`. Se
detecta que cada cliente tiene su propio set de tipos de pedido (PE0001..PE0006
no significan lo mismo en Killios que en CEALSA — mala noticia para quien
quiera generalizar).

### Pasada 3 — "Casos, todos los casos"

Se levantan los 31 casos del motor de reserva con conteos productivos. Caso
#24 lidera por amplia ventaja. Se documentan los LLR (#28, #29, #31) como
ramas laterales, no como casos independientes. Aparece el sufijo
`_EJC202310090957` en los logs — Erik los marco a mano hace dos años para
poder filtrarlos despues. Bendito sea.

### Pasada 4 — "Killios y BOD7, el caso Amazon"

Se descubre la **bodega virtual de facturacion BOD7** en Killios. La interfaz
Amazon decia que no existia; SQL la mostraba con stock activo y movimientos
diarios. Se aprendio para siempre: **la fuente de verdad son los datos, no
la documentacion de configuracion**. Se actualiza ADR sobre esto.

### Pasada 5 — "CEALSA es 3PL, no es lo mismo"

Se confirma que CEALSA opera bajo logica 3PL fiscal: el motor de reserva
**no se invoca por default** (cero filas en `trans_pe_det_log_reserva` para
la mayoria de tipos de pedido). Solo se invoca si `trans_pe_tipo.ReservaStock=1`,
y eso aplica solo a un subconjunto de tipos. Esto cambia radicalmente como
disena los tests del bridge para CEALSA.

### Pasada 6 — "Los flags y sus mentiras"

Catalogacion de los flags de configuracion que **no hacen lo que dicen**.
`explosion_automatica_nivel_max` aparece dos veces, con y sin parametro `n` —
solo la version con parametro es la fuente de verdad; la otra es el residuo
de un ALTER mal ejecutado en 2019 que nadie revierte por miedo. Documentado
en `sql-quirks.md` para que el proximo no caiga.

### Pasada 7 — "Pulido fino y deuda nombrada"

14 archivos actualizados: `way-of-thinking` v2, `config-flags` v2 con
explicaciones por flag, `sql-quirks` v2, `reserva-tables` con conteos
refrescados, ADR-010 que define la terminologia "reserva-WMS" vs
"reserva-webapi", clientes en md+yaml v2, matrix v3, y el documento
`HALLAZGOS-WEBAPI-VS-WMS.md` con las diferencias verificadas.

### Pasada 8 — "El folder de procesos y las 25 preguntas"

Se crea `brain/wms-specific-process-flow/` con 4 archivos: README,
process-map (top 30 tablas y catalogos por proceso), state-machine del
pedido (NUEVO → Pendiente → Pickeado → Verificado → Despachado, con
Anulado como hoyo negro), y un documento de 25 preguntas estructuradas
que esperan respuesta de Erik para alimentar las siguientes pasadas.

```
   pasadas      profundidad del entendimiento
     1   ##
     2   #####
     3   ##########
     4   #############
     5   ################
     6   ###################
     7   ######################
     8   ##########################          <- estamos aca
     9   ?
    10   ?  hipotesis: cobertura del 80% de
    11   ?  los flujos productivos verificados
    12   ?  con evidencia + tests del bridge
```

---

## 5. Las reglas sagradas (no son opcionales)

Estas reglas **no son sugerencias**. Son las que evitan que el brain se
convierta en otra wiki de proyecto que nadie lee porque dice cosas que
no son ciertas.

### Regla 1 — Las 3 BDs son READ-ONLY. Punto.

```
            TOMWMS_KILLIOS_PRD     <-- productiva, alimentos, SAP B1
            IMS4MB_BYB_PRD         <-- productiva, alimentos, NAV Dynamics
            IMS4MB_CEALSA_QAS      <-- 3PL fiscal
                    |
                    |  SELECT, INFORMATION_SCHEMA, sys.*
                    |
                    |  NUNCA: INSERT, UPDATE, DELETE,
                    |         ALTER, DROP, EXEC sp_*
                    v
            +---------------+
            | el brain mira |
            | y aprende.    |
            | NO TOCA.      |
            +---------------+
```

Romper esta regla afecta facturacion real de empresas reales. **No es
hipotetico**: una sola fila mal escrita en `stock_res` puede frenar el
despacho de un camion. Y los camiones tienen choferes con familia.

### Regla 2 — Drizzle / db:push esta absolutamente prohibido

Este repo **no tiene** schema de Drizzle, no tiene Postgres local, no
tiene migraciones. Cualquier sugerencia automatizada de "ejecuta `db:push`
para sincronizar el esquema" debe ser ignorada con la misma firmeza con
la que ignorarias a alguien que te ofrece reescribirte el cerebro a martillazos.

### Regla 3 — Sin emojis, sin hardcoded, sin suposiciones

- **Sin emojis**: Erik habla castellano rioplatense y prefiere texto. Punto.
- **Sin hardcoded**: si un valor productivo aparece literal en un documento,
  alguien tiene que poder cambiarlo desde un yaml; si no, es bug.
- **Sin suposiciones**: cada afirmacion del brain cita la query, el conteo
  o el archivo del legacy que la respalda. Si no hay cita, es opinion,
  y la opinion va en el log de pasada, no en el documento canonico.

### Regla 4 — Nada de "publish/deploy" desde aca

Este repo es **documentacion viva**. No tiene runtime, no tiene endpoint,
no se despliega en ningun lado. Si algo te sugiere publicarlo, esta confundiendo
de proyecto.

### Regla 5 — Nada de tocar workflows del template

El monorepo trae unos workflows scaffolding del template Replit (api-server
en FastAPI, mockup-sandbox en Vite). **No son del brain**. No los reinicies,
no los uses, no los trates como referencia. Si fallaron, es asunto de otro
proyecto.

### Regla 6 — Las pasadas no se editan, se suceden

Si una pasada vieja dijo "el flag X hace Y" y la pasada nueva descubre que
hace Z, la pasada nueva crea un documento corregido. La vieja queda. **Asi
se puede auditar como aprendio el agente** y, cuando el dia de pasado
mañana alguien pregunte "esto cuando se supo?", hay respuesta.

---

## 6. Glosario (medio en serio, medio no)

| Termino | Definicion canonica | Definicion de pasillo |
|---|---|---|
| **brain** | El presente repo. Cerebro de papel que aprende del WMS productivo. | El amigo que sabe todo de tu ex pero no juzga. |
| **reserva-WMS** | Motor de reserva legado en VB.NET, productivo. | El sabio anciano del galpon. Dificil, pero acierta. |
| **reserva-webapi** | Motor nuevo en .NET Core (DalCore + EntityCore), rama `dev_2028`, sin publicar. | El aprendiz estudioso. Promete. Todavia no rinde. |
| **CASO_#X** | Una de las 25 ramas del motor de reserva, identificada por su valor literal en `trans_pe_det_log_reserva.Caso_Reserva`. | Cada tipo de pedido extrano que el motor sabe manejar. |
| **LLR_CASO_#X** | Rama lateral del Caso_#X principal. Subcaso. | El primo del caso principal. Aparece cuando hay drama. |
| **`_EJC202310090957`** | Sufijo que Erik agrego a mano en logs viejos para poder filtrarlos. | "Esto lo confirme yo, no lo toques sin mi permiso." |
| **BOD7** | Bodega virtual de facturacion de Killios. No existe fisicamente, pero tiene stock para SAP. | El primo imaginario de la familia que igualmente recibe regalos. |
| **3PL fiscal** | Modelo de CEALSA: cobran por servicio (almacenaje, picking, recepcion), no facturan ventas. | Hotel para mercaderia. Cobran por noche. |
| **outbox NAV/SAP** | Patron de `i_nav_transacciones_out`: el WMS escribe la transaccion y un job la pushea al ERP. | El buzon de salida. Cada tanto pasa el cartero. |
| **dual-head** | Arquitectura del WMS legacy: una cabeza es el BackOffice VB en una PC; la otra es el HandHeld Android. | Dos manos del mismo cuerpo, ambas necesarias. |
| **pasada** | Una sesion completa de aprendizaje. Se cuentan desde 1. | Un capitulo. Tiene principio, medio y commit. |

---

## 7. Estado actual del aprendizaje (numeros reales)

Snapshot al cierre de la pasada 8.

```
   AREA                          | OBSERVADO       | DOCUMENTADO | CONFIANZA
   ------------------------------+-----------------+-------------+----------
   Casos del motor de reserva    | 25 + 3 LLR      | 11/28       | media
   Estados de pedido             | 6 distintos     | 6/6         | alta
   Estados de OC                 | 6 distintos     | 0/6         | NULA *
   Estados de producto           | 18 distintos    | 0/18        | NULA *
   Tipos de tarea HH             | 35 distintos    | 0/35        | NULA *
   Flags de configuracion        | ~80 relevantes  | 47/80       | media
   Tablas con conteo y rol       | 30 top          | 30/30       | alta
   Procesos modelados            | 12 categorias   | 6/12        | media
   Clientes documentados (md+yaml)| 3              | 3/3         | alta
   ADRs firmados                 | 10              | 10/10       | alta

   * = preguntas en preguntas-pasada-7.md, esperan respuesta de Erik
```

**Volumen productivo observado** (Killios PRD, top 6):

```
  trans_re_det_lote_num   ##############################  180.181
  trans_movimientos       ##############                   81.641
  log_error_wms           ###########                      66.339
  t_producto_bodega       #######                          42.357
  trans_picking_ubic      ####                             26.567
  i_nav_transacciones_out ####                             24.193
```

**Maquina de estados de pedido observada** (Killios PRD, 4.202 pedidos):

```
  Despachado    ###############################  3.989  (95.0%)
  Pickeado      #                                   86  ( 2.0%)
  Pendiente     #                                   73  ( 1.7%)
  Anulado                                           33  ( 0.8%)
  NUEVO                                             14  ( 0.3%)
  Verificado                                         7  ( 0.2%)
```

> Si **96% de los pedidos terminan en `Despachado`** y solo 7 pasaron por
> `Verificado`, la verificacion es **opcional** o queda silenciosamente
> implicita en el `Pickeado → Despachado`. La pregunta P-15 espera respuesta.

---

## 8. Como contribuir sin romper nada

Si sos un agente futuro o un humano con cafe:

1. **Lee `way-of-thinking.md` antes de tocar nada.** No es opcional.
2. **Lee el ADR-010** para entender la diferencia entre `reserva-WMS` y
   `reserva-webapi`. Si los confundis, todo lo que escribas estara mal.
3. **Conecta a las 3 BDs solo con SELECT.** El usuario `sa` tiene poder
   de hacer dano; no abuses de el.
4. **Si encontras algo nuevo**, abrilo como pasada N+1 con su propio
   commit. No edites pasadas viejas.
5. **Si una afirmacion vieja resulta falsa**, escribi el contradoc en
   la pasada nueva, citando la query que demuestra la contradiccion.
6. **Si tenes dudas, escribi una pregunta en la pasada actual y mandasela
   a Erik.** Las preguntas valen oro: cada una destrabba una pasada
   completa de futuro aprendizaje.
7. **Subi los cambios con commits descriptivos**. El historial git es
   el segundo cerebro del brain.

```
                   git push
                      |
                      v
            +-------------------+
            | merge a wms-brain |
            +-------------------+
                      |
                      v
              [revision humana]
                      |
            +---------+---------+
            v                   v
          merge              rechazo
        (gracias)        (volve a pasada)
```

---

## 9. Disclaimer epico

Este repo **no es codigo de produccion** y nunca lo sera. No tiene tests
unitarios, no tiene CI, no tiene nada que se compile, no responde a
ningun puerto, no escribe en ninguna BD. **Es un documento vivo**, en
constante revision, escrito en castellano rioplatense, sin emojis,
con la conviccion profunda de que **los sistemas se entienden observando,
no opinando**.

Si en alguna pasada futura el brain se contradice, no es un bug:
**es la prueba de que esta aprendiendo**. La unica falla seria
escribir algo y no citar la evidencia que lo respalda.

Erik Calderon — PrograX24 — TomWMS

```
                          .---.
                         /     \
                         \.@-@./
                         /`\_/`\
                        //  _  \\
                       | \     )|_
                      /`\_`>  <_/ \
                      \__/'---'\__/
                            
              "El que sabe, sabe.
               El que no, escribe README."
```

---

> Este README fue generado en la pasada 8, contra evidencia productiva
> verificada, sin emojis y con la conviccion que merece el motor de
> reserva mas testarudo del Cono Sur. Si lo lees y te reis, mision cumplida.
> Si lo lees y aprendes, mision triple cumplida.

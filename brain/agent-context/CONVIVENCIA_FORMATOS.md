---
id: CONVIVENCIA_FORMATOS
tipo: agent-context
estado: vigente
titulo: "CONVIVENCIA_FORMATOS.md — Por que clients/*.md y clients/*.yaml conviven"
ramas: [dev_2023_estable, dev_2028_merge]
tags: [agent-context]
---

# CONVIVENCIA_FORMATOS.md — Por que clients/*.md y clients/*.yaml conviven

> Decision provisional aceptada al 2026-04-30, pendiente de un ADR formal.
> Mientras tanto, este documento explica el estado y como mantener consistencia.

---

## 1. Estado actual

En el brain existen DOS conjuntos paralelos de archivos por cliente:

### 1.1 `brain/clients/*.md` (formato extendido humano)

Existentes hoy (4 archivos):
- `becofarma.md`
- `byb.md`
- `cealsa.md`
- `killios.md`

Caracteristicas:
- Markdown narrativo, escrito para que un humano (Erik o un agente nuevo)
  entienda al cliente: que ERP usa, que rama de WMS corre, particularidades
  operativas (ej. "BYB tiene NAV y solo BYB usa NAV"), historias relevantes.
- Pueden incluir tablas, citas de logs, screenshots del Kardex.
- **Fuente de verdad para contexto narrativo**.

### 1.2 `brain/client-index/*.yaml` (formato maquina)

Existentes hoy (7 archivos, viven en la rama `wms-brain` de GitHub —
todavia NO bajados al workspace local; ver `DRIFT_DETECTADO.md`):
- `becofarma.yaml`
- `byb.yaml`
- `cealsa.yaml`
- `killios.yaml`
- `mampa.yaml`
- `mercopan.yaml`
- `merhonsa.yaml`

Caracteristicas:
- YAML estructurado, consumible por scripts (`brain/client-index/replicate.py`,
  potencialmente por un futuro extractor que genere reportes cross-cliente).
- Schema fijo: `slug`, `db_name`, `branch`, `erp`, `host`, `port`, `notas`.
- **Fuente de verdad para automatizaciones**.

---

## 2. Por que dos formatos

Tres razones reales:

1. **El `.md` precede al `.yaml`**. El brain arranco siendo solo narrativo;
   los `.yaml` se agregaron despues como facilitador para automatizar
   replicaciones de queries cross-cliente.
2. **Los consumidores son distintos**. Un agente que va a abrir un caso
   nuevo necesita contexto narrativo (lee el `.md`). Un script que necesita
   iterar sobre los 7 clientes y hacer la misma query en cada BD necesita
   metadata estructurada (lee el `.yaml`).
3. **Un solo archivo dual no funciona bien**. Embeber YAML dentro de
   markdown (como front matter) genera friccion para scripts que solo
   quieren los campos. Y partir el markdown en bloques YAML internos rompe
   la lectura humana.

---

## 3. Como mantener consistencia

### 3.1 Cuando agregar un cliente nuevo

1. Crear `brain/clients/<slug>.md` con la narrativa (minimo: ERP, rama WMS
   activa, BD productiva, particularidades operativas).
2. Crear `brain/client-index/<slug>.yaml` con el schema estandar:
   ```yaml
   slug: <slug>
   nombre: <Nombre completo del cliente>
   db_name: <NOMBRE_BD>
   db_host: <ip,puerto>
   branch: dev_2023_estable | dev_2028_merge | dev_2028_Cumbre
   erp: SAP_B1 | NAV | IMS4 | Custom
   estado: prd | qas | dev
   notas: |
     Particularidades cortas, sin emojis.
   ```
3. Actualizar la tabla en `brain/agent-context/RAMAS_Y_CLIENTES.md`
   seccion 0 (TL;DR cliente -> rama -> DB).

### 3.2 Cuando cambia un dato (ej. cliente migra de rama)

- Cambiar **los dos archivos** (`.md` y `.yaml`) Y la tabla de
  `RAMAS_Y_CLIENTES.md`.
- Mencionarlo en commit message: `chore(clients): byb migra a dev_2028_merge`.

### 3.3 Validador rapido

Para detectar drift entre `.md` y `.yaml` se puede hacer un script chico
que:
1. Lista todos los `clients/*.md` y todos los `client-index/*.yaml`.
2. Asegura que el conjunto de slugs coincide (todo `.md` tiene su `.yaml` y
   viceversa).
3. Para cada par, parsea el YAML y busca menciones de los mismos campos en
   el `.md` (ej. el `db_name` del yaml deberia aparecer literal en el md).

Pendiente de escribir. Cola: `C-XXX validar consistencia clients/*.md vs
client-index/*.yaml`.

---

## 4. Cuando consolidar a un solo formato

Cuando se cumpla **al menos una** de las siguientes:

1. Erik decide formalmente cual es el formato canonico (probable: el `.md`
   con front-matter YAML, o un esquema custom). Se documenta en un ADR
   y se migra de una vez.
2. La cantidad de scripts que consumen `client-index/*.yaml` justifica
   eliminar el `.md` (poco probable: la narrativa siempre va a tener
   demanda).
3. La cantidad de drift entre los dos formatos se vuelve insostenible
   (sintoma: validador chocando todo el tiempo).

Hasta entonces, **convivencia formal**.

---

## 5. Anti-patrones

- **Editar un archivo y olvidar el otro** -> drift inmediato. Siempre
  cambiar los dos.
- **Crear un cliente solo en `.yaml`** -> queda invisible para humanos
  hasta que alguien lea el yaml. Crear siempre tambien el `.md`.
- **Crear un cliente solo en `.md`** -> queda invisible para scripts.
  Crear siempre tambien el `.yaml`.
- **Asumir que `clients/` es la unica fuente** -> recordar que tambien hay
  `RAMAS_Y_CLIENTES.md` con la tabla TL;DR. Cambia los tres lugares
  (md + yaml + RAMAS_Y_CLIENTES).

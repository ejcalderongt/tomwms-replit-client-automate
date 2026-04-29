# L-027 — PROC: orden de migracion clientes a `dev_2028_merge`

> Etiqueta: `L-027_PROC_ORDEN-MIGRACION-2028_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-MIGRACION-2023-A-2028 parte 1

## Hallazgo

El orden de migracion de clientes desde `dev_2023_estable` a
`dev_2028_merge` esta DEFINIDO pero NO tiene cronograma con fechas.
Se ejecuta secuencialmente por priorizacion del equipo.

## Estado al 2026-04-30

| # | Cliente | Estado migracion | Notas |
|---|---|---|---|
| 0 | **MAMPA** | ✅ migrado, productivo en `dev_2028_merge` | Primer cliente productivo en 2028. Talla x color justificacion principal del cambio. |
| 1 | **BYB (B&B)** | 🔄 en paralelo, QAS instalado con dev_2028_merge | Tres en paralelo |
| 1 | **IDEALSA** | 🔄 en paralelo, QAS instalado con dev_2028_merge | Tres en paralelo |
| 1 | **INELAC** | 🔄 en paralelo, QAS instalado con dev_2028_merge | Tres en paralelo |
| 2 | **CUMBRE (La Cumbre)** | ⏳ siguiente | Tiene rama propia `dev_2028_Cumbre` con fix de implosion (ver L-029) |
| 3 | **BECOFARMA** | ⏳ despues | Cliente "farmacia" — control vencimiento, lote correlativo |
| 3 | **CLARISPHARMA** | ⏳ despues | **Cliente nuevo descubierto** — probable familia Becofarma (ver L-033) |
| 4 | **MHS** | ⏳ ultimo | Es B2B-only via WMSWebAPI |

Cita literal Carolina:

> "Estamos llevando en paralelo la migracion de B&B, IDEALSA e INELAC,
> las tres empresas tienen ya instalado en QAS la dev_2028_merge.
> Luego seguimos con La Cumbre. Luego con Becofarma y Clarispharma."

> "No hay un cronograma exacto con fechas."

> "Despues de MAMPA: MHS." (interpretacion: MHS es el ultimo de la
> cola, no el siguiente; los demas van antes en el orden enumerado.)

## Lo que NO se menciono y queda como hueco

- **KILLIOS (K7)**: no aparece en el orden. ¿Sigue en `dev_2023_estable`
  indefinidamente? ¿Se mergea junto con BECOFARMA por similitud SAP B1?
  Q-KILLIOS-MIGRACION-PLAN abierta.
- **CEALSA**: no aparece. Hipotesis: como esta en proceso de migracion
  de su ERP propio a Odoo (L-028), la migracion 2028 del WMS se
  posterga hasta que el lado del ERP estabilice. Q-CEALSA-MIGRACION-2028
  abierta.
- **MERCOPAN, MERHONSA, MERCOSAL**: probable que entren bajo el
  paraguas "IDEALSA" si son holding (Wave 7 confirmo MERCOPAN +
  MERHONSA en holding; Wave 10 introduce MERCOSAL como cliente nuevo
  cuya BD acaba de pasar Carolina a Erik). Q-IDEALSA-FAMILIA-MIGRACION
  abierta.

## Patron observado

### Pre-condicion comun
Los 3 clientes en paralelo (BYB + IDEALSA + INELAC) **ya tienen QAS
instalado con dev_2028_merge**. Esto significa que el ciclo es:

```
1. instalar dev_2028_merge en QAS del cliente
2. validar funcionalidad core (recepcion, picking, despacho, ajustes)
3. validar capabilities cliente-especificas (talla x color, MI3, RFID)
4. migrar BD de prod (script de Carolina) — ventana de mantenimiento
5. desplegar dev_2028_merge en prod
6. monitoreo post-migracion (probable que reaparezcan los errores de
   concurrencia que el cambio a IDENTITY justamente arregla)
```

### Por que ESE orden

Hipotesis (a confirmar con Erik):

- **BYB primero**: caso mas simple (1 ERP NAV literal, sin
  multi-tenancy, schema mas estable). Validar la migracion ahi reduce
  riesgo para los proximos.
- **IDEALSA + INELAC en paralelo**: comparten infraestructura del
  holding (probablemente). Ahorrar trabajo migrando juntos.
- **Cumbre antes de BECOFARMA**: Cumbre tiene una rama propia
  `dev_2028_Cumbre` con fix de implosion (L-029). Para que el fix
  llegue a TODOS los clientes 2028, primero hay que cerrar Cumbre y
  mergear a `dev_2028_merge`. BECOFARMA hereda automaticamente.
- **BECOFARMA + CLARISPHARMA en paralelo**: probable familia farmacia
  (control vencimiento, lote correlativo).
- **MHS al final**: es B2B-only via WMSWebAPI, su migracion depende
  del estado de la WMSWebAPI 2028 (que tiene su propio scope).

## Implicaciones para el brain

### Para Wave 10+
- **No mas asumir que `dev_2028_merge` es teorico**: es la rama LIVE
  de produccion para MAMPA, y QAS para BYB/IDEALSA/INELAC. Cualquier
  hallazgo en codigo o schema debe diferenciar entre 2023 y 2028.
- **Trabajo duplicado evitable**: si vemos un comportamiento solo en
  un cliente migrado, puede ser bug del 2028. Si lo vemos en clientes
  pre-migrados, puede ser legacy 2023 que se va a "fixear naturalmente"
  con la migracion.

### Para `scan-comments-tree-map`
- Comentarios firmados con fecha 2024+ en `dev_2028_merge` y NO en
  `dev_2023_estable` son **comentarios de la migracion 2028**.
  Probablemente con score alto porque documentan el cambio
  arquitectonico.

### Para los fingerprints
- Cada fingerprint debe tener un campo `migracion_2028_status` con
  valor `migrated` / `qas` / `pending` / `n/a`. Carolina reset:
  - MAMPA: `migrated`
  - BYB, IDEALSA, INELAC: `qas`
  - CUMBRE, BECOFARMA, CLARISPHARMA: `pending`
  - MHS: `pending` (ultimo)
  - K7, CEALSA: `unknown` (Q-* abiertas)

## Q-* RESUELTAS

- `Q-MIGRACION-2023-A-2028` (parte 1) — orden definido, sin cronograma.

## Q-* nuevas derivadas (cuestionario bloque 14)

- Q-KILLIOS-MIGRACION-PLAN
- Q-CEALSA-MIGRACION-2028
- Q-IDEALSA-FAMILIA-MIGRACION (incluye MERCOPAN, MERHONSA, MERCOSAL)
- Q-MHS-WEBAPI-DEPENDENCIA (¿la migracion de MHS depende de un milestone
  de WMSWebAPI 2028?)
- Q-MIGRATION-CRONOGRAMA-FECHAS (mas optimista: ¿hay rangos
  trimestrales aunque no fechas exactas?)

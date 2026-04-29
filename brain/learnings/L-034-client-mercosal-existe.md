# L-034 — CLIENT: MERCOSAL existe como cliente del ecosistema, probable holding IDEALSA

> Etiqueta: `L-034_CLIENT_MERCOSAL-EXISTE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-MERCOPAN-MERCOSAL

## Hallazgo

**MERCOSAL es una BD del WMS** que Carolina acaba de pasar a Erik
(2026-04-30). Es un cliente / filial **probablemente del holding IDEALSA**,
junto con MERHONSA (Honduras) y MERCOPAN (Panama).

Cita literal Carolina respecto a MERCOPAN y MERCOSAL:

> "Ya le acabo de pasar a Erik las bases de datos para que las tengas."

## Por que MERCOSAL es probable holding IDEALSA

1. **Patron de naming**: `MER-` es el prefijo compartido con MERCOPAN
   (`Mer`cantil `Co`mercial `Pan`ama) y MERHONSA (`Mer`cantil
   `Hon`durena `S`.A.). Por extension:
   - **MERCOSAL** = `Mer`cantil `Co`mercial **¿`Sal`vador?** (El
     Salvador, completando el holding centroamericano IDEALSA)
2. **Ya estaba como rama HH historica** (`mercosal`, ver L-032).
3. **BD pasada por Carolina junto con MERCOPAN** en el mismo momento →
   misma familia operativa.
4. Wave 7 dejo abierta `Q-IDEALSA-OTROS-PAISES` (¿hay mas filiales?
   Guatemala, El Salvador, Costa Rica, Nicaragua) → MERCOSAL es la
   respuesta mas probable para El Salvador.

## Lo que SE sabe (al 2026-04-30)

| Atributo | Valor |
|---|---|
| Nombre | MERCOSAL |
| Pais probable | El Salvador (por nomenclatura `Mer-co-sal`) |
| Holding probable | IDEALSA (junto con MERHONSA, MERCOPAN) |
| BD pasada a Erik | si (no confirmado nombre exacto, probable `IMS4MB_MERCOSAL_PRD`) |
| Estado en EC2 | desconocido (aun no escaneado) |
| Schema esperado | similar al holding IDEALSA: ~315 tablas comunes con MERHONSA / MERCOPAN |
| Rol en MI3 | desconocido (probable que sea consumer junto con IDEALSA / INELAC / MERCOPAN — ver L-028) |
| Productos esperados | aceite + detergentes (igual que filiales hermanas) |

## Implicaciones para el brain

### 1. Holding IDEALSA pasa de 2 a probable 3 filiales

Actualizar `agent-context/HOLDING_IDEALSA.md`:

```
Antes (Wave 7):
| Pais | MERHONSA = Honduras | MERCOPAN = Panama |

Despues (Wave 10):
| Pais | MERHONSA = Honduras | MERCOPAN = Panama | MERCOSAL = El Salvador (probable) |
```

### 2. Schema esperado MERCOSAL

Si efectivamente es del holding, esperamos:
- ~315 tablas comunes con MERHONSA + MERCOPAN
- Tablas exclusivas comunes del holding: `diferencias_movimientos`,
  `operador_montacarga`, `stock_res_ped_164`, `us_solic_det/enc`
- Tablas regulatorias probables (si El Salvador tiene control similar
  al panameno o hondureno): `stock_jornada_*`
- Probable rol "cocinero" si MERCOSAL hace preparacion de mezclas
  como MERCOPAN

### 3. Fingerprint pendiente

Crear `fingerprint/MERCOSAL.md` cuando se tenga acceso a la BD.
Plantilla recomendada:

```
fingerprint/MERCOSAL.md
- Pais: El Salvador (a confirmar)
- Holding: IDEALSA (a confirmar)
- BD: IMS4MB_MERCOSAL_PRD (a confirmar)
- Schema common con MERHONSA/MERCOPAN: ?% (a medir)
- ERP: ? (probable consumer MI3)
- Capabilities: ? (a popular desde i_nav_config_enc)
```

### 4. Discovery tree y wave 10
Documentar el descubrimiento de MERCOSAL como evento de Wave 10.

### 5. Para `scan-comments-tree-map`
Agregar `MERCOSAL` a `config/boost-keywords.yml` seccion `clients`.

## Cierra Q-*

- `Q-MERCOPAN-MERCOSAL` (Bloque 1, media) — RESUELTA parcial:
  MERCOPAN ya estaba mapeada en Wave 7. MERCOSAL es nueva, va para
  fingerprint pendiente.

## Q-* nuevas derivadas (cuestionario bloque 14)

- **Q-MERCOSAL-FINGERPRINT** (alta) — fingerprint completo cuando se
  tenga BD accesible.
- **Q-MERCOSAL-PAIS-CONFIRMACION** (media) — ¿es realmente El Salvador?
- **Q-MERCOSAL-HOLDING-CONFIRMACION** (media) — ¿es del holding
  IDEALSA?
- **Q-IDEALSA-OTROS-PAISES** (Wave 7, sigue abierta, refinada) — si
  MERCOSAL es El Salvador, ¿hay tambien Guatemala, Costa Rica,
  Nicaragua? ¿Cuantas filiales tiene el holding?

## Action items

1. Pedir nombre exacto de la BD a Carolina/Erik.
2. Conectar a `IMS4MB_MERCOSAL_PRD` (read-only) y hacer queries
   diagnosticas: tablas, vistas, primeras filas de `i_nav_config_enc`,
   `bodega`, `propietarios`.
3. Confirmar pais y posicion en holding.
4. Crear `fingerprint/MERCOSAL.md`.
5. Actualizar `HOLDING_IDEALSA.md` con la tercera filial.

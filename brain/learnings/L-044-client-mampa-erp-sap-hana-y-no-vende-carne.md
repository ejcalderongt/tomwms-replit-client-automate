# L-044 — CLIENT: MAMPA usa SAP HANA como ERP y NO vende carne (es retail multi-categoria)

> Etiqueta: `L-044_CLIENT_MAMPA-SAP-HANA-Y-NO-CARNE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuestas Carolina a Q-MAMPA-ERP y Q-MAMPA-MERMA-CARNE-FLUJO

## Hallazgo doble

### 1. ERP de MAMPA = SAP HANA

Esto rompe la asuncion implicita de que MAMPA usaba "Nav-style interface" (clases `clsSyncNav*`) por afinidad con NAV. **NO**: el ERP es **SAP HANA** y se conecta al WMS via la **misma capa de abstraccion** "Nav-style" (porque en TOMWMS `Nav` ya no significa Microsoft NAV, ver L-025).

### 2. MAMPA NO vende carne, no tiene merma de carne

La hipotesis del cuestionario sobre "merma de carne en MAMPA" era **un error de atribucion**. MAMPA es **retail multi-categoria**:
- Ropa
- Zapatos
- Electrodomesticos
- Vitaminas

**El caso "merma de carne" probablemente sea de La Cumbre** (cliente que SI maneja productos perecederos / carniceria).

## Citas literales Carolina (Wave 11)

> Q-MAMPA-MERMA-CARNE-FLUJO:
> "En MAMPA no hay merma de carne, esta empresa vende ropa, zapatos,
> electrodomesticos, vitaminas, pero no vende carne, es posible que
> esto sea de La Cumbre."

> Q-MAMPA-ERP:
> "SAP Hana"

> Q-MAMPA-CEDIS-PANTALLA-LEGACY:
> "El Cedis hace referencia a su centro de distribucion, donde reciben
> el producto de sus proveedores y alli es que se realiza el proceso
> de costeo."

## Implicaciones

### Para el brain (correcciones a hacer)

1. **`clients/mampa.md`** (si existe) o nuevo: actualizar ERP → SAP HANA, sectores → Ropa+Zapatos+Electro+Vitaminas, NO carniceria.
2. **`clients/cumbre.md`** (si existe) o crear: agregar "merma de carne" como flujo a investigar. La Cumbre maneja perecederos.
3. **`learnings/L-025`** (Wave 10): se confirma que el prefijo `Nav` cubre tambien SAP HANA — no solo NAV/SAP B1/IMS4/ARITEC. Agregar SAP HANA al ejemplo en L-025 si no esta.

### Para mapping operativo

- Si en sql-catalog aparecen tablas tipo `i_nav_config_enc` con codigos de empresa MAMPA, los webservices apuntan a **SAP HANA**, no a NAV/B1.
- Las clases `clsSyncNavProducto` etc. para MAMPA llaman a SAP HANA WS por debajo.
- **Ojo**: los WS de SAP HANA tienen contratos distintos a los de NAV — la abstraccion vive en `i_nav_*` pero la implementacion del WS-cliente debe variar por instancia. Pendiente verificar (Q nueva).

### CEDIS aclarado

CEDIS no es una pantalla legacy del WMS, es **terminologia de cliente** = Centro de Distribucion donde MAMPA recibe productos de proveedores y hace **costeo**.

→ Si hay una pantalla en BOF llamada CEDIS, no es legacy huerfana — es la pantalla que MAMPA usa para sus operaciones de centro distribucion. Carolina implica que esto es operativo y vivo.

## Mapping correcto de la merma de carne

Hipotesis Wave 11: La Cumbre tiene flujo de merma de carne porque vende productos perecederos. Esto refuerza la posicion de Cumbre en el orden de migracion 2028 (L-027): es el **cuello de botella** porque su modelo de datos es mas complejo (perecederos, lotes, vencimientos, mermas) que el resto.

## Q-* abiertas

- Q-MAMPA-SAP-HANA-WS-CONTRATO: ¿el contrato WS de SAP HANA se documenta en algun lado? ¿Quien lo mantiene del lado MAMPA?
- Q-CUMBRE-MERMA-CARNE-FLUJO: ¿como es el flujo real de merma de carne en La Cumbre? ¿Tabla, SP, form?
- Q-NAV-CONFIG-ENC-MULTI-ERP: ¿como discrimina `i_nav_config_enc` el tipo de ERP destino para hablar SAP HANA vs NAV vs IMS4?
- Q-CUMBRE-OTROS-PERECEDEROS: ¿La Cumbre maneja vencimientos, lotes con FEFO, otros perecederos?
- Q-MAMPA-CEDIS-COSTEO-FORMULA: ¿como se calcula el costeo en CEDIS MAMPA?

## Vinculos

- L-025: prefijo Nav — SAP HANA es otro caso confirmado de "Nav no es NAV".
- L-027: orden migracion 2028 — Cumbre es etapa critica, este learning explica por que.
- L-029: bug implosion Cumbre — relacionado con manejo perecederos posiblemente.

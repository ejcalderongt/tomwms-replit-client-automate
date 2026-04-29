# L-033 — CLIENT: CLARISPHARMA existe como cliente activo, va con BECOFARMA en migracion 2028

> Etiqueta: `L-033_CLIENT_CLARISPHARMA-EXISTE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-MIGRACION-2023-A-2028 parte 1

## Hallazgo

**CLARISPHARMA es un cliente activo del WMS** que NO estaba mapeado en
el brain hasta Wave 10. Aparece en el orden de migracion despues de
La Cumbre, en paralelo con Becofarma.

Cita literal Carolina:

> "Luego con Becofarma y Clarispharma."

## Lo que SE sabe (al 2026-04-30)

| Atributo | Valor |
|---|---|
| Nombre | CLARISPHARMA |
| Sector probable | farmacia (sufijo `-pharma`, paralelo con Becofarma) |
| Estado migracion 2028 | pendiente (despues de Cumbre, junto con Becofarma) |
| BD productiva | desconocida (¿en EC2 `52.41.114.122,1437` o en otro server?) |
| ERP | desconocido |
| Holding / familia | desconocida (¿familia Becofarma?) |

## Hipotesis a confirmar

### 1. Familia Becofarma
Si BECO/BECOFARMA es la primer farmacia del WMS y CLARISPHARMA es la
segunda, es razonable suponer que comparten:
- Patrones de control de vencimiento (perecederos farmaceuticos).
- Modelo de lote correlativo (Q-LOTE-NUM-EXISTE Wave 9).
- Politicas de tolerancia de vida util.
- ERP: BECO usa `SAPBOSync.exe` (caso historico SAP B1). CLARISPHARMA
  podria usar el mismo SAP B1.

### 2. Mismo dueno corporativo
BECOFARMA y CLARISPHARMA pueden ser del mismo dueno (cadena de
farmacias) o competidoras del mismo sector. Si son del mismo dueno,
podriamos tener un nuevo "holding farmacia" similar al holding IDEALSA
(MERHONSA + MERCOPAN + MERCOSAL).

### 3. Ya tiene BD?
Carolina menciono CLARISPHARMA en el contexto de "migracion 2028"
implicando que YA existe operativamente en `dev_2023_estable`. Esto
significa que su BD ya esta en algun server. **Pendiente** descubrir
cual.

## Implicaciones para el brain

### 1. Inventario de clientes actualizado
Agregar a `clients/` una entry tentativa:

```
clients/clarispharma.md
- Estado: descubierto Wave 10, fingerprint pendiente
- Sector probable: farmacia
- Migra a 2028: junto con Becofarma
- Q-* abiertas: BD ubicacion, ERP, holding, RUT/identificacion
```

### 2. Fingerprint pendiente
Crear `fingerprint/CLARISPHARMA.md` con campos en `?` para
posteriormente popularlo cuando Erik / Carolina den acceso a la BD.

### 3. Implicaciones para Wave 4 (heat-map params)
El heat-map cross-cliente actualmente cubre BECO, K7, MAMPA, BYB, CEALSA.
**Cuando CLARISPHARMA tenga BD accesible**, expandir el heat-map a
6 clientes (puede revelar drift de parametros que hoy no se ve).

### 4. Implicaciones para `scan-comments-tree-map`
Agregar `CLARISPHARMA` al `config/boost-keywords.yml` seccion
`clients`. Si los comentarios firmados del codigo mencionan
CLARISPHARMA, suben de score.

## Cierra Q-*

Ninguna directamente — es un descubrimiento nuevo.

## Q-* nuevas derivadas (cuestionario bloque 14)

- **Q-CLARISPHARMA-FINGERPRINT** (alta) — ¿en que BD vive? ¿que ERP
  usa? ¿control vencimiento?
- **Q-CLARISPHARMA-BECOFARMA-RELACION** (media) — ¿son misma cadena
  corporativa o competidoras? ¿comparten algun proceso?
- **Q-CLARISPHARMA-RAMA-DEDICADA** (baja) — ¿tiene rama propia o usa
  `dev_2023_estable` parametrizada?

## Action items

1. Cuando Carolina / Erik tengan momento, pedir acceso de lectura a la
   BD de CLARISPHARMA para hacer fingerprint inicial.
2. Si la BD esta en el mismo EC2 productivo, hacer queries diagnosticas
   minimas: `i_nav_config_enc` (capabilities), `bodega` (cuantas
   bodegas), `producto` (cardinalidad catalogo), `producto_estado`
   (cuantos estados), `propietarios` (1 default o multi).
3. Una vez con datos, crear `fingerprint/CLARISPHARMA.md` siguiendo el
   patron de `fingerprint/BECOFARMA.md`.

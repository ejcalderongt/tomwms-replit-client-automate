---
id: 04-producto
tipo: heat-map-params
estado: vigente
titulo: "cross-cliente/04 — `producto` + `producto_bodega` + `producto_estado` + `producto_estado_ubic`"
tags: [heat-map-params]
---

# cross-cliente/04 — `producto` + `producto_bodega` + `producto_estado` + `producto_estado_ubic`

> Capa 4 de la matriz cross-cliente. Cierra el inventario exhaustivo de
> flags. Insumo final para arrancar code-deep-flow.
>
> Datos REALES de los 5 clientes en EC2 (28-abr-2026).
> EC2 es copia parcial: BECOFARMA al dia, K7 desfase 8 meses, MAMPA QA
> reciente, BYB parado 2024, CEALSA QAS sin trafico.

---

## Conteos macro

| Cliente | producto total | producto activo | producto_bodega | producto_estado | producto_estado_ubic |
|---|---:|---:|---:|---:|---:|
| BECOFARMA | 1.835 | 1.835 (100%) | 1.835 (1 bod x 1.835 prod) | 10 | 4 |
| KILLIOS | 319 | 319 (100%) | 1.914 (6 bod x 319 prod) | 18 | 2 |
| MAMPA | **31.397** | **31.397** (100%) | **1.036.101** (33 bod x 31.397 prod) | 19 | 20 |
| BYB | 623 | 623 (100%) | 1.109 (2 bod x 623 prod aprox) | 24 | 2 |
| CEALSA | 1.714 | 1.635 (95.4%) | 3.428 (2 bod x 1.714 prod) | **3.200** | 1 |

**Lecturas clave**:
- MAMPA es el catalogo MAS GRANDE: 31.397 productos x 33 bodegas =
  **1.036.101 filas en producto_bodega**. Esto es zapateria con talla x
  color x SKU. Es el caso de presion para el WebAPI.
- CEALSA es el UNICO con productos `activo=False` (79 inactivos /
  1.714 = 4.6%). Resto siempre activo=True. Patron CEALSA: borrado
  logico real en productos. Resto: solo activan.
- CEALSA `producto_estado` con 3.200 filas vs 10-24 en el resto =
  **anomalia investigada y resuelta** (ver seccion dedicada).

---

## Schema cross-cliente — `producto` (60 cols union)

Todos los clientes tienen 60 cols menos **CEALSA con 59** (le falta
`margen_impresion`). El resto es 100% identico en nombre y tipo.

### Cols ausentes por cliente

| Col | BECO | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| `margen_impresion` | OK | OK | OK | OK | **AUSENTE** |

Drift practicamente nulo: la capa producto es la MAS estandarizada de
las 4 que vimos. Esto es contrario a `bodega` (123 cols union) y
`i_nav_config_enc` (78 cols union) que tenian drift severo.

### Catalogo agrupado de las 60 cols

#### Identidad y maestros
- `IdProducto`, `nombre`, `nombre_comercial`, `codigo`, `noparte`, `noserie`
- `IdMarca`, `IdFamilia`, `IdTipoProducto`, `IdProductoOrigen`
- `IdUnidadMedidaBasica`, `IdUnidadMedidaCobro`
- `IdSimbologia` (codigo de barras), `IdTipoEtiqueta`

#### Capability flags (BOOL)
- `activo`, `serializado`, `genera_lote`, `genera_lp_old`
- `control_lote`, `control_vencimiento`, `control_peso`
- `peso_recepcion`, `peso_despacho`
- `temperatura_recepcion`, `temperatura_despacho`
- `materia_prima`, `kit`, `es_hardware`
- `capturar_aniada`, `fechamanufactura`, `captura_arancel`

#### Clasificacion / rotacion
- `IdTipoRotacion` (FIFO/FEFO/LIFO etc), `IdIndiceRotacion`
- `IdTipoManufactura`, `IdPerfilSerializado`, `IdCamara`
- `IdArancel`, `tolerancia`, `ciclo_vida`, `dias_inventario_promedio`

#### Dimensiones / pesos
- `peso`, `volumen`, `alto`, `ancho`, `largo`
- `factor_compra`, `factor_venta`, `factor_compra_und`, `factor_venta_und`

#### Precio / stock
- `precio_compra`, `precio_venta`
- `stock_minimo`, `stock_maximo`, `punto_pedido`

#### DEPRECATED (NO usar en WebAPI nuevo)
- `IDPRODUCTOPARAMETROA`, `IDPRODUCTOPARAMETROB` — diseñados para venta
  unica de repuestos automotriz que nunca opero productivamente.
  Ningun cliente los usa hoy. Mantener solo por compatibilidad schema.
- Acompañan al flag `industria_motriz` de bodega (tambien deprecated).

#### Imagen / margen
- `imagen` (image), `margen_impresion` (ausente CEALSA)

#### Auditoria
- `IdPropietario`, `user_agr`, `fec_agr`, `user_mod`, `fec_mod`, `sistema`

---

## DISTRIBUCION DE FLAGS REALES — la verdad operativa

Esta es la matriz brutal: que flag esta poblado / activo en cada cliente.
Source: `SELECT col, COUNT(*) FROM producto WITH (NOLOCK) GROUP BY col`.

### Flags SIEMPRE False / 0 / NULL en TODOS los clientes
Capabilities NUNCA implementadas en TOM (a 28-abr-2026):

| Col | Estado en los 5 clientes |
|---|---|
| `serializado` | False en 100% |
| `kit` | False en 100% |
| `materia_prima` | False en 100% |
| `temperatura_recepcion` | False en 100% |
| `temperatura_despacho` | False en 100% |
| `capturar_aniada` | False en 100% (col `capturar_aniada` con r) |
| `captura_arancel` | False en 100% |
| `es_hardware` | False en 100% |
| `tolerancia` | 0 en 100% |
| `ciclo_vida` | 0 en 100% |
| `IdCamara` | NULL en 100% |
| `IdPerfilSerializado` | NULL en 100% |
| `IdUnidadMedidaCobro` | NULL en 100% |
| `IdArancel` | NULL en 100% |

Implicacion: estas cols son **schema preparado para futuro** que TOM
nunca prendio. El WebAPI puede ignorarlas en MVP — solo mantener nullable
para no romper. Si el dia de mañana un cliente las necesita, se activan.

### Flags con default homogeneo
| Col | Valor dominante | Excepciones |
|---|---|---|
| `dias_inventario_promedio` | 90 (default cross-cliente) | BYB 16 NULLs, CEALSA 27 con 0 |
| `fechamanufactura` | False | BECOFARMA 1 producto = True (caso edge) |

### Flag con drift TOTAL: `genera_lp_old`

| Cliente | True | False |
|---|---:|---:|
| BECOFARMA | 1.835 | 0 |
| KILLIOS | 319 | 0 |
| MAMPA | 31.397 | 0 |
| **BYB** | **136** | **487** |
| CEALSA | 1.714 | 0 |

BYB es el UNICO con productos sin `genera_lp_old` (78% en False). El
resto: 100% en True.

Lectura: el sufijo `_old` es revelador. `genera_lp` "sin _old" es flag
de bodega (`i_nav_config_enc`), no de producto. Modelo dual confirmado
en wave 3. `genera_lp_old` queda como capability legacy a nivel producto
que sobrevive solo por compatibilidad. **WebAPI puede deprecarlo si
genera_lp de bodega cubre el caso.**

### Flag de control de inventario: `control_lote`

| Cliente | True | False | True % |
|---|---:|---:|---:|
| BECOFARMA | 1.788 | 47 | 97.4% |
| KILLIOS | 318 | 1 | 99.7% |
| **MAMPA** | **0** | **31.397** | **0%** |
| BYB | 457 | 166 | 73.4% |
| CEALSA | 769 | 945 | 44.9% |

**MAMPA NO usa lote en NINGUN producto** — coherente con zapateria
(numero de lote no aplica). CEALSA tiene mixto 45%, BYB tiene mixto 73%.
BECOFARMA y K7 son lote-centricos.

### Flag de vencimiento: `control_vencimiento`

| Cliente | True | False | True % |
|---|---:|---:|---:|
| BECOFARMA | 1.787 | 48 | 97.4% |
| KILLIOS | 280 | 39 | 87.7% |
| **MAMPA** | **0** | **31.397** | **0%** |
| BYB | 457 | 166 | 73.4% |
| **CEALSA** | **24** | **1.690** | **1.4%** |

**MAMPA NO controla vencimiento** (zapatos no vencen).
**CEALSA practicamente NO controla vencimiento** (24 productos / 1.4%).
Los demas si controlan vencimiento mayoritariamente.

Cruce con `control_lote` confirma: lote y vencimiento van casi siempre
juntos (mismas filas True en BECO y BYB).

### Flag de peso: `control_peso` + `peso_recepcion` + `peso_despacho`

| Cliente | control_peso True | peso_recep True | peso_desp True |
|---|---:|---:|---:|
| BECOFARMA | 0 | 0 | 0 |
| KILLIOS | 0 | 0 | 0 |
| MAMPA | 0 | 0 | 0 |
| BYB | 0 | 0 | 0 |
| **CEALSA** | **6** | **6** | **6** |

CEALSA es el UNICO con productos por peso: 6 productos exactos
(probablemente carnes / quesos al peso). Y los 3 flags se prenden
juntos (peso end-to-end).

### IdSimbologia (codigo de barras)

| Cliente | Poblado | NULL | Poblado % |
|---|---:|---:|---:|
| BECOFARMA | 93 | 1.742 | 5.1% |
| KILLIOS | 12 | 307 | 3.8% |
| MAMPA | 0 | 31.397 | 0% |
| BYB | 34 | 589 | 5.5% |
| **CEALSA** | **798** | **916** | **46.6%** |

CEALSA poblado masivo (47%). El resto residual. MAMPA cero — usa
etiqueta interna de WMS (`IdTipoEtiqueta=12`), no simbologia ERP.

### IdTipoEtiqueta (template de impresion)

| Cliente | Valor dominante | Significado inferido |
|---|---|---|
| BECOFARMA | 8 (100%) | Template farma |
| KILLIOS | 10 (100%) | Template gastro |
| MAMPA | 12 (100%) | Template zapato (talla/color) |
| BYB | 2 (99.7%) | Template estandar |
| CEALSA | 2 (99.8%) | Template estandar |

PATRON: cada cliente tiene un template dedicado. NO hay un default
universal. WebAPI debe permitir custom templates por cliente.

### IdTipoRotacion (politica de rotacion)

| Cliente | Valor dominante | Significado inferido |
|---|---|---|
| BECOFARMA | 3 (93%) | FEFO (vencimiento) |
| KILLIOS | 3 (89%) | FEFO |
| **MAMPA** | **1 (99.6%)** | **FIFO** (zapatos sin vencimiento) |
| BYB | 3 (99.8%) | FEFO |
| CEALSA | NULL (100%) | NO definido — usa default sistema |

Lectura: BECO/K7/BYB son FEFO. MAMPA es FIFO puro (sin vencimiento).
CEALSA dejo todos los productos en NULL (?).

### IdTipoProducto

| Cliente | Distribucion | Lectura |
|---|---|---|
| BECOFARMA | 1=1.441, 2=305 | 2 categorias farma |
| KILLIOS | 1=268, 2=51 | 2 categorias gastro |
| **MAMPA** | **35=3.292, 22=1.845, 21=1.378, 34=1.184, 7=990, ...** | **Catalogo MASIVO de tipos por talla/color** |
| BYB | 2062=162, 2099=52, 2005=50, ... | Catalogo NAV (codigos largos) |
| CEALSA | NULL (100%) | NO clasifica por tipo |

CEALSA tampoco clasifica por tipo (NULL). MAMPA tiene la taxonomia mas
rica (probablemente cada categoria de zapato).

### IdFamilia (jerarquia comercial)

| Cliente | Distribucion | Lectura |
|---|---|---|
| BECOFARMA | 1=1.718, NULL=117 | 1 familia dominante |
| KILLIOS | NULL (100%) | NO usa familia |
| MAMPA | 1=20.718, 2=4.334, 6=2.354, 17=725, ... | Multi-familia |
| BYB | 332=161, 330=51, 291=50, ... | Catalogo NAV |
| CEALSA | NULL (100%) | NO usa familia |

K7 y CEALSA NO clasifican por familia.

### IdIndiceRotacion

| Cliente | Estado |
|---|---|
| BECOFARMA | NULL (100%) |
| KILLIOS | NULL (100%) |
| **MAMPA** | **4 (100%)** — UNICO con valor |
| BYB | NULL en 622, 3 en 1 (residual) |
| CEALSA | NULL (100%) |

MAMPA es el UNICO cliente que populariza este flag. Significado
desconocido — abrir Q-MAMPA-IDINDICE-4 (ya estaba en el INDEX).

### IdUnidadMedidaBasica

| Cliente | Distribucion |
|---|---|
| BECOFARMA | 1 (100%) — unidad fija |
| KILLIOS | 1 (100%) — unidad fija |
| **MAMPA** | **3=20.804, 4=10.567, otros (NULL=21, 5=1, 6=1)** |
| BYB | 100 (99%), 101 (1%) |
| **CEALSA** | **2582, 3804, 3649, 1041, 3866, ...** (5+ valores) |

MAMPA tiene 2 unidades dominantes (3 y 4 — probablemente PAR / CAJA).
BYB usa codigos NAV (100, 101). CEALSA tiene catalogo de unidades
muy diverso (distribuidor de varias categorias).

---

## `producto_bodega` — sin drift

Schema **identico** en los 5 clientes (9 cols):
```
IdProductoBodega, IdProducto, IdBodega, activo, sistema,
user_agr, fec_agr, user_mod, fec_mod
```

Es solo asociacion N:M producto-bodega con flag `activo`. NO tiene
parametros funcionales. WebAPI puede tratarlo como pure join table.

### Stats

| Cliente | bodegas | productos | filas | densidad |
|---|---:|---:|---:|---|
| BECOFARMA | 1 | 1.835 | 1.835 | 100% |
| KILLIOS | 6 | 319 | 1.914 | 100% |
| MAMPA | 33 | 31.397 | 1.036.101 | 100% |
| BYB | 2 | 623 | 1.109 | 89% |
| CEALSA | 2 | 1.714 | 3.428 | 100% |

**MAMPA: 1.036.101 filas en producto_bodega**. Es la tabla mas grande
del WMS por largo. WebAPI debe tener paginacion / filtros agresivos
en endpoints que consulten esto.

BYB con 89% (1.109 / 1.246 esperado) — algunos productos NO estan en
las 2 bodegas. Patron: producto solo en 1 de las 2 bodegas.

---

## `producto_estado` — el catalogo de estados

Schema cross-cliente:

| Cliente | Cols |
|---|---:|
| BECOFARMA | 16 |
| KILLIOS | 15 |
| MAMPA | 16 |
| BYB | 16 |
| CEALSA | 15 |

### Cols ausentes
| Col | BECO | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| `reservar_en_umbas` | OK | **AUSENTE** | OK | OK | **AUSENTE** |

Las 15 cols comunes:
```
IdEstado, IdPropietario, nombre, IdUbicacionDefecto, utilizable,
activo, user_agr, fec_agr, user_mod, fec_mod, dañado,
codigo_bodega_erp, sistema, dias_vencimiento_clasificacion,
tolerancia_dias_vencimiento
```

`reservar_en_umbas` es flag adicional en BECO/MAMPA/BYB. Significado
desconocido — abrir **Q-RESERVAR-EN-UMBAS** (UMBA = unidad de medida
basica? unidad mobile boliviana?). Investigar.

### CASO CEALSA 3.200 ESTADOS — RESUELTO

`producto_estado` en CEALSA tiene **3.200 filas**:
- 3.197 con `nombre='Buen Estado'` y un `IdPropietario` distinto cada una
- 1 con `nombre='Mal Estado'`
- 1 con `nombre='RH'`
- 1 con `nombre='HR'`

Fechas: insertadas entre **2022-01-27 y 2025-04-15** por user_agr=`6`
(3.194 de las 3.200). Patron: cada nuevo IdPropietario que aparece en
el sistema, se le crea automaticamente una fila "Buen Estado".

**Pero TOM no usa propietarios productivamente** (la tabla `propietario`
ni siquiera existe — solo tablas relacionadas `propietario_destinatario`,
`propietario_reglas_enc`, etc, y vistas `VW_Movimientos_Propietario`).

`SELECT COUNT(DISTINCT IdPropietario) FROM producto`:
- BECOFARMA: 1
- KILLIOS: 1
- MAMPA: 1
- BYB: 1
- **CEALSA: 3** (los unicos 3 propietarios reales en producto)

### Diagnostico CEALSA

Los 3.197 IdPropietario en `producto_estado` son **scaffolding heredado**
de un proceso automatico que carga "Buen Estado" para cada propietario
nuevo, pero los 3.197 propietarios NO existen como productos. Solo 3
son operativos.

Significado practico: las 3.197 filas son **ruido** desde la perspectiva
WMS. WebAPI debe consultar `producto_estado` con `JOIN producto` o
con WHERE filtrando solo IdPropietario que tienen producto real.

Aclaracion abierta: **Q-CEALSA-ORIGEN-PROP-3197** — quien o que dispara
la insercion automatica? Probable trigger / job nightly. Investigar codigo
backend cuando hagamos code-deep-flow.

### Catalogo de estados real por cliente

#### BECOFARMA (10 estados)
Mapeo 1:1 estado → bodega ERP fisica:
```
Id  Nombre              codigo_bodega_erp
1   Buen Estado         01
3   Pendiente NC        15
4   CV11                04
5   CV6                 05
6   Danado              02
7   Destruccion         14   (sistema=False)
8   Muestra Medica      08   (sistema=False)
9   INSTITUCIONAL       09   (sistema=False)
10  Vencidos            06   (sistema=False)
17  Retencion           17   (sistema=False)
```
**PATRON BECO**: cada estado funcional tiene su PROPIA bodega ERP de
respaldo. Estados administrativos (Destruccion, Muestra, Institucional,
Vencidos, Retencion) son `sistema=False` (gestionables por usuario).

#### KILLIOS (18 estados)
Vocabulario industrial detallado (lacteos / refrigerados):
```
Buen Estado, Destruir, Auditoria, Fuga de tapa, Derrame,
Golpeado o lastimado, Rechazo, Reempacar, Mal estado Reemplazo,
No Encontrado, Despachado, Corregir Lote Fecha, Vencido,
Nota credito, Apachado, Mal estado, Sin etiqueta, Cambio proveedor
```
`codigo_bodega_erp='0'` o vacio en TODOS — K7 NO mapea estado a bodega
ERP. Patron diferente a BECO.

#### MAMPA (19 estados)
Vocabulario para perecederos (carniceria + abarrotes + refrigerados):
```
Buen Estado, Mal Estado, No encontrado, Estado NC,
No disponible (todas las variantes locales/import x abarrotes/refri/cong/cont),
Merma Carne Res, Merma Carne Cerdo
```
Granularidad por TIPO DE PRODUCTO + ORIGEN (Local/Import) + DETERIORO
(Danado/Vencido/Contaminado).

#### BYB (24 estados)
Vocabulario laboratorio / industria (Trebolac):
```
Buen Estado, Mal Estado, Auditoria, Fuga de tapa, Derrame,
Golpeado o lastimado, Lastimado, NEPICKING, Cuarentena_Trebolac,
Virtual_TB, Producto para departamentos, Nivel bajo, Producto grumoso,
Fallo Etiqueta, Consistencia liquida, Envase con defecto,
Cliente Especial, Vencido, NO DISPONIBLE NAV, NO DISPONIBLE NAV BD,
Bajo nivel, fallo de impresion, Monitoreo de producto, Devolucion Venta
```
Estados `NO DISPONIBLE NAV` y `NO DISPONIBLE NAV BD` confirman que BYB
es NAV y tiene estados dedicados a desincronizacion ERP.

#### CEALSA (4 estados utiles + 3.196 ruido)
```
Buen Estado, Mal Estado, RH, HR
```
RH y HR — significado desconocido (recursos humanos? rotacion humana?).
Catalogo MUY pobre comparado con el resto. CEALSA opera con minimo
overhead.

### Lecturas brutales transversales

1. **Cada cliente tiene su propio vocabulario**: NO hay estados
   estandarizados. El WebAPI debe permitir catalogo de estados libre por
   cliente.

2. **codigo_bodega_erp solo significativo en BECOFARMA**: el resto deja
   '0' o vacio. BECO mapea estado → bodega NAV especifica para
   movimientos contables.

3. **`sistema=True` significa estado de sistema (no editable)**:
   - BECO: 5 estados sistema, 5 usuario.
   - K7: ~3 sistema, 15 usuario.
   - MAMPA: ~17 sistema, 2 usuario.
   - BYB: 0 sistema visibles (todos True parecen pero reportados como
     sistema=False mayoritariamente).
   - CEALSA: 0 (todos `sistema=False`).

4. **`utilizable=True` con `dañado=True`** = paradoja aparente. Existe
   en MAMPA y K7 (estado dañado pero todavia se puede mover/operar).
   Diferente del estandar que asume utilizable XOR dañado.

5. **`reservar_en_umbas` ausente en K7 y CEALSA**: capability adicional
   que solo BECO/MAMPA/BYB tienen. Hipotesis: reserva en unidad de medida
   basica para casos donde la conversion afecta. Investigar.

---

## `producto_estado_ubic` — sin drift

Schema **identico** en los 5 clientes (9 cols):
```
IdProductoEstadUbic, IdEstado, IdUbicacionDefecto, IdBodega,
activo, fec_agr, user_agr, fec_mod, user_mod
```

### Stats — uso muy bajo

| Cliente | filas |
|---|---:|
| BECOFARMA | 4 |
| KILLIOS | 2 |
| MAMPA | 20 |
| BYB | 2 |
| CEALSA | 1 |

Tabla **practicamente vacia** en TODOS los clientes. Es relacion
estado → ubicacion por defecto en bodega especifica.

Solo MAMPA tiene 20 filas (probablemente 1 fila por bodega de las
33 con estado default + algunas extras). El resto: edge cases.

Implicacion WebAPI: NO es feature critica. Considerar de baja prioridad.

---

## Capabilities derivadas — capa producto

Resumen de las capabilities que se desprenden de los flags reales:

| Capability | BECO | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| inventory-by-lot | ON 97% | ON 99% | OFF 0% | ON 73% | MIXTO 45% |
| inventory-by-expiration | ON 97% | ON 88% | OFF 0% | ON 73% | OFF 1% |
| inventory-by-weight | OFF | OFF | OFF | OFF | ON 0.4% (6 productos) |
| inventory-by-serial | OFF | OFF | OFF | OFF | OFF |
| catalog-by-size-color | (in bodega) | OFF | ON via bodega | preparado | OFF |
| barcode-with-symbology | 5% | 4% | OFF 0% | 5% | **47%** |
| custom-label-template | T8 | T10 | T12 | T2 | T2 |
| rotation-fefo | ON 93% | ON 89% | OFF | ON 99% | NO def |
| rotation-fifo | OFF | OFF | ON 99% | OFF | NO def |
| product-active-soft-delete | OFF | OFF | OFF | OFF | **ON** (79 inactivos) |
| product-state-mapped-to-erp-warehouse | ON | OFF | OFF | OFF | OFF |
| state-vocabulary-rich | medio (10) | rico (18) | rico (19) | rico (24) | pobre (4) |

---

## Hipotesis abiertas (nuevas Q-*)

- **Q-RESERVAR-EN-UMBAS**: que es UMBA y por que solo BECO/MAMPA/BYB
  tienen `reservar_en_umbas` en `producto_estado`.
- **Q-CEALSA-ORIGEN-PROP-3197**: trigger / job que crea Buen Estado
  por propietario en CEALSA. Investigar en code-deep-flow.
- **Q-MAMPA-IDINDICE-4**: significado del valor 4 (cargado) — abierta
  desde wave 4.
- **Q-CEALSA-RH-HR**: que significan estados RH y HR en CEALSA.
- **Q-CEALSA-IDTIPO-NULL**: por que CEALSA dejo IdTipoProducto en NULL
  para todos los productos.
- **Q-BYB-NO-DISPONIBLE-NAV-BD**: diferencia entre `NO DISPONIBLE NAV`
  y `NO DISPONIBLE NAV BD` en BYB.
- **Q-MAMPA-MERMA-CARNE-FLUJO**: como se maneja el flujo de merma de
  carne (estados 20 y 21) — es proceso especifico.
- **Q-GENERA-LP-OLD-LEGADO**: confirmar que `genera_lp_old` puede
  deprecarse en WebAPI si `genera_lp` (bodega) cubre el caso.

---

## Lecturas para WebAPI .NET 10

1. **Endpoint `/producto`** debe soportar paginacion agresiva. MAMPA
   con 31.397 productos x 33 bodegas es el caso de presion.

2. **Capability flags como contract**: el endpoint debe devolver
   capability flags expanded (control_lote, control_vencimiento, etc)
   para que el HH no consulte el catalogo entero. Cachear por producto.

3. **producto_bodega es solo join**: tratarlo como pure relation. No
   merece endpoint propio — incluir en query de producto con
   `?include=bodegas`.

4. **producto_estado debe permitir catalogo libre por cliente**: NO
   estandarizar nombres. Permitir N estados por cliente con
   `sistema=True/False` para gobernanza.

5. **DEPRECATED no exponer**: `IDPRODUCTOPARAMETROA/B`,
   `industria_motriz`, `genera_lp_old` (a confirmar) — no incluir en
   los DTOs publicos del WebAPI nuevo, solo en compatibility layer.

6. **CEALSA scaffolding ruido**: filtrar `producto_estado` por
   IdPropietario presente en producto, no listar las 3.200 filas crudas.

7. **Soft delete de producto**: solo CEALSA usa `activo=False`. WebAPI
   debe soportar el flag para no romper CEALSA.

8. **MAMPA caso especial**: control_lote=False + control_vencimiento=False
   + IdTipoRotacion=1 (FIFO) — todo el modelo de inventario es distinto.
   Considerar perfil "calzado/talla-color" como modo de operacion alterno.

9. **CEALSA caso especial**: control_lote/vencimiento mixto, control_peso
   real (6 productos), simbologia poblada masiva (47%). Modelo
   distribuidor general.

10. **BECO/K7**: lote+vencimiento mayoritario, FEFO. Caso "estandar"
    farma/gastro.

11. **BYB**: lote+vencimiento mayoritario, FEFO, NAV. Caso "estandar"
    industrial NAV.

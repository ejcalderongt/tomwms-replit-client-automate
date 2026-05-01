---
id: 04-mi3-y-reserva-clavaud
tipo: code-deep-flow
estado: vigente
titulo: 04 — MI3, Estrategia Clavaud y Algoritmo de Reserva de Stock
ramas: [dev_2028_merge]
tags: [code-deep-flow]
---

# 04 — MI3, Estrategia Clavaud y Algoritmo de Reserva de Stock

> **Wave 8 (2026-04-29) — Resolución de Q-CLAVAUD-MEANING + descubrimiento del módulo MI3 + traza completa del algoritmo de reserva**
> Fuentes: revelación verbal Erik 2026-04-29 (anécdota Marcelo Clavaud / gerente logística Panamá) + lectura `clsLnStock_res_Partial.vb` (4374 líneas, reescrito en 2028) + estructura del proyecto `MI3/` y `CEALSAMI3/`.
> Estado: Clavaud RESUELTO con evidencia código + dato (función `Reserva_Stock` con parámetro `pExcluirUbicacionPicking`). MI3 mapeado al 80%.

---

## TL;DR

1. **Clavaud no es jerga técnica**. Es **Marcelo Clavaud**, gerente de logística cliente IDEALSA Panamá (MERCOPAN). Le pasó al WMS un problema real:
   > "Si reservás el pedido completo desde la zona de picking, dejás sin operar al operario de piso porque el reabasto del primer nivel no es instantáneo. Cuello de botella operativo y financiero."

   La solución se bautizó **estrategia Clavaud** y vive en el flag `i_nav_config_enc.conservar_zona_picking_clavaud` (bit). El nombre quedó porque fue su pedido específico.

2. **MI3 = Módulo de Integración con Terceros**. Es un **proyecto WCF/SOAP completo** (carpeta `MI3/` en BOF) con services para Bodega, Cliente, Direcciones, Documentos, Barras_Pallet, etc. Es la API que expone el WMS al ERP (NAV/SAP) y a sistemas externos. **Es el eufemismo interno** para "nuestra interface".

3. **El algoritmo de reserva 2028 está completamente reescrito**. `clsLnStock_res_Partial.vb` pasa de ~600 líneas a **4374**. Tiene **13+ funciones de reserva** (con variantes para NAV+BYB, lista, específico, reemplazo automático, consolidado).

4. **El ranking no se hace en SQL**, se hace en **memoria con LINQ** (`OrderBy`/`OrderByDescending`) según `IdTipoRotacion`:
   - 1 FIFO → `OrderBy(x => x.Fecha_Ingreso)`
   - 2 LIFO → `OrderByDescending(x => x.Fecha_Ingreso)`
   - 3 FEFO → `OrderBy(x => x.Fecha_vence)`
   - 4 UPSR → acrónimo aún sin definir (Q-* abierta)

5. **`IdTipoRotacion` vive en 3 tablas** (cascada de precedencia probable producto > ubicación > config bodega). Pendiente confirmar el orden exacto.

---

## Capa 1 — El problema de negocio (caso Clavaud)

### Setup operativo
- Bodega Panamá tiene zona de **picking en primer nivel** (`bodega_ubicacion.nivel = 1` + `ubicacion_picking = True`).
- Operadores de piso (sin montacarga) trabajan **solo en picking** para operaciones de detalle (cajas sueltas).
- Los niveles superiores (`nivel > 1`, "rack") almacenan pallets completos accesibles solo con montacargas.
- Producto X: 20 pallets en picking (primer nivel del rack X), cada pallet 5 cajas → **100 cajas accesibles** en picking.

### Conflicto
- Llega pedido por **100 cajas** del producto X.
- Stock candidato disponible: las 100 cajas en picking (FEFO/FIFO match) Y los pallets en niveles superiores (mismo lote/vencimiento).
- **Algoritmo "naive"**: reservar primero lo que está más a mano = **vaciar picking**.
- **Consecuencia operativa**: operador de piso se queda sin stock para cubrir pedidos pequeños que llegan después. Espera reabasto. Cuello de botella. Pérdida financiera.

### Insight de Marcelo Clavaud
> "Si el pedido equivale a Z pallets completos (calculable por `producto_presentacion.factor`), tomá los pallets directo del rack y dejá picking intacto. Si el pedido es menor a un pallet completo, ahí sí usá picking."

### Aplicabilidad
- **Pedidos a clientes**: el furgón se carga con 18-20 pallets, conviene cargar pallets enteros del rack.
- **Transferencias entre bodegas**: idem, viajan en pallets.
- **Pedidos de detalle (< 1 pallet)**: ahí sí va por picking.

→ Decisión "picking vs rack" = **función del tamaño del pedido vs factor del producto**.

---

## Capa 2 — Implementación: el flag y el parámetro

### Flag de capability
- Tabla: `i_nav_config_enc`
- Columna: `conservar_zona_picking_clavaud` (bit, default 0)
- Migration: `--#EJC202211071706: alter table i_nav_config_enc add conservar_zona_picking_clavaud bit default 0` (commit 2022-11-07, comentario en `commits.txt`)
- Por bodega/propietario: cada combinación puede tener su propia decisión.

### Cómo se materializa en código
```vb
' En clsLnStock_res_Partial.vb línea 138 (función core de reserva):
Public Shared Function Reserva_Stock(ByRef pStockRes As clsBeStock_res,
                                     ByVal pIdPropietario As Integer,
                                     ByVal DiasVencimiento As Double,
                                     ByVal MaquinaQueSolicita As String,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction,
                                     Optional ByVal pExcluirUbicacionPicking As Boolean = False,  ' ← AQUÍ
                                     Optional BeConfigEnc As clsBeI_nav_config_enc = Nothing) As Boolean
```

### Decisión upstream (en `clsLnTrans_pe_det_Partial.vb`)
El caller (típicamente desde el flujo de pedido) hace:
1. Lee `BeConfigEnc.Conservar_Zona_Picking_Clavaud`
2. Calcula `vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor`
3. Calcula equivalente en pallets enteros usando `producto_presentacion.CamasPorTarima` y `CajasPorCama`
4. Si `Conservar_Zona_Picking_Clavaud = True` AND el pedido equivale a 1+ pallet completo → llama `Reserva_Stock(..., pExcluirUbicacionPicking := True)`
5. Caso contrario → llama `Reserva_Stock(..., pExcluirUbicacionPicking := False)` y deja que el ranking normal decida

> **NOTA**: el `MsgBox("Parámetro conservar zona picking: ...")` está **comentado** en línea 3947 de `clsLnTrans_pe_det_Partial.vb`. Era un debug de desarrollo. La lógica real está integrada implícitamente en el flow del pedido. **Pendiente** ubicar el branching exacto que decide picking-vs-rack según pallets enteros.

### Resultado
- Si `pExcluirUbicacionPicking = True` → el query interno de `clsLnStock.lStock` agrega filtro `WHERE bodega_ubicacion.ubicacion_picking = 0` → solo trae candidatos de niveles altos.
- El ranking FIFO/FEFO se aplica **dentro del set ya filtrado**. Si todo el rack está vacío y solo queda picking, falla y deja sin reservar (el caller debe manejarlo).

---

## Capa 3 — Modelo de datos asociado

### `bodega_ubicacion` (38 columnas, MERCOPAN tiene 2.903 ubicaciones distribuidas en 7 niveles)
Cols clave:
| Col | Tipo | Rol |
|-----|------|-----|
| `IdUbicacion` | int | PK |
| `IdBodega` | int | a qué bodega pertenece |
| `IdTramo` | int | tramo dentro del rack |
| `nivel` | int | **0..6** (0=muelle/recepción, 1=picking, 2-5=rack, 6=top) |
| `IdTipoRotacion` | int | rotación POR UBICACIÓN (FIFO/LIFO/FEFO/UPSR) |
| `IdIndiceRotacion` | int | sub-índice de rotación |
| `ubicacion_picking` | bit | **flag explícito de zona picking** (independiente del nivel) |
| `ubicacion_recepcion` | bit | zona recepción |
| `ubicacion_despacho` | bit | zona despacho |
| `ubicacion_merma` | bit | zona merma |
| `ubicacion_muelle` | bit | muelle de carga |
| `ubicacion_virtual` | bit | virtual (no física) |
| `ubicacion_ne` | bit | "no estructurada"? — sin definir |
| `acepta_pallet` | bit | si admite pallet completo |
| `bloqueada` / `dañado` | bit | razones de exclusión |
| `posicion_x` / `posicion_y` | float | coordenadas físicas (para warehouse map UI) |

**Distribución MERCOPAN** (1 sola bodega):
```
nivel 0: 20 ubicaciones    (muelles, recepción)
nivel 1: 626 ubicaciones   ← PICKING
nivel 2: 626 ubicaciones   ← rack bajo
nivel 3: 650 ubicaciones   ← rack medio
nivel 4: 654 ubicaciones   ← rack medio-alto
nivel 5: 322 ubicaciones   ← rack alto
nivel 6: 5 ubicaciones     ← top
TOTAL:  2.903 ubicaciones
```

### `producto_presentacion` (29 columnas)
Cols clave para Clavaud:
| Col | Rol |
|-----|-----|
| `factor` | float — relación cajas/pallet o equivalente UMB |
| `EsPallet` | bit — flag presentación pallet |
| `IdPresentacionPallet` | int — FK a la presentación pallet asociada |
| `CamasPorTarima` | float — geometría: cuántas camas por tarima |
| `CajasPorCama` | float — geometría: cuántas cajas por cama |
| `MinimoExistencia` / `MaximoExistencia` | float — reglas de stock |
| `MinimoPeso` / `MaximoPeso` | float — control peso variable |
| `genera_lp_auto` | bit — autogenera LP al recepcionar |
| `permitir_paletizar` | bit |

→ **Cálculo Z pallets**: `Z = ceil(cantidad_cajas / (CamasPorTarima * CajasPorCama))` o `Z = cantidad_cajas / (factor_pallet)`.

### `tipo_rotacion` (catálogo, 4 valores)
```
1 = FIFO  (First In, First Out)
2 = LIFO  (Last In, First Out)
3 = FEFO  (First Expired, First Out)  ← perecederos
4 = UPSR  (acrónimo desconocido — Q-* abierta)
```

### `IdTipoRotacion` vive en 3 tablas (cascada de precedencia probable)
```
producto.IdTipoRotacion              ← nivel producto (más específico)
bodega_ubicacion.IdTipoRotacion      ← nivel ubicación
i_nav_config_enc.IdTipoRotacion      ← nivel bodega (default)
```
También en: `estructura_ubicacion`, `regla_ubic_det_tr`, `ubicaciones_por_regla`, `tmp_*` (tablas de reglas y temporales).

→ **Q-ROTACION-PRECEDENCIA**: ¿el WMS evalúa `producto > ubicacion > config_bodega` o al revés? Pendiente confirmar con Erik.

---

## Capa 4 — El algoritmo `Reserva_Stock` en detalle

### Firma y flujo (líneas 138-440)
```vb
Public Shared Function Reserva_Stock(
    ByRef pStockRes As clsBeStock_res,        ' lo que se quiere reservar (entrada/salida)
    ByVal pIdPropietario As Integer,
    ByVal DiasVencimiento As Double,           ' filtro: cuántos días aceptar de vencimiento
    ByVal MaquinaQueSolicita As String,        ' trazabilidad: qué máquina solicita
    ByRef lConnection As SqlConnection,
    ByRef lTransaction As SqlTransaction,
    Optional ByVal pExcluirUbicacionPicking As Boolean = False,  ' ← Clavaud
    Optional BeConfigEnc As clsBeI_nav_config_enc = Nothing
) As Boolean
```

**Pasos**:
1. Carga config (`BeConfigEnc`) — branching por `Operador_logistico` (ver capa 5)
2. Llama `clsLnStock.lStock(pStockRes, BeProducto, DiasVencimiento, BeConfigEnc, ...)` que retorna `lBeStockExistente` (lista candidatos)
3. Si `pExcluirUbicacionPicking = True`, el query interno de `lStock` filtra por `ubicacion_picking = 0`
4. Calcula `vCantidadSolicitadaPedido` convirtiendo presentación → UMB (`Math.Ceiling(decimal * factor)`)
5. Valida control peso por producto (`Tiene_Control_Por_Peso_By_IdProductoBodega`)
6. **Itera sobre el stock candidato ya ordenado** (ver capa 5 abajo) y va deduciendo cantidad hasta cubrir el pedido
7. Inserta `clsBeStock_res` por cada IdStock consumido

### Caracteristicas
- Es **stateful y transaccional** (recibe `lConnection`/`lTransaction`)
- Tiene **cache in-memory** de configs (`lBeConfigInMemory.Add(BeConfigEnc.Clone())`)
- **Manejo de error** lanza `Exception("ERROR_202210182024: ...")` si no encuentra config
- **Comentarios EJC** datados (ej: `'#EJC20180614:`, `'#EJC20190602_0137AM:`) → patrón Erik para auditar cambios

---

## Capa 5 — El ranking (líneas 4564-4736)

### Bloque de ordenamiento (LINQ en memoria, NO en SQL)
```vb
' Hay DOS bloques Case (líneas 4564 y 4722) — uno para reserva normal, otro para reemplazo
Select Case BeConfigEnc.IdTipoRotacion  ' o el de la ubicación/producto, según precedencia
    Case 1 'FIFO
        StockOrdenado = lBeStockExistente.OrderBy(Function(x) x.Fecha_Ingreso).ThenBy(...)
    Case 2 'LIFO
        StockOrdenado = lBeStockExistente.OrderByDescending(Function(x) x.Fecha_Ingreso).ThenBy(...)
    Case 3 'FEFO
        StockOrdenado = lBeStockExistente.OrderBy(Function(x) x.Fecha_vence).ThenBy(...)
    Case 4 'UPSR (?)
        ' implementación pendiente de inspeccionar
End Select
```

### Implicaciones
- **Performance**: trae TODO el stock candidato al .NET y ordena ahí. Para clientes con miles de filas de stock por producto → cuello de botella. Mitigación: el filtro previo (bodega + propietario + producto + vencimiento + picking-or-not) reduce el set.
- **No usa `ORDER BY` SQL** porque el ranking depende de campos calculados o config dinámica.
- **`ThenBy(...)`** sugiere **criterios secundarios** (probable: `Fecha_Ingreso` luego `IdStock` para desempate).

### Branch específico: `Reserva_Stock_NAV_BYB` (línea 442, ~415 líneas)
- Función específica para **NAV+BYB** (cliente BECO + integración NAV).
- Probablemente porque BYB tiene LP largo correlativo NAV (ver Wave 6.2 / `traza-001-license-plate.md`) y necesita lógica especial de match.

---

## Capa 6 — Patrón Operador_logistico (3PL vs 1PL/2PL)

### Branching descubierto (`clsLnTrans_pe_det_Partial.vb` cerca línea 3947)
```vb
If Not pEmpresa.Operador_logistico Then
    BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(...)
Else
    BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(...)
End If
```

### Significado
- **3PL (CEALSA, donde `empresas.Operador_logistico = True`)**: la config se busca por (IdBodega, IdEmpresa). Cada empresa cliente tiene su config independiente dentro de la misma bodega.
- **1PL/2PL (BECO, BYB, K7, MAMPA, MERHONSA, MERCOPAN, donde `Operador_logistico = False`)**: la config se busca por (IdBodega, IdPropietario). Como tienen 1 solo propietario default, hay 1 config por bodega.

→ **Resuelve definitivamente Q-PROPIETARIO-AGNOSTICO**: el WMS soporta ambos modelos via la columna `Operador_logistico` en `empresas`.

---

## Capa 7 — MI3 (Módulo de Integración con Terceros)

### Estructura del proyecto `MI3/`
Es un **proyecto Visual Basic separado** dentro del repo BOF. Genera `MI3.dll` y servicios WCF.

```
MI3/
├── MI3.vbproj
├── My Project/PublishProfiles/MI3.pubxml      ← config publish IIS
├── Barras_Pallet/
│   ├── Barras_Pallet.svc                       ← WCF endpoint
│   ├── Barras_Pallet.svc.vb                    ← implementación
│   └── IBarras_Pallet.vb                       ← interface
├── Bodega/
│   ├── ServiceBodega.svc / .svc.vb / IServiceBodega.vb
│   ├── Bodega_Sector/ServiceBodegaSector.svc...
│   └── Bodega_Tramo/ServiceBodegaTramo.svc...
├── Cliente/
│   ├── Cliente/ServiceCliente...
│   ├── Cliente_Bodega/ServiceClienteBodega...
│   ├── Cliente_Direcciones_Entrega/ServiceDirecciones...
│   ├── Cliente_Tiempos/ServiceClienteTiempos...
│   └── Cliente_Tipo/ServiceClienteTipo...
└── Documento_Ingreso_Ref/
    ├── srvDocumentoIngresoRef.svc
    └── IsrvDocumentoIngresoRef.vb
```

### Métodos de `ServiceBodega.svc` (ejemplo)
```vb
Public Function Get_Single_By_IdBodega(pIdBodega As Integer) As clsBeBodega
Public Function Get_IdBodegaWMS_By_Codigo_Bodega_ERP(pCodBodegaERP As String) As Integer
Public Function Get_IdBodegaWMS_By_Codigo(pCodigoBodega As String) As Integer
Public Function Get_All() As List(Of clsBeBodega)
Public Function Inserta_I_NAV_Bodega(pBeBodegaERP As clsBeI_nav_bodega) As Integer
Public Function Get_All_I_Nav_Bodega() As List(Of clsBeI_nav_bodega)
```

→ Cada service expone CRUD + queries específicas para que el ERP (NAV/SAP) consulte/actualice datos del WMS.

### `CEALSAMI3/` (variante específica para CEALSA)
**No es WCF**, es **una app de sincronización standalone** (`CEALSASYNC.sln`).
```
CEALSAMI3/
├── CEALSASYNC.sln                               ← solución
├── CELSASYNC.vbproj                             ← typo: CELSA en vez de CEALSA
├── Conn.ini / ConnDev.ini                       ← conexiones (prod/dev)
├── AppGlobal/                                   ← config global
├── Clases/
│   ├── BaseDatos.vb
│   ├── IMS.vb                                   ← cliente IMS
│   ├── ModuleMain.vb                            ← entry point
│   └── m_Global.vb
└── DAL/
    ├── DAL.vbproj
    └── Transacciones/Movimiento/
        ├── dsUbicSug.xsd                        ← ★ DATASET "Ubicación Sugerida"
        └── dsUbicSug.xsc
```

→ **`dsUbicSug` (Ubicación Sugerida)** es probablemente el **motor de sugerencia de ubicación** para movimientos. Crítico para entender la decisión "dónde colocar X producto al recepcionar".

### Diferencia MI3 vs CEALSAMI3
| Aspecto | MI3 | CEALSAMI3 |
|---------|-----|-----------|
| Tipo | WCF/SOAP services | App de sincronización standalone |
| Despliegue | IIS web service | Servicio Windows o tarea programada |
| Cliente | NAV/SAP/cualquiera vía SOAP | Específico CEALSA |
| Datasets | DataContracts WCF | `dsUbicSug` y otros XSD |

→ **Q-MI3-VS-CEALSAMI3**: ¿CEALSA usa MI3 ADEMÁS de CEALSAMI3, o reemplazó MI3? ¿Otros clientes tienen su propio `<CLIENTE>MI3/`?

---

## Síntesis: el flujo end-to-end

```
1. ERP (NAV/SAP) crea pedido y lo manda al WMS (vía MI3.svc o sync)
   ↓
2. WMS recibe pedido detalle (clsBeTrans_pe_det) con cantidad + presentación
   ↓
3. clsLnTrans_pe_det_Partial.Reservar_Stock_Por_Linea(pBePedidoDet, ...):
   ├─ Lee config: BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_(IdPropietario|IdEmpresa)
   │  └─ branching por pEmpresa.Operador_logistico (3PL vs 1PL/2PL)
   ├─ Convierte cantidad a UMB usando BePres.Factor
   ├─ Calcula equivalente en pallets enteros (CamasPorTarima * CajasPorCama)
   └─ Decide pExcluirUbicacionPicking:
      └─ Si BeConfigEnc.Conservar_Zona_Picking_Clavaud=True AND pedido >= 1 pallet completo
         → True (estrategia Clavaud activa)
      └─ Sino → False (algoritmo normal)
   ↓
4. clsLnStock_res_Partial.Reserva_Stock(pStockRes, ..., pExcluirUbicacionPicking, BeConfigEnc):
   ├─ clsLnStock.lStock(...) ← query SQL filtra stock candidato (con/sin picking)
   ├─ Ordena en memoria por IdTipoRotacion (FIFO/LIFO/FEFO/UPSR)
   ├─ Itera y va creando clsBeStock_res por IdStock consumido
   └─ Persiste reservas
   ↓
5. WMS responde al ERP con la reserva confirmada
   ↓
6. Operador HH ejecuta picking según las reservas (clsLnTrans_picking_*)
```

---

## Q-* abiertas tras Wave 8

### Resueltas en esta wave
- ✅ **Q-CLAVAUD-MEANING** — Marcelo Clavaud, gerente logística IDEALSA Panamá. Estrategia anti-vaciamiento de picking.
- ✅ **Q-MI3-IDENTIDAD** (no estaba formal) — MI3 = Módulo de Integración con Terceros. Proyecto WCF en BOF (`MI3/`). Eufemismo interno para "nuestra interface".
- ✅ **Q-UMB-CONCEPT** — UMB = Unidad de Medida Básica. Confirmado en código: `vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor`.
- ✅ **Q-PROPIETARIO-AGNOSTICO** (refinada) — el branching es por `empresas.Operador_logistico`. 3PL → config por (IdBodega, IdEmpresa); 1PL/2PL → config por (IdBodega, IdPropietario).

### Nuevas Q-* derivadas
- **Q-UPSR-MEANING** (media) — `tipo_rotacion` tiene 4 valores: FIFO, LIFO, FEFO, **UPSR**. ¿Qué significa UPSR? ¿Cómo se ordena?
- **Q-ROTACION-PRECEDENCIA** (media) — `IdTipoRotacion` vive en `producto`, `bodega_ubicacion` e `i_nav_config_enc`. ¿Cuál tiene precedencia? Hipótesis: producto > ubicación > config bodega.
- **Q-CLAVAUD-THRESHOLD** (alta) — ¿el corte "pedido >= 1 pallet completo" es exacto, o usa algún margen (ej: >= 80% del pallet)? ¿Hay parámetro configurable?
- **Q-MI3-VS-CEALSAMI3** (media) — ¿CEALSA usa ambos o solo CEALSAMI3? ¿Otros clientes tienen `<CLIENTE>MI3/` propio?
- **Q-DSUBICSUG-ALGORITMO** (alta) — `CEALSAMI3/DAL/Transacciones/Movimiento/dsUbicSug.xsd` es el motor de "ubicación sugerida". ¿Cuál es el algoritmo? ¿Solo CEALSA lo usa o se generalizó?
- **Q-RESERVA-MULTIPLE-VARIANTES** (media) — `clsLnStock_res_Partial` tiene 13+ funciones de reserva: `Reserva_Stock`, `Reserva_Stock_NAV_BYB`, `Reserva_Stock_Lista_Result`, `Reserva_Stock_Especifico` (x2 overloads), `Reservar_Stock_By_IdStock` (x3), `Reservar_Stock_By_Stock_Reem`, `Reemplazo_Automatico`, `Reemplazo_Automatico_Conso`, etc. ¿Cuándo se usa cada una?
- **Q-REEMPLAZO-AUTO** (alta) — Las funciones `Reemplazo_*` sugieren que el WMS puede REEMPLAZAR un stock ya reservado por otro. ¿Cuándo dispara esto? ¿Es lo que ocurre cuando llega stock más fresco después de la reserva?

---

## Archivos correlacionados en el brain
- `_index/INDEX.md` — sección Wave 8
- `_index/CONCEPT_MAP.md` — Q-* nuevas y resueltas
- `agent-context/CUESTIONARIO_CAROLINA.md` — bloque 12 con Q-* nuevas
- `agent-context/HOLDING_IDEALSA.md` — contexto MERCOPAN (donde nació Clavaud)
- `code-deep-flow/03-implosion-y-merge-lp.md` — wave anterior (toca el mismo dominio de stock)
- `code-deep-flow/traza-001-license-plate.md` — vida del LP (relacionado con Reserva_Stock_NAV_BYB)

---

## Apéndice: comandos de validación

```bash
# Listar funciones de reserva
git show 'origin/dev_2028_merge:TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb' \
  | grep -nE "^\s+Public.*Function (Reserva|Reservar|Reemplaz)"

# Ver los Case de IdTipoRotacion
git show 'origin/dev_2028_merge:TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb' \
  | sed -n '4560,4600p'

# Listar services MI3
git ls-tree -r origin/dev_2028_merge MI3/ | grep -E "\.svc\.vb$"
```

```sql
-- Bodegas con Clavaud activo
SELECT IdBodega, IdPropietario, conservar_zona_picking_clavaud
FROM IMS4MB_MERCOPAN_PRD.dbo.i_nav_config_enc
WHERE conservar_zona_picking_clavaud = 1;

-- Distribución de IdTipoRotacion por nivel de ubicación
SELECT nivel, IdTipoRotacion, COUNT(*) n_ubic
FROM IMS4MB_MERCOPAN_PRD.dbo.bodega_ubicacion WITH (NOLOCK)
GROUP BY nivel, IdTipoRotacion
ORDER BY nivel, IdTipoRotacion;

-- Productos por tipo de rotación
SELECT IdTipoRotacion, COUNT(*) n_productos
FROM IMS4MB_MERCOPAN_PRD.dbo.producto WITH (NOLOCK)
GROUP BY IdTipoRotacion;
```

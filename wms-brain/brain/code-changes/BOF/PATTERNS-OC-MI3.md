# Patron: Estatus de Orden de Compra MI3

> Origen: handoff inverso `codex-learning-2026-05-20-mi3-di-estatus-endpoint`.
> Endpoint: `GET /api/sync/ingresos/mi3/di-estatus?referencia=<referencia_oc>`.

## Regla de negocio CLAVE

**Resolver la OC MI3 por `trans_oc_enc.Referencia`**, NO por
`IdOrdenCompraEnc` interno.

Razon: el `IdOrdenCompraEnc` es interno de TOMWMS. MI3 (cliente externo)
referencia las OC por el campo `Referencia` que es lo que ambos sistemas
comparten. Exponer el ID interno crea acoplamiento fragil y obliga a MI3 a
conocer la numeracion de TOMWMS.

## Modelo de datos (cadena de joins)

```
trans_oc_enc                                    (cabecera OC)
   IdOrdenCompraEnc PK
   Referencia       ← clave de entrada del endpoint
   IdProveedor, FechaOC, EstadoOC, ...
   │
   │ 1..N
   ▼
trans_oc_det                                    (lineas de OC)
   IdOrdenCompraDet PK
   IdOrdenCompraEnc FK
   IdProducto, Cantidad (solicitada), ...

trans_oc_enc
   │ IdOrdenCompraEnc
   ▼
trans_re_oc                                     (link OC ↔ recepciones)
   IdOrdenCompraEnc FK
   IdRecepcionEnc   FK → trans_re_enc.IdRecepcionEnc
   │
   ▼
trans_re_enc                                    (cabecera recepcion)
   │ + trans_re_det (detalle recepcion, con cantidades efectivas)

trans_re_oc.IdRecepcionEnc
   │
   ▼
tarea_hh                                        (cola de trabajo HH)
   IdTareaHH PK
   IdTransaccion = trans_re_oc.IdRecepcionEnc
   IdTipoTarea   = 1   (recepcion HH; ver §catalogos)
   IdEstado      ∈ {1,2,3,4}   (Nuevo, Pendiente, Anulado, Finalizado)
```

## Query base (esqueleto)

```sql
-- 1) Obtener cabecera OC por Referencia
SELECT IdOrdenCompraEnc, Referencia, EstadoOC, FechaOC, IdProveedor
FROM trans_oc_enc
WHERE Referencia = @referencia;

-- 2) Lineas de OC (cantidad solicitada por producto)
SELECT d.IdOrdenCompraDet, d.IdProducto, p.Codigo, p.Descripcion, d.Cantidad
FROM trans_oc_det d
INNER JOIN producto p ON p.IdProducto = d.IdProducto
WHERE d.IdOrdenCompraEnc = @idOC;

-- 3) Recepciones asociadas (cantidad recibida por producto)
SELECT rd.IdProducto, SUM(rd.Cantidad) AS CantidadRecibida
FROM trans_re_oc ro
INNER JOIN trans_re_det rd ON rd.IdRecepcionEnc = ro.IdRecepcionEnc
WHERE ro.IdOrdenCompraEnc = @idOC
GROUP BY rd.IdProducto;

-- 4) Tareas HH asociadas (estado)
SELECT t.IdTareaHH, t.IdTransaccion AS IdRecepcionEnc,
       t.IdEstado, t.FechaCreacion, t.FechaFinalizacion
FROM tarea_hh t
INNER JOIN trans_re_oc ro ON ro.IdRecepcionEnc = t.IdTransaccion
WHERE ro.IdOrdenCompraEnc = @idOC
  AND t.IdTipoTarea = 1;
```

## DTO de respuesta

```json
{
  "data": [
    {
      "IdProducto": 12345,
      "CodigoProducto": "PROD-001",
      "DescripcionProducto": "Producto Ejemplo",
      "CantidadSolicitada": 100,
      "CantidadRecibida": 80,
      "Pendiente": 20,
      "RecepcionCompleta": false,
      "TareasHH": [
        {
          "IdTareaHH": 9001,
          "IdEstado": 4,
          "EstadoNombre": "Finalizado",
          "Finalizada": true
        },
        {
          "IdTareaHH": 9002,
          "IdEstado": 2,
          "EstadoNombre": "Pendiente",
          "Finalizada": false
        }
      ]
    }
  ],
  "error": null
}
```

Reglas derivadas:

- `Pendiente = CantidadSolicitada - CantidadRecibida` (clamp a 0).
- `RecepcionCompleta = (CantidadRecibida >= CantidadSolicitada)`.
- `Finalizada = (IdEstado == 4)`.

## Casos borde

- **Referencia no existe**: HTTP 404 con `error.code = "NOT_FOUND"`.
- **OC sin recepciones**: `CantidadRecibida = 0`, `TareasHH = []`,
  `Pendiente = CantidadSolicitada`.
- **Recepcion sin tareas HH**: lista vacia, recepcion existio pero no se
  enrutó a HH (caso valido para recepciones manuales).
- **CantidadRecibida > CantidadSolicitada**: se aceptan recepciones con
  exceso. `Pendiente = 0`, `RecepcionCompleta = true`. NO tratar como error.
- **Multiples OC con misma Referencia**: por diseno no deberia pasar, pero
  el codigo debe defenderse — devolver 409 o consolidar las dos.
  Confirmar regla con Erik antes de decidir.

## Implementacion por capas

Sigue el patron de `code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`.
Resumen:
- Controller: parse `referencia`, valida no vacia, llama `service.GetEstatusByReferencia(ref)`, envuelve en Forma A.
- Service: orquesta los 4 queries DAL, compone el array de lineas con reglas derivadas, mapea estado HH a nombre via catalogo.
- DAL: 4 metodos puntuales (`GetOcByReferencia`, `GetLineasOc`, `GetRecepcionesByOc`, `GetTareasHHByRecepciones`).
- EntityCore: `OrdenCompraEnc`, `OrdenCompraDet`, `RecepcionAgregado`, `TareaHH`, `EstatusLineaOcDto`, `TareaHHDto`.

## Nota de persistencia de lotes

- El peso fisico del pallet viaja como `pallet_Weight` en MI3 y debe persistirse de forma simetrica en `i_nav_ped_compra_det_lote` y `trans_oc_det_lote`.
- Regla de cierre aprendida el 2026-07-02: si `trans_oc_det_lote` hace `Insertar` con `peso_licencia`, entonces `Actualizar` y `Cargar` tambien deben mapearlo. Si no, el grid termina mostrando `0` aunque el payload traiga peso real.
- Para OC con multiples bobinas/lotes por la misma linea, la llave funcional debe distinguir cada lote fisico por `IdOrdenCompraDetLote` y no por solo `no_linea + codigo_producto`.

## Referencias

- Capas: `code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`
- Estados HH: `reference/catalogo-tarea-hh-estados.md`
- Forma A: `.local/skills/wms-tomwms/conventions.md` §1

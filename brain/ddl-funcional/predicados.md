---
id: predicados
tipo: ddl-funcional
estado: vigente
titulo: ddl-funcional/predicados.md — lenguaje interno (eufemismos)
tags: [ddl-funcional]
---

# ddl-funcional/predicados.md — lenguaje interno (eufemismos)

> Cada predicado encapsula un proceso operativo del WMS y se expande
> automaticamente a chequeos + flujo predicho de tablas. Es el "lenguaje"
> en el que el brain y Erik se comunican sobre operaciones, sin tener
> que repetir contexto cada vez.

## Convencion

```
<predicado>(<sujeto>) ⇒ chequeos + flujo de tablas
```

## Predicados de RECEPCION

### `predicado_recibe_producto(p, b, c)`
Recepcion de producto `p` en bodega `b` para cliente `c`.

**Chequeos sobre p:**
- `p.control_lote ∈ {0,1}`
- `p.control_vencimiento ∈ {0,1}`
- `p.control_lp = (p.genera_lp_old OR p.IdTipoEtiqueta IS NOT NULL)`
- `p.control_peso ∈ {0,1}`

**Chequeos sobre b:**
- `b.control_talla_color ∈ {0,1}`
- `b.tipo_pantalla_recepcion ∈ {0,1}`

**Flujo predicho:**
```
INSERT trans_re_det (idproducto, cantidad, ...)
INSERT stock_rec
IF b.habilitar_stock_inmediato:    -- pendiente confirmar nombre exacto
  INSERT stock
IF p.control_lote:
  REQUIRE lote → INSERT trans_re_det.lote
IF p.control_vencimiento:
  REQUIRE fecha_vence → INSERT stock_rec.fecha_vence
IF p.control_lp:
  EMIT lic_plate → INSERT licencia_item, INSERT i_nav_transacciones_out.lic_plate
IF p.control_peso:
  REQUIRE peso_recibido → VALIDATE vs (p.peso_referencia ± p.peso_tolerancia)
                       → INSERT trans_re_det.peso_verificado
IF b.control_talla_color:
  REQUIRE IdProductoTallaColor → INSERT con FK
```

**Test case implicito**: cualquier prueba "recibe producto" debe
verificar que las tablas predichas se poblen segun los flags activos
del producto y la bodega.

---

### `predicado_recibe_orden_compra(oc, c)`
Recepcion masiva contra una OC del ERP.

**Flujo:**
```
INSERT trans_oc_enc (header)
INSERT trans_oc_det (lineas)
PARA CADA linea: invocar predicado_recibe_producto
```

---

## Predicados de PICKING / DESPACHO

### `predicado_pickea_pedido(pe)`
Picking de un pedido `pe` en estado `Pendiente`.

**Chequeos sobre pe:**
- `pe.estado = 'Pendiente'`

**Flujo:**
```
SELECT trans_pe_det WHERE idpedidoenc = pe.IdPedidoEnc
PARA CADA linea:
  SELECT stock disponible (FEFO/FIFO segun p.IdTipoRotacion)
  UPDATE stock (descontar)
  INSERT trans_despacho_det con lic_plate origen
UPDATE trans_pe_enc.estado = 'Pickeado'
```

---

### `predicado_verifica_pedido(pe, c)`
Verificacion de etiquetas (modulo HH parametrizable).

**Chequeos sobre c:**
- `c.flag_verificacion_etiqueta = ON`  (capability flag por cliente,
  ubicacion: pendiente confirmar — ver Q-CAPABILITY-FLAG-VERIF)

**Si flag OFF**: salta este predicado, va directo a `predicado_despacha_pedido`.

**Flujo si flag ON:**
```
PARA CADA etiqueta del pedido pickeado:
  HH escanea codigo_barra_etiqueta
  INSERT trans_verificacion_etiqueta (lic_plate, lote, fecha_vence,
                                       peso_verificado, cantidad_verificada,
                                       fecha_verificado, user_agr)
  INSERT log_verificacion_bof
UPDATE trans_pe_enc.estado = 'Verificado'
```

---

### `predicado_despacha_pedido(pe)`
Cierre y materializacion del despacho.

**Flujo:**
```
INSERT trans_despacho_enc (header)
UPDATE trans_pe_enc.estado = 'Despachado'
INSERT i_nav_transacciones_out (tipo='SALIDA', PARA CADA linea)
  con FKs reales (idpedidoenc, iddespachoenc)
  con FKs sentinela 0 (idrecepcionenc=0, idordencompra=0)
EVENTUALLY: SAPBOSync.exe / NavSync.exe / WebAPI lee outbox y empuja al ERP
```

**Estado roto en BECOFARMA desde W12-mar-2026** (H29): este flujo deja
de ejecutarse para los `INSERT trans_despacho_enc` y subsiguientes.

---

## Predicados de STOCK

### `predicado_consulta_stock(p, b)`
Disponibilidad de producto `p` en bodega `b`.

**Flujo (read-only):**
```
SELECT stock WHERE idproducto = p.IdProducto AND idbodega = b.IdBodega
SUM(cantidad) GROUP BY (lote, lic_plate)
IF p.control_vencimiento: ORDER BY fecha_vence ASC (FEFO)
ELSE IF p.IdTipoRotacion = 'FIFO': ORDER BY fec_agr ASC
```

---

## Como usar los predicados

Cuando Erik dice "se recibe producto X en bodega Y", el brain ya tiene
mapeada la expansion completa. No hace falta enumerar tablas. La
respuesta puede ser directamente:
"para `predicado_recibe_producto(X, Y)`, dado que X.control_lote=1,
X.control_lp=1, Y.control_talla_color=1, el flujo esperado es:
trans_re_det + stock_rec + licencia_item + entry en outbox INGRESO con
lic_plate. Si querer test case, validar que esas 4 tablas tengan filas
nuevas y que stock_rec.fecha_vence sea no nula."

Esto es el "lenguaje comun" del que hablo Erik.

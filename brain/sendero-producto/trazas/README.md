# Trazas — Protocolo

> Cada traza documenta el sendero REAL de un producto representativo
> del cliente. Se elige UN producto con muchos movimientos historicos
> y se siguen las queries paso a paso desde el ingreso hasta la salida.

## Productos representativos elegidos (29-abr-2026)

| # | Cliente | IdProducto | Codigo | Nombre | Flags | Pres | Movs |
|---|---|---|---|---|---|---|---|
| 001 | BECOFARMA | 949 | Q4100304 | AMBIARE 2MG-0.25MG CAJA 10 CAP | lote+vence+lp | 0 | 1457 |
| 002 | K7 | 79 | WMS167 | MELOCOTON MIGUELS MITADES 12/820GR | lote+vence+lp | 1 | 3657 |
| 003 | MAMPA | 31345 | AG00030021 | Dama bajo tenis urbano | lp | 0 | 1120 |
| 004 | BYB | 102 | 00170440 | SALSA PICAMAS VERDE 48/100g | lote+vence | 1 | 32649 |
| 005 | CEALSA | 4828 | NEN025 | AMOXICILINA 500MG | lote+lp | 0 | 1798 |

## Protocolo de traza

Cada traza ejecuta este conjunto de queries:

### Q1: Producto (flags y catalogo)
```sql
SELECT IdProducto, codigo, nombre, control_lote, control_vencimiento,
       control_peso, genera_lp_old, IdTipoEtiqueta, IdUnidadMedidaBasica, activo
FROM producto WHERE IdProducto = ?
```

### Q2: producto_bodega (en cuantas bodegas esta)
```sql
SELECT pb.IdProductoBodega, pb.IdBodega, b.codigo, b.nombre
FROM producto_bodega pb JOIN bodega b ON pb.IdBodega = b.IdBodega
WHERE pb.IdProducto = ? ORDER BY pb.IdBodega
```

### Q3: producto_presentacion (presentaciones definidas)
```sql
SELECT * FROM producto_presentacion WHERE IdProducto = ?
```

### Q4: stock_rec (stock activo actual)
```sql
SELECT TOP 5 sr.*, pe.nombre AS estado, bu.descripcion AS ubic
FROM stock_rec sr
LEFT JOIN producto_estado pe ON sr.IdProductoEstado = pe.IdEstado
LEFT JOIN bodega_ubicacion bu ON sr.IdUbicacion = bu.IdUbicacion
WHERE sr.IdProductoBodega IN (<lista pb>) AND sr.activo = 1
ORDER BY sr.fecha_ingreso DESC
```

### Q5: trans_movimientos (ultimos N movimientos)
```sql
SELECT TOP 8 m.*, stt.Nombre AS tipo,
       peo.nombre AS estado_origen, ped.nombre AS estado_destino,
       bo.descripcion AS ubic_origen, bd.descripcion AS ubic_destino
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea stt ON m.IdTipoTarea = stt.IdTipoTarea
LEFT JOIN producto_estado peo ON m.IdEstadoOrigen = peo.IdEstado
LEFT JOIN producto_estado ped ON m.IdEstadoDestino = ped.IdEstado
LEFT JOIN bodega_ubicacion bo ON m.IdUbicacionOrigen = bo.IdUbicacion
LEFT JOIN bodega_ubicacion bd ON m.IdUbicacionDestino = bd.IdUbicacion
WHERE m.IdProductoBodega IN (<lista pb>)
ORDER BY m.fecha DESC
```

### Q6: distribucion historica de tipos de tarea
```sql
SELECT m.IdTipoTarea, stt.Nombre, COUNT(*) AS n,
       MIN(m.fecha) AS primero, MAX(m.fecha) AS ultimo
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea stt ON m.IdTipoTarea = stt.IdTipoTarea
WHERE m.IdProductoBodega IN (<lista pb>)
GROUP BY m.IdTipoTarea, stt.Nombre
ORDER BY n DESC
```

## Como leer las trazas

Cada traza tiene 6 secciones que mapean a las queries arriba +
**Interpretacion** (que sendero real se observa) + **Mapeo al graph-EQL**.

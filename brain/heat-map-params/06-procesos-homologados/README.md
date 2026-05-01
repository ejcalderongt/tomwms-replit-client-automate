---
id: README
tipo: heat-map-params
estado: vigente
titulo: Capa 6 — PROCESOS HOMOLOGADOS
tags: [heat-map-params]
---

# Capa 6 — PROCESOS HOMOLOGADOS

> El producto final del heat-map-params: para cada PROCESO operativo
> del WMS, definimos el ESTANDAR (lo que hacen casi todos los clientes
> a nivel datos identico) y las VARIANTES (deltas por cliente).

## Procesos a homologar (lista preliminar)

1. **Recepcion** (de compra, transferencia, devolucion, produccion)
2. **Put-away**
3. **Picking** (consolidado / detalle / multi-source)
4. **Verificacion** (consolidada / por linea / con foto)
5. **Packing** (tarima / caja / sin packing)
6. **Despacho**
7. **Transferencias** (WMS ↔ WMS / WMS ↔ no-WMS)
8. **Devolucion** (de cliente / a proveedor)
9. **Reabasto** (automatico / manual)
10. **Explosion / Implosion**
11. **Calidad** (cuarentena → liberacion)
12. **Inventario** (ciclico / general / ajustes)

## Formato de cada proceso

Cada proceso tiene su propio archivo `<proceso>.md` con secciones:

```
# Proceso: <nombre>

## Definicion ESTANDAR
- Flags requeridos en bodega/empresa
- Tipo(s) de documento que disparan el proceso
- sis_tipo_tarea generados
- Tablas afectadas
- Sub-grafo del sendero

## Clientes que implementan el ESTANDAR puro
- Cliente A: idem datos
- Cliente B: idem datos

## VARIANTES
- Variante CLIENTE-X: delta = "añade tabla Z" / "no usa flag Y"
- Variante CLIENTE-Y: delta = "...

## Heat-map
- BECOFARMA: estandar / variante-1 / no aplica
- K7: ...
- MAMPA: ...
- BYB: ...
- CEALSA: ...
```

## Procesos con archivo dedicado (PENDIENTES de armar)

- [ ] `recepcion.md`
- [ ] `put-away.md`
- [ ] `picking.md`
- [ ] `verificacion.md`
- [ ] `packing.md`
- [ ] `despacho.md`
- [ ] `transferencias.md`
- [ ] `devolucion.md`
- [ ] `reabasto.md`
- [ ] `explosion-implosion.md`
- [ ] `calidad.md`
- [ ] `inventario.md`
- [ ] `salida-prefactura.md` (variante exclusiva CEALSA)

## Insights iniciales (de las trazas + heat-map)

### Picking
- ESTANDAR: `8:PIK` con multi-source opcional, sin presentacion
- VARIANTE K7: multi-source intensivo (mov#99194 con 1A1+9B2 simultaneo) +
  reabasto previo automatico (REEMP_BE_PICK 70% del PIK)
- VARIANTE BYB: incluye `3:CEST` (cesta) como pre-paso

### Packing
- ESTANDAR: `bodega.empaque_tarima=True` + `12:PACK`
- USAN: BYB (14 movs PACK historicos), K7 (probable)
- NO USAN: BECOFARMA, MAMPA, CEALSA
- VARIANTE BYB: `3:CEST` antes de PACK

### Verificacion
- ESTANDAR: `11:VERI` posterior a `8:PIK`, ratio ~1.0
- USAN: BECOFARMA, K7, MAMPA
- NO USA: CEALSA (no hay PIK/VERI)
- ANOMALIA BYB: VERI/PIK = 18.4 (sobredimensionado, hipotesis: VERI usado tambien para inventario)

### Reabasto
- ESTANDAR: `25:REEMP_BE_PICK` cuando ubic_picking < umbral
- USA INTENSIVO: K7 (606 movs, 70% del PIK)
- USA PUNTUAL: BYB (manual `23:REABMAN` + automatico `25/26/27:REEMP_*_PICK`)
- NO USAN: BECOFARMA, MAMPA, CEALSA

### Transferencias entre bodegas
- ESTANDAR: `4:TRAS` con `Generar_pedido_ingreso_bodega_destino=True` (caso WMS→WMS)
- USA INTENSIVO: MAMPA (90 movs, 8% del trafico — multi-tienda)
- USA PUNTUAL: K7 (TRAS_WMS), BYB (Transferencia_Interna_WMS)
- NO USAN: BECOFARMA (1 sola bodega), CEALSA (sintetico)

### Calidad / Cuarentena
- ESTANDAR: `producto_estado_ubic` mapea Estado=Cuarentena → ubicacion CUARENTENA
- LIBERACION: `2:UBIC` x2 (CUARENTENA → PROD.LIB. → estanteria)
- USA: BECOFARMA (confirmado en traza-001)
- ¿USAN?: K7 (probable para algunos productos), MAMPA (probable para devoluciones)

### Salida via prefactura
- VARIANTE EXCLUSIVA: CEALSA
- Tablas: `trans_prefactura_enc/det/mov` + vistas dedicadas
- Sin movimientos de stock asociados

## Capability matrix (preliminar)

| Capability | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| recepcion-compra | SI | SI | SI | SI | SI (sintetico) |
| recepcion-devolucion | SI | SI | SI | SI | SI |
| recepcion-transferencia | NO | SI | SI | SI | SI |
| recepcion-produccion | ? | ? | ? | ? | NO |
| put-away | SI | SI | SI | SI | NO (omitido) |
| picking-detalle | SI | SI | SI | SI | NO |
| picking-consolidado | NO | NO | NO | SI | NO |
| picking-multi-source | NO | SI | NO | NO | NO |
| picking-por-voz | ? | ? | ? | ? | NO |
| verificacion-por-linea | SI | SI | SI | SI | NO |
| verificacion-consolidada | ? | ? | ? | SI (probable) | NO |
| packing-tarima | NO | NO | NO | SI | NO |
| despacho | SI | SI | SI | SI | NO (sintetico) |
| transferencia-wms-wms | NO | SI | SI | SI | SI |
| transferencia-wms-no-wms | ? | ? | ? | ? | ? |
| devolucion-cliente | SI | SI | SI | SI | SI |
| devolucion-proveedor | SI | SI | SI | NO (probable) | NO |
| reabasto-automatico | NO | SI | NO | SI | NO |
| reabasto-manual | NO | NO | NO | SI | NO |
| explosion | NO | NO | NO | SI | NO |
| implosion | NO | NO | NO | SI | NO |
| cuarentena-mapeo-ubicacion | SI | ? | ? | ? | NO |
| salida-via-prefactura | NO | NO | NO | NO | SI |
| picking-talla-color | NO | NO | SI | NO | NO |

## Pendientes

- Armar el archivo dedicado para cada proceso.
- Validar la capability matrix con cross-cliente exhaustivo.
- Documentar como cada capability se traduce a endpoints de la WebAPI .NET 10.

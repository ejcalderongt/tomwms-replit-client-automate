---
id: CP-014
tipo: cp-open
cliente: killios
producto: WMS62
estado: open
severidad: alta
materializa_bug: BUG-001
trazas_relacionadas: [traza-001-stock-fantasma]
modulo: trans_movimientos+stock (recepcion HH / verificacion / picking)
ramas_afectadas: [dev_2023_estable, dev_2028_merge]
descubierto: 2026-05-09
reportado_por: Zulma Martinez (Killios — garesa.co)
audiencia_interna: Carolina Fuentes (PrograX24)
tags: [cliente/killios, producto/wms62, bug/critico, bug/stock-fantasma, sap-discrepancia]
---

# CP-014 — Killios WMS62 (Maiz dulce Miguels Galon 6/2900g) — Stock fantasma 10 cajas vs SAP

> Caso de cliente ABIERTO. Materializa **BUG-001** en una nueva variante.
> Snapshot BD: `TOMWMS_KILLIOS_PRD_2026` restaurada `2026-05-09 10:23:25Z`.

---

## TL;DR

Zulma Martinez (operativo Killios, garesa.co) reporta que el reporte de
existencias del WMS muestra **10 cajas mas** que el kardex SAP del
producto **WMS62 — MAIZ DULCE MIGUELS GALON 6/2900g** (`IdProducto=271`).

**Match exacto en BD productiva**:

| Vista | UM | Cajas |
|---|---:|---:|
| Stock vivo BD bodega 1 (KILIO-GARESA, IdPropietarioBodega=1) | **2.741** | **456,83** |
| Kardex SAP entregado por el cliente | **2.681** | **446,83** |
| **Diferencia = STOCK FANTASMA** | **+60** | **+10** |

Coincide al 100% con el reporte. Las 10 cajas estan repartidas en
**27 lineas de stock** distribuidas en 17 lic_plates unicos (ver
`traza-001-stock-fantasma.md`).

**Causa raiz confirmada (variante BUG-001)**: ninguno de los 17
lic_plates vivos tiene movimientos asociados en `trans_movimientos`
para WMS62 bodega 1. El WMS escribio en `stock` sin escribir la
contraparte en `trans_movimientos`, o uso un pivote distinto. Por eso
el kardex SAP (que se reconcilia desde `trans_movimientos`
`Contabilizar=true`) no ve este stock pero el reporte de existencias
WMS si lo ve.

**Estado del caso al 2026-05-09**:
- Diagnostico forense completo (esta carpeta).
- Listado para ajuste manual con IdStock + lic_plate + ubicacion + cantidad
  generado en `traza-001-stock-fantasma.md` para que Erik pueda eliminar
  directo en la BD.
- Informe para Zulma (cliente) en `INFORME-CLIENTE-KILLIOS.md`.
- Informe para Carolina (analisis codigo, donde buscar el bug) en
  `INFORME-CAROLINA.md`.
- Plan de fix: hereda CP-013/PLAYBOOK-FIX.md (mismo BUG-001) + ajustes
  WMS62-especificos en `PLAYBOOK-FIX.md` local.

**Bloqueante**: requiere validacion fisica con Carolina antes de
ejecutar el ajuste. NO ejecutar DELETE/UPDATE sin aprobacion explicita
del cliente y verificacion fisica de las cajas.

---

## Archivos en esta carpeta

| Archivo | Que es |
|---|---|
| `INDEX.md` | Este archivo. Indice maestro del caso. |
| `traza-001-stock-fantasma.md` | Listado completo IdStock + lic_plate + ubicacion + cantidad de las 27 lineas de stock vivo bodega 1, agrupadas por categoria sano/danado/reservado, con candidatos sugeridos para ajuste. |
| `INFORME-CLIENTE-KILLIOS.md` | Borrador de respuesta para Zulma (garesa.co) explicando hallazgo, plan de ajuste y recomendaciones operativas. |
| `INFORME-CAROLINA.md` | Analisis para Carolina Fuentes: donde buscar el bug en el codigo VB.NET (BOF) y Java (HH), patron BUG-001 ola 3, hipotesis A (UPDATE-cae-INSERT) y B (capa comun), endpoints sospechosos. |
| `PLAYBOOK-FIX.md` | Plan de remediacion local. Hereda CP-013/PLAYBOOK-FIX.md §A-§H. |

---

## Cross-links

- Bug raiz: `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/`
- Caso anchor (ola 1): `customer-open-cases/CP-013-killios-wms164/`
- Snapshot productivo: `data-deep-dive/killios_2026/snapshot-2026-05-05.md`
  (10.565 lineas con `dañado_picking=1` y 0 AJCANTN compensatorios)
- Catalogo `sis_tipo_tarea` oficial: ver §3 de `INFORME-CAROLINA.md`

---

## Definition of Done para cerrar este CP

1. Carolina valida fisicamente las 27 lineas (en particular las 8
   marcadas REEMPACAR/MAL ESTADO que totalizan 364 cajas).
2. Erik ejecuta ajuste manual segun `traza-001-stock-fantasma.md` para
   eliminar las 10 cajas fantasma identificadas.
3. Stock WMS62 bodega 1 = 446,83 cajas = kardex SAP (post-ajuste).
4. Cambio aplicado tambien al cliente: validar con Zulma que el
   reporte coincide con SAP.
5. Si fix de BUG-001 ya esta aplicado (ola 1 o anterior), confirmar
   que no se generan nuevas lineas fantasma post-ajuste durante 7 dias.

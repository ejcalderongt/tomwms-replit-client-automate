# Registro de parches aplicados (TOMWMS)

> Bitácora manual de aplicación de bundles/parches en `dev_2028_merge`.
> 
> Regla: cada vez que se aplique un bundle, agregar una línea con fecha, bundle, alcance, método y estado.

| Fecha (CST) | Bundle | Alcance | Método | Estado | Notas |
|---|---|---|---|---|---|
| 2026-04-24 23:09 | v10_consolidado_2026-04-25 | v8 + v10.C1 + v10.C3 | mixto (apply + manual) | ✅ aplicado | Reemplaza v9 |
| 2026-04-24 23:19 | v11_consolidado_2026-04-25 | v11.E1 | manual | ✅ aplicado | Eliminación handler duplicado vacío |
| 2026-04-24 23:54 | v12_consolidado_2026-04-25 | v12.F1 | manual | ✅ aplicado | Hash anti-duplicado + ColProveedor nominal |
| 2026-04-25 00:01 | v13_consolidado_2026-04-25 | v13.G1 + v13.G2/G3 | manual | ✅ aplicado | Sync lista/grid + delete por stocklink |
| 2026-04-25 00:07 | v14_consolidado_2026-04-25 | v14.H1 + v14.H2 | manual | ✅ aplicado | Eliminar ajuste sin detalle + existencia live |
| 2026-04-25 00:15 | v15_consolidado_2026-04-25 | v15.I1 + v15.I2 | manual | ✅ aplicado | Importer borrador mapeo explícito + recuperación código/nombre producto |
| 2026-04-25 00:35 | v16_consolidado_2026-04-25 | v16.J1 + v16.J2 | manual | ✅ aplicado | Rewrite sync con fallback posicional + espejo borrador con MapearDetalleABorrador |
| 2026-04-25 00:43 | v17_bundle_2026-04-25 | I1 + I2 + J1/J2 | verificación/no-op | ✅ ya incorporado | Bundle en formato `.patch`; contenido ya presente por v15+v16, no cambios adicionales |
| 2026-04-25 01:29 | v18_bundle_2026-04-25 | v18.D2 | manual | ✅ aplicado | Cargar_Detalle usa existenciaLive local para CantidadP sin pisar Cantidad_original |

## Cómo actualizar esta bitácora

Agregar una fila nueva al final con:
- **Fecha (CST)**
- **Bundle**
- **Alcance** (ids de fix)
- **Método** (`apply`, `manual`, `mixto`)
- **Estado** (✅/⚠️)
- **Notas**

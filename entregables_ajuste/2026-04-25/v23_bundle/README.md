# v23 bundle (2026-04-25) - Eliminar_Ajuste_Si_Sin_Detalle robustecido

## Resumen

Tres correcciones al sub `Eliminar_Ajuste_Si_Sin_Detalle`:

- **G1**: bloquea la eliminacion si el ajuste no esta en estado borrador.
  Un ajuste ya aplicado/finalizado no puede eliminarse desde esta UI;
  debe pasar por proceso de reverso.
- **G2**: el conteo de detalles ahora se hace desde `lBeTransAjusteDetBorrador.Count`
  (lista en memoria sincronizada por `mnuDel_Click`), no iterando `dgrid.Rows`.
  El conteo viejo era erroneo: despues de eliminar filas, DevExpress puede
  dejar filas `IsNewRow` / fantasma que inflan el contador (caso reportado:
  `filasGrid = 2` con la lista en memoria vacia, bloqueando la eliminacion).
- **G3**: se mantiene la defensa contra `trans_ajuste_det_borrador` consultando
  la BD por si la lista en memoria estuviera desincronizada por algun camino
  atipico. Se elimina la consulta a `trans_ajuste_det` porque G1 ya bloqueo
  todo lo no-borrador.

## Por que esos cambios

1. **`mnuDel_Click` mantiene las listas sincronizadas**: ambas ramas
   (borrador y no-borrador) hacen `RemoveAt` sobre la lista correspondiente
   y sobre `dgrid.Rows` en paralelo. Eso convierte a las listas en la fuente
   de verdad confiable; el grid puede tener artefactos visuales (filas
   `IsNewRow`) que no representan datos reales.
2. **El borrador reserva el `IdStock`**: cuando se elimina el ajuste, lo
   correcto es ejecutar `RollBackStockRes` sobre el encabezado del borrador.
   Si el ajuste ya fue aplicado, el stock ya cambio y un `RollBack` aqui
   romperia la trazabilidad: por eso G1 lo bloquea de plano.

## Compatibilidad

- v20, v21, v22: sin conflicto (zonas distintas del archivo).
- Reemplaza la logica originalmente introducida por `#FIX_v14_ELIMINAR_AJUSTE_2026-04-25 (H1)`.
  El comentario de cabecera H1 (linea 2886) se mantiene; los marcadores G1/G2/G3
  nuevos quedan dentro del cuerpo del sub.

## Aplicar

```bash
git apply v23_bundle/patches/0001-frm-eliminar-ajuste-rules-borrador-y-lista-memoria.patch
grep -c "#FIX_v23_ELIMINAR_AJUSTE_RULES_2026-04-25" \
    TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb
# debe imprimir: 3
```

## Verificacion

| Caso | Resultado esperado |
|---|---|
| Ajuste finalizado (chkBorrador=False) | "ya esta aplicado/finalizado y no puede eliminarse" |
| Borrador con detalles en memoria | "tiene N detalle(s) en memoria. Elimine los detalles antes" |
| Borrador con grid lleno de filas IsNewRow pero lista en memoria vacia | Procede a la confirmacion (caso que antes bloqueaba) |
| Borrador con lista vacia pero filas en BD trans_ajuste_det_borrador | "tiene N detalle(s) en BD trans_ajuste_det_borrador" |
| Borrador completamente vacio (memoria + BD) | Confirma -> RollBackStockRes -> Close |

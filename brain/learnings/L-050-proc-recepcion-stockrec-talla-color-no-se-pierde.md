# L-050 - PROC: Recepcion de interface debe preservar talla/color al construir stock_rec

> Etiqueta: `L-050_PROC_RECEPCION_STOCKREC_TALLA-COLOR`
> Fecha: 2026-06-12
> Origen: trazado fino del flujo de recepcion de interfaz en `TOMIMSV4`

## Hallazgo

En el flujo de recepcion de la interface, el dato de variante puede perderse al construir `stock_rec` si la linea no reconstruye `Talla` y `Color` antes de persistir o clonar el objeto.

La traza util detectada fue:

- `C:\Users\carol\source\repos\TOMWMS_BOF\TOMIMSV4\TOMIMSV4\Transacciones\Recepcion\frmRecepcion.vb`
- `C:\Users\carol\source\repos\TOMWMS_BOF\TOMIMSV4\TOMIMSV4\Transacciones\Recepcion_BOF\frmRecepcionBOF.vb`

## Evidencia

1. En la ruta de alta manual de stock, la recepcion usa `BeStock_rec.Talla` y `BeStock_rec.Color`.
2. En una rama de guardado/finalizacion, el flujo solo copiaba cantidad e id de recepcion y luego clonaba `BeStockRec`.
3. Se detectaron typos que podian vaciar valores:
   - validacion de `Color` usando la columna `Talla`
   - retorno `False` para `Talla`/`SKU` cuando la celda venia nula
4. En la ruta de guardado de detalle, `IdProductoTallaColor` debe reconstruirse desde `Talla/Color` cuando `Control_Talla_Color` esta activo.

## Conclusion

El vacio de talla/color no parecia venir de `clsSyncTransacWMS` sino del tramo de recepcion que arma `stock_rec` en la UI/BOF. Si `IdProductoTallaColor` queda en cero, la variante no puede resolverse despues y la linea termina incompleta.

## Fix aplicado

- Corregir la lectura de `Color` para que valide la columna correcta.
- Corregir retornos nulos de `Talla`, `Color` y `SKU` a cadena vacia.
- Preservar `Talla` y `Color` en `BeStockRec` antes del `Clone()`.
- Reconstruir `IdProductoTallaColor` desde `Talla/Color` en la ruta de guardado de recepcion cuando la bodega controla variantes.

## Implicaciones

- Si la recepcion entra por una rama sin este mapeo, `stock_rec` queda sin variante aunque el documento fuente la tenga.
- El problema no debe modelarse como fallo de SAP Service Layer; la perdida ocurre mas abajo, en el armado local de la recepcion.

## Vinculos

- `brain/wms-specific-process-flow/`
- `brain/learnings/L-049-proc-carolina-modo-aprendizaje-brain-federado.md`

## Confianza

Alta, por trazado directo de codigo y comparacion de ramas de recepcion.

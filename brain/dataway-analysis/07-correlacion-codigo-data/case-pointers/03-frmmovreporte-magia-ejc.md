# CP-003 — frmMovimiento_Reporte comment "Magia por EJC para corregir cagada"

> Comment-pointer en el mismo bloque que `CP-002`. Documenta el intento de fix por parte de "EJC" (Erik José Calderón) al bug de "JP". El bloque de código que viene después es la "magia" — pero la línea de fix está **comentada**, así que en realidad la magia no se ejecuta.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-003-frmmovreporte-magia-ejc |
| Tipo | comment-pointer + código fix comentado |
| Estado | documentado — fix nunca activado en producción |
| Severidad estimada | media (no introduce bug, evidencia decisión abandonada) |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb` |
| Línea | 125 (comment), 128-130 (código del fix, comentado) |
| Persona referenciada | "EJC" (Erik José Calderón) |
| Fecha estimada | desconocida (posterior a CP-002, probablemente respuesta directa) |

## Cita textual

`frmMovimiento_Reporte.vb:122-131`:

```vb
If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
    '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
    Debug.Print("Espera")
    'Magia por EJC para corregir cagada.
    If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
        'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
        Debug.Print(BeStockEnFecha.Codigo)
    End If
End If
```

## Lectura

1. **Línea 125 — anuncio del fix**: `'Magia por EJC para corregir cagada.` — Erik se identifica como autor del intento de corrección sobre el bug introducido por "JP".

2. **Línea 127 — condición del fix**: `If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date` — la idea es: si el registro ya acumulado en `RepMovEnUnaFecha` tiene una `Fecha_Vence` posterior a la del movimiento actual, asumir que la posterior es la real (porque el bug de JP la sobreescribió con `Now()`, que tiende a ser posterior a la real).

3. **Línea 128 — el fix está COMENTADO**: `'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date` — el `'` al inicio comenta la línea. **El fix no se ejecuta**.

4. **Línea 129 — solo log**: `Debug.Print(BeStockEnFecha.Codigo)` — lo único que queda activo es escribir el código del producto a la consola de debug. En producción no hace nada visible.

## Por qué se comentó el fix

Hipótesis (sin confirmar):

- (a) **Riesgo regulatorio**: en un reporte fiscal, modificar `Fecha_Vence` aunque sea en memoria puede inducir números fiscales incorrectos. Erik decidió no tomar la responsabilidad.
- (b) **El criterio de selección no es robusto**: "fecha más posterior gana" podría ser falso para casos legítimos donde un mismo lote tiene movimientos cruzados con dos fechas reales distintas (rare pero posible).
- (c) **Se intentó probar y dio resultados raros**: Erik dejó comentado para no perder la lógica pero sin activarla, esperando datos reales para validar.
- (d) **Se decidió atacar el bug raíz** (la operación de cambio de ubicación) en lugar de parchear en el reporte. Pero el parche del raíz nunca se completó, quedando esto como evidencia.

## Acción forense

### Confirmar autoría con Erik

CP-003 es **el primer case-pointer firmado por Erik mismo**. Vale la pena preguntarle:

1. ¿Recordás haber escrito este bloque?
2. ¿Por qué comentaste la línea de fix?
3. ¿El bug de JP se resolvió en otra parte del sistema, o sigue vigente?
4. ¿Hay otros parches "magia EJC" en otros archivos del repo? (Probable — buscar.)

### Buscar otras "magias EJC"

```bash
rg -n -i "magia.*EJC|EJC.*magia|Magia por" /tmp/repos/TOMWMS_BOF/ --type vb
```

Si aparecen más, se agregan como case-pointers `CP-008+`.

## Bitácora de debug

Ver `brain/debuged-cases/CP-003.md`.

## Pendientes

- [ ] Confirmar autoría con Erik (alta probabilidad).
- [ ] Razón del comentado.
- [ ] Buscar otras "magias EJC" en el repo.
- [ ] Decidir: si el bug de JP sigue vigente, ¿activamos el fix o atacamos raíz?

## Cross-refs

- `02-frmmovreporte-fecha-vence-now-jp.md` — CP-002, mismo bloque (el bug que esta "magia" intenta corregir)
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — divergencia 1
- `brain/debuged-cases/CP-003.md` — bitácora

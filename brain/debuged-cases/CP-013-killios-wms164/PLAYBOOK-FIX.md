# PLAYBOOK-FIX CP-013 — Remediación bug dañado_picking sin descuento

## Severidad: CRÍTICA (sistémica, multi-cliente potencial)

## A. Patch funcional inmediato (BOF .NET — 1 semana)

### Cambio en módulo de gestión de pickings backoffice

Hoy: el backoffice permite marcar `dañado_picking = 1` sobre una línea `trans_picking_ubic` y guarda el cambio sin más efectos.

Cambio requerido:

1. **Forzar selección de destino del dañado** mediante combo obligatorio:
   - "Mover a área MERMA" (default) → genera `trans_movimiento` IdTipoTarea=8 (PIK) origen=ubic_picking destino=ubic_merma con `cantidad = cantidad_solicitada`.
   - "Devolver a proveedor" → genera salida con motivo devolución.
   - "Descarte / pérdida" → genera `trans_movimiento` IdTipoTarea=17 (AJCANTN) con cantidad negativa.
2. **Pedir motivo + observación obligatorios** y guardarlos en una tabla nueva `trans_picking_danado_log` (IdPickingUbic, motivo, destino, user_agr, fec_agr, observacion).
3. **Generar el descuento de `producto_bodega_stock`** según el destino seleccionado.
4. **Bloquear el guardado si no hay ubicación MERMA configurada** para la bodega.

### Validación QA

Caso golden:
- Crear lote ficticio LOTE-TEST con 100 UM en ubicación PICK01.
- Generar picking de 30 UM. Marcar dañado desde backoffice → debe pedir destino.
  - Si destino=MERMA → stock PICK01 debe quedar 70, stock MERMA debe quedar 30.
  - Si destino=AJCANTN → stock PICK01 debe quedar 70, sin stock nuevo.
- Verificar `trans_movimientos` registra el movimiento con tipo correcto.

## B. Reconciliación retroactiva de las >320,000 UM fantasma (1-2 meses)

**No se puede aplicar AJCANTN a ciegas.** Pasos:

1. **Inventario físico bodega completa** (Killios B1, ~10,800 líneas afectadas, ~500 productos).
   - Usar listado W19-G como prioridad: top 30 productos = ~150 mil UM (47% del problema).
   - Cuenta cíclica ABC, 30 días.
2. **Recalcular delta esperado** por producto:
   - delta_esperado = saldo_BD − físico_real
   - delta_dañados = SUM(cantidad_solicitada WHERE dañado_picking=1 AND activo=1)
3. **Aplicar AJCANTN** por producto:
   - Si físico_real ≈ saldo_BD − delta_dañados → todos los dañados son reales: aplicar AJCANTN -delta_dañados.
   - Si físico_real difiere → investigar mermas adicionales no reportadas.
4. **Marcar pickings reconciliados** con un nuevo flag `reconciliado_aj=1` para no contarlos otra vez.

## C. Inventarios formales periódicos (proceso, 1 mes)

Hoy: 6 movimientos INVE en historia, todos del 30-nov-2025. Cero inventarios desde entonces.

Implementar:
1. **Política de conteo cíclico ABC**:
   - A (top 20% productos = 80% facturación): conteo mensual.
   - B (siguiente 30%): conteo trimestral.
   - C (resto): conteo semestral.
2. **Reporte de discrepancia obligatorio** cada conteo: si delta > 5%, escalar a supervisión.
3. **Inventario general anual** completo.

## D. Inventario WMS164 específico (esta semana)

Para cerrar el caso reportado por Erik:

1. Contar físico de TODOS los lotes WMS164 (no solo BG2512). Lotes activos según W18-08:
   - BG2512 (lote alerta)
   - BM2601, BM2511, bm2508, bw2511, BM2510 (otros lotes con dañados o presencia activa)
2. Confirmar **factor caja real** del producto (UM por presentación). Probable 6 o 12, validar con `producto_presentacion`.
3. Aplicar AJCANTN por lote según diferencia físico vs BD.
4. Validar que el stock total queda consistente.

## E. Hardening del módulo BOF (mediano plazo, 3 meses)

1. **Audit log inmutable** para todas las acciones de backoffice sobre pickings (no solo dañados).
2. **Reverso supervisado**: permitir anular un picking dañado mal marcado, con doble autorización.
3. **Dashboard de daños**: panel con tendencia mensual de daños por producto/operador para detectar abusos.
4. **Validación cruzada**: cada vez que se marque dañado_picking=1, alertar si el producto+lote tiene >5% de daños históricos.

## F. Riesgo si NO se ejecuta

- La sobre-estimación seguirá creciendo (~32 mil UM/mes al ritmo actual).
- En 1 año más, el inventario contable será irreal en >40% para varios SKUs.
- Pérdidas por venta no cumplida + ajustes contables masivos.
- Posibles desvíos no detectados (mermas reales mezcladas con falsos dañados).


# L-037 — CLIENT: stock_jornada en CEALSA alimenta SAT, Aseguradora, facturacion, portal y reporte "Stock en linea"

> Etiqueta: `L-037_CLIENT_STOCK-JORNADA-CEALSA-CONSUMERS_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-STOCK-JORNADA-CONSUMER

## Hallazgo

En CEALSA, la tabla `stock_jornada` **NO es solo auditoria interna**. Es la fuente de cinco consumers de negocio:

| Consumer | Naturaleza | Criticidad |
|---|---|---|
| Reportes SAT | regulatorio (Superintendencia Administracion Tributaria, GT) | ALTA |
| Reportes Aseguradora | regulatorio/contractual | ALTA |
| Modulo facturacion | financiero | ALTA |
| Portal CEALSA | propietarios 3PL ven su stock historico | MEDIA-ALTA |
| Reporte **"Stock en linea"** | operativo, "super importante" segun Carolina | ALTA |

## Cita literal Carolina (Wave 11)

> "El stock jornada se utiliza bastante en CEALSA para reportes
> especificos que requieren para la SAT y para la Aseguradora.
> Tambien se utiliza para el modulo de facturacion.
> El portal de CEALSA tambien lo utiliza.
> Hay un reporte que se llama Stock en linea que es super importante
> y utiliza esta tabla."

## Implicaciones operativas

1. **Si stock_jornada falla un dia en CEALSA**, hay impacto regulatorio (SAT) y contractual (aseguradora). No es opcional.
2. **El "Stock en linea" es candidate-1 a investigar** en proximo deep-flow. Carolina lo califica de "super importante" — merece su propio mapping en `code-deep-flow/`.
3. **Portal CEALSA depende de stock_jornada** → confirma que el portal (Bloque 3, Q16-Q24) necesita acceso al historico, no solo al stock vivo. Esto es input de diseño para cuando Erik/Efren respondan Bloque 3.
4. **Modulo facturacion** confirma que CEALSA tiene logica de prefactura (Q34 tambien) que se cobra basado en stock historico, no instantaneo. Tipico de 3PL: cobrar almacenaje por dia x m³ x propietario.

## Q-* abiertas

- Q-STOCK-EN-LINEA-DEEP-FLOW (nueva, prioridad alta): mapear el reporte "Stock en linea" — query, store proc, parametros, quien lo consume.
- Q-PORTAL-CEALSA-USA-STOCK-JORNADA: confirmar que el portal lee `stock_jornada` y no `stock` directo.
- Q-FACTURACION-CEALSA-FORMULA: cual es la formula de la prefactura que usa stock_jornada (ya en Q34 → Efren).

## Vinculos

- L-036: como se dispara stock_jornada (parametros).
- Bloque 3 cuestionario: portal CEALSA — pendiente Erik/Efren.
- Q34: prefactura CEALSA — pendiente Efren.

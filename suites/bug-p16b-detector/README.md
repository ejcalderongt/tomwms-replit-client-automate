# Suite: bug-p16b-detector

Detecta el bug **P-16b**: pedidos cuyo encabezado vuelve de
`DESPACHADO` a `PENDIENTE` despues del despacho. Reportado en
`brain/wms-specific-process-flow/bug-report-p16b.md`.

## Como se ejecuta

```powershell
Invoke-WmsBrainAnalysis -Suite bug-p16b-detector -Profile K7-PRD
Invoke-WmsBrainAnalysis -Suite bug-p16b-detector -Profile BB-PRD -OutputJson .\bb-p16b.json
```

## Queries

| Id | Para que |
|---|---|
| B1 | Lista directa de transiciones anomalas (requiere pedido_estado_log) |
| B2 | Detecta bug **sin** historico (encabezado dice PENDIENTE, todas las lineas DESPACHADAS) |
| B3 | Frecuencia mensual del bug ultimos 12 meses |
| B4 | Lista de SPs candidatos a ser responsables (UPDATE pedido_enc.estado) |
| B5 | Correlacion temporal con eventos del outbox (~1h ventana) |

## Estrategia de uso

1. Correr **B2 primero** (no requiere historico). Si retorna >0, el bug
   esta vivo.
2. Si pedido_estado_log existe, correr B1 y B3 para magnitud historica.
3. Correr B4 para enumerar SPs sospechosos.
4. Correr B5 para validar hipotesis "NavSync dispara el bug".

## Red flags

- **RF1**: B1 retorna >0 filas en los ultimos 30d → bug activo, frenar
  deploys hasta encontrar SP responsable.
- **RF2**: B4 muestra >1 SP que UPDATEa estado → multiples puntos de
  escritura, consolidar.

## Vinculo con learning

Cuando se confirme la causa raiz, generar **learning card**
`L-XXX-bug-p16b-causa-raiz.md` con:
- SP responsable.
- Patron de codigo culpable.
- Fix propuesto.
- Workaround temporal.

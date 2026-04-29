# CP-007 — frmStockEnUnaFecha marker `Serie = "#EJCAJUSTEDESFASE"`

> El string `"#EJCAJUSTEDESFASE"` se asigna a `Serie` de los movimientos `trans_movimientos` modificados por el `ModoDepuracion` del reporte estándar. Es la **única huella en BD** de que un movimiento fue tocado por la herramienta de auto-corrección. Las iniciales `EJC` son de Erik.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-007-stockfecha-marker-ejcajustedesfase |
| Tipo | hardcode (string asignado a campo persistente de BD) |
| Estado | documentado — efecto persistente en producción |
| Severidad estimada | alta (es la huella del `V-DATAWAY-001`) |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb` |
| Línea | dentro del bloque `Llena_Grid` L401-435 |
| Persistencia | **sí** — se escribe a `trans_movimientos.Serie` |
| Bug enlazado | `V-DATAWAY-001` (anti-patrón ModoDepuracion) |

## Cita textual

`frmStockEnUnaFecha.vb` (dentro del bloque `If Diferencia <> 0 AndAlso ModoDepuracion`):

```vb
For Each M In lMovimientos
    If Diferencia <> 0 Then
        Debug.Print("IdMovimiento: " & M.IdMovimiento & " " & M.Cantidad)
        If M.Cantidad >= Math.Abs(Diferencia) Then
            M.Cantidad += Diferencia
            Diferencia += 1
        Else
            M.Cantidad = 0
            Diferencia += M.Cantidad
        End If
        M.Serie = "#EJCAJUSTEDESFASE"   ' <-- aquí
        If M.Cantidad = 0 Then
            clsLnTrans_movimientos.Eliminar(M)
        Else
            clsLnTrans_movimientos.Actualizar(M)
        End If
    End If
Next
```

## Por qué es un case-pointer especial

A diferencia de los CP-001..CP-006 (que viven en código y no afectan datos), **CP-007 deja huella persistente en `trans_movimientos`**. Cualquier auditoría de BD puede encontrar todos los movimientos que pasaron por el `ModoDepuracion`.

Esto lo convierte en:

- **Case-pointer auto-confirmable**: no requiere reproducir el bug, solo contar registros con esa `Serie`.
- **Métrica del impacto histórico** del `V-DATAWAY-001`.
- **Trail de auditoría informal**: aunque no hay tabla de auditoría formal, la `Serie` actúa como tag de "este movimiento fue mutado por la herramienta de auto-corrección".

## Estructura del marker

```
#EJCAJUSTEDESFASE
│└┬┘└─────┬──────┘
│ │       └─ AJUSTEDESFASE = "ajuste de desfase" (compensación de gap)
│ └─ EJC = iniciales Erik José Calderón
└─ # = prefijo distintivo (probable convención: tags de sistema empiezan con #)
```

**Comparar con otros prefijos `#` en el sistema** para confirmar la convención:

```bash
rg -n 'Serie\s*=\s*"#[A-Z]' /tmp/repos/TOMWMS_BOF/ --type vb
```

Si aparecen otros (`#EJCAJUSTEINICIAL`, `#SISTEMA`, etc.), CP-007 es parte de un sistema más amplio de tags. Si es único, es exclusivamente esta herramienta.

## Métrica del impacto histórico

Query propuesta (a agregar a `tools/case-seed/queries/data-discrepancy/`):

```sql
-- 06_movimientos_ejcajustedesfase.sql
-- Cuántos movimientos tienen el marker del ModoDepuracion?
-- Por cliente, fecha primer y último uso, total de cantidad afectada.

SELECT
    L.NombreLicencia,
    COUNT(*) AS CantidadMovimientos,
    MIN(M.Fecha) AS PrimerUso,
    MAX(M.Fecha) AS UltimoUso,
    SUM(ABS(M.Cantidad)) AS TotalCantidadAfectada
FROM trans_movimientos M
INNER JOIN licencia L ON L.IdLicencia = M.IdLicencia
WHERE M.Serie = '#EJCAJUSTEDESFASE'
GROUP BY L.NombreLicencia
ORDER BY UltimoUso DESC
```

Esta query responde:

1. **¿En qué clientes se usó?** (Si solo aparece en uno, era una herramienta para un caso específico; si aparece en muchos, era de uso rutinario.)
2. **¿Cuándo fue la última vez?** (Si última fecha es reciente: la herramienta sigue activa. Si es vieja: deuda muerta.)
3. **¿Cuál es la magnitud del impacto?** (Suma de cantidades afectadas — nos dice si es ruido o si se reescribió mucha historia.)

## Decisión a tomar (futura)

Una vez que tengamos los números:

- **Si la herramienta se sigue usando**: deprecación urgente + auditoría real (ver `R1` del `anti-patron-modo-depuracion.md`).
- **Si no se usó en años**: borrar el código del `ModoDepuracion` + dejar el `Serie = "#EJCAJUSTEDESFASE"` como marker histórico de auditoría (no borrar de BD, es evidencia).
- **Si se usó pero hace tiempo**: no hacer nada urgente, pero documentar cómo se usaba para que no vuelva.

## Bitácora de debug

Ver `brain/debuged-cases/CP-007.md`. Es la bitácora "más cerca de evidencia dura" porque la query 06 puede confirmar/refutar el impacto sin entrevistar a nadie.

## Cross-refs

- `00-INDEX.md`
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — `V-DATAWAY-001` (análisis completo de la herramienta que produce este marker)
- `dataway-analysis/00-modelo-identidad-idstock.md` — la mutación afecta también la inferencia de linaje del IdStock
- `tools/case-seed/queries/data-discrepancy/06_movimientos_ejcajustedesfase.sql` — pendiente agregar
- `brain/debuged-cases/CP-007.md`

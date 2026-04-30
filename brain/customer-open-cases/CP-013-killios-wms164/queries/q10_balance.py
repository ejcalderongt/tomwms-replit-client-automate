"""q10: aplicar la formula de balance al caso para confirmar que las cantidades
    cuadran (el bug no es de cantidad — es de persistencia).

Balance teorico:
  Inicial (RECE 2179)        +
  Sum(Ingresos abril)        +
  Sum(AjustesPos abril)      -
  Sum(AjustesNeg abril)      -
  Sum(Salidas abril)         =
  Existencia teorica = SUM(stock.Cantidad para IdProductoBodega=381)

Esperado: 270 UN (= 40+95+40+30+65 = los 5 movimientos UBIC/CEST suman al
mismo total que la posicion fisica).
"""
from _db import q

print("=== sum por TipoTarea para abril ===")
for r in q("""
    SELECT t.Codigo, COUNT(*) cnt, SUM(m.Cantidad) total
    FROM trans_movimientos m
    JOIN sis_tipo_tarea t ON t.IdTipoTarea = m.TipoTarea
    WHERE m.IdProductoBodega = 381
      AND m.fec_agr >= '2026-04-01'
    GROUP BY t.Codigo
"""): print(r)

print("=== existencia actual ===")
for r in q("""
    SELECT SUM(Cantidad) total, COUNT(*) filas
    FROM stock WHERE IdProductoBodega = 381 AND Cantidad > 0
"""): print(r)

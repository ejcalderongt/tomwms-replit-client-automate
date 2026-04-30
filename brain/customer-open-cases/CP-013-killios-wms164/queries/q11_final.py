"""q11 (refutacion ModoDepuracion): contar marker '#EJCAJUSTEDESFASE' en Killios.

Si el caso WMS164 fuera una mutacion de los 3 reportes con ModoDepuracion
(CP-007 / CP-008), aparecerian marcas en trans_movimientos.Serie. Si el
conteo es 0, queda CATEGORICAMENTE refutada esa hipotesis y se confirma
que el bug es independiente (V-DATAWAY-004, no V-DATAWAY-001).
"""
from _db import q

for r in q("""
    SELECT COUNT(*) cnt
    FROM trans_movimientos
    WHERE Serie = '#EJCAJUSTEDESFASE'
"""): print(r)

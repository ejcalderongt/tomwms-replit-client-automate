# -*- coding: utf-8 -*-
"""
Helpers para escribir CSVs consistentes desde scripts de auditoría.

Uso:
    from csv_helpers import csv_out
    csv_out('outputs/wave-21/W21-01-foo.csv', rows)

Maneja:
- Lista vacía -> escribe '(sin filas)' al archivo y avisa.
- Tipos datetime/date -> str().
- Crea directorios padre si no existen.
"""
import csv
import os
import datetime as _dt


def _coerce(v):
    if isinstance(v, (_dt.datetime, _dt.date)):
        return v.isoformat()
    if isinstance(v, bytes):
        return v.decode('utf-8', errors='replace')
    return v


def csv_out(path: str, rows: list) -> int:
    """Escribe rows (lista de dicts) a path. Retorna n filas escritas."""
    os.makedirs(os.path.dirname(os.path.abspath(path)) or '.', exist_ok=True)
    if not rows:
        with open(path, 'w', encoding='utf-8') as f:
            f.write('(sin filas)\n')
        print(f"  {os.path.basename(path)}: 0 filas")
        return 0
    keys = list(rows[0].keys())
    with open(path, 'w', newline='', encoding='utf-8') as f:
        w = csv.DictWriter(f, fieldnames=keys)
        w.writeheader()
        for r in rows:
            w.writerow({k: _coerce(r.get(k)) for k in keys})
    print(f"  {os.path.basename(path)}: {len(rows)} filas")
    return len(rows)

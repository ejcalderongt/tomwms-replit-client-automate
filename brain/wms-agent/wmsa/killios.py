"""Cliente de SQL Server productivo — SOLO LECTURA.

Lazy import de pyodbc para que el agente funcione sin ODBC instalado
(quien no use el módulo `db` no lo necesita).

Validación de queries: rechaza cualquier statement que pueda escribir.
La validación es defensiva, no a prueba de actores maliciosos. Para un
agente local que el dueño controla, alcanza.
"""

from __future__ import annotations

import re
from typing import Any, Iterator

from .config import Config

# Comandos permitidos al inicio de la query (case-insensitive).
_ALLOWED_PREFIXES = ("SELECT", "WITH", "EXEC", "EXECUTE", "SET", "DECLARE", "PRINT")
# Patrones bloqueados — aunque empiecen con SELECT, si contienen estas
# palabras como statement (no en strings), se rechazan.
_FORBIDDEN_RE = re.compile(
    r"\b(INSERT|UPDATE|DELETE|MERGE|TRUNCATE|DROP|ALTER|CREATE|GRANT|REVOKE|BULK\s+INSERT)\b",
    re.IGNORECASE,
)


class KilliosError(Exception):
    """Error consultando KILLIOS."""


class QueryRejected(KilliosError):
    """La query fue rechazada por el guardrail read-only."""


def assert_read_only(sql: str) -> None:
    """Guardrail: asegura que la query no escribe.

    Levanta QueryRejected si detecta un statement de escritura.
    No es una protección criptográfica — un atacante con acceso al CLI
    puede inventar comentarios para ofuscar. Asumimos buena fe del dueño.
    """
    stripped = sql.strip()
    if not stripped:
        raise QueryRejected("Query vacía")
    # Quitar comentarios de línea y bloque para análisis.
    cleaned = re.sub(r"--[^\n]*", "", stripped)
    cleaned = re.sub(r"/\*.*?\*/", "", cleaned, flags=re.DOTALL)
    head = cleaned.lstrip().upper()
    if not any(head.startswith(p) for p in _ALLOWED_PREFIXES):
        raise QueryRejected(
            f"Query rechazada: debe empezar con {', '.join(_ALLOWED_PREFIXES)}"
        )
    if _FORBIDDEN_RE.search(cleaned):
        raise QueryRejected(
            "Query rechazada: contiene un statement de escritura "
            "(INSERT/UPDATE/DELETE/MERGE/DROP/ALTER/CREATE/TRUNCATE)"
        )


class KilliosClient:
    def __init__(self, config: Config) -> None:
        self._cfg = config
        if not (config.db.host and config.db.name and config.db.user):
            raise KilliosError(
                "DB no configurada. Corré: wmsa config init"
            )
        if not config.db.password:
            raise KilliosError(
                "Password de DB no encontrado en keyring ni en env."
            )
        try:
            import pyodbc  # noqa: F401
        except ImportError as e:
            raise KilliosError(
                "pyodbc no instalado. Corré: pip install -e .[db]"
            ) from e
        self._pyodbc = __import__("pyodbc")

    def _connect(self):
        cn_str = (
            "DRIVER={ODBC Driver 17 for SQL Server};"
            f"SERVER={self._cfg.db.host};"
            f"DATABASE={self._cfg.db.name};"
            f"UID={self._cfg.db.user};"
            f"PWD={self._cfg.db.password};"
            "Encrypt=no;"
            "ApplicationIntent=ReadOnly;"
        )
        try:
            return self._pyodbc.connect(cn_str, timeout=10, autocommit=True)
        except self._pyodbc.Error as e:
            # Enriquecer el error para que sea obvio qué host/db/user se usó,
            # SIN exponer el password. Los errores ODBC crudos son crípticos.
            sqlstate = e.args[0] if e.args else "?"
            msg = str(e)
            raise KilliosError(
                f"No se pudo conectar a SQL Server.\n"
                f"  host = {self._cfg.db.host}\n"
                f"  db   = {self._cfg.db.name}\n"
                f"  user = {self._cfg.db.user}\n"
                f"  sqlstate = {sqlstate}\n"
                f"  driver said: {msg[:300]}"
            ) from e

    def query(self, sql: str, max_rows: int = 1000) -> dict[str, Any]:
        assert_read_only(sql)
        with self._connect() as cn:
            cur = cn.cursor()
            cur.execute(sql)
            if cur.description is None:
                return {"columns": [], "rows": [], "row_count": 0, "truncated": False}
            cols = [c[0] for c in cur.description]
            rows: list[list[Any]] = []
            truncated = False
            for i, r in enumerate(cur):
                if i >= max_rows:
                    truncated = True
                    break
                rows.append([_to_jsonable(v) for v in r])
            return {
                "columns": cols,
                "rows": rows,
                "row_count": len(rows),
                "truncated": truncated,
            }

    def list_tables(self, like: str | None = None) -> list[dict[str, str]]:
        sql = (
            "SELECT TABLE_SCHEMA AS [schema], TABLE_NAME AS [name], TABLE_TYPE AS [type] "
            "FROM INFORMATION_SCHEMA.TABLES"
        )
        if like:
            sql += " WHERE TABLE_NAME LIKE ?"
        sql += " ORDER BY TABLE_SCHEMA, TABLE_NAME"
        with self._connect() as cn:
            cur = cn.cursor()
            if like:
                cur.execute(sql, (like,))
            else:
                cur.execute(sql)
            return [
                {"schema": r[0], "name": r[1], "type": r[2]} for r in cur.fetchall()
            ]

    def sp_text(self, name: str) -> str | None:
        """Devuelve el cuerpo de un SP/función/vista."""
        with self._connect() as cn:
            cur = cn.cursor()
            cur.execute(
                "SELECT m.definition "
                "FROM sys.sql_modules m "
                "JOIN sys.objects o ON o.object_id = m.object_id "
                "WHERE o.name = ?",
                (name,),
            )
            row = cur.fetchone()
            return row[0] if row else None


def _to_jsonable(v: Any) -> Any:
    """Convierte tipos pyodbc a algo JSON-friendly."""
    if v is None:
        return None
    if isinstance(v, (str, int, float, bool)):
        return v
    # datetime, Decimal, bytes, etc → str
    return str(v)

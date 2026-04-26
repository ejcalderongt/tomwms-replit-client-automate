"""Punto de entrada del CLI `wmsa`."""

from __future__ import annotations

import json
from getpass import getpass
from typing import Optional

import typer
from rich import print as rprint
from rich.console import Console
from rich.table import Table

from . import __version__
from .config import Config

app = typer.Typer(help="wms-agent — swiss-army CLI del proyecto WMS.")
console = Console()

# ---------- subapps ---------------------------------------------------------

config_app = typer.Typer(help="Configurar credenciales y endpoints.")
brain_app = typer.Typer(help="Consultas al TOMWMS Brain (Replit).")
db_app = typer.Typer(help="Queries de SOLO LECTURA a la BD productiva.")
case_app = typer.Typer(help="Casos: intake estructurado + escalado a Replit.")

app.add_typer(config_app, name="config")
app.add_typer(brain_app, name="brain")
app.add_typer(db_app, name="db")
app.add_typer(case_app, name="case")


@app.command()
def version() -> None:
    """Muestra la versión instalada."""
    rprint(f"wms-agent [bold]{__version__}[/bold]")


# ---------- config ----------------------------------------------------------


@config_app.command("init")
def config_init() -> None:
    """Setup interactivo de credenciales (no-sensible en JSON, secrets en keyring)."""
    cfg = Config.load()
    rprint("[bold]Brain (Replit)[/bold]")
    cfg.brain.base_url = typer.prompt(
        "Brain base URL",
        default=cfg.brain.base_url
        or "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain",
    )
    token = getpass("Brain token (X-Brain-Token; vacío para no cambiar): ")
    if token:
        cfg.store_brain_token(token)

    rprint("\n[bold]BD productiva (KILLIOS u otra)[/bold]")
    cfg.db.host = typer.prompt("DB host[,puerto]", default=cfg.db.host or "52.41.114.122,1437")
    cfg.db.name = typer.prompt("DB name", default=cfg.db.name or "TOMWMS_KILLIOS_PRD")
    cfg.db.user = typer.prompt("DB user", default=cfg.db.user or "wmsuser")
    pwd = getpass("DB password (vacío para no cambiar): ")

    cfg.save()
    if pwd:
        cfg.store_db_password(pwd)

    rprint("\n[green]Listo.[/green] Config guardada en " + str(cfg.to_safe_dict()["config_file"]))


@config_app.command("show")
def config_show() -> None:
    """Muestra la config actual sin secrets."""
    cfg = Config.load()
    rprint(json.dumps(cfg.to_safe_dict(), indent=2, ensure_ascii=False))


@config_app.command("set-db")
def config_set_db(
    name: str = typer.Argument(..., help="Nuevo nombre de DB"),
    host: Optional[str] = typer.Option(None, "--host", help="Cambiar host también"),
) -> None:
    """Cambia rápido la BD destino (útil al saltar entre clientes)."""
    cfg = Config.load()
    cfg.db.name = name
    if host:
        cfg.db.host = host
    cfg.save()
    pwd = cfg.db.password
    if not pwd:
        rprint(f"[yellow]Aviso: no hay password en keyring para {cfg.db.host}/{name}.[/yellow]")
        rprint("Corré `wmsa config init` o seteá WMS_KILLIOS_DB_PASSWORD.")
    else:
        rprint(f"[green]OK[/green] DB ahora apunta a {cfg.db.host}/{name}")


@config_app.command("clear")
def config_clear(
    confirm: bool = typer.Option(False, "--yes", help="Confirmar sin preguntar"),
) -> None:
    """Borra todos los secrets del keyring (no toca el JSON de config)."""
    if not confirm:
        ok = typer.confirm("¿Borrar token del Brain y password de DB del keyring?")
        if not ok:
            return
    cfg = Config.load()
    cfg.clear_secrets()
    rprint("[green]Secrets eliminados del keyring.[/green]")


# ---------- brain -----------------------------------------------------------


def _brain():
    from .brain import BrainClient  # lazy import

    return BrainClient(Config.load())


def _print_json(data) -> None:
    rprint(json.dumps(data, indent=2, ensure_ascii=False))


@brain_app.command("health")
def brain_health() -> None:
    _print_json(_brain().health())


@brain_app.command("search")
def brain_search(
    q: str = typer.Argument(..., help="Texto a buscar"),
    kind: Optional[str] = typer.Option(None, "--kind"),
    repo: Optional[str] = typer.Option(None, "--repo"),
    limit: int = typer.Option(20, "--limit"),
) -> None:
    _print_json(_brain().search(q, kind=kind, repo=repo, limit=limit))


@brain_app.command("impact")
def brain_impact(
    symbol: str = typer.Argument(...),
    kind: Optional[str] = typer.Option(None, "--kind"),
    repo: Optional[str] = typer.Option(None, "--repo"),
    depth: int = typer.Option(1, "--depth"),
    limit: int = typer.Option(100, "--limit"),
) -> None:
    _print_json(_brain().impact(symbol, kind=kind, repo=repo, depth=depth, limit=limit))


@brain_app.command("dependencies")
def brain_dependencies(
    symbol: str = typer.Argument(...),
    kind: Optional[str] = typer.Option(None, "--kind"),
    repo: Optional[str] = typer.Option(None, "--repo"),
    depth: int = typer.Option(1, "--depth"),
    limit: int = typer.Option(100, "--limit"),
) -> None:
    _print_json(
        _brain().dependencies(symbol, kind=kind, repo=repo, depth=depth, limit=limit)
    )


@brain_app.command("writers")
def brain_writers(
    symbol: str = typer.Argument(...),
    op: Optional[str] = typer.Option(
        None, "--op", help="CSV: insert,update,delete,merge,exec"
    ),
    kind: Optional[str] = typer.Option(None, "--kind"),
    repo: Optional[str] = typer.Option(None, "--repo"),
    limit: int = typer.Option(100, "--limit"),
) -> None:
    _print_json(_brain().writers(symbol, op=op, kind=kind, repo=repo, limit=limit))


@brain_app.command("reindex")
def brain_reindex(
    kind: str = typer.Argument(..., help="vb | java"),
    repos: list[str] = typer.Argument(..., help="Repos a reindexar"),
) -> None:
    _print_json(_brain().reindex(kind, repos))


# ---------- db --------------------------------------------------------------


def _db():
    from .killios import KilliosClient  # lazy import

    return KilliosClient(Config.load())


@db_app.command("query")
def db_query(
    sql: str = typer.Argument(..., help='SQL de solo lectura, entre comillas'),
    max_rows: int = typer.Option(100, "--max-rows"),
    json_out: bool = typer.Option(False, "--json", help="Salida JSON cruda"),
) -> None:
    """Ejecuta una query read-only contra la BD configurada."""
    res = _db().query(sql, max_rows=max_rows)
    if json_out:
        _print_json(res)
        return
    if not res["rows"]:
        rprint("[yellow](sin filas)[/yellow]")
        return
    table = Table(show_lines=False)
    for c in res["columns"]:
        table.add_column(str(c))
    for row in res["rows"]:
        table.add_row(*[str(v) if v is not None else "" for v in row])
    console.print(table)
    if res["truncated"]:
        rprint(f"[yellow](truncado a {max_rows} filas; subí --max-rows si necesitás más)[/yellow]")


@db_app.command("tables")
def db_tables(
    like: Optional[str] = typer.Option(None, "--like", help="Patrón LIKE, ej. %stock%"),
) -> None:
    rows = _db().list_tables(like=like)
    if not rows:
        rprint("[yellow](sin resultados)[/yellow]")
        return
    table = Table()
    table.add_column("schema")
    table.add_column("name")
    table.add_column("type")
    for r in rows:
        table.add_row(r["schema"], r["name"], r["type"])
    console.print(table)


@db_app.command("sp-text")
def db_sp_text(name: str) -> None:
    """Imprime la definición SQL de un SP/vista/función."""
    text = _db().sp_text(name)
    if text is None:
        rprint(f"[red]No se encontró módulo `{name}`[/red]")
        raise typer.Exit(1)
    rprint(text)


# ---------- case ------------------------------------------------------------


@case_app.command("new")
def case_new() -> None:
    """Intake interactivo de un caso nuevo. Guarda en ~/.wmsa/cases/."""
    from .case import Case, new_case_id, CASE_TYPES, SEVERITIES

    rprint("[bold]Nuevo caso WMS[/bold] — Enter para saltar campos opcionales.\n")
    reporter = typer.prompt("Reportado por (nombre o iniciales)")
    cid = typer.prompt("ID del caso (Enter = auto)", default=new_case_id(reporter))
    ctype = typer.prompt(f"Tipo {CASE_TYPES}", default="data-discrepancy")
    severity = typer.prompt(f"Severidad {SEVERITIES}", default="media")
    cfg = Config.load()
    client_db = typer.prompt("Cliente / BD", default=cfg.db.name or "KILLIOS")
    reported_at = typer.prompt(
        "Fecha del reporte (YYYY-MM-DD)",
        default=__import__("datetime").date.today().isoformat(),
    )
    symptom = typer.prompt("Síntoma observable (frase del usuario)")

    case = Case(
        id=cid,
        type=ctype,
        severity=severity,
        client_db=client_db,
        reporter=reporter,
        reported_at=reported_at,
        symptom=symptom,
    )
    case.created_at = __import__("datetime").datetime.now().isoformat(timespec="seconds")

    case.sku = typer.prompt("SKU / IdProducto", default="")
    case.bodega = typer.prompt("Bodega / IdBodega", default="")
    case.lote_idstock = typer.prompt("IdLote / IdStock", default="")
    case.last_good_at = typer.prompt("Última vez bien (YYYY-MM-DD HH:MM)", default="")
    case.first_bad_at = typer.prompt("Primera vez mal (YYYY-MM-DD HH:MM)", default="")
    case.hypothesis = typer.prompt("Hipótesis del equipo", default="")
    case.expected_result = typer.prompt("Resultado esperado", default="")

    case.save()
    rprint(f"\n[green]Caso guardado[/green] en {case.path()}")
    rprint("Próximos pasos:")
    rprint(f"  wmsa case show {cid}")
    rprint(f"  wmsa case escalate {cid}     # imprime el bloque listo para Replit")


@case_app.command("list")
def case_list() -> None:
    from .case import Case

    cases = Case.list_all()
    if not cases:
        rprint("[yellow](sin casos)[/yellow]")
        return
    table = Table()
    table.add_column("id")
    table.add_column("tipo")
    table.add_column("sev")
    table.add_column("cliente")
    table.add_column("síntoma")
    for c in cases:
        table.add_row(
            c.id,
            c.type,
            c.severity,
            c.client_db,
            (c.symptom or "")[:60],
        )
    console.print(table)


@case_app.command("show")
def case_show(case_id: str) -> None:
    from .case import Case

    rprint(Case.load(case_id).to_markdown())


@case_app.command("escalate")
def case_escalate(
    case_id: str,
    out: Optional[str] = typer.Option(None, "--out", help="Guardar a archivo"),
) -> None:
    """Render del caso en Markdown listo para pegar en chat de Replit."""
    from .case import Case

    md = Case.load(case_id).to_markdown()
    if out:
        from pathlib import Path

        Path(out).write_text(md, encoding="utf-8")
        rprint(f"[green]OK[/green] escrito a {out}")
    else:
        print(md)


if __name__ == "__main__":
    app()

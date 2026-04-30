# -*- coding: utf-8 -*-
"""
replicate.py — Carga el yml de un cliente y permite re-correr hallazgos.

Uso:
    python replicate.py --all                            # lista TODOS los clientes registrados
    python replicate.py <slug> --profile                 # ficha completa del cliente
    python replicate.py <slug> --list                    # lista revisiones
    python replicate.py <slug> --smoke                   # smoke test conexión
    python replicate.py <slug> --caso <case_id>          # info del caso
    python replicate.py <slug> --query <query_id>        # corre query replicable
    python replicate.py <slug> --bugs                    # lista bugs documentados
    python replicate.py <slug> --outliers                # lista outliers conocidos

Requiere PyYAML.

El yml vive en el mismo directorio que este script. Yml mínimos (sin
'revisiones' ni 'queries_replicables') son válidos: representan un perfil
de cliente registrado pero sin investigación todavía.
"""
import argparse
import os
import sys

try:
    import yaml
except ImportError:
    print("ERROR: pyyaml no instalado. pip install pyyaml", file=sys.stderr)
    sys.exit(1)

HERE = os.path.dirname(os.path.abspath(__file__))


def load(slug: str) -> dict:
    path = os.path.join(HERE, f"{slug}.yml")
    if not os.path.exists(path):
        print(f"ERROR: no existe {path}", file=sys.stderr)
        sys.exit(2)
    with open(path, encoding='utf-8') as f:
        return yaml.safe_load(f)


def cmd_list(cli: dict):
    print(f"== {cli['cliente']} (slug: {cli['slug']}) ==")
    print(f"BD activa: {cli['bd'].get('database_actual', '<env>')}")
    revs = cli.get('revisiones', [])
    print(f"\nRevisiones registradas: {len(revs)}")
    for r in revs:
        print(f"  - {r['fecha']}  {r['caso']}")
        if r.get('sintoma_original_cliente'):
            print(f"      síntoma: {r['sintoma_original_cliente'][:80]}")
        for h in r.get('hallazgos', [])[:3]:
            print(f"      * {h}")
        if len(r.get('hallazgos', [])) > 3:
            print(f"      ... ({len(r['hallazgos'])-3} hallazgos más)")


def cmd_smoke(cli: dict):
    # Inyecta el slug en WMS_CLIENTE para que _db.py tome las vars correctas
    os.environ['WMS_CLIENTE'] = cli['slug'].upper()
    # Importar después de setear env
    sys.path.insert(0, os.path.join(HERE, '..', 'data-seek-strategy', 'templates'))
    try:
        from _db import smoke_test  # type: ignore
    except Exception as e:
        print(f"ERROR cargando _db.py: {e}", file=sys.stderr)
        sys.exit(3)
    smoke_test()


def cmd_caso(cli: dict, caso_id: str):
    revs = [r for r in cli.get('revisiones', []) if r['caso'] == caso_id]
    if not revs:
        print(f"ERROR: caso '{caso_id}' no registrado para {cli['cliente']}.")
        print("Casos disponibles:")
        for r in cli.get('revisiones', []):
            print(f"  - {r['caso']}")
        sys.exit(4)
    r = revs[0]
    print(f"== Caso {r['caso']} ({r['fecha']}) ==")
    print(f"Rama brain: {r['rama_brain']}")
    if r.get('sintoma_original_cliente'):
        print(f"\nSíntoma original: {r['sintoma_original_cliente']}")
    print(f"\nHallazgos ({len(r['hallazgos'])}):")
    for h in r['hallazgos']:
        print(f"  * {h}")
    print(f"\nWaves ({len(r.get('waves', []))}):")
    for w in r.get('waves', []):
        print(f"  - W{w['id']}: {w['descripcion']}")
    print(f"\nDocumentos vigentes:")
    for d in r.get('documentos_vigentes', []):
        print(f"  - {d}")


def cmd_query(cli: dict, query_id: str):
    queries = cli.get('queries_replicables', {})
    if query_id not in queries:
        print(f"ERROR: query '{query_id}' no definida.")
        print("Queries disponibles:")
        for k, v in queries.items():
            print(f"  - {k}: {v.get('descripcion', '')}")
        sys.exit(5)
    qdef = queries[query_id]
    print(f"== Query: {query_id} ==")
    print(f"Descripción: {qdef.get('descripcion')}")
    if qdef.get('parametros'):
        print(f"Parámetros: {qdef['parametros']}")
    if qdef.get('referencia_doc'):
        print(f"Documentación: {qdef['referencia_doc']}")
    if qdef.get('referencia_template'):
        print(f"Template ejecutable: {qdef['referencia_template']}")
    if qdef.get('sql'):
        os.environ['WMS_CLIENTE'] = cli['slug'].upper()
        sys.path.insert(0, os.path.join(HERE, '..', 'data-seek-strategy', 'templates'))
        from _db import q  # type: ignore
        print("\nResultado:")
        for row in q(qdef['sql'])[:20]:
            print(f"  {row}")


def cmd_bugs(cli: dict):
    print(f"== Bugs documentados {cli['cliente']} ==")
    for b in cli.get('bugs_documentados', []):
        print(f"\n[{b['severidad'].upper()}] {b['id']}")
        print(b['descripcion'])
        if b.get('cronicidad'):
            print(f"Cronicidad desde: {b['cronicidad'].get('desde_estimado')}")
            print(f"Evidencia: {b['cronicidad'].get('evidencia')}")
        print(f"Caso de estudio: {b.get('caso_de_estudio')}")


def cmd_outliers(cli: dict):
    print(f"== Outliers conocidos {cli['cliente']} ==")
    for o in cli.get('outliers_conocidos', []):
        print(f"\nTabla: {o['tabla']} | PK: {o['pk']}")
        print(f"  Valor: {o.get('valor')}")
        print(f"  Fecha: {o.get('fecha')}")
        print(f"  Observación: {o.get('observacion')}")
        print(f"  Descripción: {o['descripcion']}")


def cmd_all():
    """Lista TODOS los clientes registrados en client-index/."""
    print("== Clientes registrados ==")
    files = sorted(f for f in os.listdir(HERE) if f.endswith('.yml'))
    if not files:
        print("  (ninguno)")
        return
    print(f"  {'slug':<14} {'cliente':<14} {'estado':<12} {'BD activa':<28} bugs")
    print('  ' + '-' * 100)
    for fn in files:
        slug = fn[:-4]
        try:
            with open(os.path.join(HERE, fn), encoding='utf-8') as f:
                cli = yaml.safe_load(f)
            estado = str(cli.get('estado', '-'))
            bd = str(cli.get('bd', {}).get('database_actual') or cli.get('bd', {}).get('nombre', '-'))
            n_bugs = len(cli.get('bugs_documentados') or cli.get('bugs_conocidos', []))
            print(f"  {slug:<14} {cli.get('cliente','-'):<14} {estado:<12} {bd:<28} {n_bugs}")
        except Exception as e:
            print(f"  {slug:<14} <ERROR: {e}>")


def cmd_profile(cli: dict):
    """Imprime perfil completo del cliente sin formato. Útil para yml mínimos."""
    import pprint
    print(f"== Perfil completo: {cli['cliente']} ({cli['slug']}) ==\n")
    pprint.pprint(cli, sort_dicts=False, width=100)


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument('slug', nargs='?', help='slug del cliente (ej: killios). Omitir si usás --all.')
    ap.add_argument('--all', action='store_true', help='lista TODOS los clientes registrados')
    ap.add_argument('--profile', action='store_true', help='ficha completa del cliente')
    ap.add_argument('--list', action='store_true', help='lista revisiones')
    ap.add_argument('--smoke', action='store_true', help='smoke test conexión')
    ap.add_argument('--caso', help='info de un caso registrado')
    ap.add_argument('--query', help='corre/info de una query replicable')
    ap.add_argument('--bugs', action='store_true', help='lista bugs documentados')
    ap.add_argument('--outliers', action='store_true', help='lista outliers conocidos')
    args = ap.parse_args()

    if args.all:
        cmd_all()
        return

    if not args.slug:
        ap.error('falta el slug (o usá --all)')

    cli = load(args.slug)

    if args.profile:
        cmd_profile(cli)
    elif args.list:
        cmd_list(cli)
    elif args.smoke:
        cmd_smoke(cli)
    elif args.caso:
        cmd_caso(cli, args.caso)
    elif args.query:
        cmd_query(cli, args.query)
    elif args.bugs:
        cmd_bugs(cli)
    elif args.outliers:
        cmd_outliers(cli)
    else:
        # Sin sub-comando: si no tiene revisiones, mostrar perfil; sino, listar revisiones.
        if cli.get('revisiones'):
            cmd_list(cli)
        else:
            cmd_profile(cli)


if __name__ == '__main__':
    main()

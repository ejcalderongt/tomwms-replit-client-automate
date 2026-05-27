#!/usr/bin/env python3
import argparse
from pathlib import Path

def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--trace-map", required=True)
    ap.add_argument("--param-impact", required=True)
    ap.add_argument("--out", required=True)
    args = ap.parse_args()
    trace = Path(args.trace_map).read_text(encoding="utf-8", errors="ignore")
    params = Path(args.param_impact).read_text(encoding="utf-8", errors="ignore")
    out = [
        "id: telemetry-correlation",
        "tag: '#EJC20260527'",
        "summary:",
        f"  trace_map_lines: {len(trace.splitlines())}",
        f"  param_impact_lines: {len(params.splitlines())}",
        "notes:",
        "  - 'Correlate trace_id and entity_ids during incident replay.'",
        "  - 'Prioritize parameters with high occurrences and cross-process spread.'",
    ]
    Path(args.out).write_text("\n".join(out) + "\n", encoding="utf-8")
    print(f"Wrote {args.out}")

if __name__ == "__main__":
    main()


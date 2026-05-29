param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")]
    [string]$Process,
    [Parameter(Mandatory = $true)]
    [string]$Symptom,
    [string]$RepoRoot = "C:\Users\yejc2\source\repos\wms-brain",
    [string]$Tag = "EJC20260527"
)

$ErrorActionPreference = "Stop"

$slug = ($Symptom.ToLowerInvariant() -replace '[^a-z0-9]+', '-').Trim('-')
if ([string]::IsNullOrWhiteSpace($slug)) { $slug = "incident" }
$ts = Get-Date -Format "yyyyMMdd-HHmmss"
$relativeOut = "wms-brain/brain/handoffs/2026-05-27-root-cause-accelerator/$ts-$Process-$slug.yml"
$out = Join-Path $RepoRoot $relativeOut

$dir = Split-Path -Parent $out
if (-not (Test-Path $dir)) {
    New-Item -ItemType Directory -Path $dir | Out-Null
}

$pathHints = @{
    "picking" = @("HH: frm_preparacion_picking / reemplazo picking", "WS: WSHHRN asmx picking methods", "DAL: clsTransacciones/clsPicking", "SQL: trans_picking_ubic + trans_movimientos")
    "cambio_ubicacion" = @("HH: frm_cambio_ubicacion_*", "WS: move/apply endpoints", "DAL: movement apply methods", "SQL: trans_movimientos.IdUbicacionDestino")
    "verificacion" = @("HH: verificacion forms", "WS: verification services", "DAL: verify/update methods", "SQL: trans_pe_det / trans_movimientos / stock_res")
    "recepcion" = @("HH: recepcion forms", "WS: recepcion services", "DAL: recepcion update methods", "SQL: trans_re_* + stock + trans_movimientos")
    "existencias" = @("HH: consulta existencias", "WS: stock query methods", "DAL: stock readers", "SQL: stock + views + optional pagination points")
    "inventario" = @("HH/BOF inventory flows", "WS: inventory services", "DAL: inventory apply", "SQL: trans_inv_* + stock")
    "packing" = @("HH/BOF packing flows", "WS: packing services", "DAL: packing movement", "SQL: trans_packing_* + trans_movimientos")
}

$content = @(
    "id: rca-$ts-$Process",
    "tag: '#$Tag'",
    "created_at: '$((Get-Date).ToString("s"))'",
    "process: $Process",
    "symptom: ""$Symptom""",
    "status: in_analysis",
    "candidate_path:",
    "  - ""$($pathHints[$Process][0])""",
    "  - ""$($pathHints[$Process][1])""",
    "  - ""$($pathHints[$Process][2])""",
    "  - ""$($pathHints[$Process][3])""",
    "branch_drift:",
    "  compare_against:",
    "    - dev_2026_mampa",
    "    - dev_2023_estable",
    "  findings: []",
    "null_guard_risks: []",
    "causal_hypothesis: """"",
    "fix_plan: []",
    "post_fix_verification:",
    "  checklist: []",
    "  result: pending"
)

[System.IO.File]::WriteAllLines($out, $content, [System.Text.UTF8Encoding]::new($false))
Write-Output "Created $relativeOut"

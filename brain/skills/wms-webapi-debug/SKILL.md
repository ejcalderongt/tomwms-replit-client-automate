---
name: wms-webapi-debug
description: Use for TOMWMS ASP.NET Core WebAPI debugging and validation: running WMSWebAPI locally, testing MI3 endpoints, creating or posting payloads, enforcing curl 120s timeouts, reading appsettings safely, validating response contracts, checking SyncSalidasService, inspecting WebAPI logs, and comparing API results against SQL Server state.
---

# WMS WebAPI Debug

## Load First

Inside `C:\Users\yejc2\source\repos\TOMWMS`, read:

1. `AGENTS.md`
2. `brain/agents/domain-webapi.yml`
3. `brain/agents/domain-database.yml`
4. If reservation/MI3: `brain/agents/domain-reserva.yml`
5. If performance/timeout: `brain/agents/domain-performance.yml`
6. If BYB or known reference document: `brain/agents/client-byb.yml`

## Local Run Contract

Stop existing API processes before build/run:

```powershell
Get-CimInstance Win32_Process |
  Where-Object {
    ($_.Name -eq 'WMSWebAPI.exe') -or
    ($_.Name -eq 'dotnet.exe' -and $_.CommandLine -like '*WMSWebAPI*')
  } |
  ForEach-Object { Stop-Process -Id $_.ProcessId -Force }
```

Build:

```powershell
dotnet build .\WMSWebAPI\WMSWebAPI.csproj -c Debug --no-restore
```

Run HTTP profile:

```powershell
dotnet run --project WMSWebAPI\WMSWebAPI.csproj --launch-profile http --no-build
```

Post payload with a hard timeout:

```powershell
curl.exe --max-time 120 -X POST "http://localhost:5196/api/sync/salidas/mi3/insertar" `
  -H "Content-Type: application/json" `
  --data-binary "@payload.json"
```

## Response Checks

HTTP 200 is not enough. Confirm:

- `exito`
- `mensaje`
- `lineasProcesadas`
- `totalSolicitado`
- `totalReservado`
- `lineasDetalle`

Then validate DB counts/sums. For reservation parity, use `compare-mi3-reserva-baseline.ps1`.

## Safety

- Do not print full connection strings.
- Confirm `ConnectionStrings:CST` points to the intended QA/client DB before any write.
- Use synthetic document numbers for tests.
- Delete synthetic documents after testing.

## Frequent Failures

- Identity insert error: inspect entity insert PK handling and propagate identity after insert.
- Presentation error for `CJ`: inspect Variant_Code vs Unit_of_Measure_Code presentation resolution.
- Timeout/no response: inspect `#MI3_PERF_SERVICE_IMPORTAR`, `#MI3_PERF_IF_END`, `#MI3_PERF_PIPELINE_TOTAL`.

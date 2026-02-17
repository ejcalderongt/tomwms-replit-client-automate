param(
  [string]$Root = (Get-Location).Path
)

Write-Host "Scanning: $Root" -ForegroundColor Cyan

# Patrones que delatan el origen (packages.config / HintPath / Import / redirects / direct dll refs)
$patterns = @(
  'System\.ValueTuple',
  'System\.ValueTuple\.dll',
  'System\.ValueTuple\.',
  'packages\\System\.ValueTuple',
  'HintPath>.*packages\\System\.ValueTuple',
  '<Import Project=".*packages\\',
  'packages\.config',
  'bindingRedirect.*System\.ValueTuple',
  'assemblyIdentity name="System\.ValueTuple"'
)

# Tipos de archivos donde normalmente aparece el problema
$includeExt = @('*.vbproj','*.csproj','*.fsproj','packages.config','NuGet.config','app.config','web.config','*.targets','*.props','Directory.Packages.props','*.sln')

# Recorre y busca
$results = New-Object System.Collections.Generic.List[object]

Get-ChildItem -Path $Root -Recurse -File -ErrorAction SilentlyContinue |
  Where-Object {
    $extOk = $false
    foreach($e in $includeExt){ if($_.Name -like $e){ $extOk = $true; break } }
    $extOk
  } |
  ForEach-Object {
    $file = $_.FullName
    try {
      $content = Get-Content -Path $file -Raw -ErrorAction Stop
      foreach($p in $patterns){
        if($content -match $p){
          # saca líneas con contexto (linea/archivo)
          $lines = Get-Content -Path $file -ErrorAction Stop
          for($i=0; $i -lt $lines.Count; $i++){
            if($lines[$i] -match $p){
              $results.Add([pscustomobject]@{
                File = $file
                Line = $i + 1
                Match = $lines[$i].Trim()
                Pattern = $p
              })
            }
          }
        }
      }
    } catch { }
  }

# Ordenar, quitar duplicados "iguales" y mostrar
$results |
  Sort-Object File, Line |
  Select-Object File, Line, Pattern, Match |
  Format-Table -AutoSize

# Exporta a CSV para que lo abras y filtres
$out = Join-Path $Root "valuetuple_hits.csv"
$results | Sort-Object File, Line | Export-Csv -NoTypeInformation -Encoding UTF8 $out
Write-Host "`nSaved: $out" -ForegroundColor Green

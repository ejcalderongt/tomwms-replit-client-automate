# L-022 — Patron de naming del binario sincronizador por ERP

> Etiqueta: `L-022_INTEG_NAMING-SYNC-EXE_APPLIED`
> Fecha: 29-abr-2026
> Origen: comparacion cross-cliente K7/BECOFARMA/MAMPA/BYB/CEALSA

## Hallazgo

El campo `i_nav_config_enc.nombre_ejecutable` define el binario
ClickOnce que sincroniza el WMS con el ERP del cliente. El naming
del binario codifica el ERP destino:

| Patron | ERP | Ejemplos confirmados |
|---|---|---|
| `SAPBOSync.exe` | SAP B1 (caso historico, 1 cliente) | BECOFARMA |
| `SAPBOSync<Cliente>.exe` | SAP B1 (caso moderno) | KILLIOS → `SAPBOSyncKillios.exe`, MAMPA → `SAPBOSyncMampa.exe` |
| `NavSync.exe` | NAV / Business Central (caso historico) | BYB |
| `Nav<Cliente>Sync.exe` | NAV (hipotesis, no confirmado todavia) | (pendiente confirmar) |
| `<Cliente>Sync.exe` | ERP propio del cliente | CEALSA → `CEALSASync.exe` |

## Predicado de inferencia

```
sync_exe = c.i_nav_config_enc.nombre_ejecutable

ERP destino = match(sync_exe):
  case "SAPBOSync.exe" or "SAPBOSync*.exe" → "SAP B1"
  case "NavSync.exe" or "Nav*Sync.exe"      → "NAV / BC"
  case other                                 → "Custom / cliente-specific"
```

## Combinacion con `interface_sap`

Confirmacion cruzada: cuando `nombre_ejecutable` empieza con SAPBOSync,
`interface_sap=True`. Cuando es NavSync o custom, `interface_sap=False`.
Estos dos campos son redundantes — se podrian unificar en la WebAPI
a un enum.

## Patron predicho para clientes pendientes

| Cliente | Nombre esperado | Hipotesis ERP |
|---|---|---|
| CUMBRE | `SAPBOSyncCumbre.exe` o `CumbreSync.exe` | a confirmar |
| IDEALSA | `SAPBOSyncIdealsa.exe` o `IDEALSASync.exe` | a confirmar |
| INELAC | `SAPBOSyncInelac.exe` o `INELACSync.exe` | a confirmar |

## Implicaciones para WebAPI

1. La WebAPI nueva debe **leer este campo** para decidir el adaptador
   ERP a usar (puerto/conexion/transformacion de payload).
2. **Strategy pattern** clasico: `IErpAdapter` con implementaciones
   `SapB1Adapter`, `NavAdapter`, `CustomCealsaAdapter`, etc.
3. La fabrica de adaptadores se basa en parsear `nombre_ejecutable` o
   un nuevo campo `erp_kind` (enum) que se podria agregar.

## Cierra hipotesis

- L-015 reforzada: el patron de ClickOnce dispatch es UN BINARIO POR
  CLIENTE para SAP B1 (post-historico) y para custom. NAV historico
  usa binario generico. Caso CEALSA muestra patron `<Cliente>Sync.exe`
  para ERP propio.

## Pendientes

- Confirmar Erik si hay clientes con `Nav<Cliente>Sync.exe` (post-historico
  para NAV).
- Documentar todos los binarios encontrados al hacer fingerprints de
  CUMBRE, IDEALSA, INELAC.

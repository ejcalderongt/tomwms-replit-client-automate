# P-001 — Breakpoint arqueológico con código hardcoded

> Pattern repetido en el repo TOMWMS_BOF: un `If` con un código de producto (o `IdStock`) hardcodeado, cuyo cuerpo es un `Debug.Print` o `Debug.Write` con texto corto ("Espera", "Aqui", "Wait a second!"). Es un breakpoint dejado para parar el debugger **solo** en el caso problemático sin tener que hacer step-through de los casos sanos previos.

## Plantilla canónica

```vb
If <Producto>.Codigo = "<SKU>" [OrElse <Producto>.Codigo = "<OTRO_SKU>" OrElse <BeStock>.IdStock = <ID>] Then
    Debug.Print("<TEXTO_CORTO>")        ' o Debug.Write
End If
```

Variantes observadas:

- Cuerpo **activo** (CP-001, CP-010, CP-011, CP-012)
- Cuerpo **comentado** (CP-009, CP-005)
- Guard simple (CP-001, CP-010, CP-011, CP-012)
- Guard multi-objeto con `OrElse` (CP-009)
- Guard combinando códigos de producto + `IdStock` numérico (CP-009)
- Texto del `Debug.Print` típico: `"Espera"`, `"Aqui"`, `"Wait a second!"`

## Instancias documentadas

| Case-pointer | Archivo | Línea | Hardcode | Cuerpo | Estado |
|---|---|---|---|---|---|
| **CP-001** | `Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb` | 137-145 | `Codigo = "030772033524"` | `Debug.Print("Wait a second!")` activo | open, alta |
| **CP-009** | `Transacciones/Inventario/frmRegularizarInventario.vb` | 526 | `Codigo = "01007121" OrElse Codigo = "01007011" OrElse IdStock = 4427` | `'Debug.Print("Espera")` comentado | open, media |
| **CP-010** | `DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` | 20947 | `Codigo = "00190454"` | `Debug.Print("Aqui")` activo | open, baja |
| **CP-011** | `DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` | 27264 | `Codigo = "00091035"` | `Debug.Write("Espera")` activo | open, baja |
| **CP-012** | `Reportes/Resumen_Stock/frmExistenciasConReserva.vb` | 283 | `Codigo = "01008076"` | `Debug.Print("Espera")` activo | open, baja |

## Hipótesis de origen

1. **Sesión de debug en producción**: el desarrollador (probablemente Erik en la mayoría de los casos) tenía que reproducir un caso específico. En vez de poner un breakpoint en VS y navegar manualmente, agregó un `If <Codigo> = "<SKU>" Then Debug.Print(...)` para que el debugger pare solo cuando ese SKU pasa por el bloque.

2. **Olvido de limpieza**: una vez resuelto el caso (típicamente con un fix ad-hoc en producción), el breakpoint quedó.

3. **Política de "dejar rastro"**: dado el volumen (5 instancias documentadas), es posible que sea política implícita de Erik dejar el rastro como **firma del caso** — para que en el futuro alguien pueda buscar `00190454` en el código y encontrar el sitio donde se trabajó.

## Tratamiento recomendado

Por instancia, decidir entre:

| Decisión | Cuándo | Acción |
|---|---|---|
| **Borrar** | Caso confirmado resuelto, ningún cliente reporta hace > 2 años | Eliminar el `If` entero |
| **Comentar** | Caso resuelto pero querés mantener rastro arqueológico | Cambiar `Debug.Print` activo por `'Debug.Print` comentado |
| **Promover a CP** | Caso vigente, requiere investigación | Documentar como case-pointer y bitácora viva (ya hecho para CP-001/009/010/011/012) |
| **Convertir a check legítimo** | El caso revela una clase de problema real | Reemplazar el `If <SKU específico>` por un `If <propiedad estructural del problema>` con logging real (no `Debug.Print`) |

## Búsqueda heurística

Para encontrar todas las instancias en el repo:

```bash
# Hardcode de Codigo de producto (SKU 8+ dígitos)
rg -n 'Codigo\s*=\s*"[0-9]{8,}"' /tmp/repos/TOMWMS_BOF/ --type vb

# IdStock hardcodeado
rg -n 'IdStock\s*=\s*\d{4,}' /tmp/repos/TOMWMS_BOF/ --type vb

# Debug.Print/Write con textos típicos del pattern
rg -n 'Debug\.(Print|Write)\s*\(\s*"(Espera|Aqui|Wait a second|Acá|Ojo|Mira)' /tmp/repos/TOMWMS_BOF/ --type vb
```

## Pista de autoría

`Debug.Print` es la API estándar de VB.NET para escribir al panel Output del IDE. `Debug.Write` es funcionalmente equivalente (sin newline). El uso casi exclusivo de `Debug.Print` en el repo sugiere una mano dominante (probablemente EJC). La aparición aislada de `Debug.Write` en CP-011 es **pista débil** de otra mano o de un copy-paste de otro contexto.

## Cross-refs

- `case-pointers/01-stockfecha-codigo-030772033524.md` — CP-001
- `case-pointers/09-frmregularizar-triple-hardcode-congelado.md` — CP-009 (variante multi-objeto del pattern)
- `case-pointers/10-stockres-codigo-00190454-aqui.md` — CP-010
- `case-pointers/11-stockres-codigo-00091035-espera.md` — CP-011
- `case-pointers/12-existencias-codigo-01008076-espera.md` — CP-012
- `brain/conventions/comments-firmados-EJC.md` — convención de autoría que se complementa con este pattern

# Handoff: fix packing parcial — VW IS NULL + pickSnap HH La Cumbre
**Fecha:** 2026-05-30
**Agente:** Replit Main Agent (EJC session)
**Repos afectados:** TOMWMS_BOF, TOMHH2025 (rama dev_2028_merge)
**Cliente:** La Cumbre

---

## Problema
Tras hacer packing parcial de una licencia (ej. 5 de 10 unidades), la pantalla
`frm_lista_packing_lp` mostraba "REGISTRO(S): 0" y el flujo quedaba bloqueado.

## Root cause
`VW_PickingUbic_By_IdPickingEnc` filtra `fecha_packing IS NULL`.
`Inserta_Packing` (BOF) escribía `fecha_packing = 1900-01-01` como sentinel "parcial",
pero `1900-01-01 IS NULL = FALSE` → ubic excluida del VIEW → WS devolvía 404b (vacío)
→ `pick.items = []` en HH → `creaListaLotes` sin fuente → 0 ítems en lista.

## Fixes aplicados

### BOF — TOMWMS_BOF commit `7c1b670f`
**Archivo:** `TOMIMSV4/DAL/Transacciones/Packing/clsLnTrans_packing_enc_Partial.vb`

En `Inserta_Packing`, bloque `For Each Picking In ListaPikcing`:
- **Completo** (`CantidadEmpacada >= Picking.Cantidad_Verificada`): igual que antes,
  `Picking.Fecha_packing = Date.Now()` → `Actualizar_FechaPacking(...)`.
- **Parcial** (Else): **NUEVO** — UPDATE inline con `fecha_packing = NULL`
  por `IdPickingUbic` vía `SqlCommand` en `Using`. No llama `Actualizar_FechaPacking`.

NULL es compatible tanto con `IS NULL` (VW) como con el branch `IS NULL` del OR en
`Get_All_PickingUbic_For_Packing` (`WHERE ... OR fecha_packing < '19010101'`).

### HH — TOMHH2025 commit `5194b334` (2 archivos)

**`frm_preparacion_packing.java`** — capa defensiva anti-vacío:
| Punto | Cambio |
|---|---|
| Declarations | `ArrayList<clsBeTrans_picking_ubic> pickSnap = new ArrayList<>()` |
| `processListUbic` | Guarda snapshot: `pickSnap = new ArrayList<>(pick.items)` cuando non-empty |
| `agregaLP` | Si `pick.items.isEmpty() && !pickSnap.isEmpty()`: repoblar `pick.items = new ArrayList<>(pickSnap)` antes del stream search → `indexOf` y `addItem` siguen funcionando |
| `creaListaLotes` | `sourceItems = !pick.items.isEmpty() ? pick.items : pickSnap`; for-each sobre `sourceItems` |
| `processActualizaEstado` | `pickSnap.clear()` antes de `finish()` al finalizar todo el packing |

**`frm_lista_packing_lp.java`** — UX:
- `lblPackingLicencia` → `"PENDIENTES DE PACKING"` (era "Licencia Packing: LP")
- `lblFiltro` siempre visible: `"LP: <lp>  <bodega>  <operador>"` (antes se ocultaba si no había filtro)
- `Log.d PKG_T` en `onCreate` y `listItems`

## Estado
- ✅ Ambos commits en ADO `dev_2028_merge`
- ⏳ Pendiente: despliegue TOMWMS_BOF a EC2 + build APK TOMHH2025 + prueba La Cumbre
- ⏳ Pendiente: verificar query VW_PickingUbic_By_IdPickingEnc en BD TOMWMS_KILLIOS_PRD
  para confirmar que el filtro es exactamente `IS NULL` (login BD falló en esta sesión)

## Regla actualizada (memory)
`VW_PickingUbic_By_IdPickingEnc` usa `IS NULL` — el sentinel 1900-01-01 NO aplica aquí.
`Get_All_PickingUbic_For_Packing` usa `< '19010101'` — el sentinel 1900 SÍ aplica.
Para packing parcial usar siempre `NULL`; para completo usar `Date.Now()`.

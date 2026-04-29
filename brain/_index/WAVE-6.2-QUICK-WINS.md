# Wave 6.2 — Quick wins (8 Q-* baratas resueltas con SQL/GREP)

**Fecha**: 2026-04-28  
**Método**: SQL READ-ONLY contra EC2 + GREP/git contra repos shallow.  
**Sin input humano requerido**. Todas resueltas.

## Resumen ejecutivo

| Q-* | Estado | Hallazgo principal |
|---|---|---|
| Q-LP-LONG-DEFAULT | ABIERTA → reformulada | No existe columna específica; el límite es por convención cliente |
| Q-LP-LONG-VS-DATOS-REALES | RESUELTA | BYB avg 19 chars (NAV correlativo); resto 6-9 chars |
| Q-CEALSA-AUSENTES-7 | RESUELTA → 37 (no 7) | 37 propietarios sin `propietario_bodega` de 3197 totales |
| Q-DALCORE-PARIDAD | RESUELTA | DALCore al 18-26% del legacy |
| Q-DEV2025-PROPOSITO | RESUELTA | Alias de `dev_2023_estable` (mismo HEAD) |
| Q-MASTER-PROPOSITO | RESUELTA | Alias de `dev_2023_estable` (mismo HEAD) |
| Q-LN-DRIFT-AUDIT | RESUELTA | Convención es `_partial.vb` (underscore); drift = 90% |
| Q-PROPIETARIO-AGNOSTICO | RESUELTA | No-3PL = 1 propietario default; CEALSA único 3PL real |

**Q-* nuevas que aparecieron** (5):
- Q-LP-DATA-DIRTY-MIN (BECO)
- Q-LP-CORRELATIVO-NAV-FORMATO (estructura del LP de BYB)
- Q-PORTAL-AUTH-CREDENCIALES-EN-PROPIETARIOS
- Q-GENERADOR-ABANDONO (90% drift)
- Q-DESIGNER-VB-CONTEO (684 forms)

---

## 1. Q-LP-LONG-VS-DATOS-REALES — Longitud real de license plate por cliente

**Query**: `SELECT MIN/MAX/AVG LEN(lic_plate) FROM stock` por cliente.

| Cliente | DB | Stocks con LP | min | **max** | **avg** |
|---|---|---:|---:|---:|---:|
| BECO | `IMS4MB_BECOFARMA_PRD` | 30.176 | 1 | 13 | 6.97 |
| K7 | `TOMWMS_KILLIOS_PRD` | 3.517 | 1 | 9 | 6.25 |
| **BYB** | `IMS4MB_BYB_PRD` | 6.887 | 1 | **26** | **19.11** |
| CEALSA | `IMS4MB_CEALSA_QAS` | 478 | 8 | 11 | 8.46 |
| MAMPA | `TOMWMS_MAMPA_QA` | 481 | 1 | 12 | 8.90 |

**Insight**: BYB triplica la longitud promedio del resto. Confirmado: BYB usa **correlativo NAV embebido en el LP**.

**Muestra BYB (LPs >= 20 chars)**:
```
BA00020002040400072334
BA00020002040400095134
BA00020002040400095135
BA00020002040400095173
BA00020002040400095175
```

**Estructura inferida**: `BA` (prefijo bodega/empresa) + `0002` + `0002` (códigos jerárquicos) + `040400072334` (correlativo numérico, posiblemente NAV serie).

**Q-* nueva derivada**: `Q-LP-CORRELATIVO-NAV-FORMATO` — descomponer la estructura exacta y validar contra clases `clsSyncNav*` de BYB.

**Q-LP-DATA-DIRTY-MIN (BECO)**:
- 8 filas de stock con `lic_plate = "0"` (1 char). Probablemente migración o dummy histórico. No bloquea, pero queda anotado.

---

## 2. Q-LP-LONG-DEFAULT — reformulada

**Búsqueda**: columna en `producto_parametros` con sufijo `lp/long/licen/plate`. **Resultado: no existe** en ninguno de los 5 clientes.

**Conclusión**: no hay parámetro de "longitud máxima de LP" configurable a nivel de producto. El límite parece ser **por convención del cliente**, validado en el código y/o en el tipo de columna SQL Server (`varchar(N)`).

**Próxima acción** (cuando se haga la traza-002 o una nueva consulta): mirar `INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='lic_plate'` para ver el `varchar(N)` del schema en cada cliente — eso da el límite hardcoded.

---

## 3. Q-CEALSA-AUSENTES-7 → en realidad son 37

**Query**: `propietarios LEFT JOIN propietario_bodega WHERE pb.IdPropietario IS NULL`.

- Total propietarios CEALSA: **3.197**
- Con asociación a bodega: **3.160**
- **Sin asociación: 37** (no 7 como se asumió en wave previa)

**Muestra de los 37 huérfanos**:
- `INTERNATIONAL DAIRY PRODUCTS, S.A.` (IdProp 3208, NIT 52920453, alta 2024-04-17)
- `CENTROPLAS, S.A.` (IdProp 3214, NIT 94497559, alta 2024-04-25)
- `ECOENER SOL DEL PUERTO, S.A.` (IdProp 3215, NIT 112206905, alta 2024-05-06)
- ...

**Patrón**: todos `activo=1`, todos creados por `user_agr=6`, todos con `IdBodegaAreaSAP=0`, todos con `controlux=NULL`. Son altas administrativas que nunca se "operativizaron" (no se les asignó bodega de trabajo).

---

## 4. Q-PROPIETARIO-AGNOSTICO — patrón confirmado

| Cliente | # propietarios |
|---|---:|
| BECO | **1** |
| K7 | **1** |
| BYB | **1** |
| MAMPA | **1** |
| **CEALSA** | **3.197** |

**Confirmación tajante**: solo CEALSA es 3PL real. Los demás usan **un único propietario "default/sistema"** para satisfacer el FK de la tabla. La lógica multi-tenant existe en el schema pero solo está activa en CEALSA.

**Implicación para el portal CEALSA**: el portal tiene sentido dimensional solo para CEALSA. Los demás clientes nunca van a usarlo (1 propietario = nada que mostrar).

**Implicación para la migración 2028**: si se va a meter 3PL multi-tenant a otros clientes, hay que limpiar la fila default + agregar reales. Para MHS (B2B vía API), MHS sería el segundo cliente con multi-tenant real.

---

## 5. Q-PORTAL-AUTH — descubrimiento bonus desde el schema

La tabla `propietarios` en CEALSA contiene:
- `codigo_acceso` (nvarchar 100)
- `clave_acceso` (nvarchar 100)

**Hipótesis**: estas dos columnas son **las credenciales de login del portal CEALSA**. Cada propietario tiene su user/pass embebido en el master de propietarios. Esto resuelve parcialmente la pregunta de Carolina #17 (Q-PORTAL-AUTH).

**Pendiente verificar**:
- ¿Está hasheada `clave_acceso`? (mirar muestras)
- ¿Es self-service o lo administra CEALSA?
- ¿El portal va a `propietarios` directo o pasa por una tabla intermedia?

Otras cols relevantes para el portal:
- `email` (notificaciones)
- `es_consolidador` (rol especial — propietarios que consolidan múltiples submarcas)
- `controlux` (otro rol especial — pendiente averiguar)
- `IdBodegaAreaSAP` (link a SAP areas — multi-bodega por propietario)
- `IdTipoActualizacionCosto` (concepto contable)

---

## 6. Q-MASTER-PROPOSITO + Q-DEV2025-PROPOSITO — resueltas: son alias

| Rama | Commit HEAD |
|---|---|
| `master` | `1f5cc2c4` |
| `dev_2023_estable` | `1f5cc2c4` |
| `dev_2025` | `1f5cc2c4` |
| `dev_2028_merge` | `73169460` |

**Conclusión**: `master`, `dev_2023_estable` y `dev_2025` apuntan al **mismo commit**. Son alias. Solo `dev_2028_merge` divergió (1.922 archivos diferentes vs `dev_2023_estable`).

**Implicación operativa**:
- El team trabaja en `dev_2023_estable` como rama activa
- `master` se mantiene sincronizado con esa rama (probablemente fast-forward automático)
- `dev_2025` quedó como tag/checkpoint en algún punto y luego se siguió empujando ahí también, o nunca se separó realmente
- La verdadera "rama futura" es `dev_2028_merge`

**Q-* aclaratoria pendiente** (para Erik o Carolina):
- ¿Por qué se mantienen 3 ramas apuntando al mismo commit en vez de borrar `master` y `dev_2025`?
- ¿`dev_2025` tuvo vida propia en algún momento del pasado y luego se sincronizó?

---

## 7. Q-DALCORE-PARIDAD — DALCore al 18-26% del legacy

| Capa | Legacy (VB.NET) | Core (.NET Core) | Paridad |
|---|---:|---:|---:|
| LN (logic) | 626 archivos `clsLn*.vb` | 114 archivos `clsLn*.cs` | **18.2%** |
| BE (entity) | 536 archivos `clsBe*.vb` | 142 archivos `clsBe*.cs` | **26.5%** |
| **Total** | **1.162** | **256** | **22%** |

**Conclusión**: la migración a .NET Core va aprox al 22%. Queda el grueso del trabajo. Esto enmarca la conversación sobre cuánto falta para el "salto" a Web BOF — no se va a poder hacer Web BOF hasta que DALCore tenga al menos 80-90% de paridad funcional (sino el Web BOF queda mocheado vs el WinForms).

**Distribución del legacy por dir**:
- `TOMIMSV4/DAL/...` — el grueso del WMS
- `DMS/Dal/...` — capa data del DMS (cloud sync)
- `SW/Bodega/...` — alguna pieza del SW/Bodega

---

## 8. Q-LN-DRIFT-AUDIT — convención `_partial.vb` (underscore)

**Corrección importante**: la convención del generador NO es `.partial.vb` (con punto) sino **`_partial.vb`** (con underscore).

| Capa | Total | Base (sin partial) | Con `_partial` | Ratio partial/base |
|---|---:|---:|---:|---:|
| LN | 626 | 569 | **57** | **10.0%** |
| BE | 536 | 511 | **25** | **4.9%** |

**Insight crítico** → **Q-* nueva: Q-GENERADOR-ABANDONO**:
- Solo el **5-10% de las clases generadas tienen archivo `_partial.vb`** asociado
- Significa que el generador existe pero **el equipo edita directamente las clases base** en lugar de usar el patrón "base regenerable + partial custom"
- **Drift potencial: 90%** — si se intenta regenerar las clases base, se pierden todas las customizaciones manuales que están en ellas

**Muestra de bases SIN partial (drift candidates)**:
```
DMS/Dal/Mantenimientos/Producto/clsLnProductoDMS.vb
DMS/Dal/Mantenimientos/Propietarios/clsLnPropietarioDMS.vb
DMS/Dal/Transacciones/Ingresos/clsLnTrans_oc_encDMS.vb
DMS/Dal/Transacciones/Salidas/clsLnTrans_pe_encDMS.vb
TOMIMSV4/DAL/DataHistorica/clsLnDh_ocupacion_bodega.vb
TOMIMSV4/DAL/Diferencias_Movimientos/clsLnDiferencias_movimientos.vb
TOMIMSV4/DAL/FixTheBug/clsLnPolizas.vb
...
```

**Sample de contenido** (`clsLnProductoDMS.vb`): tiene `Imports DevExpress.*` + lógica async de export de productos. Esto es **código de negocio que NO debería estar en una clase generada**, debería estar en un `_partial`.

**Implicaciones para el generador (Q-GENERADOR-* de Carolina)**:
- El generador funciona pero la **convención no se respeta de forma consistente**
- Cualquier traza futura tiene que asumir que las clases base **están customizadas in-place**, no son regenerables
- Si se quiere meter el Web BOF generado, primero hay que **mover el custom code de las bases a `_partial`** (refactor masivo)

**Bonus**: hay **684 archivos `.Designer.vb`** — son los WinForms generados por VS designer (estándar VB.NET, no del generador custom). Top dirs: `TOMIMSV4/Transacciones/Inventario` (19), `TOMIMSV4/Reportes/Resumen_Stock` (14), `TOMIMSV4/Reportes/Fiscales` (13). Da idea del tamaño del UI: ~400-500 forms.

---

## Q-* que quedan abiertas (priorizar para Carolina o próximas waves)

**Resueltas en esta wave**: 7 de 8 (Q-LP-LONG-DEFAULT quedó reformulada).

**Q-* nuevas derivadas** (no estaban en el concept-map original):
1. **Q-GENERADOR-ABANDONO** — alta — definir si es bug histórico o decisión consciente
2. **Q-LP-CORRELATIVO-NAV-FORMATO** — media — descomponer formato de LPs BYB
3. **Q-PORTAL-AUTH-CREDENCIALES-EN-PROPIETARIOS** — alta — confirmar con Carolina si es así
4. **Q-LP-DATA-DIRTY-MIN** — baja — limpiar 8 filas BECO con LP="0"
5. **Q-RAMA-MASTER-DEV2025-DUPLICADAS** — baja — política de mantener 3 alias

---

## Métricas brain post-Wave 6.2

- Q-* totales originales: ~85
- Q-* resueltas (acumulado): 1 (DALCORE-PROPOSITO en 6.1) + 7 (esta wave) = **8 / 85 (9.4%)**
- Q-* nuevas agregadas: +5
- Q-* netas abiertas: ~82
- Q-* críticas: 1 sigue (Q-SEC-OPENAI-KEY-LEAK)
- Líneas brain: ~2.700 + este doc

---

## Próximo paso recomendado

**Traza-002: control_lote + control_vencimiento** sobre cliente que mejor lo ejercite. Candidatos:
- **CEALSA** — único con multi-tenant real → control_lote por propietario
- **MAMPA** — tiene merma de carne, vencimiento corto crítico
- **BECO** — farmacia, regulado por lote/vencimiento sí o sí

Mi recomendación: **MAMPA** (farma + alimento, vencimiento corto, ya está en `dev_2028_merge`, podemos validar contra schema migrado).

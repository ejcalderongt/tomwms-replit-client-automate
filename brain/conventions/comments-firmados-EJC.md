# C-001 — Comments firmados `'#EJC<YYYYMMDD>[_REF<NN>_<HHMM><AM/PM>]: <body>`

> Convención personal de Erik Calderón para dejar **bitácora datada inline** en el código fuente. Cada cambio significativo se firma con un comment VB del estilo `'#EJC20210830: ...`. Aparece **3270 veces** en `TOMWMS_BOF`.

## Definición formal

Un comment firmado válido sigue el formato:

```
'#EJC<YYYYMMDD>[<sufijo>]: <body>
```

Donde:

- `'` es el caracter de comment de VB.NET (apóstrofe).
- `#EJC` es el prefijo identificador de autoría.
- `<YYYYMMDD>` es la fecha en formato compacto (8 dígitos).
- `<sufijo>` es opcional y puede ser:
  - `_REF<NN>` (número de refactor del día)
  - `_<HHMM><AM|PM>` (hora del cambio)
  - `_<HHMM><AM|PM>_REF<NN>` (combinación)
  - Ninguno (solo fecha)
- `:` separador.
- `<body>` texto libre del cambio.

## Ejemplos reales del repo

```vb
'#EJC20171108_REF05_0643PM: Refactoring structure, se agrego función validación de llave dentro de try
'#EJC20171108_REF02_0605PM: Refactoring clsBeLicencia_llave
'#EJC20180106: Cambie el valor BuscarMac = True si no se encuentra por nombre de HostName
'#EJC20180508: Refactoring
'#EJC20210715: Buscar columnas por nombre en grid antes de colocar visible/invisible.
'#EJC20210830: Validación de Picking asociado al stock reservado antes de liberarlo y también liberarlo de los procesos de picking.
'#EJC20230821: Buscar con 1 día de desfase hacia adelante.
'#EJC20250424: aqui se obtiene el lote del archivo de texto
```

## Cobertura medida

Conteo total de ocurrencias `'#EJC[0-9]{8}` en `/tmp/repos/TOMWMS_BOF/` (búsqueda `rg`):

**Total: 3270 comments firmados.**

### Top 10 archivos con más firmas

| Archivo | Comments firmados |
|---|---|
| `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` | 343 |
| `TOMIMSV4/DAL/Transacciones/Stock/clsLnStock_Partial.vb` | 160 |
| `TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb` | 92 |
| `TOMIMSV4/Transacciones/Pedido/frmPedido.vb` | 86 |
| `TOMIMSV4/TOMIMSV4/Transacciones/Pedido/frmPedido.vb` | 86 |
| `DynamicsNavInterface/Clases Interface Sync/Envios_Almacen/clsSyncNavEnvioAlm.vb` | 72 |
| `TOMIMSV4/Transacciones/Recepcion/frmRecepcion.vb` | 64 |
| `TOMIMSV4/TOMIMSV4/Transacciones/Recepcion/frmRecepcion.vb` | 63 |
| `TOMIMSV4/Transacciones/Orden_Compra/frmOrdenCompra.vb` | 60 |
| `TOMIMSV4/TOMIMSV4/Transacciones/Orden_Compra/frmOrdenCompra.vb` | 58 |

(El archivo más editado es `clsLnStock_res_Partial.vb`, el "corazón monstruo" de la DAL de stock reservado.)

### Distribución temporal

Las fechas observadas van desde `20171031` hasta `20250424` — **8+ años de bitácora continua**.

## Valor para el equipo

1. **Trazabilidad sin git**: aún en archivos sin historia de control de versiones limpia (Visual SourceSafe legacy, archivos `vctmp50560_*` huérfanos), las firmas dan un registro inline de cuándo se cambió cada bloque.

2. **Identificación de autoría**: la firma `EJC` permite distinguir cambios de Erik vs cambios de otros (que mayoritariamente no firman). Cuando aparece código sin firma cerca de código firmado, es **pista de otra mano**.

3. **Recuperación de contexto**: leer las firmas de los últimos N años en un archivo da una "línea de tiempo" del razonamiento que llevó al estado actual.

4. **Compatibilidad con la herramienta `scan-comments-tree-map`**: la convención fue formalizada como expectativa de scan en wave 9. Cualquier herramienta de auditoría puede parsearla.

## Diferencias con otros prefijos

- `'#EJC...:` — firma de cambio (esta convention)
- `EJC_<TIMESTAMP>_<DESC>:` (sin apóstrofe) — label GoTo (candidato C-002, ver CP-010)
- `"#EJCAJUSTEDESFASE"` — string asignado a `Serie` para marcar mutaciones del `ModoDepuracion` (CP-007 / CP-008)

Las tres convenciones comparten las iniciales `EJC` pero **no son la misma cosa**:

| Forma | Lugar | Función | Documento |
|---|---|---|---|
| `'#EJC<DATE>:` | comment | bitácora inline de cambio | esta convention C-001 |
| `EJC_<TS>_<DESC>:` | label de bloque GoTo | firma de bloque/región | candidato C-002 |
| `"#EJCAJUSTEDESFASE"` | string literal asignado a campo | marcar dato mutado en BD | case-pointer CP-007 / CP-008 |

## Riesgo / antipatrón asociado

La convention en sí es valiosa, pero hay un riesgo: comments firmados pueden volverse **mentirosos** cuando el código adyacente se modifica sin actualizar el comment. Recomendación:

- Si modificás código firmado, **agregá una nueva firma** con la fecha actual en vez de modificar la firma vieja.
- Si la modificación contradice la firma vieja, dejá ambas para mostrar la evolución.
- Nunca borres una firma vieja silenciosamente.

## Pendientes / propuestas

- [ ] Formalizar como expectativa documentada para futuros contribuidores (incluido el agente del brain).
- [ ] Considerar agregar una firma análoga `'#JP<DATE>:` o similar para JP cuando se confirme su autoría (CP-002).
- [ ] Considerar lint check: warning si un cambio en archivo firmado no agrega nueva firma.

## Cross-refs

- `dataway-analysis/07-correlacion-codigo-data/case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md` — pattern P-001 que se complementa con esta convention
- `brain/scan-comments-tree-map/` — herramienta complementaria (Wave 9 followup)
- `brain/debuged-cases/CP-002.md` — caso JP que motivaría una convention paralela `'#JP...`

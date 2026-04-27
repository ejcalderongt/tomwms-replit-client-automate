# Familia legacy `clsLnStock_res.Ejecuta_QA_CASO_*`

> Inventario y protocolo de migración de los **20 casos canónicos** de
> prueba de reservas escritos en VB.NET dentro del BOF, en la clase
> `Partial Public Class clsLnStock_res`.
>
> Estos casos fueron validados manualmente por Erik a lo largo del
> tiempo. Son **fuente de verdad** del comportamiento esperado del
> motor de reservas (legacy y MI3 nuevo).

Skill que opera: `wms-brain/skills/wms-test-bridge/SKILL.md`.
ADR: `wms-brain/decisions/004-bridge-wms-test-automation.md`.

---

## 1. Por qué nos importan

El motor de reservas es el módulo más complejo del WMS. Combina:
- FEFO (First Expired, First Out).
- Zona de picking vs almacenamiento.
- Paquete completo vs fragmentación.
- Explosión automática a nivel máximo.
- Control de lote y vencimiento.
- Múltiples tipos de pedido (cliente, traslado interno, manufactura).
- Múltiples handlers según config (`i_nav_config_enc`).

Los 20 `Ejecuta_QA_CASO_*` son la **biblioteca de variantes** que Erik
fue construyendo a medida que descubría comportamientos esperados.
Releerlos ayuda a:

1. **Entender las variantes**: cada caso aisla una capacidad concreta.
2. **Calibrar el catálogo nuevo**: cada YAML del catálogo en
   `test-scenarios/reservation/RES-NNN-*.yaml` debe mapearse a uno o
   varios casos legacy.
3. **No dejar capacidades sin cubrir**: si los 20 casos cubrieron
   regresiones históricas, el catálogo nuevo debe cubrir las mismas
   regresiones.
4. **Migración del legacy al MI3 nuevo**: los mismos casos deben
   pasar contra ambos motores. Si MI3 nuevo da resultado distinto, hay
   que decidir si es mejora o regresión.

---

## 2. Inventario (estado actual del conocimiento)

| ID nuevo | Caso legacy | Título | Ya documentado | Mapeo a YAML nuevo |
|---|---|---|---|---|
| RES-LEG-01 | `Ejecuta_QA_CASO_1_IDEAL_20231002011101` | FEFO almacenaje vs picking, vencimiento | sí ([CASO-01](./CASO-01-IDEAL_20231002011101.md)) | RES-001 |
| RES-LEG-02 | `Ejecuta_QA_CASO_2_*` | (pendiente leer) | no | TODO |
| RES-LEG-03 | `Ejecuta_QA_CASO_3_*` | (pendiente leer) | no | TODO |
| RES-LEG-04 | `Ejecuta_QA_CASO_4_*` | (pendiente leer) | no | TODO |
| RES-LEG-05 | `Ejecuta_QA_CASO_5_*` | (pendiente leer) | no | TODO |
| RES-LEG-06 | `Ejecuta_QA_CASO_6_*` | (pendiente leer) | no | TODO |
| RES-LEG-07 | `Ejecuta_QA_CASO_7_*` | (pendiente leer) | no | TODO |
| RES-LEG-08 | `Ejecuta_QA_CASO_8_*` | (pendiente leer) | no | TODO |
| RES-LEG-09 | `Ejecuta_QA_CASO_9_*` | (pendiente leer) | no | TODO |
| RES-LEG-10 | `Ejecuta_QA_CASO_10_*` | (pendiente leer) | no | TODO |
| RES-LEG-11 | `Ejecuta_QA_CASO_11_*` | (pendiente leer) | no | TODO |
| RES-LEG-12 | `Ejecuta_QA_CASO_12_*` | (pendiente leer) | no | TODO |
| RES-LEG-13 | `Ejecuta_QA_CASO_13_*` | (pendiente leer) | no | TODO |
| RES-LEG-14 | `Ejecuta_QA_CASO_14_*` | (pendiente leer) | no | TODO |
| RES-LEG-15 | `Ejecuta_QA_CASO_15_*` | (pendiente leer) | no | TODO |
| RES-LEG-16 | `Ejecuta_QA_CASO_16_*` | (pendiente leer) | no | TODO |
| RES-LEG-17 | `Ejecuta_QA_CASO_17_*` | (pendiente leer) | no | TODO |
| RES-LEG-18 | `Ejecuta_QA_CASO_18_*` | (pendiente leer) | no | TODO |
| RES-LEG-19 | `Ejecuta_QA_CASO_19_*` | (pendiente leer) | no | TODO |
| RES-LEG-20 | `Ejecuta_QA_CASO_20_*` | (pendiente leer) | no | TODO |

**Bloqueante actual**: solo tenemos acceso al CASO 1 vía adjunto. Para
documentar 2..20 necesitamos:
1. (preferido) Acceso al archivo `clsLnStock_res.vb` completo desde el
   repo Azure DevOps `ejcalderon0892/TOMWMS_BOF` rama `dev_2028_merge`.
2. (alternativa) Que Erik adjunte cada caso en chat.
3. (alternativa) Acceso vía controller `/api/test-bridge/inspect/code`
   en el futuro (idea: un endpoint que retorne snippets del código
   fuente para que Brain los procese — requiere ADR aparte).

---

## 3. Estructura común de un `Ejecuta_QA_CASO_N_*`

A partir del CASO 1 ya leído, el patrón es:

```vb.net
Public Shared Function Ejecuta_QA_CASO_N_NOMBRE(
    ByVal IdBodega As Integer,
    ByVal IdUsuario As Integer,
    ByRef Resultado As String) As Boolean

    ' 1) Limpiar BD
    Clear_BD_For_Test_Cases()

    ' 2) Preparar inventario:
    '    - clsLnBodega.GetSingle_By_Idbodega
    '    - clsLnProducto.Get_Single_By_Codigo("47022" o el del caso)
    '    - clsLnBodega_ubicacion.Get_Single_Ubicacion_No_Picking / _Picking
    '    - clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario
    '    - clsLnPropietario_bodega.Get_IdPropietarioBodega_*
    '    - clsLnCliente.Get_Cliente_Defecto_Pruebas
    '
    '    Insertar 1..N filas en stock con:
    '    - clsBeStock con IdBodega, IdProductoBodega, IdPropietarioBodega
    '    - IdProductoEstado, Presentacion, IdUnidadMedida
    '    - IdUbicacion (picking o no-picking según caso)
    '    - Lote = "CASO_USOX_MI3_AUTOMATE"
    '    - Fecha_vence = New Date(...)
    '    - Cantidad = N * Factor de presentación
    '    - clsLnStock.Insertar(...)

    ' 3) Armar pedido:
    '    - clsBeI_nav_ped_traslado_enc con Document_Type=Pedido_De_Cliente
    '    - Product_Owner_Code = "01"
    '    - Lineas_Detalle con clsBeI_nav_ped_traslado_det

    ' 4) Disparar flujo:
    '    - clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia
    '    - Internamente esto llama al motor de reservas

    ' 5) Resultado vía RichTextBox (texto descriptivo)
End Function
```

**Insumos universales**:
- Producto sentinel: `47022` (al menos en CASO 1).
- Propietario: el de `i_nav_config_enc.IdPropietario` (típicamente 1).
- Cliente: el de `clsLnCliente.Get_Cliente_Defecto_Pruebas`.
- Bodega y usuario: parámetros del caso.

**Variables entre casos**: cantidad de stocks insertados, ubicaciones
(picking vs no-picking), lotes, vencimientos, presentación enviada,
cantidad pedida.

---

## 4. Protocolo de migración legacy → catálogo nuevo

Cuando se documenta un caso legacy:

1. Crear `CASO-NN-NOMBRE.md` en este directorio con:
   - Resumen del summary VB.NET del caso.
   - Setup detallado (qué stocks se siembran).
   - Acción (qué pedido se envía).
   - Resultado esperado (qué debe haber en `stock_res`,
     `trans_pe_*`, etc.).
   - Snippet del código original.
2. Decidir el mapeo:
   - Si calza con un escenario nuevo existente
     (`RES-001..005`), agregar `legacy_ref:` en ese YAML.
   - Si no calza, crear un escenario nuevo
     (`RES-006`, `RES-007`, ...) que lo represente.
3. Marcar la fila correspondiente en la tabla de §2 con el mapeo.
4. Actualizar `test-scenarios/README.md` si hay nuevo escenario.

---

## 5. Reglas de la familia legacy

1. **Los 20 casos son ground truth**. Cuando un escenario nuevo da
   resultado distinto al CASO legacy, **el legacy gana** salvo que
   Erik decida lo contrario explícitamente.
2. **No reescribir, traducir**. Los YAMLs nuevos son traducción
   declarativa, no reinterpretación. Mismas precondiciones, mismas
   acciones, mismas assertions.
3. **Conservar IDs y constantes**. Producto `47022`, lote
   `CASO_USOX_MI3_AUTOMATE`, fechas exactas. Si el cliente target
   no tiene esos datos, el placeholder los reemplaza pero el caso
   legacy queda como referencia inmutable.
4. **Cada CASO documentado es un PR independiente**. Para que el
   review sea trazable.

---

## 6. Próximos pasos

- Conseguir acceso a `clsLnStock_res.vb` completo (rama
  `dev_2028_merge` del repo BOF en Azure DevOps).
- Documentar casos 2..20 (uno por archivo `CASO-NN-*.md`).
- Mapear cada CASO a un YAML del catálogo nuevo (crear nuevos
  escenarios `RES-006+` si hace falta).
- Una vez completos los 20 casos, hacer una pasada de cobertura:
  ¿hay flags de `i_nav_config_enc` o de los 88 bits que NO están
  cubiertos por ningún caso legacy ni nuevo? Eso son gaps a cubrir.

---

## 7. Referencias

- ADR: `wms-brain/decisions/004-bridge-wms-test-automation.md`.
- Skill: `wms-brain/skills/wms-test-bridge/SKILL.md`.
- Catálogo nuevo: `wms-brain/test-scenarios/README.md`.
- Módulo flagship: `wms-brain/entities/modules/reservation/README.md`.
- DDL: `wms-brain/sql-catalog/reservation-tables.md`.
- Repo BOF Azure DevOps: `ejcalderon0892/TOMWMS_BOF`, rama `dev_2028_merge`.

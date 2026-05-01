---
id: CASO-DINAMICO
tipo: test-scenario
estado: vigente
titulo: Legacy CASO Dinamico — Caso dinámico (input desde clsBeConfiguracion_qa)
ramas: [dev_2028_merge]
modulo: [reservation]
tags: [test-scenario, modulo/reservation]
---

# Legacy CASO Dinamico — Caso dinámico (input desde clsBeConfiguracion_qa)

> Origen: `TOMWMS_BOF` rama `dev_2028_merge` archivo `/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` funcion `Ejecuta_QA_CASO_Dinamico` líneas 17232-17372.

## Mapeo a escenario canónico

| Campo | Valor |
|---|---|
| Escenario nuevo | `RES-DIN` |
| Categoría | `dinamico` |
| Cliente origen del caso | `CUALQUIERA` |
| Vigente para | (ver `brain/test-scenarios/_matrix/compatibility.md`) |

## Resumen del caso (extraído del summary VB.NET)

> _(sin summary humano en el código fuente)_

## Setup (datos embebidos)

| Campo | Valor |
|---|---|
| Producto | `47022` |
| Lotes | `-` |
| Fechas `New Date(y,m,d)` | - |
| Tipo documento | `Pedido_De_Cliente` |
| Owner / Propietario | `01` |
| Cantidad solicitada | _(ver código)_ |
| Flags BeConfig accedidos | - |
| Función orquestadora | `clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia` |

## Acción

Ejecutar `Ejecuta_QA_CASO_Dinamico(IdBodega, IdUsuario, ByRef Resultado)`.

## Expected (del summary)

- _(sin summary humano)_

## Notas para portar al bridge

1. **Producto y owner están hardcoded** en el legacy (`47022` y `"01"`). El escenario canónico debe parametrizarlos.
2. **El lote** sigue patrón `CASO_USOnn_MI3_AUTOMATE`. En el bridge el lote también debe ser parámetro del escenario.
3. **Fechas relativas**: este caso usa fechas absolutas (...). El bridge debe permitir expresarlas como `hoy + N días` para que no caduquen.
4. **Flag de rechazo por incompleto** y demás banderas se leen desde `clsLnConfiguracion_qa.Obtiene_Configuracion(IdBodega)`. En el bridge esto se mapea a `clients/<slug>/flags.yaml`.

## Referencia

- Función VB: [`/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` líneas 17232-17372](https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF?path=/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb&version=GBdev_2028_merge)

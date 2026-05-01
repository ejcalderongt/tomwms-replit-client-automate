---
id: README
tipo: test-scenario
estado: vigente
titulo: "Inventario `clsLnStock_res` legacy"
ramas: [dev_2028_merge]
modulo: [reservation]
tags: [test-scenario, modulo/reservation]
---

# Inventario `clsLnStock_res` legacy

Archivo fuente único:
[`/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`](https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF?path=/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb&version=GBdev_2028_merge)
en repo `TOMWMS_BOF`, rama `dev_2028_merge`.

> Tamaño: ~2.2 MB / 34381 líneas. Las funciones QA empiezan en la línea 13119.

## Funciones detectadas

Total: **21 funciones** (17 IDEAL + 1 Dinámico + 3 BYB).

| # | Función | Líneas | Cliente origen | Bloque temático | Mapeo nuevo |
|---|---|---:|---|---|---|
| 1 | `Ejecuta_QA_CASO_1_IDEAL_20231002011101` | 13119–13347 (229) | IDEAL | FEFO básico | `RES-001` |
| 2 | `Ejecuta_QA_CASO_2_IDEAL_20231002011120` | 13348–13577 (230) | IDEAL | FEFO ALM corto + completar picking | `RES-006` |
| 3 | `Ejecuta_QA_CASO_3_IDEAL_20231002011128` | 13578–13806 (229) | IDEAL | FEFO ALM corto + completar picking (var.) | `RES-007` |
| 4 | `Ejecuta_QA_CASO_4_IDEAL_20231002011132` | 13807–14043 (237) | IDEAL | Zigzag ALM-PICK-ALM | `RES-008` |
| 5 | `Ejecuta_QA_CASO_5_IDEAL_20231002011134` | 14044–14285 (242) | IDEAL | Zigzag PICK-ALM-PICK | `RES-009` |
| 6 | `Ejecuta_QA_CASO_6_IDEAL_20231002011136` | 14286–14511 (226) | IDEAL | Zigzag (variante 6) | `RES-010` |
| 7 | `Ejecuta_QA_CASO_7_IDEAL_20231002011140` | 14512–14730 (219) | IDEAL | Sin picking, sólo ALM | `RES-011` |
| 8 | `Ejecuta_QA_CASO_8_IDEAL_20231002011140` | 14731–14949 (219) | IDEAL | Sin picking + ALM insuf. → rechazo | `RES-012` |
| 9 | `Ejecuta_QA_CASO_9_IDEAL_20231002011144` | 14950–15178 (229) | IDEAL | Explosión desde caja en picking | `RES-013` |
| 10 | `Ejecuta_QA_CASO_10_IDEAL_20231002011146` | 15179–15407 (229) | IDEAL | Explosión + faltante → rechazo | `RES-014` |
| 11 | `Ejecuta_QA_CASO_11_IDEAL_20231002011153` | 15408–15650 (243) | IDEAL | Múltiples ALM más cortas | `RES-015` |
| 12 | `Ejecuta_QA_CASO_12_IDEAL_20231002011159` | 15651–15893 (243) | IDEAL | Múltiples ALM (variante 12) | `RES-016` |
| 13 | `Ejecuta_QA_CASO_13_IDEAL_20231002011201` | 15894–16139 (246) | IDEAL | Sin picking explosionable → rechazo | `RES-017` |
| 14 | `Ejecuta_QA_CASO_14_IDEAL_20231002011201` | 16140–16388 (249) | IDEAL | Picking corto + complementar sin re-explosionar | `RES-018` |
| 15 | `Ejecuta_QA_CASO_15_IDEAL_20231018130000` | 16389–16660 (272) | IDEAL | Mezcla UDS+CJS, FEFO estricto | `RES-019` |
| 16 | `Ejecuta_QA_CASO_16_IDEAL_202310200156` | 16661–16940 (280) | IDEAL | Mezcla UDS+CJS + explosión final | `RES-020` |
| 17 | `Ejecuta_QA_CASO_17_IDEAL_202311040904` | 16941–17231 (291) | IDEAL | Empate de fecha → priorizar UDS | `RES-021` |
| D | `Ejecuta_QA_CASO_Dinamico` | 17232–17372 (141) | _(parametrizado)_ | Caso dinámico desde `clsBeConfiguracion_qa` | `RES-DIN` |
| 18 | `Ejecuta_QA_CASO_18_BYB_202311141034` | 17373–17614 (242) | BYB | BYB - variante 18 | `RES-022` |
| 19 | `Ejecuta_QA_CASO_19_BYB_202311162103` | 17615–17874 (260) | BYB | BYB - variante 19 | `RES-023` |
| 20 | `Ejecuta_QA_CASO_20_BYB_202311171219` | 17875–18102 (228) | BYB | BYB - variante 20 (incluye 1900-01-01) | `RES-024` |

## Constantes hardcoded en TODOS los casos

Estas constantes están repetidas en los 21 casos y son el primer **olor a refactor** que el bridge resuelve parametrizando:

| Campo | Valor en legacy | En bridge |
|---|---|---|
| `vCodigoProducto` | `"47022"` | parámetro de `RES-NNN.input.product` |
| `Product_Owner_Code` | `"01"` | parámetro de `RES-NNN.input.owner` |
| `Document_Type` | `Pedido_De_Cliente` | parámetro de `RES-NNN.input.doc_type` |
| Patrón de lote | `CASO_USOnn_MI3_AUTOMATE` | parámetro de `RES-NNN.input.lote_pattern` |
| Función orquestadora | `clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia` | endpoint `POST /api/test-bridge/import-pedido-cliente` |

## Flags de configuración (`BeConfig.*`) que el legacy lee

Lista parcial detectada por el extractor (los primeros que aparecen en cada función):

- `IdProductoEstado` — qué estado considerar disponible
- `IdPropietario` — propietario por defecto
- `Reservar_Desde_Picking_Antes_Que_Almacenamiento` — invierte FEFO si no, pero gana fecha
- `Rechazar_Pedido_Si_Esta_Incompleto` — disparador de los CASOs 8, 10, 13
- `Permite_Explosion_En_Picking` — disparador de los CASOs 9, 13, 14
- `Considerar_Inventario_En_Otra_Bodega` — alcance de búsqueda

> **Nota**: este listado se completa al ejecutar `wmsa learn-config <cliente>` contra una BD real del cliente y leer `clsLnConfiguracion_qa.Obtiene_Configuracion(IdBodega)`.

## Cómo se relaciona con la categorización por cliente

Cada caso queda mapeado a un escenario `RES-NNN` que declara qué configuración necesita. Después, en `brain/test-scenarios/_matrix/`, cruzamos esa lista con la configuración real conocida de cada cliente para ver dónde aplica.

Ejemplo:

> **CASO 8** (`RES-012`) requiere `Rechazar_Pedido_Si_Esta_Incompleto = true`. Si IDEALSA y KILLIOS lo tienen activo pero LA CUMBRE no, el escenario `RES-012` aplica para IDEALSA y KILLIOS, y se marca como `not_applicable` para LA CUMBRE (no es un fallo del cliente, es que no aplica).

Detalle: ver [`brain/test-scenarios/_matrix/README.md`](../_matrix/README.md).

## Stubs por caso

Cada caso tiene su propio archivo MD en este directorio con el summary, los datos embebidos extraídos y notas de portado:

- `CASO-01-1_IDEAL_20231002011101.md` ← documentado en detalle previamente
- `CASO-02-2_IDEAL_20231002011120.md` ... `CASO-17-17_IDEAL_202311040904.md`
- `CASO-DINAMICO.md`
- `CASO-18-18_BYB_202311141034.md` ... `CASO-20-20_BYB_202311171219.md`

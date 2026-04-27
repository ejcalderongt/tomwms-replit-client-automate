# Configuración de Interface — Flags relevantes para reservas

> Fuente legacy: `clsBeI_nav_config_enc.vb` (entidad), `clsLnI_nav_config_enc_Partial.vb` (DAL).
> Tabla SQL: `i_nav_config_enc` | Vista canónica: `VW_Configuracioninv`.
> Filtro estándar: `WHERE idEmpresa = 1 AND idBodega = @idBodega`.

Esta tabla es **por bodega** (no por cliente). En la práctica un "cliente" en nuestra
matriz se mapea a una o varias bodegas que comparten flags.

## Flags que afectan reservas (los relevantes para los CASOs legacy)

| Flag | Tipo | Default | Significado |
|---|---|---|---|
| `Rechazar_pedido_incompleto` | enum `tRechazarPedidoIncompleto` | `No` (0) | **TRAMPA SEMÁNTICA**: `No` (0) = rechaza el pedido completo cuando alguna línea no se puede abastecer. `Si` (1) = procesa las demás líneas y da por válido el pedido. El nombre dice lo opuesto a lo que pasa. |
| `Despachar_existencia_parcial` | enum `tDespacharExistenciaParcial` | `No` (0) | `No` = rechaza la línea entera si no hay stock completo. `Si` = despacha la cantidad disponible. |
| `Convertir_decimales_a_umbas` | Integer | `0` | Si es `> 0`, convierte cantidades fraccionarias en presentación a UMBas equivalentes. |
| `Reservar_UMBas_Primero` | Boolean | `False` | Si `True`, agota UMBas antes de tocar presentaciones. Si `False`, prioriza presentación según orden estándar. |
| `Implosion_Automatica` | Boolean | `False` | Permite agrupar UMBas residuales en presentación si suman una caja completa. |
| `Explosion_Automatica` | Boolean | `False` | Permite romper una presentación (caja) en UMBas durante la reserva. CASOs 9, 10, 13, 14, 15, 16. |
| `Explosion_Automatica_Desde_Ubicacion_Picking` | Boolean | `True` | Restringe la explosión a ubicaciones marcadas como picking. Si `False`, también explota desde almacenamiento. |
| `Explosion_Automatica_Nivel_Max` | Integer | `1` | Niveles máximos de explosión recursiva (caja → fardo → unidad). |
| `IdTipoRotacion` | enum `tTipoRotacion` | `FEFO` (3) | `FIFO`=1, `LIFO`=2, `FEFO`=3. Determina orden de ranking de stock. Todos los CASOs legacy asumen FEFO. |
| `IdProductoEstado` | Integer | `0` | Si `> 0`, filtra stock por estado (apto/cuarentena/etc.). |
| `Conservar_Zona_Picking_Clavaud` | Boolean | `False` | Reserva primero del picking aunque venza después (heurística cliente Clavaud). Anti-FEFO. |
| `Excluir_Ubicaciones_Reabasto` | Boolean | `False` | Excluye ubicaciones marcadas para reabasto del pool reservable. |
| `considerar_paletizado_en_reabasto` | Boolean | `False` | Suma stock paletizado al cálculo de reabasto. |
| `Considerar_Disponibilidad_Ubicacion_Reabasto` | Boolean | `False` | Verifica capacidad de la ubicación destino antes de reservar. |
| `Valida_Solo_Codigo` | Boolean | `False` | Reserva por código de producto sin validar otras dimensiones (lote/vence/estado). |
| `Control_lote` | Boolean | `False` | Si `True`, lote es dimensión obligatoria de reserva. |
| `Control_vencimiento` | Boolean | `False` | Si `True`, vencimiento es dimensión obligatoria. **Habilita FEFO real.** |
| `Genera_lp` | Boolean | `False` | Genera license plate al confirmar reserva. |

## Enums

```vb
Public Enum tRechazarPedidoIncompleto
    No = 0   ' Rechaza el pedido completo
    Si = 1   ' Procesa las demás líneas
End Enum

Public Enum tDespacharExistenciaParcial
    No = 0   ' Rechaza la línea
    Si = 1   ' Despacha lo disponible
End Enum

Public Enum tTipoRotacion
    FIFO = 1
    LIFO = 2
    FEFO = 3
End Enum
```

## Cómo se llena la matriz a partir de una corrida `learn-config`

1. Ejecutar `sql/learn-config.sql` reemplazando `:id_bodega`.
2. Tomar la fila resultante y mapear cada columna a su entrada en `clients/<cliente>.yaml` bajo `flags:`.
3. Establecer `learned: true` y `learned_from: { branch, bodega, fecha }`.
4. Re-correr `compute-matrix.cjs` para regenerar `compatibility.md`.

## Flags fuera del alcance de reservas

`Crear_Recepcion_De_Compra_NAV`, `Push_Ingreso_NAV_Desde_HH`, `SAP_Control_Draft_*`,
`Bodega_Facturacion`, `Bodega_Prorrateo*`, `Centro_Costo_*`, etc. son flags de
integración con ERPs externos y NO impactan los escenarios `RES-*`. Quedan
documentados en la entidad pero no se incluyen en `requires_config` de los YAMLs.

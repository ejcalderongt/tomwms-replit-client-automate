# Suite: state-machine-pedido

Audita la maquina de estados de los pedidos: distribucion, coherencia
encabezado/detalle, transiciones registradas, edad por estado, y
coherencia de cantidades.

## Como se ejecuta

```powershell
Invoke-WmsBrainAnalysis -Suite state-machine-pedido -Profile K7-PRD
Invoke-WmsBrainAnalysis -Suite state-machine-pedido -Profile BB-PRD -OutputJson .\bb-statemachine.json
Invoke-WmsBrainAnalysis -Suite state-machine-pedido -Profile K7-PRD -OnlyQueries S2,S3
```

## Queries

| Id | Tipo | Para que |
|---|---|---|
| S1 | data | Distribucion de estados (pedidos por estado) |
| S2 | data | Estado enc vs estado det (matriz de coherencia) |
| S3 | data | Pedidos terminales con detalle pendiente (P-16b sintoma) |
| S4 | data | Transiciones ultimos 30d (requiere pedido_estado_log) |
| S5 | data | Pedidos sin ninguna transicion |
| S6 | data | Edad de pedidos en cada estado |
| S7 | data | Coherencia cantidad solicitada vs despachada |

## Red flags

- **RF1**: S2 muestra encabezado=DESPACHADO con lineas != DESPACHADO →
  posible bug P-16b o transicion incompleta.
- **RF2**: S3 retorna >0 filas → pedidos cerrados con cantidad
  pendiente. Datos corruptos o falla en commit transaccional.
- **RF3**: S6 muestra pedidos en PENDIENTE con edad >30 dias →
  abandonados o atascados.

## Dependencias

- Tabla `pedido_estado_log` (S4 y S5). Si no existe, esas queries
  van a loggear error y el resto de la suite continua.
- Cols `cantidad_despachada` en `pedido_det` (S3, S7). Si el cliente
  usa otro nombre, ajustar la query y correr local primero.

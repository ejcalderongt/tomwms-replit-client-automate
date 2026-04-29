# L-026 — ARCH: cambio a IDENTITY autoincremental como root cause #1 del salto 2023→2028

> Etiqueta: `L-026_ARCH_IDENTITIES-2028-CONCURRENCIA_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-MIGRACION-2023-A-2028 parte 3 (breaking changes 2028)

## Hallazgo

El cambio principal de la version 2028 del WMS NO es funcionalidad nueva.
Es **arquitectonico de la capa de persistencia**: pasar el manejo de
identities (PKs autoincrementales) del patron historico
`MAX(id) + 1` calculado en aplicacion, al patron correcto `IDENTITY`
nativo de SQL Server.

Cita literal Carolina:

> "El cambio principal de esta version es el manejo de identities en
> tablas donde antes insertabamos el id con el max de la llave, esto
> repercutia en errores de concurrencia que provocaban que algunos
> datos no se insertaran correctamente, tales como recepciones,
> reservas de inventario y otros."

## Diagnostico tecnico del bug historico

### Patron viejo (pre-2028)

```vbnet
' En clsLn<Tabla>_Partial.vb (no enforced uniformemente)
Dim newId As Integer
newId = clsLn<Tabla>.Get_Max_Id() + 1   ' SELECT MAX(IdX) FROM tabla
Dim be As New clsBe<Tabla>()
be.IdX = newId
be.... = ...
clsLn<Tabla>.Insert(be)                 ' INSERT con IdX = newId
```

### Por que fallaba

1. **No es atomico**. Entre el `SELECT MAX` y el `INSERT` hay una
   ventana de N milisegundos donde otra sesion puede ejecutar lo
   mismo, leer el mismo `MAX`, y ambos intentar insertar con el mismo
   id.
2. **Sin transaccion serializable**. El codigo no rodea el bloque con
   un `BEGIN TRAN ... COMMIT` con isolation `SERIALIZABLE`.
3. **PK constraint violation aleatoria**. La segunda transaccion en
   llegar lanza `Violation of PRIMARY KEY constraint`. La aplicacion
   capturaba la excepcion y, dependiendo del modulo, **silenciaba el
   error o lo logueaba en `log_error_wms` sin retry**, perdiendo el
   dato.
4. **Modulos afectados confirmados**: recepciones, reservas de
   inventario "y otros". Probable: trans_movimientos, trans_picking_*,
   trans_despacho_*, faltantes (todas las tablas con alta concurrencia
   por la HH multi-operador).

### Patron nuevo (2028)

```sql
-- Schema 2028
CREATE TABLE trans_re_enc (
    IdRecepcionEnc INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    ...
);
```

```vbnet
' En clsLn<Tabla>_Partial.vb (2028)
Dim be As New clsBe<Tabla>()
be.... = ...                             ' SIN setear IdX
clsLn<Tabla>.Insert(be)                  ' INSERT, SQL Server asigna identity
Dim newId As Integer = be.IdX            ' SCOPE_IDENTITY() devuelto
```

El motor de SQL Server garantiza atomicidad e unicidad del IDENTITY
sin depender de la aplicacion.

## Por que tomo tantos anos cambiar esto

Hipotesis (a confirmar con Erik):

1. **Codigo legacy generado por el codegen de Efren** (L-* pendiente
   sobre el generador): los `clsLn<Tabla>.Insert(be)` se generan
   automaticamente. Cambiar la convencion implica regenerar 600+ clases
   y validar que ninguna logica adhoc dependa del valor pre-INSERT del
   id.
2. **Bugs eran intermitentes y silenciados**. Los errores de PK
   violation en alta concurrencia se "resolvian" en `log_error_wms`
   sin disparar alerta. Pasaron desapercibidos hasta que un caso
   masivo (alta concurrencia HH con muchos operadores simultaneos en
   recepcion BYB o MAMPA) los hizo evidentes.
3. **No hay rollback simple**. Cambiar de `MAX+1` a IDENTITY requiere
   DROP + ADD column o `DBCC CHECKIDENT` con manejo de los IDs
   existentes (que pueden tener gaps, valores fuera de rango, etc).
   Es DDL pesada de prod.

## Implicaciones del cambio 2028

### Tablas que se sabe migraron a IDENTITY (parcial, falta inventario completo)
- `trans_re_enc` (recepciones — confirmado por Carolina)
- `stock_res` (reservas de inventario — confirmado)
- (probable, falta inventariar) `trans_picking_enc`, `trans_picking_det`,
  `trans_despacho_enc`, `trans_despacho_det`, `trans_movimientos`,
  `trans_movimiento_pallet`, `faltantes`

### Lo que rompe al migrar 2023 → 2028 en un cliente vivo

1. **Existing rows con gaps en el id**: `DBCC CHECKIDENT` debe setear
   el seed correctamente para NO chocar con valores existentes.
2. **Triggers / SPs que asumen el patron viejo**: si un SP hace
   `INSERT ... VALUES (Get_Max + 1, ...)`, falla porque IDENTITY no
   permite insertar el id. Hay que reescribirlos sin la columna id en
   el INSERT.
3. **Codigo cliente / extensiones**: si CEALSAMI3 / SAPBOSyncMampa
   tienen INSERTs hardcoded con id, se rompen.
4. **Audit trail**: si habia un campo `IdAnterior` que copiaba el
   `IdRecepcionEnc` para trazabilidad cross-tabla, hay que validar que
   no se rompa la cadena.

## Riesgo residual

Q-* nueva derivada:
- **Q-IDENTITIES-MIGRACION-CHECKLIST** (a abrir): hay un script /
  checklist que enumere TODAS las tablas migradas a IDENTITY en 2028,
  con orden recomendado de DDL para cliente vivo? Carolina confirmo
  que tiene un script en su PC pero no esta versionado.
- **Q-IDENTITIES-LOG-RETROCESO** (a abrir): cuantos errores
  historicos en `log_error_wms` se atribuyen al patron viejo? Es
  metrica util para vender el cambio.

## Cierra Q-*

- `Q-MIGRACION-2023-A-2028 (parte 3)` — breaking changes que justifican
  major → 4 cambios principales, este es el #1.

## Conexion con otros learnings

- **L-027** (proc orden migracion 2028): el orden BYB+IDEALSA+INELAC en
  paralelo prioriza clientes que YA TIENEN QAS instalado, probablemente
  porque ahi se valido el cambio de IDENTITY sin riesgo productivo.
- **L-013** (outbox granularidad por linea): si el outbox depende del
  IdLineaPedido como FK, la migracion a IDENTITY puede impactar la
  reanudacion de outboxes parados (caso BYB outbox 2024).
- **L-017** (i_nav_transacciones_out FKs sentinela cero): el patron
  sentinela cero NULL→0 puede ser legacy del MAX+1, donde 0 era un
  "id no asignado todavia". Con IDENTITY no aplica.

## Pendientes

- Inventariar las tablas migradas a IDENTITY en 2028 (revisar git diff
  schema entre `dev_2023_estable` y `dev_2028_merge`).
- Documentar el procedimiento de migracion (Carolina tiene script
  local, hay que llevarlo a un repo versionado — Q83 cuestionario
  bloque 14).

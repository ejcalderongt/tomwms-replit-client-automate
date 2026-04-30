# 01 — Setup conexión BD

## Stack

- **Motor:** SQL Server (varía la versión por cliente, generalmente 2016+)
- **Driver Python:** `pymssql` (no `pyodbc`, da menos dolor en entornos sin drivers ODBC nativos)
- **Modo:** read-only via cuenta dedicada o, en su defecto, `sa` con compromiso explícito de no ejecutar DML

## Variables de entorno requeridas (template)

| Var                                | Significado                            | Ejemplo (placeholder)        |
|------------------------------------|----------------------------------------|------------------------------|
| `WMS_DB_HOST`                      | Host:puerto del SQL Server             | `52.41.114.122,1437`         |
| `WMS_DB_USER`                      | Usuario                                | `sa` (o RO dedicado)         |
| `WMS_<CLIENTE>_DB_PASSWORD`        | Password (uno por cliente, no compartir) | secreto                    |
| `WMS_<CLIENTE>_DB_NAME`            | Nombre de la BD                        | `TOMWMS_KILLIOS_PRD_2026`    |

**Regla:** un secreto por cliente. Nunca pongas la password de un cliente en el yml de otro. Si hay password compartida, igual replicá la variable por cliente para que rotar uno no afecte al resto.

## Por qué pymssql y no pyodbc

- pymssql se instala como wheel sin dependencias nativas adicionales en la mayoría de entornos.
- pyodbc requiere `unixODBC + msodbcsql17/18` que muchas veces no está disponible.
- Para queries puntuales pymssql sobra.

Limitación conocida pymssql: el campo `datetime` viene como `datetime.datetime`/`datetime.date`. Cuidado al serializar a CSV. El helper `csv_out` ya lo maneja con str().

## Patrón de conexión seguro

Ver `templates/_db.py`. Resumen:

1. Lee env vars (no hardcodea nada).
2. Conecta con `as_dict=True` (rows como dict, no tuplas).
3. Expone `q(sql, params=None)` que devuelve lista de dicts.
4. Cierra cursor y conexión por query (simple, sin pool — son scripts batch).
5. **Bloquea cualquier comando que empiece con** `INSERT|UPDATE|DELETE|DROP|TRUNCATE|ALTER|CREATE|MERGE|EXEC` por seguridad explícita, aunque la cuenta sea read-only.

## Lo que NUNCA hay que hacer

1. **No ejecutar `SELECT *` en tablas grandes** sin LIMIT/TOP. `trans_movimientos` puede tener millones de filas.
2. **No olvidar `WITH (NOLOCK)`** en queries de auditoría sobre tablas vivas — podés bloquear el WMS productivo del cliente. Si la BD del cliente está en producción, agregar `WITH (NOLOCK)` a todas las tablas que se sabe son escritas en tiempo real (`stock`, `stock_res`, `trans_movimientos`, `trans_picking_ubic`).
3. **No pegar passwords en commits.** Las queries que se commitean al brain referencian env vars, nunca el valor.
4. **No dejar conexiones abiertas.** Cada `q()` abre y cierra. Si hacés muchas queries seguidas, agrupalas en una transacción de lectura.
5. **No correr scripts contra `sa` desde un script generado por LLM sin revisar.** Aunque el helper bloquee DML, una query MAL escrita con `JOIN` cartesiano puede matar el server. Hacé `SET STATISTICS TIME ON` o `SET ROWCOUNT 1000` durante la exploración inicial.

## Verificación inicial recomendada (smoke test)

Antes de cualquier extracción real, correr:

```python
from _db import q
print(q("SELECT @@VERSION as v")[0]['v'])
print(q("SELECT name FROM sys.databases ORDER BY name")[:20])
print(q("SELECT TOP 5 name FROM sys.tables ORDER BY create_date DESC"))
print(q("SELECT COUNT(*) n FROM trans_movimientos")[0]['n'])
```

Si alguna falla, parar y revisar credenciales/red antes de seguir.

## Permisos mínimos para una cuenta RO dedicada

Si el cliente acepta crear cuenta dedicada (mejor que `sa`), pedirle:

```sql
CREATE LOGIN brain_ro WITH PASSWORD = '...';
USE TOMWMS_<cliente>_PRD;
CREATE USER brain_ro FOR LOGIN brain_ro;
ALTER ROLE db_datareader ADD MEMBER brain_ro;
GRANT VIEW DEFINITION TO brain_ro;
DENY INSERT, UPDATE, DELETE, EXECUTE TO brain_ro;
```

`VIEW DEFINITION` es importante para poder explorar `sys.columns` y entender estructura de tablas nuevas.

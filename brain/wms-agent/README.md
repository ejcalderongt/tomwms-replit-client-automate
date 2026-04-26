# wms-agent (`wmsa`)

CLI swiss-army local para el proyecto WMS de Erik Calderón. Pensado para
correr en la PC de desarrollo, junto a openclaw (Claude Code / OpenCode).

**Filosofía**: resolver localmente lo que se pueda; escalar a Replit lo que no.
Empezar minimal, ir agregando comandos a medida que aparezca la necesidad.

## Setup

```cmd
cd tools\wms-agent
pip install -e .
pip install -e .[db]
```

La segunda línea agrega `pyodbc` para consultar la BD productiva (requiere
driver ODBC SQL Server 17+, el mismo que usa SSMS).

### Invocación: `wmsa` vs `python -m wmsa.cli`

Cuando pip instala en modo `--user` en Windows, el script `wmsa.exe` queda
en `%APPDATA%\Python\Python3xx\Scripts\` que puede no estar en el PATH.
Tenés 3 opciones:

1. **Fallback siempre funcional**: `python -m wmsa.cli ...` (no requiere PATH).
2. **Wrapper portable**: copiar `tools\wms-agent\wmsa.cmd` a una carpeta que
   ya esté en tu PATH (ej. `C:\Users\yejc2\bin`). Es un shim que llama
   `python -m wmsa.cli %*`.
3. **Agregar Scripts al PATH**: agregar `%APPDATA%\Python\Python3xx\Scripts`
   a la variable de ambiente PATH (requiere relogin).

## Configurar credenciales (interactivo)

```cmd
wmsa config init
```

Te pregunta:
- URL del Brain (Replit dev domain).
- Token del Brain (`BRAIN_IMPORT_TOKEN`).
- Host / DB / user / password de KILLIOS prod.

Las credenciales se guardan en el **keyring del SO** (Credential Manager en
Windows). Nunca quedan en archivo plano. Nombre de la BD también se guarda
porque puede cambiar entre clientes.

```cmd
wmsa config show          REM ver config (sin secrets)
wmsa config set-db NAME   REM cambiar DB sin re-pedir password
wmsa config clear         REM borrar todo
```

## Comandos disponibles

### Brain (consulta el grafo en Replit)

```cmd
wmsa brain health
wmsa brain search stock --kind sql-table
wmsa brain impact VW_Stock_Res --depth 2
wmsa brain dependencies VW_Stock_Res
wmsa brain writers stock --op update,delete --kind sql-table
```

### DB KILLIOS (solo lectura, refusa cualquier write)

```cmd
wmsa db query "SELECT TOP 10 * FROM stock"
wmsa db tables --like %stock%
wmsa db sp-text Get_Stock_Actual
```

### Caso (intake estructurado para escalar a Replit)

```cmd
wmsa case new                        REM intake interactivo, guarda en ~/.wmsa/cases/
wmsa case list
wmsa case show CASE_ID
wmsa case escalate CASE_ID           REM imprime el bloque markdown listo para pegar en chat de Replit
```

## Roadmap (lo que viene cuando lo necesitemos)

- [ ] `wmsa case investigate CASE_ID` — corre brain queries automáticas
      según el tipo de caso y enriquece el case file.
- [ ] `wmsa brain re-index --vb` / `--java` — reindex remoto con un comando.
- [ ] `wmsa diff CONTROLLER METHOD` — atajo para "qué se perdió en este método".
- [ ] MCP server (`wmsa mcp serve`) para que openclaw vea las tools nativas.
- [ ] `wmsa db trace SP_NAME` — Extended Events / Profiler del SP.
- [ ] Cache local de respuestas del Brain con TTL.

## Cómo extender

Cada comando vive en `wmsa/commands/<area>.py`. Para agregar uno nuevo:

1. Crear el archivo (ej. `wmsa/commands/diff.py`).
2. Definir un `app = typer.Typer()` con los subcomandos.
3. Registrarlo en `wmsa/cli.py` con `app.add_typer(...)`.

Cualquier acceso a recursos externos (Brain, KILLIOS, sistema de archivos
del usuario) tiene que pasar por `wmsa.config` y `wmsa.brain` / `wmsa.killios`.
**Nunca hardcodear URLs ni credenciales en los comandos.**

## Reglas de seguridad

1. Las queries SQL se validan: rechaza cualquier statement que no empiece
   con `SELECT`, `WITH`, `EXEC`/`EXECUTE` (SP de lectura) o `SET` (config).
   Bloquea `INSERT`, `UPDATE`, `DELETE`, `MERGE`, `DROP`, `ALTER`, `CREATE`,
   `TRUNCATE`.
2. Connection strings nunca se loguean.
3. Si una query devuelve > 1000 filas se paginan; el agente no se cuelga ni
   imprime megabytes en consola.
4. El token del Brain se manda solo por header `X-Brain-Token`, nunca por URL.

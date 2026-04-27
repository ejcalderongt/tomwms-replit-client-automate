# Configuracion multi-ambiente

> Schema y politica para gestionar credenciales de distintos ambientes desde una unica config local.

## Problema

El agente `wmsa` se conecta a multiples BDs SQL Server: clientes (Killios/BYB/CEALSA), distintas instancias por entorno (PRD, QAS, DEV) y eventualmente otros servidores. La version actual de `wmsa/config.py` solo soporta una conexion a la vez. Necesitamos selector de ambiente sin recompilar.

## Solucion: archivo TOML por usuario

**Ubicacion**: `~/.wmsa/config.toml` (se respeta `XDG_CONFIG_HOME` si existe).

**Formato**:

```toml
# Server por defecto cuando no se especifica --env
default_env = "killios.prd"

[brain]
base_url = "https://brain.example.com"
# token: leido de env BRAIN_IMPORT_TOKEN

# Server fisico (compartido por varios ambientes)
[servers.ec2-prd]
host = "52.41.114.122"
port = 1437
encrypt = true
trust_server_certificate = true
description = "EC2 productivo compartido"

[servers.ec2-stg]
host = "x.x.x.x"
port = 1433

# Ambientes (cada uno apunta a un server + database + credencial)
[envs."killios.prd"]
server = "ec2-prd"
database = "TOMWMS_KILLIOS_PRD"
user_env = "WMS_KILLIOS_DB_USER"            # NOMBRE de la env var (no el valor)
password_env = "WMS_KILLIOS_DB_PASSWORD"
client = "killios"
read_only = true
notes = "Productiva. Solo SELECT/EXEC."

[envs."byb.prd"]
server = "ec2-prd"
database = "IMS4MB_BYB_PRD"
user_env = "WMS_BYB_DB_USER"
password_env = "WMS_BYB_DB_PASSWORD"
client = "byb"
read_only = true

[envs."cealsa.qas"]
server = "ec2-prd"
database = "IMS4MB_CEALSA_QAS"
user_env = "WMS_CEALSA_DB_USER"
password_env = "WMS_CEALSA_DB_PASSWORD"
client = "cealsa"
read_only = true

# Atajos: si el usuario y password son los mismos para todos los ambientes (caso actual de Erik con sa),
# se puede usar shared_credentials:
[shared_credentials]
user_env = "WMS_DBA_USER"
password_env = "WMS_DBA_PASSWORD"
```

## Reglas de oro

1. **NUNCA** se almacena el password en el TOML. Solo el **nombre** de la env var (`password_env`).
2. La env var puede vivir en:
   - Shell del usuario (`~/.bashrc`, `~/.zshrc`)
   - Keyring del sistema (gestionable con `wmsa config set-secret <env_name>`)
   - Vault corporativo (si existe)
3. Si `user_env`/`password_env` no estan resueltos, el comando falla con mensaje explicito **antes** de intentar conectar.
4. `read_only=true` activa el guard de `assert_read_only` (ya existe en `wmsa/killios.py`).
5. El TOML se versiona **sin secretos** (es seguro commitearlo). Las env vars son secretos por usuario y NO se commitean.

## Comandos del CLI extendido

Estos comandos no existen aun, son el **diseño** propuesto:

```bash
# Listar ambientes
wmsa env list

# Activar ambiente por defecto
wmsa env use killios.prd

# Probar conexion a un ambiente (sin ejecutar query)
wmsa env test --env killios.prd

# Ejecutar query contra un ambiente especifico (override del default)
wmsa db query "SELECT TOP 10 * FROM bodega" --env byb.prd

# Comparar mismo query en multiples ambientes
wmsa db query "SELECT * FROM i_nav_config_enc" --env killios.prd --env byb.prd --env cealsa.qas --diff

# Setear/rotar password en keyring
wmsa secrets set killios.prd.password
```

## Migracion del codigo actual

`wmsa/config.py` actual lee 4 env vars sueltas (`WMS_KILLIOS_DB_HOST/NAME/USER/PASSWORD`). Para migrar:

1. Mantener compatibilidad hacia atras: si no existe `~/.wmsa/config.toml`, leer de las env vars sueltas y comportarse como hasta ahora.
2. Si existe el TOML, ignorar las env vars sueltas y usar el sistema multi-env.
3. `wmsa config init` genera el TOML inicial preguntando (o autodetectando desde env vars existentes).

Cambio en `Config`:

```python
@dataclass
class EnvConfig:
    name: str
    server: str        # nombre logico del server (resolverlo en config.servers[name])
    database: str
    user: str
    password: str      # resuelto en runtime desde env var, nunca persistido
    read_only: bool = True

@dataclass
class Config:
    default_env: str
    envs: dict[str, EnvConfig]
    servers: dict[str, ServerConfig]

    def get_env(self, name: str | None = None) -> EnvConfig: ...
```

## Ejemplo de uso desde codigo

```python
from wmsa.config import Config
from wmsa.killios import KilliosClient

cfg = Config.load()
env = cfg.get_env("killios.prd")  # o None para default
client = KilliosClient(env)
result = client.query("SELECT TOP 5 * FROM bodega")
```

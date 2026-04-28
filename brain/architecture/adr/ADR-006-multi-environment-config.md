# ADR-006: Configuracion multi-ambiente para wmsa

## Contexto

`wmsa/config.py` actualmente solo soporta una conexion a la vez (4 env vars sueltas `WMS_KILLIOS_DB_*`). Necesitamos conectar a multiples BDs (Killios PRD, BYB PRD, CEALSA QAS, futuros: Killios DEV, etc.) sin recompilar ni reiniciar.

## Decision

Adoptar archivo TOML `~/.wmsa/config.toml` con secciones `[servers.X]` y `[envs."client.tier"]`. Las credenciales (user/password) se referencian por **nombre de env var**, nunca como valor literal. Detalle completo en `brain/skills/wms-test-bridge/reference/multi-env-config.md`.

## Alternativas consideradas

- **JSON**: descartado por ser menos legible para humanos editando a mano.
- **Variables de entorno con prefijo**: `WMS_<CLIENT>_<TIER>_DB_*`. Funciona pero se vuelve inmanejable con >5 ambientes y dificulta documentar metadatos por ambiente.
- **YAML**: similar a TOML pero mas frágil con tipos.

## Consecuencias

- Compatible hacia atras (si no existe TOML, lee env vars sueltas como hoy).
- El TOML es seguro para versionar (sin secretos).
- Permite `wmsa db query --env A --env B --diff` para comparativas cross-cliente.
- Requiere extension del CLI: comandos `env list/use/test` y `secrets set`.

## Migracion

Fase 1 (este ciclo): documentar el schema y el comportamiento esperado.
Fase 2: implementar lectura del TOML en `Config.load()`.
Fase 3: agregar comandos CLI nuevos.
Fase 4: migrar uso interno y deprecar env vars sueltas.

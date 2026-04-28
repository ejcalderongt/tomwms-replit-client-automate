# Parametros y capability flags por cliente

> Conocidos hasta hoy: pocos. Esta hoja crece a medida que Erik aclara
> donde viven los flags de configuracion.

## Flags conocidos

### `i_nav_config_enc.nombre_ejecutable`
- **Tipo**: VARCHAR
- **Por cliente**:
  - K7: `SAPSync.exe` (probable, confirmar)
  - BB: `NavSync.exe` (probable)
  - BECOFARMA: `SAPBOSync.exe` (confirmado L-015)
  - C9: ?
- **Uso**: dispatch dinamico del binario sincronizador desde el
  ClickOnce que empaqueta TODOS (L-015).

## Flags candidatos (presumibles, sin ubicacion confirmada)

### `verificacion_etiqueta_activa`
- **Tipo**: bit (presumido)
- **Donde vive**: probablemente `i_nav_config_enc` o tabla similar.
  PENDIENTE confirmar con Erik (Q-CAPABILITY-FLAG en
  `funcionalidades-por-cliente.md`).
- **Por cliente** (segun evidencia operativa, no segun el flag):
  - K7: ON (screenshot 29-abr-2026)
  - BB: ?
  - BECOFARMA: ON (modulo activo confirmado)
  - C9: ?

### `log_error_segmentado_activo`
- **Tipo**: bit (presumido)
- **Donde vive**: probablemente codigo de aplicacion (no flag de BD),
  segun L-016 (es una decision de codebase, no de configuracion).
- **Por cliente**:
  - K7: OFF (sigue usando `log_error_wms` unificado)
  - BB: OFF
  - BECOFARMA: ON (las 5 tablas segmentadas presentes)
  - C9: OFF

## Pregunta abierta master

Necesitamos un **inventario completo de capability flags y parametros
por cliente** para alimentar:
1. La WebAPI .NET 10 (que endpoints exponer por cliente, que
   validaciones aplicar).
2. El generador de sets de prueba (que casos correr para cada
   cliente).
3. El runbook de deploy / migracion (que se debe configurar al subir
   un cliente nuevo).

Pregunta para Erik: ¿existe ya un documento o tabla maestra con
todos los capability flags por cliente? Si no, ¿lo construimos
juntos en una sesion dedicada?

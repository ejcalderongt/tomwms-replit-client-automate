# ADR-005 — Migración a IDENTITY y equivalencia funcional dev_2023_merge ↔ dev_2028_merge

**Status**: Accepted
**Date**: 2026-04-27
**Decided by**: Erik Calderón (PrograX24)

## Contexto

El bridge WMS-Brain necesita aprender la configuración real de cada cliente (`learn-config`)
para producir una matriz `escenario × cliente × OK/N/A` confiable. La fuente de verdad es
la BD SQL Server de **Killios** en producción, pero esa BD corre la rama **dev_2023_merge**
del producto, no la rama de desarrollo activa **dev_2028_merge**.

## Decisión

Aceptamos usar la BD Killios PRD (rama dev_2023_merge) como fuente para `learn-config`,
con las siguientes condiciones:

1. **Solo lectura.** La BD Killios PRD se accede READ-ONLY estricta. Ningún script del bridge
   puede ejecutar `INSERT`, `UPDATE`, `DELETE` ni DDL. Los aprendizajes se persisten
   en el repo (YAMLs en `clients/`), no en la BD.

2. **Equivalencia funcional verificada.** Para los archivos de configuración que el bridge
   consulta, dev_2023_merge y dev_2028_merge son idénticos:
   - Diff de `clsLnConfiguracion_qa.vb` entre ambas ramas: vacío.
   - Diff de `clsLnI_nav_config_enc_Partial.vb`: equivalente (los SELECTs sobre
     `VW_Configuracioninv` y `i_nav_config_enc` son los mismos).
   - La entidad `clsBeI_nav_config_enc` no cambió de schema entre ramas.

3. **El único cambio significativo conocido entre ramas es la migración de varias
   tablas transaccionales a `IDENTITY` en dev_2028_merge**, eliminando el patrón
   `SELECT MAX(id) + 1` que provocaba colisiones bajo concurrencia. Las tablas
   afectadas son tablas de movimiento (no de configuración), por lo que no afectan
   a `learn-config`. Sí afectan a casos de inserción concurrente fuera del alcance
   actual del bridge.

## Consecuencias

- Los flags aprendidos contra Killios PRD (dev_2023) son **válidos como configuración
  efectiva del cliente** para el resto del trabajo del bridge.
- Los YAMLs en `clients/<cliente>.yaml` deben registrar la rama desde la que se
  aprendieron (`learned_from.branch: dev_2023_merge`) para trazabilidad.
- Roadmap: cuando se migren las tablas de movimiento críticas a IDENTITY también en
  el resto de las ramas (TODO declarado por Erik), el bridge debe ser **agnóstico al
  patrón de generación de IDs** (no asumir `MAX(id) + 1`, no precalcular ids).
  Esto ya está presente como requerimiento en cualquier escenario que inserte datos.

## Tablas que se conoce o se proyecta migrar a IDENTITY (no exhaustivo)

> Pendiente confirmar lista exacta. Erik mencionó que la lista crece según se detectan
> colisiones por concurrencia. Tablas candidatas conocidas: las que registran
> movimientos de stock, líneas de documentos de salida e ingreso, y tablas de cola
> entre BOF y Handheld. Cuando se confirme la lista, agregar acá.

## Referencias

- `brain/skills/wms-test-bridge/sql/learn-config.sql`
- `brain/test-scenarios/_matrix/README.md`
- VB sources: `/TOMIMSV4/Entity/Interface/Configuracion/ConfiguracionEncabezado/clsBeI_nav_config_enc.vb`

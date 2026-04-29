# L-040 — ARCH: la BD NAV NO es accesible para el WMS, todo va por WebServices NAV

> Etiqueta: `L-040_ARCH_BD-NAV-INACCESIBLE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-BYB-NO-DISPONIBLE-NAV-BD

## Hallazgo

**La BD de NAV en BYB (y por extension cualquier cliente con NAV) no esta expuesta al WMS.** Cero acceso directo: ni read, ni write, ni replica, ni linked server, ni snapshot.

**Todo el trafico WMS↔NAV pasa por los WebServices de NAV.** Tanto catalogos como transacciones como creacion de picking.

## Cita literal Carolina (Wave 11)

> "La BD de NAV no esta accesible para el WMS.
> Accedemos a los catalogos y a las transacciones siempre por los Web
> service de NAV.
> A traves de los Web Services podemos leer las diferentes
> transacciones, registrar procesos, crear picking."

## Capacidades WS NAV (segun Carolina)

| Operacion | Direccion | Donde se invoca |
|---|---|---|
| Leer catalogos | NAV → WMS | BOF y/o WSHHRN al sincronizar maestros (clases `clsSyncNav*`) |
| Leer transacciones | NAV → WMS | BOF al consultar OP/Transferencias/Devoluciones |
| Registrar procesos | WMS → NAV | HH al recibir licencia + cerrar documento (L-039) |
| Crear picking | WMS → NAV | BOF y/o HH cuando se genera picking que NAV debe conocer |

## Implicaciones tecnicas

1. **Sin acceso SQL a NAV → no hay reportes cross-system con JOINs**. Todo reporte que mezcle WMS+NAV se hace por aplicacion (pull del WS, materializar local, joinear).
2. **Sin replica → no hay analytics tipo data warehouse desde NAV** sin pedirselo a NAV. Los datos NAV en el WMS son los que se traen via WS y se persisten en tablas con prefijo `i_nav_*` o en clases `clsSyncNav*`.
3. **La sincronizacion es pull-based**: el WMS pide; NAV no empuja. (A confirmar con Erik: ¿hay webhooks NAV → WMS o todo es polling?)
4. **Esto refuerza L-039**: la HH llama directo a NAV WS porque no hay otra forma. No es preferencia, es la unica via.
5. **Para BD masters tipo Productos, Familias, etc**, las clases `clsSyncNav*` (vistas en code-deep-flow previo) son el camino canonico. Cambiarlas afecta sincronizacion para TODOS los clientes con interface "Nav-style" (ver L-025: el prefijo Nav no es solo NAV-Dynamics).

## Por que es relevante para el master plan 2028

- **Migracion BYB a 2028 NO requiere migrar NAV**. NAV se queda donde esta. Solo las tablas/clases del WMS que consumen WS NAV.
- **Si NAV cambia versionado de sus WS**, BYB se rompe sin que el WMS lo cause. Riesgo externo.
- **CEALSA migrando ARITEC → Odoo (L-031)**: el patron deberia ser el mismo — Odoo expone WS, WMS los consume. NO acceso BD Odoo. Confirmar con Erik/Efren.

## Q-* abiertas

- Q-NAV-WS-WEBHOOKS: ¿NAV puede empujar cambios al WMS (webhook) o todo es pull?
- Q-NAV-WS-VERSIONADO: ¿hay control de version del contrato NAV WS? ¿Que pasa si NAV upgrade rompe payload?
- Q-NAV-WS-CACHE-WMS: ¿el WMS cachea respuestas NAV (catalogos)? ¿TTL?
- Q-ODOO-WS-EQUIVALENTE: cuando CEALSA migre a Odoo, ¿el patron es el mismo (WS, no BD directa)? Pendiente Erik/Efren.

## Vinculos

- L-039: HH llama directo a NAV WS (consecuencia practica de este learning).
- L-025: prefijo Nav en codigo es generico ERP — el patron WS-only se aplica a NAV, ARITEC, IMS4, Odoo, SAP B1, etc.
- L-031: master congelada (CEALSA ARITEC → Odoo).

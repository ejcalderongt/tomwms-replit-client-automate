# Equipo PrograX24 — Roster operativo

> Fuente: dictado verbal de Erik (EJC), 2026-04-30, Wave 10.
> Este archivo identifica a las personas detrás de los iniciales que aparecen en commits, comentarios `'#XX<fecha>`, ramas y conversaciones del brain.
> Reglas: nunca mencionar a personas con tono crítico. El brain documenta hechos, no juicios sobre individuos.

## Iniciales → Persona

### EJC — Erik José Calderón
- Fundador / arquitecto / cerebro de WMS.
- Firma personal en código: `'#EJC<fecha>` (comentarios dispersos en BOF y HH).
- Responsable del brain externo (`tomwms-replit-client-automate`, rama `wms-brain`).
- Voz narrativa de `naked-erik-anatomy/`.

### CF / CKFK — Carolina Fuentes Kemp
- Co-líder del WMS junto a EJC.
- **El corazón de WMS** (ver `naked-erik-anatomy/003-2026-04-30-para-carol.md`, escrito a mano por Erik).
- Interlocutora principal del cuestionario Wave 10 (`CUESTIONARIO_CAROLINA.md`).
- Conocimiento operativo profundo de los clientes en producción.

### GT — Efrén Gustavo
- Origen: Chimaltenango, San Lucas.
- Formación: Ingeniero en Sistemas, Maestría en Seguridad.
- Rol: Desarrollador senior.
- Responsabilidad principal: **operación del WMS en CEALSA** (cliente con ERP ARITEC migrando a Odoo, ver L-031 y Q-079/080).

### AT — Anderly Teleguario
- Formación: Ingeniero en Sistemas + Maestría en Seguridad.
- Rol: Desarrollador senior.
- Par técnico de GT en proyectos de mayor complejidad.

### MA — Marcela Álvarez
- Origen: Nicaragua (NI).
- Rol: Desarrolladora junior.

### MECR — Melvin Estuardo Cojtí Rabinal
- Rol: Desarrollador junior.

## Notas de uso para el agente

1. Cuando aparezca una sigla en commits, ramas o comentarios (`'#GT2025-...`, `'#AT2024-...`, etc.), resolverla contra esta tabla.
2. Si una sigla **no** está en esta tabla, marcarla como desconocida en el `architecture_inbox/` y pedir confirmación a EJC o CF antes de asumir autoría.
3. Esta tabla es **append-only**: si entra alguien nuevo se agrega; si alguien sale, se marca con fecha de salida pero **no se borra** (el código histórico sigue firmado con su sigla).

## Pendientes

- Confirmar con EJC si hay más colaboradores externos (consultores, freelance, partners ARITEC) que firmen código.
- Mapear quién toca qué cliente (matriz persona × cliente) — se construye a partir de `clients/` y `tasks-historicas/` cuando haya señal suficiente.

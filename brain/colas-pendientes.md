# Colas pendientes — investigaciones a profundizar

Cada cola es una pregunta abierta o tarea encolada. Cuando se cierra, mover a `debuged-cases/` o eliminar.

---

## C-001 — Aislar 5-10 casos HH puros del bug `dañado_picking`

**Origen:** CP-014 §5
**Pregunta:** los ~678 casos de bug originados desde HH (`IdOperadorBodega_Pickeo > 0`), ¿se comportan distinto que los del BOF? Hipótesis Erik: la HH sí intenta mover stock pero el webservice del BOF lo deshace.

**Plan:**
1. En `TOMWMS_KILLIOS_PRD_2026`, listar los 72 casos con `IdOperadorBodega_Pickeo > 0 AND dañado_picking=1 AND cantidad_verificada=0`.
2. Para cada uno, correr `audit-linea-tiempo-producto.py --idpb <X>` y revisar:
   - ¿Hay movimientos posteriores cerca de la fecha del marcado?
   - ¿Hay alguna evidencia de stock movido y luego revertido?
3. Comparar contra 72 casos análogos de BOF (`IdOperadorBodega_Pickeo = 0`).
4. Si HH sí mueve stock y el BOF deshace → bug del webservice.
5. Si HH tampoco mueve nada → bug compartido del flujo común post-marca.

**Salida esperada:** documento corto en CP-014 con tabla de comparación.

---

## C-002 — Confirmar estado operativo de MERCOPAN y BYB

**Origen:** CP-014, los backups en EC2 terminan en julio-2024 (MERCOPAN) y diciembre-2023 (BYB).

**Pregunta:** ¿siguen operando con TOMWMS hoy, abril-2026? Si sí, ¿el bug sigue acumulando?

**Plan:**
1. Erik confirma con su equipo si MERCOPAN/BYB están activos.
2. Si activos, pedir backup actualizado para reflashear en EC2.
3. Re-correr `audit-bug-danado-multi-bd.py --dbs IMS4MB_MERCOPAN_PRD IMS4MB_BYB_PRD` con los datos nuevos.
4. Calcular crecimiento del bug entre el backup viejo y el nuevo.

**Salida esperada:** anexo "estado-2026.md" en CP-014 con cifras actualizadas.

---

## C-003 — Verificar comportamiento HH BYB (outlier 21 % HH)

**Origen:** CP-014 — BYB es la única BD donde el bug viene 21 % de la HH (vs 1-3 % en el resto).

**Pregunta:** ¿BYB tiene una versión distinta del HH Android? ¿O su flujo HH es operativamente distinto?

**Plan:**
1. Identificar si `producto_bodega.sistema` u otros campos delatan la versión HH instalada en BYB.
2. Comparar `parametros_sistema` (si existe) entre KILLIOS, MERCOPAN, BYB.
3. Aislar los 101 casos HH de BYB y mirar usuarios/horarios/lic_plates en busca de patrón.

**Salida esperada:** nota en `client-index/byb.yml` con conclusión.

---

## C-004 — Por qué BECOFARMA NO usa `dañado_picking`

**Origen:** CP-014 §4
**Pregunta:** BECOFARMA dispara 591 AJCANTN en 3 meses sin marcar nunca dañados. ¿Su flujo es "ajusto antes de picking"? ¿Se podría replicar en Killios como mitigación temporal del bug?

**Plan:**
1. Identificar quién (rol/usuario) dispara los 591 AJCANTN en BECOFARMA — ¿supervisor? ¿auditoría? ¿operador?
2. Ver si hay configuración de `parametros_sistema` que oculte el botón "marcar dañado" en su BOF.
3. Documentar el flujo alternativo y proponer si es transferible a Killios mientras se hace el fix BOF.

**Salida esperada:** sección en `becofarma.yml` y posible nota en `CP-013/PLAYBOOK-FIX.md` como mitigación temporal.

---

## C-005 — Validar `audit-linea-tiempo-producto.py` con caso WMS164 conocido

**Origen:** CP-013 (caso ya analizado al detalle).

**Plan:** correr el nuevo programa con WMS164 y confirmar que la línea de tiempo coincide con lo documentado en `REPORTE-CONCLUSION-V3.md`. Si difiere, ajustar el programa.

```bash
python3 audit-linea-tiempo-producto.py --db TOMWMS_KILLIOS_PRD_2026 --codigo WMS164 --desde 2025-11-01
```

**Salida esperada:** confirmación o lista de ajustes necesarios.

---

(Mover entradas cerradas a `debuged-cases/` o eliminar. Crear nuevas con id incremental C-NNN.)

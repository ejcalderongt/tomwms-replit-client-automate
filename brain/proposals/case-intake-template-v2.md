# Propuesta — `CASE_INTAKE_TEMPLATE_v2.md` (mejora del template del repo TOMWMS_BOF)

> **Status**: PROPUESTA — no se pushea al repo `tomwms-replit-client-automate` ni al repo `TOMWMS_BOF` hasta que Erik confirme. Este documento queda en el brain como sugerencia de cambio externo.
>
> **Origen**: liderazgo del intake delegado en el agente. Surge de la práctica de las waves 13-1 a 13-7 con los 7 case-pointers.

## Por qué v2

El template actual (`CASE_INTAKE_TEMPLATE.md`, si existe en el repo TOMWMS_BOF) está orientado a **bugs reportados**. Lo que las waves recientes mostraron es que también hay un caudal importante de casos donde:

- No hay reporte formal — el evidence vive en código (hardcodes, comments, markers)
- El bug raíz puede ser viejo y ya estar mitigado, pero el residuo (breakpoint fosilizado, marker en BD) sigue ahí
- Hay autoría humana implícita (`JP`, `EJC`) que el template no captura

v2 unifica: **bugs reportados + case-pointers descubiertos por arqueología**.

## Cambios propuestos

### 1. Nueva sección `case_pointer_origen`

Para casos descubiertos por arqueología (no por reporte), capturar el case-pointer que originó el ticket:

```markdown
## Case-pointer de origen
- **ID**: CP-NNN
- **Tipo**: hardcode | comment | marker | breakpoint | otro
- **Archivo**: <path>
- **Líneas**: <range>
- **Descubierto en**: wave-NN
- **Autor referenciado** (si aplica): <iniciales o nombre>
```

### 2. Sección `evidencia_persistente`

Si el caso deja huella en BD (CP-007 es el ejemplo), capturar la query que confirma el impacto:

```markdown
## Evidencia persistente en BD
- **Tiene huella en BD**: sí | no
- **Query de confirmación**: `tools/case-seed/queries/.../NN_*.sql`
- **Resultado esperado**: <descripción>
- **Última corrida**: YYYY-MM-DD — <resumen>
```

### 3. Campo `severidad_dual`

Distinguir severidad del caso histórico vs severidad del residuo actual:

```markdown
## Severidad
- **Histórica** (cuando el bug estaba activo): baja | media | alta | crítica
- **Residual** (estado actual del residuo): baja | media | alta | crítica
- **Justificación de la diferencia**: <texto>
```

Ejemplo: CP-001 puede tener severidad histórica alta (cuando el caso se debugueaba en 2019) pero severidad residual baja (es solo ruido de debug).

### 4. Sección `accion_propuesta`

Distinguir entre limpieza superficial vs investigación raíz:

```markdown
## Acción propuesta
- [ ] Solo limpiar el residuo (e.g. borrar Debug.Print)
- [ ] Limpiar + documentar para que no vuelva
- [ ] Investigar bug raíz aunque ya no se manifieste
- [ ] Investigar bug raíz porque sigue manifestándose
- [ ] Wont-fix con razón documentada
```

### 5. Sección `clientes_afectados`

Especialmente importante para sistemas con divergencia por cliente (control de póliza, reportes especializados):

```markdown
## Clientes afectados
- **Conocidos**: <lista>
- **Probables (mismo perfil)**: <lista>
- **No afectados (descartados)**: <lista>
- **Pendiente de investigar**: <lista>
```

### 6. Cross-link a la bitácora viva

```markdown
## Bitácora de debug
Ver `brain/debuged-cases/CP-NNN.md` para historial de avance.
```

## Estructura completa propuesta del v2

```markdown
# CASE INTAKE — <título corto>

## Identificación
- **ID**: CASE-YYYY-NNN
- **Origen**: reporte cliente | arqueología | reporte interno | hipótesis
- **Fecha apertura**: YYYY-MM-DD
- **Reporter**: <nombre>
- **Asignado a**: <nombre>

## Case-pointer de origen (si aplica)
[ver sección 1]

## Descripción
<narrativa>

## Severidad
[ver sección 3]

## Evidencia persistente en BD
[ver sección 2]

## Reproducción
- **Pasos**: 1. ... 2. ...
- **Resultado esperado**: ...
- **Resultado actual**: ...

## Clientes afectados
[ver sección 5]

## Acción propuesta
[ver sección 4]

## Bitácora de debug
[ver sección 6]

## Cross-refs
- bugs relacionados:
- case-pointers relacionados:
- waves donde se mencionó:
```

## Compatibilidad con v1

Si el repo TOMWMS_BOF ya tiene un `CASE_INTAKE_TEMPLATE.md` v1, no se borra. v2 puede coexistir como `CASE_INTAKE_TEMPLATE_v2.md` y el equipo decide cuál usar para qué tipo de caso.

## Pendiente de Erik

- [ ] Confirmar que el repo TOMWMS_BOF tiene un template v1 (o no)
- [ ] Decidir si v2 reemplaza o coexiste
- [ ] Decidir nombre final (v2, advanced, archaeology, etc.)
- [ ] Push al repo TOMWMS_BOF cuando esté aprobado

## No-decisión deliberada

Esta propuesta **no se pushea automáticamente**. Las decisiones que afectan a otros repos pasan por Erik antes. Este documento queda en `brain/proposals/` como referencia.

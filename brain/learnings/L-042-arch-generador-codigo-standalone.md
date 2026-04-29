# L-042 — ARCH: el generador de codigo BE/LN-base/LN-partial es standalone, fuera de TOMWMS_BOF

> Etiqueta: `L-042_ARCH_GENERADOR-CODIGO-STANDALONE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-GENERADOR-UBICACION

## Hallazgo

El generador de codigo que produce los archivos BE (Business Entity), LN-base (Logica de Negocio base) y LN-partial (Logica de Negocio partial) **NO vive dentro de TOMWMS_BOF**. Es una **aplicacion standalone**, en su propio repositorio.

**Mantenedores activos**: Erik (EJC) y Efren (GT).

**Repo exacto**: Carolina **no recuerda el nombre**. Hay que descubrirlo.

## Cita literal Carolina (Wave 11)

> "Esta aplicacion esta fuera del proyecto de TOMWMS_BOF, si es
> standalone, esta en un repositorio, pero no se si es el mismo
> TOMWMS_BOF.
> Quienes le han dado mantenimiento son Erik y Efren."

(Notar la contradiccion suave: dice "esta en un repositorio, pero no se si es el mismo TOMWMS_BOF" — duda razonable. Confirmar con Erik.)

## Implicaciones

1. **Para ubicar el generador hay que buscar en Azure DevOps**, no en TOMWMS_BOF. Candidatos a investigar:
   - Repos de PrograX24 con nombre tipo `*Generator`, `*CodeGen`, `DALGen`, `BEGen`, `TomWMS_CodeGen`.
   - Repos vacios o con poca actividad (`TOMWMS5`, `CEALSA`) — descartar primero.
   - Posiblemente vive en un repo personal de Erik o como sub-folder de otro proyecto.
2. **Si Erik+Efren lo mantienen ambos**, hay alta probabilidad de divergencia local (branches sin merge, copias en dev boxes). Pedir ramas activas.
3. **El generador es parte critica del workflow**: cualquier nueva entidad agregada al WMS pasa por aqui. Sin el generador, el patron BE/LN-base/LN-partial se rompe.
4. **No esta en TOMWMS_BOF** explica por que el code-deep-flow no lo encontro: estabamos buscando en el repo equivocado.

## Hipotesis a validar

- Hipotesis A: vive en Azure DevOps en un repo dedicado tipo `TOMWMS_CodeGen` o `TomWMSGenerator`.
- Hipotesis B: vive como solucion VS adyacente al BOF, no versionada en su propio repo.
- Hipotesis C: vive en GitHub personal de Erik (no Azure DevOps corporativo).

## Q-* abiertas (preguntar a Erik directamente)

- Q-GENERADOR-REPO-EXACTO: nombre exacto del repo donde vive el generador.
- Q-GENERADOR-INPUTS (ya en cuestionario, Carolina no supo): ¿plantillas T4? ¿introspeccion SQL? ¿config files? — pendiente Erik o Efren.
- Q-GENERADOR-BRANCHES-ACTIVAS: ¿que ramas tienen Erik y Efren activas? ¿hay divergencia?
- Q-GENERADOR-PARA-WEB-BOF (Q32 cuestionario, Carolina no supo): ¿el generador se va a extender a una tercera variante para Web BOF?
- Q-GENERADOR-CICD: ¿el generador corre en CI o se ejecuta manual desde la maquina del dev?

## Por que esto importa para 2028

- Si DALCore (L-041) es el data layer del futuro y el generador es el productor de la capa BE/LN, **el generador necesita una variante .NET Core** (si no la tiene ya). Sin eso, todo lo nuevo que entre al WMSWebAPI se escribe a mano → riesgo de inconsistencia con el patron BE/LN.
- Si el generador vive fuera de Azure DevOps corporativo (Hipotesis C), es **bus factor 1-2** (Erik + Efren). Critical path para empezar a versionarlo formalmente.

## Vinculos

- L-041 (Wave 11): DALCore es .NET Core, generador probablemente lo alimenta.
- L-031 (Wave 10): master congelada — el generador trabaja sobre la master de cada cliente.

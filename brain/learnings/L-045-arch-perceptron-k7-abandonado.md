# L-045 — ARCH: el perceptron K7 fue abandonado, hoy se usa "metodo nuevo" de ubicacion sugerida

> Etiqueta: `L-045_ARCH_PERCEPTRON-ABANDONADO_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-K7-ML-MODELO / Q-PERCEPTRON-USO-REAL

## Hallazgo

El **perceptron** que aparece en codigo de K7 (referenciado en code-deep-flow) **NO esta en uso productivo hoy**.

- **Quien lo escribio**: Erik (EJC).
- **Para que era**: aprender como **ubicar producto** automaticamente (ubicacion sugerida en bodega).
- **Por que fallo**: a medida que las bases de datos crecieron, el modelo **dejo de ser funcional**. No escalo.
- **Que lo reemplazo**: Erik desarrollo un **metodo nuevo** de ubicacion sugerida, **mas funcional y eficiente**. Este metodo nuevo es el que esta vivo hoy.

## Cita literal Carolina (Wave 11)

> "Erik desarrollo esta neuronita para que aprendiera sobre como ubicar
> el producto, pero a medida que las bases de datos crecieron, no fue
> muy funcional y creo un nuevo metodo para la ubicacion Sugerida que
> quedo mucho mas funcional y eficiente."

(Notar el carineo de Carolina: "esta neuronita". El perceptron fue una iteracion experimental, parte de la historia personal de Erik con el WMS — candidate a entrada en `naked-erik-anatomy/`.)

## Implicaciones

1. **El codigo del perceptron sigue en repo K7** (probablemente en BOF o WSHHRN, branch 2025/master). NO hay que extenderlo, NO hay que confiar en su salida si alguien todavia lo invoca.
2. **El metodo nuevo de ubicacion sugerida NO esta documentado en el brain hasta hoy**. Hay que mapearlo (deep-flow).
3. **Patron general**: Erik tiene historial de probar enfoques avanzados (ML), descartarlos por escala/operatividad y reemplazarlos por algoritmica clasica. Esto informa estilo de desarrollo: experimentar permitido, mantener simple si lo experimental no escala.
4. **Si en code-deep-flow aparecen referencias a `clsPerceptron`, `RedNeuronal`, `entrenarUbicacion`, etc.**, marcarlas como **codigo muerto / no productivo**, no como funcionalidad activa.

## Q-* abiertas

- Q-UBICACION-SUGERIDA-METODO-ACTUAL: ¿cual es el algoritmo del "metodo nuevo" de ubicacion sugerida? ¿Donde vive el codigo? ¿Solo K7 o todos los clientes?
- Q-PERCEPTRON-CODIGO-LIMPIAR: ¿el codigo viejo del perceptron se puede eliminar del repo o es referencia historica intocable?
- Q-PERCEPTRON-DATOS-RESIDUALES: ¿hay tablas tipo `perceptron_pesos`, `perceptron_entrenamiento` que persisten datos del modelo viejo? ¿Se pueden tirar?

## Para naked-erik-anatomy

Candidate a entrada: **"La neuronita que envejecio mal"** — historia de Erik intentando ML para ubicacion sugerida, descubriendo que el operador concreto y la algoritmica clasica le ganaban a la red, y volviendo al pragmatismo. Tono: humor + autocritica honesta.

## Vinculos

- L-031 (Wave 10): master congelada — el perceptron quedo en master pero descongelar es riesgo.
- L-032 (Wave 10): ramas cliente HH obsoletas — el perceptron es analogo (codigo dormido).
- code-deep-flow: revisar referencias a perceptron y marcarlas como muertas.

# L-043 — ARCH: modelo nuevo de licencias = licencias por Presentacion (MHS-driven), parametro IdTipoEtiqueta

> Etiqueta: `L-043_ARCH_LICENCIAS-POR-PRESENTACION_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-LICENCIAS-MODELO-NUEVO

## Hallazgo

El "modelo nuevo de licencias" mencionado en codigo y conversaciones 2028 es **licencias por Presentacion**. Su consumer principal es **MHS** (L-038, primer cliente WMSWebAPI).

**Mecanismo de implementacion**:
- Nueva columna en `producto_presentacion`: **`IdTipoEtiqueta`**.
- Permite definir un diseño de etiqueta por presentacion, distinto al diseño tradicional de licencia (a nivel producto / lote).

## Cita literal Carolina (Wave 11)

> "Esto hace referencia a las licencias por Presentaciones que va a
> utilizar MHS, para esto agregamos un parametro en
> producto_presentacion `IdTipoEtiqueta` para poder definir el diseño
> que va a ser distinta a la de la licencia."

## Por que es nuevo

Modelo legacy: la licencia se imprime con un diseño global (o por bodega). Una caja de Producto X = una etiqueta segun el diseño global.

Modelo nuevo: cada **presentacion** del producto puede tener **su propia etiqueta**. Producto X presentacion "caja 12u" tiene un diseño; presentacion "blister 6u" otro.

→ Granularidad mas fina = mas combinaciones de etiquetas a mantener pero mas precision visual para el cliente final.

## Implicaciones tecnicas

1. **Tabla afectada**: `producto_presentacion` — agregar columna `IdTipoEtiqueta` (FK a tabla de tipos de etiqueta, cuyo nombre Carolina no menciono — probable `tipo_etiqueta` o `cat_tipo_etiqueta`).
2. **Codigo afectado**:
   - Modulo de impresion de licencias (BOF + HH).
   - WMSWebAPI: el endpoint que MHS usa para consultar/escribir presentaciones debe exponer `IdTipoEtiqueta`.
   - Diseñador de etiquetas: si existe (tipo Crystal Reports, Bartender, Loftware), debe soportar la nueva relacion.
3. **Migracion datos legacy**: clientes que ya tienen presentaciones existentes sin `IdTipoEtiqueta` deben recibir un default. ¿Cual? — Q nueva.
4. **Esto es ejemplo concreto de L-035**: NO se codifica `if Cliente = "MHS"`, se agrega un parametro (`IdTipoEtiqueta`) y se aplica a quien lo use. Si CEALSA quiere licencias por presentacion mañana, solo setea el parametro.

## Doble estreno MHS

MHS estrena simultaneamente (L-038):
- WMSWebAPI como canal de integracion (primer cliente).
- Licencias por presentacion (este learning).

→ **Combinatoria de riesgo**. Si algo falla cerca del go-live 20-ago, hay que aislar cual de los dos novedades es la causa.

## Q-* abiertas

- Q-LICENCIAS-TIPO-ETIQUETA-TABLA: nombre exacto de la tabla de tipos de etiqueta.
- Q-LICENCIAS-DISEÑADOR: ¿con que herramienta se diseñan las etiquetas? ¿Bartender, Crystal, Loftware, custom?
- Q-LICENCIAS-IMPRESORA-MHS: ¿que impresoras usa MHS? Zebra, Datamax, otra?
- Q-LICENCIAS-MIGRACION-DEFAULT: para presentaciones legacy sin IdTipoEtiqueta, ¿cual es el default?
- Q-LICENCIAS-RETROCOMPATIBLE: ¿el modelo viejo (etiqueta global) sigue funcionando para clientes que no quieren migrar?

## Vinculos

- L-035: NO codificar por cliente — `IdTipoEtiqueta` es ejemplo prototipico.
- L-038: MHS, primer consumer.

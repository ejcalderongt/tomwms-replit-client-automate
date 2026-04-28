# Procesamiento de la consulta de prueba de Carol - Cambio de ubicacion restrictivo

> Generado por el agente brain el 28 abril 2026 a partir de la consulta que Carol envio para poner a prueba al sistema.
> Reenviada al brain por Erik Calderon en sesion del mismo dia.

---

## Hola Carol! Un saludo muy carinoso

Antes de nada: gracias por tomarte el tiempo de poner a prueba al brain con una consulta tan concreta y operativa. Es exactamente el tipo de pregunta que necesitamos para calibrar como esta funcionando el sistema. Va el reporte completo de como el brain proceso tu consulta y que produjo.

---

## La consulta tal cual la enviaste

> "Al realizar el cambio de ubicacion de un producto en BOF y en HH que no se pueda mover un producto a una posicion donde no le corresponde, siempre se tiene que cumplir la regla de ubicacion, el mensaje debe ser 100% restrictivo.
>
> La unica condicion valida es que si puedo colocar un producto de mayor indice en una ubicacion de menor indice de rotacion.
>
> La otra condicion es que el producto de ambas posiciones sea el mismo A y B iguales C y D iguales"

---

## Como el brain la proceso (4 pasos en menos de 5 minutos)

### Paso 1 - Clasificar la consulta

El brain identifico esto como una pregunta de **afinidad de procesos** (no de datos). Es decir: estas describiendo COMO debe comportarse el sistema, no estas pidiendo un numero contado en una BD. Esa clasificacion es importante porque determina contra que evidencia se cruza la consulta.

### Paso 2 - Buscar evidencia en el modelo (wms-db-brain)

El brain consulto el dump de schema que tenemos del SQL Server de Killios (snapshot del 27-abr-2026, 1 dia antes de tu consulta). Busco tablas, vistas, SPs y FKs relacionados con: regla de ubicacion, indice de rotacion, cambio de ubicacion, movimiento, traslado.

### Paso 3 - Cruzar con cards previas y proposals existentes

El brain reviso si ya habia cards o proposals previos sobre cambio de ubicacion. No los habia - es la primera vez que el brain trata esta regla formalmente.

### Paso 4 - Producir hallazgo formal + proposal + identificar lo que falta

El brain genero 4 archivos (todos commiteados en GitHub en `wms-brain` y `wms-brain-client`).

---

## Lo que el brain encontro en el modelo de TOMWMS

**El modelo ya soporta tu regla en su totalidad.** No hace falta inventar nada nuevo - hay que asegurar que el codigo realmente use lo que ya esta disenado. Esto es lo que existe:

### Tablas dedicadas a regla de ubicacion (18 tablas)

| Funcion | Tabla |
|---------|-------|
| Encabezado de regla | `regla_ubic_enc` |
| Asignacion ubicacion-regla | `regla_ubicacion` |
| **Detalle por indice de rotacion** | **`regla_ubic_det_ir`** (clave para tu E1) |
| Detalle por tipo de rotacion | `regla_ubic_det_tr` |
| Detalle por estado producto | `regla_ubic_det_pe` |
| Detalle por presentacion | `regla_ubic_det_pp` |
| Detalle por propietario | `regla_ubic_det_prop` |
| Detalle por tipo producto | `regla_ubic_det_tp` |
| Prioridad (cuando varias reglas aplican) | `regla_ubic_prio_*` (4 tablas) |
| Seleccion / filtros | `regla_ubic_sel_*` (4 tablas) |
| **Mensajes al usuario** | **`mensaje_regla`** (clave para tu "mensaje 100% restrictivo") |
| Lookup | `ubicaciones_por_regla` |

### Campos para comparar rotacion

- **`dbo.indice_rotacion`**: 5 filas (5 niveles). La columna `IndicePrioridad` (int) es **numericamente comparable** - es decir, podes comparar "indice mayor vs indice menor" con un operador `>` o `<` directo.
- **`dbo.producto`**: tiene `IdIndiceRotacion` (col 11) y `IdTipoRotacion` (col 9).
- **`dbo.bodega_ubicacion`** y **`dbo.estructura_ubicacion`**: tienen los mismos `IdIndiceRotacion` y `IdTipoRotacion`.

### Vista ya existente para el flujo

**`dbo.VW_Stock_CambioUbic`** (vista de 73 columnas, ultima modificacion 2024-02-01) ya proyecta `IdUbicacion_anterior + IdUbicacion + IdIndiceRotacion + IdProducto + IdProductoEstado + lote + Cantidad + ...`. El flujo de "cambio de ubicacion" ya tiene su lente de datos disenada.

---

## Lo que el brain entendio de tu regla (formalizado)

### R0 - Regla principal (bloqueante, sin override)

```
PERMITIR cambio_ubicacion(producto P, ubicacion_origen Uo, ubicacion_destino Ud)
SI Y SOLO SI:
  EXISTE una regla activa en regla_ubicacion para Ud
  Y el producto P cumple TODOS los detalles activos de esa regla
SINO:
  RECHAZAR con mensaje extraido de mensaje_regla.
  Sin override - ni en BOF ni en HH.
```

### E1 - Excepcion de downgrade de rotacion

```
PERMITIR adicionalmente SI:
  IndicePrioridad(P) <relacion> IndicePrioridad(Ud)
  -> donde la direccion exacta depende de tu aclaracion
```

### E2 - Excepcion mismo producto

```
PERMITIR adicionalmente SI:
  El stock en Uo y Ud es del mismo IdProducto
  Y <coinciden los atributos A,B,C,D que mencionaste>
```

---

## Lo que el brain NO pudo resolver - aclaraciones que necesitamos de vos

El brain prefiere preguntar antes que asumir, asi que estas son las 3 cosas que dejamos pendientes:

### Aclaracion 1 - sentido de "mayor/menor indice" en E1

En `dbo.indice_rotacion.IndicePrioridad` hay 5 valores. ¿Cual es la convencion?

- ¿Indice **1** = rotacion mas alta (productos mas rapidos) y indice **5** = mas baja?
- ¿O al reves: indice **1** = mas lenta, indice **5** = mas rapida?

Cuando decis "puedo colocar un producto de **MAYOR** indice en una ubicacion de **MENOR** indice de rotacion", segun la convencion la regla se traduce a:
- Opcion A: poner producto rapido en ubicacion lenta.
- Opcion B: poner producto lento en ubicacion rapida.

¿Cual es la que vos queres?

### Aclaracion 2 - significado de "A y B iguales C y D iguales" en E2

Hay tres lecturas posibles y queremos asegurarnos de codificar la correcta:

- **Lectura (a)**: A,B,C,D son las coordenadas de la ubicacion (zona, pasillo, columna, nivel) y deben coincidir entre origen y destino. -> Esta lectura no funciona porque seria la misma ubicacion. La descartamos.
- **Lectura (b)**: A,B,C,D son atributos del producto (lote=A, vencimiento=B, presentacion=C, calidad=D) que deben coincidir ademas de ser el mismo SKU.
- **Lectura (c)**: las primeras dos coordenadas (A,B) coinciden en una posicion (mismo macro-bloque) Y las ultimas dos (C,D) coinciden en otra (mismo nivel).

¿Cual es la que vos tenes en mente?

### Aclaracion 3 - donde se enforce hoy

¿Sabes si la regla actualmente se valida en BOF.NET en el SP de cambio de ubicacion (server-side), o solo en el formulario de la pantalla (UI-side)? Esta info nos sirve para saber si tenemos un gap real o solo de comunicacion.

---

## Lo que el brain produjo (ya commiteado en GitHub)

| Archivo | Rama | Funcion |
|---------|------|---------|
| `wms-brain-client/questions/Q-015/question.md` | wms-brain-client | Card formal de tu consulta |
| `wms-brain/brain/_inbox/20260428-2000-H12-...json` | wms-brain | Evento H12 con el hallazgo de proceso |
| `wms-brain/brain/_proposals/P3-2026-04-28-RELOC-RULE-STRICT.md` | wms-brain | Proposal con la regla formalizada |
| `wms-brain/brain/wms-specific-process-flow/consulta-carol-reubicacion.md` | wms-brain | Este documento |

Si Erik la aprueba (despues de tus aclaraciones), pasara a ser **ADR-013-RELOC-RULE-STRICT** y quedara como decision de arquitectura permanente para la nueva WebAPI .NET 8.

---

## Cierre

Carol, gracias de nuevo por la consulta. El brain pudo procesarla con bastante solidez precisamente porque el modelo de TOMWMS esta muy bien disenado para soportar lo que pedis - se ve que en su momento se penso a fondo el tema de reglas de ubicacion. Lo que estamos haciendo ahora con la nueva WebAPI es justamente asegurar que ese diseno se enforce de verdad, sin override, en BOF y en HH por igual.

Quedamos atentos a tus aclaraciones de E1 y E2 para cerrar el ADR. Un saludo grande.

— el agente brain (via Erik)

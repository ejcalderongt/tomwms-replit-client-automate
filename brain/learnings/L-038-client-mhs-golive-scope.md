# L-038 — CLIENT: MHS GoLive 20-ago-2026, primer cliente del WMSWebAPI, scope cerrado

> Etiqueta: `L-038_CLIENT_MHS-GOLIVE-SCOPE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-MHS-COMO-CLIENTE

## Hallazgo

**MHS es el primer cliente del WMSWebAPI** (no de TOMWMS — el BOF/HH son legacy). Fecha go-live: **20 de agosto de 2026**. Scope cerrado y conocido.

## Cita literal Carolina (Wave 11)

> "Si MHS es el primer cliente del WMSWebApi, la fecha de GoLive esta para el 20 de agosto."
>
> Datos maestros:
> - Productos
> - Familias
> - Clasificacion
> - Tipos
> - Marcas
> - Presentaciones
> - Codigos de barra
> - Clientes
> - Proveedores
>
> Transacciones:
> - Ingresos de produccion
> - Traslados entre bodegas
> - Pedidos de clientes
> - Ajustes de inventario
>
> "Tengo entendido que Erik esta terminando de depurar los traslados."

## Implicaciones

### 1. WMSWebAPI deja de ser piloto

A partir del 20-ago-2026 hay un cliente real escribiendo y leyendo. Cualquier breaking change en el contrato OpenAPI a partir de esa fecha tiene impacto productivo.

### 2. Scope MAESTROS (9 entidades, escritura desde MHS hacia WMS)

```
Productos
Familias
Clasificacion  (¿categorias?)
Tipos          (¿tipo_producto?)
Marcas
Presentaciones
Codigos de barra
Clientes
Proveedores
```

→ Cada uno necesita endpoint REST en WMSWebAPI con validacion Zod/OpenAPI. Hay que verificar que TODOS esten implementados antes del 20-ago.

### 3. Scope TRANSACCIONES (4 flujos, lectura desde WMS hacia MHS, posiblemente bidireccional)

```
Ingresos de produccion        ← MHS produce, WMS recibe
Traslados entre bodegas       ← WMS interno, MHS quiere ver  (Erik depurando)
Pedidos de clientes           ← MHS coloca, WMS picks
Ajustes de inventario         ← WMS detecta, MHS reconcilia
```

### 4. Ruta critica: Erik depurando traslados

**Riesgo de timing**: si el bug de traslados no se cierra antes de fines de julio, hay riesgo de slip de go-live. Vale la pena agregar Q-* de seguimiento.

### 5. Vinculo con licencias por presentacion (L-043)

MHS tambien estrena el modelo nuevo de licencias por presentacion (`producto_presentacion.IdTipoEtiqueta`). Es decir, MHS es **doble estreno**: WMSWebAPI + licencias por presentacion. Riesgo de combinatoria.

## Q-* abiertas (bloque 14 cuestionario)

- Q-MHS-TRASLADOS-STATUS: status del bug de traslados que Erik esta depurando. Repro? Branch? PR?
- Q-MHS-WEBAPI-ENDPOINTS-CHECKLIST: lista de los 9 maestros + 4 trans, marcar cuales endpoints ya existen y cuales faltan. Antes del 20-ago.
- Q-MHS-AUTENTICACION: ¿como se autentica MHS contra WMSWebAPI? API key? OAuth? mTLS?
- Q-MHS-AMBIENTE: ¿hay ambiente QAS dedicado para MHS o comparte con BYB/IDEALSA/INELAC?
- Q-MHS-LICENCIAS-PRESENTACION-DISEÑO: ¿quien diseña las etiquetas (printers MHS, plantilla CEALSA, custom)?

## Vinculos

- L-043: licencias por presentacion (estreno conjunto con MHS).
- L-027 (Wave 10): MHS es el ultimo en el orden de migracion 2028 (despues de Cumbre, Beco, Clarispharma).
- Bloque 7 cuestionario Q-LICENCIAS-MODELO-NUEVO (resuelta esta wave).

# L-047 — META: CEDIS no es pantalla legacy del WMS, es Centro de Distribucion (terminologia de cliente)

> Etiqueta: `L-047_META_CEDIS-ES-CENTRO-DISTRIBUCION_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-MAMPA-CEDIS-PANTALLA-LEGACY

## Hallazgo

La hipotesis del cuestionario "CEDIS pantalla legacy especifica de MAMPA" era equivocada en framing. **CEDIS = Centro de Distribucion**, terminologia operativa del cliente, no etiqueta interna del WMS.

Concretamente en MAMPA, el CEDIS es el lugar fisico donde:
1. Se reciben productos de proveedores.
2. Se realiza el **proceso de costeo**.

## Cita literal Carolina (Wave 11)

> "El Cedis hace referencia a su centro de distribucion, donde reciben
> el producto de sus proveedores y alli es que se realiza el proceso
> de costeo."

## Implicaciones

1. **Si en BOF aparece una pantalla "CEDIS"**, NO es legacy en el sentido peyorativo. Es la pantalla **operativa** que maneja el flujo recepcion + costeo en MAMPA. Esta vivo, esta soportado.
2. **El proceso de costeo en MAMPA vive en el CEDIS**. Esto es input importante: no es costeo distribuido por bodega, es centralizado en el CEDIS al recibir.
3. **Otros clientes pueden tener su propio CEDIS o llamarlo distinto** (Centro de Distribucion, CD, Hub). Si Carolina enumera mas adelante, agregar al brain.
4. **Para el agente**: cuando lea `frmCedis`, `cls_Cedis_*`, `oc_cedis`, etc., interpretar como "operacion de centro de distribucion del cliente", no como "pantalla heredada que hay que matar".

## Patron mas amplio: terminologia de cliente vs terminologia de WMS

Esto se conecta con L-025 (NAV no es NAV) y L-035 (no codificar por cliente). El WMS tiene **dos vocabularios coexistentes**:

| Vocabulario | Origen | Ejemplos |
|---|---|---|
| WMS interno | Erik / equipo PrograX24 | `licencia`, `propietario`, `bodega`, `picking`, `stock_jornada` |
| Cliente | operacion de cada empresa | `CEDIS`, `Almacen Fiscal`, `Factura_Reserva_Proveedor`, `Stock en linea` |

**El brain debe respetar ambos** y traducir cuando sea necesario. No reemplazar terminologia de cliente por la del WMS o viceversa — es contexto operativo importante.

## Q-* abiertas

- Q-CEDIS-COSTEO-FORMULA: ¿como se calcula costeo en CEDIS MAMPA? ¿Promedio ponderado, FIFO, ultimo costo?
- Q-CEDIS-OTROS-CLIENTES: ¿que otros clientes tienen CEDIS u operacion equivalente?
- Q-VOCABULARIO-CLIENTE-MAPEO: ¿hay un glosario interno PrograX24 que mapee terminologia cliente ↔ WMS? ¿O hay que reconstruirlo?

## Vinculos

- L-025: prefijo Nav — caso analogo de terminologia historica que sobrevive.
- L-046: K7 OC tipos — otra muestra de vocabulario cliente (Almacen Fiscal, Poliza, NC, Factura Reserva Proveedor).
- L-044: MAMPA usa SAP HANA y CEDIS, ambos terminologia cliente.

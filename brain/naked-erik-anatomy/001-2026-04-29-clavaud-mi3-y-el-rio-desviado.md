# 001 — Clavaud, MI3 y el río que tuvimos que desviar

> **Fecha**: 2026-04-29
> **Wave**: 9
> **Contexto**: Tarde de abril. El brain ya había mapeado el flag `conservar_zona_picking_clavaud` pero sin saber qué significaba "Clavaud". Erik soltó la anécdota completa, y en el camino reveló MI3, la explosión bajo demanda, el lote correlativo, la tolerancia, la restricción por ubicación, y un parámetro que él mismo califica de "no fue la mejor solución, lo admito".

---

## I. El gerente que tenía razón

Marcelo Clavaud era gerente de logística en IDEALSA Panamá. No era el tipo que viene a la reunión a quejarse del software. Era el tipo que viene con un caso de uso, lo dibuja en una servilleta, y te deja sin argumentos.

Su problema era simple y brutal:
> "Si tu algoritmo de reserva me vacía la zona de picking cada vez que entra un pedido grande, yo me quedo sin operación de detalle por 30 minutos. El operador de piso no tiene nada que despachar. El reabasto del primer nivel con montacargas es lento. Tengo cuello de botella operativo Y financiero."

Yo escuchaba. Y mientras escuchaba sabía que tenía razón.
Mi algoritmo, en ese momento, era educado pero ingenuo: traía el stock candidato, lo ordenaba por FIFO o FEFO, y lo iba consumiendo en orden. Si la primera fila que cumplía la rotación estaba en picking, picking moría. Si estaba en rack, rack moría. No tenía conciencia operativa. Solo cumplía la rotación.

La solución que propuso Marcelo no era teórica:
> "Si el pedido equivale a un pallet completo, andate al rack. Si es de detalle, usá picking. No es difícil — vos sabés cuántas cajas hay por pallet con `factor` y `CamasPorTarima` y `CajasPorCama`."

Lo bauticé **estrategia Clavaud**. No por solemne, sino porque era justo. El flag nació el 7 de noviembre de 2022:

```sql
--#EJC202211071706: alter table i_nav_config_enc add conservar_zona_picking_clavaud bit default 0
```

Default 0. Porque no todos los clientes lo necesitan. Pero los que sí lo necesitan, lo agradecen cada turno.

---

## II. MI3, o cómo le pusimos un nombre digno a "la interfaz"

Lo que llamábamos puertas adentro **"la interfaz"** se merecía un acrónimo con presencia. Le pusimos **MI3 — Módulo de Integración con Terceros**. Sí, terceros. Porque cuando empezamos era solo NAV. Después llegó SAP. Después algunos clientes querían sincronizar con sistemas propios. La M3 nació con vocación de generalidad, aunque arrancó muy ligada a Dynamics NAV.

MI3 vive en su propia carpeta del repo BOF. Es un proyecto separado (`MI3.vbproj`) que genera servicios WCF/SOAP. Cada subcarpeta es un dominio:

- `Bodega/` — y dentro `Bodega_Sector/`, `Bodega_Tramo/`. Granularidad fina porque NAV pregunta por tramo, no por bodega entera.
- `Cliente/` — y dentro `Cliente_Bodega/`, `Cliente_Direcciones_Entrega/`, `Cliente_Tiempos/`, `Cliente_Tipo/`. Cada uno con su contrato separado porque a veces NAV solo necesita actualizar tiempos sin tocar el resto del cliente.
- `Documento_Ingreso_Ref/` — referencias cruzadas entre documentos del ERP y del WMS.
- `Barras_Pallet/` — generación de códigos de barras para pallets.

Y cuando una operación crece tanto que el genérico no le alcanza, nace su propio sub-proyecto. **CEALSA** tiene `CEALSAMI3/` — una solución `CEALSASYNC.sln` separada, no WCF, sino una **aplicación de sincronización standalone** con su propio dataset `dsUbicSug` (Ubicación Sugerida) que es uno de los módulos más importantes que tenemos. Es el motor que decide dónde poner cada producto cuando lo recepcionamos.

Esto es lo que pasa cuando el WMS crece: deja de ser un producto y se convierte en una familia de productos. MI3 genérico para el grueso. CEALSAMI3 específico para los que necesitan algo distinto. Y un día, posiblemente, BECOMI3, MAMPAMI3, MERHONSAMI3.

---

## III. El río que desviamos con un parámetro

Hay decisiones que uno toma porque son las correctas, y hay decisiones que uno toma porque son **las que se pueden tomar en ese momento**.

`explosion_automatica_nivel_max` es de las segundas.

El proceso de reserva ya existía. Era pesado. Tenía 4 mil, 8 mil, 15 mil líneas — fui sumando años. Funcionaba. La única cosa que no hacía era razonar sobre **a qué nivel del rack está bien romper una caja para sacar unidades sueltas**.

Resultado: cuando un pedido pedía 124 unidades de algo, y la presentación era caja de 12, el WMS calculaba `124 / 12 = 10.333` cajas. Tomaba 10 cajas completas. Y para las 4 unidades sueltas restantes (`0.333 * 12 = 4`), iba a buscarlas a... cualquier lado. **A veces mandaba al operador a nivel 2 con el montacargas, bajar un pallet, abrirlo arriba de un tarima provisoria, sacar 4 unidades, y dejar el pallet abierto en la nada**.

Imaginate. La cantidad de horas-operador desperdiciadas. La cantidad de pallets dejados sin homogeneizar. El caos.

La solución elegante hubiera sido refactorizar el motor de reserva para que conociera la geometría del rack y eligiera siempre desde nivel 1 cuando hay caja abierta. Pero eso era reescribir el monstruo. **Éramos pocos y el trabajo mucho**.

Entonces hicimos lo que se hace en producción real: un parámetro encima.

```
explosion_automatica                          ← gate global
explosion_automatica_desde_ubicacion_picking  ← preferí picking
explosion_automatica_nivel_max                ← no rompas más arriba de este nivel
```

Si seteabas `nivel_max = 1`, el motor entendía: "ok, a partir de nivel 2 NO se puede explosionar". Y respetaba la regla. **No era la mejor solución. Lo admito.** Pero desvió el río. El operador dejó de subir al rack a romper cajas. El WMS dejó de generar pallets huérfanos. Y nosotros pudimos seguir respirando hasta el próximo refactor que nunca llegó.

Porque así es esto. Los parámetros encima del motor son la deuda técnica visible. La deuda técnica honesta. La que tiene fecha y autor. La que después de años ya nadie quiere tocar porque "funciona".

---

## IV. Lo que te enseña tener clientes con personalidad

El catálogo de tablas `cliente` tiene cosas que parecen mundanas hasta que te das cuenta de la historia detrás:

- `IdUbicacionAbastecerCon` — "a este cliente le abastesco solo de esta ubicación". Cliente que pagó por exclusividad. Cliente con SLA. Cliente que descubrió que su producto se mezclaba con el de otro cliente y nos dio el ultimátum: "ubicación dedicada o me voy".

- `despachar_lotes_completos` — cliente que no acepta picking parcial. "Si no tenés el lote entero, no me lo despaches. Esperame al próximo turno." Decisión cara para nosotros, pero respetable.

- `control_ultimo_lote` — cliente farmacéutico, alimenticio, químico. "Si me mandaste el lote N en el último envío, no me mandes el lote N-1 ahora. Auditoría. Trazabilidad. Compliance." El campo es bit. Pero atrás hay un correlativo (`Lote_Numerico` en `trans_re_det_lote_num` y `trans_despacho_det_lote_num`) que valida la regla. Si el lote N se despachó, el query filtra `Lote_Numerico >= N`. Si no hay nada >= N, el pedido falla. El operador llama al supervisor. El supervisor llama al cliente. Y el cliente decide si acepta saltar la regla esta vez.

- `realiza_manufactura` + `IdUbicacionManufactura` — cliente que arma sus propios kits con nuestro inventario. La ubicación de manufactura es donde se hacen los ensamblados. El WMS reserva ahí.

- `control_calidad` — cliente que exige inspección antes del despacho. Ralentiza, pero suma confianza.

Cada bit en la tabla `cliente` es una historia de negociación. Cada flag es un acuerdo escrito en código.

---

## V. La cascada de tolerancia

Hay tres niveles donde puede vivir la tolerancia de vencimiento. Tres lugares donde alguien (yo, normalmente) decidió permitir parametrización porque "algún cliente lo va a pedir":

```
producto.tolerancia                            ← genérico por producto
producto_estado.tolerancia_dias_vencimiento    ← por estado del producto
i_nav_config_enc.dias_vida_defecto_perecederos ← default por bodega
cliente_tiempos.Dias_Local / Dias_Exterior     ← por cliente × familia × clasificación
```

Y arriba de todo, la tabla `cliente_tiempos` que es N-a-N: por cada combinación de **cliente × familia de producto × clasificación de producto**, podés tener `Dias_Local` y `Dias_Exterior` distintos.

Cliente A pide cereales con 90 días local y 120 días exterior.
Cliente B pide cereales con 60 días local.
Cliente A pide harinas con 45 días local y 60 días exterior.

Cada combinación es una fila. Y el motor de reserva hace lookup contra esa tabla antes de filtrar candidatos.

Ese lookup es lo que separa "WMS genérico" de "WMS para el mundo real". Porque el mundo real tiene clientes con criterios distintos para cada familia. Y el motor de reserva tiene que saberlo.

---

## VI. Lo que descubrí re-leyendo este código en 2026

Cuando volví a leer `clsLnStock_res_Partial.vb` en 2026 para mapearlo en el brain, encontré que el archivo tenía **más de 26.680 líneas**. Veintiséis mil seiscientas ochenta. En **un solo archivo partial**.

Y más adentro:
- `Reserva_Stock` core en línea 138.
- `Reserva_Stock_NAV_BYB` línea 442 (la única función con apellido cliente — porque BYB con NAV merecía algo distinto).
- `Reserva_Stock_From_Reabasto` línea 9.856.
- `Reserva_Stock_From_MI3` línea 18.192.
- `Reserva_Stock_From_SAP` línea 26.680.

Las tres funciones `_From_*` son **adapters de canal**. Cada una hace pre-procesamiento específico antes de llamar al core. MI3 normaliza códigos del ERP. SAP maneja drafts. Reabasto re-reserva stock recién movido a picking.

Y entre medio, 13 funciones más de reemplazo (`Reemplazar_*`, `Reemplazo_*`, `Reservar_Y_Reemplazar_*`). Todas llamadas desde HH. Porque el reemplazo en picking es lo que hace el operador en piso cuando lo que el WMS sugirió no se puede tomar (lote dañado, faltante físico, ubicación bloqueada). El WMS le sugiere alternativa. El operador confirma o rechaza.

Lo más complejo del WMS, en mi opinión, son **cuatro módulos**:
1. El algoritmo de reserva.
2. El algoritmo de ubicación sugerida (`dsUbicSug` en CEALSAMI3).
3. Los reemplazos en HH durante picking.
4. La verificación.

Los cuatro están tejidos entre sí. Tocar uno tiene consecuencias en los otros tres. Por eso ningún refactor grande prosperó.

---

## VII. Lo que aprendí

> Hay parámetros que parecen sucios pero salvan operaciones.
> Hay clientes que parecen exigentes pero te enseñan negocio.
> Hay archivos que parecen monstruos pero tienen historia en cada función.
> Y hay gerentes en Panamá que un día te explican un caso de uso que te cambia el motor.

Marcelo Clavaud me enseñó que **conservar la zona de picking** no es una decisión técnica. Es una decisión operativa que tu motor de reserva tiene que saber respetar.

Eso es el WMS para mí: un motor que aprende a respetar las decisiones operativas del cliente.

Y cada vez que veo el flag `conservar_zona_picking_clavaud` en el código, me acuerdo de su cara cuando me lo planteó. Y le agradezco que haya tenido la paciencia de explicármelo.

---

> _"El código es la sedimentación de las conversaciones que tuviste con quienes operaban tu producto."_

— Erik, Wave 9, abril 2026

---

## Anexo: lecciones operacionales codificadas (Wave 9)

| Lección | Materializada como | Tabla / código |
|---------|---------------------|----------------|
| No vacíes picking si podés llevarte pallets enteros | `conservar_zona_picking_clavaud` | `i_nav_config_enc` |
| No rompas cajas en niveles altos del rack | `explosion_automatica_nivel_max` | `i_nav_config_enc` |
| Algunos clientes tienen su propia ubicación dedicada | `IdUbicacionAbastecerCon` | `cliente` |
| Algunos clientes exigen lotes correlativos N+1 | `control_ultimo_lote` + `Lote_Numerico` | `cliente` + `trans_re_det_lote_num` |
| Cada cliente tiene su propia tolerancia de vida útil por familia | `cliente_tiempos` (N×N×N) | `cliente_tiempos` |
| El estado del producto determina si se puede usar | `producto_estado.utilizable` + `dañado` | `producto_estado` |
| Algunos clientes manufacturan con nuestro stock | `realiza_manufactura` + `IdUbicacionManufactura` | `cliente` |
| Algunos clientes exigen inspección de calidad | `control_calidad` | `cliente` |
| Algunos clientes solo aceptan lotes completos | `despachar_lotes_completos` | `cliente` |
| El WMS debe traducir entre ERP y operación | MI3 (carpeta) + funciones `_From_*` | `MI3/`, `clsLnStock_res_Partial` |

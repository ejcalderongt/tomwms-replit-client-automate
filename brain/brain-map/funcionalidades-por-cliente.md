---
id: funcionalidades-por-cliente
tipo: brain-map
estado: vigente
titulo: Matriz funcionalidad × cliente — TOMWMS
tags: [brain-map]
---

# Matriz funcionalidad × cliente — TOMWMS

> Ultima actualizacion: 29-abr-2026
>
> **NOTA CRITICA**: las celdas marcadas con `(?ec2)` indican que la
> conclusion se baso en la copia EC2 (`52.41.114.122,1437`) y NO en
> la productiva real del laptop de Erik. EC2 es copia parcial. Toda
> celda con `(?ec2)` es **probable** pero requiere ratificacion.
>
> Leyenda: ON / OFF / PARCIAL / `?` (DESCONOCIDO)

## A — Procesos core de pedido (salida)

| Funcionalidad | K7 (Killios) | BB (B&B) | BECOFARMA | C9 (Cealsa-QAS) | Evidencia |
|---|---|---|---|---|---|
| Recibe pedidos del ERP via outbox/interface | ON | ON | ON | ON | L-008, todos tienen `trans_pe_enc` poblada |
| Estado `NUEVO` -> `Pendiente` | ON | ON | ON | ? | conteos en `trans_pe_enc.estado` |
| Estado `Pendiente` -> `Pickeado` | ON | ON | ON | ? | conteos |
| Estado `Pickeado` -> `Verificado` (paso intermedio) | **ON (?ec2 dice OFF, screenshot dice ON)** | ?ec2 OFF | ON (4 filas en `Verificado`) | ? | screenshot Erik 29-abr-2026 muestra `trans_verificacion_etiqueta` poblada en KILLIOS_PRD; en EC2 esta vacia o no existe |
| Estado `Pickeado/Verificado` -> `Despachado` | ON | ON | **ROTO desde W12-mar-2026** | ? | H29 evidencia operativa |
| Devoluciones con `IdPedidoEncDevol` | ? | ? | ? | ? | tabla tiene la columna pero no investigamos |

## B — Procesos core de ingreso (entrada)

| Funcionalidad | K7 | BB | BECOFARMA | C9 | Evidencia |
|---|---|---|---|---|---|
| Recibe OC del ERP | ON | ON | ON | ? | `trans_oc_enc` poblada |
| Recepcion -> outbox INGRESO | ON | ON | ON | ? | L-008, L-017 |

## C — Verificacion de etiquetas (modulo HH)

> Erik 29-abr-2026: "es CORE-WMS parametrizable por cliente, ligada
> al HH. Durante verificacion se puede emitir una licencia (license
> plate) que hace unico lo que se esta verificando."
>
> Tablas asociadas: `trans_verificacion_etiqueta`, `verificacion_estado`,
> `log_verificacion_bof`.

| Cliente | Modulo verificacion ON/OFF | Evidencia primaria | Evidencia secundaria |
|---|---|---|---|
| **K7** | **ON** | Screenshot Erik 29-abr-2026: `select * from trans_verificacion_etiqueta` en TOMWMS_KILLIOS_PRD devuelve 9 filas con datos reales (lic_plate F7000079-F7000083, FU05242, FU05020; lotes E5F7245, 1405C, 24249; verificaciones de enero 2026; user_agr=46) | EC2 dice "no existe" pero EC2 es copia parcial → ignorar |
| **BB** | **DESCONOCIDO** | EC2 dice "no existe" pero por L-014 y por extension del hallazgo K7, no es concluyente. Pendiente de validar con productiva real | — |
| **BECOFARMA** | **ON** | EC2: `verificacion_estado` (2 filas catalogo), `log_verificacion_bof` (84 filas), `trans_verificacion_etiqueta` (existe vacia en EC2). 4 pedidos en estado `Verificado` historicos | Modulo activo confirmado por Erik (capability flag por cliente) |
| **C9 (Cealsa-QAS)** | **DESCONOCIDO** | EC2 dice "no existe" pero por las mismas razones, no es concluyente. Es entorno QAS sin trafico | — |

### Si necesitas generar pruebas para verificacion de etiquetas:

- **K7**: SI valida etiqueta en verificacion. Generar caso de prueba
  que invoque el flujo `Pickeado -> Verificado` desde HH, escaneando
  un `codigo_barra_etiqueta` y validando que se cree fila en
  `trans_verificacion_etiqueta` con `lic_plate` no nulo.

- **BB**: pendiente confirmar con Erik antes de armar prueba. Si dice
  ON, mismo set que K7. Si dice OFF, no aplica (saltar).

- **BECOFARMA**: SI valida etiqueta en verificacion + es donde se uso
  (probablemente) hasta marzo 2026 cuando el flujo se rompio (ver H29).

- **C9-QAS**: pendiente confirmar.

## D — Licenciamiento de stock (license plate / lic_plate)

> Erik 29-abr-2026: "license plate hace unico lo que se esta verificando".
> Hallazgo brain: `lic_plate` cubre 99-100% de TODAS las transacciones
> de `i_nav_transacciones_out` en TODOS los clientes — es identificador
> universal, no exclusivo de verificacion.

| Funcionalidad | K7 | BB | BECOFARMA | C9 | Evidencia |
|---|---|---|---|---|---|
| `lic_plate` poblado en outbox INGRESO | ON 100% | ON 99.8% | ON 100% | ON (vacio outbox) | L-018 §Cobertura |
| `lic_plate` poblado en outbox SALIDA | ON 99.9% | ON 99.1% | ON 100% | ON (vacio) | L-018 |
| Tabla `licencia_item` (relacion lic↔producto) | ON (23) | ON (2) | ON (18) | ON (34) | EC2 |
| Tabla `licencia_login` (asignacion operador) | ON (38) | ON (3) | ON (37) | ON (105) | EC2 |
| Tabla `licencia_solic` (solicitudes) | ON (1) | ON (2) | ON (1) | OFF (vacia) | EC2 |
| Tabla `Licencias` (PascalCase, modelo nuevo?) | ?ec2 OFF | ?ec2 OFF | ON (5) | ON (261) | EC2 — pregunta para Erik si esto es el modelo nuevo y K7/BB migran |
| Tabla `licencia_pendientes` (cola por licencia) | ?ec2 OFF | ?ec2 OFF | ON (45) | ON (45) | EC2 |
| Tabla `producto_clasificacion_etiqueta` | OFF (vacia) | OFF (vacia) | ON (3) | ON (2) | EC2 |
| Tabla `tipo_etiqueta_detalle` | OFF (vacia) | OFF (vacia) | ON (3) | ?ec2 OFF | EC2 |
| Tabla `tmp_licencia_item` (staging temporal) | ON (65) | ?ec2 OFF | ?ec2 OFF | ?ec2 OFF | EC2 — exclusivo K7? |

### Lectura del patron:

- **Modelo de licencia core** (`licencia_item`, `licencia_llave`,
  `licencia_login`, `licencia_solic`, `licencias_pendientes_retroactivo`,
  `tipo_etiqueta`, `temp_licencia_llave`): TODOS los clientes lo tienen.

- **Modelo de licencia extendido** (`Licencias`, `LIcencias2`,
  `licencia_pendientes`): solo BECOFARMA y CEALSA. Probablemente
  modelo mas nuevo. Pregunta para Erik: ¿son tablas legacy en proceso
  de migracion, o features adicionales del modulo extendido?

- **`tmp_licencia_item` solo en K7**: probable tabla de trabajo
  temporal de algun proceso especifico de Killios. Pregunta para Erik.

## E — Logging y errores

| Funcionalidad | K7 | BB | BECOFARMA | C9 | Evidencia |
|---|---|---|---|---|---|
| `log_error_wms` (legacy unificado) | ON | ON | ON | ON | L-011 |
| `log_error_wms_pe` (segmentado pedido) | OFF | OFF | ON | OFF | L-016 — segmentacion fue mejora reciente solo aplicada en BECOFARMA todavia |
| `log_error_wms_rec` (segmentado recepcion) | OFF | OFF | ON | OFF | L-016 |
| `log_error_wms_reab` (segmentado reabasto) | OFF | OFF | ON | OFF | L-016 |
| `log_error_wms_ubic` (segmentado ubicacion) | OFF | OFF | ON | OFF | L-016 |
| `log_error_wms_pick` (segmentado picking) | OFF | OFF | ON | OFF | L-016 |
| `log_verificacion_bof` (BOF VB.NET) | ?ec2 OFF | ?ec2 OFF | ON (84) | ?ec2 OFF | EC2 |

## F — Interface al ERP (interfaces "outbound")

| Funcionalidad | K7 | BB | BECOFARMA | C9 | Evidencia |
|---|---|---|---|---|---|
| `i_nav_transacciones_out` (outbox) | ON 24K | ON 533K | ON 36K | ON vacio | L-008, L-017 |
| Binario sincronizador (.exe en ClickOnce) | SAPSync.exe (?) | NavSync.exe | SAPBOSync.exe | ? | L-009, L-010, L-012, L-015 |
| ERP destino | SAP B1 (solo enteros, L-009) | NAV (no procesa ingresos, L-010) | SAP B1 | ? | dichos learnings |
| Tabla de config interface por cliente | `i_nav_config_enc` | idem | idem | idem | L-015 §dispatch dinamico |

## G — Otros modulos (DESCONOCIDO o por explorar)

- Reabasto (`log_error_wms_reab` aparece en BECOFARMA → modulo activo).
- Ubicacion / putaway (`log_error_wms_ubic` en BECOFARMA).
- Picking (`log_error_wms_pick` en BECOFARMA, pero estado `Pickeado`
  esta en TODOS los clientes).
- Conteo ciclico (no investigado).
- Devoluciones (columnas `IdPedidoEncDevol`, `IdDespachoDet` en outbox
  pero no investigado uso real).
- Auditoria (columna `auditar bit NOT NULL` en outbox — no investigado).

## Preguntas abiertas levantadas por esta matriz

1. **Q-VERIF-K7**: confirmar que K7 productiva tiene `trans_verificacion_etiqueta`
   activa y operativa. El screenshot del 29-abr-2026 lo demuestra, pero
   ¿es siempre? ¿hay periodos en que el modulo se desactivo?

2. **Q-VERIF-BB**: ¿BB tiene modulo verificacion ON o OFF?

3. **Q-VERIF-C9**: ¿CEALSA tiene modulo verificacion ON o OFF?

4. **Q-CAPABILITY-FLAG**: ¿donde vive el capability flag de
   "verificacion_etiqueta_activa" por cliente? Probable: campo bit en
   `i_nav_config_enc` o tabla similar. Necesario para que la WebAPI
   nueva sepa que endpoints exponer.

5. **Q-LICENCIAS-MODELO-NUEVO**: las tablas `Licencias`/`LIcencias2`/
   `licencia_pendientes` (PascalCase, exclusivas de BECOFARMA+CEALSA),
   ¿son modelo nuevo migracion en curso, o features adicionales que
   no aplican a K7/BB?

6. **Q-EC2-DESFASE**: ¿que tan desfasada esta cada BD del EC2 respecto
   a su productiva real? ¿hay un calendario de sincronizacion, o son
   snapshots one-shot?

Estas 6 preguntas pasan a `_inbox/` como hipotesis dirigidas a Erik.

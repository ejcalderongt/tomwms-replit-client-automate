# Propuesta funcional: inventario ciclico consolidado sin solicitar licencia

Estado: aprobado para implementacion
Fecha: 2026-08-28
Ramas de trabajo: `feature/inventario_ciclico_consolidado_sin_licencia`

## Objetivo

Permitir que un inventario ciclico agrupe para conteo varias existencias que solo
difieren por licencia. La HH captura un total consolidado y BOF lo distribuye
entre las lineas congeladas sin modificar el stock operativo hasta aplicar la
regularizacion.

## Configuracion

- Etiqueta: `Consolidar conteo sin solicitar licencia`.
- Campo de `trans_inv_enc`: `Conteo_Consolidado_Sin_Licencia bit NOT NULL DEFAULT 0`.
- BOF lo muestra marcado por defecto en inventarios nuevos, sujeto a decision
  explicita al guardar.
- Puede modificarse en BOF solamente mientras no exista ningun conteo.
- HH lo muestra como solo lectura.
- Una vez aplicado el inventario no puede modificarse.

## Llave de agrupacion

- IdInventarioEnc
- IdPropietario
- IdBodega
- IdUbicacion
- Gondola, cuando aplique
- IdProductoBodega
- Lote_stock
- Fecha_vence_stock
- IdProductoEstado
- IdPresentacion
- IdUnidadMedida
- Atributos no intercambiables aplicables

La licencia se excluye deliberadamente. Presentaciones y unidades de medida no
se convierten ni mezclan. Productos con talla/color y el conteo por caja master
quedan fuera de esta modalidad.

## Conteo HH y BOF

- HH presenta una linea por grupo y conserva el mecanismo actual de captura.
- Escanear una licencia localiza el producto, pero abre el grupo completo.
- Mensajes: `N licencias consolidadas`, `Sin licencia` o
  `N licencias + registros sin licencia`.
- La licencia no informada para productos nuevos se persiste como texto `"0"`.
- Se permiten grupos mixtos con licencias reales y licencia `"0"`.
- BOF distribuye el total por `IdStock` ascendente.
- Faltantes se descuentan sin producir negativos; sobrantes se agregan a la
  licencia ancla del mismo grupo.
- Conteo cero deja todas las lineas del grupo contadas en cero.
- Cantidad se redondea a seis decimales y no admite negativos.
- Distribucion, marcacion y auditoria se guardan en una transaccion.
- Se usa control optimista por version para conteos concurrentes.
- Al reabrir un grupo se muestra lo contado y se crea una version nueva.
- Solo la version vigente participa en regularizacion.
- El inventario congelado no cambia despues de su creacion.

## Auditoria

Crear encabezado y detalle versionados para registrar:

- inventario, ubicacion y gondola;
- llave del grupo;
- version y estado vigente/reemplazado;
- operador y fecha;
- teorico y contado consolidados;
- licencias e IdStock participantes;
- distribucion aplicada a cada linea;
- resultado posterior de regularizacion.

## Regularizacion

- `nuevo_stock = 0` significa no calculado.
- `nuevo_stock = -1` representa stock nuevo calculado igual a cero.
- Se puede volver al conteo hasta presionar `Aplicar inventario`.
- Al aplicar se consideran movimientos, reservas y tareas posteriores al conteo.
- Las reservas no alteran lo contado ni la seleccion de licencia ancla.
- Fallar una linea revierte toda la aplicacion y el inventario no se finaliza.
- Un resultado ya satisfecho por movimientos se registra sin generar ajuste
  duplicado.

## Resolvedor de linaje de IdStock

La regularizacion debe resolver el IdStock congelado antes de aplicar:

1. usar el IdStock original si sigue vigente;
2. recorrer `stock_hist.IdStock -> IdNuevoStock` hasta descendientes vigentes;
3. soportar un descendiente unico o linaje dividido;
4. validar identidad de propietario, producto, presentacion, UM, licencia y
   atributos no intercambiables;
5. corroborar cambios de ubicacion, estado, lote o vencimiento con historico y
   movimientos;
6. aplicar y verificar filas afectadas y saldo final;
7. si no puede resolver de forma inequivoca, revertir y reportar causa.

Resultados minimos de diagnostico:

- RESUELTO_ID_ORIGINAL
- RESUELTO_LINAJE
- RESUELTO_LINAJE_DIVIDIDO
- RESUELTO_LLAVE_UNICA
- YA_CONSUMIDO_POR_MOVIMIENTOS
- SIN_DESCENDIENTE
- MULTIPLES_CANDIDATOS
- IDENTIDAD_INCONSISTENTE
- CANTIDAD_INSUFICIENTE
- ACTUALIZACION_SIN_FILAS
- SALDO_FINAL_INCORRECTO

## Compatibilidad

- BOF nuevo mantiene los metodos actuales por licencia.
- HH antigua opera con el comportamiento anterior.
- Campo ausente en contratos antiguos equivale a `False`.
- Inventarios historicos conservan valor `0`.
- No se incrementan versionCode/versionName de HH automaticamente.

## Implementacion local 2026-09-10

- Rama: `feature/inventario_ciclico_consolidado_sin_licencia` en BOF, HH y DBA.
- El listado consolidado se expone mediante un contrato JSON opt-in; las HH
  anteriores conservan el listado individual.
- BOF construye la llave sin licencia y conserva `IdStock` e `IdInvCiclico` en
  las lineas congeladas originales.
- Al guardar, distribuye el total en orden `IdStock`, limita faltantes a cero y
  coloca el sobrante en la primera linea del grupo.
- Encabezado de auditoria, detalle, distribucion y marcado `contado` se escriben
  dentro de la misma transaccion.
- La huella de las lineas y la version vigente rechazan conteos abiertos antes
  de un cambio o guardados concurrentemente.
- La HH muestra la descripcion del grupo, permite localizarlo al escanear una
  licencia participante y mantiene el flujo individual para talla/color.

Validacion local: compilacion `WSHHRN.vbproj` y
`:app:compileDebugJavaWithJavac` correctas; casos de distribucion 8/12, 15/12 y
0/12 verificados.

---
title: "CKFK: recepción 28335 recuperada; caja master, saldos e identidad"
kind: fact
clients: [MAMPA_QA]
branches: [dev_2026_estable]
status: draft
validated_at: null
tags: [CKFK, recepcion, caja-master, J100000635, AG00034526041365, recuperacion, XML]
---

# Recepción 28335: recuperación confirmada por CKFK

<!-- #CKFK20261117_REC28335_CIERRE -->

Responsable: Carolina Fuentes Kemp, **CKFK**. Alcance: recepción **28335**,
ingreso **28359**, detalle **2**, producto **AG00034526041365**, licencia
**J100000635**, `TOMWMS_MAMPA_QA`, rama `dev_2026_estable`.

**Cierre operativo:** CKFK confirmó en el chat «Listo, ya quedó todo bien.
gracias» y solicitó registrar la recuperación confirmada. Conservar este cierre
como confirmación de la operadora; no presentarlo como consulta SQL posterior
ni extenderlo a otros clientes, ramas o despliegues. Los registros de procedencia
siguen `draft` porque no hay revisión independiente de fuentes commiteadas.

## Causa demostrada y síntomas

1. La OC esperaba **7** unidades, distribuidas en los lotes **1348=4** y **1349=3**.
   `Procesa_Caja_Master` reutilizaba el mismo objeto de OC en ambas posiciones de
   `ListaFiltradaOc` y modificaba su cantidad. Al procesar 4 y después 3, las dos
   posiciones quedaban en **3**. El replay reproduce **3+3=6**; invertir el orden
   produce 4+4=8, por lo que no era una omisión inocua del segundo renglón.
2. QA contenía los detalles de recepción **46491/46492**, ambos de 3. Los
   `stock_rec` **48381/48382** y `stock` **649920/649921** estaban asociados al
   primero: el identificador temporal compartido permitía una correlación ambigua.
3. Los lotes aún indicaban recibido **0/0**. La LP física de los detalles y stock
   estaba vacía; la rutina de actualización exigía que coincidiera con la LP
   documental. La HH exigía escanear la licencia y recargaba todo el contenido,
   aunque la OC ya indicaba 6 recibidas. No confundir LP física parametrizada con
   la identidad del lote documental.

## Corrección

**HH**, en su propio repositorio:

- `clsBeTrans_oc_det`: copia independiente para las cantidades de cada lote.
- `frm_list_rec_prod.Procesa_Caja_Master`: usa `cantidad-cantidad_recibida`,
  conserva la OC original, omite lotes y detalles OC completos y valida producto,
  línea y variante cuando corresponde por control talla/color.
- `frm_recepcion_datos.LoadCajaMaster` y `Guardar_Caja_Master`: reconstrucción
  de lista sin acumulación y temporales negativos diferentes, compartidos entre
  cada detalle y su `stock_rec`, antes de obtener el identity real del servidor.

**Servicio/DAL**, separado de HH:

- `GuardarHH_CM` llama a `ActualizarLotesCajaMaster`: una licencia documental,
  PK únicas, cantidades finitas, identidad y cantidad detalle/stock consistentes.
- Relee y bloquea saldos por PK con `UPDLOCK,HOLDLOCK`, distribuye el ingreso por
  identidad funcional e incrementa cada lote dentro de la transacción existente.
- Rechaza saldo desactualizado, sobrecantidad, variantes equivocadas y temporales
  ambiguos. Conserva las reglas de recepción y la parametrización de LP.
- No se cambió el esquema SQL ni el contrato SOAP. No se usó identidad de empresa
  para activar reglas. Los nombres de cliente delimitan únicamente la evidencia.

## Compatibilidad del APK posterior

Al publicar el servicio apareció `XMLObject Error 2`: el campo
`permitir_abrir_recepcion_enviada` no existía en `clsBeBodega` de esta rama HH.
Sí existía en el servicio y en `dev_2028_merge`, commit
`4d8be0152b8f3c668653dc2e4f5e84ee102c0315`. No había sido eliminado de la rama
estable: faltaba incorporarlo allí.

Se agregó a `clsBeBodega` como `@Element(required=false)`, booleano por defecto
false, más sus accesores. El parser real y la clase compilada aceptaron true,
false y campo ausente; se regeneró el APK. No se desactivó la validación XML
ni se cambió el valor del parámetro en base de datos.

## Recuperación preparada y cierre comunicado

El script canónico está en
`C:/Users/carol/source/repos/DBA/20261117_recuperar_producto_recepcion_28335.sql`.
Conserva seis unidades ya recibidas y concilia **cinco filas**:

| Tabla / fila | Corrección preparada |
|---|---|
| Lote 1348 | recibido 0 → 3 de 4 |
| Lote 1349 | recibido 0 → 3 de 3 |
| stock_rec 48382 | detalle 46491 → 46492 |
| stock 649921 | detalle 46491 → 46492 |
| movimiento 856573 | detalle 46491 → 46492 |

No suma inventario ni recibe la unidad faltante por SQL. Las dos filas de cola
ERP estaban correctamente asociadas y sin enviar; no se incluyeron en los cambios.
Vista previa por defecto, confirmación de HH/servicio actualizados, transacción,
11 precondiciones y comprobación final. Las condiciones se contrastaron con
SELECT; el agente no ejecutó DML.

La primera pantalla posterior al cambio aún mostraba **4/3**, no **1**. La
consulta confirmó el estado anterior; CKFK aclaró que no había ejecutado el script.
Esto prueba que corregir el código no concilia datos históricos automáticamente.
El recorrido indicado fue revisar/aplicar la conciliación, salir y volver a cargar
la recepción, escanear la LP y recibir **1 unidad con la HH**. Después, CKFK
confirmó el resultado satisfactorio. No inventar el ID de esa nueva recepción
ni una lectura SQL final que no se realizó.

## Pruebas, estado Git y límites

Compilaciones de Java Android, APK debug y WSHHRN con dependencias satisfactorias.
Pruebas de fragmentos reales con datos en memoria: 4/3 y 3/4, referencias,
preservación de OC, saldo 1, completos, variante, reintento obsoleto, exceso,
NaN, licencias, stock y PK duplicadas; caso con/sin control talla/color.
Script parseado sin errores. El adaptador de pruebas no simula locks/rollback
contra SQL Server; el cierre operativo se apoya en el reporte de CKFK.

En el inventario de 2026-11-17 BOF ya está limpio, HEAD
`9f0254cb53f0f7c27440489e07564f6d9f732ffc`, que toca el fuente de recepción.
HH y el script DBA siguen pendientes en el inventario. No confundir «no se hizo
commit durante esta sesión» con «el cambio sigue sin commit para siempre».
Los HEAD y hashes actuales constan en el inventario general y su manifiesto;
la publicación y los binarios instalados no se deducen de Git.

## Fuentes

- [Diagnóstico original](../../../analysis/20261117-recepcion-28335/diagnostico.md).
- [Preparación, pruebas y límites](../../../analysis/20261117-recepcion-28335/entrega.md).
- [Confirmación de CKFK](../../../analysis/20261117-recepcion-28335/confirmacion-ckfk.md).
- [Evidencia acotada previa](../../../analysis/20261117-recepcion-28335/resumen-evidencia.json).
- [Registro de procedencia](../../../analysis/20261117-brain-pending/claims.json).
- [Catálogo completo de pendientes](../2026-11-17-ckfk-pendientes/README.md).

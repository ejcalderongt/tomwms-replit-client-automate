# Estado del Arte — Caja Master en Inventario Cíclico (HH/BOF/WS/DAL)

Fecha: 2026-05-26  
Autor: EJC/Codex  
Objetivo: documentar el flujo real de Caja Master y su relación con `Control_Talla_Color` antes de continuar cambios funcionales.

---

## 1) Resumen ejecutivo

La funcionalidad **Caja Master** en inventario cíclico hoy está implementada como un flujo HH que:

1. parte desde `frm_inv_cic_conteo` (escaneo en filtro),
2. agrupa registros por `Licence_plate`,
3. abre `frm_conteo_caja_master`,
4. confirma y envía lista al WS `Inventario_Ciclico_Conteo_Caja_Master`,
5. DAL marca `contado = 1` y `cantidad = cant_stock` por cada línea.

### Hallazgo central de negocio

En el diseño actual del cliente HH, **Caja Master sí viene acoplada conceptualmente a talla/color** en al menos un punto de entrada UI:

- `frm_inv_cic_conteo.java`: `chkCajaMaster` se mostraba originalmente condicionado por `gl.Control_Talla_Color`.

Esto sugiere que el comentario “Caja Master no debe depender de talla/color” es una simplificación técnica que puede contradecir el concepto funcional vigente del cliente.

---

## 2) Mapa end-to-end del proceso

## 2.1 HH - Pantalla de conteo

Archivo:  
`C:/Users/yejc2/StudioProjects/TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_conteo.java`

Puntos relevantes:

- Escaneo en `txtBuscFiltro` (`KEYCODE_ENTER`):
  - si `chkCajaMaster` está activo:
    - filtra `gl.reconteo_list` por `item.Licence_plate.equalsIgnoreCase(texto)`.
    - llena `ListaCajaMasterConteo`.
    - si lista no vacía, abre `frm_conteo_caja_master`.
  - si no, usa `procesarEscaneoInteligente(texto)` (flujo normal).

- Condición visual histórica:
  - `chkCajaMaster` ligado a `gl.Control_Talla_Color` (estado original previo al ajuste reciente).

## 2.2 HH - Pantalla de confirmación Caja Master

Archivo:  
`C:/Users/yejc2/StudioProjects/TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_conteo_caja_master.java`

Puntos relevantes:

- Muestra licencia de referencia y listado de líneas a impactar.
- Al guardar:
  - construye `List<clsBeTrans_inv_ciclico>` con `IdInventarioEnc`, `IdInvCiclico`, `IdProductoBodega`, `Contado = true`.
  - invoca WS `Inventario_Ciclico_Conteo_Caja_Master`.

## 2.3 WS (WSHHRN)

Archivo:  
`C:/Users/yejc2/source/repos/TOMWMS/WSHHRN/TOMHHWS.asmx.vb`

Método:

- `Inventario_Ciclico_Conteo_Caja_Master(pInvCiclico)`
  - delega a `clsLnTrans_inv_ciclico.Actualiza_Conteo_Ciclico_Caja_Master_HH`.

## 2.4 DAL

Archivo:  
`C:/Users/yejc2/source/repos/TOMWMS/TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_Partial.vb`

Métodos:

- `Actualiza_Conteo_Ciclico_Caja_Master_HH(ListaTransInvCiclico)`
  - resuelve inventario objetivo por producto.
  - fuerza:
    - `InvCiclico.Contado = True`
    - `InvCiclico.Cantidad = InvCiclico.Cant_stock`
  - persiste con `Act_Inventario_Ciclico_Caja_Master`.

- `Act_Inventario_Ciclico_Caja_Master`
  - actualiza tabla `trans_inv_ciclico` por `idinvciclico`.

---

## 3) Relación Caja Master vs Talla/Color (estado actual)

## 3.1 Señales de acoplamiento funcional

- Entrada de Caja Master en HH históricamente condicionada por `Control_Talla_Color`.
- Inventario cíclico hoy incluye `IdProductoTallaColor`, `CodigoSKU`, talla/color en varias rutas.
- Escaneo y resolución por SKU/talla-color fue extendido en los cambios recientes (Abi/Anderly).

## 3.2 Señales de independencia técnica parcial

- El WS de Caja Master no recibe explícitamente talla/color en su contrato principal.
- La actualización final opera por `IdInvCiclico`/`IdProductoBodega` del lote recibido.

## 3.3 Conclusión de estado de arte

**Conceptualmente (negocio/UI)**: Caja Master está acoplada a escenarios talla/color en esta versión.  
**Técnicamente (persistencia)**: puede operar sin campo explícito talla/color si el conjunto origen ya viene correctamente delimitado.

En otras palabras: la separación es posible, pero no está “homologada” en toda la cadena funcional actual.

---

## 4) Riesgos si se desacopla sin redefinir regla

1. Se habilita Caja Master en bodegas/procesos donde el usuario no espera ese comportamiento.
2. Se mezclan líneas de licencia con semántica distinta (por ejemplo, variantes no equivalentes para el proceso del cliente).
3. Se rompe trazabilidad conceptual entre “conteo por variante” y “conteo por licencia”.

---

## 5) Decisiones recomendadas antes de continuar código

## Opción A (conservadora, recomendada si el concepto actual se mantiene)

- Mantener gate de visibilidad/uso de Caja Master ligado a `Control_Talla_Color` (o a una regla equivalente de negocio ya vigente).
- Documentar explícitamente en QA cuándo aplica y cuándo no.

## Opción B (evolutiva, si se quiere generalizar)

- Crear parámetro explícito de negocio (`Control_Caja_Master` o similar).
- Definir matriz de aplicación:
  - con talla/color,
  - sin talla/color,
  - con/sin licencia,
  - impacto esperado en reconteo.
- Ajustar HH + BOF + capacitación QA en un cambio controlado.

---

## 6) Recomendación inmediata para esta rama

Dado el contexto que indicas, la recomendación es:

1. tratar el ajuste “no depende de talla/color” como **hipótesis técnica**, no como regla cerrada;
2. alinear primero regla funcional con negocio;
3. luego aplicar ajuste final de visibilidad/comportamiento según decisión A o B.

---

## 7) Referencias

- HH conteo cíclico:
  - `.../frm_inv_cic_conteo.java`
  - `.../frm_conteo_caja_master.java`
- WS:
  - `.../WSHHRN/TOMHHWS.asmx.vb` (`Inventario_Ciclico_Conteo_Caja_Master`)
- DAL:
  - `.../clsLnTrans_inv_ciclico_Partial.vb`


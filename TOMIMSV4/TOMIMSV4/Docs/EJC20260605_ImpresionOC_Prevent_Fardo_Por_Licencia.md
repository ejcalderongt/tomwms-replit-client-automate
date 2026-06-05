# EJC20260605 - Impresión OC: prevent de sobreimpresión de fardos por licencia

## Objetivo

Definir un prevent funcional/técnico para evitar imprimir más etiquetas de fardo de las permitidas por licencia y por línea de OC, y persistir el conteo de fardos impresos por licencia.

## Traza actual (código)

Formulario:
- `TOMIMSV4/TOMIMSV4/Mantenimientos/Impresion_OC/frmImpresion_OC.vb`

Puntos clave:
- `cmdImpresionBarra_Click(...)`: valida e imprime fardos.
- `Imprimir_Producto(...)`: realiza el loop de impresión de fardos.
- `CerrarEImprimirLicenciaConBultos(...)`: cierra licencia y persiste en `i_nav_barras_pallet`.
- `CrearPalletPreImpresion(...)`: arma el objeto que se guarda en `i_nav_barras_pallet`.

Listado de licencias en pestaña "Licencias":
- `clsLnTrans_oc_det_lote.Get_Barras_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(...)`
- lee directamente de `i_nav_barras_pallet` columnas:
  - `Cantidad_Presentacion`
  - `Cantidad_UMP`

## Hallazgo funcional

En modo `LicenciaBulto`, el cierre de licencia guarda:
- `Cantidad_Presentacion = CamasPorTarima * CajasPorCama` (capacidad teórica fija)
- `Cantidad_UMP = pBeTransOcDet.Cantidad` (cantidad total de la línea)

Consecuencia:
- para una línea con remanente, varias licencias quedan con la misma cantidad (ej. 136 / 272 en todas), aunque la última debería ser parcial;
- no hay persistencia del número real de fardos impresos por licencia;
- el prevent actual depende mucho de estado en memoria (`pBultosPendientesLicenciaActual`), no de una huella persistida del proceso.

## Respuesta a la pregunta

Sí, se puede y se recomienda persistir "cuántas etiquetas/fardos se imprimieron por licencia" a nivel de `i_nav_barras_pallet` como primera capa.

## Diseño recomendado (sin cambio de esquema, quirúrgico)

### 1) Persistir cantidad real por licencia al cerrar

Usar los campos existentes de `i_nav_barras_pallet` para representar la licencia cerrada real:
- `Cantidad_Presentacion`: fardos reales impresos para esa licencia.
- `Cantidad_UMP`: `Cantidad_Presentacion * FactorPresentacion` (o equivalente UMBAS).

No guardar capacidad fija ni total de línea en esos campos.

### 2) Contador real en memoria de la licencia activa

En `frmImpresionRecepcion_OC`:
- agregar/aclarar variable de trabajo: `pFardosImpresosLicenciaActual`.
- inicializar en `ReiniciarEstadoLicenciaActual()`.
- incrementar en `Imprimir_Producto(...)` por `pImpresiones`.

Nota de negocio:
- `pImpresiones` = fardos.
- `txtCopias` = copias físicas de etiqueta.
Si se quiere auditoría exacta física, guardar también copias por separado.

### 3) Prevent dual antes de imprimir fardo

Antes de imprimir:
1. Prevent por licencia activa:
   - `pFardosImpresosLicenciaActual + cantidadSolicitada <= capacidadLicencia`.
2. Prevent global por línea:
   - `sum(Cantidad_Presentacion ya persistidas en i_nav_barras_pallet) + pFardosImpresosLicenciaActual + cantidadSolicitada <= Cantidad de la línea`.

Esto cubre remanentes y evita sobreimpresión acumulada.

### 4) Cierre de licencia coherente

En `CerrarEImprimirLicenciaConBultos(...)`:
- persistir `Cantidad_Presentacion` con el contador real de fardos de la licencia;
- calcular `Cantidad_UMP` desde factor/presentación;
- no usar valores fijos de capacidad ni total de línea.

## Opción robusta (recomendada a mediano plazo)

Además de la agregación en `i_nav_barras_pallet`, crear bitácora de eventos de impresión de fardo:
- tabla propuesta: `i_nav_barras_pallet_print_det` (1 fila por acción de impresión),
- campos sugeridos:
  - `IdPrint`, `IdPallet`/`Codigo_Barra`, `IdOrdenCompraEnc`, `IdOrdenCompraDet`,
  - `Cantidad_Fardos`, `Copias`, `Usuario`, `Fecha`, `Equipo`.

Ventajas:
- trazabilidad real de reimpresiones y copias;
- auditoría y RCA más fina;
- prevent basado en agregación confiable aun con concurrencia.

## Concurrencia y consistencia

Para blindar:
- encapsular "leer acumulado + validar + persistir" en transacción al cerrar licencia;
- evaluar índice único por `Codigo_Barra` en `i_nav_barras_pallet` (si no existe) para evitar duplicados por carrera.

## Criterios de aceptación

1. Caso remanente:
   - línea de 240 con capacidad 136 -> licencias quedan 136 y 104.
2. No sobreimpresión por licencia:
   - no permite imprimir fardos por encima de capacidad de licencia activa.
3. No sobreimpresión global:
   - no permite exceder cantidad total de la línea al acumular licencias.
4. Grilla "Licencias" consistente:
   - `Cantidad_Presentacion` y `Cantidad_UMP` reflejan lo real por licencia.
5. Reimpresión controlada:
   - reimpresión no altera acumulados operativos, solo deja traza si se implementa bitácora.


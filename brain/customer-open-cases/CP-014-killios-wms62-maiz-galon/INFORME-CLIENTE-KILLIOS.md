---
id: INFORME-CLIENTE-KILLIOS-CP014
tipo: cp-open
estado: borrador
titulo: Informe al cliente Killios — Caso WMS62 Maiz Galon (10 cajas fantasma vs SAP)
cliente: killios
producto: WMS62
materializa_bug: BUG-001
fecha: 2026-05-09
audiencia: cliente-final (Zulma Martinez — garesa.co)
---

# Informe al cliente Killios — WMS62 Maiz dulce Miguels Galon

**Para**: Zulma Martinez — garesa.co
**De**: Erik Calderon — PrograX24
**Asunto**: Caso WMS62 — diferencia 10 cajas vs kardex SAP — diagnostico y plan
**Fecha**: 09-may-2026

---

## 1. Lo que reportaste

Reportaste que el reporte de existencias del WMS muestra **10 cajas mas**
que el kardex SAP del producto **WMS62 — Maiz Dulce Miguels Galon
6/2900g** en la bodega operativa.

---

## 2. Lo que confirmamos en la base de datos

Trabajamos sobre una restauracion de la BD productiva del 09-may-2026
10:23 hs (sin tocar la BD productiva de operacion). El resultado coincide
exactamente con tu reporte.

| Vista | UM | Cajas |
|---|---:|---:|
| Stock vivo del WMS bodega 1 | 2.741 | **456,83** |
| Kardex SAP entregado por uds. | 2.681 | **446,83** |
| **Diferencia (sobra en WMS)** | +60 | **+10 cajas** |

La diferencia es exacta. No es un error de redondeo ni de presentacion.

---

## 3. Donde estan las 10 cajas — listado tecnico

El stock vivo se compone de **27 lineas** distribuidas en **17 matriculas
de pallet (lic_plate)**. La distribucion por categoria:

| Categoria | Lineas | UM | Cajas |
|---|---:|---:|---:|
| Sano (Buen Estado, libre) | 16 | 247 | 41,17 |
| Reservado (en pedidos en curso) | 4 | 384 | 64,00 |
| Reempacar (marcados como danados) | 7 | 1.690 | 281,67 |
| Mal Estado en Recepcion | 1 | 420 | 70,00 |
| **Total** | **27** | **2.741** | **456,83** |

Te paso aparte el listado completo con IdStock + matricula + ubicacion +
cantidad (archivo `traza-001-stock-fantasma.md`) para que el equipo
operativo pueda confirmar fisicamente cada linea antes de hacer cualquier
ajuste.

---

## 4. Que es lo que esta pasando (en lenguaje simple)

Cuando el operativo del HH (handheld) hace ciertas operaciones — pickings,
verificacion, marcar como reempacar — el sistema deberia escribir **dos
cosas a la vez**:

1. Actualizar el stock fisico (la fila viva).
2. Anotar el movimiento en el historial (`trans_movimientos`), que es lo
   que SAP usa para reconstruir el kardex.

En este caso (y en otros que ya documentamos previamente para Carolina
con WMS164) el sistema esta escribiendo el paso 1 pero no el paso 2,
o lo escribe con datos diferentes (otra matricula, otra clave). Como el
kardex SAP solo ve el paso 2, las 10 cajas quedan "huerfanas" en el WMS
sin que SAP las explique.

---

## 5. Plan recomendado

### Paso 1 — Verificacion fisica (esta semana, equipo operativo Killios)

Tomar el listado tecnico de las 27 lineas y validar fisicamente:
- Confirmar las 8 lineas marcadas Reempacar / Mal Estado (315 cajas en
  total). En particular las 4 lineas de 49 cajas c/u en P15-C1-N1-A-#1038
  (matriculas FU08994, FU08995, FU09120, FU09220, lote 60120).
- Confirmar las 4 lineas reservadas (64 cajas) — si los pedidos siguen
  vigentes, estas cajas NO se ajustan.

### Paso 2 — Ajuste manual de stock (post-validacion)

Una vez confirmado el fisico, Erik aplica un ajuste tipo **AJCANTN
(ajuste cantidad negativa)** sobre los IdStock que correspondan, con
observacion "Reconciliacion SAP CP-014". Esto deja el saldo del WMS
igual al kardex SAP.

### Paso 3 — Fix de fondo (mediano plazo, equipo desarrollo PrograX24)

Este caso suma a la lista de evidencia para el bug que ya estamos
trabajando (BUG-001). El fix tecnico esta en queue para la rama
`dev_2028_merge` (proximo release). Mientras tanto, recomendamos:
- **Inventario ciclico mensual** de los productos top-30 mas movidos.
- **Reporte de discrepancia** automatico semanal vs SAP para detectar
  estos desbalances dentro de los 7 dias.
- **NO marcar Reempacar / Mal Estado desde el backoffice** sin elegir
  un destino fisico claro (merma, devolucion, descarte).

---

## 6. Que necesitamos de uds. para avanzar

1. Confirmacion de validacion fisica de las 27 lineas (puede ser
   conteo asistido por el listado adjunto).
2. Si hay alguna linea que claramente ya no esta fisicamente, marcala
   en el listado para priorizar el ajuste.
3. OK de Carolina + tuyo para que Erik aplique el ajuste manual.

Quedo a disposicion para una llamada esta semana si quieren revisar el
listado linea por linea.

---

**Erik Calderon**
PrograX24 — Direccion de Desarrollo TOMWMS

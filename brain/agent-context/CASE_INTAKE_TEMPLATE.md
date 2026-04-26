# Plantilla de intake de casos — TOMWMS

> Copiá este bloque, llenalo, y pegámelo en el chat de Replit cuando abras un
> caso. Mientras más completo, menos preguntas te hago y más rápido arranco.
>
> No tenés que llenar TODO. Lo crítico está marcado **(obligatorio)**.

---

## 🆔 Identificación

- **Caso interno / ticket**:
- **Tipo** (obligatorio): `data-discrepancy` | `hh-bug` | `vb-exception` | `sql-perf` | `feature-request` | `otro`
- **Severidad**: `bloqueante` | `alta` | `media` | `baja`
- **Cliente / BD** (obligatorio): `KILLIOS` | `<otro>` → DB: `TOMWMS_KILLIOS_PRD`
- **Reportado por**: nombre / rol
- **Fecha del reporte**:

## 🎯 Síntoma observable (obligatorio)

Una o dos frases en lenguaje del usuario, tal como lo dijo él.

> Ejemplo: "El Kardex de 'Guinda mitades Bolsa' muestra entradas y salidas que
> no cuadran con el stock actual. Falta media tarima."

## 📦 Datos puntuales

> Mientras más IDs reales, mejor. No me hagas adivinar.

- **SKU / IdProducto**:
- **Bodega / IdBodega**:
- **Ubicación / IdUbicacion**:
- **IdLote / IdStock**:
- **Documento (recepción/despacho/ajuste)**:
- **Usuario que operó**:
- **Terminal HH** (modelo + serial, si aplica):

## ⏱ Ventana temporal (obligatorio para discrepancias)

- **Última vez que estaba bien**: `YYYY-MM-DD HH:MM`
- **Primera vez que se vio mal**: `YYYY-MM-DD HH:MM`
- **Operación sospechosa entre esas fechas**:

## 🔁 Reproducibilidad

- [ ] Reproducible a demanda
- [ ] Intermitente
- [ ] Una sola vez (no se repitió)
- [ ] No probado / no aplica

**Pasos exactos para reproducir** (si reproducible):
1.
2.
3.

## 🧪 Lo que YA se intentó

- Queries diagnósticos corridos:
- Cambios revertidos / probados:
- Hipótesis descartadas:

## 🤔 Hipótesis del reporter / equipo

> Lo que ellos creen que pasó, aunque no esté confirmado. Es información valiosa.

## 📎 Adjuntos

> Pegámelos como bloque de código o subí archivos al chat de Replit.

- [ ] Stack trace / excepción VB:
- [ ] Log HH (logcat):
- [ ] Screenshot del Kardex / pantalla del usuario:
- [ ] Output de queries diagnósticos:
- [ ] Definición del SP/vista sospechoso:
- [ ] Trace SQL (Profiler / Extended Events):

## 🔐 Acceso necesario

- [ ] Solo análisis estático (Brain alcanza)
- [ ] Necesito que vos corras una query en KILLIOS prod (yo no escribo nunca)
- [ ] Necesito que vos repliques el caso en QA y me pases el resultado

## 🎁 Resultado esperado

> Qué considerás "caso resuelto". Sin esto, no sé cuándo parar.

> Ejemplo: "Saber qué método/SP dejó el stock negativo, y cómo prevenirlo."

---

## Ejemplo lleno (caso real Guinda mitades)

```
Caso interno: GMB-2026-04-16
Tipo: data-discrepancy
Severidad: alta
Cliente / BD: KILLIOS / TOMWMS_KILLIOS_PRD
Reportado por: Operador del cliente
Fecha del reporte: 2026-04-16

Síntoma observable:
"El Kardex de 'Guinda mitades Bolsa' no cuadra con stock actual,
falta inventario."

SKU: <IdProducto del SKU Guinda mitades Bolsa>
Bodega: <IdBodega>
Lote: pendiente de identificar

Ventana temporal:
- Última vez bien: 2026-04-15 fin de día (cierre OK)
- Primera vez mal: 2026-04-16 mañana
- Operación sospechosa: ajuste cíclico nocturno o despacho 16/04 madrugada

Reproducibilidad: una sola vez, no se ha repetido.

Lo que ya se intentó:
- Comparar Kardex vs SP_Stock_Actual: diferencia de 0.5 tarima.
- Revisar logs de ajuste: hay un Eliminar() que se ejecutó 03:14 a.m.

Hipótesis: ajuste cíclico borró registro de stock sin generar movimiento
de Kardex compensatorio.

Adjuntos:
- (pegar stack trace / queries / screenshots)

Acceso necesario:
- Necesito que corras dos queries en KILLIOS prod (lectura).

Resultado esperado:
Identificar qué método VB o SP borró el stock sin Kardex, y proponer fix.
```

---

## Qué hago yo cuando recibo un intake completo

1. **Triage** (1 min): clasifico el caso y reviso `replit.md` por contexto previo.
2. **Brain query plan**: armo `/search` → `/impact` → `/writers` según el tipo.
3. **Hipótesis priorizadas**: te devuelvo 2-3 sospechosos con evidencia (línea
   exacta del código + fragmento SQL) en menos de 5 minutos.
4. **Queries diagnósticos**: si necesito ver dato real, te paso el SQL exacto
   listo para correr en KILLIOS y me devolvés el resultado.
5. **Diagnóstico final + fix propuesto**: con código concreto y `/impact` del
   fix antes de aplicarlo.
6. **Re-index post-fix**: cuando aplicás el cambio, re-indexo el repo afectado
   para que el Brain quede al día.

## Qué NO hago (para que sepas y no me lo pidas)

- Conectarme directo a KILLIOS prod desde Replit (la prod está detrás de tu
  firewall, vos sos el bridge).
- Hacer commits o pushes en tus repos (vos los hacés).
- Ejecutar writes en producción.
- Cambiar `Reference.vb` ni mezclar HH+VB en un mismo cambio.

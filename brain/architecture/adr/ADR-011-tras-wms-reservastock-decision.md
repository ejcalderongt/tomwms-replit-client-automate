---
id: ADR-011
titulo: TRAS_WMS.ReservaStock - documentar como reservado-para-futuro, NO eliminar del schema
status: proposed (provisional - esperan ratificacion de EJC)
fecha: 2026-04-28
proponente: agente brain (sesion replit)
ratificador_pendiente: Erik Calderon
hallazgo_origen: H-01 (brain/_inbox/20260428-1900-H01-tras-wms-reservastock-muerto.json)
preguntas_origen: P-18
adrs_relacionados: ADR-010 (reserva-webapi vs wms-legacy)
---

# ADR-011 - TRAS_WMS.ReservaStock: documentar como reservado-para-futuro, NO eliminar del schema

## Contexto

Triple confirmacion de que `trans_pe_tipo.ReservaStock=false` para TRAS_WMS no se valida en el motor de reserva actual:

1. **Erik (tanda-1, P-18)**: "tienes razon, me parece que no se esta validando especificamente esa bandera". Documenta DEUDA-001.
2. **Carol (pasada-7, P-18)**: "Actualmente no se utiliza esa bandera para reservar o no producto de un documento de salida".
3. **SQL agente (tanda-2, PEND-01)**: confirma `trans_pe_tipo.ReservaStock=false` literal en BD para tipo TRAS_WMS.

Erik aporta el contexto historico de la intencion original (vision futura no implementada): la bandera estaba pensada para soportar un flujo de bucket/abastecimiento batch policy-driven, donde TRAS_WMS recibiria documentos como "bolson" para procesamiento posterior con politicas de prorrateo (ej. 50% CDs, 30% nuevos clientes, 20% calidad y merma).

## Decision

**Opcion (c): documentar como reservado-para-futuro.**

Mantener la columna `ReservaStock` en el schema del WebAPI nuevo (`reserva-webapi`) con marca explicita `@reservado-para-futuro`. Implementar validacion explicita: si un cliente del WebAPI intenta poblar este campo con `true` para tipo TRAS_WMS, rechazar con HTTP 400 + mensaje explicativo que apunte a este ADR.

### Schema esperado del WebAPI

```csharp
[JsonPropertyName("reservaStock")]
[Description("Reservado para uso futuro - flujo bucket/abastecimiento policy-driven. " +
             "Hoy debe enviarse en false para TRAS_WMS. Ver ADR-011.")]
public bool ReservaStock { get; set; }
```

### Validacion en el endpoint

```csharp
if (request.IdTipoPedido == "TRAS_WMS" && request.ReservaStock == true) {
    return BadRequest(new ProblemDetails {
        Title = "Reserva Stock Reservado",
        Detail = "TRAS_WMS no soporta ReservaStock=true en esta version. " +
                 "Ver ADR-011 (reservado-para-futuro).",
        Type = "https://brain.tomwms/adr/011"
    });
}
```

### Test de paridad

Bridge debe validar que el WebAPI nuevo rechace el caso patologico (devuelve 400) y que el WMS legacy lo acepte sin alarma (comportamiento actual). Es decir, **el bridge documenta la divergencia controlada**, no exige paridad ciega.

## Alternativas consideradas

### Opcion (a): eliminar del schema

- **Pros**: limpio, no hay codigo muerto.
- **Contras**: quema la posibilidad de implementar el bucket/abastecimiento sin migracion de schema futura. Erik tiene una vision arquitectonica documentada que se perderia.
- **Veredicto**: rechazada. La capacidad latente vale mas que la limpieza estetica.

### Opcion (b): implementar el bucket/abastecimiento ahora

- **Pros**: cierra la deuda y honra la intencion original.
- **Contras**: scope-creep masivo. Requiere configuracion de politicas por cliente, job batch, proyeccion de demanda. NO es requisito de la migracion reserva-webapi y agregaria semanas de trabajo.
- **Veredicto**: rechazada. Out-of-scope para esta fase.

## Consecuencias

### Positivas

- Preserva la opcion futura del bolson/bucket sin reescribir el schema.
- Hace explicita la deuda (deja de ser codigo muerto silencioso, pasa a ser feature flag documentada).
- El bridge tiene contrato claro de paridad/divergencia.

### Negativas

- Deja una "columna fantasma" en el schema del WebAPI con riesgo de confundir desarrolladores nuevos.

### Mitigacion

1. Documentar explicitamente en el archivo `openapi.yaml` del WebAPI con `description` extendida.
2. Test que verifica el rechazo del campo cuando el tipo es TRAS_WMS.
3. Linkear este ADR desde el property comment en C#.
4. Si en algun momento se decide implementar el flujo bucket, agregar entrada en CHANGELOG y deprecar este ADR con sucesor.

## Ratificacion

Esta decision es **provisional**. Se aplica en el scaffold inicial del WebAPI. Si Erik la veta o pide cambiar, hacer rollback antes de la primera release de reserva-webapi.

## Referencias

- Hallazgo: `brain/_inbox/20260428-1900-H01-tras-wms-reservastock-muerto.json`
- Pregunta origen: `brain/wms-specific-process-flow/preguntas-pasada-7.md` P-18
- Respuesta Erik: `brain/wms-specific-process-flow/respuestas-tanda-1.md` P-18
- Respuesta Carol: `brain/wms-specific-process-flow/respuestas-pasada-7.md` P-18
- Confirmacion SQL: `brain/wms-specific-process-flow/respuestas-tanda-2.md` PEND-01
- Consolidacion: `brain/wms-specific-process-flow/consolidacion-pasada-7.md` R-05

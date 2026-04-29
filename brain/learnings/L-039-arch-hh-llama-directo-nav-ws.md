# L-039 — ARCH: la HH llama DIRECTO a NAV-WebServices por tipo de documento, WSHHRN no es proxy

> Etiqueta: `L-039_ARCH_HH-LLAMA-DIRECTO-NAV-WS_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-WSHHRN-AS-PROXY-BYB

## Hallazgo (revisa hipotesis previa)

**WSHHRN NO es un proxy/adapter SOAP hacia NAV.** La hipotesis del cuestionario era equivocada.

WSHHRN cumple un solo rol: **es el WebService que conecta el BOF de TOMWMS con la HH** (canal interno entre las dos cabezas del dual-head WMS).

**La HH habla DIRECTO con los WebServices de NAV** (cuando el cliente tiene NAV — caso BYB), sin pasar por WSHHRN. Los WebServices de NAV varian por **tipo de documento**:

- Orden de Produccion
- Transferencias
- Devoluciones

## Cita literal Carolina (Wave 11)

> "En BYB este proyecto [WSHHRN] es el Web service que relaciona el BOF
> del TOMWMS con la HH.
>
> En la HH se hacen algunos llamados directos hacia los WebServices de
> NAV, los Web Service varian por tipos de documentos: Orden de
> produccion, Transferencias, devoluciones, y se utilizan para:
>
> 1. A medida que se van recibiendo los productos estos se va a
>    registrando licencia por licencia en NAV.
> 2. Cuando se finaliza una Recepcion en la HH se debe registra ese
>    documento en NAV."

## Diagrama mental corregido

```
         BOF TOMWMS (VB.NET, WinForms)
                |
                | (intra-WMS, canal interno)
                v
            WSHHRN (Web service)
                |
                v
         HH Android (Xamarin)
                |
                | (directo, por tipo de documento)
                v
        NAV WebServices (cliente BYB / Dynamics NAV / Business Central)
```

**WSHHRN no es un middleware NAV.** Es el bus interno BOF↔HH. NAV vive afuera y la HH lo invoca por su cuenta.

## Patron de uso real (BYB, recepcion)

Dos puntos de invocacion HH→NAV durante una recepcion:

1. **Por licencia (tiempo real)**: cada licencia escaneada se registra inmediatamente en NAV via WS de "Orden de Produccion" o el que corresponda. Granularidad: **una llamada NAV por licencia**.
2. **Al finalizar recepcion (commit)**: cuando la HH cierra el documento, se hace una segunda llamada a NAV para registrar el documento completo (cierre / posting).

Esto implica:
- **Latencia NAV impacta UX HH directamente**. Si NAV WS responde lento, el operador siente lag por licencia.
- **Falla de red HH↔NAV bloquea la recepcion** (no hay cache local segun esta version, salvo evidencia contraria).
- **WSHHRN NO loggea trafico HH↔NAV** porque no esta en el path. Para auditar HH↔NAV hay que mirar logs en NAV o sniffear desde HH.

## Implicaciones para el agente

1. **Cuando el problema sea "HH lenta en BYB"**, NO mirar WSHHRN. Mirar latencia NAV.
2. **Cuando aparezca un error tipo "documento no se posteo en NAV"**, el log esta en HH o en NAV, no en WSHHRN.
3. **Para clientes sin NAV (CEALSA, MAMPA, BECO, K7)**, este path no existe. La HH solo habla con WSHHRN y la integracion ERP la hace el BOF.
4. **WSHHRN NO se desacopla facil de WMS.exe.config** (Q-F bonus Bloque 9, no respondida) porque tiene la config del WMS embebida — pendiente Erik.

## Q-* abiertas

- Q-HH-NAV-CACHE-OFFLINE: ¿la HH cachea localmente las llamadas a NAV cuando no hay red? ¿O bloquea?
- Q-HH-NAV-RETRY: ¿hay retry / circuit breaker en las llamadas HH→NAV?
- Q-HH-NAV-TIMEOUT: ¿que timeout usan las llamadas HH→NAV? ¿UX-acceptable?
- Q-HH-NAV-ENDPOINTS-LISTA: lista exhaustiva de los WS de NAV que la HH invoca (no solo OP/Transferencias/Devoluciones — Carolina dijo "los WebService varian por tipo", pueden haber mas).

## Vinculos

- L-040 (Wave 11): BD NAV inaccesible → todo via WS, esto refuerza este patron.
- L-035: NO codificar por cliente — confirma que la decision "HH llama NAV directo" es por configuracion en BYB, no por `if Cliente = "BYB"`.

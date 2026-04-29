# L-041 — ARCH: DALCore (.NET Core) lo consume SOLO el WMSWebAPI, el DMS no

> Etiqueta: `L-041_ARCH_DALCORE-SOLO-WMSWEBAPI_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-DALCORE-CONSUMERS

## Hallazgo

DALCore en .NET Core (data access layer moderno, distinto del DAL legacy de TOMWMS_BOF) tiene **un solo consumer en produccion**: el **WMSWebAPI**. El DMS NO lo usa (sigue con su propio acceso a datos, presumiblemente DAL legacy o ADO directo).

## Cita literal Carolina (Wave 11)

> "Hasta donde tengo entendido lo utiliza el WMSWebAPI, el DMS no lo utiliza."

(Carolina lo califica de "hasta donde tengo entendido" → no es respuesta de autoridad final, conviene confirmar con Erik o Efren.)

## Implicaciones

1. **DALCore es estrategia de futuro, no presente compartido**. Hoy es .NET Core only, vive con WMSWebAPI. Los demas componentes (BOF VB.NET, WSHHRN, DMS, NavSync.exe) no lo conocen.
2. **Cualquier cambio en DALCore solo impacta WMSWebAPI** → bajo blast radius.
3. **Hay duplicacion de logica de acceso a datos** entre DALCore (lo que usa WMSWebAPI) y el DAL clasico de TOMWMS_BOF. Si ambos tocan las mismas tablas con reglas distintas, hay riesgo de divergencia.
4. **El DMS usa su propio acceso a datos** → habria que mapear cual es. Probablemente VB.NET legacy + ADO. (Pendiente: deep-flow del DMS.)

## Por que esto es importante para 2028

El plan implicito parece ser:
- BOF VB.NET → eventualmente Web BOF (Q38, no respondida).
- WSHHRN + HH → siguen.
- WMSWebAPI + DALCore → expansion (MHS estrenando, futuros clientes B2B).
- DMS → no se moderniza con DALCore, queda con su stack actual.

Si el plan a largo plazo es **"DALCore es el data layer canonico"**, falta migrar el DMS y eventualmente el BOF. Carolina no lo afirmo — esto es inferencia. Pendiente confirmar con Erik.

## Q-* abiertas

- Q-DALCORE-COVERAGE-VS-DAL-CLASICO: ¿DALCore implementa todas las entidades o solo las que necesita WMSWebAPI? ¿Hay backlog?
- Q-DALCORE-VS-DAL-CONFLICTOS: ¿hay tablas tocadas por ambas DAL con reglas distintas?
- Q-DMS-DAL-STACK: ¿que usa el DMS para acceso a datos? ¿Plan de modernizar?
- Q-DALCORE-PLAN-LARGO-PLAZO: ¿DALCore es target final para BOF futuro y para todos los componentes nuevos?

## Vinculos

- L-038 (Wave 11): WMSWebAPI estrena cliente real con MHS — refuerza importancia de DALCore.
- L-042 (Wave 11): generador BE/LN-base/LN-partial standalone — probablemente alimenta DALCore tambien.

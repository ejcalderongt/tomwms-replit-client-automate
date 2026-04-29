# L-025 — META: el prefijo "Nav" en el codigo del WMS NO es Microsoft Dynamics NAV

> Etiqueta: `L-025_META_NAV-NO-ES-DYNAMICS_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta de Carolina al Q-NAV-PREFIJO-CEALSA (Bloque 2 cuestionario)

## Hallazgo

En el codigo del WMS (BOF, WSHHRN, WMSWebAPI), el prefijo `Nav` aparece
en muchos identificadores: tablas (`i_nav_config_enc`, `i_nav_transacciones_out`,
`i_nav_ped_traslado_det`), clases (`clsSyncNavProducto`,
`clsSyncNavCategoriasProducto`, `clsSyncNavGruposProducto`,
`clsSyncNavTablaConversion`), columnas (`codigo_bodega_erp_nc`,
`Codigo_Bodega_ERP_NC`), executables (`NavSync.exe`).

**El prefijo `Nav` ya NO significa Microsoft Dynamics NAV / Business
Central**. Hoy es **sinonimo interno de "ERP" o "Interface de
Integracion"**. Cualquier ERP que se conecte al WMS, sea SAP B1, NAV,
custom de ARITEC, IMS4, Odoo, o el que sea, vive bajo nombres con
prefijo `Nav` en las tablas y clases compartidas.

## Por que paso esto

Cita literal Carolina (Wave 10):

> "NAV significa lo que indicas, porque fue la primera Interface que
> Erik desarrollo para TOMWMS, pero resulto que luego al crear las
> nuevas Interfaces a Erik le resulto funcional manejar las mismas
> tablas y por tanto las mismas clases y nos quedamos con ese nombre
> el cual ya no lo identificamos con NAV sino que para nosotros es
> sinonimo de ERP o de Interface."

Cronologia inferida:
1. Primera integracion del WMS fue contra Microsoft Dynamics NAV (cliente
   BYB). Erik diseno tablas + clases con prefijo `Nav` literal.
2. Cuando aparecieron nuevos clientes con SAP B1 (BECOFARMA, KILLIOS,
   despues MAMPA), Erik **reuso las mismas tablas** (`i_nav_config_enc`,
   `i_nav_transacciones_out`) y las mismas clases base
   (`clsSyncNavProducto`, etc.) en vez de duplicar con prefijo `SapBO`.
3. Cuando aparecio CEALSA con su ERP custom de ARITEC, mismo patron:
   reusar la infraestructura `Nav*`.
4. Resultado: el prefijo perdio su semantica original. Hoy es ruido
   historico que NADIE en el equipo asocia ya con NAV.

## Implicaciones para el brain

### 1. Reinterpretacion de hallazgos previos

- **Wave 4 (heat-map params cross-cliente)**: las menciones a tablas
  `i_nav_config_enc`, `i_nav_transacciones_out`, columnas `*_nc`/`*_NC`
  NO indican que ese cliente use NAV. Son nombres genericos de la capa
  de integracion ERP.
- **Wave 6 (traza-001 LP)**: el campo `Get_Nuevo_Correlativo_LicensePlate`
  expuesto desde NAV en BYB es **caso real** (BYB SI usa Dynamics NAV
  literal). Pero `clsSyncNavProducto` en CEALSAMI3 NO tiene relacion
  con Dynamics; sincroniza productos del ERP de ARITEC (Wave 10
  L-028).
- **Wave 6.1 (DALCore + DMS)**: la columna `interface_sap` en
  `i_nav_config_enc` no es contradictoria con el prefijo `nav` de
  la tabla; es una bandera secundaria que indica si el ERP de fondo es
  SAP B1 (entonces el dispatch usa SAPBOSync.exe en vez del default).

### 2. Mapeo "Nav* → ERP real" por cliente

| Cliente | Prefijo `Nav*` en codigo | ERP real detras |
|---|---|---|
| BYB | si | Microsoft Dynamics NAV (caso historico literal) |
| BECOFARMA | si | SAP B1 (via SAPBOSync.exe) |
| KILLIOS | si | SAP B1 (via SAPBOSyncKillios.exe) |
| MAMPA | si | SAP B1 (via SAPBOSyncMampa.exe) |
| CEALSA | si | ERP custom desarrollado por **ARITEC**, en proceso de migracion a **Odoo** (Wave 10 L-028) |
| MERHONSA | si | (pendiente confirmar) — probable variante NAV/SAP holding IDEALSA |
| MERCOPAN | si | (pendiente confirmar) — idem |
| INELAC | si (consume MI3) | (pendiente confirmar) |
| CUMBRE | si | (pendiente confirmar) — probable SAPBOSyncCumbre.exe segun L-022 |

### 3. Convencion para escribir en el brain

A partir de Wave 10:
- Cuando se mencione `clsSyncNav<X>`, NO inferir que el cliente usa NAV.
- Cuando se mencione `i_nav_config_enc`, decir "tabla de configuracion
  por interface ERP" (no "tabla de NAV").
- Cuando se mencione `NavSync.exe` o `nombre_ejecutable LIKE 'Nav%'`,
  **si necesario clarificar**, decir "sincronizador-historico-naming
  para ERP destino X (segun `interface_sap` o el binario)".
- Cuando se mencione `*_NC` o `_nc` en columnas, NO confundir con
  NavCorporate o algo asi; es solo prefijo historico.

### 4. Para el scanner `scan-comments-tree-map`

Agregar al `boost-keywords.yml` el keyword `Nav` con peso REDUCIDO, no
amplificado, porque su mencion en comentarios suele ser ambigua. Agregar
nota en SCORING.md.

### 5. Para el web BOF / WMSWebAPI futuros

**Oportunidad**: la migracion al WebAPI puede aprovechar para renombrar
la capa de integracion a algo neutro (`erp_*`, `integration_*`, `sync_*`).
Hoy es deuda nominal. Si se hace en el cambio mayor 2028+1, se
documentaria como ADR.

## Cierra Q-*

- `Q-NAV-PREFIJO-CEALSA` (Bloque 2, alta — bloqueante) → **RESUELTA**.
  Era la mas critica del bloque integraciones porque cambia
  interpretacion de TODO el codigo con prefijo Nav.

## Refuerza L-*

- L-022 (`patron-naming-sincronizador`) sigue valido pero matizado:
  el prefijo `NavSync` en el binario indica el sincronizador
  historico-naming que reusa tablas `Nav*`, NO necesariamente que el
  ERP destino sea Microsoft Dynamics NAV. Para saber el ERP real hay
  que cruzar con `interface_sap` y/o el cliente.

## Pendientes derivados

- Q-CEALSA-ARITEC-ERP-NOMBRE (cuestionario nuevo Q79): nombre exacto
  del ERP que ARITEC desarrollo para CEALSA.
- Q-CEALSA-MIGRACION-ODOO-TIMELINE (Q80): cuando arranca la migracion
  a Odoo, que impacto tiene en CEALSAMI3.
- Sugerencia: documentar L-035 META-CONVENCIONES-NAMING-HISTORICO con
  el patron general "naming primer cliente queda como generico" (otros
  ejemplos: SAPBOSync.exe original era para BECOFARMA, hoy es base
  generica SAP B1).

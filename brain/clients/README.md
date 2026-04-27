# Catálogo de clientes

| Cliente | DB | ERP | Bodegas | Modulo distintivo |
|---|---|---|---|---|
| KILLIOS | TOMWMS_KILLIOS_PRD | SAP B1 (DI-API) | 6 (3 pares principal/prorrateo) | Conversion cajas/decimales SAP |
| BYB | IMS4MB_BYB_PRD | NAV Dynamics | 2 | Reabastecimiento de picking activo |
| CEALSA | IMS4MB_CEALSA_QAS | (sin ERP integrado) | 2 (1 general + 1 fiscal) | Stock jornada + prefacturacion 3PL |

Server compartido EC2: `52.41.114.122,1437` (SQL Server, mixed auth).

Cada `<cliente>.md` tiene perfil completo. Cada `<cliente>.yaml` tiene flags learned con timestamp y query origen, listo para consumir desde el runner del bridge.

## Politica de conexion

- Las BDs de Killios y BYB son **PRODUCTIVAS**. CEALSA es QAS pero contiene datos reales del cliente.
- TODA la operacion del agente es `SELECT`/`EXEC` de stored procedures de lectura. **Cero modificaciones**.
- El whitelist de prefijos permitidos esta en `brain/wms-agent/wmsa/killios.py` (`SELECT, WITH, EXEC, EXECUTE, SET, DECLARE, PRINT`) con bloqueo activo de `INSERT, UPDATE, DELETE, DROP, TRUNCATE, ALTER, CREATE, EXEC sp_executesql` con texto mutante.

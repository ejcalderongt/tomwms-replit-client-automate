# MHS: espacios fisicos asociados y capacidad de tramo

Estado: implementacion inicial en `dev_2028_merge`, pendiente de despliegue y prueba integrada.

## Contrato funcional

- `bodega_asociacion_virtual` representa aristas bidireccionales entre ubicaciones que comparten un mismo espacio fisico.
- El grupo fisico es el componente transitivo formado por asociaciones activas. La ocupacion de un grupo no afecta a otro grupo, aunque intervengan las mismas bodegas.
- Un grupo admite stock activo y positivo en una sola de sus ubicaciones. La licencia puede ser igual o diferente: la regla protege el espacio, no la identidad de la licencia.
- La base de datos es la autoridad final y valida dentro de la transaccion del movimiento. BOF y HH prevalidan para informar oportunamente al operador.
- `bodega_tramo.limite_licencias` es nullable: `NULL` o `0` desactiva la validacion; un valor mayor que cero limita las licencias normalizadas distintas por `IdBodega + IdTramo`.
- La capacidad se calcula con el estado proyectado. Mover dentro del mismo tramo o agregar a una licencia que ya esta en el tramo no incrementa el conteo.
- En la primera fase, exceder la capacidad es una advertencia permisiva en HH. El conflicto de espacio fisico es bloqueante.

## Contrato tecnico inicial

- Script DBA idempotente: columna, restricciones, indices, auditoria previa, procedimientos centrales y triggers de integridad.
- DAL BOF: prevalidacion reutilizable y validacion dura utilizando la misma conexion y transaccion del movimiento.
- WebMethod JSON independiente para prevalidar; no se cambia el contrato historico de ejecucion.
- HH: prevalidacion en cambio individual y licencia completa, con opcion de cancelar o continuar solamente para la capacidad del tramo.

## Datos pendientes

- Confirmar con operacion como se dividen fisicamente las asociaciones de PF, PH y PS dentro de MH53. Esto afecta unicamente la configuracion de aristas; no cambia el algoritmo por grupos.
- Extender posteriormente la llamada transaccional y la prevalidacion a recepcion, inventario, ajustes, explosion y demas integraciones que escriban stock.

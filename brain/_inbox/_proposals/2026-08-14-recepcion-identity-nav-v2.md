#EJC20260814

## Propuesta: retorno estructurado del guardado unitario de recepcion

- Rama: `dev_2026_estable`.
- BOF: agregar `Guardar_Recepcion_S_V2` sin modificar `Guardar_Recepcion_S`.
- Contrato: `ResultadoGuardarRecepcion` con `Exito`, `Mensaje`, `IdRecepcionEnc` e `IdRecepcionDet`.
- HH: usar V2 solamente en el flujo unitario que actualmente llama `Guardar_Recepcion_S` y que continuara hacia NAV.
- Selector NAV centralizado: `Push_To_NAV`, tipo soportado y datos ERP requeridos; compra excluye `Interface_SAP`.
- Tipos cubiertos: orden de produccion (`case 25`), ingreso, devolucion de venta y transferencia de ingreso (`case 26`).
- NAV: ejecutar el push solo despues de asignar y validar el identity retornado.
- Compatibilidad: no cambia recepcion sin presentacion, caja master, multiples detalles, SAP ni consumidores existentes del WebMethod anterior.

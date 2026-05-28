---
tipo: other
autores: [erik]
---
# WmsTrace BOF/MI3 — Guía de diseño e implementación

**Diseñado:** `#EJC20260528`  
**Archivos:** `WmsTrace.vb` (423 líneas), `parse_trace_bof.py` (238 líneas)

---

## 1. Arquitectura del sistema

```
ERP (NAV/MI3)
    │
    ▼  MI3 WCF boundary  ← MiEntry / MiExit (2 líneas)
PedidoCompra.svc.vb
PedidoCliente.svc.vb
TransaccionesOut.svc.vb
    │
    ▼  LN layer          ← OpStart / OpEnd (2 líneas por método clave)
clsLnI_nav_ped_compra_enc.vb
clsLn[Recepcion|Picking|Despacho].vb
    │
    ▼  clsTransaccion    ← TxBegin / TxCommit / TxRollback (3 puntos ya existentes)
Begin_Transaction()
Commit_Transaction()
RollBack_Transaction()
    │
    ▼  DAL (opcional, granular)  ← SqlStart / SqlEnd por SP crítico
SqlCommand.ExecuteNonQuery / ExecuteReader
    │
    ▼  SQL Server
```

**Total de líneas en código existente para cobertura básica: ~22 líneas**  
(3 en clsTransaccion + 12 en 3 servicios MI3 + 7 en LNs críticos)

---

## 2. Patch a `clsTransaccion.vb` (3 puntos)

```diff
  Public Function Begin_Transaction() As Boolean
      Try
+         WmsTrace.TxBegin()   '#EJC20260528 trace
          lConnection = New SqlConnection(...)
          lConnection.Open() : lTransaction = lConnection.BeginTransaction(...)
          Return True

  Public Sub Commit_Transaction()
      Try
          If Not lTransaction Is Nothing Then
              lTransaction.Commit()
+             WmsTrace.TxCommit()   '#EJC20260528 trace
          End If

  Public Sub RollBack_Transaction()
      Try
          If lTransaction IsNot Nothing Then
              lTransaction.Rollback()
+             WmsTrace.TxRollback(reason:="")   '#EJC20260528 trace
          End If
```

---

## 3. Patch a servicios MI3 (patrón aplicar a los 3 .svc.vb)

### `PedidoCompra.svc.vb` — método `Insert`

```diff
  Public Function Insert(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc,
                         ByRef Resultado As String) As Integer Implements IPedidoCompra.Insert
      Insert = 0
+     WmsTrace.MiEntry("PedidoCompra", "Insert", BeINavPedCompraEnc.NoEnc)   '#EJC20260528
      Try
          ...
          ' Al final, antes de cada Return/Throw:
+         WmsTrace.MiExit("PedidoCompra", "Insert", BeINavPedCompraEnc.NoEnc, success:=True)
          Return Insert
      Catch ex As Exception
+         WmsTrace.MiExit("PedidoCompra", "Insert", BeINavPedCompraEnc.NoEnc, False, ex.Message)
          Throw New Exception(...)
      End Try
  End Function
```

**Repetir el patrón para:**  
- `PedidoCompra.Insert_Multiple`, `Update`, `Delete`  
- `PedidoCliente.Insert`, `Update`  
- `TransaccionesOut` (todos los métodos)

---

## 4. Patch a capa LN (métodos críticos de negocio)

### `clsLnI_nav_ped_compra_enc.vb` — `Procesar_Pedido_Compra_MI3`

```vb
Public Shared Function Procesar_Pedido_Compra_MI3(
    ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc,
    ByRef BePedidoCompraEnc  As clsBeTrans_oc_enc,
    ByRef vResult            As String) As Boolean

    WmsTrace.OpStart("Procesar_Pedido_Compra_MI3", BeINavPedCompraEnc.NoEnc)   '#EJC20260528
    Try
        ' ... código existente ...
        WmsTrace.OpEnd("Procesar_Pedido_Compra_MI3", True, , BeINavPedCompraEnc.NoEnc)   '#EJC20260528
        Return True
    Catch ex As Exception
        WmsTrace.OpEnd("Procesar_Pedido_Compra_MI3", False, ex.Message, BeINavPedCompraEnc.NoEnc)
        Throw
    End Try
End Function
```

**Candidatos LN prioritarios para trazar:**

| Método LN | Contexto sugerido |
|---|---|
| `Procesar_Pedido_Compra_MI3` | `BeINavPedCompraEnc.NoEnc` |
| `Insert_Single_Pedido_From_ERP` | `BeINavPedCompraEnc.NoEnc` |
| `Insert_Multiple_Pedido_Compra_From_ERP` | `"batch-" & lPedidos.Count` |
| `Guardar_Recepcion` (BOF WinForms) | `IdRecepcionEnc` |
| `Generar_Picking` | `IdPedidoEnc` |
| `Confirmar_Despacho` | `IdDespachoEnc` |

---

## 5. Uso en sesión de prueba

### Activar (en `Application_Start` de MI3 o `Form_Load` de BOF)

```vb
WmsTrace.ENABLED = True
WmsTrace.Reset("mi3-killios-oc-batch-20260528")
```

### Recolectar el log

El archivo se genera en: `C:\TOM\Logs\wms-trace-YYYYMMDD.log`

Para copiar del servidor EC2:
```bash
# Desde máquina local (via scp o RDP)
scp ec2-user@52.41.114.122:"C:\TOM\Logs\wms-trace-20260528.log" .
```

O para BOF WinForms (localhost):
```
%PROGRAMDATA%\TOM\Logs\wms-trace-20260528.log
# o ajustar LogDir en WmsTrace.vb
```

### Analizar

```bash
python3 parse_trace_bof.py wms-trace-20260528.log
```

### Antes de cerrar la sesión (en `Application_End` o botón debug)

```vb
WmsTrace.DumpStats()
WmsTrace.FlushLog()
WmsTrace.ENABLED = False
```

---

## 6. Qué detecta automáticamente

| Tag en log | Qué significa | Acción |
|---|---|---|
| `!! N+1` | Mismo SP > 3x en 500ms desde mismo contexto | Consolidar en SELECT IN o TVP |
| `!! SLOW_TX` | Transacción > 5,000ms | Revisar CURSOR, deadlocks, SELECT dentro de TX |
| `!! SLOW_OP` | Operación LN > 8,000ms | Revisar índices, SP batch, paginación |
| `!! TX_ORPHAN` | TX abierta > 30s sin Commit/Rollback | Bug: Catch no llama RollBack_Transaction |
| `[SLOW_SQL]` | SP individual > 3,000ms | SET STATISTICS IO ON y revisar plan |
| `[SLOW_MI]` | WCF call MI3 > 5,000ms | Revisar SLA con ERP, timeouts de WCF |

---

## 7. Diferencias respecto a WmsTrace HH (Android)

| Aspecto | HH (Android) | BOF/MI3 (VB.NET) |
|---|---|---|
| Output | adb logcat | Rolling file `C:\TOM\Logs\wms-trace-*.log` |
| Threading | Background + UI thread pattern | Sync (WinForms) + Async (WCF/BackgroundWorker) |
| Activación | `ENABLED = true` en onCreate | `ENABLED = True` en App_Start / Form_Load |
| Punto principal | switch(case) en wsExecute/wsCallBack | WCF method entry + LN method entry |
| Detección clave | Race condition, reload | N+1 SQL, TX lenta, TX orphan |
| Scope | 1 formulario (frm_recepcion_datos) | Cross-layer: MI3 → LN → TX → SQL |
| Parser | `parse_trace.py` | `parse_trace_bof.py` (mismo estilo) |

---

## 8. Extensión a BOF WinForms (sin MI3)

Para trazar operaciones directas de usuario en WinForms (ej: `frm_Recepcion_BOF`):

```vb
' En el handler del botón Guardar:
Private Sub btnGuardar_Click(...)
    WmsTrace.OpStart("Guardar_Recepcion_BOF", txtIdRecepcion.Text)
    Try
        ' ... lógica existente ...
        WmsTrace.OpEnd("Guardar_Recepcion_BOF", True, , txtIdRecepcion.Text)
    Catch ex As Exception
        WmsTrace.OpEnd("Guardar_Recepcion_BOF", False, ex.Message, txtIdRecepcion.Text)
        ' ... manejo existente ...
    End Try
End Sub
```

---

## 9. Dónde ubicar WmsTrace.vb en el proyecto

```
TOMWMS_BOF/
└── TOMIMSV4/
    └── DAL/
        └── General/
            └── Servidor/
                ├── clsServidor.vb     (existente)
                └── WmsTrace.vb        ← NUEVO aquí
```

Como es un `Module` (no una `Class`), no requiere instanciación. Todos los proyectos que referencian `DAL.dll` lo tienen disponible automáticamente: `TOMIMSV4`, `MI3`, `CEALSAMI3`, `WSHHRN`, etc.

---

## 10. Checklist de implementación

- [ ] Agregar `WmsTrace.vb` en `TOMIMSV4/DAL/General/Servidor/`
- [ ] Patch `clsTransaccion.vb` (3 líneas)
- [ ] Patch `MI3/Transacciones/PedidoCompra.svc.vb` (entry/exit en Insert, Insert_Multiple, Update)
- [ ] Patch `MI3/Transacciones/PedidoCliente.svc.vb` (entry/exit)
- [ ] Patch `MI3/Transacciones/TransaccionesOut.svc.vb` (entry/exit)
- [ ] Patch `clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3` (OpStart/OpEnd)
- [ ] Verificar que `LogDir` (`C:\TOM\Logs\`) existe o ajustar ruta
- [ ] Compilar y verificar: `WmsTrace.ENABLED = True` no rompe nada en debug
- [ ] Prueba: correr 5 OCs de prueba, copiar log, correr parser

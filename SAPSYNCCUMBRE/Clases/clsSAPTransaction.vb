Imports SAPbobsCOM

Public Class clsSapTransaction

    Private ReadOnly _company As Company
    Private _transactionStarted As Boolean

    '#EJC20260602_SYNC_INGRESO_SAP: Carol, este helper viene del patron que ya usamos en Killios.
    ' La idea es sencilla: SAP no debe confirmar la entrada/traslado hasta que WMS tambien
    ' haya pasado sus marcas locales. Si algo falla en medio, se revierte SAP y evitamos
    ' que el mismo documento quede como "no enviado" en TOMWMS pero ya creado en SAP.
    Public Sub New(ByVal company As Company)
        _company = company
        _transactionStarted = False
    End Sub

    Public Sub BeginTransaction()
        If _company Is Nothing Then
            Throw New Exception("No hay conexion SAP disponible para iniciar transaccion.")
        End If

        If Not _transactionStarted Then
            _company.StartTransaction()
            _transactionStarted = True
        End If
    End Sub

    Public Sub CommitTransaction()
        If _transactionStarted Then
            _company.EndTransaction(BoWfTransOpt.wf_Commit)
            _transactionStarted = False
        End If
    End Sub

    Public Sub RollbackTransaction()
        If _transactionStarted Then
            _company.EndTransaction(BoWfTransOpt.wf_RollBack)
            _transactionStarted = False
        End If
    End Sub

End Class

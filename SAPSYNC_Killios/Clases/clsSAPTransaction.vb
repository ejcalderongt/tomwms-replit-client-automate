Public Class clsSapTransaction
    Public Property Company As SAPbobsCOM.Company
    Public Property TransactionStarted As Boolean

    Public Sub New(company As SAPbobsCOM.Company)
        Me.Company = company
        Me.TransactionStarted = False
    End Sub

    Public Sub BeginTransaction()
        If Not TransactionStarted Then
            Company.StartTransaction()
            TransactionStarted = True
        End If
    End Sub

    Public Sub CommitTransaction()
        If TransactionStarted Then
            Company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            TransactionStarted = False
        End If
    End Sub

    Public Sub RollbackTransaction()
        If TransactionStarted Then
            Company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            TransactionStarted = False
        End If
    End Sub
End Class

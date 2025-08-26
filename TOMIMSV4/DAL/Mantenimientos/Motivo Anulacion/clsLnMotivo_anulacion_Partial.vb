Imports System.Data.SqlClient

Partial Public Class clsLnMotivo_anulacion
    Implements IDisposable

    Public Shared Function GetSingle(ByVal pIdMotivoAnulacion As Integer) As clsBeMotivo_anulacion

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM motivo_anulacion WHERE IdMotivoAnulacion=@IdMotivoAnulacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoAnulacion", pIdMotivoAnulacion)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeMotivo_anulacion()

                        Cargar(Obj, lRow)
                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean) As List(Of clsBeMotivo_anulacion)

        Dim lReturnList As New List(Of clsBeMotivo_anulacion)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_MotivoAnulacion WHERE 1 > 0 "

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_anulacion

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_anulacion
                            Obj.Empresa = New clsBeEmpresa
                            Cargar(Obj, lRow)

                            If lRow("IdEmpresa") IsNot DBNull.Value AndAlso lRow("IdEmpresa") IsNot Nothing Then
                                Obj.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdEmpresa As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("DELETE FROM motivo_anulacion WHERE IdEmpresa={0}", pIdEmpresa), lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    lCommand.ExecuteNonQuery()
                    lConnection.Close()

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Exists(ByVal pIdMotivoAnulacion As Integer) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM motivo_anulacion WHERE IdMotivoAnulacion=@IdMotivoAnulacion"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdMotivoAnulacion", pIdMotivoAnulacion)
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxIdMotivoAnulacion() As Integer

        Try


            Dim vSQL As String = "SELECT  MAX(IdMotivoAnulacion) + 1 as nuevo FROM motivo_anulacion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                MaxIdMotivoAnulacion = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            Else
                MaxIdMotivoAnulacion = 1
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeMotivo_anulacion)

        Dim lMa As New List(Of clsBeMotivo_anulacion)
        Dim Ma As New clsBeMotivo_anulacion

        Try

            Dim sp As String = " SELECT  ma.IdMotivoAnulacion,ma.IdEmpresa, ma.Nombre, ma.activo,
                                         ma.user_agr, ma.fec_agr, ma.user_mod, ma.fec_mod
                                 FROM motivo_anulacion ma INNER JOIN 
                                      motivo_anulacion_bodega mab ON ma.IdMotivoAnulacion =mab.IdMotivoAnulacion 
                                 WHERE (mab.IdBodega = @IdBodega) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            For Each vma As DataRow In dt.Rows
                Ma = New clsBeMotivo_anulacion
                Ma.Empresa = New clsBeEmpresa
                Cargar(Ma, vma)

                If vma("IdEmpresa") IsNot DBNull.Value AndAlso vma("IdEmpresa") IsNot Nothing Then
                    Ma.Empresa.Nombre = CType(vma("IdEmpresa"), String)
                End If

                lMa.Add(Ma)


            Next

            Return lMa

        Catch ex As Exception
            Throw New Exception("GetAllByBodega_Motivo_Anulacion_Partial: " & ex.Message)
        End Try

    End Function

    Public Shared Sub GuardarTransaccion(ByVal pListObjMA As List(Of clsBeMotivo_anulacion))

        Dim ObjMA As New clsLnMotivo_anulacion()
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeMotivo_anulacion In pListObjMA
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    ObjMA.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If lConnection.State = ConnectionState.open then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

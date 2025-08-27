Imports System.Data.SqlClient

Partial Public Class clsLnPropietario_destinatario
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdDestinatarioPropietario),0) FROM propietario_destinatario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByIdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_destinatario)

        Try

            Dim lReturnList As New List(Of clsBePropietario_destinatario)


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1630pm: Quité String.Format.
                Dim vSQL As String = "SELECT * FROM propietario_destinatario WHERE IdPropietario=@IdPropietario"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBePropietario_destinatario

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBePropietario_destinatario()
                            Cargar(Obj, lRow)
                            Obj.IsNew = False
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

    Public Shared Function GetAllMail(ByVal pListObjRegla As List(Of Integer)) As List(Of clsBePropietario_destinatario)

        Dim Lista As New List(Of clsBePropietario_destinatario)
        Dim lWhere As String = String.Empty

        Try

            For Each regla As Integer In pListObjRegla
                lWhere += regla & ","
            Next
            lWhere = lWhere.Remove(lWhere.Length - 1, 1)

        Catch ex As Exception
            Throw ex
        End Try

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lDTA As New SqlDataAdapter(String.Format("SELECT DISTINCT d.correo_electronico FROM Propietario_reglas_det AS det INNER JOIN propietario_destinatario AS d ON det.IdDestinatarioPropietario = d.IdDestinatarioPropietario WHERE det.activo=1 AND det.IdReglaPropietarioEnc IN ({0})", lWhere), lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim Obj As clsBePropietario_destinatario

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBePropietario_destinatario

                            If lRow("Correo_electronico") IsNot DBNull.Value AndAlso lRow("Correo_electronico") IsNot Nothing Then
                                Obj.Correo_electronico = CType(lRow("Correo_electronico"), String)
                            End If

                            Lista.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return Lista

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub DeleteDestinatario(ByVal pIdDestinatarioPropietario As Integer)

        Dim vSQL As String = "DELETE FROM propietario_destinatario WHERE IdDestinatarioPropietario=@pIdDestinatarioPropietario"

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            '#HS 07112017 Quité query dentro de SqlCommand.
            Using lCommand As New SqlCommand(vSQL, lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@pIdDestinatarioPropietario", pIdDestinatarioPropietario)

                lConnection.Open()
                lCommand.ExecuteNonQuery()
                lConnection.Close()

            End Using

        End Using

    End Sub

    Public Shared Function GuardarDestinatario(ByVal pListObj As List(Of clsBePropietario_destinatario)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim ObjLn As New clsLnPropietario_destinatario()

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = clsLnPropietario_destinatario.MaxID()
            For Each Obj As clsBePropietario_destinatario In pListObj
                If Obj.IsNew Then
                    lMax += 1
                    Obj.IdDestinatarioPropietario = lMax
                    ObjLn.Insertar(Obj, lConnection, lTransaction)
                Else
                    ObjLn.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()
            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function EliminarDestinatario(ByVal pListObjE As List(Of clsBePropietario_destinatario)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing



        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBePropietario_destinatario In pListObjE
                If Obj.IsNew = False Then
                    clsLnPropietario_destinatario.Eliminar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()
            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Function

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

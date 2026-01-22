Imports System.Data.SqlClient

Partial Public Class clsLnPropietario_reglas_det
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdReglaPropietarioDet),0) FROM propietario_reglas_det"), lConnection)
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

    Public Shared Function Get_All_By_IdReglaPropietarioEnc(ByVal pIdReglaPropietarioEnc As Integer) As List(Of clsBePropietario_reglas_det)

        Dim Lista As New List(Of clsBePropietario_reglas_det)

        Dim vSQL As String = "SELECT det.*,p.Nombre + ' '  + p.Apellido AS Nombre FROM propietario_reglas_det det " _
               & "INNER JOIN propietario_destinatario AS p ON det.IdDestinatarioPropietario = p.IdDestinatarioPropietario " _
               & "WHERE IdReglaPropietarioEnc=@IdReglaPropietarioEnc"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdReglaPropietarioEnc", pIdReglaPropietarioEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim Obj As clsBePropietario_reglas_det

                            For Each lRow As DataRow In lDT.Rows

                                Obj = New clsBePropietario_reglas_det
                                Cargar(Obj, lRow)
                                Obj.NombreDestinatario = IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))
                                Obj.IsNew = False
                                Lista.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Lista

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdReglaPropietarioEnc(ByVal pIdReglaPropietarioEnc As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBePropietario_reglas_det)

        Dim Lista As New List(Of clsBePropietario_reglas_det)

        Dim vSQL As String = "SELECT det.*,p.Nombre + ' '  + p.Apellido AS Nombre FROM propietario_reglas_det det 
                              INNER JOIN propietario_destinatario AS p ON det.IdDestinatarioPropietario = p.IdDestinatarioPropietario 
                              WHERE IdReglaPropietarioEnc=@IdReglaPropietarioEnc"

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdReglaPropietarioEnc", pIdReglaPropietarioEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim Obj As clsBePropietario_reglas_det

                    For Each lRow As DataRow In lDT.Rows

                        Obj = New clsBePropietario_reglas_det
                        Cargar(Obj, lRow)
                        Obj.IsNew = False

                        Lista.Add(Obj)

                    Next

                End If

            End Using

            Return Lista

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBePropietario_reglas_det)

        Dim Lista As New List(Of clsBePropietario_reglas_det)

        Dim vSQL As String = "SELECT det.*,p.Nombre + ' '  + p.Apellido AS Nombre FROM propietario_reglas_det det " _
               & "INNER JOIN propietario_destinatario AS p ON det.IdDestinatarioPropietario = p.IdDestinatarioPropietario "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 08112017 Quité query dentro de SqlDataAdapter.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDT As New DataTable()

                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim Obj As clsBePropietario_reglas_det

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBePropietario_reglas_det

                            Cargar(Obj, lRow)

                            Obj.IsNew = False

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

    Public Shared Sub Desactivar(ByVal pIdReglaPropietarioDet As Integer)

        Try

            Dim vSQL As String = "UPDATE propietario_reglas_det SET activo=0 WHERE IdReglaPropietarioDet=@IdReglaPropietarioDet"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdReglaPropietarioDet", pIdReglaPropietarioDet)
                        lCommand.ExecuteNonQuery()

                    End Using

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                             ByVal pTransaction As SqlTransaction) As Integer
        Dim lMax As Integer = 0
        Dim mustCloseConn As Boolean = False

        Try
            Dim vSQL As String = "SELECT ISNULL(MAX(IdReglaPropietarioDet),0) FROM propietario_reglas_det"

            ' Tomamos la conexión externa si viene, si no creamos una interna
            Dim conn As SqlConnection = pConnection
            If conn Is Nothing Then
                conn = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                mustCloseConn = True
            End If

            Using cmd As New SqlCommand(vSQL, conn)
                cmd.CommandType = CommandType.Text

                ' Si viene una transacción externa, la usamos
                If pTransaction IsNot Nothing Then
                    cmd.Transaction = pTransaction
                End If

                ' Abrimos sólo si está cerrada
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Dim lReturnValue As Object = cmd.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            ' Si la conexión la creamos nosotros, la cerramos nosotros
            If mustCloseConn AndAlso conn IsNot Nothing AndAlso conn.State <> ConnectionState.Closed Then
                conn.Close()
            End If

            Return lMax

        Catch ex As Exception
            Throw
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

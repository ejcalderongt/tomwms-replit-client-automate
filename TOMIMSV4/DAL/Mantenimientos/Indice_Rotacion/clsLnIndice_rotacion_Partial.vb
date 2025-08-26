Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnIndice_rotacion
    Implements IDisposable

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT IdIndiceRotacion AS Código,Descripcion AS Nombre,IndicePrioridad, Grupo FROM Indice_rotacion WHERE 1 > 0 "

            If pActivo Then
                sp += " AND activo=1 "
            Else
                sp += " AND activo=0 "
            End If

            sp &= " ORDER BY Descripcion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pDescripcion As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM indice_rotacion WHERE descripcion=@descripcion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@descripcion", pDescripcion)
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

    Public Shared Function Exists(ByVal pDescripcion As String,
                                  ByVal lConnection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM indice_rotacion WHERE descripcion=@descripcion"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@descripcion", pDescripcion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdIndiceRotacionByNombre(ByVal pDescripcion As String,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Integer

        GetIdIndiceRotacionByNombre = 0

        Try

            Dim vSQL As String = "SELECT IdIndiceRotacion FROM indice_rotacion WHERE descripcion=@descripcion"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@descripcion", pDescripcion)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Return lDT(0)(0)
                End If

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Seleccion() As List(Of clsBeIndice_rotacion)

        Try

            Dim lReturnList As New List(Of clsBeIndice_rotacion)
            Const sp As String = "SELECT * FROM Indice_rotacion "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeIndice_rotacion As New clsBeIndice_rotacion

            For Each dr As DataRow In dt.Rows

                vBeIndice_rotacion = New clsBeIndice_rotacion
                Cargar(vBeIndice_rotacion, dr)
                vBeIndice_rotacion.Activo = False
                lReturnList.Add(vBeIndice_rotacion)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal Activo As Boolean) As List(Of clsBeIndice_rotacion)

        Try

            Dim lReturnList As New List(Of clsBeIndice_rotacion)
            Const sp As String = "SELECT * FROM Indice_rotacion WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeIndice_rotacion As New clsBeIndice_rotacion

            For Each dr As DataRow In dt.Rows

                vBeIndice_rotacion = New clsBeIndice_rotacion
                Cargar(vBeIndice_rotacion, dr)
                lReturnList.Add(vBeIndice_rotacion)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal Activo As Boolean) As DataTable

        Try

            Const sp As String = "SELECT IdIndiceRotacion,Descripcion as Nombre FROM Indice_rotacion WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeIndice_rotacion As clsBeIndice_rotacion,
                                   ByVal lConnection As SqlConnection,
                                   ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Indice_rotacion" &
            " Where(IdIndiceRotacion = @IdIndiceRotacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeIndice_rotacion.IdIndiceRotacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeIndice_rotacion, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
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

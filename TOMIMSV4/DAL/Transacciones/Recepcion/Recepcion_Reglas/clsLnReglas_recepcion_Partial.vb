Imports System.Data.SqlClient

Partial Public Class clsLnReglas_recepcion

    Public Shared Function Get_Reglas_Recepcion() As List(Of clsBeReglas_recepcion)

        Get_Reglas_Recepcion = Nothing

        Try

            Dim lReglasRecepcion As New List(Of clsBeReglas_recepcion)
            Dim vBeReglasRecepcion As New clsBeReglas_recepcion

            Dim DT As New DataTable

            DT = Listar()

            For Each pm As DataRow In DT.Rows

                vBeReglasRecepcion = New clsBeReglas_recepcion
                Cargar(vBeReglasRecepcion, pm)
                lReglasRecepcion.Add(vBeReglasRecepcion)

            Next

            DT.Dispose()

            Return lReglasRecepcion

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetAll(ByVal Activo As Boolean) As List(Of clsBeReglas_recepcion)


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeReglas_recepcion)
            Const sp As String = "SELECT * FROM Reglas_recepcion WHERE Activo = @Activo"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeReglas_recepcion As New clsBeReglas_recepcion

            For Each dr As DataRow In dt.Rows

                vBeReglas_recepcion = New clsBeReglas_recepcion
                Cargar(vBeReglas_recepcion, dr)
                lReturnList.Add(vBeReglas_recepcion)

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll_By_Ingreso(ByVal Activo As Boolean) As List(Of clsBeReglas_recepcion)


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeReglas_recepcion)
            Const sp As String = "SELECT * FROM Reglas_recepcion WHERE Activo = @Activo"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeReglas_recepcion As New clsBeReglas_recepcion

            For Each dr As DataRow In dt.Rows

                vBeReglas_recepcion = New clsBeReglas_recepcion
                Cargar(vBeReglas_recepcion, dr)

                If Not vBeReglas_recepcion.Es_Proceso Then
                    lReturnList.Add(vBeReglas_recepcion)
                End If

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll_By_Proceso(ByVal Activo As Boolean) As List(Of clsBeReglas_recepcion)


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeReglas_recepcion)
            Const sp As String = "SELECT * FROM Reglas_recepcion WHERE Activo = @Activo"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeReglas_recepcion As New clsBeReglas_recepcion

            For Each dr As DataRow In dt.Rows

                vBeReglas_recepcion = New clsBeReglas_recepcion
                Cargar(vBeReglas_recepcion, dr)

                If vBeReglas_recepcion.Es_Proceso Then
                    lReturnList.Add(vBeReglas_recepcion)
                End If
            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class

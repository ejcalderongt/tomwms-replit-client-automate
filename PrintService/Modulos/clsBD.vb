Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsBD
    Public Shared Property Instancia As New clsCadenaConexion

    Public Shared Function Xcute(ByVal pSQL$, ByRef lTrans As SqlTransaction, ByRef Conn As SqlConnection) As Double

        Dim cmd As New SqlCommand(pSQL, Conn)

        Try

            cmd.CommandTimeout = 30
            If Not lTrans Is Nothing Then cmd.Transaction = lTrans
            Xcute = cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Ejecuta sentencias "SELECT"  en la base de datos.
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="vSQL"></param>
    ''' <remarks></remarks>
    Public Sub OpenDT(ByRef dT As DataTable, ByVal vSql As String)

        Dim conn As New SqlConnection

        Try

            conn.ConnectionString = Instancia.CadenaConexionSQLClient
            conn.Open()

            Dim dAdapter As New SqlDataAdapter(vSql, conn)
            Dim dSet As New DataSet
            dAdapter.Fill(dSet, "Query")
            dT = dSet.Tables("Query")
            dAdapter.Dispose()
            dSet.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

End Class

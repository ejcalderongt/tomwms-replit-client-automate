Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS

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

    Public Shared Function GetLongitudByTabla(ByVal pNombreTabla As String) As List(Of clsTabla)

        Dim lReturnList As New List(Of clsTabla)

        Try

            'Validacion y estandarización de los datos
            Using lCnn As New SqlConnection(Instancia.CadenaConexionSQLClient)

                Dim lSQL As String = "SELECT col.name As columna, " _
                                                         & "TYP.name As Tipo, " _
                                                         & "Longitud = Case TYP.name " _
                                                         & "WHEN 'nvarchar' THEN col.LENGTH/2 " _
                                                         & "WHEN 'varchar' THEN col.LENGTH/2 " _
                                                         & "Else col.LENGTH " _
                                                         & "END " _
                                                         & "FROM syscolumns col " _
                                                         & "JOIN sysobjects OBJ ON OBJ.id = col.id " _
                                                         & "JOIN systypes TYP On TYP.xusertype = col.xtype " _
                                                         & "LEFT JOIN sysforeignkeys FK On FK.fkey = col.colid And FK.fkeyid=OBJ.id " _
                                                         & "LEFT JOIN sysobjects OBJ2 ON OBJ2.id = FK.rkeyid " _
                                                         & "LEFT JOIN syscolumns col2 On col2.colid = FK.rkey And col2.id = OBJ2.id " _
                                                         & "WHERE OBJ.name = '" & pNombreTabla & "' AND (OBJ.xtype='U' OR OBJ.xtype='V')"

                'Acceso a los datos.
                Using lDTA As New SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsTabla

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsTabla

                            Obj.NombreCampo = CType(lRow("Columna"), String)
                            Obj.Tipo = CType(lRow("Tipo"), String)
                            Obj.Longitud = CType(lRow("Longitud"), Integer)

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class

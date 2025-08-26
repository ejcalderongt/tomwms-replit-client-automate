Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnMontacarga_bodega

    Public Shared Function ListarxFiltro(ByVal pIdEmpresa As Integer, ByVal pFiltro As String) As List(Of clsBeMontacarga)

        Dim lReturnList As New List(Of clsBeMontacarga)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM MontaCarga WHERE 1>0  "

                If String.IsNullOrEmpty(pFiltro) = False Then
                    vSQL += " AND (IdMontaCarga LIKE '%@IdMontaCarga%'"
                    vSQL += " OR Nombre LIKE '%@Nombre%'"
                    vSQL += " OR modelo LIKE '%@modelo%'"
                    vSQL += " OR serie LIKE '%@serie%')"
                End If

                If pIdEmpresa <> 0 Then
                    vSQL += " AND IdEmpresa=@IdEmpresa"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    If String.IsNullOrEmpty(pFiltro) = False Then

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdMontaCarga", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@modelo", pFiltro)
                        lDTA.SelectCommand.Parameters.AddWithValue("@serie", pFiltro)
                    Else

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                    End If

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMontacarga

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMontacarga

                            Obj.IdMontacarga = CType(lRow("IdMontacarga"), System.Int32)

                            Obj.IdEmpresa = CType(lRow("IdEmpresa"), System.Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), System.String)
                            End If

                            If lRow("Modelo") IsNot DBNull.Value AndAlso lRow("Modelo") IsNot Nothing Then
                                Obj.Modelo = CType(lRow("Modelo"), System.String)
                            End If

                            If lRow("Serie") IsNot DBNull.Value AndAlso lRow("Serie") IsNot Nothing Then
                                Obj.Serie = CType(lRow("Serie"), System.String)
                            End If

                            If lRow("capacidad_basica") IsNot DBNull.Value AndAlso lRow("capacidad_basica") IsNot Nothing Then
                                Obj.Capacidad_basica = CType(lRow("capacidad_basica"), System.Double)
                            End If

                            If lRow("desplazamiento_motor") IsNot DBNull.Value AndAlso lRow("desplazamiento_motor") IsNot Nothing Then
                                Obj.Desplazamiento_motor = CType(lRow("desplazamiento_motor"), System.Double)
                            End If

                            If lRow("tipo_combustible") IsNot DBNull.Value AndAlso lRow("tipo_combustible") IsNot Nothing Then
                                Obj.Tipo_combustible = CType(lRow("tipo_combustible"), System.String)
                            End If

                            If lRow("tipo_montacarga") IsNot DBNull.Value AndAlso lRow("tipo_montacarga") IsNot Nothing Then
                                Obj.Tipo_montacarga = CType(lRow("tipo_montacarga"), System.String)
                            End If

                            If lRow("fecha_compra") IsNot DBNull.Value AndAlso lRow("fecha_compra") IsNot Nothing Then
                                Obj.Fecha_compra = CType(lRow("fecha_compra"), System.DateTime)
                            End If

                            If lRow("fecha_inicio_operaciones") IsNot DBNull.Value AndAlso lRow("fecha_inicio_operaciones") IsNot Nothing Then
                                Obj.Fecha_inicio_operaciones = CType(lRow("fecha_inicio_operaciones"), System.DateTime)
                            End If

                            If lRow("proximo_mantenimiento") IsNot DBNull.Value AndAlso lRow("proximo_mantenimiento") IsNot Nothing Then
                                Obj.Proximo_mantenimiento = CType(lRow("proximo_mantenimiento"), System.DateTime)
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

    Public Shared Function Get_All_By_IdMontaCarga(ByVal IdMontaCarga As Integer) As List(Of clsBeMontacarga_bodega)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeMontacarga_bodega)
            Const sp As String = "SELECT * FROM Montacarga_bodega WHERE IdMontaCarga = @IdMontaCarga AND Activo = 1"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdMontaCarga", IdMontaCarga)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMontacarga_bodega As New clsBeMontacarga_bodega

            For Each dr As DataRow In dt.Rows

                vBeMontacarga_bodega = New clsBeMontacarga_bodega
                Cargar(vBeMontacarga_bodega, dr)
                vBeMontacarga_bodega.Bodega = clsLnBodega.GetSingle_By_Idbodega(vBeMontacarga_bodega.IdBodega, lConnection, lTransaction)
                lReturnList.Add(vBeMontacarga_bodega)

            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Existe_By_IdBodega(ByVal pIdMontaCarga As Integer,
                                              ByVal pIdBodega As Integer) As Boolean

        Existe_By_IdBodega = False

        Try

            Const sp As String = "SELECT * FROM Montacarga_bodega 
                                  Where(IdMontacarga = @IdMontacarga AND IdBodega= @IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMONTACARGA", pIdMontaCarga))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Existe_By_IdBodega = (dt.Rows.Count = 1)

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class

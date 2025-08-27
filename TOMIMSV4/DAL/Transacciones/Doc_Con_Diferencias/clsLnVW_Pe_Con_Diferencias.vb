Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_Pe_Con_Diferencias

    Public Shared Sub Cargar(ByRef oBeVW_Pe_Con_Diferencias As clsBeVW_Pe_Con_Diferencias, ByRef dr As DataRow)
        Try
            With oBeVW_Pe_Con_Diferencias
                .ORDENPEDIDO = IIf(IsDBNull(dr.Item("ORDENPEDIDO")), 0, dr.Item("ORDENPEDIDO"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Cant_despachada = IIf(IsDBNull(dr.Item("cant_despachada")), 0.0, dr.Item("cant_despachada"))
                .PRESENTACION = IIf(IsDBNull(dr.Item("PRESENTACION")), "", dr.Item("PRESENTACION"))
                .DIFERENCIA = IIf(IsDBNull(dr.Item("DIFERENCIA")), 0.0, dr.Item("DIFERENCIA"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .BODEGA = IIf(IsDBNull(dr.Item("BODEGA")), "", dr.Item("BODEGA"))
                .PROPIETARIO = IIf(IsDBNull(dr.Item("PROPIETARIO")), "", dr.Item("PROPIETARIO"))
                .IdTipoPedido = IIf(IsDBNull(dr.Item("IdTipoPedido")), 0, dr.Item("IdTipoPedido"))
                .NOMBRE_PEDIDO = IIf(IsDBNull(dr.Item("NOMBRE_PEDIDO")), "", dr.Item("NOMBRE_PEDIDO"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .UMBas = IIf(IsDBNull(dr.Item("UMBas")), 0, dr.Item("UMBas"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Fecha_Pedido = IIf(IsDBNull(dr.Item("Fecha_Pedido")), Date.Now, dr.Item("Fecha_Pedido"))
                '.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                '.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub


    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Pe_Con_Diferencias"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeVW_Pe_Con_Diferencias)

        Dim lReturnList As New List(Of clsBeVW_Pe_Con_Diferencias)

        Try

            Const sp As String = "SELECT * FROM VW_Pe_Con_Diferencias"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeVW_Pe_Con_Diferencias As New clsBeVW_Pe_Con_Diferencias

                        For Each dr As DataRow In lDataTable.Rows
                            vBeVW_Pe_Con_Diferencias = New clsBeVW_Pe_Con_Diferencias()
                            Cargar(vBeVW_Pe_Con_Diferencias, dr)
                            lReturnList.Add(vBeVW_Pe_Con_Diferencias)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeVW_Pe_Con_Diferencias As clsBeVW_Pe_Con_Diferencias)

        Try

            Const sp As String = "SELECT * FROM VW_Pe_Con_Diferencias"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeVW_Pe_Con_Diferencias As New clsBeVW_Pe_Con_Diferencias

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeVW_Pe_Con_Diferencias, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM VW_Pe_Con_Diferencias"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_Movimientos_By_IdProducto(ByVal pFechaDel As Date,
                                                     ByVal pFechaAl As Date,
                                                     ByVal pIdProductoBodega As Integer,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pIdPropietario As Integer) As List(Of clsBeVW_Pe_Con_Diferencias)

        Dim lReturnList As New List(Of clsBeVW_Pe_Con_Diferencias)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT ORDENPEDIDO,CODIGO_PRODUCTO,NOMBRE_PRODUCTO,CANTIDAD,Cant_despachada,
					DIFERENCIA,PRESENTACION,IDPROPIETARIOBODEGA,BODEGA,PROPIETARIO,
					IdTipoPedido,NOMBRE_PEDIDO,IDPRODUCTOBODEGA,IDPRESENTACION,IDUNIDADMEDIDABASICA,UMBas,
					ESTADO,ACTIVO,Fecha_Pedido
                    FROM VW_Pe_Con_Diferencias
                    WHERE DIFERENCIA <> 0 AND IDPRODUCTOBODEGA=@IdProductoBodega"

            vSQL += " AND IdBodega=@IdBodega and IdPropietario=@IdPropietario"

            vSQL += String.Format(" And cast(Fecha_Pedido AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY Fecha_Pedido,ORDENPEDIDO"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Pe_Con_Diferencias

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Pe_Con_Diferencias
                                clsLnVW_Pe_Con_Diferencias.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_IdPropietario_And_Bodega(ByVal pFechaDel As Date,
                                                     ByVal pFechaAl As Date,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pIdPropietario As Integer) As List(Of clsBeVW_Pe_Con_Diferencias)

        Dim lReturnList As New List(Of clsBeVW_Pe_Con_Diferencias)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT ORDENPEDIDO,CODIGO_PRODUCTO,NOMBRE_PRODUCTO,CANTIDAD,Cant_despachada,
					DIFERENCIA,PRESENTACION,IDPROPIETARIOBODEGA,BODEGA,PROPIETARIO,
					IdTipoPedido,NOMBRE_PEDIDO,IDPRODUCTOBODEGA,IDPRESENTACION,IDUNIDADMEDIDABASICA,UMBas,
					ESTADO,ACTIVO,Fecha_Pedido
                    FROM VW_Pe_Con_Diferencias
                    WHERE DIFERENCIA <>0 AND IdBodega=@IdBodega and IdPropietario=@IdPropietario"

            vSQL += String.Format(" And cast(Fecha_Pedido AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY Fecha_Pedido,ORDENPEDIDO"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Pe_Con_Diferencias

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Pe_Con_Diferencias
                                clsLnVW_Pe_Con_Diferencias.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class

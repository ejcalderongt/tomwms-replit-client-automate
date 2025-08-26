Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_Existencia_Valores_Fiscales

    Public Shared Sub Cargar(ByRef oBeVW_Existencia_Valores_Fiscales As clsBeVW_Existencia_Valores_Fiscales, ByRef dr As DataRow)
        Try
            With oBeVW_Existencia_Valores_Fiscales
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                .Proveedor = IIf(IsDBNull(dr.Item("Proveedor")), "", dr.Item("Proveedor"))
                .Bodega = IIf(IsDBNull(dr.Item("Bodega")), "", dr.Item("Bodega"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .No_DocumentoOC = IIf(IsDBNull(dr.Item("No_DocumentoOC")), "", dr.Item("No_DocumentoOC"))
                .No_DocumentoRec = IIf(IsDBNull(dr.Item("No_DocumentoRec")), "", dr.Item("No_DocumentoRec"))
                .ReferenciaOC = IIf(IsDBNull(dr.Item("ReferenciaOC")), "", dr.Item("ReferenciaOC"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), Date.Now, dr.Item("Fecha"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .TipoTrans = IIf(IsDBNull(dr.Item("TipoTrans")), "", dr.Item("TipoTrans"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .Muelle = IIf(IsDBNull(dr.Item("Muelle")), "", dr.Item("Muelle"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Fecha_Agrego = IIf(IsDBNull(dr.Item("Fecha_Agrego")), Date.Now, dr.Item("Fecha_Agrego"))
                .CodigoProd = IIf(IsDBNull(dr.Item("CodigoProd")), "", dr.Item("CodigoProd"))
                .BarraProd = IIf(IsDBNull(dr.Item("BarraProd")), "", dr.Item("BarraProd"))
                .NombreProd = IIf(IsDBNull(dr.Item("NombreProd")), "", dr.Item("NombreProd"))
                .Recibido = IIf(IsDBNull(dr.Item("Recibido")), 0.0, dr.Item("Recibido"))
                .Existencia_Actual_UMBas = IIf(IsDBNull(dr.Item("Existencia_Actual_UMBas")), 0.0, dr.Item("Existencia_Actual_UMBas"))
                .Existencia_Actual_Pres = IIf(IsDBNull(dr.Item("Existencia_Actual_Pres")), 0.0, dr.Item("Existencia_Actual_Pres"))
                .UM = IIf(IsDBNull(dr.Item("UM")), "", dr.Item("UM"))
                .EstadoProd = IIf(IsDBNull(dr.Item("EstadoProd")), "", dr.Item("EstadoProd"))
                .PresProd = IIf(IsDBNull(dr.Item("PresProd")), "", dr.Item("PresProd"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Vence = IIf(IsDBNull(dr.Item("Vence")), Date.Now, dr.Item("Vence"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .Ubicacion_Origen = IIf(IsDBNull(dr.Item("Ubicacion_Origen")), "", dr.Item("Ubicacion_Origen"))
                '.NoPoliza = IIf(IsDBNull(dr.Item("NoPoliza")), "", dr.Item("NoPoliza"))
                .Valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0.0, dr.Item("valor_aduana"))
                .Valor_fob = IIf(IsDBNull(dr.Item("valor_fob")), 0.0, dr.Item("valor_fob"))
                .Valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0.0, dr.Item("valor_iva"))
                .Valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0.0, dr.Item("valor_dai"))
                .Valor_seguro = IIf(IsDBNull(dr.Item("valor_seguro")), 0.0, dr.Item("valor_seguro"))
                .Valor_flete = IIf(IsDBNull(dr.Item("valor_flete")), 0.0, dr.Item("valor_flete"))
                .Peso_neto = IIf(IsDBNull(dr.Item("peso_neto")), 0.0, dr.Item("peso_neto"))
                .codigo_poliza = IIf(IsDBNull(dr.Item("codigo_poliza")), 0.0, dr.Item("codigo_poliza"))
                .numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), 0.0, dr.Item("numero_orden"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Existencia_Valores_Fiscales"
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

    Public Shared Function Get_All() As List(Of clsBeVW_Existencia_Valores_Fiscales)

        Dim lReturnList As New List(Of clsBeVW_Existencia_Valores_Fiscales)

        Try

            Const sp As String = "SELECT * FROM VW_Existencia_Valores_Fiscales"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeVW_Existencia_Valores_Fiscales As New clsBeVW_Existencia_Valores_Fiscales

                        For Each dr As DataRow In lDataTable.Rows
                            vBeVW_Existencia_Valores_Fiscales = New clsBeVW_Existencia_Valores_Fiscales()
                            Cargar(vBeVW_Existencia_Valores_Fiscales, dr)
                            lReturnList.Add(vBeVW_Existencia_Valores_Fiscales)
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

    Public Shared Sub GetSingle(ByRef pBeVW_Existencia_Valores_Fiscales As clsBeVW_Existencia_Valores_Fiscales)

        Try

            Const sp As String = "SELECT * FROM VW_Existencia_Valores_Fiscales"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeVW_Existencia_Valores_Fiscales As New clsBeVW_Existencia_Valores_Fiscales

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeVW_Existencia_Valores_Fiscales, lDataTable.Rows(0))
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM VW_Existencia_Valores_Fiscales"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_IdProducto(ByVal pFechaDel As Date,
                                                     ByVal pFechaAl As Date,
                                                     ByVal pIdProductoBodega As Integer,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pIdPropietario As Integer) As List(Of clsBeVW_Existencia_Valores_Fiscales)

        Dim lReturnList As New List(Of clsBeVW_Existencia_Valores_Fiscales)

        Try

            Dim vSQL As String = ""

            vSQL = "select 
					IdRecepcionEnc,Propietario,Proveedor,Bodega,IdOrdenCompraEnc,
					No_DocumentoOC,No_DocumentoRec,ReferenciaOC,Fecha,Estado,
					TipoTrans,Descripcion,Muelle,Activo,Fecha_Agrego,
					CodigoProd,BarraProd,NombreProd,Recibido,Existencia_Actual_UMBas,Existencia_Actual_Pres,
					UM,EstadoProd,PresProd,Lic_plate,Factor,
					Lote,Vence,IdStock,Ubicacion_Origen,
					codigo_poliza,numero_orden,
					Valor_aduana,Valor_fob,Valor_iva,Valor_dai,Valor_seguro,Valor_flete,Peso_neto
					from VW_Existencia_Valores_Fiscales"

            'vSQL += " AND IdBodega=@IdBodega and IdPropietario=@IdPropietario"

            vSQL += String.Format(" where cast(FECHA AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " ORDER BY FECHA,IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        'lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        'lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        'lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Existencia_Valores_Fiscales

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Existencia_Valores_Fiscales
                                clsLnVW_Existencia_Valores_Fiscales.Cargar(Obj, lRow)
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
                                                     ByVal pIdPropietario As Integer) As List(Of clsBeVW_Existencia_Valores_Fiscales)

        Dim lReturnList As New List(Of clsBeVW_Existencia_Valores_Fiscales)

        Try

            Dim vSQL As String = ""

            vSQL = "select 
					IdRecepcionEnc,Propietario,Proveedor,Bodega,IdOrdenCompraEnc,
					No_DocumentoOC,No_DocumentoRec,ReferenciaOC,Fecha,Estado,
					TipoTrans,Descripcion,Muelle,Activo,Fecha_Agrego,
					CodigoProd,BarraProd,NombreProd,Recibido,Existencia_Actual_UMBas,Existencia_Actual_Pres,
					UM,EstadoProd,PresProd,Lic_plate,Factor,
					Lote,Vence,IdStock,Ubicacion_Origen,codigo_poliza,numero_orden,
					Valor_aduana,Valor_fob,Valor_iva,Valor_dai,Valor_seguro,Valor_flete,Peso_neto
					from VW_Existencia_Valores_Fiscales"

            vSQL += String.Format(" WHERE cast(FECHA AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += "ORDER BY FECHA,IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        'lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        'lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Existencia_Valores_Fiscales

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Existencia_Valores_Fiscales
                                clsLnVW_Existencia_Valores_Fiscales.Cargar(Obj, lRow)
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

Imports System.Data.SqlClient

Partial Public Class clsLnProducto_parametros
    Implements IDisposable

    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_parametros)

        Dim lReturnList As New List(Of clsBeProducto_parametros)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = String.Format("SELECT PP.*, '' AS valor_unico " &
                                                   " FROM producto_parametros PP " &
                                                   " WHERE PP.IdProducto={0} " &
                                                   " AND PP.activo={1}", pIdProducto, IIf(pActivo, 1, 0))


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_parametros

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_parametros
                            CargarHH(Obj, lRow)
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

    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer,
                                                 ByVal pActivo As Boolean,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_parametros)

        Dim lReturnList As New List(Of clsBeProducto_parametros)

        Try

            Dim vSQL As String = String.Format("SELECT PP.*, '' AS valor_unico 
                                             FROM producto_parametros PP 
                                             WHERE PP.IdProducto={0} 
                                             AND PP.activo={1}", pIdProducto, IIf(pActivo, 1, 0))


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_parametros

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_parametros
                        CargarHH(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_ProductoParametros_By_IdProducto_HH(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_parametros)

        Dim lReturnList As New List(Of clsBeProducto_parametros)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = String.Format("SELECT p.*, case when tipo = 'Lógico' THEN logico ELSE CASE WHEN tipo = 'Númerico' THEN numerico ELSE 
                                      CASE WHEN tipo = 'Texto' then texto ELSE CASE WHEN tipo = 'Fecha' THEN fecha END END END END AS valor_unico 
                                      FROM (SELECT pp.*, 
                                      CONVERT(NVARCHAR(50), IIf(pp.valor_logico = 0,'False', 'True')) AS logico,
                                      CONVERT(NVARCHAR(50), pp.valor_numerico) AS numerico,
                                      CONVERT(NVARCHAR(50),pp.valor_fecha,112) AS fecha, 
                                      CONVERT(NVARCHAR(50), pp.valor_texto) AS texto, p.tipo 
                                      FROM producto_parametros pp INNER JOIN p_parametro p ON p.idparametro = pp.idparametro
                                      WHERE IdProducto={0} AND pp.activo={1}) as p", pIdProducto, IIf(pActivo, 1, 0))


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_parametros

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_parametros
                            CargarHH(Obj, lRow)
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

    Public Shared Sub CargarHH(ByRef oBeProducto_parametros As clsBeProducto_parametros, ByRef dr As DataRow)

        Try

            With oBeProducto_parametros

                .IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), 0, dr.Item("IdProductoParametro"))
                .IdParametro = IIf(IsDBNull(dr.Item("IdParametro")), 0, dr.Item("IdParametro"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), Date.Now, dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .Capturar_siempre = IIf(IsDBNull(dr.Item("capturar_siempre")), False, dr.Item("capturar_siempre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .TipoParametro.IdParametro = .IdParametro
                clsLnP_parametro.Obtener(.TipoParametro)
                .Valor_Unico = IIf(IsDBNull(dr.Item("valor_unico")), "", dr.Item("valor_unico"))

            End With

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Shared Sub CargarHH(ByRef oBeProducto_parametros As clsBeProducto_parametros,
                               ByRef dr As DataRow,
                               ByRef lConnection As SqlConnection,
                               ByRef lTransaction As SqlTransaction)

        Try

            With oBeProducto_parametros

                .IdProductoParametro = IIf(IsDBNull(dr.Item("IdProductoParametro")), 0, dr.Item("IdProductoParametro"))
                .IdParametro = IIf(IsDBNull(dr.Item("IdParametro")), 0, dr.Item("IdParametro"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Valor_texto = IIf(IsDBNull(dr.Item("valor_texto")), "", dr.Item("valor_texto"))
                .Valor_numerico = IIf(IsDBNull(dr.Item("valor_numerico")), 0.0, dr.Item("valor_numerico"))
                .Valor_fecha = IIf(IsDBNull(dr.Item("valor_fecha")), Date.Now, dr.Item("valor_fecha"))
                .Valor_logico = IIf(IsDBNull(dr.Item("valor_logico")), False, dr.Item("valor_logico"))
                .Capturar_siempre = IIf(IsDBNull(dr.Item("capturar_siempre")), False, dr.Item("capturar_siempre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .TipoParametro.IdParametro = .IdParametro
                clsLnP_parametro.Obtener(.TipoParametro, lConnection, lTransaction)
                .Valor_Unico = IIf(IsDBNull(dr.Item("valor_unico")), "", dr.Item("valor_unico"))

            End With

        Catch ex As Exception
            Throw New Exception("Producto_parametros_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoParametro),0) FROM producto_parametros"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Desactivar(ByVal pIdProductoParametro As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("UPDATE producto_parametros SET Activo=0 WHERE IdProductoParametro={0}", pIdProductoParametro), lConnection)

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

    Public Shared Function GetSingle(ByVal pIdProductoParametro As Integer) As clsBeProducto_parametros

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = String.Format("SELECT * FROM VW_ProductoParametro WHERE IdProductoParametro={0}", pIdProductoParametro)

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeProducto_parametros

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                        Dim lRow As DataRow = lDT.Rows(0)
                        Obj = New clsBeProducto_parametros()

                        Obj.IdProductoParametro = CType(lRow("IdProductoParametro"), Int32)

                        If lRow("IdParametro") IsNot DBNull.Value AndAlso lRow("IdParametro") IsNot Nothing Then
                            Obj.IdParametro = CType(lRow("IdParametro"), Int32)
                        End If

                        If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                            Obj.IdProducto = CType(lRow("IdProducto"), Int32)
                        End If

                        If lRow("valor_texto") IsNot DBNull.Value AndAlso lRow("valor_texto") IsNot Nothing Then
                            Obj.Valor_texto = CType(lRow("valor_texto"), String)
                        End If

                        If lRow("valor_numerico") IsNot DBNull.Value AndAlso lRow("valor_numerico") IsNot Nothing Then
                            Obj.Valor_numerico = CType(lRow("valor_numerico"), Double)
                        End If

                        If lRow("valor_fecha") IsNot DBNull.Value AndAlso lRow("valor_fecha") IsNot Nothing Then
                            Obj.Valor_fecha = CType(lRow("valor_fecha"), DateTime)
                        End If

                        If lRow("valor_logico") IsNot DBNull.Value AndAlso lRow("valor_logico") IsNot Nothing Then
                            Obj.Valor_logico = CType(lRow("valor_logico"), Boolean)
                        End If

                        If lRow("Capturar_Siempre") IsNot DBNull.Value AndAlso lRow("Capturar_Siempre") IsNot Nothing Then
                            Obj.Capturar_siempre = CType(lRow("Capturar_Siempre"), Boolean)
                        End If
                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("activo"), Boolean)
                        End If
                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                            Obj.User_agr = CType(lRow("user_agr"), String)
                        End If
                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                            Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
                        End If
                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                            Obj.User_mod = CType(lRow("user_mod"), String)
                        End If
                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                            Obj.Fec_mod = CType(lRow("fec_mod"), DateTime)
                        End If

                        If lRow("tipo") IsNot DBNull.Value AndAlso lRow("tipo") IsNot Nothing Then
                            Obj.TipoParametro = New clsBeP_parametro()
                            Obj.TipoParametro.Tipo = CType(lRow("tipo"), String)
                        End If
                        Return Obj
                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
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

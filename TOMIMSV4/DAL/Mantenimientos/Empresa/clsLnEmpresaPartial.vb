Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnEmpresa
    Implements IDisposable

    Public Shared Function Get_List_Empresas_For_HH(ByVal IdEmpresa As Integer) As List(Of clsBeEmpresa)

        Get_List_Empresas_For_HH = Nothing

        Dim vBeEmpresa As New clsBeEmpresa
        Dim lEmpresas As New List(Of clsBeEmpresa)

        Try

            Dim vSQL As String = "SELECT " &
                    " IdEmpresa, nombre, direccion, telefono, email, razon_social, representante, corr_cod_barra, " &
                    " path_printer, activo, user_agr, fec_agr, user_mod, fec_mod, clienteRapido, operador_logistico, " &
                    " puerto_escaner, control_presentaciones, anulaciones_por_supervisor, codigo, clave, intento, " &
                    " duracionclave, duracionclavetemporal " &
                    " FROM EMPRESA WHERE IdEmpresa = @IdEmpresa And Activo = 1"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    For Each Dr As DataRow In lDataTable.Rows

                        vBeEmpresa = New clsBeEmpresa
                        Cargar(vBeEmpresa, Dr)
                        lEmpresas.Add(vBeEmpresa)

                    Next

                End Using

            End Using

            Return lEmpresas

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_List_Empresas_For_HH() As List(Of clsBeEmpresa)

        Get_List_Empresas_For_HH = Nothing

        Dim vBeEmpresa As New clsBeEmpresa
        Dim lEmpresas As New List(Of clsBeEmpresa)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT " &
                    " IdEmpresa, nombre, direccion, telefono, email, razon_social, representante, corr_cod_barra, " &
                    " path_printer, activo, user_agr, fec_agr, user_mod, fec_mod, clienteRapido, operador_logistico, " &
                    " puerto_escaner, control_presentaciones, anulaciones_por_supervisor, codigo, clave, intento, " &
                    " duracionclave, duracionclavetemporal " &
                    " FROM EMPRESA WHERE Activo = 1"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    For Each Dr As DataRow In lDataTable.Rows

                        vBeEmpresa = New clsBeEmpresa
                        Cargar(vBeEmpresa, Dr)
                        lEmpresas.Add(vBeEmpresa)

                    Next

                End Using

            End Using

            Return lEmpresas

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exigir_Politica_Contraseñas(ByVal IdEmpresa As Integer) As Boolean

        Exigir_Politica_Contraseñas = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT politica_contraseñas " &
                        " FROM EMPRESA WHERE IdEmpresa = @IdEmpresa And Activo = 1"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Dr As DataRow
                    Dr = lDataTable.Rows(0)

                    Exigir_Politica_Contraseñas = IIf(IsDBNull(Dr.Item("politica_contraseñas")), False, Dr.Item("politica_contraseñas"))

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("GetAllFiltro_Empresa: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Empresa_By_Codigo_And_Clave(ByVal Codigo As String, ByVal Clave As String) As clsBeEmpresa

        Get_Empresa_By_Codigo_And_Clave = Nothing

        Dim vBeEmpresa As New clsBeEmpresa

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT " &
                    " IdEmpresa, nombre, direccion, telefono, email, razon_social, representante, corr_cod_barra, " &
                    " path_printer, activo, user_agr, fec_agr, user_mod, fec_mod, clienteRapido, operador_logistico, " &
                    " puerto_escaner, control_presentaciones, anulaciones_por_supervisor, codigo, clave, intento, " &
                    " duracionclave, duracionclavetemporal " &
                    " FROM EMPRESA WHERE Codigo = @Codigo And Clave = @Clave And Activo = 1"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", Codigo)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Clave", clsPublic.Encriptar(Clave))

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    For Each Dr As DataRow In lDataTable.Rows

                        vBeEmpresa = New clsBeEmpresa
                        Cargar(vBeEmpresa, Dr)

                    Next

                    lDTA.Dispose()

                End Using

                lConnection.Close()

            End Using

            Return vBeEmpresa

        Catch ex As Exception
            Throw New Exception("GetAllFiltro_Empresa: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetStrBD() As String
        Return Configuration.ConfigurationManager.AppSettings("CST")
    End Function

    Public Shared Function Get_Id_Motivo_Ajuste(ByVal pIdEmpresa As Integer) As Integer

        Get_Id_Motivo_Ajuste = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdMotivoAjusteInventario FROM empresa WHERE idempresa = @idempresa "

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.Parameters.AddWithValue("@idempresa", pIdEmpresa)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Get_Id_Motivo_Ajuste = CInt(lReturnValue)
                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Registros_Tabla_Relacionada(ByVal pIdCampoo As Integer, ByVal pNombreTabla As String, ByVal pNombreCampo As String) As Integer

        Get_Cantidad_Registros_Tabla_Relacionada = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = String.Format("SELECT COUNT({0}) FROM {1} WHERE {0}={2}", pNombreCampo, pNombreTabla, pIdCampoo)

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Get_Cantidad_Registros_Tabla_Relacionada = CInt(lReturnValue)
                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Tablas_Relacionadas(ByVal pNombreTabla As String, ByVal IdToDelete As Integer) As List(Of clsBeTablasRelacionadas)

        Try
            Dim lReturnList As New List(Of clsBeTablasRelacionadas)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT  t.name as nombreTabla, fk.constraint_column_id as noRelaciones, c.name as columnaForanea  from  sys.foreign_key_columns as fk inner join  sys.tables as t  on fk.parent_object_id = t.object_id inner join  sys.columns as c on fk.parent_object_id = c.object_id and fk.parent_column_id = c.column_id where fk.referenced_object_id = (select object_id from sys.tables where name = @name) order by nombreTabla, noRelaciones"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@name", pNombreTabla)
                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeTablasRelacionadas

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBeTablasRelacionadas()

                            If lRow("nombreTabla") IsNot DBNull.Value AndAlso lRow("nombreTabla") IsNot Nothing Then
                                Obj.NombreTabla = CType(lRow("nombreTabla"), String)
                            End If

                            If lRow("ColumnaForanea") IsNot DBNull.Value AndAlso lRow("ColumnaForanea") IsNot Nothing Then
                                Obj.ColumnaForanea = CType(lRow("ColumnaForanea"), String)
                            End If

                            Obj.NoRelaciones = Get_Cantidad_Registros_Tabla_Relacionada(IdToDelete, Obj.NombreTabla, Obj.ColumnaForanea)

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "Select idEmpresa as Código,Nombre,Telefono,Email,Razon_Social,Representante " &
                               "from empresa WHERE 1 > 0 "
            If pActivo Then
                sp += " and activo=1 "
            Else
                sp += " and activo=0 "
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Listar_Empresa: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetAllForComboBox() As DataTable

        Try

            Const sp As String = "SELECT IdEmpresa, Nombre FROM Empresa"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable("clsBeEmpresa")

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetImagen(ByVal IdEmpresa As Integer) As Byte()

        GetImagen = Nothing

        Try

            Dim vSQL As String = "SELECT imagen FROM Empresa where IdEmpresa=@IdEmpresa"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Return lReturnValue
                    End If

                End Using

            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Android_Get_All() As List(Of clsBeEmpresaBase)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeEmpresaBase)
            Const sp As String = "SELECT IdEmpresa, Nombre, Imagen, buscar_actualizacion_hh FROM Empresa"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            For Each dr As DataRow In dt.Rows

                Dim vBeEmpresa As New clsBeEmpresaBase
                Android_Cargar(vBeEmpresa, dr)
                lReturnList.Add(vBeEmpresa)

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Private Shared Sub Android_Cargar(ByRef oBeEmpresa As clsBeEmpresaBase, ByRef dr As DataRow)

        Try

            With oBeEmpresa
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))
                .buscar_actualizacion_hh = IIf(IsDBNull(dr.Item("buscar_actualizacion_hh")), False, dr.Item("buscar_actualizacion_hh"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Get_cantidad_decimales_despliegue(ByVal pIdEmpresa As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim Cantidad_Decimales = 0

            Dim lCommand As New SqlCommand

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL As String = "SELECT cantidad_decimales_despliegue FROM EMPRESA WHERE IdEmpresa=@IdEmpresa"


            lCommand = New SqlCommand(vSQL, lConnection, lTransaction)

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Cantidad_Decimales = CDbl(lReturnValue)
            End If

            lConnection.Close()

            Return Cantidad_Decimales

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_cantidad_decimales_calculo(ByVal pIdEmpresa As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim Cantidad_Decimales = 0

            Dim lCommand As New SqlCommand

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL As String = "SELECT cantidad_decimales_calculo  FROM EMPRESA WHERE IdEmpresa=@IdEmpresa"


            lCommand = New SqlCommand(vSQL, lConnection, lTransaction)

            lCommand.CommandType = CommandType.Text
            lCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Cantidad_Decimales = CDbl(lReturnValue)
            End If

            lConnection.Close()

            Return Cantidad_Decimales

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle_By_IdBodega(ByRef pBeEmpresa As clsBeEmpresa,
                                                 ByVal pIdBodega As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction)

        GetSingle_By_IdBodega = Nothing

        Try

            Const sp As String = "SELECT Empresa.* 
                                  FROM Bodega INNER JOIN Empresa ON Bodega.IdEmpresa = Empresa.IdEmpresa
                                  Where(IdBodega = @IdBodega)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim BeEmpresa As New clsBeEmpresa
                Cargar(BeEmpresa, dt.Rows(0))
                Return BeEmpresa
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdBodega(ByRef pBeEmpresa As clsBeEmpresa,
                                                 ByVal pIdBodega As Integer)

        Try

            Const sp As String = "SELECT Empresa.* 
                                  FROM Bodega INNER JOIN Empresa ON Bodega.IdEmpresa = Empresa.IdEmpresa
                                  Where(IdBodega = @IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeEmpresa, dt.Rows(0))
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


    Public Shared Function GetSingle_By_IdBodega(ByVal pIdBodega As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeEmpresa

        GetSingle_By_IdBodega = Nothing

        Try

            Const sp As String = "SELECT Empresa.* 
                                  FROM Bodega INNER JOIN Empresa ON Bodega.IdEmpresa = Empresa.IdEmpresa
                                  Where(IdBodega = @IdBodega)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim BeEmpresa As New clsBeEmpresa
                Cargar(BeEmpresa, dt.Rows(0))
                Return BeEmpresa
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("Error_202303011547: {0} {1} El valor IdBodega es: " & pIdBodega, MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdBodega(ByVal pIdBodega As Integer) As clsBeEmpresa

        GetSingle_By_IdBodega = Nothing

        Try

            Const sp As String = "SELECT Empresa.* 
                                  FROM Bodega INNER JOIN Empresa ON Bodega.IdEmpresa = Empresa.IdEmpresa
                                  Where(IdBodega = @IdBodega)"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(sp, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim vBeEmpresa = New clsBeEmpresa
                    Cargar(vBeEmpresa, lDataTable.Rows(0))

                    GetSingle_By_IdBodega = vBeEmpresa

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Id_Motivo_Ajuste(ByVal pIdEmpresa As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_Id_Motivo_Ajuste = 0

        Try

            Dim vSQL As String = "SELECT IdMotivoAjusteInventario FROM empresa WHERE idempresa = @idempresa "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.Parameters.AddWithValue("@idempresa", pIdEmpresa)
                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Id_Motivo_Ajuste = CInt(lReturnValue)
                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_Empresa_By_IdEmpresa(ByVal pIdEmpresa As Integer) As String

        Get_Nombre_Empresa_By_IdEmpresa = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Dim vSQL As String = "SELECT Nombre FROM Empresa WHERE IdEmpresa=@IdEmpresa"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count > 0 Then
                            Get_Nombre_Empresa_By_IdEmpresa = IIf(IsDBNull(lDataTable.Rows(0).Item("Nombre")), "", lDataTable.Rows(0).Item("Nombre"))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdEmpresa_By_IdBodega(ByVal pIdBodega As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As Integer

        GetIdEmpresa_By_IdBodega = 0

        Try

            Const sp As String = "SELECT Empresa.* 
                                  FROM Bodega INNER JOIN Empresa ON Bodega.IdEmpresa = Empresa.IdEmpresa
                                  Where(IdBodega = @IdBodega)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim BeEmpresa As New clsBeEmpresa
                GetIdEmpresa_By_IdBodega = dt.Rows(0).Item("IdEmpresa")
            End If

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

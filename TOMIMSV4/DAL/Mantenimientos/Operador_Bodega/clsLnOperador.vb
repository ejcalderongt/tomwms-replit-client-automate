Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnOperador

    Public Shared Sub Cargar(ByRef oBeOperador As clsBeOperador, ByRef dr As DataRow)

        Try

            With oBeOperador

                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdRolOperador = IIf(IsDBNull(dr.Item("IdRolOperador")), 0, dr.Item("IdRolOperador"))
                .IdJornada = IIf(IsDBNull(dr.Item("IdJornada")), 0, dr.Item("IdJornada"))
                .Nombres = IIf(IsDBNull(dr.Item("nombres")), "", dr.Item("nombres"))
                .Apellidos = IIf(IsDBNull(dr.Item("apellidos")), "", dr.Item("apellidos"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Clave = IIf(IsDBNull(dr.Item("clave")), "", dr.Item("clave"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Costo_hora = IIf(IsDBNull(dr.Item("costo_hora")), 0.0, dr.Item("costo_hora"))
                .Usa_hh = IIf(IsDBNull(dr.Item("usa_hh")), False, dr.Item("usa_hh"))
                .Foto = IIf(IsDBNull(dr.Item("foto")), Nothing, dr.Item("foto"))
                '#EJC_20200407_WMS_Features: Operadores DAL
                .Recibe = IIf(IsDBNull(dr.Item("Recibe")), False, dr.Item("Recibe"))
                .Ubica = IIf(IsDBNull(dr.Item("ubica")), False, dr.Item("ubica"))
                .Transporta = IIf(IsDBNull(dr.Item("transporta")), False, dr.Item("transporta"))
                .Pickea = IIf(IsDBNull(dr.Item("pickea")), False, dr.Item("pickea"))
                .Verifica = IIf(IsDBNull(dr.Item("verifica")), False, dr.Item("verifica"))
                .Montacarga = IIf(IsDBNull(dr.Item("Montacarga")), False, dr.Item("Montacarga"))
                '#GT26012024: operador para resolucion LP en recepcion BOF
                .sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeOperador As clsBeOperador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("operador")
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idroloperador", "@idroloperador", DataType.Parametro)
            Ins.Add("idjornada", "@idjornada", DataType.Parametro)
            Ins.Add("nombres", "@nombres", DataType.Parametro)
            Ins.Add("apellidos", "@apellidos", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("clave", "@clave", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("costo_hora", "@costo_hora", DataType.Parametro)
            Ins.Add("usa_hh", "@usa_hh", DataType.Parametro)

            '#EJC_20200407_WMS_Features: Operadores DAL_Insert
            Ins.Add("recibe", "@recibe", DataType.Parametro)
            Ins.Add("ubica", "@ubica", DataType.Parametro)
            Ins.Add("transporta", "@transporta", DataType.Parametro)
            Ins.Add("pickea", "@pickea", DataType.Parametro)
            Ins.Add("verifica", "@verifica", DataType.Parametro)
            Ins.Add("montacarga", "@montacarga", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)

            If Not oBeOperador.Foto Is Nothing Then Ins.Add("foto", "@foto", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeOperador.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDROLOPERADOR", oBeOperador.IdRolOperador))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeOperador.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES", oBeOperador.Nombres))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS", oBeOperador.Apellidos))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeOperador.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeOperador.Telefono))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeOperador.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeOperador.Clave))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeOperador.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeOperador.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeOperador.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeOperador.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@COSTO_HORA", oBeOperador.Costo_hora))
            cmd.Parameters.Add(New SqlParameter("@USA_HH", oBeOperador.Usa_hh))

            '#EJC_20200407_WMS_Features: Operadores DAL_Insert_Parameters
            cmd.Parameters.Add(New SqlParameter("@recibe", oBeOperador.Recibe))
            cmd.Parameters.Add(New SqlParameter("@ubica", oBeOperador.Ubica))
            cmd.Parameters.Add(New SqlParameter("@transporta", oBeOperador.Transporta))
            cmd.Parameters.Add(New SqlParameter("@pickea", oBeOperador.Pickea))
            cmd.Parameters.Add(New SqlParameter("@verifica", oBeOperador.Verifica))
            cmd.Parameters.Add(New SqlParameter("@montacarga", oBeOperador.Montacarga))
            cmd.Parameters.Add(New SqlParameter("@sistema", oBeOperador.Sistema))

            If Not oBeOperador.Foto Is Nothing Then cmd.Parameters.Add(New SqlParameter("@FOTO", oBeOperador.Foto))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeOperador.IdOperador = CInt(cmd.Parameters("@IDOPERADOR").Value)

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

    Public Shared Function Actualizar(ByRef oBeOperador As clsBeOperador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("operador")
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idroloperador", "@idroloperador", DataType.Parametro)
            Upd.Add("idjornada", "@idjornada", DataType.Parametro)
            Upd.Add("nombres", "@nombres", DataType.Parametro)
            Upd.Add("apellidos", "@apellidos", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("clave", "@clave", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("costo_hora", "@costo_hora", DataType.Parametro)
            Upd.Add("usa_hh", "@usa_hh", DataType.Parametro)
            If Not oBeOperador.Foto Is Nothing Then Upd.Add("foto", "@foto", DataType.Parametro)

            '#EJC_20200407_WMS_Features: Operadores DAL_Update_Fields
            Upd.Add("recibe", "@recibe", DataType.Parametro)
            Upd.Add("ubica", "@ubica", DataType.Parametro)
            Upd.Add("transporta", "@transporta", DataType.Parametro)
            Upd.Add("pickea", "@pickea", DataType.Parametro)
            Upd.Add("verifica", "@verifica", DataType.Parametro)
            Upd.Add("montacarga", "@montacarga", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)

            Upd.Where("IdOperador = @IdOperador")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeOperador.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDROLOPERADOR", oBeOperador.IdRolOperador))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeOperador.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@NOMBRES", oBeOperador.Nombres))
            cmd.Parameters.Add(New SqlParameter("@APELLIDOS", oBeOperador.Apellidos))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeOperador.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeOperador.Telefono))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeOperador.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeOperador.Clave))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeOperador.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeOperador.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeOperador.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeOperador.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@COSTO_HORA", oBeOperador.Costo_hora))
            cmd.Parameters.Add(New SqlParameter("@USA_HH", oBeOperador.Usa_hh))

            '#EJC_20200407_WMS_Features: Operadores DAL_Update_Parameters
            cmd.Parameters.Add(New SqlParameter("@recibe", oBeOperador.Recibe))
            cmd.Parameters.Add(New SqlParameter("@ubica", oBeOperador.Ubica))
            cmd.Parameters.Add(New SqlParameter("@transporta", oBeOperador.Transporta))
            cmd.Parameters.Add(New SqlParameter("@pickea", oBeOperador.Pickea))
            cmd.Parameters.Add(New SqlParameter("@verifica", oBeOperador.Verifica))
            cmd.Parameters.Add(New SqlParameter("@montacarga", oBeOperador.Montacarga))
            cmd.Parameters.Add(New SqlParameter("@sistema", oBeOperador.Sistema))

            If Not oBeOperador.Foto Is Nothing Then cmd.Parameters.Add(New SqlParameter("@FOTO", oBeOperador.Foto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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


    Public Shared Function Eliminar(ByRef oBeOperador As clsBeOperador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Operador" &
             "  Where(IdOperador = @IdOperador)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IdOperador))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Operador"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Operador"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeOperador As clsBeOperador) As Boolean

        Try

            Const sp As String = "SELECT * FROM Operador" &
            " Where(IdOperador = @IdOperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IDOPERADOR))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeOperador, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeOperador As clsBeOperador,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Operador 
                                  Where(IdOperador = @IdOperador AND activo=1)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Cargar(oBeOperador, dt.Rows(0))

                oBeOperador.RolOperador.IdRolOperador = oBeOperador.IdRolOperador

                If clsLnRol_operador.GetSingle(oBeOperador.RolOperador,
                                               lConnection,
                                               lTransaction) Then
                    oBeOperador.RolOperador.ListMenuRolOp = clsLnMenu_rol_op.Get_List_Menu_By_IdRolOperador(oBeOperador.IdRolOperador,
                                                                                                            lConnection,
                                                                                                            lTransaction)
                End If

                Return True

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeOperador As clsBeOperador,
                                   ByRef dr As DataRow,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Cargar(oBeOperador, dr)

            clsLnRol_operador.Cargar(oBeOperador.RolOperador, dr)

            oBeOperador.RolOperador.ListMenuRolOp = clsLnMenu_rol_op.Get_List_Menu_By_IdRolOperador(oBeOperador.IdRolOperador, lConnection, lTransaction)

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ObtenerByInventario(ByRef oBeOperador As clsBeOperador) As Boolean

        Try

            Const sp As String = "SELECT * FROM Operador" &
            " Where(IdOperador = @IdOperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador.IdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeOperador, dt.Rows(0))
            Else
                Return False
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

    Public Shared Function GetAll() As List(Of clsBeOperador)

        Try

            Dim lReturnList As New List(Of clsBeOperador)
            Const sp As String = "SELECT * FROM Operador "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeOperador As New clsBeOperador

            For Each dr As DataRow In dt.Rows

                vBeOperador = New clsBeOperador
                Cargar(vBeOperador, dr)
                lReturnList.Add(vBeOperador)

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

    Public Shared Function GetAllForCombo() As DataTable

        Try

            Const sp As String = "SELECT IdOperador,nombres + ' ' + apellidos as Nombre FROM Operador WHERE activo=1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
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

    Public Shared Function GetAllForCombo_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Try

            Const sp As String = "SELECT Operador.IdOperador,Operador.nombres + ' ' + Operador.apellidos as Nombre 
                                  FROM Operador inner join operador_bodega ON Operador.IdOperador = operador_bodega.IdOperador
                                  WHERE Operador.activo=1 AND operador_bodega.IdBodega = @IdBodega AND operador_bodega.activo =1 "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
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

    Public Shared Function Get_All_For_Combo_By_Rol_Inventario(ByVal pIdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_For_Combo_By_Rol_Inventario = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT distinct Operador.IdOperador,Operador.nombres + ' ' + Operador.apellidos as Nombre
                                  FROM Operador inner join 
                                       operador_bodega ON Operador.IdOperador = operador_bodega.IdOperador inner join
	                                   menu_rol_op m on m.IdRolOperador = operador.IdRolOperador inner join
	                                   menu_sistema_op s on s.IdMenuSistemaOP = m.IdMenuSistemaOP
                                  WHERE Operador.activo=1 AND operador_bodega.IdBodega = @IdBodega AND operador_bodega.activo =1
                                        and s.Nombre = 'Inventario'"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Get_All_For_Combo_By_Rol_Inventario = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeOperador As clsBeOperador) As Boolean

        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Operador " &
            " Where(IdOperador = @IdOperador)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pBeOperador.IdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeOperador, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOperador),0) FROM Operador"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOperador(ByVal pIdOperador As Integer) As clsBeOperador

        Get_Single_By_IdOperador = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Operador " &
                                 " Where(IdOperador = @IdOperador) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pIdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeOperador As New clsBeOperador
                Cargar(pBeOperador, dt.Rows(0))
                Get_Single_By_IdOperador = pBeOperador
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOperador(ByVal pIdOperador As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As clsBeOperador

        Get_Single_By_IdOperador = Nothing

        Try

            Const sp As String = "SELECT * FROM Operador " &
        " Where(IdOperador = @IdOperador)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pIdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeOperador As New clsBeOperador
                Cargar(pBeOperador, dt.Rows(0))
                Get_Single_By_IdOperador = pBeOperador
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#CKFK20230126 Agregué esta función para obtener el operador
    Public Shared Function Get_Single_By_IdOperadorBodega(ByVal pIdOperadorBodega As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As clsBeOperador

        Get_Single_By_IdOperadorBodega = Nothing

        Try

            Const sp As String = "SELECT Operador.* 
                                  FROM Operador INNER JOIN operador_bodega ON Operador.IdOperador = operador_bodega.IdOperador " &
                                  " Where(operador_bodega.IdOperadorBodega = @IdOperadorBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeOperador As New clsBeOperador
                Cargar(pBeOperador, dt.Rows(0))
                Get_Single_By_IdOperadorBodega = pBeOperador
            End If


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'GT27062022_1944: Obtener operador para reabasto por el nombre
    Public Shared Function Get_Operador_By_Name(ByRef oBeOperador As clsBeOperador,
                                                ByVal pNombres As String,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As clsBeOperador

        Get_Operador_By_Name = Nothing

        Try

            Const sp As String = "SELECT * FROM Operador 
                                  Where(nombres + ' ' + apellidos=@pNombre AND activo=1)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@pNombre", pNombres))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Cargar(oBeOperador, dt.Rows(0))

                oBeOperador.RolOperador.IdRolOperador = oBeOperador.IdRolOperador

                If clsLnRol_operador.GetSingle(oBeOperador.RolOperador,
                                               lConnection,
                                               lTransaction) Then
                    oBeOperador.RolOperador.ListMenuRolOp = clsLnMenu_rol_op.Get_List_Menu_By_IdRolOperador(oBeOperador.IdRolOperador,
                                                                                                          lConnection,
                                                                                                            lTransaction)

                    Get_Operador_By_Name = oBeOperador
                End If

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_BOF() As clsBeOperador

        GetSingle_By_BOF = Nothing

        Dim BeOperador As New clsBeOperador()
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Operador  Where(sistema = 1)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                BeOperador = New clsBeOperador()
                Cargar(BeOperador, dt.Rows(0))
                GetSingle_By_BOF = BeOperador
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function GetSingle_By_BOF(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeOperador

        GetSingle_By_BOF = Nothing

        Dim BeOperador As New clsBeOperador()

        Try

            Const sp As String = "SELECT * FROM Operador  Where(sistema = 1)"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                BeOperador = New clsBeOperador()
                Cargar(BeOperador, dt.Rows(0))
                GetSingle_By_BOF = BeOperador
            End If

        Catch ex1 As SqlException
            Throw ex1
        End Try

    End Function

    Public Shared Function Get_All_For_Combo_By_Rol_Inventario(ByVal pIdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_For_Combo_By_Rol_Inventario = Nothing

        Try

            Const sp As String = "SELECT distinct Operador.IdOperador,Operador.nombres + ' ' + Operador.apellidos as Nombre
                                  FROM Operador inner join 
                                       operador_bodega ON Operador.IdOperador = operador_bodega.IdOperador inner join
	                                   menu_rol_op m on m.IdRolOperador = operador.IdRolOperador inner join
	                                   menu_sistema_op s on s.IdMenuSistemaOP = m.IdMenuSistemaOP
                                  WHERE Operador.activo=1 AND operador_bodega.IdBodega = @IdBodega AND operador_bodega.activo =1
                                        and s.Nombre = 'Inventario'"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Get_All_For_Combo_By_Rol_Inventario = dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#CKFK2024118: Obtener nombre del operador by IdOperador
    Public Shared Function Get_Nombre_By_IdOperador(ByVal pIdOperador As Integer) As String

        Get_Nombre_By_IdOperador = ""

        Try

            Const vSQL As String = "SELECT nombres + ' ' + apellidos Nombre  
                                    FROM operador o
                                    WHERE o.IdOperador=@pIdOperador AND activo=1 "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOperador", pIdOperador)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Get_Nombre_By_IdOperador = IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

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


End Class

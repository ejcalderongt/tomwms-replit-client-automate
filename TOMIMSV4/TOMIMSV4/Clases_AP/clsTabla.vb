
Imports System.Data.SqlClient

Public Class clsTabla

    Public Property NombreCampo As String
    Public Property Tipo As String
    Public Property Longitud As Integer

    Public Shared Sub Verificar_Tablas_Sistema()

        Dim lConnection As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction()

            ' Verificamos la tabla trans_re_tr
            For i As Integer = 1 To 9
                If clsLnTrans_re_tr.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeTrans_re_tr(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.IdTipoTransaccion) = False Then
                        clsLnTrans_re_tr.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla trans_pe_tipo
            For i As Integer = 1 To 4
                If clsLnTrans_pe_tipo.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeTrans_pe_tipo(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then
                        clsLnTrans_pe_tipo.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla trans_oc_estado
            For i As Integer = 1 To 6
                If clsLnTrans_oc_estado.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeTrans_oc_estado(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Nombre) = False Then
                        clsLnTrans_oc_estado.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla trans_oc_tipo
            For i As Integer = 1 To 6
                If clsLnTrans_oc_estado.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeTrans_oc_estado(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Nombre) = False Then
                        clsLnTrans_oc_estado.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla tipo_rotacion
            For i As Integer = 1 To 3
                If clsLnTipo_rotacion.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeTipo_rotacion(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then
                        clsLnTipo_rotacion.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla tipo_actualizacion_costo
            For i As Integer = 1 To 6
                If clsLnTipo_actualizacion_costo.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeTipo_actualizacion_costo(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.NombreActualizacionCosto) = False Then
                        clsLnTipo_actualizacion_costo.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla sis_tipo_tarea
            For i As Integer = 1 To 8
                If clsLnSis_tipo_tarea.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeSis_tipo_tarea(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Nombre) = False Then
                        clsLnSis_tipo_tarea.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla sis_tipo_accion
            For i As Integer = 1 To 2
                If clsLnSis_tipo_accion.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeSis_tipo_accion(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Nombre) = False Then
                        clsLnSis_tipo_accion.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla sis_tipo_accion
            For i As Integer = 1 To 4
                If clsLnSis_prioridad_tarea_hh.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeSis_prioridad_tarea_hh(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then
                        clsLnSis_prioridad_tarea_hh.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla sis_obs_log
            For i As Integer = 1 To 3
                If clsLnSis_obs_log.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeSis_obs_log(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then
                        clsLnSis_obs_log.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla sis_estado_tarea_hh
            For i As Integer = 1 To 4
                If clsLnSis_estado_tarea_hh.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeSis_estado_tarea_hh(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Descripcion) = False Then
                        clsLnSis_estado_tarea_hh.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            ' Validamos la tabla simbologias_codigo_barra
            For i As Integer = 1 To 9
                If clsLnSimbologias_codigo_barra.Exists(i, lConnection, lTransaction) = False Then
                    Dim Obj As New clsBeSimbologias_codigo_barra(i)
                    If Obj IsNot Nothing AndAlso String.IsNullOrEmpty(Obj.Nombre) = False Then
                        clsLnSimbologias_codigo_barra.Insertar(Obj, lConnection, lTransaction)
                    End If
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Class clsBeTrans_re_tr

        Public Property IdTipoTransaccion() As String
        Public Property Descripcion() As String
        Public Property Funcionalidad() As String
        Public Property UsaHH() As Boolean
        Public Property DescDev() As String
        Public Property TipoTrans() As String
        Public Property ConRef() As Boolean

        Public Sub New(ByVal pIndex As Integer)

            Select Case pIndex

                Case 1
                    IdTipoTransaccion = "HCOC00"
                    Descripcion = "Ingreso con Orden de Compra"
                    Funcionalidad = "Permite realizar el ingreso con HH de una O.C."
                    UsaHH = True
                    DescDev = "Se debe poder seleccionar una orden de compra a traves del link O.C. y se guarda la transacción y el detalle de la recepción es llenado por la O.C."
                    TipoTrans = "INGRESO"
                    ConRef = True
                Case 2
                    IdTipoTransaccion = "HCOD00"
                    Descripcion = "Devolución de Pedido"
                    Funcionalidad = "Permite realizar el ingreso de una devolución con HH a partir de un pedido"
                    UsaHH = True
                    DescDev = "Se debe cambiar el link label de Orden de Compra a Pedido, se debe selecionar un pedido y se pregunta si se quiere devolver el pedido completo, de ser así se debe llenar el detalle de la recepción con el detalle del pedido y el encabezado en las tablas de devolucion, como es una transacción para la handheld, el detalle de la devolución del pedido se llena con la handheld. (Se debe selecionar motivo de devolución de catálogo)"
                    TipoTrans = "DEVOLUCION"
                    ConRef = True
                Case 3
                    IdTipoTransaccion = "HHSR00"
                    Descripcion = "Ingreso sin referencia HH"
                    Funcionalidad = "Permite realizar un ingreso en la HH, sin detalle de lo que se va a recibir"
                    UsaHH = True
                    DescDev = "Permite realizar un ingreso en la HH, si detalle de lo que se va a recibir, no se debe digitar detalle en PC, solo genera tarea para la handheld"
                    TipoTrans = "INGRESO"
                    ConRef = False
                Case 4
                    IdTipoTransaccion = "HSOC00"
                    Descripcion = "Ingreso sin Orden de Compra"
                    Funcionalidad = "Permite realizar un ingreso con HH Ciego sin O.C."
                    UsaHH = 1
                    DescDev = "Se debe ocultar el link porque no se puede seleccionar orden de compra ni pedido, por ser para la handheld, se debe digitar el detalle de lo que la handheld va a recibir."
                    TipoTrans = "INGRESO"
                    ConRef = False
                Case 5
                    IdTipoTransaccion = "HSOD00"
                    Descripcion = "Ingreso de Devolución sin referencia"
                    Funcionalidad = "Permite realizar el ingreso con HH de una devolución sin referencia"
                    UsaHH = 1
                    DescDev = "Se debe ocultar el link porque no se puede seleccionar orden de compra ni pedido, la transacción es considerada un ingreso por devolución, por ser para la handheld, se debe digitar el detalle de lo que la handheld va a recibir en esa devolución, guardar el detalle y enc en tablas de devolución y generar tarea para que la handheld la reciba (Se debe selecionar motivo de devolución de catálogo) y guardar en tablas de devolución"
                    TipoTrans = "DEVOLUCION"
                    ConRef = False
                Case 6
                    IdTipoTransaccion = "MCOC00"
                    Descripcion = "Ingreso con Orden de Compra"
                    Funcionalidad = "Permite realizar el ingreso en PC de una O.C."
                    UsaHH = False
                    DescDev = "Permite realizar el ingreso en PC de una O.C."
                    TipoTrans = "INGRESO"
                    ConRef = True
                Case 7
                    IdTipoTransaccion = "MCOD00"
                    Descripcion = "Devolución de Pedido (PC)"
                    Funcionalidad = "Permite realizar el ingreso de una devolución en PC a partir de un pedido"
                    UsaHH = False
                    DescDev = "Se debe cambiar el link label de Orden de Compra a Pedido, se debe selecionar un pedido y se pregunta si se quiere devolver el pedido completo, de ser así se debe llenar el detalle de la recepción con el detalle del pedido y el encabezado en las tablas de devolucion, como es una transacción para la handheld, el detalle de la devolución del pedido se llena manualmente (Se debe selecionar motivo de devolución de catálogo)"
                    TipoTrans = "DEVOLUCION"
                    ConRef = True
                Case 8
                    IdTipoTransaccion = "MSOC00"
                    Descripcion = "Ingreso sin Orden de Compra (PC)"
                    Funcionalidad = "Permite realizar un ingreso en PC Ciego sin O.C."
                    UsaHH = False
                    DescDev = "Se debe ocultar el link porque no se puede seleccionar orden de compra ni pedido, la transacción es considerada un ingreso, por ser para la PC, se debe digitar el detalle de lo que se va a recibir, guardar el detalle y enc en tablas de recepción y generar reporte de ingreso (Se debe llenar existencias -> stock)"
                    TipoTrans = "INGRESO"
                    ConRef = False
                Case 9
                    IdTipoTransaccion = "MSOD00"
                    Descripcion = "Devolución sin referencia (PC)"
                    Funcionalidad = "Permite realizar el ingreso en PC de una devolución sin referencia"
                    UsaHH = False
                    DescDev = "Se debe ocultar el link porque no se puede seleccionar orden de compra ni pedido, la transacción es considerada un ingreso por devolución, por ser para la PC, se debe digitar el detalle de lo que se va a recibir en esa devolución, guardar el detalle y enc en tablas de devolución y generar reporte de ingreso por devolución (Se debe selecionar motivo de devolución de catálogo)"
                    TipoTrans = "DEVOLUCION"
                    ConRef = True

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnTrans_re_tr

        Public Shared Function Insertar(ByRef oBeTrans_re_tr As clsBeTrans_re_tr, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("trans_re_tr")
                Ins.Add("idtipotransaccion", "@idtipotransaccion", "F")
                Ins.Add("descripcion", "@descripcion", "F")
                Ins.Add("funcionalidad", "@funcionalidad", "F")
                Ins.Add("usahh", "@usahh", "F")
                Ins.Add("descdev", "@descdev", "F")
                Ins.Add("tipotrans", "@tipotrans", "F")
                Ins.Add("conref", "@conref", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeTrans_re_tr.Descripcion) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTrans_re_tr.IdTipoTransaccion))
                    cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_re_tr.Descripcion))
                    cmd.Parameters.Add(New SqlParameter("@FUNCIONALIDAD", oBeTrans_re_tr.Funcionalidad))
                    cmd.Parameters.Add(New SqlParameter("@USAHH", oBeTrans_re_tr.UsaHH))
                    cmd.Parameters.Add(New SqlParameter("@DESCDEV", oBeTrans_re_tr.DescDev))
                    cmd.Parameters.Add(New SqlParameter("@TIPOTRANS", oBeTrans_re_tr.TipoTrans))
                    cmd.Parameters.Add(New SqlParameter("@CONREF", oBeTrans_re_tr.ConRef))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeTrans_re_tr.IdTipoTransaccion = CStr(cmd.Parameters("@IDTIPOTRANSACCION").Value)

            Catch ex As Exception
                Throw New Exception("Trans_re_tr_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM trans_re_tr WHERE IdTipoTransaccion=@IdTipoTransaccion"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    Select Case pIndex
                        Case 1
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "HCOC00")
                        Case 2
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "HCOD00")
                        Case 3
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "HHSR00")
                        Case 4
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "HSOC00")
                        Case 5
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "HSOD00")
                        Case 6
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "MCOC00")
                        Case 7
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "MCOD00")
                        Case 8
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "MSOC00")
                        Case 9
                            lCommand.Parameters.AddWithValue("@IdTipoTransaccion", "MSOD00")

                        Case Else
                            Exit Select
                    End Select

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeTrans_pe_tipo

        Public Property IdTipoPedido() As Integer
        Public Property Nombre() As String
        Public Property Descripcion() As String
        Public Property Preparar() As Boolean
        Public Property Verificar() As Boolean
        Public Property ReservaStock() As Boolean
        Public Property ImprimeBarrasPicking() As Boolean
        Public Property ImprimeBarrasPacking() As Boolean

        Public Sub New(ByVal pIndex As Integer)

            IdTipoPedido = pIndex

            Select Case pIndex

                Case 1
                    Nombre = "PE0001"
                    Descripcion = "PEDIDO SIN PREPARACIÓN, SOLO NECESITA VERIFICACIÓN"
                    Preparar = False
                    Verificar = True
                    ReservaStock = True
                    ImprimeBarrasPicking = False
                    ImprimeBarrasPacking = False
                Case 2
                    Nombre = "PE0002"
                    Descripcion = "PEDIDO SIN PREPARACIÓN Y SIN VERIFICACIÓN"
                    Preparar = False
                    Verificar = False
                    ReservaStock = True
                    ImprimeBarrasPicking = False
                    ImprimeBarrasPacking = False
                Case 3
                    Nombre = "PE0003"
                    Descripcion = "PEDIDO CON PREPARACIÓN Y VERIFICACIÓN"
                    Preparar = True
                    Verificar = True
                    ReservaStock = True
                    ImprimeBarrasPicking = False
                    ImprimeBarrasPacking = False
                Case 4
                    Nombre = "PE0004"
                    Descripcion = "PEDIDO SIN RESERVA INMEDIATA, PREPARACIÓN Y VERIFICACIÓN"
                    Preparar = True
                    Verificar = True
                    ReservaStock = False
                    ImprimeBarrasPicking = False
                    ImprimeBarrasPacking = False

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnTrans_pe_tipo

        Public Shared Function Insertar(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("trans_pe_tipo")
                Ins.Add("idtipopedido", "@idtipopedido", "F")
                Ins.Add("nombre", "@nombre", "F")
                Ins.Add("descripcion", "@descripcion", "F")
                Ins.Add("preparar", "@preparar", "F")
                Ins.Add("verificar", "@verificar", "F")
                Ins.Add("reservastock", "@reservastock", "F")
                Ins.Add("imprimebarraspicking", "@imprimebarraspicking", "F")
                Ins.Add("imprimebarraspacking", "@imprimebarraspacking", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeTrans_pe_tipo.Nombre) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_tipo.IdTipoPedido))
                    cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_pe_tipo.Nombre))
                    cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_pe_tipo.Descripcion))
                    cmd.Parameters.Add(New SqlParameter("@PREPARAR", oBeTrans_pe_tipo.Preparar))
                    cmd.Parameters.Add(New SqlParameter("@VERIFICAR", oBeTrans_pe_tipo.Verificar))
                    cmd.Parameters.Add(New SqlParameter("@RESERVASTOCK", oBeTrans_pe_tipo.ReservaStock))
                    cmd.Parameters.Add(New SqlParameter("@IMPRIMEBARRASPICKING", oBeTrans_pe_tipo.ImprimeBarrasPicking))
                    cmd.Parameters.Add(New SqlParameter("@IMPRIMEBARRASPACKING", oBeTrans_pe_tipo.ImprimeBarrasPacking))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeTrans_pe_tipo.IdTipoPedido = CInt(cmd.Parameters("@IDTIPOPEDIDO").Value)

            Catch ex As Exception
                Throw New Exception("Trans_pe_tipo_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM trans_pe_tipo WHERE IdTipoPedido=@IdTipoPedido"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdTipoPedido", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeTrans_oc_ti
        Implements ICloneable

        Public Property IdTipoIngresoOC() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Es_devolucion() As Boolean = False
        Public Property User_agr() As String = ""
        Public Property Fec_agr() As Date = Date.Now
        Public Property User_mod() As String = ""
        Public Property Fec_mod() As Date = Date.Now
        Public Property Activo() As Boolean = False

        Sub New()
        End Sub

        Sub New(ByRef IdTipoIngresoOC As Integer, ByVal Nombre As String, ByVal es_devolucion As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
            Me.IdTipoIngresoOC = IdTipoIngresoOC
            Me.Nombre = Nombre
            Me.Es_devolucion = es_devolucion
            Me.User_agr = user_agr
            Me.Fec_agr = fec_agr
            Me.User_mod = user_mod
            Me.Fec_mod = fec_mod
            Me.Activo = activo
        End Sub

        Public Function Clone() As Object Implements ICloneable.Clone
            Return MyBase.MemberwiseClone()
        End Function

    End Class

    Public Class clsLnTrans_oc_ti

        Public Shared Sub Cargar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, ByRef dr As DataRow)
            Try
                With oBeTrans_oc_ti
                    .IdTipoIngresoOC = IIf(IsDBNull(dr.Item("IdTipoIngresoOC")), 0, dr.Item("IdTipoIngresoOC"))
                    .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                    .Es_devolucion = IIf(IsDBNull(dr.Item("es_devolucion")), False, dr.Item("es_devolucion"))
                    .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                    .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                    .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                    .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                    .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                End With
            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_Cargar: " & ex.Message)
            End Try
        End Sub

        Public Function Insertar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

            Try

                Ins.Init("trans_oc_ti")
                Ins.Add("idtipoingresooc", "@idtipoingresooc", "F")
                Ins.Add("nombre", "@nombre", "F")
                Ins.Add("es_devolucion", "@es_devolucion", "F")
                Ins.Add("user_agr", "@user_agr", "F")
                Ins.Add("fec_agr", "@fec_agr", "F")
                Ins.Add("user_mod", "@user_mod", "F")
                Ins.Add("fec_mod", "@fec_mod", "F")
                Ins.Add("activo", "@activo", "F")

                Dim sp As String = Ins.SQL()
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else
                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))
                cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_ti.Nombre))
                cmd.Parameters.Add(New SqlParameter("@ES_DEVOLUCION", oBeTrans_oc_ti.Es_devolucion))
                cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_ti.User_agr))
                cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_ti.Fec_agr))
                cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_ti.User_mod))
                cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_ti.Fec_mod))
                cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_ti.Activo))

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                cmd.Dispose()

                Return rowsAffected

                oBeTrans_oc_ti.IdTipoIngresoOC = CInt(cmd.Parameters("@IDTIPOINGRESOOC").Value)

            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
            End Try

        End Function

        Public Function Actualizar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

            Try

                Upd.Init("trans_oc_ti")
                Upd.Add("idtipoingresooc", "@idtipoingresooc", "F")
                Upd.Add("nombre", "@nombre", "F")
                Upd.Add("es_devolucion", "@es_devolucion", "F")
                Upd.Add("user_agr", "@user_agr", "F")
                Upd.Add("fec_agr", "@fec_agr", "F")
                Upd.Add("user_mod", "@user_mod", "F")
                Upd.Add("fec_mod", "@fec_mod", "F")
                Upd.Add("activo", "@activo", "F")
                Upd.Where("IdTipoIngresoOC = @IdTipoIngresoOC")

                Dim sp As String = Upd.SQL()

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else
                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))
                cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_ti.Nombre))
                cmd.Parameters.Add(New SqlParameter("@ES_DEVOLUCION", oBeTrans_oc_ti.Es_devolucion))
                cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_ti.User_agr))
                cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_ti.Fec_agr))
                cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_ti.User_mod))
                cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_ti.Fec_mod))
                cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_ti.Activo))

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                cmd.Dispose()

                Return rowsAffected

            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_Actualizar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
            End Try

        End Function

        Public Function Eliminar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

            Try

                Const sp As String = " Delete from Trans_oc_ti" &
             "  Where(IdTipoIngresoOC = @IdTipoIngresoOC)"

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

                If EsTransaccional Then

                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()

                End If

                cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                cmd.Dispose()

                Return rowsAffected

            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_Eliminar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
            End Try
        End Function

        Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)

            Try

                Const sp As String = " Delete from Trans_oc_ti"

                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                Else
                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()

                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                cmd.Dispose()

                Return rowsAffected

            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_Eliminar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
            End Try

        End Function

        Public Function Listar() As DataTable

            Try

                Const sp As String = "SELECT * FROM Trans_oc_ti"

                Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

                Dim dad As New SqlDataAdapter(cmd)

                Dim dt As New DataTable
                dad.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_Listar: " & ex.Message)
            End Try

        End Function

        Public Function Obtener(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti) As Boolean

            Try

                Const sp As String = "SELECT * FROM Trans_oc_ti" &
            " Where(IdTipoIngresoOC = @IdTipoIngresoOC)"

                Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

                Dim dad As New SqlDataAdapter(cmd)

                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Cargar(oBeTrans_oc_ti, dt.Rows(0))
                Else
                    Throw New Exception("No se pudo obtener el registro")
                End If

                Return True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function GetAll() As List(Of clsBeTrans_oc_ti)

            Try

                Dim lReturnList As New List(Of clsBeTrans_oc_ti)
                Const sp As String = "SELECT * FROM Trans_oc_ti"
                Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                Dim dt As New DataTable

                dad.Fill(dt)

                Dim vBeTrans_oc_ti As New clsBeTrans_oc_ti

                For Each dr As DataRow In dt.Rows

                    vBeTrans_oc_ti = New clsBeTrans_oc_ti
                    Cargar(vBeTrans_oc_ti, dr)
                    lReturnList.Add(vBeTrans_oc_ti)

                Next

                cnn.Dispose()
                cmd.Dispose()

                Return lReturnList

            Catch ex As Exception
                Throw New Exception("Trans_oc_ti_GetAll: " & ex.Message)
            End Try

        End Function

        Public Function GetSingle(ByRef pBeTrans_oc_ti As clsBeTrans_oc_ti)

            Try

                Const sp As String = "SELECT * FROM Trans_oc_ti" &
            " Where(IdTipoIngresoOC = @IdTipoIngresoOC)"

                Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
                Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

                Dim dad As New SqlDataAdapter(cmd)

                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", pBeTrans_oc_ti.IdTipoIngresoOC))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    clsLnTrans_oc_ti.Cargar(pBeTrans_oc_ti, dt.Rows(0))
                End If

                Return True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

    End Class

    Public Class clsBeTrans_oc_estado

        Public Property IdEstadoOC() As Integer
        Public Property Nombre() As String

        Sub New(ByVal pIndex As Integer)

            IdEstadoOC = pIndex

            Select Case pIndex
                Case 1
                    Nombre = "NUEVA"
                Case 2
                    Nombre = "ASIGNADA"
                Case 3
                    Nombre = "EN PROCESO"
                Case 4
                    Nombre = "CERRADA"
                Case 5
                    Nombre = "ANULADA"
                Case 6
                    Nombre = "BACK ORDER"

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnTrans_oc_estado

        Public Shared Function Insertar(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("trans_oc_estado")
                Ins.Add("idestadooc", "@idestadooc", "F")
                Ins.Add("nombre", "@nombre", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeTrans_oc_estado.Nombre) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC))
                    cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_estado.Nombre))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeTrans_oc_estado.IdEstadoOC = CInt(cmd.Parameters("@IDESTADOOC").Value)

            Catch ex As Exception
                Throw New Exception("Trans_oc_estado_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM trans_oc_estado WHERE IdEstadoOC=@IdEstadoOC"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConection)

                    lCommand.Transaction = pTransaction
                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdEstadoOC", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeTipo_rotacion

        Public Property IdTipoRotacion() As Integer
        Public Property Descripcion() As String
        Public Property Activo() As Boolean

        Sub New(ByVal pIndex As Integer)

            IdTipoRotacion = pIndex

            Select Case pIndex
                Case 1
                    Descripcion = "FIFO"
                    Activo = True
                Case 2
                    Descripcion = "LIFO"
                    Activo = True
                Case 3
                    Descripcion = "FEFO"
                    Activo = True

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnTipo_rotacion

        Public Shared Function Insertar(ByRef oBeTipo_rotacion As clsBeTipo_rotacion, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("tipo_rotacion")
                Ins.Add("idtiporotacion", "@idtiporotacion", "F")
                Ins.Add("descripcion", "@descripcion", "F")
                Ins.Add("activo", "@activo", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else
                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeTipo_rotacion.Descripcion) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeTipo_rotacion.IdTipoRotacion))
                    cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTipo_rotacion.Descripcion))
                    cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_rotacion.Activo))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeTipo_rotacion.IdTipoRotacion = CInt(cmd.Parameters("@IDTIPOROTACION").Value)

            Catch ex As Exception
                Throw New Exception("Tipo_rotacion_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM tipo_rotacion WHERE IdTipoRotacion=@IdTipoRotacion"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdTipoRotacion", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeTipo_actualizacion_costo

        Public Property IdTipoActualizacionCosto() As Integer
        Public Property NombreActualizacionCosto() As String

        Sub New(ByVal pIndex As Integer)

            IdTipoActualizacionCosto = pIndex

            Select Case pIndex

                Case 1
                    NombreActualizacionCosto = "si nuevo costo es mayor (>)"
                Case 2
                    NombreActualizacionCosto = "si nuevo costo es menor (<)"
                Case 3
                    NombreActualizacionCosto = "si nuevo costo diferente (<>)"

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnTipo_actualizacion_costo

        Public Shared Function Insertar(ByRef oBeTipo_actualizacion_costo As clsBeTipo_actualizacion_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("tipo_actualizacion_costo")
                Ins.Add("idtipoactualizacioncosto", "@idtipoactualizacioncosto", "F")
                Ins.Add("nombreactualizacioncosto", "@nombreactualizacioncosto", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeTipo_actualizacion_costo.NombreActualizacionCosto) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDTIPOACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.IdTipoActualizacionCosto))
                    cmd.Parameters.Add(New SqlParameter("@NOMBREACTUALIZACIONCOSTO", oBeTipo_actualizacion_costo.NombreActualizacionCosto))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeTipo_actualizacion_costo.IdTipoActualizacionCosto = CInt(cmd.Parameters("@IDTIPOACTUALIZACIONCOSTO").Value)

            Catch ex As Exception
                Throw New Exception("Tipo_actualizacion_costo_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM tipo_actualizacion_costo WHERE IdTipoActualizacionCosto=@IdTipoActualizacionCosto"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdTipoActualizacionCosto", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeSis_tipo_tarea

        Public Property IdTipoTarea() As Integer
        Public Property Nombre() As String
        Public Property Contabilizar() As Boolean

        Sub New(ByVal pIndex As Integer)

            IdTipoTarea = pIndex

            Select Case pIndex

                Case 1
                    Nombre = "REC"
                    Contabilizar = True
                Case 2
                    Nombre = "UBI"
                    Contabilizar = False
                Case 3
                    Nombre = "CES"
                    Contabilizar = False
                Case 4
                    Nombre = "TRS"
                    Contabilizar = True
                Case 5
                    Nombre = "DES"
                    Contabilizar = True
                Case 6
                    Nombre = "INV"
                    Contabilizar = True
                Case 7
                    Nombre = "AJU"
                    Contabilizar = True
                Case 8
                    Nombre = "PIC"
                    Contabilizar = True

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnSis_tipo_tarea

        Public Shared Function Insertar(ByRef oBeSis_tipo_tarea As clsBeSis_tipo_tarea, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("sis_tipo_tarea")
                Ins.Add("idtipotarea", "@idtipotarea", "F")
                Ins.Add("nombre", "@nombre", "F")
                Ins.Add("contabilizar", "@contabilizar", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeSis_tipo_tarea.Nombre) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeSis_tipo_tarea.IdTipoTarea))
                    cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeSis_tipo_tarea.Nombre))
                    cmd.Parameters.Add(New SqlParameter("@CONTABILIZAR", oBeSis_tipo_tarea.Contabilizar))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeSis_tipo_tarea.IdTipoTarea = CInt(cmd.Parameters("@IDTIPOTAREA").Value)

            Catch ex As Exception
                Throw New Exception("Sis_tipo_tarea_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM sis_tipo_tarea WHERE IdTipoTarea=@IdTipoTarea"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConnection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdTipoTarea", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeSis_tipo_accion

        Public Property IdTipoAccion() As Integer
        Public Property Nombre() As String

        Sub New(ByVal pIndex As Integer)

            IdTipoAccion = pIndex

            Select Case pIndex
                Case 1
                    Nombre = "Notificar"
                Case 2
                    Nombre = "Generar Automaticamente"

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnSis_tipo_accion

        Public Shared Function Insertar(ByRef oBeSis_tipo_accion As clsBeSis_tipo_accion, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("sis_tipo_accion")
                Ins.Add("idtipoaccion", "@idtipoaccion", "F")
                Ins.Add("nombre", "@nombre", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else
                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeSis_tipo_accion.Nombre) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDTIPOACCION", oBeSis_tipo_accion.IdTipoAccion))
                    cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeSis_tipo_accion.Nombre))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeSis_tipo_accion.IdTipoAccion = CInt(cmd.Parameters("@IDTIPOACCION").Value)

            Catch ex As Exception
                Throw New Exception("Sis_tipo_accion_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM sis_tipo_accion WHERE IdTipoAccion=@IdTipoAccion"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConnection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdTipoAccion", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeSis_prioridad_tarea_hh

        Public Property IdPrioridad() As Integer
        Public Property Descripcion() As String

        Sub New(ByVal pIndex As Integer)

            IdPrioridad = pIndex

            Select Case pIndex

                Case 1
                    Descripcion = "Bajo"
                Case 2
                    Descripcion = "Medio"
                Case 3
                    Descripcion = "Alto"
                Case 4
                    Descripcion = "Más Atendido"

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnSis_prioridad_tarea_hh

        Public Shared Function Insertar(ByRef oBeSis_prioridad_tarea_hh As clsBeSis_prioridad_tarea_hh, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("sis_prioridad_tarea_hh")
                Ins.Add("idprioridad", "@idprioridad", "F")
                Ins.Add("descripcion", "@descripcion", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeSis_prioridad_tarea_hh.Descripcion) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDPRIORIDAD", oBeSis_prioridad_tarea_hh.IdPrioridad))
                    cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeSis_prioridad_tarea_hh.Descripcion))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeSis_prioridad_tarea_hh.IdPrioridad = CInt(cmd.Parameters("@IDPRIORIDAD").Value)

            Catch ex As Exception
                Throw New Exception("Sis_prioridad_tarea_hh_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM sis_prioridad_tarea_hh WHERE IdPrioridad=@IdPrioridad"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConnection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdPrioridad", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeSis_obs_log

        Public Property IdObservacion() As Integer
        Public Property Descripcion() As String

        Sub New(ByVal pIndex As Integer)

            IdObservacion = pIndex

            Select Case pIndex

                Case 1
                    Descripcion = "Abastecimiento Completado"
                Case 2
                    Descripcion = "No se pudo abastecer debido a que el stock era insuficiente"
                Case 3
                    Descripcion = "Se pospusieron todos los productos de la lista"

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnSis_obs_log

        Public Shared Function Insertar(ByRef oBeSis_obs_log As clsBeSis_obs_log, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("sis_obs_log")
                Ins.Add("idobservacion", "@idobservacion", "F")
                Ins.Add("descripcion", "@descripcion", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeSis_obs_log.Descripcion) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDOBSERVACION", oBeSis_obs_log.IdObservacion))
                    cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeSis_obs_log.Descripcion))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeSis_obs_log.IdObservacion = CInt(cmd.Parameters("@IDOBSERVACION").Value)

            Catch ex As Exception
                Throw New Exception("Sis_obs_log_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM sis_obs_log WHERE IdObservacion=@IdObservacion"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConnection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdObservacion", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeSis_estado_tarea_hh

        Public Property IdEstado() As Integer
        Public Property Descripcion() As String

        Sub New(ByVal pIndex As Integer)

            IdEstado = pIndex

            Select Case pIndex
                Case 1
                    Descripcion = "Nuevo"
                Case 2
                    Descripcion = "Pendiente"
                Case 3
                    Descripcion = "Anulado"
                Case 4
                    Descripcion = "Finalizado"

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnSis_estado_tarea_hh

        Public Shared Function Insertar(ByRef oBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("sis_estado_tarea_hh")
                Ins.Add("idestado", "@idestado", "F")
                Ins.Add("descripcion", "@descripcion", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeSis_estado_tarea_hh.Descripcion) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeSis_estado_tarea_hh.IdEstado))
                    cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeSis_estado_tarea_hh.Descripcion))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeSis_estado_tarea_hh.IdEstado = CInt(cmd.Parameters("@IDESTADO").Value)

            Catch ex As Exception
                Throw New Exception("Sis_estado_tarea_hh_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM sis_estado_tarea_hh WHERE IdEstado=@IdEstado"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConnection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdEstado", pIndex)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

    Public Class clsBeSimbologias_codigo_barra

        Public Property IdSimbologia() As Integer
        Public Property Nombre() As String
        Public Property Activo() As Boolean

        Sub New(ByVal pIndex As Integer)

            Select Case pIndex
                Case 1
                    IdSimbologia = 1
                    Nombre = "Codabar"
                    Activo = True
                Case 2
                    IdSimbologia = 3
                    Nombre = "Code39"
                    Activo = True
                Case 3
                    IdSimbologia = 5
                    Nombre = "Code93"
                    Activo = True
                Case 4
                    IdSimbologia = 7
                    Nombre = "Code128"
                    Activo = True
                Case 5
                    IdSimbologia = 12
                    Nombre = "UPCA"
                    Activo = True
                Case 6
                    IdSimbologia = 13
                    Nombre = "EAN8"
                    Activo = True
                Case 7
                    IdSimbologia = 14
                    Nombre = "EAN128"
                    Activo = True
                Case 8
                    IdSimbologia = 20
                    Nombre = "PDF417"
                    Activo = True
                Case 9
                    IdSimbologia = 22
                    Nombre = "QRCode"
                    Activo = True

                Case Else
                    Exit Select
            End Select

        End Sub

    End Class

    Public Class clsLnSimbologias_codigo_barra

        Public Shared Function Insertar(ByRef oBeSimbologias_codigo_barra As clsBeSimbologias_codigo_barra, Optional ByVal pConnection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

            Dim cnn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Dim cmd As New SqlCommand

            Try

                Ins.Init("simbologias_codigo_barra")
                Ins.Add("idsimbologia", "@idsimbologia", "F")
                Ins.Add("nombre", "@nombre", "F")
                Ins.Add("activo", "@activo", "F")

                Dim sp As String = Ins.SQL()

                Dim EsTransaccional As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

                cmd.CommandType = CommandType.Text

                If EsTransaccional Then
                    cmd = New SqlCommand(sp, pConnection)
                    cmd.Transaction = pTransaction
                Else

                    cmd = New SqlCommand(sp, cnn)
                    cnn.Open()
                End If

                If String.IsNullOrEmpty(oBeSimbologias_codigo_barra.Nombre) = False Then
                    cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIA", oBeSimbologias_codigo_barra.IdSimbologia))
                    cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeSimbologias_codigo_barra.Nombre))
                    cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeSimbologias_codigo_barra.Activo))
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected

                oBeSimbologias_codigo_barra.IdSimbologia = CInt(cmd.Parameters("@IDSIMBOLOGIA").Value)

            Catch ex As Exception
                Throw New Exception("Simbologias_codigo_barra_Insertar: " & ex.Message)
            Finally
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cmd.Dispose()
            End Try

        End Function

        Public Shared Function Exists(ByVal pIndex As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

            Try

                Dim lExists As Boolean = False
                'Validacion y estandarizacion de los datos

                '#HS 08112017 Quité query dentro de SqlCommand.
                vSQL = "SELECT COUNT(1) FROM simbologias_codigo_barra WHERE IdSimbologia=@IdSimbologia"

                'Acceso a los datos.
                Using lCommand As New SqlCommand(vSQL, pConnection)

                    lCommand.Transaction = pTransaction

                    lCommand.CommandType = CommandType.Text

                    Select Case pIndex

                        Case 1
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 1)
                        Case 2
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 3)
                        Case 3
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 5)
                        Case 4
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 7)
                        Case 5
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 12)
                        Case 6
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 13)
                        Case 7
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 14)
                        Case 8
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 20)
                        Case 9
                            lCommand.Parameters.AddWithValue("@IdSimbologia", 22)

                        Case Else

                            Exit Select

                    End Select

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

                Return lExists

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class

End Class

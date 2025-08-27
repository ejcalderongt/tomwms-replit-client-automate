Imports System.IO

Public Class IMS

    Public Property UsuarioAp As New clsBeUsuario

    Public ReadOnly Property Alto_Pantalla() As Integer
        Get
            Return My.Computer.Screen.Bounds.Height
        End Get
    End Property

    Public ReadOnly Property Ancho_Pantalla() As Integer
        Get
            Return My.Computer.Screen.Bounds.Width
        End Get
    End Property
    Public Overridable Property IdEmpresa() As Integer = 0
    Public Property idbodega() As String
    Public Property NomBodega() As String
    Public Property NomEmpresa() As String
    Public Property No_Tareas() As Integer

    Public vSQL As String = ""

    Public Function Existe_Ini() As Boolean
        Existe_Ini = False
        If File.Exists(BD.AppPath & "Conn.ini") Then Existe_Ini = True
    End Function

    Public Sub WaitCur()
        Cursor.Current = Cursors.WaitCursor
    End Sub

    Public Sub DefCur()
        Cursor.Current = Cursors.Default
    End Sub

    Public Sub EsNumerico(ByRef Texto As Object)
        If Texto.Text <> "" And Not IsNumeric(Texto.Text) Then
            Texto.Text = ""
            MessageBox.Show("Ingrese un dato numérico!", " ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Texto.Focus()
        End If
    End Sub

    Public Overloads Function Listar_Empresas(ByRef Cmb As ComboBox) As Boolean

        Listar_Empresas = False

        Dim DT As New DataTable

        Try

            vSQL = "select idempresa, nombre from empresa where activo=1"
            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idempresa"
                Cmb.DataSource = DT
            Else
                'Throw New Exception("No hay definidas empresas")
            End If

            Listar_Empresas = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Function Listar_EmpresaTransportePorEmpresa(ByRef Cmb As ComboBox, ByVal pIdEmpresa As Integer) As Boolean

        Listar_EmpresaTransportePorEmpresa = False

        Dim DT As New DataTable

        Try

            vSQL = "SELECT IdEmpresaTransporte, nombre FROM empresa_transporte WHERE activo=1 AND IdEmpresa=" & pIdEmpresa
            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdEmpresaTransporte"
                Cmb.DataSource = DT
            Else
                'Throw New Exception("No hay definidas empresas")
            End If

            Listar_EmpresaTransportePorEmpresa = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Function Listar_Propietario(ByRef Cmb As ComboBox) As Boolean

        Listar_Propietario = False

        Dim DT As New DataTable

        Try

            vSQL = "SELECT idPropietario, nombre_comercial FROM propietarios WHERE activo=1"
            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre_comercial"
                Cmb.ValueMember = "IdPropietario"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay Propietarios Definidos")
            End If

            Listar_Propietario = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Function Listar_UnidadMedida(ByRef Cmb As ComboBox) As Boolean

        Listar_UnidadMedida = False

        Dim DT As New DataTable

        Try

            vSQL = "SELECT IdUnidadMedida, nombre FROM unidad_medida WHERE activo=1"
            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdUnidadMedida"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay Productos Definidos")
            End If

            Listar_UnidadMedida = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Function Listar_Producto(ByRef Cmb As ComboBox) As Boolean

        Listar_Producto = False

        Dim DT As New DataTable

        Try

            vSQL = "SELECT IdProducto, nombre FROM producto WHERE activo=1"
            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdProducto"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay Productos Definidos")
            End If

            Listar_Producto = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Overloads Function Listar_Proveedor(ByRef Cmb As ComboBox) As Boolean

        Listar_Proveedor = False

        Dim DT As New DataTable

        Try

            vSQL = "SELECT IdProveedor, nombre FROM proveedor WHERE activo=1"
            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdProveedor"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay Proveedores Definidos")
            End If

            Listar_Proveedor = DT.Rows.Count > 0

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Listar_Bodegas(ByRef Cmb As ComboBox) As Boolean

        Listar_Bodegas = False

        Dim DT As New DataTable

        Try

            DT = clsLnBodega.Listar(IdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idbodega"
                Cmb.DataSource = DT
            End If

            Listar_Bodegas = DT.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Listar_BodegasPorEmpresa(ByRef Cmb As ComboBox, empresa As Integer) As Boolean

        Listar_BodegasPorEmpresa = False

        Dim DT As New DataTable

        Try

            vSQL$ = "select idbodega, nombre from bodega where idempresa = " & empresa &
             " and activo=1"

            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idbodega"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay definidas bodegas para la empresa: " & IdEmpresa)
            End If

            Listar_BodegasPorEmpresa = DT.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Listar_JornadasPorBodega(ByRef Cmb As ComboBox, ByVal pIdBodega As Integer) As Boolean

        Listar_JornadasPorBodega = False

        Dim DT As New DataTable

        Try
            '#HS 20171026 1509 Quité String.Format.
            DT = clsLnJornada_laboral.Listar(pIdBodega)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "Nombre_Jornada"
                Cmb.ValueMember = "IdJornada"
                Cmb.DataSource = DT
            Else
                Throw New Exception("No hay definidas bodegas para la bodega")
            End If

            Listar_JornadasPorBodega = DT.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Listar_Turnos(ByRef Cmb As ComboBox) As Boolean

        Listar_Turnos = False

        Dim DT As New DataTable

        Try

            vSQL$ = "SELECT IdTurno, nombre FROM turno WHERE Activo=1"

            BD.OpenDT(DT, vSQL$)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "IdTurno"
                Cmb.DataSource = DT
            End If

            Listar_Turnos = DT.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function Consulta_Tareas() As String

        Consulta_Tareas = ""

        Dim vCadena$ = String.Format(" and a.idempresa={0} and a.idbodega='{1}'", IdEmpresa, idbodega)

        Try

            vSQL$ = "select a.correlativo, 'Recepción' as tarea, a.fecha, b.nombres + ' ' + b.apellidos as operador, estado, convert(nvarchar(25),'') as documento " &
            " from recepcion_enc a, operador b  " &
            " where a.operadorid = b.operadorid and a.idempresa=b.idempresa and a.idbodega=b.idbodega " &
            " and a.estado in ('nuevo','pendiente') " & vCadena$ &
            " union " &
            " select a.correlativo, 'Picking' as tarea, a.fecha, b.nombres + ' ' + b.apellidos as operador, estado, convert(nvarchar(25),a.correlativo) as documento " &
            " from picking_enc a, operador b  " &
            " where a.operadorid = b.operadorid and a.idempresa=b.idempresa and a.idbodega=b.idbodega " &
            " and a.estado in ('nuevo','pendiente') " & vCadena$ &
            " union " &
            " select a.correlativo , 'Pedido Cliente' as tarea, a.fecha, b.nombres + ' ' + b.apellidos as operador, estado, convert(nvarchar(25),a.no_documento) as documento " &
            " from pedido_enc a, operador b  " &
            " where a.operadorid = b.operadorid and a.idempresa=b.idempresa and a.idbodega=b.idbodega " &
            " and a.estado in ('pendiente') " & vCadena$ &
            " union " &
            " select a.correlativo, 'Verificación' as tarea, a.fecha, b.nombres + ' ' + b.apellidos as operador, estado, '0' as documento " &
            " from pedido_enc a, operador b  " &
            " where a.operadorid = b.operadorid and a.idempresa=b.idempresa and a.idbodega=b.idbodega " &
            " and estado in ('activado') " & vCadena$ &
            " union " &
            " select a.correlativo, 'Toma Inventario' as tarea, a.fecha, b.nombres + ' ' + b.apellidos as operador, estado, '0' as documento " &
            " from toma_inv_enc a, operador b  " &
            " where a.operadorid = b.operadorid and a.idempresa=b.idempresa and a.idbodega=b.idbodega " &
            " and a.estado in ('nuevo','pendiente') " & vCadena$ &
            " order by a.estado "

            Consulta_Tareas = vSQL

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Sub Cerrar_Ventana(ByVal frm As Form)

        Try

            If MessageBox.Show("¿Salir de " & frm.Text & "?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                frm.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub Centrar(ByVal Objeto As Object)

        If TypeOf Objeto Is Form Then
            Dim frm As Form = CType(Objeto, Form)

            With Screen.PrimaryScreen.WorkingArea
                frm.Top = (.Height - frm.Height) \ 2
                frm.Left = (.Width - frm.Width) \ 2
            End With
        Else
            Dim c As Control = CType(Objeto, Control)
            With c
                .Top = (.Parent.ClientSize.Height - c.Height) \ 2
                .Left = (.Parent.ClientSize.Width - c.Width) \ 2
            End With
        End If
    End Sub

    Public Sub LLena_Unid_Med(ByRef pCombo As ComboBox)

        Dim DT As New DataTable

        Try

            vSQL$ = "SELECT Nombre, unidadid as Codigo FROM unidad_medida " &
            " where idempresa = " & AP.IdEmpresa &
            " and idbodega='" & AP.idbodega & "'"

            BD.OpenDT(DT, vSQL$)

            pCombo.ValueMember = "Codigo"
            pCombo.DisplayMember = "Nombre"
            pCombo.DataSource = DT
            pCombo.SelectedIndex = -1
            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub LLena_Tipologia(ByRef pCombo As ComboBox, ByVal pTipo$)

        Dim DT As New DataTable

        Try

            vSQL$ = "SELECT descripcion as Nombre, tipoid as Codigo FROM tipologia " &
            " WHERE padre IN (SELECT tipoid FROM tipologia " &
            " WHERE descripcion = '" & pTipo$ & "')" &
            " ORDER BY descripcion ASC"

            BD.OpenDT(DT, vSQL$)

            pCombo.ValueMember = "Codigo"
            pCombo.DisplayMember = "Nombre"
            pCombo.DataSource = DT
            pCombo.SelectedIndex = -1
            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub Llena_Tipologia_Padre(ByRef pPadre As ComboBox)

        Dim DT As New DataTable

        Try

            vSQL$ = "select descripcion,tipoid from tipologia " &
            " where idempresa=" & AP.IdEmpresa &
            " and idbodega='" & AP.idbodega & "'" &
            " and sistema=1 "
            BD.OpenDT(DT, vSQL$)

            pPadre.DisplayMember = "descripcion"
            pPadre.ValueMember = "tipoid"
            pPadre.DataSource = DT

            DT.Dispose()

            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Function Imagen_A_Bytes(ByVal img As Image) As Byte()
        Dim sTemp As String = Path.GetTempFileName()
        Dim fs As New FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite)
        img.Save(fs, System.Drawing.Imaging.ImageFormat.Png)
        fs.Position = 0
        Dim imgLength As Integer = fs.Length
        Dim bytes(0 To imgLength - 1) As Byte
        fs.Read(bytes, 0, imgLength)
        fs.Close()
        Return bytes
    End Function

    Public Function Bytes_A_Imagen(ByVal bytes() As Byte) As Image
        If bytes Is Nothing Then Return Nothing
        Dim ms As New MemoryStream(bytes)
        Dim bm As Bitmap = Nothing
        Try
            bm = New Bitmap(ms)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
        Return bm
    End Function

    Public Function Confirma_Transaccion() As Boolean

        Confirma_Transaccion = False

        If MessageBox.Show("¿Esta seguro de guardar el registro?", " Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Confirma_Transaccion = True
        End If

    End Function

    Public Function Listar_BodegasLogin(ByRef Cmb As ComboBox) As Boolean

        Listar_BodegasLogin = False

        Dim DT As New DataTable

        Try

            DT = clsLnBodega.Listar(IdEmpresa)

            If DT.Rows.Count > 0 Then
                Cmb.DisplayMember = "nombre"
                Cmb.ValueMember = "idbodega"
                Cmb.DataSource = DT
            End If

            Listar_BodegasLogin = DT.Rows.Count > 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

End Class

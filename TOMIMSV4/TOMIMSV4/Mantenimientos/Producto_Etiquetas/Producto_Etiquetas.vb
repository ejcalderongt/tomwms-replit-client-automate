Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid

Public Class Producto_Etiquetas

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Private ListProductos As New List(Of clsBeProducto_Barras_Seleccion)

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private IdPropietario As Integer = 0
    Private IdFamilia As Integer = 0
    Private IdMarca As Integer = 0
    Private IdClasificacion As Integer = 0

    Public Obj As clsBeTipo_etiqueta
    Public BeBodega As New clsBeBodega

    Private Sub Cargar_Productos()

        Try

            grdPrds.DataSource = ListProductos
            GridView1.LayoutChanged()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridView1.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritemProducto_CheckedChanged

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ritemProducto_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As clsBeProducto_Barras_Seleccion = GridView1.GetFocusedRow

                If ritem.Checked Then
                    ListProductos.Where(Function(x) x.Codigo = Dr.Codigo).FirstOrDefault.Seleccionar = True
                Else
                    ListProductos.Where(Function(x) x.Codigo = Dr.Codigo).FirstOrDefault.Seleccionar = False
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Producto_Etiquetas_Load(sender As Object, e As EventArgs) Handles Me.Load

        GridView1.FocusRectStyle = Views.Grid.DrawFocusRectStyle.RowFullFocus
        Dim ritem As RepositoryItemCheckEdit = TryCast(GridView1.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
        AddHandler ritem.CheckedChanged, AddressOf ritemProducto_CheckedChanged

        Try

            Llena_lista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnklblProp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblProp.LinkClicked

        Try

            Dim Rec As New frmPropietario_List() With {
                   .Modo = frmPropietario_List.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            If Rec.pObjPropietario IsNot Nothing AndAlso Rec.pObjPropietario.IdPropietario <> 0 Then
                txtIdPropietario.Text = Rec.pObjPropietario.IdPropietario
                txtNombrePropietario.Text = Rec.pObjPropietario.Nombre_comercial
                IdPropietario = Rec.pObjPropietario.IdPropietario
            End If

            Llena_lista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnklblClas_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblClas.LinkClicked

        Try

            Dim Rec As New frmProducto_ClasificacionList() With {
                   .Modo = frmProducto_ClasificacionList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            If Rec.pObjPC IsNot Nothing AndAlso Rec.pObjPC.IdClasificacion <> 0 Then
                txtIdClasificacion.Text = Rec.pObjPC.IdClasificacion
                txtNombreClas.Text = Rec.pObjPC.Nombre
                IdClasificacion = Rec.pObjPC.IdClasificacion
            End If

            Llena_lista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnklblFam_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblFam.LinkClicked

        Try

            Dim Rec As New frmProducto_FamiliaList() With {
                   .Modo = frmProducto_FamiliaList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            If Rec.pObjPF IsNot Nothing AndAlso Rec.pObjPF.IdFamilia <> 0 Then
                txtIdFamilia.Text = Rec.pObjPF.IdFamilia
                txtNombreFam.Text = Rec.pObjPF.Nombre
                IdFamilia = Rec.pObjPF.IdFamilia
            End If

            Llena_lista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnklblMarca_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblMarca.LinkClicked

        Try

            Dim Rec As New frmProducto_MarcaList() With {
                   .Modo = frmProducto_MarcaList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            If Rec.pObjM IsNot Nothing AndAlso Rec.pObjM.IdMarca <> 0 Then
                txtIdMarca.Text = Rec.pObjM.IdMarca
                txtNombreMarca.Text = Rec.pObjM.Nombre
                IdMarca = Rec.pObjM.IdMarca
            End If

            Llena_lista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdClasificacion_Validated(sender As Object, e As EventArgs) Handles txtIdClasificacion.Validated

        Try

            If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False AndAlso txtIdClasificacion.Text > "0" Then

                Dim pBeClasificacion As New clsBeProducto_clasificacion

                pBeClasificacion = clsLnProducto_clasificacion.GetSingle(txtIdClasificacion.Text)

                If pBeClasificacion IsNot Nothing AndAlso pBeClasificacion.IdClasificacion > 0 Then
                    txtNombreClas.Text = Trim(String.Format("{0}", pBeClasificacion.Nombre))
                    IdClasificacion = pBeClasificacion.IdClasificacion
                    Cargar_Productos()
                Else
                    XtraMessageBox.Show(String.Format("No existe clasificación con código {0}", txtIdClasificacion.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdClasificacion.Focus()
                    txtIdClasificacion.SelectAll()
                    pBeClasificacion = Nothing
                    IdClasificacion = 0
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdFamilia_Validated(sender As Object, e As EventArgs) Handles txtIdFamilia.Validated

        Try

            If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False AndAlso txtIdFamilia.Text > "0" Then

                Dim pBeFamilia As New clsBeProducto_familia

                pBeFamilia = clsLnProducto_familia.GetSingle(txtIdFamilia.Text)

                If pBeFamilia IsNot Nothing AndAlso pBeFamilia.IdFamilia > 0 Then
                    txtNombreFam.Text = Trim(String.Format("{0}", pBeFamilia.Nombre))
                    IdFamilia = pBeFamilia.IdFamilia
                    Cargar_Productos()
                Else
                    XtraMessageBox.Show(String.Format("No existe familia con código {0}", txtIdFamilia.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdFamilia.Focus()
                    txtIdFamilia.SelectAll()
                    pBeFamilia = Nothing
                    IdFamilia = 0
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdMarca_Validated(sender As Object, e As EventArgs) Handles txtIdMarca.Validated

        Try

            If String.IsNullOrEmpty(txtIdMarca.Text.Trim()) = False AndAlso txtIdMarca.Text > "0" Then

                Dim pBeMarca As New clsBeProducto_marca

                pBeMarca = clsLnProducto_marca.GetSingle(txtIdMarca.Text)

                If pBeMarca IsNot Nothing AndAlso pBeMarca.IdMarca > 0 Then
                    txtNombreMarca.Text = Trim(String.Format("{0}", pBeMarca.Nombre))
                    IdMarca = pBeMarca.IdMarca
                    Cargar_Productos()
                Else
                    XtraMessageBox.Show(String.Format("No existe marca con código {0}", txtIdMarca.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdMarca.Focus()
                    txtIdMarca.SelectAll()
                    pBeMarca = Nothing
                    IdMarca = 0
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdPropietario_Validated(sender As Object, e As EventArgs) Handles txtIdPropietario.Validated

        Try

            If String.IsNullOrEmpty(txtIdPropietario.Text.Trim()) = False AndAlso txtIdPropietario.Text > "0" Then

                Dim pBePropietario As New clsBePropietarios

                pBePropietario = clsLnPropietarios.GetSingle(txtIdPropietario.Text)

                If pBePropietario IsNot Nothing AndAlso pBePropietario.IdPropietario > 0 Then
                    txtNombrePropietario.Text = Trim(String.Format("{0}", pBePropietario.Nombre_comercial))
                    IdPropietario = pBePropietario.IdPropietario
                    Cargar_Productos()
                Else
                    XtraMessageBox.Show(String.Format("No existe propietario con código {0}", txtIdPropietario.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdPropietario.Focus()
                    txtIdPropietario.SelectAll()
                    pBePropietario = Nothing
                    IdPropietario = 0
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdClasificacion_TextChanged(sender As Object, e As EventArgs) Handles txtIdClasificacion.TextChanged
        txtNombreClas.Text = ""

        If txtIdClasificacion.Text Is "" Then
            IdClasificacion = 0
            Cargar_Productos()
        End If

    End Sub

    Private Sub txtIdFamilia_TextChanged(sender As Object, e As EventArgs) Handles txtIdFamilia.TextChanged

        txtNombreFam.Text = ""

        If txtIdFamilia.Text Is "" Then
            IdFamilia = 0
            Cargar_Productos()
        End If

    End Sub

    Private Sub txtIdMarca_TextChanged(sender As Object, e As EventArgs) Handles txtIdMarca.TextChanged

        txtNombreMarca.Text = ""

        If txtIdMarca.Text Is "" Then
            IdMarca = 0
            Cargar_Productos()
        End If

    End Sub

    Private Sub txtIdPropietario_TextChanged(sender As Object, e As EventArgs) Handles txtIdPropietario.TextChanged

        txtNombrePropietario.Text = ""

        If txtIdPropietario.Text Is "" Then
            IdPropietario = 0
            Cargar_Productos()
        End If

    End Sub

    Private Sub mnuMarcarTodoss_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMarcarTodoss.CheckedChanged

        Try

            pgbPrds.Properties.PercentView = True
            pgbPrds.Properties.Minimum = 0
            pgbPrds.Properties.Step = 1
            pgbPrds.Visible = True
            pgbPrds.EditValue = 0

            Dim Codigo As String
            Dim vMarcado As Boolean = mnuMarcarTodoss.Checked

            For i = 0 To GridView1.DataRowCount - 1

                Codigo = CType(GridView1.GetRowCellValue(i, "Codigo"), String)

                ListProductos.Find(Function(x) x.Codigo = Codigo).Seleccionar = vMarcado
                GridView1.SetRowCellValue(i, "Seleccionar", vMarcado)

                pgbPrds.PerformStep()
                pgbPrds.Update()

            Next

            Cargar_Productos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            pgbPrds.Visible = False
        End Try
    End Sub

    Private Sub Llena_lista()

        Try

            ListProductos = clsLnProducto.Get_All_Productos_ForImpresionBarras(IdPropietario, IdClasificacion, IdFamilia, IdMarca)

            Cargar_Productos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPrint.ItemClick

        Try
            cmdPrint.Enabled = False

            Dim vBarra As String = ""

            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()

            If DialogResult.OK = pd.ShowDialog(Me) Then

                For Each Prd As clsBeProducto_Barras_Seleccion In ListProductos
                    If Prd.Seleccionar Then
                        Imprimir_Etiqueta(Prd.Codigo, pd.PrinterSettings.PrinterName)
                    End If
                Next

            End If

            cmdPrint.Enabled = True
        Catch ex As Exception
            cmdPrint.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Shared Sub Imprimir_Etiqueta(ByVal Codigo As String,
                                  ByVal PrinterName As String)

        'CODE128 - A
        Dim ZPLString As String = String.Format("  ^XA
                                                    ^MMT
                                                    ^PW812
                                                    ^LL0406
                                                    ^LS0
                                                    ^FT800,374^A0I,25,24^FH\^FDTOM, WMS. - Location Tag^FS
                                                    ^FO598,191^GB211,162,8^FS
                                                    ^FO598,13^GB206,172,8^FS
                                                    ^FO697,18^GB0,165,4^FS
                                                    ^FO702,196^GB0,154,5^FS
                                                    ^BY5,3,275^FT478,70^BCI,,Y,N
                                                    ^FD>:{5}^FS
                                                    ^PQ1,0,1,Y
                                                    ^XZ", Codigo)

        Try

            RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Impresión de ubicaciones",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Llena_lista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Producto_Etiquetas_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Close()
        End If

    End Sub

End Class
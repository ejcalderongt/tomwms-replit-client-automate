Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmMotivo_Correccion

    Private DTBodegasAsignadasMotivosCorreccionPoliza As DataTable
    Private pListObjT As New List(Of clsTabla)

    Public pBeMotivoCorreccionBodega As New clsBeTrans_oc_pol_motivo_correccion
    Public pBeMotivoCorreccion As New clsBeTrans_oc_pol_motivo_correccion
    Public pBeMotivoCorreccionBodegaList As List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)
    Public pIdMotivoCorreccion As Integer
    Public Delegate Sub Listar_Motivos_Correccion()
    Public Property Listar As Listar_Motivos_Correccion

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Listar_Bodegas()

        Try

            Grid.BeginUpdate()

            If DTBodegasAsignadasMotivosCorreccionPoliza.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0

                For i As Integer = 0 To DTBodegasAsignadasMotivosCorreccionPoliza.Rows.Count - 1

                    vIdBodega = DTBodegasAsignadasMotivosCorreccionPoliza(i)(0)
                    Dim lRow As DataRow = DsMotivoCorreccion.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Asignar") = False

                    If TipoTrans.Editar Then

                        If pBeMotivoCorreccionBodegaList IsNot Nothing AndAlso pBeMotivoCorreccionBodegaList.Count > 0 Then

                            For Each BeTrans_oc_pol_motivo_correccion_bodegaBeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega In pBeMotivoCorreccionBodegaList

                                If BeTrans_oc_pol_motivo_correccion_bodegaBeTrans_oc_pol_motivo_correccion_bodega.IdBodega = CInt(DTBodegasAsignadasMotivosCorreccionPoliza(i)(0)) AndAlso BeTrans_oc_pol_motivo_correccion_bodegaBeTrans_oc_pol_motivo_correccion_bodega.Activo Then
                                    lRow.Item("Asignar") = True
                                    lRow.Item("IdMotivoCorreccionBodega") = BeTrans_oc_pol_motivo_correccion_bodegaBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccionBodega
                                End If

                                lRow.Item("IdInterno") = BeTrans_oc_pol_motivo_correccion_bodegaBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccionBodega

                            Next

                        End If

                    End If

                    DsMotivoCorreccion.Data.AddDataRow(lRow)

                Next

            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridView1.Columns("Asignar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If ritem IsNot Nothing Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pBeMotivoCorreccionBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                                 AndAlso b.IdMotivoCorreccion = pBeMotivoCorreccionBodega.IdMotivoCorreccion)
                If lIndex > -1 Then

                    If ritem.Checked Then
                        pBeMotivoCorreccionBodegaList(lIndex).Activo = True
                    Else
                        pBeMotivoCorreccionBodegaList(lIndex).Activo = False
                    End If

                    pBeMotivoCorreccionBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeMotivoCorreccionBodegaList(lIndex).Fec_mod = Now

                Else

                    Dim BeTrans_oc_pol_motivo_correccion_bodega As New clsBeTrans_oc_pol_motivo_correccion_bodega() With {.IdBodega = Dr.Item("IdBodega"), .IdMotivoCorreccion = pBeMotivoCorreccionBodega.IdMotivoCorreccion, .User_agr = AP.UsuarioAp.IdUsuario, .Fec_agr = Now, .User_mod = AP.UsuarioAp.IdUsuario, .Fec_mod = Now, .Activo = True}
                    pBeMotivoCorreccionBodegaList.Add(BeTrans_oc_pol_motivo_correccion_bodega)

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

    Private Sub Listar_Bodegas_Asignadas()

        Try

            DsMotivoCorreccion.Clear()
            DTBodegasAsignadasMotivosCorreccionPoliza = IMS.Listar_Bodegas()
            pBeMotivoCorreccionBodegaList = New List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)
            pBeMotivoCorreccionBodegaList = clsLnTrans_oc_pol_motivo_correccion_bodega.Get_All_By_MotivoCorreccion(pBeMotivoCorreccionBodega.IdMotivoCorreccion).ToList()

            Listar_Bodegas()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMotivo_Correccion_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("trans_oc_pol_motivo_correccion")

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTrans_oc_pol_motivo_correccion.MaxID() + 1
                    cmbEmpresa.Enabled = True

                    'User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    'Fec_agrDateEdit.Text = Now
                    'User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    'Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbEmpresa.Enabled = True

                    TabDatos.TabPages.Remove(TabMotivoCorreccionBodega)

                Case TipoTrans.Editar

                    pBeMotivoCorreccion = clsLnTrans_oc_pol_motivo_correccion.GetSingle(pBeMotivoCorreccion.IdMotivoCorreccion)

                    lblCodigo.Text = pBeMotivoCorreccion.IdMotivoCorreccion
                    cmbEmpresa.EditValue = pBeMotivoCorreccion.IdEmpresa
                    NombreTextEdit.Text = pBeMotivoCorreccion.Nombre

                    'User_agrTextEdit.Text = pBeMotivoCorreccion.User_agr
                    'Fec_agrDateEdit.Text = pBeMotivoCorreccion.Fec_agr
                    'User_modTextEdit.Text = pBeMotivoCorreccion.User_mod
                    'Fec_modDateEdit.Text = pBeMotivoCorreccion.Fec_mod

                    chkActivo.Checked = pBeMotivoCorreccion.Activo

                    pBeMotivoCorreccionBodega.IdMotivoCorreccion = pBeMotivoCorreccion.IdMotivoCorreccion
                    pBeMotivoCorreccionBodega.IdEmpresa = pBeMotivoCorreccion.IdEmpresa
                    pBeMotivoCorreccionBodega.Nombre = pBeMotivoCorreccion.Nombre

                    pBeMotivoCorreccionBodega.User_agr = pBeMotivoCorreccion.User_agr
                    pBeMotivoCorreccionBodega.Fec_agr = pBeMotivoCorreccion.Fec_agr
                    pBeMotivoCorreccionBodega.User_mod = pBeMotivoCorreccion.User_mod
                    pBeMotivoCorreccionBodega.Fec_mod = pBeMotivoCorreccion.Fec_mod
                    pBeMotivoCorreccionBodega.Activo = pBeMotivoCorreccion.Activo

                    cmbEmpresa.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    Listar_Bodegas_Asignadas()

            End Select

            NombreTextEdit.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try
            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        If XtraMessageBox.Show("Se guardó el registro, ¿asignar bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            TabDatos.TabPages.Add(TabMotivoCorreccionBodega)

                            Listar_Bodegas_Asignadas()

                            mnuGuardar.Enabled = False
                            mnuActualizar.Enabled = True
                            mnuEliminar.Enabled = True

                        Else
                            Close()
                        End If

                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(NombreTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            ElseIf NombreTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim ObjN As New clsBeTrans_oc_pol_motivo_correccion() With {.IdMotivoCorreccion = clsLnTrans_oc_pol_motivo_correccion.MaxID() + 1,
                .IdEmpresa = cmbEmpresa.EditValue,
                .Nombre = NombreTextEdit.Text,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            pBeMotivoCorreccionBodega.IdMotivoCorreccion = ObjN.IdMotivoCorreccion

            Guardar = clsLnTrans_oc_pol_motivo_correccion.Insertar(ObjN) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pBeMotivoCorreccionBodega.IdEmpresa = cmbEmpresa.EditValue
                pBeMotivoCorreccionBodega.Nombre = NombreTextEdit.Text.Trim()

                pBeMotivoCorreccionBodega.User_mod = AP.UsuarioAp.IdUsuario
                If pBeMotivoCorreccionBodega.User_agr Is Nothing Then pBeMotivoCorreccionBodega.User_agr = AP.UsuarioAp.IdUsuario
                If pBeMotivoCorreccionBodega.Fec_agr = Nothing Then pBeMotivoCorreccionBodega.Fec_agr = Now

                pBeMotivoCorreccionBodega.Fec_mod = Now
                pBeMotivoCorreccionBodega.Activo = chkActivo.Checked

                Actualizar = clsLnTrans_oc_pol_motivo_correccion.ActualizarDatos(pBeMotivoCorreccionBodega, pBeMotivoCorreccionBodegaList)

            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function


End Class
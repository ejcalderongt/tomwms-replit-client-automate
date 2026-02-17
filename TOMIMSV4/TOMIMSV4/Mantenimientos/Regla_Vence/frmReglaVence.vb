Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmReglaVence

    Private ReglaVencimiento As New clsBeRegla_vencimiento
    Private ExisteReglaVencimiento As New clsBeRegla_vencimiento
    Private lReglaVencimientos As New List(Of clsBeRegla_vencimiento)
    Private pReglaVencimiento As New clsBeRegla_vencimiento
    Private pExisteReglaVencimiento As New clsBeRegla_vencimiento

    Public Sub New()
        InitializeComponent()
    End Sub

    Private DTGridReglaVencimiento As New DataTable("DetalleIngreso")

    Private Sub Set_Datata_Table_Grid_Detalle_Documento_Ingreso()

        DTGridReglaVencimiento.Columns.Clear()
        DTGridReglaVencimiento.Columns.Add("IdReglaVencimiento", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("NombreRegla", GetType(String))
        DTGridReglaVencimiento.Columns.Add("IdBodega", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("IdProductoFamilia", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("IdProductoClasificacion", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("TiempoVencimientoDias", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("TipoNotificacion", GetType(String))
        DTGridReglaVencimiento.Columns.Add("IdPropietario", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("IdProveedor", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("IdCliente", GetType(Integer))
        DTGridReglaVencimiento.Columns.Add("Activa", GetType(Boolean))
        DTGridReglaVencimiento.Columns.Add("FechaCreacion", GetType(DateTime))
        DTGridReglaVencimiento.Columns.Add("UsuarioCreacion", GetType(String))
        DTGridReglaVencimiento.Columns.Add("FechaModificacion", GetType(DateTime))
        DTGridReglaVencimiento.Columns.Add("UsuarioModificacion", GetType(String))
    End Sub

    Private Sub frmReglaVence_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            mnuActualizar.Enabled = False

            Set_Datata_Table_Grid_Detalle_Documento_Ingreso()
            Set_Columnas_Grid_Regla_Vencimiento()
            Cargar_Reglas_Vencimiento()

            If DTGridReglaVencimiento.Rows.Count > 0 Then
                mnuEliminar.Enabled = True
                '#EJC201906312: Uitlicé el footer del grid para desplegar la cantida dde registros.            
                lblRegs.Caption = String.Format("Registros: {0}", DTGridReglaVencimiento.Rows.Count)
            Else
                mnuEliminar.Enabled = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Reglas_Vencimiento()
        Try


            DTGridReglaVencimiento.Clear()
            DgridReglaVencimiento.DataSource = Nothing

            lReglaVencimientos = New List(Of clsBeRegla_vencimiento)
            lReglaVencimientos = clsLnRegla_vencimiento.Get_All()

            If lReglaVencimientos.Count > 0 Then

                For Each Obj As clsBeRegla_vencimiento In lReglaVencimientos

                    DTGridReglaVencimiento.Rows.Add(Obj.IdReglaVencimiento,
                                Obj.NombreRegla,
                                Obj.IdBodega,
                                Obj.IdProductoFamilia,
                                Obj.IdProductoClasificacion,
                                Obj.TiempoVencimientoDias,
                                Obj.TipoNotificacion,
                                Obj.IdPropietarioMercancia,
                                Obj.IdProveedor,
                                Obj.IdCliente,
                                Obj.Activa,
                                Obj.FechaCreacion,
                                Obj.UsuarioCreacion,
                                Obj.FechaModificacion,
                                Obj.UsuarioModificacion)
                Next

            End If



            DgridReglaVencimiento.DataSource = DTGridReglaVencimiento

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub bbiSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        Try
            mnuGuardar.Enabled = False

            If Datos_Correctos() Then
                If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'If Not InvokeListarServicios Is Nothing Then InvokeListarServicios.Invoke()
                        Close()

                    End If
                End If
            End If

            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If Not Process_Servicios() Then
                XtraMessageBox.Show("El detalle de la regla tiene errores o no se han llenado los campos requeridos, por favor revise el detalle.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Existe_Regla() Then
                XtraMessageBox.Show(msgExisteRegla, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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


    Private Function Process_Servicios() As Boolean

        Process_Servicios = False

        Try

            ExisteReglaVencimiento = Nothing

            If gvReglaVencimiento.DataRowCount > 0 Then

                Dim vNombreRegla As String = ""
                Dim vIdBodega As Integer = 0
                Dim vIdPropietarioBodega As Integer = 0
                Dim vIdProductoFamilia As Integer = 0
                Dim vIdProductoClasificacion As Integer = 0
                Dim vIdProveedor As Integer = 0
                Dim vIdCliente As Integer = 0
                Dim vIdReglaVencimiento As Integer = 0
                Dim vTiempoVencimientoDias As Integer = 0
                Dim vTipoNotificacion As Integer = 0

                '#GT15052024: limpiamos el objeto porque se utilizó para cargar reglas existentes en el load
                ' y ahora lo usamos para llenarlo desde el grid, sino se limpia se podrian duplicar registros.
                lReglaVencimientos = New List(Of clsBeRegla_vencimiento)

                For i As Integer = 0 To gvReglaVencimiento.DataRowCount - 1

                    vIdReglaVencimiento = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdReglaVencimiento")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdReglaVencimiento"))
                    vNombreRegla = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "NombreRegla")), "", gvReglaVencimiento.GetRowCellValue(i, "NombreRegla"))
                    vIdBodega = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdBodega")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdBodega"))
                    vIdProductoFamilia = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdProductoFamilia")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdProductoFamilia"))
                    vIdProductoClasificacion = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdProductoClasificacion")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdProductoClasificacion"))
                    vTiempoVencimientoDias = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "TiempoVencimientoDias")), 0, gvReglaVencimiento.GetRowCellValue(i, "TiempoVencimientoDias"))
                    vTipoNotificacion = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "TipoNotificacion")), "", gvReglaVencimiento.GetRowCellValue(i, "TipoNotificacion"))
                    vIdPropietarioBodega = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdPropietario")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdPropietario"))
                    vIdProveedor = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdProveedor")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdProveedor"))
                    vIdCliente = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdCliente")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdCliente"))


                    '#GT12122023: validar que regla no exista?
                    ExisteReglaVencimiento = clsLnRegla_vencimiento.Get_By_IdReglaVencimiento(vIdReglaVencimiento)

                    If ExisteReglaVencimiento Is Nothing Then '#EJC20210420: La regla no existe
                        ReglaVencimiento = New clsBeRegla_vencimiento
                        ReglaVencimiento.IsNew = True
                        ReglaVencimiento.NombreRegla = vNombreRegla.Trim()
                        ReglaVencimiento.IdBodega = vIdBodega
                        ReglaVencimiento.IdCliente = vIdCliente
                        ReglaVencimiento.Activa = True
                        ReglaVencimiento.FechaCreacion = Now
                        ReglaVencimiento.UsuarioCreacion = AP.UsuarioAp.IdUsuario
                        ReglaVencimiento.FechaModificacion = Now
                        ReglaVencimiento.UsuarioModificacion = AP.UsuarioAp.IdUsuario
                        ReglaVencimiento.IdProductoClasificacion = vIdProductoClasificacion
                        ReglaVencimiento.IdProductoFamilia = vIdProductoFamilia
                        ReglaVencimiento.TiempoVencimientoDias = vTiempoVencimientoDias
                        ReglaVencimiento.TipoNotificacion = vTipoNotificacion
                        ReglaVencimiento.IdPropietarioMercancia = vIdPropietarioBodega
                        ReglaVencimiento.IdProveedor = vIdProveedor
                        lReglaVencimientos.Add(ReglaVencimiento)
                    Else
                        '#GT15052024: si existe regla validar que los días.
                        If ExisteReglaVencimiento.TiempoVencimientoDias <> vTiempoVencimientoDias Then
                            ExisteReglaVencimiento.TiempoVencimientoDias = vTiempoVencimientoDias
                            ExisteReglaVencimiento.UsuarioModificacion = AP.UsuarioAp.IdUsuario
                            ExisteReglaVencimiento.FechaModificacion = Now
                            '#GT15052024: si la regla ya existe, solo editamos dias, Isnew es falso.
                            ExisteReglaVencimiento.IsNew = False
                            lReglaVencimientos.Add(ExisteReglaVencimiento)
                        Else
                            ExisteReglaVencimiento.IsNew = False
                            ExisteReglaVencimiento.NombreRegla = vNombreRegla.Trim()
                            ExisteReglaVencimiento.IdBodega = vIdBodega
                            ExisteReglaVencimiento.IdCliente = vIdCliente
                            ExisteReglaVencimiento.Activa = True
                            ExisteReglaVencimiento.FechaCreacion = Now
                            ExisteReglaVencimiento.UsuarioCreacion = AP.UsuarioAp.IdUsuario
                            ExisteReglaVencimiento.FechaModificacion = Now
                            ExisteReglaVencimiento.UsuarioModificacion = AP.UsuarioAp.IdUsuario
                            ExisteReglaVencimiento.IdProductoClasificacion = vIdProductoClasificacion
                            ExisteReglaVencimiento.IdProductoFamilia = vIdProductoFamilia
                            ExisteReglaVencimiento.TiempoVencimientoDias = vTiempoVencimientoDias
                            ExisteReglaVencimiento.TipoNotificacion = vTipoNotificacion
                            ExisteReglaVencimiento.IdPropietarioMercancia = vIdPropietarioBodega
                            ExisteReglaVencimiento.IdProveedor = vIdProveedor
                            lReglaVencimientos.Add(ExisteReglaVencimiento)
                        End If
                    End If

                Next

                Process_Servicios = (lReglaVencimientos.Count > 0)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Dim msgExisteRegla As String = ""
    Private Function Existe_Regla() As Boolean

        Existe_Regla = False

        Try

            For Each regla In lReglaVencimientos

                If regla.IsNew Then
                    pExisteReglaVencimiento = clsLnRegla_vencimiento.Existe_Regla_Vencimiento(regla)

                    If pExisteReglaVencimiento IsNot Nothing Then
                        Existe_Regla = True
                        msgExisteRegla = "La configuración de la nueva regla: " & regla.NombreRegla & " ya existe en la regla: " & pExisteReglaVencimiento.IdReglaVencimiento & "-" & pExisteReglaVencimiento.NombreRegla
                        Exit For
                    End If

                End If

            Next

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            If lReglaVencimientos IsNot Nothing Then
                clsLnRegla_vencimiento.Guardar_Lista(lReglaVencimientos)
                Guardar = True
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



    Private PropietarioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ProductoFamiliaGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private BodegaGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ProductoClasificacionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ProveedorGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ClienteGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private Sub Set_Columnas_Grid_Regla_Vencimiento()

        Dim vContadorVisibleIndex As Integer = 0

        Try

            DgridReglaVencimiento.DataSource = DTGridReglaVencimiento
            gvReglaVencimiento.OptionsView.ShowFooter = True
            gvReglaVencimiento.OptionsView.ShowGroupPanel = False
            gvReglaVencimiento.OptionsView.ColumnAutoWidth = False
            gvReglaVencimiento.Columns.Clear()

            ' Columna - IdReglaVencimiento
            Dim ColIdReglaVencimiento As New GridColumn With {
            .FieldName = "IdReglaVencimiento",
            .Caption = "IdReglaVencimiento",
            .Visible = False
        }
            gvReglaVencimiento.Columns.Add(ColIdReglaVencimiento)

            ' Columna - NombreRegla
            Dim ColNombreRegla As New GridColumn With {
            .FieldName = "NombreRegla",
            .Caption = "Nombre",
            .Visible = True,
            .VisibleIndex = vContadorVisibleIndex
        }
            ColNombreRegla.Width = 200
            gvReglaVencimiento.Columns.Add(ColNombreRegla)
            vContadorVisibleIndex += 1

#Region "Columna Bodega"

            Try

                BodegaGridLookUpEdit.View.Columns.Clear()

                BodegaGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Bodega", .Visible = True}
                })

                BodegaGridLookUpEdit.ValueMember = "Codigo"
                BodegaGridLookUpEdit.DisplayMember = "Nombre"
                BodegaGridLookUpEdit.NullText = ""
                BodegaGridLookUpEdit.DataSource = clsLnBodega.Get_All_By_IdEmpresa_And_IdUsuario_DT(AP.IdEmpresa, AP.UsuarioAp.IdUsuario)
                BodegaGridLookUpEdit.View.BestFitColumns()
                BodegaGridLookUpEdit.PopupFormWidth = 700

                RemoveHandler BodegaGridLookUpEdit.Leave, AddressOf BodegaGridLookUpEdit_ReglaVencimiento_Leave
                AddHandler BodegaGridLookUpEdit.Leave, AddressOf BodegaGridLookUpEdit_ReglaVencimiento_Leave

                BodegaGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

                Dim ColBodega As New GridColumn With {
                    .FieldName = "IdBodega",
                    .Caption = "Bodega",
                    .Visible = True,
                    .VisibleIndex = vContadorVisibleIndex,
                    .ColumnEdit = BodegaGridLookUpEdit
                }

                ColBodega.Width = 200
                ColBodega.OptionsColumn.AllowEdit = True
                gvReglaVencimiento.Columns.Add(ColBodega)

            Catch ex As Exception
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

#End Region

#Region "Columna Propietario"

            PropietarioGridLookUpEdit.View.Columns.Clear()

            PropietarioGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPropietario", .Caption = "IdPropietario", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PropietarioGridLookUpEdit.ValueMember = "IdPropietario"
            PropietarioGridLookUpEdit.DisplayMember = "Nombre"
            PropietarioGridLookUpEdit.NullText = ""
            PropietarioGridLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(AP.IdBodega)
            PropietarioGridLookUpEdit.PopupFormWidth = 700
            PropietarioGridLookUpEdit.View.BestFitColumns()

            RemoveHandler PropietarioGridLookUpEdit.Leave, AddressOf PropietarioGridLookUpEditDetalleIngreso_Leave
            AddHandler PropietarioGridLookUpEdit.Leave, AddressOf PropietarioGridLookUpEditDetalleIngreso_Leave

            PropietarioGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPropieario As New GridColumn With {
                .FieldName = "IdPropietario",
                .Caption = "Propietario",
                .Visible = True,
                .VisibleIndex = vContadorVisibleIndex,
                .ColumnEdit = PropietarioGridLookUpEdit
            }
            ColPropieario.Width = 300
            ColPropieario.OptionsColumn.AllowEdit = True
            ColPropieario.Visible = True
            gvReglaVencimiento.Columns.Add(ColPropieario)
            vContadorVisibleIndex += 1

#End Region

#Region "Columna Producto Familia"

            Try

                ProductoFamiliaGridLookUpEdit.View.Columns.Clear()

                ProductoFamiliaGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                    New GridColumn With {.FieldName = "IdFamilia", .Caption = "IdFamilia", .Visible = False},
                    New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                    New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
                })

                ProductoFamiliaGridLookUpEdit.ValueMember = "IdFamilia"
                ProductoFamiliaGridLookUpEdit.DisplayMember = "Nombre"
                ProductoFamiliaGridLookUpEdit.NullText = ""
                ProductoFamiliaGridLookUpEdit.DataSource = clsLnProducto_familia.Listar_For_Grid()
                ProductoFamiliaGridLookUpEdit.View.BestFitColumns()
                ProductoFamiliaGridLookUpEdit.PopupFormWidth = 700

                ProductoFamiliaGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

                Dim ColProductoFamilia As New GridColumn With {
                    .FieldName = "IdProductoFamilia",
                    .Caption = "Familia",
                    .Visible = True,
                    .VisibleIndex = 2,
                    .ColumnEdit = ProductoFamiliaGridLookUpEdit
                }

                ColProductoFamilia.Width = 300
                ColProductoFamilia.OptionsColumn.AllowEdit = True
                gvReglaVencimiento.Columns.Add(ColProductoFamilia)

                RemoveHandler ProductoFamiliaGridLookUpEdit.Leave, AddressOf ProductoFamiliaGridLookUpEdit_ReglaVencimiento_Leave
                AddHandler ProductoFamiliaGridLookUpEdit.Leave, AddressOf ProductoFamiliaGridLookUpEdit_ReglaVencimiento_Leave

            Catch ex As Exception
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

#End Region

#Region "Columna Producto Clasificación"

            Try

                ProductoClasificacionGridLookUpEdit.View.Columns.Clear()

                ProductoClasificacionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                    New GridColumn With {.FieldName = "IdClasificacion", .Caption = "IdClasificacion", .Visible = False},
                    New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                    New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
                })

                ProductoClasificacionGridLookUpEdit.ValueMember = "IdClasificacion"
                ProductoClasificacionGridLookUpEdit.DisplayMember = "Nombre"
                ProductoClasificacionGridLookUpEdit.NullText = ""
                ProductoClasificacionGridLookUpEdit.DataSource = clsLnProducto_clasificacion.Listar_For_Grid()
                ProductoClasificacionGridLookUpEdit.View.BestFitColumns()
                ProductoClasificacionGridLookUpEdit.PopupFormWidth = 700

                ProductoClasificacionGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

                Dim ColProductoClasificacion As New GridColumn With {
                    .FieldName = "IdProductoClasificacion",
                    .Caption = "Clasificación",
                    .Visible = True,
                    .VisibleIndex = 3,
                    .ColumnEdit = ProductoClasificacionGridLookUpEdit
                }

                ColProductoClasificacion.Width = 300
                ColProductoClasificacion.OptionsColumn.AllowEdit = True
                gvReglaVencimiento.Columns.Add(ColProductoClasificacion)

                RemoveHandler ProductoClasificacionGridLookUpEdit.Leave, AddressOf ProductoClasificacionGridLookUpEdit_ReglaVencimiento_Leave
                AddHandler ProductoClasificacionGridLookUpEdit.Leave, AddressOf ProductoClasificacionGridLookUpEdit_ReglaVencimiento_Leave

            Catch ex As Exception
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

#End Region

#Region "Columna Tiempo Vencimiento (Días)"

            Dim ColTiempoVencimientoDias As New GridColumn With {
            .FieldName = "TiempoVencimientoDias",
            .Caption = "Vencimiento (Días)",
            .Visible = True,
            .VisibleIndex = 4
        }
            ColTiempoVencimientoDias.Width = 150
            gvReglaVencimiento.Columns.Add(ColTiempoVencimientoDias)

#End Region

#Region "Columna Tipo Notificacion"

            Try
                Dim TipoNotificacionLookUpEdit As New RepositoryItemGridLookUpEdit()

                TipoNotificacionLookUpEdit.DataSource = CrearTablaTipoNotificacion()
                TipoNotificacionLookUpEdit.ValueMember = "Id"
                TipoNotificacionLookUpEdit.DisplayMember = "Descripcion"
                TipoNotificacionLookUpEdit.NullText = ""
                TipoNotificacionLookUpEdit.View.Columns.Clear()
                TipoNotificacionLookUpEdit.View.Columns.Add(New GridColumn With {.FieldName = "Descripcion", .Caption = "Tipo", .Visible = True})
                TipoNotificacionLookUpEdit.PopupFormWidth = 150
                TipoNotificacionLookUpEdit.View.BestFitColumns()
                TipoNotificacionLookUpEdit.View.OptionsView.ShowColumnHeaders = False

                Dim ColTipoNotificacion As New GridColumn With {
            .FieldName = "TipoNotificacion",
            .Caption = "Tipo de Notificación",
            .Visible = True,
            .VisibleIndex = 5,
            .ColumnEdit = TipoNotificacionLookUpEdit
        }

                ColTipoNotificacion.Width = 150
                gvReglaVencimiento.Columns.Add(ColTipoNotificacion)

            Catch ex As Exception
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

#End Region

#Region "Columna - Proveedor"

            ProveedorGridLookUpEdit.View.Columns.Clear()
            ProveedorGridLookUpEdit.DataSource = clsLnProveedor.Get_All_For_Grid()
            ProveedorGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdProveedor", .Caption = "IdProveedor", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            ProveedorGridLookUpEdit.ValueMember = "IdProveedor"
            ProveedorGridLookUpEdit.DisplayMember = "Nombre"
            ProveedorGridLookUpEdit.NullText = ""
            ProveedorGridLookUpEdit.PopupFormWidth = 700
            ProveedorGridLookUpEdit.View.BestFitColumns()

            ' Configurar los eventos Leave y otros según sea necesario.
            RemoveHandler ProveedorGridLookUpEdit.Leave, AddressOf ProveedorGridLookUpEdit_Leave
            AddHandler ProveedorGridLookUpEdit.Leave, AddressOf ProveedorGridLookUpEdit_Leave

            ProveedorGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColProveedor As New GridColumn With {
                .FieldName = "IdProveedor",
                .Caption = "Proveedor",
                .Visible = True,
                .VisibleIndex = 7,  ' Ajusta el índice según sea necesario
                .ColumnEdit = ProveedorGridLookUpEdit
            }
            ColProveedor.Width = 300
            ColProveedor.OptionsColumn.AllowEdit = True
            gvReglaVencimiento.Columns.Add(ColProveedor)

#End Region

#Region "Columna - IdCliente"

            ClienteGridLookUpEdit.View.Columns.Clear()

            ClienteGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdCliente", .Caption = "IdCliente", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            ClienteGridLookUpEdit.ValueMember = "IdCliente"
            ClienteGridLookUpEdit.DisplayMember = "Nombre"
            ClienteGridLookUpEdit.NullText = ""
            ClienteGridLookUpEdit.DataSource = clsLnCliente.Get_All_By_IdEmpresa_For_Combo_QA(AP.IdBodega) ' Asumiendo que clsLnCliente es tu clase lógica
            ClienteGridLookUpEdit.PopupFormWidth = 700
            ClienteGridLookUpEdit.View.BestFitColumns()

            RemoveHandler ClienteGridLookUpEdit.Leave, AddressOf ClienteGridLookUpEdit_Leave
            AddHandler ClienteGridLookUpEdit.Leave, AddressOf ClienteGridLookUpEdit_Leave

            ClienteGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColIdCliente As New GridColumn With {
                    .FieldName = "IdCliente",
                    .Caption = "Cliente",
                    .Visible = True,
                    .VisibleIndex = 8,
                    .ColumnEdit = ClienteGridLookUpEdit
                }
            ColIdCliente.Width = 300
            ColIdCliente.OptionsColumn.AllowEdit = True
            gvReglaVencimiento.Columns.Add(ColIdCliente)

#End Region

            gvReglaVencimiento.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

        Catch ex As Exception
            ' Manejo de excepciones
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub ProductoClasificacionGridLookUpEdit_ReglaVencimiento_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim clasificacionLookUpEdit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            Dim drReglaVencimiento As DataRow = gvReglaVencimiento.GetFocusedDataRow()
            If drReglaVencimiento Is Nothing Then Return

            Dim clasificacionSeleccionada As Object = clasificacionLookUpEdit.Properties.GetRowByKeyValue(clasificacionLookUpEdit.EditValue)

            If Not clasificacionSeleccionada Is Nothing Then
                Dim drClasificacion As DataRow = (TryCast(clasificacionLookUpEdit.Properties.GetRowByKeyValue(clasificacionLookUpEdit.EditValue), DataRowView)).Row
                If drClasificacion Is Nothing Then Return

                Dim vIdClasificacion As Integer = drClasificacion("IdClasificacion")
                Dim vNombreClasificacion As String = drClasificacion("Nombre").ToString()

                ' Actualizar el nombre de la clasificación de producto en la fila de la tabla
                drReglaVencimiento("IdProductoClasificacion") = vIdClasificacion

                ' Realizar actualizaciones adicionales en el GridView si es necesario
                gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("TiempoVencimientoDias")
                gvReglaVencimiento.PostEditor()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub ProductoFamiliaGridLookUpEdit_ReglaVencimiento_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim familiaLookUpEdit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            Dim drReglaVencimiento As DataRow = gvReglaVencimiento.GetFocusedDataRow()
            If drReglaVencimiento Is Nothing Then Return

            Dim familiaSeleccionada As Object = familiaLookUpEdit.Properties.GetRowByKeyValue(familiaLookUpEdit.EditValue)

            If Not familiaSeleccionada Is Nothing Then
                Dim drFamilia As DataRow = (TryCast(familiaLookUpEdit.Properties.GetRowByKeyValue(familiaLookUpEdit.EditValue), DataRowView)).Row
                If drFamilia Is Nothing Then Return

                Dim vIdFamilia As Integer = drFamilia("IdFamilia")
                Dim vNombreFamilia As String = drFamilia("Nombre").ToString()

                ' Actualizar el nombre de la familia de producto en la fila de la tabla
                drReglaVencimiento("IdProductoFamilia") = vIdFamilia

                ' Realizar actualizaciones adicionales en el GridView si es necesario
                gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("IdProductoClasificacion")
                gvReglaVencimiento.PostEditor()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub BodegaGridLookUpEdit_ReglaVencimiento_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            Dim drReglaVencimiento As DataRow = gvReglaVencimiento.GetFocusedDataRow()
            If drReglaVencimiento Is Nothing Then Return

            Dim bodegaSeleccionada As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not bodegaSeleccionada Is Nothing Then
                Dim drBodega As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drBodega Is Nothing Then Return
                Dim vIdBodega As Integer = 0
                Dim vCodigoBodega As String = ""
                vIdBodega = drBodega("Codigo")
                drReglaVencimiento("IdBodega") = vIdBodega

                ' Realizar actualizaciones adicionales en el GridView si es necesario
                'gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("IdPropietarioBodega")
                'gvReglaVencimiento.PostEditor()

                Me.DgridReglaVencimiento.BeginInvoke(New MethodInvoker(Sub()
                                                                           gvReglaVencimiento.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                           gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("IdPropietario")
                                                                           gvReglaVencimiento.ShowEditor()
                                                                       End Sub))


            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub PropietarioGridLookUpEditDetalleIngreso_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            Dim drLineaRequisicion As DataRow = gvReglaVencimiento.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim vObjProp As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjProp Is Nothing Then

                Dim drPropietario As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drPropietario Is Nothing Then Return
                Dim vIdPropietario As Integer = 0
                'vIdPropietario = drPropietario("IdPropietarioBodega")
                vIdPropietario = drPropietario("IdPropietario")

                Llena_ProductosLookUp_Grid(vIdPropietario)

                drLineaRequisicion("IdPropietario") = vIdPropietario


                Me.DgridReglaVencimiento.BeginInvoke(New MethodInvoker(Sub()
                                                                           gvReglaVencimiento.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                           gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("TiempoVencimientoDias")
                                                                           gvReglaVencimiento.ShowEditor()
                                                                       End Sub))

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Llena_ProductosLookUp_Grid(ByVal pIdPropietarioBodega As Integer)

        Try

            ProductoGridLookUpEdit.DataSource = clsLnProducto.Get_Lista_For_Grid_By_IdPropietario_And_IdBodega(pIdPropietarioBodega, AP.IdBodega)

        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    Private Function Get_No_Linea_Grid_Detalle() As Integer

        Get_No_Linea_Grid_Detalle = 1

        Try

            Dim vNoLineaGrid As Integer = 0
            Dim vNoLineaSiguiente As Integer = 0

            For i As Integer = 0 To gvReglaVencimiento.DataRowCount - 1
                vNoLineaGrid = IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(i, "IdReglaVencimiento")), 0, gvReglaVencimiento.GetRowCellValue(i, "IdReglaVencimiento"))
            Next i

            Get_No_Linea_Grid_Detalle = vNoLineaGrid + 1

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function
    Private Function CrearTablaTipoNotificacion() As DataTable

        Dim dt As New DataTable
        dt.Columns.Add("Id", GetType(Integer))
        dt.Columns.Add("Descripcion", GetType(String))

        dt.Rows.Add(1, "Correo")
        dt.Rows.Add(2, "Pantalla")
        dt.Rows.Add(3, "Correo/Pantalla")

        Return dt

    End Function
    Private Sub ProveedorGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            Dim drLineaReglaVencimiento As DataRow = gvReglaVencimiento.GetFocusedDataRow()
            If drLineaReglaVencimiento Is Nothing Then Return

            Dim vObjProv As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjProv Is Nothing Then

                Dim drProveedor As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drProveedor Is Nothing Then Return
                Dim vIdProveedor As Integer = 0
                vIdProveedor = drProveedor("IdProveedor")

                ' Aquí puedes insertar cualquier lógica adicional que necesites realizar
                ' cuando un proveedor es seleccionado, por ejemplo, actualizar otros campos
                ' en la línea actual del grid basado en la selección del proveedor.

                ' Actualizar el nombre del proveedor en la fila actual del grid.
                drLineaReglaVencimiento("IdProveedor") = vIdProveedor

                ' Mover el enfoque a la siguiente columna editada habitualmente en tu grid.
                gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("IdCliente") ' Cambia "SiguienteColumna" al nombre real de tu columna.
                gvReglaVencimiento.PostEditor()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub ClienteGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            Dim drLineaReglaVencimiento As DataRow = gvReglaVencimiento.GetFocusedDataRow()
            If drLineaReglaVencimiento Is Nothing Then Return

            Dim vObjCliente As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjCliente Is Nothing Then

                Dim drCliente As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drCliente Is Nothing Then Return
                Dim vIdCliente As Integer = 0
                vIdCliente = drCliente("IdCliente")

                ' Agrega aquí cualquier lógica adicional que necesites realizar
                ' cuando un cliente es seleccionado.

                ' Actualizar el nombre del cliente en la fila actual del grid.
                drLineaReglaVencimiento("NombreCliente") = lista.Text

                ' Mover el enfoque a la siguiente columna editada habitualmente en tu grid.
                'gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.Columns("SiguienteColumna")
                gvReglaVencimiento.PostEditor()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Grid_Tiene_Error As Boolean = False
    Private Sub gvReglaVencimiento_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gvReglaVencimiento.ValidateRow

        Try

            Dim View As GridView = CType(sender, GridView)
            Dim colIdReglaVencimiento As GridColumn = View.Columns("IdReglaVencimiento")
            Dim colNombreRegla As GridColumn = View.Columns("NombreRegla")
            Dim colIdBodega As GridColumn = View.Columns("IdBodega")
            Dim colIdPropietario As GridColumn = View.Columns("IdPropietario")
            Dim colIdProductoFamilia As GridColumn = View.Columns("IdProductoFamilia")
            Dim colIdProductoClasificacion As GridColumn = View.Columns("IdProductoClasificacion")
            Dim colTiempoVencimientoDias As GridColumn = View.Columns("TiempoVencimientoDias")
            Dim colTipoNotificacion As GridColumn = View.Columns("TipoNotificacion")
            Dim colIdProveedor As GridColumn = View.Columns("IdProveedor")
            Dim colIdCliente As GridColumn = View.Columns("IdCliente")

            Dim IdReglaVencimiento As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdReglaVencimiento")), 0, View.GetRowCellValue(e.RowHandle, "IdReglaVencimiento"))
            Dim NombreRegla As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "NombreRegla")), "", View.GetRowCellValue(e.RowHandle, "NombreRegla"))
            Dim IdBodega As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdBodega")), 0, View.GetRowCellValue(e.RowHandle, "IdBodega"))
            Dim IdPropietario As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdPropietario")), 0, View.GetRowCellValue(e.RowHandle, "IdPropietario"))
            Dim IdProductoFamilia As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProductoFamilia")), 0, View.GetRowCellValue(e.RowHandle, "IdProductoFamilia"))
            Dim IdProductoClasificacion As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProductoClasificacion")), 0, View.GetRowCellValue(e.RowHandle, "IdProductoClasificacion"))
            Dim TiempoVencimientoDias As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "TiempoVencimientoDias")), 0, View.GetRowCellValue(e.RowHandle, "TiempoVencimientoDias"))
            Dim TipoNotificacion As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "TipoNotificacion")), 0, View.GetRowCellValue(e.RowHandle, "TipoNotificacion"))
            Dim IdProveedor As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProveedor")), 0, View.GetRowCellValue(e.RowHandle, "IdProveedor"))
            Dim IdCliente As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdCliente")), 0, View.GetRowCellValue(e.RowHandle, "IdCliente"))

            'Dim vExiste_Linea As Boolean = Existe_Linea(IdReglaVencimiento)
            Dim NoLineaGrid As Integer = 0
            Dim Etapa_Uno_Correcta As Boolean = False
            Dim Etapa_Dos_Correcta As Boolean = False
            Dim Etapa_Tres_Correcta As Boolean = False

            '#EJC20210313: Es culpable hasta que se demuestre lo contrario.
            Grid_Tiene_Error = True

            If NombreRegla = "" Then
                e.Valid = False
                View.SetColumnError(colNombreRegla, "Ingrese un nombre para la regla.")
            ElseIf IdBodega = 0 Then
                e.Valid = False
                View.SetColumnError(colIdBodega, "Seleccione una bodega")
            ElseIf IdPropietario = 0 Then
                e.Valid = False
                View.SetColumnError(colIdPropietario, "Seleccione un propietario.")
            ElseIf TiempoVencimientoDias = 0 Then
                e.Valid = False
                View.SetColumnError(colTiempoVencimientoDias, "Ingrese dias > 0")
            ElseIf TipoNotificacion = 0 Then
                e.Valid = False
                View.SetColumnError(colTipoNotificacion, "Seleccione un tipo de notificación.")
            Else
                e.Valid = True : Etapa_Uno_Correcta = True
            End If

            If Etapa_Uno_Correcta Then

                Dim vNombreCorrecto = False
                Dim vReglaUnica = False

                '#GT15052024: dejamos de validar en la lista, porque no existe lo que se agrega al grid
                'Dim vIndiceExistente As clsBeRegla_vencimiento = lReglaVencimientos.FindAll(Function(x) x.NombreRegla = NombreRegla).FirstOrDefault

                Dim vIndiceExistente As Boolean
                Dim pIdRegla As Integer = 0
                Dim pNombreRegla As String = ""

                For indice As Integer = 0 To gvReglaVencimiento.RowCount - 1

                    pIdRegla = CInt(IIf(IsDBNull(gvReglaVencimiento.GetRowCellValue(indice, "IdReglaVencimiento")), 0, gvReglaVencimiento.GetRowCellValue(indice, "IdReglaVencimiento")))
                    pNombreRegla = gvReglaVencimiento.GetRowCellValue(indice, "NombreRegla")

                    If NombreRegla = pNombreRegla AndAlso IdReglaVencimiento <> pIdRegla Then
                        vIndiceExistente = True
                        Exit For
                    End If

                Next

                If vIndiceExistente Then
                    View.SetColumnError(colNombreRegla, "El nombre de regla ya existe!")
                Else
                    vNombreCorrecto = True
                End If



                '#GT15052024: tambien dejamos de validar en la lista, no existe lo agregado en tiempo de ejecución
                'Dim vExisteRegla As clsBeRegla_vencimiento = lReglaVencimientos.Find(Function(x) x.IdPropietarioMercancia = IdPropietario AndAlso
                '                                                                                     x.TiempoVencimientoDias = TiempoVencimientoDias AndAlso
                '                                                                                     x.IdBodega = IdBodega AndAlso
                '                                                                                     x.IdProveedor = IdProveedor)

                Dim vExisteRegla As Boolean
                Dim pPropietarioMercandia As Integer = 0
                Dim pTiempoVencimiento As Integer = 0
                Dim pBodega As Integer = 0
                Dim pProveedor As Integer = 0

                For indice As Integer = 0 To gvReglaVencimiento.RowCount - 1

                    pPropietarioMercandia = CInt(gvReglaVencimiento.GetRowCellValue(indice, "IdPropietario"))
                    pTiempoVencimiento = CInt(gvReglaVencimiento.GetRowCellValue(indice, "TiempoVencimientoDias"))
                    pBodega = CInt(gvReglaVencimiento.GetRowCellValue(indice, "IdBodega"))
                    pProveedor = CInt(gvReglaVencimiento.GetRowCellValue(indice, "IdProveedor"))

                    If pPropietarioMercandia = IdPropietario AndAlso pTiempoVencimiento = TiempoVencimientoDias AndAlso
                        pBodega = IdBodega AndAlso pProveedor = IdProveedor Then
                        vExisteRegla = True
                        Exit For
                    End If

                Next

                If vExisteRegla Then
                    View.SetColumnError(colIdBodega, "Ya existe una regla con las mismas condiciones!")
                Else
                    vReglaUnica = True
                End If

                e.Valid = vNombreCorrecto AndAlso vReglaUnica

                Etapa_Dos_Correcta = e.Valid

            End If

            If Etapa_Uno_Correcta AndAlso Etapa_Dos_Correcta Then
                Grid_Tiene_Error = False
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvReglaVencimiento_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles gvReglaVencimiento.InvalidRowException

        Try

            '#EJC20210307: Evita que salte mensaje indicando si se quiere corregir la fila.
            e.ExceptionMode = ExceptionMode.NoAction

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function CheckAddNewRow() As Boolean
        If gvReglaVencimiento.FocusedColumn.VisibleIndex = gvReglaVencimiento.VisibleColumns.Count - 1 Then
            If gvReglaVencimiento.IsNewItemRow(gvReglaVencimiento.FocusedRowHandle) Then
                gvReglaVencimiento.PostEditor()
                gvReglaVencimiento.UpdateCurrentRow()
            End If

            If gvReglaVencimiento.IsLastRow Then Return AddNewRow()
        End If

        Return False
    End Function

    Private Function AddNewRow() As Boolean
        gvReglaVencimiento.AddNewRow()
        gvReglaVencimiento.FocusedColumn = gvReglaVencimiento.VisibleColumns(0)
        Return True
    End Function

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        cmdImprimir.Enabled = False
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")
        Imprimir_Vista()
        cmdImprimir.Enabled = True
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = DgridReglaVencimiento
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            SplashScreenManager.CloseForm(False)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado reglas vencimiento"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            Dim Dr As DataRowView = gvReglaVencimiento.GetFocusedRow
            Dim lSelectionIndex As Integer = gvReglaVencimiento.FocusedRowHandle

            Dim IdRegla As Integer = IIf(IsDBNull(Dr.Item("IdReglaVencimiento")), 0, Dr.Item("IdReglaVencimiento"))
            Dim NombreRegla As String = IIf(IsDBNull(Dr.Item("NombreRegla")), "", Dr.Item("NombreRegla"))
            Dim bodega As String = IIf(IsDBNull(Dr.Item("IdBodega")), 0, Dr.Item("IdBodega"))
            Dim IdPropietario As Integer = IIf(IsDBNull(Dr.Item("IdPropietario")), 0, Dr.Item("IdPropietario"))

            If Dr Is Nothing Then Return

            Dim vContinuar As Boolean = True


            Dim vMensaje As String = "¿Elliminar la regla de vencimiento: " & IdRegla & " - " & NombreRegla

            If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            End If

        Catch ex As Exception

        End Try

    End Sub

End Class
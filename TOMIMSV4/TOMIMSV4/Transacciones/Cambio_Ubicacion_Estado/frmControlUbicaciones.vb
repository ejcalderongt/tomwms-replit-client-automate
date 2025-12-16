Imports DevExpress.XtraEditors
Public Class frmControlUbicaciones

    Public pBeBodega_ubicacion As Integer
    Public listaUbicaciones As New List(Of clsBeBodega_ubicacion)
    Public listaUbicacionesUsadas As New List(Of clsBeBodega_ubicacion)


    Public Sub cargar_datos()

        Try

            Dim total, usadas As Integer

            listaUbicaciones = clsLnBodega_ubicacion.GetAll()
            listaUbicacionesUsadas = clsLnBodega_ubicacion.GetUbicacionesStock()

            usadas = listaUbicacionesUsadas.Count
            total = listaUbicaciones.Count

            ArcScaleRangeBarComponent4.Value = usadas
            ArcScaleComponent4.MaxValue = total

            LabelComponent4.Text = usadas
            LabelComponent6.Text = total

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pStackTrace:=ex.StackTrace,
                                                  pUsrAgr:=AP.UsuarioAp.IdUsuario)

        End Try

    End Sub

    Private Sub frmControlUbicaciones_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            cargar_datos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pIdEmpresa:=AP.IdEmpresa,
                                                  pIdBodega:=AP.IdBodega,
                                                  pStackTrace:=ex.StackTrace,
                                                  pUsrAgr:=AP.UsuarioAp.IdUsuario)

        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Close()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        cargar_datos()
    End Sub
End Class
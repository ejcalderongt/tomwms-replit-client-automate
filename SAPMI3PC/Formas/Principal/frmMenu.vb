Imports System.Reflection
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors

Public Class frmMenu
    Private Property Interface_A_Ejecutar As pInterfaceAEjecutar = -1
    Private Property IdConfiguracionInterface As Integer = -1
    Public Sub CheckAndCloseIfNoChildren()
        If Me.MdiChildren.Length = 0 Then
            Me.Close()
        End If
    End Sub

    Public Enum pInterfaceAEjecutar

        Ninguna = -1
        Bodegas = 0
        Productos = 1
        Proveedores = 2
        Recibir_Pedidos_De_Compra = 3
        Recibir_Pedidos_De_Traslado = 4
        Envio_Pedidos_De_Compra = 5
        Envio_Pedidos_De_Transferencia = 6

    End Enum

    Public Sub New(ByVal InterfaceAEjecutar As pInterfaceAEjecutar, ByVal pIdConfiguracion As Integer, ByVal pIdUsuario As Integer)

        InitializeComponent()

        Interface_A_Ejecutar = InterfaceAEjecutar
        IdConfiguracionInterface = pIdConfiguracion
        IdUsuario = pIdUsuario

    End Sub

    Public Sub New(ByVal pIdConfiguracion As Integer)

        InitializeComponent()

        IdConfiguracionInterface = pIdConfiguracion

    End Sub

    Public Sub New(ByVal InterfaceAEjecutar As pInterfaceAEjecutar, ByVal pIdConfiguracion As Integer)

        InitializeComponent()

        Interface_A_Ejecutar = InterfaceAEjecutar
        IdConfiguracionInterface = pIdConfiguracion

    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub mnuPrueba_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPrueba.ItemClick

        Try

            With frmEjecucion
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

            rbMain.SelectedPage = rbMain.MergedPages(0)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles Me.Load

        UseWaitCursor = True

        Try

            Set_Info_Conexion()

            BarStaticItem1.Caption = gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion)

            rbMain.MdiMergeStyle = RibbonMdiMergeStyle.Always

            If RemoteCallBack Then

                mnuPrueba.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                With frmEjecucion
                    .MdiParent = Me
                    .WindowState = FormWindowState.Maximized
                    .Interface_A_Ejecutar = Interface_A_Ejecutar
                    .Show()
                    .Focus()
                End With

                rbMain.SelectedPage = rbMain.MergedPages(0)

            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            UseWaitCursor = False
        End Try

    End Sub

    Private Sub Set_Info_Conexion()

        Try

            lblServerAPP.Caption += " " & BD.Instancia.Server
            lblBDAPP.Caption += " " & BD.Instancia.NombreBD
            lblNombrePC.Caption = Net.Dns.GetHostName()
            lblEmpresa.Caption += " " & BD.Instancia.SAP_COMPANY_DB
            lblBodega.Caption += " " & clsLnBodega.Get_Nombre_Bodega_By_IdBodega(BeConfigEnc.Idbodega)
            lblInstancia.Caption = gNombreInstancia

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

End Class
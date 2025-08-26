Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraEditors

Public Class frmDashViewer1

    Public Property IdentificadorTablero As pTipoTablero = pTipoTablero.Tipo_Movimientos
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Enum pTipoTablero

        Tipo_Movimientos = 0
        Productividad_Ingresos_Por_Operador_Y_Tipo_Documento = 1
        Productividad_Picking_Por_Operador = 2
        Productos_con_menor_rotación_Por_Mes_Y_Año = 3
        Top_15_Productos_Salida = 4
        Top_15_Productos_Salida_Por_Mes = 5
        Dash_Movimientos_Por_Periodo = 6
        Cantidad_De_Pedidos_Por_Cliente = 7

    End Enum

    Sub New()
        InitializeComponent()
    End Sub

    Private Sub dv_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs) Handles DashboardViewer1.ConfigureDataConnection

        Try

            Dim connectionParameters As CustomStringConnectionParameters = New CustomStringConnectionParameters(clsBD.Instancia.CadenaConexionSQLClient)
            e.ConnectionParameters = connectionParameters

            Dim oParam As New MsSqlConnectionParameters()
            oParam.AuthorizationType = MsSqlAuthorizationType.SqlServer
            oParam.ServerName = clsBD.Instancia.Server
            oParam.DatabaseName = clsBD.Instancia.NombreBD
            oParam.UserName = clsBD.Instancia.Usuario
            oParam.Password = clsBD.Instancia.Clave
            e.ConnectionParameters = oParam
            e.ConnectionParameters = connectionParameters

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        DashboardViewer1.ReloadData()
    End Sub

    Private Sub frmDashViewer1_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Dim vNombreTemplate As String = CurDir() & "\Tableros_Por_Defecto\"

            Select Case IdentificadorTablero

                Case pTipoTablero.Tipo_Movimientos

                    vNombreTemplate += "Dash_Mov_Periodo_ProdList.xml"

                Case pTipoTablero.Productividad_Ingresos_Por_Operador_Y_Tipo_Documento

                    vNombreTemplate += "Productividad_Ingresos_Por_Operador_Y_Tipo_Documento.xml"

                Case pTipoTablero.Top_15_Productos_Salida

                    vNombreTemplate += "Top_15_Productos_Salida.xml"

                Case pTipoTablero.Top_15_Productos_Salida_Por_Mes

                    vNombreTemplate += "Top_15_Productos_Salida_Por_Mes.xml"

                Case pTipoTablero.Productividad_Picking_Por_Operador

                    vNombreTemplate += "Productividad_Picking_Por_Operador.xml"

                Case pTipoTablero.Dash_Movimientos_Por_Periodo

                    vNombreTemplate += "Dash_Movimientos_Por_Periodo.xml"

                Case pTipoTablero.Productos_con_menor_rotación_Por_Mes_Y_Año

                    vNombreTemplate += "Productos_con_menor_rotación_Por_Mes_Y_Año.xml"

                Case pTipoTablero.Cantidad_De_Pedidos_Por_Cliente

                    vNombreTemplate += "Cantidad_De_Pedidos_Por_Cliente.xml"

            End Select

            If Not IdentificadorTablero = -1 Then
                If IO.File.Exists(vNombreTemplate) Then
                    DashboardViewer1.DashboardSource = vNombreTemplate
                Else
                    MsgBox("No existe la plantilla del tablero o no tiene permisos para utilizarla", MsgBoxStyle.Exclamation, Text)
                    Close()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuAbrirXMLExistente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAbrirXMLExistente.ItemClick
        Try
            ' Crear un OpenFileDialog para seleccionar un archivo XML
            Using openFileDialog As New OpenFileDialog()
                openFileDialog.Filter = "XML Files (*.xml)|*.xml"
                openFileDialog.Title = "Seleccionar archivo XML"
                openFileDialog.InitialDirectory = CurDir()
                ' Mostrar el diálogo de selección de archivo
                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    ' Obtener la ruta del archivo seleccionado
                    Dim filePath As String = openFileDialog.FileName

                    ' Crear un Dashboard desde el archivo XML seleccionado
                    Dim dashboard As New DevExpress.DashboardCommon.Dashboard()
                    dashboard.LoadFromXml(filePath)

                    ' Cargar el Dashboard en el Dashboard Viewer
                    DashboardViewer1.Dashboard = dashboard
                End If
            End Using
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


End Class
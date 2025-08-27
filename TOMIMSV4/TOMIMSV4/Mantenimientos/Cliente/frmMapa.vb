Imports DevExpress.XtraEditors
Imports DevExpress.XtraMap

Public Class frmMapa

    ' Create a layer to display vector items.
    Dim itemsLayer As New VectorItemsLayer()
    ' Create a layer to load image tiles from OpenStreetMap.
    Dim tileLayer As New ImageLayer() '#CKFK 20181026 Por sugerencia de Visual Studio cambié la clase ImageTilesLayer

    Public Property lDirCliente As New List(Of clsBeCliente_direccion)

    Public Sub New()
        InitializeComponent()
        'RouteLayer.DataProvider = New BingRouteDataProvider() With {.BingKey = bingKey}
    End Sub

    'Private Const bingKey As String = "AoZkOQftuJprFNNQLB5x~OG8m1_5gZz3QlY_S8sGfdQ~AuaUPmTIFkD3si14kY0kOFGvWVZ0MIH0MaP8sy2oU52ewaSyS-SgS03WLnaXce99"
    'Private ReadOnly Property RouteLayer() As InformationLayer
    '    Get
    '        Return CType(MapControl1.Layers(1), InformationLayer)
    '    End Get
    'End Property
    'Private ReadOnly Property RouteProvider() As BingRouteDataProvider
    '    Get
    '        Return CType(RouteLayer.DataProvider, BingRouteDataProvider)
    '    End Get
    'End Property

    Private Sub frmMapa_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try


            tileLayer.DataProvider = New OpenStreetMapDataProvider()
            Map.Layers.Add(tileLayer)
            Map.Layers.Add(itemsLayer)
            Map.CenterPoint = New GeoPoint(14.8, -90)
            Map.ZoomLevel = 5
            Map.Zoom(7)

            GetUbicaciones()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GetUbicaciones()

        Try

            Dim storage As New MapItemStorage()

            For Each D As clsBeCliente_direccion In lDirCliente
                Dim lmap As MapItem = New MapPushpin() With {.Location = New GeoPoint(D.Coordenada_x, D.Coordenada_y), .Text = D.Direccion}
                storage.Items.Add(lmap)
            Next

            itemsLayer.Data = storage

            Map.Refresh()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'Private Function GetUbicaciones() As MapItem()

    '    Dim Pin As MapPushpin = New DevExpress.XtraMap.MapPushpin()
    '    Pin.Location = GP

    '    VectorLayer.Items.Add(Pin)
    '    Poly = New DevExpress.XtraMap.MapPolygon()
    '    Poly.Fill = Color.Red

    '    For Each mp As MapPushpin In VectorLayer.Items
    '        Poly.Points.Add(mp.Location)

    '    Next mp
    '    VectorLayer.Items.Add(Poly)

    '    Try

    '        Dim i As Integer = 0
    '        Dim lmap As MapItem
    '        Dim lPoint As New MapCallout


    '        For Each D As clsBeCliente_direccion In lDirCliente

    '            lPoint = New MapCallout
    '            lPoint.Text = D.Direccion
    '            lPoint.Location = New GeoPoint(D.Coordenada_x, D.Coordenada_y)
    '            lmap(i) = lPoint
    '            i += 1
    '            'Dim Pin As MapPushpin = New DevExpress.XtraMap.MapPushpin()
    '            'Pin.Location = New GeoPoint(D.Coordenada_x, D.Coordenada_y)

    '            'itemsLayer.Items.Add(Pin)
    '            'Poly = New DevExpress.XtraMap.MapPolygon()
    '            ''Poly.Fill = Color.Red

    '            ''For Each mp As MapPushpin In itemsLayer.Items
    '            ''    Poly.Points.Add(mp.Location)
    '            ''Next mp

    '            ''itemsLayer.Items.Add(Poly)

    '        Next

    '        'For Each D As clsBeCliente_direccion In lDirCliente

    '        '    Dim Pin As MapPushpin = New DevExpress.XtraMap.MapPushpin()
    '        '    Pin.Location = New GeoPoint(D.Coordenada_x, D.Coordenada_y)

    '        '    itemsLayer.Items.Add(Pin)
    '        '    Poly = New DevExpress.XtraMap.MapPolygon()
    '        '    'Poly.Fill = Color.Red

    '        '    'For Each mp As MapPushpin In itemsLayer.Items
    '        '    '    Poly.Points.Add(mp.Location)
    '        '    'Next mp

    '        '    'itemsLayer.Items.Add(Poly)

    '        'Next


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Function



End Class

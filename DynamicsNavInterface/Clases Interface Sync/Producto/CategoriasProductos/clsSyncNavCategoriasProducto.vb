Imports System.Reflection
Imports TOMWMS.WSCategoriasProductos

Public Class clsSyncNavCategoriasProducto : Inherits clsInterfaceBase
    Implements IDisposable

    Private fichaCategoriasProductos() As Categorias_Productos

    Private wsCatProdService As New Categorias_Productos_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Function Get_Categoria_Producto_FromWS(ByVal Codigo As String) As Categorias_Productos

        Get_Categoria_Producto_FromWS = Nothing

        Try

            wsCatProdService.Url = My.Settings.DynamicsNavInterface_wsCategoriasProductos_Categorias_Productos_Service
            Return wsCatProdService.Read(Codigo)

        Catch ex As Exception
            '#EJC20180404: Error de permiso de lectura en el wms, deshabilitado temporalmente para sincronizar los productos
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Get_Categorias_Productos_FromWS() As Categorias_Productos()

        Try

            'Dim vFiltro1 As New Productos_Filter() With {.Field = Productos_Fields.Item_Category_Code, .Criteria = "01"}            
            'Dim vFiltros As Productos_Filter() = New Productos_Filter() {vFiltro1, vFiltro2, vFiltro3}

            fichaCategoriasProductos = wsCatProdService.ReadMultiple(Nothing, Nothing, 0)

            Return fichaCategoriasProductos

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    'Private Function Importar_Categorias_Productos_DesdeWSNav_A_TablaIntermedia(ByRef cnn As SqlConnection,
    '                                                                 ByRef ltrans As SqlTransaction,
    '                                                                 ByVal lblprg As RichTextBox,
    '                                                                 ByRef prg As ProgressBar) As Boolean

    '    Importar_Categorias_Productos_DesdeWSNav_A_TablaIntermedia = False

    '    Try

    '        fichaCategoriasProductos = Get_Categorias_Productos_FromWS()

    '        Application.DoEvents()

    '        'Dim BeI_nav_Producto As clsBeI_nav_producto

    '        'lblprg.AppendText(String.Format("Productos encontrados en WS: {0} ", fichaProductos.Count))
    '        'lblprg.SelectionStart = lblprg.TextLength
    '        'lblprg.ScrollToCaret()
    '        'lblprg.Refresh()

    '        'prg.Maximum = fichaProductos.Count

    '        'Dim vContador As Integer = 0

    '        'For Each Prod As Productos In fichaProductos

    '        '    BeI_nav_Producto = New clsBeI_nav_producto
    '        '    BeI_nav_Producto.No = Prod.No
    '        '    BeI_nav_Producto.Description = Prod.Description
    '        '    BeI_nav_Producto.Description_2 = Prod.Description_2
    '        '    BeI_nav_Producto.Inventory = Prod.Inventory
    '        '    BeI_nav_Producto.Base_Unit_Of_Measure = Prod.Base_Unit_of_Measure
    '        '    BeI_nav_Producto.Unit_Cost = Prod.Unit_Cost
    '        '    BeI_nav_Producto.Inventory_Posting_Group = Prod.Inventory_Posting_Group
    '        '    BeI_nav_Producto.Gen_Prod_Posting_Group = Prod.Gen_Prod_Posting_Group
    '        '    BeI_nav_Producto.Search_Description = Prod.Search_Description
    '        '    BeI_nav_Producto.Item_Category_Code = Prod.Item_Category_Code
    '        '    BeI_nav_Producto.Product_Group_Code = Prod.Product_Group_Code
    '        '    BeI_nav_Producto.Sales_Unit = Prod.Sales_Unit_of_Measure
    '        '    BeI_nav_Producto.Item_Tracking_Code = Prod.Item_Tracking_Code

    '        '    lblprg.AppendText(String.Format("Procesando Producto: {0} ", BeI_nav_Producto.No))
    '        '    lblprg.SelectionStart = lblprg.TextLength
    '        '    lblprg.ScrollToCaret()

    '        '    If clsLnI_nav_producto.Exists(BeI_nav_Producto.No, cnn, ltrans) Then
    '        '        lblprg.AppendText(String.Format("Actualizando Producto: {0} ", BeI_nav_Producto.No))
    '        '        lblprg.SelectionStart = lblprg.TextLength
    '        '        lblprg.ScrollToCaret()
    '        '        clsLnI_nav_producto.Actualizar(BeI_nav_Producto, cnn, ltrans)
    '        '    Else
    '        '        lblprg.AppendText(String.Format("Insertando Producto: {0} ", BeI_nav_Producto.No))
    '        '        lblprg.SelectionStart = lblprg.TextLength
    '        '        lblprg.ScrollToCaret()
    '        '        clsLnI_nav_producto.Insertar(BeI_nav_Producto, cnn, ltrans)
    '        '    End If

    '        '    prg.Value = vContador

    '        '    vContador += 1

    '        'Next

    '        Importar_Categorias_Productos_DesdeWSNav_A_TablaIntermedia = True

    '    Catch ex As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    End Try

    'End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
        If wsCatProdService IsNot Nothing Then
            wsCatProdService.Dispose()
            wsCatProdService = Nothing
        End If
    End Sub
#End Region

End Class
Partial Public Class clsBeTrans_packing_enc
    Implements IDisposable

    Private disposedValue As Boolean
    Public Property Nombre_Talla As String = ""
    Public Property Codigo_Talla As String = ""
    Public Property Nombre_Color As String = ""
    ' #EJC20260529 FIX_CUMBRE (PUNTO 2/4) - ENTIDAD SIN CAMPOS DE DISPLAY:
    ' La clase base clsBeTrans_packing_enc.vb solo declaraba IDs numéricos
    ' (Idproductobodega, Idpresentacion, Idunidadmedida, Idproductoestado).
    ' El SOAP WS serializa a XML SOLO lo declarado como Property en la clase.
    ' Sin estas propiedades, Get_All_Packing_By_IdPicking nunca incluía nombre
    ' ni código en la trama XML — llegaban vacíos a la HH.
    ' La HH los rellenaba buscando en pick.items (ubics pendientes), pero ese
    ' workaround se rompió cuando PUNTO 1 filtró las ubics ya empacadas:
    ' si un producto quedaba 100% empacado, desaparecía de pick.items y el
    ' lookup devolvía  → los labels Código y Nombre quedaban en blanco.
    ' FIX: declarar aquí los campos display para que el WS los serialice.
    ' La fuente de datos (JOIN a trans_pe_det) se implementó en PUNTO 3.
    Public Property nom_prod As String = ""
    Public Property CodigoProducto As String = ""
    Public Property ProductoPresentacion As String = ""
    Public Property ProductoUnidadMedida As String = ""
    Public Property ProductoEstado As String = ""
    Public Property Codigo_Color As String = ""

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: eliminar el estado administrado (objetos administrados)
            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    ' ' TODO: reemplazar el finalizador solo si "Dispose(disposing As Boolean)" tiene código para liberar los recursos no administrados
    ' Protected Overrides Sub Finalize()
    '     ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class

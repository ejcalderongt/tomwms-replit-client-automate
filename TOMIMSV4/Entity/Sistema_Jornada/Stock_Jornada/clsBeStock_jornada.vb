Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Cryptography
Imports System.Text

<Serializable>
Public Class clsBeStock_jornada
    Implements ICloneable

    Public Property IdStockJornada() As Integer = 0
    Public Property IdJornadaSistema() As Integer = 0
    Public Property Fecha() As Date = New Date(1900, 1, 1)
    Public Property IdBodega() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdUbicacion_anterior() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdRecepcionDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Serial() As String = ""
    Public Property Cantidad() As Double = 0.0
    ''' <summary>
    ''' #EJC20210519: Esta singularidad, indica que el stock se recibió y se despachó, total o parcialmente y se necesita reflejar que estuvo un período X almacenado.
    ''' Realizado para CEALSA, En el año 2021, un 19 de Mayo. (tengo privado el sábado, que nervios no he podido estudiar jaja)
    ''' </summary>
    ''' <returns></returns>
    Public Property Cantidad_Ingreso_Afecta_A_salida() As Double = 0
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Uds_lic_plate() As Double = 0.0
    Public Property No_bulto() As Integer = 0
    Public Property Fecha_manufactura() As Date = Date.Now
    Public Property Añada() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Peso() As Double = 0.0
    Public Property Temperatura() As Double = 0.0
    Public Property Atributo_variante_1() As String = ""
    Public Property Pallet_no_estandar() As Boolean = False
    Public Property Propietario() As String = ""
    Public Property Proveedor() As String = ""
    Public Property Bodega() As String = ""
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property No_DocumentoOC() As String = ""
    Public Property No_DocumentoRec() As String = ""
    Public Property ReferenciaOC() As String = ""
    Public Property Fecha_Recepcion() As Date = Date.Now
    Public Property TipoTrans() As String = ""
    Public Property Fecha_Agrego() As Date = Date.Now
    Public Property Codigo_producto() As String = ""
    Public Property Codigo_barra_producto() As String = ""
    Public Property Nombre_producto() As String = ""
    Public Property Existencia() As Double = 0.0
    Public Property Nom_umBas() As String = ""
    Public Property Nom_estado_producto() As String = ""
    Public Property Nom_presentacion_producto() As String = ""
    Public Property Ubicacion_origen() As String = ""
    Public Property No_poliza() As String = ""
    Public Property Valor_aduana() As Double = 0.0
    Public Property Valor_fob() As Double = 0.0
    Public Property Valor_iva() As Double = 0.0
    Public Property Valor_dai() As Double = 0.0
    Public Property Valor_seguro() As Double = 0.0
    Public Property Valor_flete() As Double = 0.0
    Public Property Peso_neto() As Double = 0.0
    Public Property Numero_orden() As String = ""
    Public Property Codigo_regimen() As String = ""
    Public Property Nombre_regimen() As String = ""
    Public Property Dias_vencimiento_regimen() As Integer = 0
    Public Property Fecha_Ingreso_Ticket_TMS As Date = Date.Now()
    Public Property Es_Retroactivo As Boolean = False
    Public Property Factor As Double = 0
    Public Property CamasPorTarima As Double = 0
    Public Property CajasPorCama As Double = 0
    Public Property Costo_Unitario As Double = 0
    ''' <summary>
    ''' #EJC20210519: Se utiliza como blockchain para evitar determinar en algún momento de auditoría si existió edición de registro en el retroactivo de CEALSA.
    ''' </summary>
    ''' <returns></returns>
    Public Property Stock_Jornada_Hash As String = ""

    ''' <summary>
    ''' #EJC20210520: De momento no se guarda en la tabla stock_jornada, pero me sirve para hacer la actualización y procesamiento del ticket.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdTicketTMS As Integer = 0
    Public Property IdPropietario As Integer = 0
    Public Property IdClasificacion As Integer = 0
    Public Property Clasificacion As String = ""
    Public Property Regimen As String = ""
    Public Property Posiciones As Integer = 0
    Public Property No_Documento_Procesado_ERP As String = ""
    Public Property Procesado_ERP As Boolean = False

    ''' <summary>
    ''' #CKFK 20120924: De momento no se guarda en la tabla stock_jornada, pero me sirve para un reporte de CEALSA
    ''' </summary>
    ''' <returns></returns>
    Public Property TipoRubro As String = ""

    ''' <summary>
    ''' #CKFK 20120924: De momento no se guarda en la tabla stock_jornada, pero me sirve para un reporte de CEALSA
    ''' </summary>
    ''' <returns></returns>
    Public Property Bultos_Por_Tarima As Double = 0

    Public Property Fecha_Procesado_Stock_Jornada = Date.Now
    Public Property Año As Integer = 0
    Public Property Mes As Integer = 0
    Public Property Dia As Integer = 0

    '#GT16022023:Si la linea de recepción con la LP ya ejecutó retroactivo, activar, para que no ejecute todos los dias 
    'el mismo proceso, es redundante y causa duplicados.
    Public Property Procesado_Retroactivo As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

    Private Shared Function ObjectToByteArray(ByVal obj As Object) As Byte()
        If obj Is Nothing Then Return Nothing
        Dim bf As BinaryFormatter = New BinaryFormatter()
        Using ms As MemoryStream = New MemoryStream()
            bf.Serialize(ms, obj)
            Return ms.ToArray()
        End Using
    End Function

    Public Shared Function GetRecordHash(ByVal pObj As Object) As String

        GetRecordHash = ""

        Try

            Dim hashValue As Byte()
            Dim messageBytes As Byte() = ObjectToByteArray(pObj)
            Dim ue As UnicodeEncoding = New UnicodeEncoding()
            Dim shHash As SHA256 = SHA256.Create()
            hashValue = shHash.ComputeHash(messageBytes)

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To hashValue.Length - 1
                sBuilder.Append(hashValue(n).ToString("X2"))
            Next n

            GetRecordHash = sBuilder.ToString()

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class

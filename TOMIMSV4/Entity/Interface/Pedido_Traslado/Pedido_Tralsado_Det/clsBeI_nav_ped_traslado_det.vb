Public Class clsBeI_nav_ped_traslado_det
    Implements ICloneable

    ''' <summary>
    ''' Gets or sets the no enc.
    ''' </summary>
    ''' <value>The no enc.</value>
    Public Property NoEnc() As String = ""
    ''' <summary>
    ''' Gets or sets the line_ no.
    ''' </summary>
    ''' <value>The line_ no.</value>
    Public Property Line_No() As Integer = 0
    ''' <summary>
    ''' Gets or sets the variant_ code.
    ''' </summary>
    ''' <value>The variant_ code.</value>
    Public Property Variant_Code() As String = ""
    ''' <summary>
    ''' Gets or sets the no.
    ''' </summary>
    ''' <value>The no.</value>
    Public Property No() As String = ""
    ''' <summary>
    ''' Gets or sets the description.
    ''' </summary>
    ''' <value>The description.</value>
    Public Property Description() As String = ""
    ''' <summary>
    ''' Gets or sets the item_ no.
    ''' </summary>
    ''' <value>The item_ no.</value>
    Public Property Item_No() As String = ""
    ''' <summary>
    ''' Gets or sets the qty_to_ receive.
    ''' </summary>
    ''' <value>The qty_to_ receive.</value>
    Public Property Qty_to_Receive() As Decimal = 0.0
    ''' <summary>
    ''' Indica la cantidad disponible a despachar por el WMS.
    ''' </summary>
    ''' <value>The qty_to_ ship.</value>
    Public Property Qty_to_Ship() As Decimal = 0.0
    ''' <summary>
    ''' Gets or sets the quantity.
    ''' </summary>
    ''' <value>The quantity.</value>
    Public Property Quantity() As Decimal = 0.0

    Public Property Quantity_Shipped As Decimal = 0
    ''' <summary>
    ''' Gets or sets the transfer_to_ code field.
    ''' </summary>
    ''' <value>The transfer_to_ code field.</value>
    Public Property Transfer_to_CodeField() As String = ""
    ''' <summary>
    ''' Gets or sets the transfer_to_ code field.
    ''' </summary>
    ''' <value>The transfer_to_ code field.</value>
    Public Property Transfer_From_CodeField() As String = ""
    ''' <summary>
    ''' Gets or sets the shipment_ date.
    ''' </summary>
    ''' <value>The shipment_ date.</value>
    Public Property Shipment_Date() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the unit_of_ measure_ code.
    ''' </summary>
    ''' <value>The unit_of_ measure_ code.</value>
    Public Property Unit_of_Measure_Code() As String = ""

    ''' <summary>
    ''' Indica si el registro de detalle fue procesado en el pedido o no.
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Integer = 0

    ''' <summary>
    ''' Muestra el resultado del procesamiento por la interface MI3 del WMS.
    ''' </summary>
    ''' <returns></returns>
    Public Property Process_Result As String = ""

    Public Property Price As Double = 0

    Public Property Lote_No As String = ""

    Public Property Expiration_Date As Date = New Date(1900, 1, 1)

    ''' <summary>
    ''' #EJC20210614: En el caso de los envíos de NAV indica el número de pedido de venta contenido en el envío.
    ''' </summary>
    ''' <returns></returns>
    Public Property Source_ID As String = ""
    ''' <summary>
    ''' #EJC20220128: Actualizar con que IdPedidoDet fue procesada la línea de solicitud en la interface.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdPedidoDet As Integer = 0

    ''' <summary>
    ''' #EJC20220315: Wrap de campo para manejar la explosión (unidades) de una línea con presentación.
    ''' </summary>
    ''' <returns></returns>
    Public Property Quantity_In_UMBas() As Decimal = 0.0

    Public Property Is_Partially_Processed As Boolean = False

    Public Property Quantity_Reserved_WMS As Double = 0

    Public Property Scan_Type As String = ""
    Public Property Color As String = ""
    Public Property Size As String = ""

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ped_traslado_det"/> class.
    ''' </summary>
    Sub New()
    End Sub


    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ped_traslado_det"/> class.
    ''' </summary>
    ''' <param name="NoEnc">The no enc.</param>
    ''' <param name="No">The no.</param>
    ''' <param name="Description">The description.</param>
    ''' <param name="Item_No">The item_ no.</param>
    ''' <param name="Qty_to_Receive">The qty_to_ receive.</param>
    ''' <param name="Qty_to_Ship">The qty_to_ ship.</param>
    ''' <param name="Quantity">The quantity.</param>
    ''' <param name="transfer_to_CodeField">The transfer_to_ code field.</param>
    ''' <param name="Shipment_Date">The shipment_ date.</param>
    ''' <param name="Unit_of_Measure_Code">The unit_of_ measure_ code.</param>
    Sub New(ByRef NoEnc As String, ByVal No As String, ByVal Description As String, ByVal Item_No As String, ByVal Qty_to_Receive As Decimal, ByVal Qty_to_Ship As Decimal, ByVal Quantity As Decimal, ByVal transfer_to_CodeField As String, ByVal Shipment_Date As Date, ByVal Unit_of_Measure_Code As String)
        Me.NoEnc = NoEnc
        Me.No = No
        Me.Description = Description
        Me.Item_No = Item_No
        Me.Qty_to_Receive = Qty_to_Receive
        Me.Qty_to_Ship = Qty_to_Ship
        Me.Quantity = Quantity
        Me.Transfer_to_CodeField = transfer_to_CodeField
        Me.Shipment_Date = Shipment_Date
        Me.Unit_of_Measure_Code = Unit_of_Measure_Code
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class

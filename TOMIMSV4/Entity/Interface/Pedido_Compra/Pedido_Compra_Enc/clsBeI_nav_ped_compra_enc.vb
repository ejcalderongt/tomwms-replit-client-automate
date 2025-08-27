' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 09-18-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-29-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ped_compra_enc.vb" company="TEAM OS">
'     Copyright  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_ped_compra_enc.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_ped_compra_enc
    Implements ICloneable

    ''' <summary>
    ''' Gets or sets the no.
    ''' </summary>
    ''' <value>The no.</value>
    Public Property No() As String = ""

    ''' <summary>
    ''' Gets or sets the buy_ from_ vendor_ no.
    ''' </summary>
    ''' <value>The buy_ from_ vendor_ no.</value>
    Public Property Buy_From_Vendor_No() As String = ""

    ''' <summary>
    ''' Gets or sets the name of the buy_ from_ vendor_.
    ''' </summary>
    ''' <value>The name of the buy_ from_ vendor_.</value>
    Public Property Buy_From_Vendor_Name() As String = ""

    ''' <summary>
    ''' Gets or sets the posting_ description.
    ''' </summary>
    ''' <value>The posting_ description.</value>
    Public Property Posting_Description() As String = ""

    ''' <summary>
    ''' Gets or sets the posting_ date.
    ''' </summary>
    ''' <value>The posting_ date.</value>
    Public Property Posting_Date() As Date? = Date.Now

    ''' <summary>
    ''' Gets or sets the order_ date.
    ''' </summary>
    ''' <value>The order_ date.</value>
    Public Property Order_Date() As Date? = Date.Now

    ''' <summary>
    ''' Gets or sets the document_ date.
    ''' </summary>
    ''' <value>The document_ date.</value>
    Public Property Document_Date() As Date? = Date.Now

    ''' <summary>
    ''' #EJC202406062666: DocNum en interface SAP, Cumbre, HND.
    ''' </summary>
    ''' <value>The vendor_ invoice_ no.</value>
    Public Property Vendor_Invoice_No() As String = ""

    ''' <summary>
    ''' Gets or sets the status.
    ''' </summary>
    ''' <value>The status.</value>
    Public Property Status() As Object

    ''' <summary>
    ''' Gets or sets the payment_ terms_ code.
    ''' </summary>
    ''' <value>The payment_ terms_ code.</value>
    Public Property Payment_Terms_Code() As String = ""

    ''' <summary>
    ''' Gets or sets the name of the ship_ to_.
    ''' </summary>
    ''' <value>The name of the ship_ to_.</value>
    Public Property Ship_To_Name() As String = ""

    ''' <summary>
    ''' Gets or sets the location_ code.
    ''' </summary>
    ''' <value>The location_ code.</value>
    Public Property Location_Code() As String = ""

    ''' <summary>
    ''' #EJC202406062666: NoContenedor interface SAP, Cumbre, HND.
    ''' </summary>
    ''' <value>The ship_ to_ contact.</value>
    Public Property Ship_To_Contact() As String = ""

    ''' <summary>
    ''' Gets or sets the expected_ receipt_ date.
    ''' </summary>
    ''' <value>The expected_ receipt_ date.</value>
    Public Property Expected_Receipt_Date() As Date? = Now.Date

    ''' <summary>
    ''' Gets or sets a value indicating if the documents is an [transferencia interna].
    ''' From other warehouse pej.
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [transferencia interna]; otherwise, <c>Is a Order from provider</c>.
    ''' </value>
    Public Property Is_Internal_Transfer As Boolean = False

    ''' <summary>
    ''' código de Propietario.
    ''' </summary>
    ''' <returns></returns>
    Public Property Product_Owner_Code As String = ""

    '#EJC20190709: Implementado para el control de transferencias y stock en trónsito.
    ''' <summary>
    ''' número de referencia del pedido de cliente en la bodega origen (Emisora) de merncancías.
    ''' </summary>
    ''' <returns></returns>
    Public Property Internal_Transfer_Document_No As String = ""

    Public Property Document_Type As clsDataContractDI.tTipoDocumentoIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso

    ''' <summary>
    ''' #EJC20231027: Determina si el documento de ingreso es o no una importación para considerar la generación de NC en SAP.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsImport As Boolean = False

    ''' <summary>
    ''' #EJC20240807: Identificador de empresa o sociedad de SAP.
    ''' </summary>
    ''' <returns></returns>
    Public Property Company_Code As String = ""
    ''' <summary>
    ''' #EJC20250428: Identifica el número de campaña (SAP Mampa)
    ''' </summary>
    ''' <returns></returns>
    Public Property Campaign_No As String = ""

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ped_compra_enc"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ped_compra_enc"/> class.
    ''' </summary>
    ''' <param name="No">The no.</param>
    ''' <param name="Buy_From_Vendor_No">The buy_ from_ vendor_ no.</param>
    ''' <param name="Buy_From_Vendor_Name">Name of the buy_ from_ vendor_.</param>
    ''' <param name="Posting_Description">The posting_ description.</param>
    ''' <param name="Posting_Date">The posting_ date.</param>
    ''' <param name="Order_Date">The order_ date.</param>
    ''' <param name="Document_Date">The document_ date.</param>
    ''' <param name="Vendor_Invoice_No">The vendor_ invoice_ no.</param>
    ''' <param name="Status">The status.</param>
    ''' <param name="Payment_Terms_Code">The payment_ terms_ code.</param>
    ''' <param name="Ship_To_Name">Name of the ship_ to_.</param>
    ''' <param name="Location_Code">The location_ code.</param>
    ''' <param name="Ship_To_Contact">The ship_ to_ contact.</param>
    ''' <param name="Expected_Receipt_Date">The expected_ receipt_ date.</param>
    Sub New(ByRef No As String, ByVal Buy_From_Vendor_No As String, ByVal Buy_From_Vendor_Name As String, ByVal Posting_Description As String, ByVal Posting_Date As Date, ByVal Order_Date As Date, ByVal Document_Date As Date, ByVal Vendor_Invoice_No As String, ByVal Status As String, ByVal Payment_Terms_Code As String, ByVal Ship_To_Name As String, ByVal Location_Code As String, ByVal Ship_To_Contact As String, ByVal Expected_Receipt_Date As String)
        Me.No = No
        Me.Buy_From_Vendor_No = Buy_From_Vendor_No
        Me.Buy_From_Vendor_Name = Buy_From_Vendor_Name
        Me.Posting_Description = Posting_Description
        Me.Posting_Date = Posting_Date
        Me.Order_Date = Order_Date
        Me.Document_Date = Document_Date
        Me.Vendor_Invoice_No = Vendor_Invoice_No
        Me.Status = Status
        Me.Payment_Terms_Code = Payment_Terms_Code
        Me.Ship_To_Name = Ship_To_Name
        Me.Location_Code = Location_Code
        Me.Ship_To_Contact = Ship_To_Contact
        Me.Expected_Receipt_Date = Expected_Receipt_Date
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class

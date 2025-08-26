' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 02-12-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 03-13-2018
' ***********************************************************************
' <copyright file="clsBeProducto_pallet.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeProducto_pallet.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeProducto_pallet
    Implements ICloneable
    ''' <summary>
    ''' Gets or sets the identifier pallet.
    ''' </summary>
    ''' <value>The identifier pallet.</value>
    Public Property IdPallet() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier propietario bodega.
    ''' </summary>
    ''' <value>The identifier propietario bodega.</value>
    Public Property IdPropietarioBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto bodega.
    ''' </summary>
    ''' <value>The identifier producto bodega.</value>
    Public Property IdProductoBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier presentacion.
    ''' </summary>
    ''' <value>The identifier presentacion.</value>
    Public Property IdPresentacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier operador bodega.
    ''' </summary>
    ''' <value>The identifier operador bodega.</value>
    Public Property IdOperadorBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier impresora.
    ''' </summary>
    ''' <value>The identifier impresora.</value>
    Public Property IdImpresora() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier recepcion enc.
    ''' </summary>
    ''' <value>The identifier recepcion enc.</value>
    Public Property IdRecepcionEnc() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier recepcion det.
    ''' </summary>
    ''' <value>The identifier recepcion det.</value>
    Public Property IdRecepcionDet() As Integer = 0
    ''' <summary>
    ''' Gets or sets the codigo_barra.
    ''' </summary>
    ''' <value>The codigo_barra.</value>
    Public Property Codigo_Barra() As String = ""

    Public Property Codigo_Producto() As String = ""
    ''' <summary>
    ''' Gets or sets the cantidad.
    ''' </summary>
    ''' <value>The cantidad.</value>
    Public Property Cantidad() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the lote.
    ''' </summary>
    ''' <value>The lote.</value>
    Public Property Lote() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_pallet"/> is impreso.
    ''' </summary>
    ''' <value><c>true</c> if impreso; otherwise, <c>false</c>.</value>
    Public Property Impreso() As Boolean = False
    ''' <summary>
    ''' Gets or sets the reimpresiones.
    ''' </summary>
    ''' <value>The reimpresiones.</value>
    Public Property Reimpresiones() As Integer = 0
    ''' <summary>
    ''' Gets or sets the fecha_vence.
    ''' </summary>
    ''' <value>The fecha_vence.</value>
    Public Property Fecha_vence() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the fecha_ingreso.
    ''' </summary>
    ''' <value>The fecha_ingreso.</value>
    Public Property Fecha_ingreso() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_pallet"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_pallet"/> class.
    ''' </summary>
    Sub New()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_pallet"/> class.
    ''' </summary>
    ''' <param name="IdPallet">The identifier pallet.</param>
    ''' <param name="IdPropietarioBodega">The identifier propietario bodega.</param>
    ''' <param name="IdProductoBodega">The identifier producto bodega.</param>
    ''' <param name="IdPresentacion">The identifier presentacion.</param>
    ''' <param name="IdOperadorBodega">The identifier operador bodega.</param>
    ''' <param name="IdImpresora">The identifier impresora.</param>
    ''' <param name="IdRecepcionEnc">The identifier recepcion enc.</param>
    ''' <param name="codigo_barra">The codigo_barra.</param>
    ''' <param name="cantidad">The cantidad.</param>
    ''' <param name="lote">The lote.</param>
    ''' <param name="Impreso">if set to <c>true</c> [impreso].</param>
    ''' <param name="Reimpresiones">The reimpresiones.</param>
    ''' <param name="fecha_vence">The fecha_vence.</param>
    ''' <param name="fecha_ingreso">The fecha_ingreso.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="IdRecepcionDet">The identifier recepcion det.</param>
    Sub New(ByRef IdPallet As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdPresentacion As Integer, ByVal IdOperadorBodega As Integer, ByVal IdImpresora As Integer, ByVal IdRecepcionEnc As Integer, ByVal codigo_barra As String, ByVal cantidad As Double, ByVal lote As String, ByVal Impreso As Boolean, ByVal Reimpresiones As Integer, ByVal fecha_vence As Date, ByVal fecha_ingreso As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal IdRecepcionDet As Integer)
        Me.IdPallet = IdPallet
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdPresentacion = IdPresentacion
        Me.IdOperadorBodega = IdOperadorBodega
        Me.IdImpresora = IdImpresora
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdRecepcionDet = IdRecepcionDet
        Me.Codigo_barra = codigo_barra
        Me.Cantidad = cantidad
        Me.Lote = lote
        Me.Impreso = Impreso
        Me.Reimpresiones = Reimpresiones
        Me.Fecha_vence = fecha_vence
        Me.Fecha_ingreso = fecha_ingreso
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
    End Sub
    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class

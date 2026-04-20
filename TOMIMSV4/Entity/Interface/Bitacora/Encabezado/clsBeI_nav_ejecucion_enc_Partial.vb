' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 10-31-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 10-31-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ejecucion_enc_Partial.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Partial Public Class clsBeI_nav_ejecucion_enc

    ''' <summary>
    ''' Gets or sets the identifier empresa.
    ''' </summary>
    ''' <value>The identifier empresa.</value>
    Public Property IdEmpresa() As Integer
    ''' <summary>
    ''' Gets or sets the identifier propietario.
    ''' </summary>
    ''' <value>The identifier propietario.</value>
    Public Property IdPropietario() As Integer
    ''' <summary>
    ''' Gets or sets the empresa.
    ''' </summary>
    ''' <value>The empresa.</value>
    Public Property Empresa As String
    ''' <summary>
    ''' Gets or sets the bodega.
    ''' </summary>
    ''' <value>The bodega.</value>
    Public Property Bodega As String
    ''' <summary>
    ''' Gets or sets the propietario.
    ''' </summary>
    ''' <value>The propietario.</value>
    Public Property Propietario As String
    ''' <summary>
    ''' Gets or sets the configuracion.
    ''' </summary>
    ''' <value>The configuracion.</value>
    Public Property Configuracion As String
    ''' <summary>
    ''' Gets or sets the interfaces.
    ''' </summary>
    ''' <value>The interfaces.</value>
    Public Property Interfaces As String
    ''' <summary>
    ''' Gets or sets the registros_ws.
    ''' </summary>
    ''' <value>The registros_ws.</value>
    Public Property registros_ws As Integer
    ''' <summary>
    ''' Gets or sets the registros_ti.
    ''' </summary>
    ''' <value>The registros_ti.</value>
    Public Property registros_ti As Integer
    ''' <summary>
    ''' Gets or sets the registros_wms.
    ''' </summary>
    ''' <value>The registros_wms.</value>
    Public Property registros_wms As Integer
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeI_nav_ejecucion_enc"/> is exitosa_res.
    ''' </summary>
    ''' <value><c>true</c> if exitosa_res; otherwise, <c>false</c>.</value>
    Public Property Exitosa_res As Boolean
    ''' <summary>
    ''' Gets or sets the errores.
    ''' </summary>
    ''' <value>The errores.</value>
    Public Property Errores As String
    ''' <summary>
    ''' Gets or sets the fecha_error.
    ''' </summary>
    ''' <value>The fecha_error.</value>
    Public Property Fecha_error As Date
    ''' <summary>
    ''' Gets or sets the referencia.
    ''' </summary>
    ''' <value>The referencia.</value>
    Public Property Referencia As String
    ''' <summary>
    ''' Gets or sets the identifier bodega.
    ''' </summary>
    ''' <value>The identifier bodega.</value>
    Public Property IdBodega() As Integer = 0

End Class

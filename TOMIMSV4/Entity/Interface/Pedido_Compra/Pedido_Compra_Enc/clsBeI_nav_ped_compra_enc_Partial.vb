' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 09-26-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-28-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ped_compra_enc_Partial.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Partial Public Class clsBeI_nav_ped_compra_enc

    ''' <summary>
    ''' The list of details in resume
    ''' </summary>
    Public Lineas_Detalle As New List(Of clsBeI_nav_ped_compra_det)
    Public Lineas_Detalle_Lotes As New List(Of clsBeI_nav_ped_compra_det_lote)
    Public Lineas_Detalle_Talla_Color As New List(Of clsBeProducto_talla_color)
    Public Property Campaña As New clsBeCampaña

End Class

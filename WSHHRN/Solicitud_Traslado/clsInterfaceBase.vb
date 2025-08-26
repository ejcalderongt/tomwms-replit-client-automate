Public MustInherit Class clsInterfaceBase
    Public Property UsarCredencialesPorDefecto As Boolean = False
    Public Shared Property BeConfigEnc As New clsBeI_nav_config_enc
    Public Shared Property BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
    Public Shared Property BeNavEjecucionDet As New clsBeI_nav_ejecucion_det_error
    Public Shared Property BeNavEjecucionRes As New clsBeI_nav_ejecucion_res
    Public Shared Property BeConfigDet As New clsBeI_nav_config_det
    Public Property ListaDetalleConfigDet As New List(Of clsBeI_nav_config_det)

End Class
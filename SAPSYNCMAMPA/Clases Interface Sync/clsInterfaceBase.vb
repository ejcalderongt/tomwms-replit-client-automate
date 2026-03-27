Imports System.Net
Imports System.Reflection

Public MustInherit Class clsInterfaceBase

    Private vClaveWS As String = My.Settings.clavews '"26qa/06+17*" '#EJC20180531: Se agregó clave al settins del proyecto
    Private vUsuarioWS As String = My.Settings.usuariows

    Public Property CredencialesConexion As New NetworkCredential With
                {.Domain = "byb",
                .UserName = vUsuarioWS,
                .Password = vClaveWS}

    Public Property UsarCredencialesPorDefecto As Boolean = False
    Public Shared Property BeConfigEnc As New clsBeI_nav_config_enc With {.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface}
    Public Shared Property BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
    Public Shared Property BeNavEjecucionDet As New clsBeI_nav_ejecucion_det_error
    Public Shared Property BeNavEjecucionRes As New clsBeI_nav_ejecucion_res
    Public Shared Property BeConfigDet As New clsBeI_nav_config_det

    Public Shared ListaDetalleConfigDet As New List(Of clsBeI_nav_config_det)

    Public Shared Function Ejecutar_Interfaz(ByVal NombreEntidad As String) As Boolean

        Ejecutar_Interfaz = False

        Dim diaActual As Integer = Now.Day
        Dim horaActual As Date = TimeOfDay

        Try

            ListaDetalleConfigDet = clsLnI_nav_config_det.Get_All_By_IdEnc(BD.Instancia.IdConfiguracionInterface, NombreEntidad)

            If ListaDetalleConfigDet.Count > 0 Then

                Dim vBeConfigDet As New clsBeI_nav_config_det
                vBeConfigDet = ListaDetalleConfigDet(0)
                BeConfigDet = vBeConfigDet

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
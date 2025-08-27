Imports System.IO

Public Class clsBeConfiguracion_usuario_det
    Implements ICloneable

    Public Property IdConfiguracionUsuarioDet() As Integer = 0
    Public Property IdConfiguracionUsuarioEnc() As Integer = 0
    Public Property Maquina_Host() As String = ""
    Public Property Maquina_IP() As String = ""
    Public Property Nombre_Template() As String = ""
    Public Property String_Template() As String = ""
    Public Property Stream_Template As Stream = Nothing
    Public Property Fecha_Agregado() As Date = Date.Now
    Public Property Fecha_Modificado() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class

Imports System.Reflection

Partial Public Class clsBeBodega_ubicacion

    '#CKFK 2018-01-03 09:11PM Cree esta variable privada para que el valor se lo asigne la property.
    Private NombreCompleto_ As String = ""
    Public Property Tramo As New clsBeBodega_tramo
    Public Property Sector As New clsBeBodega_sector

    '#CKFK 2018-01-03 09:11PM Le tuve que quitar el ReadOnly a esta property porque no bajaba a la HH
    Public Property NombreCompleto() As String

        Get

            Try

                If Tramo.Descripcion.ToString.Length > 0 Then '#CKFK 20180603 La validación debe ser en relación a la descripción del tramo

                    If Not Tramo Is Nothing Then

                        If Tramo.Es_Rack Then


                            Dim Pos As String = Orientacion_pos
                            Dim Col As String = Right("00" & Indice_x, 2)
                            Dim vNivel As String = Right("00" & Nivel, 2)
                            Dim Rack As String = IIf(Tramo.Descripcion.IndexOf("-") < 0, Tramo.Descripcion, Tramo.Descripcion.Replace("-", ""))
                            Dim Tunel As String = IIf(Tramo.Descripcion.IndexOf("-") < 0, "", "T" & Tramo.Descripcion.Substring(IIf(Tramo.Descripcion.IndexOf("-") < 0, 0, Tramo.Descripcion.IndexOf("-") + 1), 1))
                            Dim ss As String
                            If Tunel = "" Then
                                ss = String.Format("{0} - C{1} - N{2} - {3} - #{4} ", Rack, Col, vNivel, Pos, IdUbicacion)
                            Else
                                ss = String.Format("{0} - C{1} - T{2} - N{3} - {4} - #{5} ", Rack, Col, Tunel, vNivel, Pos, IdUbicacion)
                            End If

                            NombreCompleto_ = ss

                        Else
                            NombreCompleto_ = Tramo.Descripcion & " - #" & IdUbicacion
                        End If

                    Else
                        NombreCompleto_ = Descripcion
                    End If

                Else
                    NombreCompleto_ = IdUbicacion
                End If

                Return NombreCompleto_

            Catch ex As Exception
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End Try
        End Get

        Set(value As String)
            NombreCompleto_ = value
        End Set

    End Property

End Class

Public Class Init

    Public Shared Sub Main(ByVal args() As String)

        Try

            Dim Inicio As New frmImpresiones()

            If args.Length = 1 Then

                BD.Instancia.Indice = args(0)
                IdEmpresa = args(1)
                IdBodega = args(2)
                System.Windows.Forms.Application.Run(Inicio)
                Inicio.Dispose()
            Else
                System.Windows.Forms.Application.Run(Inicio)
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadKey()
        End Try

    End Sub



End Class
